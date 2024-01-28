using ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid.Models;

namespace ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid.Helpers;

internal static class EmailRequestBuilder
{
    internal static SendMailRequest BuildForSingleContact(string to, string subject, string body, string senderEmail)
    {
        return new SendMailRequest
        {
            Personalizations = new List<Personalization>
            {
                new()
                {
                    To = new List<EmailAddress>
                    {
                        new()
                        {
                            Email = to
                        }
                    },
                    Subject = subject
                }
            },
            From = new EmailAddress
            {
                Email = senderEmail
            },
            Content = new List<Content>
            {
                new()
                {
                    Type = "text/html",
                    Value = body
                }
            }
        };
    }
}
