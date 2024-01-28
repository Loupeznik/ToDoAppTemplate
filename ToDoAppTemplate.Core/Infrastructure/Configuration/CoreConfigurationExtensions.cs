using System.Net.Http.Headers;
using ToDoAppTemplate.Domain.Common;
using Fluid.ViewEngine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoAppTemplate.Core.Auth.Services;
using ToDoAppTemplate.Core.Infrastructure.Communication.Email;
using ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid;
using ToDoAppTemplate.Core.Infrastructure.Communication.Email.SendGrid.Settings;
using ToDoAppTemplate.Core.Infrastructure.Communication.Email.Settings;
using ToDoAppTemplate.Core.Infrastructure.Communication.Email.Smtp;
using ToDoAppTemplate.Core.Infrastructure.Security;
using ToDoAppTemplate.Core.Templating;

namespace ToDoAppTemplate.Core.Infrastructure.Configuration;

public static class CoreConfigurationExtensions
{
    public static void AddToDoCore(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
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
            services.AddSingleton(emailConfig.SendGrid);
        }

        if (emailConfig.SenderType is SenderType.None)
        {
            services.AddScoped<IEmailSender, DummyEmailSender>();
        }

        services.AddTemplating(environment);
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

    private static void AddTemplating(this IServiceCollection services, IHostEnvironment hostEnvironment)
    {
        var options = new FluidViewEngineOptions
        {
#if DEBUG
            PartialsFileProvider = new FileProviderMapper(hostEnvironment.ContentRootFileProvider,
                "bin/Debug/net8.0/Templating/Views"),
            ViewsFileProvider = new FileProviderMapper(hostEnvironment.ContentRootFileProvider,
                "bin/Debug/net8.0/Templating/Views"),
#else
            PartialsFileProvider = new FileProviderMapper(hostEnvironment.ContentRootFileProvider, "Templating/Views"),
            ViewsFileProvider = new FileProviderMapper(hostEnvironment.ContentRootFileProvider, "Templating/Views"),
#endif

            LayoutsLocationFormats =
            {
                string.Concat("/Email/{0}", Constants.ViewExtension),
            },

            ViewsLocationFormats =
            {
                string.Concat("/Email/{0}", Constants.ViewExtension)
            },
        };

        services.AddSingleton(options);

        services.AddSingleton<IFluidViewRenderer>(_ => new FluidViewRenderer(options));
        services.AddScoped<TemplatingService>();
    }
}
