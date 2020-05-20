using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide;

namespace PolarisLog.Infra.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly Context _context;

        public LogRepository(Context context)
        {
            _context = context;
        }

        public PagedList<Log> ObterTodos(
            int pageNumber,
            int pageSize,
            Expression<Func<Log, bool>> predicate = null,
            Func<IQueryable<Log>, IOrderedQueryable<Log>> orderBy = null)
        {
            var query = _context.Logs.AsNoTracking();
            query = query.Where(log => log.Origem == null);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return PagedList<Log>.ToPagedList(query, pageNumber, pageSize);
        }

        public async Task<Log> ObterPorId(Guid id)
        {
            return await _context.Logs.FindAsync(id);
        }
        
        public async Task<Log> Adicionar(Log log)
        {
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task Atualizar(Log log)
        {
            _context.Logs.Update(log);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Log log)
        {
            _context.Logs.Remove(log);
            await _context.SaveChangesAsync();
        }
    }
}