using FluentAssertions;
using PolarisLog.Domain.Entities;
using Xunit;

namespace PolarisLog.Tests.Domain.Entities
{
    public class UsuarioTest
    {
        [Fact]
        public void Usuario_DeveAdicionarNomeEEmail()
        {
            var nome = "nome";
            var email = "email@email.com";
            var usuario = new Usuario(nome, email);

            usuario.Id.Should().NotBeEmpty();
            usuario.Nome.Should().Be(nome);
            usuario.Email.Should().Be(email);
        }
    }
}