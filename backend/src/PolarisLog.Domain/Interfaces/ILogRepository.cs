using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide;

namespace PolarisLog.Domain.Interfaces
{
    public interface ILogRepository
    {
        PagedList<Log> ObterTodos(
            int pageNumber,
            int pageSize,
            Expression<Func<Log, bool>> predicate = null);
        Task<Log[]> ObterPorIds(params Guid[] id);
        Task<Log> Adicionar(Log log);
        Task Atualizar(params Log[] logs);
        Task Deletar(params Log[] logs);
    }
}