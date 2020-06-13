using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            Expression<Func<Log, bool>> predicate = null)
        {
            var query = _context.Logs
                .Include(log => log.Usuario)
                .Include(log => log.Ambiente)
                .Include(log => log.Nivel)
                .AsNoTracking();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return PagedList<Log>.ToPagedList(query, pageNumber, pageSize);
        }

        public async Task<Log[]> ObterPorIds(params Guid[] ids)
        {
            return await _context.Logs.Where(log => ids.Contains(log.Id)).ToArrayAsync();
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

        public async Task Deletar(params Log[] logs)
        {
            _context.Logs.RemoveRange(logs);
            await _context.SaveChangesAsync();
        }
    }
}