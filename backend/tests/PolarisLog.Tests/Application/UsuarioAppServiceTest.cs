using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using PolarisLog.Application.Services;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.CommandSide.Commands.Usuario;
using PolarisLog.Domain.Notifications;
using Xunit;

namespace PolarisLog.Tests.Application
{
    public class UsuarioAppServiceTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        
        public UsuarioAppServiceTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }
        
        [Fact]
        public async Task Adicionar_DeveEnviarCommandParaAdicionarUsuario()
        {
            var usuario = new UsuarioViewModel
            {
                Nome = "nome",
                Email = "email@email.com",
                Senha = "senha",
                SenhaConfirmacao = "senha"
            };
            var usuarioAppService = new UsuarioAppService(_mediatorMock.Object, new DomainNotificationHandler());
            await usuarioAppService.Adicionar(usuario);
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<AdicionarNovoUsuarioCommand>(), CancellationToken.None));
        }
    }
}