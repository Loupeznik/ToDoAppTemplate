using DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid.Settings;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email.Smtp.Settings;

namespace DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email.Settings;

internal sealed class EmailConfiguration
{
    public SenderType SenderType { get; init; }
    
    public SmtpConfiguration? Smtp { get; init; }
    
    public SendGridConfiguration? SendGrid { get; init; }
}
