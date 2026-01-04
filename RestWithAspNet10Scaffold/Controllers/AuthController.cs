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
}