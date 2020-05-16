using System;
using System.Threading.Tasks;
using PolarisLog.Application.ViewModels;

namespace PolarisLog.Application.Interfaces
{
    public interface ILogAppService
    {
        Task<Guid> Adicionar(LogViewModel logViewModel);
        Task Arquivar(Guid id);
    }
}