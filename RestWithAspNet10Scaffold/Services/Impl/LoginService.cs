using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using RestWithAspNet10Scaffold.Auth.Configuration;
using RestWithAspNet10Scaffold.Auth.Contract;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class LoginService(
    IUserAuthService userAuthService, 
    IPasswordHasher passwordHasher, 
    ITokenGenerator tokenGenerator, 
    TokenConfig configurations,
    ILogger<LoginService> logger
    ) : ILoginService
{
    private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
    
    public TokenDto? ValidateCredentials(UserDto userDto)
    {
        var user = userAuthService.FindByUserName(userDto.UserName);
        if (user == null) return null;
        
        if (!passwordHasher.Verify(userDto.Password, user.Password)) return null;
        
        return GenerateToken(user);
    }

    private TokenDto GenerateToken(User user, IEnumerable<Claim>? existingClaims = null)
    {
        var claims = existingClaims?.ToList() ?? new List<Claim>
        {
            //new Claim(ClaimTypes.Name, user.UserName),
            //new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("UserId", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };

        var accessToken = tokenGenerator.GenerateAccessToken(claims);
        var refreshToken = tokenGenerator.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(configurations.DaysToExpiry);

        userAuthService.Update(user);

        var createdDate = DateTime.UtcNow;
        var expirationDate = DateTime.UtcNow.AddMinutes(configurations.Minutes);

        logger.LogInformation("Generated tokens for user {UserName} at {Time}", user.UserName, createdDate);
        
        return new TokenDto
        {
            Authenticated = true,
            Created = createdDate.ToString(DATE_FORMAT),
            Expiration = expirationDate.ToString(DATE_FORMAT),
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public TokenDto? ValidateCredentials(TokenDto tokenDto)
    {
        var principal = tokenGenerator.GetPrincipalFromExpiredToken(tokenDto.AccessToken);
        if (principal == null) return null;

        var userName = principal.Identity?.Name;
        if (string.IsNullOrEmpty(userName)) return null;

        var user = userAuthService.FindByUserName(userName);
        if (user == null) return null;

        if (user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return null;

        return GenerateToken(user, principal.Claims);
    }

    public bool RevokeToken(string userName)
    {
        throw new NotImplementedException();
    }

    public AccountCredentialsDto Create(AccountCredentialsDto accountDto)
    {
        throw new NotImplementedException();
    }
}