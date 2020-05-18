using System;
using System.Threading.Tasks;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide;

namespace PolarisLog.Domain.Interfaces
{
    public interface ILogRepository
    {
        PagedList<Log> ObterTodos(int pageNumber, int pageSize);
        Task<Log> ObterPorId(Guid id);
        Task<Log> Adicionar(Log log);
        Task Atualizar(Log log);
        Task Deletar(Log log);
    }
}