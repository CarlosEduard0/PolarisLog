using System;
using System.Threading.Tasks;

namespace PolarisLog.Application.Interfaces
{
    public interface ISessionAppService
    {
        Task<Guid> Login(string email, string senha);
    }
}