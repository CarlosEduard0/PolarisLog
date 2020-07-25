using System;
using System.Net;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using PolarisLog.Domain.Entities;
using PolarisLog.Tests.Helpers;
using PolarisLog.WebApi;
using PolarisLog.WebApi.Payloads.Log;
using Xunit;

namespace PolarisLog.Tests.Controllers
{
    [Collection(nameof(PolarisLogFixtureCollection))]
    public class LogControllerTest
    {
        private readonly PolarisLogFixture<StartupTest> _polarisLogFixture;

        public LogControllerTest(PolarisLogFixture<StartupTest> polarisLogFixture)
        {
            _polarisLogFixture = polarisLogFixture;
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarVazioQuandoNaoExistirLogs()
        {
            await _polarisLogFixture.RealizarLogin();
            _polarisLogFixture.Client.AtribuirToken(_polarisLogFixture.AccessToken);

            var response = await _polarisLogFixture.Client.GetAsync("Logs");
            var logs = await response.Content.ReadAsJsonAsync<Log[]>();

            response.EnsureSuccessStatusCode();
            logs.Should().BeEmpty();
        }

        [Fact]
        public async Task Adicionar_DeveRetornarErroQuandoLogForInvalido()
        {
            await _polarisLogFixture.RealizarLogin();
            _polarisLogFixture.Client.AtribuirToken(_polarisLogFixture.AccessToken);

            var response = await _polarisLogFixture.Client.PostAsJsonAsync("Logs", new CadastrarLogPayload());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarErroQuandoNaoExistirAmbiente()
        {
            await _polarisLogFixture.RealizarLogin();
            _polarisLogFixture.Client.AtribuirToken(_polarisLogFixture.AccessToken);
            var cadastrarLogPayload = new CadastrarLogPayload
            {
                AmbienteId = Guid.NewGuid().ToString(),
                NivelId = Guid.NewGuid().ToString(),
                Titulo = _polarisLogFixture.Faker.Lorem.Word(),
                Descricao = _polarisLogFixture.Faker.Lorem.Paragraph(),
                Origem = _polarisLogFixture.Faker.Internet.Ip()
            };

            var response = await _polarisLogFixture.Client.PostAsJsonAsync("Logs", cadastrarLogPayload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Arquivar_DeveRetornarSucessoQuandoLogExistir()
        {
            await _polarisLogFixture.RealizarLogin();
            _polarisLogFixture.Client.AtribuirToken(_polarisLogFixture.AccessToken);
            var result = await _polarisLogFixture.Client.GetAsync("Ambientes");
            
            var response = await _polarisLogFixture.Client.PutAsync($"Logs/Arquivar/{Guid.NewGuid().ToString()}", null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Arquivar_DeveRetornarErroQuandoLogNaoExistir()
        {
            await _polarisLogFixture.RealizarLogin();
            _polarisLogFixture.Client.AtribuirToken(_polarisLogFixture.AccessToken);

            var response = await _polarisLogFixture.Client.PutAsync($"Logs/Arquivar/{Guid.NewGuid().ToString()}", null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ArquivarPorIds_DeveRetornarErroQuandoLogsNaoExistirem()
        {
            await _polarisLogFixture.RealizarLogin();
            _polarisLogFixture.Client.AtribuirToken(_polarisLogFixture.AccessToken);
            var arquivarLogPayload = new ArquivarLogPayload
            {
                Ids = new[] {Guid.NewGuid()}
            };

            var response = await _polarisLogFixture.Client.PutAsJsonAsync("Logs", arquivarLogPayload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task Deletar_DeveRetornarErroQuandoLogNaoExistir()
        {
            await _polarisLogFixture.RealizarLogin();
            _polarisLogFixture.Client.AtribuirToken(_polarisLogFixture.AccessToken);

            var response = await _polarisLogFixture.Client.DeleteAsync($"Logs/{Guid.NewGuid().ToString()}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task DeletarPorIds_DeveRetornarErroQuandoLogsNaoExistirem()
        {
            await _polarisLogFixture.RealizarLogin();
            _polarisLogFixture.Client.AtribuirToken(_polarisLogFixture.AccessToken);

            var response = await _polarisLogFixture.Client.DeleteAsync($"Logs?ids={Guid.NewGuid().ToString()}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}