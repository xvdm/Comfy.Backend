using Microsoft.AspNetCore.Identity;

namespace Comfy.Domain.Identity;

public sealed class User : IdentityUser<Guid>
{
    public string Name { get; set; } = null!;
}