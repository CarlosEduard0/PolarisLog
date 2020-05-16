using System;
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

        public LogAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Guid> Adicionar(LogViewModel logViewModel)
        {
            var command = new AdicionarNovoLogCommand(logViewModel.Level, logViewModel.Descricao, logViewModel.Origem);
            return await _mediator.Send(command);
        }

        public async Task Arquivar(Guid id)
        {
            var command = new ArquivarLogCommand(id);
            await _mediator.Send(command);
        }
    }
}