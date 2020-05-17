using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using PolarisLog.Domain.CommandSide.CommandHandlers;
using PolarisLog.Domain.CommandSide.Commands.Session;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;
using PolarisLog.Infra;
using PolarisLog.Infra.Repositories;
using PolarisLog.Tests.Helpers.Factories;
using Xunit;

namespace PolarisLog.Tests.Domain.CommandSide
{
    public class SessionCommandHandlerTest
    {
        private readonly Context _context;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly Mock<IMediator> _mediatorMock;

        public SessionCommandHandlerTest()
        {
            _context = ContextFactory.Create();
            _usuarioRepository = new UsuarioRepository(_context);
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task HandlerLogar_DeveRetonarIdDoUsuarioQuandoEmailESenhaForCorretos()
        {
            var usuario = new Usuario("nome", "email@email.com", "senha");
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            
            var command = new LogarCommand(usuario.Email, "senha");
            var commandHandler = new SessionCommandHandler(_mediatorMock.Object, _usuarioRepository);

            var userId = await commandHandler.Handle(command, CancellationToken.None);

            userId.Should().Be(usuario.Id);
        }

        [Fact]
        public async Task HandlerLogar_DeveInvalidarCommandQuandoEmailForNullOuVazio()
        {
            var commandNull = new LogarCommand(null, "senha");
            var commandVazio = new LogarCommand("", "senha");
            var commandHandler = new SessionCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(commandNull, CancellationToken.None);
            await commandHandler.Handle(commandVazio, CancellationToken.None);

            (await commandNull.EhValido()).Should().Be(false);
            commandNull.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "E-mail deve possuir conteúdo");

            (await commandVazio.EhValido()).Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "E-mail deve possuir conteúdo");
        }

        [Fact]
        public async Task HandlerLogar_DeveInvalidarCommandQuandoSenhaForNullOuVazio()
        {
            var commandNull = new LogarCommand("email@email.com", null);
            var commandVazio = new LogarCommand("email@email.com", "");
            var commandHandler = new SessionCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(commandNull, CancellationToken.None);
            await commandHandler.Handle(commandVazio, CancellationToken.None);

            (await commandNull.EhValido()).Should().Be(false);
            commandNull.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Senha deve possuir conteúdo");

            (await commandVazio.EhValido()).Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Senha deve possuir conteúdo");
        }

        [Fact]
        public async Task HandlerLogar_DeveLancarNotificacaoQuandoEmailOuSenhaIncorreto()
        {
            var usuario = new Usuario("nome", "email@email.com", "senha");
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            
            var command = new LogarCommand("email@email.com", "senhaincorreta");
            var commandHandler = new SessionCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(command, CancellationToken.None);
            
            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }

        [Fact]
        public async Task HandlerLogar_DeveLancarNotificacaoQuandoUsuarioNaoForEncontrado()
        {
            var command = new LogarCommand("email@email.com", "senha");
            var commandHandler = new SessionCommandHandler(_mediatorMock.Object, _usuarioRepository);

            await commandHandler.Handle(command, CancellationToken.None);
            
            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }
    }
}