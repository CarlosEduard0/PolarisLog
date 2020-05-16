using System;
using System.Threading.Tasks;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task<Log[]> ObterTodos();
        Task<Log> ObterPorId(Guid id);
        Task<Log> Adicionar(Log log);
        Task Atualizar(Log log);
    }
}