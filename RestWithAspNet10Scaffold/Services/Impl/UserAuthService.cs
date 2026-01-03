using RestWithAspNet10Scaffold.Auth.Contract;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Models;
using RestWithAspNet10Scaffold.Repositories;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class UserAuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ILogger<UserAuthService> logger) : IUserAuthService
{
    public User? FindByUserName(string userName)
    {
        logger.LogInformation($"FindByUserName {userName}");
        return userRepository.FindByUserName(userName);
    }

    public User Create(AccountCredentialsDto dto)
    {
        logger.LogInformation($"Create User {dto}");
        
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        
        var user = new User
        {
            UserName = dto.UserName,
            Password = passwordHasher.Hash(dto.Password),
            FullName = dto.FullName,
            RefreshToken = string.Empty,
            RefreshTokenExpiryTime = null
        };

        return userRepository.Create(user);
    }

    public bool RevokeToken(string userName)
    {
        logger.LogInformation($"RevokeToken {userName}");
        var user = FindByUserName(userName);
        if (user == null) return false;

        user.RefreshToken = string.Empty;
        user.RefreshTokenExpiryTime = null;
        Update(user);
        
        return true;
    }

    public User? Update(User user)
    {
        logger.LogInformation($"Update {user}");
        return userRepository.Update(user);
    }
}