using PolarisLog.Domain.Entities;

namespace PolarisLog.Tests.Helpers.Factories
{
    public class NivelFactory
    {
        public static Nivel Create()
        {
            return new Nivel("Debug");
        }
    }
}