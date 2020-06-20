using Bogus;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Tests.Helpers.Factories
{
    public class NivelFactory
    {
        public static Nivel GerarNivel()
        {
            return new Faker<Nivel>("pt_BR").CustomInstantiator(faker => new Nivel(faker.Lorem.Word()));
        }
    }
}