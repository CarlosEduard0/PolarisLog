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
    public class NivelControllerTest
    {
        private readonly PolarisLogFixture<StartupTest> _polarisLogFixture;

        public NivelControllerTest(PolarisLogFixture<StartupTest> polarisLogFixture)
        {
            _polarisLogFixture = polarisLogFixture;
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarVazioQuandoNaoExistirNiveis()
        {
            await _polarisLogFixture.RealizarLogin();
            _polarisLogFixture.Client.AtribuirToken(_polarisLogFixture.AccessToken);

            var response = await _polarisLogFixture.Client.GetAsync("Niveis");
            var niveis = await response.Content.ReadAsJsonAsync<Nivel[]>();

            response.EnsureSuccessStatusCode();
            niveis.Should().BeEmpty();
        }
    }
}