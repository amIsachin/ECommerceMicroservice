namespace Auth.Core.IdentityEntities;

public class RegisterEntity
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
}
