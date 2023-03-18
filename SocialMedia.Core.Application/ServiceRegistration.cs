using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IPublicacionesService, PublicacionesService>();
            services.AddTransient<IComentariosService, ComentariosService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFriendsService, FriendsService>();
            #endregion
        }
    }
}
