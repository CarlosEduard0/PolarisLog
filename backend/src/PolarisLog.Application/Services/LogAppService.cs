using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.QuerySide.Queries.Log;

namespace PolarisLog.Application.Services
{
    public class LogAppService : ILogAppService
    {
        private readonly IMediator _mediator;

        public LogAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<LogViewModel[]> ObterTodos()
        {
            var query = new ObterTodosOsLogsQuery();
            var logs = await _mediator.Send(query);
            return logs.Select(log => new LogViewModel
            {
                Level = log.Level,
                Descricao = log.Descricao,
                Origem = log.Origem,
                CadastradoEm = log.CadastradoEm
            }).ToArray();
        }

        public async Task<Guid> Adicionar(LogViewModel logViewModel)
        {
            var command = new AdicionarNovoLogCommand(logViewModel.UsuarioId, logViewModel.Level, logViewModel.Descricao, logViewModel.Origem);
            return await _mediator.Send(command);
        }

        public async Task Arquivar(Guid id)
        {
            var command = new ArquivarLogCommand(id);
            await _mediator.Send(command);
        }
    }
}