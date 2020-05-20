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
            Expression<Func<Log, bool>> predicate = null,
            Func<IQueryable<Log>, IOrderedQueryable<Log>> orderBy = null);
        Task<Log> ObterPorId(Guid id);
        Task<Log> Adicionar(Log log);
        Task Atualizar(Log log);
        Task Deletar(Log log);
    }
}