using System.Threading.Tasks;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Adicionar(Usuario usuario);
    }
}