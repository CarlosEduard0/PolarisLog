using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;

namespace PolarisLog.Domain.CommandSide.CommandHandlers
{
    public class LogCommandHandler : IRequestHandler<AdicionarNovoLogCommand>
    {
        private readonly IMediator _mediator;
        private readonly ILogRepository _logRepository;

        public LogCommandHandler(IMediator mediator, ILogRepository logRepository)
        {
            _mediator = mediator;
            _logRepository = logRepository;
        }

        public async Task<Unit> Handle(AdicionarNovoLogCommand request, CancellationToken cancellationToken)
        {
            if (!await ValidarCommando(request)) return Unit.Value;

            var log = new Log(request.Level, request.Descricao, request.Origem);

            await _logRepository.Adicionar(log);
            return Unit.Value;
        }
        
        private async Task<bool> ValidarCommando(Command command)
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