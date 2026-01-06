using RestWithAspNet10Scaffold.Data.Dto.V1;

namespace RestWithAspNet10Scaffold.Services;

public interface ILoginService
{
    TokenDto? ValidateCredentials(UserDto userDto);
    TokenDto? ValidateCredentials(TokenDto tokenDto);
    bool RevokeToken(string userName);
    AccountCredentialsDto Create(AccountCredentialsDto accountDto);
}