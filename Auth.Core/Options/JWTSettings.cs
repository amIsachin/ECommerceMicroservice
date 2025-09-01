namespace Auth.Core.Options;

public class JWTSettings
{
    public string Secret { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public int ExpirationMinutes { get; set; }
}
