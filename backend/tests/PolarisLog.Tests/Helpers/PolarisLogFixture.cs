using System;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using Bogus;
using PolarisLog.Tests.Helpers.Factories;
using PolarisLog.WebApi;
using PolarisLog.WebApi.Payloads.Usuario;
using PolarisLog.WebApi.ViewModels;
using Xunit;

namespace PolarisLog.Tests.Helpers
{
    [CollectionDefinition(nameof(PolarisLogFixtureCollection))]
    public class PolarisLogFixtureCollection : ICollectionFixture<PolarisLogFixture<StartupTest>> {}
    
    public class PolarisLogFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly PolarisLogAppFactory<TStartup> Factory;
        public readonly Faker Faker;
        public HttpClient Client;
        public string AccessToken;

        public PolarisLogFixture()
        {
            Factory = new PolarisLogAppFactory<TStartup>();
            Faker = new Faker("pt_BR");
            Client = Factory.CreateClient();
            CadastrarUsuario().Wait();
        }

        public async Task RealizarLogin()
        {
            var logarPayload = new LogarPayload
            {
                Email = "eduardoazevedo@example.com",
                Senha = "123456"
            };
            var response = await Client.PostAsJsonAsync("Usuarios/Logar", logarPayload);
            response.EnsureSuccessStatusCode();

            var logarViewModel = await response.Content.ReadAsJsonAsync<LogarViewModel>();
            AccessToken = logarViewModel.AccessToken;
        }
        
        public void Dispose()
        {
            Factory.Dispose();
            Client.Dispose();
        }
        
        private async Task CadastrarUsuario()
        {
            var cadastrarUsuarioPayload = new CadastrarUsuarioPayload
            {
                Nome = "Eduardo Azevedo",
                Email = "eduardoazevedo@example.com",
                Senha = "123456",
                SenhaConfirmacao = "123456"
            };

            var response = await Client.PostAsJsonAsync("Usuarios", cadastrarUsuarioPayload);
            response.EnsureSuccessStatusCode();
        }
    }
}