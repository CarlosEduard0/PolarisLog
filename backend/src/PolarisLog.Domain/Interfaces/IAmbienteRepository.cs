using System;
using System.Threading.Tasks;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide;

namespace PolarisLog.Domain.Interfaces
{
    public interface IAmbienteRepository
    {
        PagedList<Ambiente> ObterTodos(int pageNumber, int pageSize);
        Task<Ambiente> ObterPorId(Guid id);
    }
}