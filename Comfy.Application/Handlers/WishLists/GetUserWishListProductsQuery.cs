using AutoMapper;
using Comfy.Application.Common.Constants;
using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.WishLists.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.WishLists;

public sealed record GetUserWishListProductsQuery(Guid UserId) : IRequest<IEnumerable<WishListProductDTO>>;//, IJwtValidation;


public sealed class GetUserWishListProductsQueryHandler : IRequestHandler<GetUserWishListProductsQuery, IEnumerable<WishListProductDTO>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetUserWishListProductsQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WishListProductDTO>> Handle(GetUserWishListProductsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(x => x.WishList)
                .ThenInclude(x => x!.Products)
                    .ThenInclude(x => x.Images.OrderBy(y => y.Id).Take(1))
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null) throw new NotFoundException(LocalizationStrings.User);
        if (user.WishList is null) return new List<WishListProductDTO>();

        var mappedProducts = _mapper.Map<IEnumerable<WishListProductDTO>>(user.WishList.Products);
        return mappedProducts;
    }
} 