using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolarisLog.Domain.CommandSide.CommandHandlers;
using PolarisLog.Domain.CommandSide.Commands.Usuario;
using PolarisLog.Domain.Entities;
using PolarisLog.Infra;
using PolarisLog.Infra.Repositories;
using PolarisLog.Tests.Helpers.Factories;
using Xunit;

namespace PolarisLog.Tests.Domain.CommandSide.CommandHandlers
{
    public class UsuarioCommandHandlerTest
    {
        private readonly Context _context;

        public UsuarioCommandHandlerTest()
        {
            _context = ContextFactory.Create();
        }

        [Fact]
        public async Task Handler_DeveInserirUsuarioComNomeEmailESenha()
        {
            var nome = "nome";
            var email = "email@email.com";
            var senha = "senha";
            var senhaConfirmacao = "senha";
            var passwordHasher = new PasswordHasher<Usuario>();
            var command = new AdicionarNovoUsuarioCommand(nome, email, senha, senhaConfirmacao);
            var commandHandler = new UsuarioCommandHandler(new UsuarioRepository(_context));

            await commandHandler.Handle(command, CancellationToken.None);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync();
            usuario.Nome.Should().Be(nome);
            usuario.Email.Should().Be(email);
            passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, senha)
                .Should().Be(PasswordVerificationResult.Success);
        }
    }
}