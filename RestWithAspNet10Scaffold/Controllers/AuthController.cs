using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Services;

namespace RestWithAspNet10Scaffold.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(
    ILoginService loginService, 
    IUserAuthService userAuthService,
    ILogger<AuthController> logger
    ) : Controller
{
    [HttpPost("signin")]
    [AllowAnonymous]
    public IActionResult SignIn([FromBody] UserDto userDto)
    {
        if (userDto == null || string.IsNullOrWhiteSpace(userDto.UserName) || string.IsNullOrWhiteSpace(userDto.Password))
        {
            logger.LogWarning("SignIn attempt with invalid payload");
            return BadRequest("UserName and Password must be provided");
        }
        var token = loginService.ValidateCredentials(userDto);
        if (token == null)
        {
            logger.LogWarning("Failed login attempt for user {UserName}", userDto.UserName);
            return Unauthorized("Invalid credentials");
        }

        logger.LogInformation("User {UserName} signed in successfully", userDto.UserName);
        return Ok(token);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public IActionResult Refresh([FromBody] TokenDto tokenDto)
    {
        if (tokenDto == null || string.IsNullOrWhiteSpace(tokenDto.AccessToken) ||
            string.IsNullOrWhiteSpace(tokenDto.RefreshToken))
        {
            logger.LogWarning("Refresh attempt with invalid payload");
            return BadRequest("AccessToken and RefreshToken must be provided");
        }

        var token = loginService.ValidateCredentials(tokenDto);
        if (token == null)
        {
            logger.LogWarning("Failed token refresh attempt");
            return Unauthorized("Invalid tokens");
        }

        logger.LogInformation("Token refreshed successfully");
        return Ok(token);
    }
}