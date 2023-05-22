using AutoMapper;
using Comfy.Application.Handlers.Users.DTO;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.Users;

public sealed record GetUserQuery(Guid Id) : IRequest<UserDTO>;


public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public GetUserQueryHandler(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        var userDTO = _mapper.Map<UserDTO>(user);
        return userDTO;
    }
}