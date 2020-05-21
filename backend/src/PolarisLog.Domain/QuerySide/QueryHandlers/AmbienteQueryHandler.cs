using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide.Queries.Ambiente;

namespace PolarisLog.Domain.QuerySide.QueryHandlers
{
    public class AmbienteQueryHandler : 
        IRequestHandler<ObterTodosOsAmbientesQuery, PagedList<Ambiente>>,
        IRequestHandler<ObterAmbientePorIdQuery, Ambiente>
    {
        private readonly IAmbienteRepository _ambienteRepository;

        public AmbienteQueryHandler(IAmbienteRepository ambienteRepository)
        {
            _ambienteRepository = ambienteRepository;
        }
        
        public Task<PagedList<Ambiente>> Handle(ObterTodosOsAmbientesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_ambienteRepository.ObterTodos(request.PageNumber, request.PageSize));
        }

        public async Task<Ambiente> Handle(ObterAmbientePorIdQuery request, CancellationToken cancellationToken)
        {
            if (!await request.EhValido()) return null;

            return await _ambienteRepository.ObterPorId(request.Id);
        }
    }
}