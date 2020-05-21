using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide;

namespace PolarisLog.Infra.Repositories
{
    public class NivelRepository : INivelRepository
    {
        private readonly Context _context;

        public NivelRepository(Context context)
        {
            _context = context;
        }

        public PagedList<Nivel> ObterTodos(int pageNumber, int pageSize)
        {
            return PagedList<Nivel>.ToPagedList(_context.Niveis.AsNoTracking(), pageNumber, pageSize);
        }

        public async Task<Nivel> ObterPorId(Guid id)
        {
            return await _context.Niveis.FindAsync(id);
        }
    }
}