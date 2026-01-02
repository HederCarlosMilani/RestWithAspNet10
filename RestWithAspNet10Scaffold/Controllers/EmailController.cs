using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Services;

namespace RestWithAspNet10Scaffold.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController(ILogger<EmailController> logger, IEmailService emailService)
    : Controller
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult SendSimpleEmail([FromBody] EmailRequestDto emailRequestDto)
    {
        try
        {
            emailService.SendSimpleEmail(emailRequestDto.to, emailRequestDto.subject, emailRequestDto.body);
            return Ok("Email sent successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to send email: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to send email.");
        }
    }
}