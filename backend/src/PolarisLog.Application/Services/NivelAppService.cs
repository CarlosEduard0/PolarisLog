using System.Threading.Tasks;
using MediatR;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide;
using PolarisLog.Domain.QuerySide.Queries.Nivel;

namespace PolarisLog.Application.Services
{
    public class NivelAppService : INivelAppService
    {
        private readonly IMediator _mediator;

        public NivelAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PagedList<Nivel>> ObterTodos(QueryViewModel queryViewModel)
        {
            return await _mediator.Send(new ObterTodosOsNiveisQuery(queryViewModel.PageNumber, queryViewModel.PageSize));
        }
    }
}