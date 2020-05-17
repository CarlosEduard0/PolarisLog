using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.Services;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;
using PolarisLog.Infra.Repositories;
using PolarisLog.WebApi.Services;

namespace PolarisLog.WebApi.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            
            // Token
            services.AddScoped<TokenService>();
            
            // Usuário
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            
            // Session
            services.AddScoped<ISessionAppService, SessionAppService>();
            
            // Log
            services.AddScoped<ILogAppService, LogAppService>();
            services.AddScoped<ILogRepository, LogRepository>();
        }
    }
}