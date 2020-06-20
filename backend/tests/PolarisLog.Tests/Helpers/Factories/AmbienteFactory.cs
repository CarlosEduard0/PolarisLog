using Bogus;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Tests.Helpers.Factories
{
    public class AmbienteFactory
    {
        public static Ambiente GerarAmbiente()
        {
            return new Faker<Ambiente>("pt_BR").CustomInstantiator(faker => new Ambiente(faker.Lorem.Word()));
        }
    }
}