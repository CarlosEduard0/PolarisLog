using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;
using PolarisLog.Domain.QuerySide.Queries.Usuario;

namespace PolarisLog.Domain.CommandSide.CommandHandlers
{
    public class LogCommandHandler : 
        IRequestHandler<AdicionarNovoLogCommand, Guid>,
        IRequestHandler<ArquivarLogCommand, Unit>
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
            
            var log = new Log(request.UsuarioId, request.Level, request.Descricao, request.Origem);

            await _logRepository.Adicionar(log);
            return log.Id;
        }
        
        public async Task<Unit> Handle(ArquivarLogCommand request, CancellationToken cancellationToken)
        {
            if (!await ValidarCommando(request)) return Unit.Value;

            var log = await _logRepository.ObterPorId(request.Id);
            if (log == null)
            {
                await _mediator.Publish(new DomainNotification("log", "Log não encontrado"));
                return Unit.Value;
            }

            if (log.ArquivadoEm != null)
            {
                await _mediator.Publish(new DomainNotification("log", "Log já foi arquivado"));
                return Unit.Value;
            }
            
            log.Arquivar();
            await _logRepository.Atualizar(log);

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