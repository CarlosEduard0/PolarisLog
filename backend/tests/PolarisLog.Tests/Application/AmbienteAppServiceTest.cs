using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using PolarisLog.Application.Services;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.QuerySide.Queries.Ambiente;
using Xunit;

namespace PolarisLog.Tests.Application
{
    public class AmbienteAppServiceTest
    {
        private readonly Mock<IMediator> _mediatorMock;

        public AmbienteAppServiceTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }
        
        [Fact]
        public async Task ObterTodos_DeveEnviarQueryDeObterTodosOsAmbientes()
        {
            var ambienteAppService = new AmbienteAppService(_mediatorMock.Object);
            
            await ambienteAppService.ObterTodos(new QueryViewModel { PageNumber = 1, PageSize = 20 });
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<ObterTodosOsAmbientesQuery>(), CancellationToken.None));
        }
    }
}