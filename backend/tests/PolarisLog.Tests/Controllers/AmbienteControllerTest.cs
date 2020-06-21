using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using PolarisLog.Domain.Entities;
using PolarisLog.Tests.Helpers;
using PolarisLog.WebApi;
using Xunit;

namespace PolarisLog.Tests.Controllers
{
    [Collection(nameof(PolarisLogFixtureCollection))]
    public class AmbienteControllerTest
    {
        private readonly PolarisLogFixture<StartupTest> _polarisLogFixture;

        public AmbienteControllerTest(PolarisLogFixture<StartupTest> polarisLogFixture)
        {
            _polarisLogFixture = polarisLogFixture;
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarVazioQuandoNaoExistirAmbientes()
        {
            await _polarisLogFixture.RealizarLogin();
            _polarisLogFixture.Client.AtribuirToken(_polarisLogFixture.AccessToken);

            var response = await _polarisLogFixture.Client.GetAsync("Ambientes");
            var ambientes = await response.Content.ReadAsJsonAsync<Ambiente[]>();

            response.EnsureSuccessStatusCode();
            ambientes.Should().BeEmpty();
        }
    }
}