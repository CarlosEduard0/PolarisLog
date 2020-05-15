﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.Notifications;

namespace PolarisLog.Application.Interfaces
{
    public interface ILogAppService
    {
        Task<List<DomainNotification>> Adicionar(LogViewModel logViewModel);
    }
}