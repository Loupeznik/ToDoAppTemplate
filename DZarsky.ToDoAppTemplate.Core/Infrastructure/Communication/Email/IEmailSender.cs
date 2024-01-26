namespace DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email;

public interface IEmailSender
{
    public Task Send(string to, string subject, string body);
}
