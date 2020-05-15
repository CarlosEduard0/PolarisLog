using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.CommandSide.Commands.Usuario;
using PolarisLog.Domain.Notifications;

namespace PolarisLog.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IMediator _mediator;
        private readonly DomainNotificationHandler _notifications;

        public UsuarioAppService(IMediator mediator, INotificationHandler<DomainNotification> notifications)
        {
            _mediator = mediator;
            _notifications = (DomainNotificationHandler) notifications;
        }

        public async Task<List<DomainNotification>> Adicionar(Usuario usuario)
        {
            var command = new AdicionarNovoUsuarioCommand(usuario.Nome, usuario.Email, usuario.Senha, usuario.SenhaConfirmacao);
            await _mediator.Send(command);
            return _notifications.ObterNotificacoes();
        }
    }
}