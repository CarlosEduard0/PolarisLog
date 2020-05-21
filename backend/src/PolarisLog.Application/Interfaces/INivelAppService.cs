using System.Threading.Tasks;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.QuerySide;

namespace PolarisLog.Application.Interfaces
{
    public interface INivelAppService
    {
        Task<PagedList<Nivel>> ObterTodos(QueryViewModel logQueryViewModel);
    }
}