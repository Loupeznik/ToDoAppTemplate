namespace DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email.Smtp;

public sealed class SmtpEmailSender : IEmailSender
{
    public Task Send(string to, string subject, string body)
    {
        throw new NotImplementedException();
    }
}
