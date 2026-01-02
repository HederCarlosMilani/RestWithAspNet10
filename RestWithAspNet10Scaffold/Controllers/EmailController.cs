using System.Text.Json;
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
            emailService.SendSimpleEmail(emailRequestDto);
            return Ok("Email sent successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to send email: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to send email.");
        }
    }

    [HttpPost("with-attachment")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SendEmailWithAttachment([FromForm] string emailRequest, 
        [FromForm] FileUploadDto fileUploadDto)
    {
        try
        {
            EmailRequestDto? emailRequestDto = null;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            emailRequestDto = JsonSerializer.Deserialize<EmailRequestDto>(emailRequest, options);
            if (emailRequestDto == null)
            {
                logger.LogError("Email request data is null or invalid.");
                return BadRequest("Email request data is null or invalid.");
            }
            
            await emailService.SendMailWithAttachmentAsync(emailRequestDto, fileUploadDto.File);
            return Ok("Email with attachment sent successfully.");
        }
        catch (ArgumentException argEx)
        {
            logger.LogError($"Invalid argument: {argEx.Message}");
            return BadRequest(argEx.Message);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to send email with attachment: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to send email with attachment.");
        }
    }
}