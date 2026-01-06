using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using RestWithAspNet10Scaffold.Mail.Settings;

namespace RestWithAspNet10Scaffold.Mail;

public class EmailSender(EmailSettings settings, ILogger<EmailSender> logger)
{
    private string _to;
    private string _subject;
    private string _body;
    private readonly List<MailboxAddress> _recipients = new();
    private string? _attachment;
    
    public EmailSender To(string to)
    {
        _to = to;
        _recipients.Clear();
        _recipients.AddRange(ParseRecipients(to));
        return this;
    }
    
    public EmailSender WithSubject(string subject)
    {
        _subject = subject;
        return this;
    }
    
    public EmailSender WithBody(string body)
    {
        _body = body;
        return this;
    }
    
    public EmailSender WithAttachment(string filePath)
    {
        if (File.Exists(filePath))
        {
            _attachment = filePath;
        }
        else
        {
            logger.LogWarning($"File {filePath} does not exist");
        }
        
        return this;
    }
    
    public void Send()
    {
        var message = new MimeMessage();
        
        message.From.Add(new MailboxAddress(settings.From, settings.Username));
        
        message.To.AddRange(_recipients);
        
        message.Subject = _subject ?? settings.Subject;
        
        var bodyBuilder = new BodyBuilder
        {
            TextBody = _body ?? settings.Body ?? ""
        };
        
        if (_attachment != null)
        {
            var fileName = Path.GetFileName(_attachment);
            bodyBuilder.Attachments.Add(fileName, File.ReadAllBytes(_attachment));
        }
        
        message.Body = bodyBuilder.ToMessageBody();

        try
        {
            using var client = new SmtpClient();

            client.Connect(
                settings.SmtpServer,
                settings.SmtpPort,
                settings.Ssl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);

            if (settings.Properties.SmtpAuth)
                client.Authenticate(settings.Username, settings.Password);

            client.Send(message);
            client.Disconnect(true);
            logger.LogInformation("Email Success sent to {Recipients}", string.Join("; ", _recipients));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Failed to send email to {string.Join("; ", _recipients)}: {ex.Message}");
            throw;
        }
        finally
        {
            Reset();
        }
    }

    private void Reset()
    {
        _to = string.Empty;
        _subject = string.Empty;
        _body = string.Empty;
        _attachment = null;
        _recipients.Clear();
    }

    private IEnumerable<MailboxAddress> ParseRecipients(string to)
    {
        var tosWithoutSpaces = to.Replace(" ", "");
        var addresses = tosWithoutSpaces.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
        
        var addressList = new List<MailboxAddress>();
        foreach (var address in addresses)
        {
            try
            {
                addressList.Add(MailboxAddress.Parse(address));
            }
            catch (FormatException)
            {
                logger.LogWarning($"Invalid email address format: {address}");
            }
        }

        return addressList;
    }
}