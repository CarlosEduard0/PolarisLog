using System;
using System.Net;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using PolarisLog.Tests.Helpers;
using PolarisLog.WebApi;
using PolarisLog.WebApi.Payloads.Usuario;
using Xunit;

namespace PolarisLog.Tests.Controllers
{
    [Collection(nameof(PolarisLogFixtureCollection))]
    public class UsuarioControllerTest
    {
        private readonly PolarisLogFixture<StartupTest> _polarisLogFixture;

        public UsuarioControllerTest(PolarisLogFixture<StartupTest> polarisLogFixture)
        {
            _polarisLogFixture = polarisLogFixture;
        }

        [Fact]
        public async Task Adicionar_DeveAdicionarUsuarioComNomeEmailESenha()
        {
            var cadastrarUsuarioPayload = new CadastrarUsuarioPayload
            {
                Nome = "Carlos Eduardo",
                Email = "carloseduardofox8@gmail.com",
                Senha = "123456",
                SenhaConfirmacao = "123456"
            };

            var response = await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios", cadastrarUsuarioPayload);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Adicionar_DeveRetornarErroQuandoUsuarioForInvalido()
        {
            var cadastrarUsuarioPayload = new CadastrarUsuarioPayload();

            var response = await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios", cadastrarUsuarioPayload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Logar_DeveRetornarTokenEUsuario()
        {
            var cadastrarUsuarioPayload = new CadastrarUsuarioPayload
            {
                Nome = "Carlos Eduardo",
                Email = "carloseduardofox8@gmail.com",
                Senha = "123456",
                SenhaConfirmacao = "123456"
            };
            await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios", cadastrarUsuarioPayload);
            var logarPayload = new LogarPayload
            {
                Email = "carloseduardofox8@gmail.com",
                Senha = "123456"
            };

            var response = await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios/Logar", logarPayload);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Logar_DeveRetornarErroQuandoEmailOuSenhaForInvalidos()
        {
            var logarPayload = new LogarPayload
            {
                Email = "carloseduardo@email.com",
                Senha = "12345678"
            };

            var response = await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios/Logar", logarPayload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task RecuperarSenha_DeveRetornarSucesso()
        {
            var cadastrarUsuarioPayload = new CadastrarUsuarioPayload
            {
                Nome = "Carlos Eduardo",
                Email = "carloseduardofox8@gmail.com",
                Senha = "123456",
                SenhaConfirmacao = "123456"
            };
            await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios", cadastrarUsuarioPayload);
            var recuperarSenhaPayload = new RecuperarSenhaPayload
            {
                Email = "carloseduardofox8@gmail.com"
            };

            var response =
                await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios/RecuperarSenha", recuperarSenhaPayload);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task RecuperarSenha_DeveRetornarErroQuandoUsuarioNaoExistir()
        {
            var recuperarSenhaPayload = new RecuperarSenhaPayload
            {
                Email = "emailnaoexiste@example.com"
            };

            var response =
                await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios/RecuperarSenha", recuperarSenhaPayload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ResetarSenha_DeveRetornarErroQuandoUsuarioNaoExistir()
        {
            var resetarSenhaPayload = new ResetarSenhaPayload
            {
                Email = "emailnaoexiste@example.com",
                Senha = "123456",
                Token = Guid.NewGuid().ToString()
            };

            var response = await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios/ResetarSenha", resetarSenhaPayload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ResetarSenha_DeveRetornarErroQuandoTokenForInvalido()
        {
            var cadastrarUsuarioPayload = new CadastrarUsuarioPayload
            {
                Nome = "Carlos Eduardo",
                Email = "carloseduardofox8@gmail.com",
                Senha = "123456",
                SenhaConfirmacao = "123456"
            };
            await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios", cadastrarUsuarioPayload);

            var resetarSenhaPayload = new ResetarSenhaPayload
            {
                Email = cadastrarUsuarioPayload.Email,
                Senha = "123456",
                Token = Guid.NewGuid().ToString()
            };

            var response = await _polarisLogFixture.Client.PostAsJsonAsync("Usuarios/ResetarSenha", resetarSenhaPayload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}