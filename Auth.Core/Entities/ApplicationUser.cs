using Microsoft.AspNetCore.Identity;

namespace Auth.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = null!;
}
