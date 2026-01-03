using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Services;

public interface IUserAuthService
{
    User? FindByUserName(string userName);
    User Create(AccountCredentialsDto dto);
    bool RevokeToken(string userName);
    User? Update(User user);
}