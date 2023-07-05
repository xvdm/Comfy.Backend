using Comfy.Application.Common.Constants;
using Comfy.Application.Common.Exceptions;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.WishLists;

public sealed record RemoveProductFromWishListCommand(Guid UserId, int ProductId) : IRequest;


public sealed class RemoveProductFromWishListCommandHandler : IRequestHandler<RemoveProductFromWishListCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _context;

    public RemoveProductFromWishListCommandHandler(UserManager<User> userManager, IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task Handle(RemoveProductFromWishListCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(x => x.WishList)
                .ThenInclude(x => x!.Products)
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null) throw new NotFoundException(LocalizationStrings.User);

        var product = user.WishList?.Products.FirstOrDefault(x => x.Id == request.ProductId);
        if(product is null) throw new NotFoundException(LocalizationStrings.Product);

        user.WishList?.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
} 