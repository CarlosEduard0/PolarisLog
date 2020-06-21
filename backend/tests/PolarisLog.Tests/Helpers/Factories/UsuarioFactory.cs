using Bogus;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Tests.Helpers.Factories
{
    public class UsuarioFactory
    {
        public static Usuario GerarUsuario()
        {
            return new Faker<Usuario>("pt_BR")
                .CustomInstantiator(faker => new Usuario(faker.Name.FullName(), faker.Internet.Email()));
        }
    }
}