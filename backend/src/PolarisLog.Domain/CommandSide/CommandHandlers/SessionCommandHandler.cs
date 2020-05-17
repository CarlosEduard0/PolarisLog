using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PolarisLog.Domain.CommandSide.Commands.Session;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;

namespace PolarisLog.Domain.CommandSide.CommandHandlers
{
    public class SessionCommandHandler : IRequestHandler<LogarCommand, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IUsuarioRepository _usuarioRepository;

        public SessionCommandHandler(IMediator mediator, IUsuarioRepository usuarioRepository)
        {
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Guid> Handle(LogarCommand request, CancellationToken cancellationToken)
        {
            if (!await ValidarCommando(request)) return Guid.Empty;
            
            var usuario = await _usuarioRepository.ObterPorEmail(request.Email);
            if (usuario == null)
            {
                await _mediator.Publish(new DomainNotification("usuário", "E-mail ou senha inválidos"));
                return Guid.Empty;
            }
            
            var passwordHasher = new PasswordHasher<Usuario>();
            if (passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, request.Senha) ==
                PasswordVerificationResult.Failed)
            {
                await _mediator.Publish(new DomainNotification("usuário", "E-mail ou senha inválidos"));
                return Guid.Empty;
            }
            
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