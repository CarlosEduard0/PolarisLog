using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;

namespace PolarisLog.Infra.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly Context _context;

        public LogRepository(Context context)
        {
            _context = context;
        }

        public async Task<Log[]> ObterTodos()
        {
            return await _context.Logs.ToArrayAsync();
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