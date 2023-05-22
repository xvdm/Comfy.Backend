using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Identity;

namespace Comfy.Application.Handlers.Users.DTO;

public sealed record UserDTO : IMapWith<User>
{
    public string UserName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public bool EmailConfirmed { get; init; }
    public string SecurityStamp { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public bool PhoneNumberConfirmed { get; init; }
    public bool TwoFactorEnabled { get; init; }
    public bool LockoutEnabled { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDTO>();
    }
}