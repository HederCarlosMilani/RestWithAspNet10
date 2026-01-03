using System.Security.Claims;

namespace RestWithAspNet10Scaffold.Auth.Contract;

public interface ITokenGenerator
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}