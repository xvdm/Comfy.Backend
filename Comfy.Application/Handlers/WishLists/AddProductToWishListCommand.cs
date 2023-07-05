using Comfy.Application.Common.Constants;
using Comfy.Application.Common.Exceptions;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using Comfy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.WishLists;

public sealed record AddProductToWishListCommand(Guid UserId, int ProductId) : IRequest, IJwtValidation;


public sealed class AddProductToWishListCommandHandler : IRequestHandler<AddProductToWishListCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public AddProductToWishListCommandHandler(IApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task Handle(AddProductToWishListCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.IsActive && x.Id == request.ProductId, cancellationToken);
        if (product is null) throw new ProductIsNotAvailableException();

        var user = await _userManager.Users
            .Include(x => x.WishList)
                .ThenInclude(x => x!.Products)
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null) throw new NotFoundException(LocalizationStrings.User);

        if (user.WishList is null)
        {
            user.WishList = new WishList
            {
                UserId = request.UserId,
                Products = new List<Product> { product }
            };
        }
        else
        {
            if (user.WishList.Products.Contains(product) == false)
            {
                user.WishList.Products.Add(product);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
} 