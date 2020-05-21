using PolarisLog.Domain.Entities;

namespace PolarisLog.Tests.Helpers.Factories
{
    public class AmbienteFactory
    {
        public static Ambiente Create()
        {
            return new Ambiente("Dev");
        }
    }
}