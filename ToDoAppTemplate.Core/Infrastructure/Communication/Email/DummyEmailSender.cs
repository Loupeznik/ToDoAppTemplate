using Microsoft.Extensions.Logging;

namespace ToDoAppTemplate.Core.Infrastructure.Communication.Email;

public sealed class DummyEmailSender : IEmailSender
{
    private readonly ILogger<DummyEmailSender> _logger;

    public DummyEmailSender(ILogger<DummyEmailSender> logger) => _logger = logger;

    public Task Send(string to, string subject, string body)
    {
        _logger.LogInformation("Attempted to send email, but no provider is configured");

        return Task.CompletedTask;
    }
}
