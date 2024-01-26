using DZarsky.ToDoAppTemplate.Domain.Common;

namespace DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid;

public sealed class SendGridEmailSender : IEmailSender
{
    private readonly HttpClient _httpClient;

    public SendGridEmailSender(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(HttpClients.SendGrid);
    }

    public Task Send(string to, string subject, string body)
    {
        throw new NotImplementedException();
    }
}
