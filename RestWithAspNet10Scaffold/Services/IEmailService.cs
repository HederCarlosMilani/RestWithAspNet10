using RestWithAspNet10Scaffold.Data.Dto.V1;

namespace RestWithAspNet10Scaffold.Services;

public interface IEmailService
{
    void SendSimpleEmail(EmailRequestDto emailRequestDto);
    Task SendMailWithAttachmentAsync(EmailRequestDto emailRequestDto, IFormFile attachment);
}