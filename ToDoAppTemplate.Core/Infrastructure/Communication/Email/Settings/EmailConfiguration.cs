using ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid.Settings;
using ToDoAppTemplate.Core.Infrastructure.Communication.Email.Smtp.Settings;

namespace ToDoAppTemplate.Core.Infrastructure.Communication.Email.Settings;

internal sealed class EmailConfiguration
{
    public SenderType SenderType { get; init; }
    
    public SmtpConfiguration? Smtp { get; init; }
    
    public SendGridConfiguration? SendGrid { get; init; }
}
