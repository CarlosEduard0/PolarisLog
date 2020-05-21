using System;
using System.Threading.Tasks;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide;

namespace PolarisLog.Domain.Interfaces
{
    public interface INivelRepository
    {
        PagedList<Nivel> ObterTodos(int pageNumber, int pageSize);
        Task<Nivel> ObterPorId(Guid id);
    }
}