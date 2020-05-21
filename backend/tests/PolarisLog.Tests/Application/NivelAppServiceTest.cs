using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using PolarisLog.Application.Services;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.QuerySide.Queries.Nivel;
using Xunit;

namespace PolarisLog.Tests.Application
{
    public class NivelAppServiceTest
    {
        private readonly Mock<IMediator> _mediatorMock;

        public NivelAppServiceTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }
        
        [Fact]
        public async Task ObterTodos_DeveEnviarQueryDeObterTodosOsNiveis()
        {
            var nivelAppService = new NivelAppService(_mediatorMock.Object);
            
            await nivelAppService.ObterTodos(new QueryViewModel { PageNumber = 1, PageSize = 20 });
            
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<ObterTodosOsNiveisQuery>(), CancellationToken.None));
        }
    }
}