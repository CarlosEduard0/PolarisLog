using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using PolarisLog.Application.Services;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide.Queries.Log;
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
                AmbienteId = Guid.NewGuid(),
                NivelId = Guid.NewGuid(),
                Titulo = "título",
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
            var logAppService = new LogAppService(_mediatorMock.Object);
            
            await logAppService.ObterTodos(new LogQueryViewModel { PageNumber = 1, PageSize = 20, Origem = null, Descricao = null});
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<ObterTodosOsLogsQuery>(), CancellationToken.None));
        }
    }
}