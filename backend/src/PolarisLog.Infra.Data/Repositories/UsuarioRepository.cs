using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;

namespace PolarisLog.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Context _context;

        public UsuarioRepository(Context context)
        {
            _context = context;
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
    }
}