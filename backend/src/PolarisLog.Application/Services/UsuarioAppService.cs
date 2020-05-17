using System;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.CommandSide.Commands.Usuario;

namespace PolarisLog.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IMediator _mediator;

        public UsuarioAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Guid> Adicionar(UsuarioViewModel usuarioViewModel)
        {
            var command = new AdicionarNovoUsuarioCommand(usuarioViewModel.Nome, usuarioViewModel.Email, usuarioViewModel.Senha, usuarioViewModel.SenhaConfirmacao);
            return await _mediator.Send(command);
        }
    }
}