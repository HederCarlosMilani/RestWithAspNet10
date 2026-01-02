using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Mail;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class EmailService(ILogger<EmailService> logger, EmailSender emailSender) : IEmailService
{
    public void SendSimpleEmail(string to, string subject, string body)
    {
        try
        {
            emailSender
                .To(to)
                .WithSubject(subject)
                .WithBody(body)
                .Send();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error sending email to {to}: {ex.Message}");
            throw;
        }
    }

    public Task SendMailWithAttachmentAsync(EmailRequestDto emailRequestDto, IFormFile attachment)
    {
        throw new NotImplementedException();
    }
}