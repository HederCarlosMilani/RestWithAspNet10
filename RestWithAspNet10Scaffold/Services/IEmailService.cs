using RestWithAspNet10Scaffold.Data.Dto.V1;

namespace RestWithAspNet10Scaffold.Services;

public interface IEmailService
{
    void SendSimpleEmail(string to, string subject, string body);
    Task SendMailWithAttachmentAsync(EmailRequestDto emailRequestDto, IFormFile attachment);
}