using System.Net.Http.Headers;
using DZarsky.ToDoAppTemplate.Core.Auth.Services;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid.Settings;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email.Settings;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Communication.Email.Smtp;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Security;
using DZarsky.ToDoAppTemplate.Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DZarsky.ToDoAppTemplate.Core.Infrastructure.Configuration;

public static class CoreConfigurationExtensions
{
    public static void AddToDoCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuthManager>();
        services.AddScoped<PasswordValidator>();
        services.AddScoped<PasswordHasher>();

        var emailConfig = configuration.GetSection("Email").Get<EmailConfiguration>();

        if (emailConfig is null)
        {
            throw new Exception("Email configuration is missing.");
        }

        if (emailConfig.SenderType is SenderType.Smtp)
        {
            services.AddSingleton(
                emailConfig.Smtp ?? throw new Exception("Smtp configuration is missing."));
            services.AddScoped<IEmailSender, SmtpEmailSender>();
        }

        if (emailConfig.SenderType is SenderType.SendGrid && emailConfig.SendGrid is not null &&
            !string.IsNullOrWhiteSpace(emailConfig.SendGrid.ApiKey))
        {
            services.AddSendGridHttpClient(emailConfig.SendGrid);
            services.AddScoped<IEmailSender, SendGridEmailSender>();
        }
    }

    private static void AddSendGridHttpClient(this IServiceCollection services, SendGridConfiguration configuration)
    {
        if (configuration is null || string.IsNullOrWhiteSpace(configuration.ApiKey))
        {
            throw new Exception("SendGrid configuration is missing.");
        }

        services.AddHttpClient(HttpClients.SendGrid, httpClient =>
        {
            httpClient.BaseAddress = new Uri(configuration.BaseUrl);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", configuration.ApiKey);
        });
    }
}
