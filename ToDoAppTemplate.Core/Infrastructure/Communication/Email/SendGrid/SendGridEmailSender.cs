using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid.Helpers;
using ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid.Settings;
using ToDoAppTemplate.Domain.Common;

namespace ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid;

public sealed class SendGridEmailSender : IEmailSender
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SendGridEmailSender> _logger;
    private readonly string _senderEmail;

    public SendGridEmailSender(IHttpClientFactory factory, ILogger<SendGridEmailSender> logger,
        SendGridConfiguration configuration)
    {
        _httpClient = factory.CreateClient(HttpClients.SendGrid);
        _logger = logger;
        _senderEmail = configuration.SenderEmail;
    }

    public async Task Send(string to, string subject, string body)
    {
        try
        {
            await _httpClient.PostAsJsonAsync("mail/send",
                EmailRequestBuilder.BuildForSingleContact(to, subject, body, _senderEmail));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email: {Message}", ex.Message);
        }
    }
}
