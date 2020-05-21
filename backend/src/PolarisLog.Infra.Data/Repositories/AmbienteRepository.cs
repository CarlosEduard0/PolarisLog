using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide;

namespace PolarisLog.Infra.Repositories
{
    public class AmbienteRepository : IAmbienteRepository
    {
        private readonly Context _context;

        public AmbienteRepository(Context context)
        {
            _context = context;
        }
        
        public PagedList<Ambiente> ObterTodos(int pageNumber, int pageSize)
        {
            return PagedList<Ambiente>.ToPagedList(_context.Ambientes.AsNoTracking(), pageNumber, pageSize);
        }

        public async Task<Ambiente> ObterPorId(Guid id)
        {
            return await _context.Ambientes.FindAsync(id);
        }
    }
}