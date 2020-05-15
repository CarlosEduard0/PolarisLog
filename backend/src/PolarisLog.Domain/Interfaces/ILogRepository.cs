using System.Threading.Tasks;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task<Log> Adicionar(Log log);
    }
}