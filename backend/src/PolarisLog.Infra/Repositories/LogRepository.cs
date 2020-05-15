using System.Threading.Tasks;
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

        public async Task<Log> Adicionar(Log log)
        {
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            return log;
        }
    }
}