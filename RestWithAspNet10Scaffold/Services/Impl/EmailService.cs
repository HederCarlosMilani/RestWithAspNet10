using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Mail;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class EmailService(ILogger<EmailService> logger, EmailSender emailSender) : IEmailService
{
    public void SendSimpleEmail(EmailRequestDto emailRequestDto)
    {
        try
        {
            emailSender
                .To(emailRequestDto.to)
                .WithSubject(emailRequestDto.subject)
                .WithBody(emailRequestDto.body)
                .Send();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error sending email to {emailRequestDto.to}: {ex.Message}");
            throw;
        }
    }

    public async Task SendMailWithAttachmentAsync(EmailRequestDto emailRequestDto, IFormFile? attachment)
    {
        if (attachment == null || attachment.Length == 0)
        {
            logger.LogWarning("Attachment is empty");
            throw new ArgumentException("Attachment is null or empty", nameof(attachment));
        }
        
        string tempFilePath = Path.Combine(Path.GetTempPath(), attachment.FileName);

        try
        {
            await using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await attachment.CopyToAsync(stream);
            }

            emailSender
                .To(emailRequestDto.to)
                .WithSubject(emailRequestDto.subject)
                .WithBody(emailRequestDto.body)
                .WithAttachment(tempFilePath)
                .Send();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error send Email with attachment to file");
            throw;
        }
        finally
        {
            File.Delete(tempFilePath);
        }
    }
}