using System;
using Microsoft.EntityFrameworkCore;
using PolarisLog.Infra;

namespace PolarisLog.Tests.Helpers.Factories
{
    public class ContextFactory
    {
        public static Context Create()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new Context(options);
        }
    }
}