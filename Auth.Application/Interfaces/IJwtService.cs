using Auth.Core.Entities;

namespace Auth.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}
