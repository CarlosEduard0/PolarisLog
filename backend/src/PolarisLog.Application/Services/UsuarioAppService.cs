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

        public async Task<List<DomainNotification>> Adicionar(UsuarioViewModel usuarioViewModel)
        {
            var command = new AdicionarNovoUsuarioCommand(usuarioViewModel.Nome, usuarioViewModel.Email, usuarioViewModel.Senha, usuarioViewModel.SenhaConfirmacao);
            await _mediator.Send(command);
            return _notifications.ObterNotificacoes();
        }
    }
}