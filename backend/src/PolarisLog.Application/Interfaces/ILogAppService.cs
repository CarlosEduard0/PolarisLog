using System;
using System.Threading.Tasks;
using PolarisLog.Application.ViewModels;

namespace PolarisLog.Application.Interfaces
{
    public interface ILogAppService
    {
        Task<LogViewModel[]> ObterTodos();
        Task<Guid> Adicionar(LogViewModel logViewModel);
        Task Arquivar(Guid id);
        Task Deletar(Guid id);
    }
}