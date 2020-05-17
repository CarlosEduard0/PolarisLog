using System;
using System.Threading.Tasks;

namespace PolarisLog.Application.Interfaces
{
    public interface ISessionAppService
    {
        Task<Guid> Logar(string email, string senha);
    }
}