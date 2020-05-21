using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide.Queries.Nivel;

namespace PolarisLog.Domain.QuerySide.QueryHandlers
{
    public class NivelQueryHandler :
        IRequestHandler<ObterTodosOsNiveisQuery, PagedList<Nivel>>,
        IRequestHandler<ObterNivelPorIdQuery, Nivel>
    {
        private readonly INivelRepository _nivelRepository;

        public NivelQueryHandler(INivelRepository nivelRepository)
        {
            _nivelRepository = nivelRepository;
        }

        public Task<PagedList<Nivel>> Handle(ObterTodosOsNiveisQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_nivelRepository.ObterTodos(request.PageNumber, request.PageSize));
        }
        
        public async Task<Nivel> Handle(ObterNivelPorIdQuery request, CancellationToken cancellationToken)
        {
            if (!await request.EhValido()) return null;

            return await _nivelRepository.ObterPorId(request.Id);
        }
    }
}