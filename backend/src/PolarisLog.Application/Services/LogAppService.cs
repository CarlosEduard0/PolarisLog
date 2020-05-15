using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.Notifications;

namespace PolarisLog.Application.Services
{
    public class LogAppService : ILogAppService
    {
        private readonly IMediator _mediator;
        private readonly DomainNotificationHandler _notificationHandler;

        public LogAppService(IMediator mediator, INotificationHandler<DomainNotification> notificationHandler)
        {
            _mediator = mediator;
            _notificationHandler = (DomainNotificationHandler) notificationHandler;
        }

        public async Task<List<DomainNotification>> Adicionar(LogViewModel logViewModel)
        {
            var command = new AdicionarNovoLogCommand(logViewModel.Level, logViewModel.Descricao, logViewModel.Origem);
            await _mediator.Send(command);
            return _notificationHandler.ObterNotificacoes();
        }
    }
}