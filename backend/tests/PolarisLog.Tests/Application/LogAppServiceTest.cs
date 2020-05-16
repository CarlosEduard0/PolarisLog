using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using PolarisLog.Application.Services;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide.Queries.Log;
using PolarisLog.Infra;
using PolarisLog.Tests.Helpers.Factories;
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
        public async Task Adicionar_DeveEnviarCommandDeAdicionarLog()
        {
            var logViewModel = new LogViewModel
            {
                UsuarioId = Guid.NewGuid(),
                Level = Level.Verbose,
                Descricao = "descrição",
                Origem = "0.0.0.0"
            };
            var logAppService = new LogAppService(_mediatorMock.Object);

            await logAppService.Adicionar(logViewModel);
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<AdicionarNovoLogCommand>(), CancellationToken.None));
        }
        
        [Fact]
        public async Task Arquivar_DeveEnviarCommandDeArquivarLog()
        {
            var logAppService = new LogAppService(_mediatorMock.Object);

            await logAppService.Arquivar(Guid.NewGuid());
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<ArquivarLogCommand>(), CancellationToken.None));
        }
        
        [Fact]
        public async Task Deletar_DeveEnviarCommandDeDeletarLog()
        {
            var logAppService = new LogAppService(_mediatorMock.Object);

            await logAppService.Deletar(Guid.NewGuid());
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<DeletarLogCommand>(), CancellationToken.None));
        }
        
        [Fact]
        public async Task ObterTodos_DeveEnviarQueryDeObterTodosOsLogs()
        {
            _mediatorMock
                .Setup(mediator => mediator.Send(It.IsAny<ObterTodosOsLogsQuery>(), CancellationToken.None))
                .Returns(async () => await Task.Run(() => new[] {LogFactory.Create()}));
            var logAppService = new LogAppService(_mediatorMock.Object);

            var logs = await logAppService.ObterTodos();
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<ObterTodosOsLogsQuery>(), CancellationToken.None));
            logs.Should().HaveCount(1);
            logs.First().CadastradoEm.Should().BeCloseTo(DateTime.Now.ToUniversalTime(), 1000);
        }
    }
}