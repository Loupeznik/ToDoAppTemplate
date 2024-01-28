using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using ToDoAppTemplate.Core.Infrastructure.Communication.Email.Smtp.Settings;

namespace ToDoAppTemplate.Core.Infrastructure.Communication.Email.Smtp;

public sealed class SmtpEmailSender : IEmailSender
{
    private readonly SmtpConfiguration _configuration;
    private readonly ILogger<SmtpEmailSender> _logger;

    public SmtpEmailSender(SmtpConfiguration configuration, ILogger<SmtpEmailSender> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task Send(string to, string subject, string body)
    {
        using var client = new SmtpClient(_configuration.Host, _configuration.Port);
        client.Credentials = new NetworkCredential(_configuration.Username, _configuration.Password);
        client.EnableSsl = _configuration.Port == 465;

        var message = new MailMessage(_configuration.Username, to, subject, body)
        {
            IsBodyHtml = true
        };

        try
        {
            await client.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email: {Message}", ex.Message);
        }
    }
}
