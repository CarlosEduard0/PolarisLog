using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using PolarisLog.Application.Services;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.Entities;
using Xunit;

namespace PolarisLog.Tests.Application
{
    public class LogAppServiceTest
    {
        private readonly Mock<IMediator> _mediatorMock;

        public LogAppServiceTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }
        
        [Fact]
        public async Task Adicionar_DeveEnviarCommandParaAdicionarLog()
        {
            var logViewModel = new LogViewModel
            {
                Level = Level.Verbose,
                Descricao = "descrição",
                Origem = "0.0.0.0"
            };
            var logAppService = new LogAppService(_mediatorMock.Object);

            await logAppService.Adicionar(logViewModel);
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<AdicionarNovoLogCommand>(), CancellationToken.None));
        }
        
        [Fact]
        public async Task Arquivar_DeveEnviarCommandParaArquivarLog()
        {
            var logAppService = new LogAppService(_mediatorMock.Object);

            await logAppService.Arquivar(Guid.NewGuid());
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<ArquivarLogCommand>(), CancellationToken.None));
        }
    }
}