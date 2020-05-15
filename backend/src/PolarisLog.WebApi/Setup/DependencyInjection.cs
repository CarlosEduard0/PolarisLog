using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.Services;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;
using PolarisLog.Infra.Repositories;

namespace PolarisLog.WebApi.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            
            // Usuário
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}