using System;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Application.Interfaces;
using PolarisLog.Domain.CommandSide.Commands.Session;

namespace PolarisLog.Application.Services
{
    public class SessionAppService : ISessionAppService
    {
        private readonly IMediator _mediator;

        public SessionAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Guid> Login(string email, string senha)
        {
            var command = new LoginCommand(email, senha);
            return await _mediator.Send(command);
        }
    }
}