using System.Net.Http.Json;
using DZarsky.ToDoAppTemplate.Domain.Common;

namespace DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid;

public sealed class SendGridEmailSender : IEmailSender
{
    private readonly HttpClient _httpClient;

    public SendGridEmailSender(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(HttpClients.SendGrid);
    }

    public async Task Send(string to, string subject, string body)
    {
        // TODO: Add email model, add logging
        await _httpClient.PostAsJsonAsync("mail/send ", new
        {
            personalizations = new[]
            {
                new
                {
                    to = new[]
                    {
                        new
                        {
                            email = to
                        }
                    },
                    subject
                }
            },
            from = new
            {
                email = "contact@dzarsky.eu",
            },
            content = new[]
            {
                new
                {
                    type = "text/html",
                    value = body
                }
            }
        });
    }
}
