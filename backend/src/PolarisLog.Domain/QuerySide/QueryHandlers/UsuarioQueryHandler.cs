using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide.Queries.Usuario;

namespace PolarisLog.Domain.QuerySide.QueryHandlers
{
    public class UsuarioQueryHandler : IRequestHandler<ObterUsuarioPorIdQuery, Usuario>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Handle(ObterUsuarioPorIdQuery request, CancellationToken cancellationToken)
        {
            if (!await request.EhValido()) return null;

            return await _usuarioRepository.ObterPorId(request.Id);
        }
    }
}