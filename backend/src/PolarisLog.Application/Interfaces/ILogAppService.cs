using System;
using System.Threading.Tasks;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide;

namespace PolarisLog.Application.Interfaces
{
    public interface ILogAppService
    {
        Task<PagedList<Log>> ObterTodos(QueryViewModel queryViewModel);
        Task<Guid> Adicionar(LogViewModel logViewModel);
        Task Arquivar(Guid id);
        Task Deletar(Guid id);
    }
}