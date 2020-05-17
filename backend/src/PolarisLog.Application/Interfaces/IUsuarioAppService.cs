using System;
using System.Threading.Tasks;
using PolarisLog.Application.ViewModels;

namespace PolarisLog.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        public Task<Guid> Adicionar(UsuarioViewModel usuarioViewModel);
    }
}