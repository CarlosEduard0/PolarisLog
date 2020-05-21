using System;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Tests.Helpers.Factories
{
    public class LogFactory
    {
        public static Log Create()
        {
            return new Log(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "título", "descrição", "0.0.0.0");
        }
    }
}