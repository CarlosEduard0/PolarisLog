using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using PolarisLog.Application.Services;
using PolarisLog.Domain.CommandSide.Commands.Session;
using PolarisLog.Tests.Helpers.Factories;
using Xunit;

namespace PolarisLog.Tests.Application
{
    public class SessionAppServiceTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        
        public SessionAppServiceTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }
        
        [Fact]
        public async Task Logar_DeveEnviarCommandParaLogarUsuario()
        {
            var usuario = UsuarioFactory.Create();
            var sessionAppService = new SessionAppService(_mediatorMock.Object);
            
            await sessionAppService.Logar(usuario.Email, usuario.Senha);
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<LogarCommand>(), CancellationToken.None));
        }
    }
}