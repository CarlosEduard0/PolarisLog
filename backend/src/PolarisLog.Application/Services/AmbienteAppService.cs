using System.Threading.Tasks;
using MediatR;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide;
using PolarisLog.Domain.QuerySide.Queries.Ambiente;

namespace PolarisLog.Application.Services
{
    public class AmbienteAppService : IAmbienteAppService
    {
        private readonly IMediator _mediator;

        public AmbienteAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PagedList<Ambiente>> ObterTodos(QueryViewModel queryViewModel)
        {
            return await _mediator.Send(new ObterTodosOsAmbientesQuery(queryViewModel.PageNumber, queryViewModel.PageSize));
        }
    }
}