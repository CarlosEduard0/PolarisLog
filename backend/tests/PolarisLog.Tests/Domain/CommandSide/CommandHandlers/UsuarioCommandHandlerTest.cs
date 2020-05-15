using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using PolarisLog.Domain.CommandSide.CommandHandlers;
using PolarisLog.Domain.CommandSide.Commands.Usuario;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;
using PolarisLog.Infra;
using PolarisLog.Infra.Repositories;
using PolarisLog.Tests.Helpers.Factories;
using Xunit;

namespace PolarisLog.Tests.Domain.CommandSide.CommandHandlers
{
    public class UsuarioCommandHandlerTest
    {
        private readonly Context _context;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly Mock<IMediator> _mediatorMock;

        public UsuarioCommandHandlerTest()
        {
            _context = ContextFactory.Create();
            _usuarioRepository = new UsuarioRepository(_context);
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Handler_DeveAdicionarUsuarioComNomeEmailESenha()
        {
            var nome = "nome";
            var email = "email@email.com";
            var senha = "senha";
            var senhaConfirmacao = "senha";
            var passwordHasher = new PasswordHasher<Usuario>();
            var command = new AdicionarNovoUsuarioCommand(nome, email, senha, senhaConfirmacao);
            var commandHandler = new UsuarioCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync();
            usuario.Nome.Should().Be(nome);
            usuario.Email.Should().Be(email);
            passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, senha)
                .Should().Be(PasswordVerificationResult.Success);
        }

        [Fact]
        public async Task Handler_LancarNotificacaoQuandoCommandForInvalido()
        {
            var command = new AdicionarNovoUsuarioCommand(null, "email@email.com", "senha", "senha");
            var commandHandler = new UsuarioCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(command, CancellationToken.None);
            
            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }

        [Fact]
        public async Task Handler_DeveInvalidarCommandQuandoNomeForNullOuVazio()
        {
            var commandNull = new AdicionarNovoUsuarioCommand(null, "email@email.com", "senha", "senha");
            var commandVazio = new AdicionarNovoUsuarioCommand("", "email@email.com", "senha", "senha");
            var commandHandler = new UsuarioCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(commandNull, CancellationToken.None);
            await commandHandler.Handle(commandVazio, CancellationToken.None);

            commandNull.ValidationResult.IsValid.Should().Be(false);
            commandNull.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Nome deve possuir conteúdo");

            commandVazio.ValidationResult.IsValid.Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Nome deve possuir conteúdo");
        }

        [Fact]
        public async Task Handler_DeveInvalidarCommandQuandoEmailForNullOuVazio()
        {
            var commandNull = new AdicionarNovoUsuarioCommand("nome", null, "senha", "senha");
            var commandVazio = new AdicionarNovoUsuarioCommand("nome", "", "senha", "senha");
            var commandHandler = new UsuarioCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(commandNull, CancellationToken.None);
            await commandHandler.Handle(commandVazio, CancellationToken.None);

            commandNull.ValidationResult.IsValid.Should().Be(false);
            commandNull.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "E-mail deve possuir conteúdo");

            commandVazio.ValidationResult.IsValid.Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "E-mail deve possuir conteúdo");
        }

        [Fact]
        public async Task Handler_DeveInvalidarCommandQuandoSenhaForNullOuVazio()
        {
            var commandNull = new AdicionarNovoUsuarioCommand("nome", "email@email.com", null, "senha");
            var commandVazio = new AdicionarNovoUsuarioCommand("nome", "email@email.com", "", "senha");
            var commandHandler = new UsuarioCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(commandNull, CancellationToken.None);
            await commandHandler.Handle(commandVazio, CancellationToken.None);

            commandNull.ValidationResult.IsValid.Should().Be(false);
            commandNull.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Senha deve possuir conteúdo");

            commandVazio.ValidationResult.IsValid.Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Senha deve possuir conteúdo");
        }

        [Fact]
        public async Task Handler_DeveInvalidarCommandQuandoSenhasNaoCoincidem()
        {
            var command = new AdicionarNovoUsuarioCommand("nome", "email@email.com", "senha", "outrasenha");
            var commandHandler = new UsuarioCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            command.ValidationResult.IsValid.Should().Be(false);
            command.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "As senhas não coincidem");
        }

        [Fact]
        public async Task Handler_DeveInvalidarCommandQuandoNomeForMaiorQue50Caracteres()
        {
            var nome = "Nome com mais de 50 caracteres Nome com mais de 50 ";
            var command = new AdicionarNovoUsuarioCommand(nome, "email@email.com", "senha", "senha");
            var commandHandler = new UsuarioCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            command.ValidationResult.IsValid.Should().Be(false);
            command.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Nome deve ter no máximo 50 caracteres");
        }

        [Fact]
        public async Task Handler_DeveInvalidarCommandQuandoEmailForMaiorQue50Caracteres()
        {
            var email = "emailcommaisde50caracteres@emailcommaisde50cara.ter";
            var command = new AdicionarNovoUsuarioCommand("nome", email, "senha", "senha");
            var commandHandler = new UsuarioCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            command.ValidationResult.IsValid.Should().Be(false);
            command.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "E-mail deve ter no máximo 50 caracteres");
        }
    }
}