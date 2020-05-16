using PolarisLog.Domain.Entities;

namespace PolarisLog.Tests.Helpers.Factories
{
    public class LogFactory
    {
        public static Log Create()
        {
            return new Log(Level.Verbose, "descrição", "0.0.0.0");
        }
    }
}