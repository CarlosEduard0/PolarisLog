using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.CommandSide.Commands.Usuario;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;

namespace PolarisLog.Domain.CommandSide.CommandHandlers
{
    public class UsuarioCommandHandler : IRequestHandler<AdicionarNovoUsuarioCommand, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCommandHandler(IMediator mediator, IUsuarioRepository usuarioRepository)
        {
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Guid> Handle(AdicionarNovoUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!await ValidarCommando(request)) return Guid.Empty;

            if (await _usuarioRepository.ObterPorEmail(request.Email) != null)
            {
                await _mediator.Publish(new DomainNotification("usuário", "Já existe um usuário cadastrado com esse email"));
                return Guid.Empty;
            }
            
            var usuario = new Usuario(request.Nome, request.Email, request.Senha);

            await _usuarioRepository.Adicionar(usuario);
            
            return usuario.Id;
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