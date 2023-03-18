using SocialMedia.Infrastucture.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Core.Domain.Settings;
using SocialMedia.Core.Application.Interfaces.Services;

namespace SocialMedia.Infrastucture.Shared.Services
{

    public static class ServiceRegistration
    {
        public static void AddShareInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
