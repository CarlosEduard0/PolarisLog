using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;
using PolarisLog.Domain.QuerySide.Queries.Ambiente;
using PolarisLog.Domain.QuerySide.Queries.Nivel;
using PolarisLog.Domain.QuerySide.Queries.Usuario;

namespace PolarisLog.Domain.CommandSide.CommandHandlers
{
    public class LogCommandHandler : 
        IRequestHandler<AdicionarNovoLogCommand, Guid>,
        IRequestHandler<ArquivarLogCommand, Unit>,
        IRequestHandler<DeletarLogCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly ILogRepository _logRepository;

        public LogCommandHandler(IMediator mediator, ILogRepository logRepository)
        {
            _mediator = mediator;
            _logRepository = logRepository;
        }

        public async Task<Guid> Handle(AdicionarNovoLogCommand request, CancellationToken cancellationToken)
        {
            if (!await ValidarCommando(request)) return Guid.Empty;

            if (await _mediator.Send(new ObterUsuarioPorIdQuery(request.UsuarioId)) == null)
            {
                await _mediator.Publish(new DomainNotification("usuario", "Usuário não encontrado"));
                return Guid.Empty;
            }
            
            if (await _mediator.Send(new ObterAmbientePorIdQuery(request.AmbienteId)) == null)
            {
                await _mediator.Publish(new DomainNotification("ambiente", "Ambiente não encontrado"));
                return Guid.Empty;
            }
            
            if (await _mediator.Send(new ObterNivelPorIdQuery(request.NivelId)) == null)
            {
                await _mediator.Publish(new DomainNotification("nivel", "Nível não encontrado"));
                return Guid.Empty;
            }
            
            var log = new Log(request.UsuarioId, request.AmbienteId, request.NivelId, request.Titulo, request.Descricao, request.Origem);

            await _logRepository.Adicionar(log);
            
            return log.Id;
        }
        
        public async Task<Unit> Handle(ArquivarLogCommand request, CancellationToken cancellationToken)
        {
            if (!await ValidarCommando(request)) return Unit.Value;

            var logs = await _logRepository.ObterPorIds(request.Ids);
            if (!logs.Any())
            {
                await _mediator.Publish(new DomainNotification("log", "Log não encontrado"));
                return Unit.Value;
            }
            
            var logsNaoArquivados = logs.Where(log => log.ArquivadoEm == null).ToList();
            logsNaoArquivados.ForEach(log => log.Arquivar());
            
            await _logRepository.Atualizar(logsNaoArquivados.ToArray());

            return Unit.Value;
        }
        
        
        public async Task<Unit> Handle(DeletarLogCommand request, CancellationToken cancellationToken)
        {
            if (!await ValidarCommando(request)) return Unit.Value;
            
            var logs = await _logRepository.ObterPorIds(request.Ids);
            if (!logs.Any())
            {
                await _mediator.Publish(new DomainNotification("log", "Log não encontrado"));
                return Unit.Value;
            }
            
            await _logRepository.Deletar(logs);

            return Unit.Value;
        }
        
        private async Task<bool> ValidarCommando<T>(Command<T> command)
        {
            if (await command.EhValido()) return true;

            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediator.Publish(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
            
            return false;
        }
    }
}