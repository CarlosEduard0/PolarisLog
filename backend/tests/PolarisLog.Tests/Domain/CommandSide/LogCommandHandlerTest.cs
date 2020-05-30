using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using PolarisLog.Domain.CommandSide.CommandHandlers;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;
using PolarisLog.Domain.QuerySide.Queries.Ambiente;
using PolarisLog.Domain.QuerySide.Queries.Nivel;
using PolarisLog.Domain.QuerySide.Queries.Usuario;
using PolarisLog.Infra;
using PolarisLog.Infra.Repositories;
using PolarisLog.Tests.Helpers.Factories;
using Xunit;

namespace PolarisLog.Tests.Domain.CommandSide
{
    public class LogCommandHandlerTest
    {
        private readonly Context _context;
        private readonly ILogRepository _logRepository;
        private readonly Mock<IMediator> _mediatorMock;

        public LogCommandHandlerTest()
        {
            _context = ContextFactory.Create();
            _logRepository = new LogRepository(_context);
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task HandlerAdicionar_DeveAdicionarLogComUsuarioAmbienteNivelTituloDescricaoEOrigem()
        {
            var usuario = UsuarioFactory.Create();
            var ambiente = AmbienteFactory.Create();
            var nivel = NivelFactory.Create();
            var titulo = "título";
            var descricao = "descrição";
            var origem = "0.0.0.0";

            await _context.AddRangeAsync(usuario, ambiente, nivel);
            await _context.SaveChangesAsync();

            _mediatorMock
                .Setup(mediator => mediator.Send(It.IsAny<ObterUsuarioPorIdQuery>(), CancellationToken.None))
                .Returns(async () => await Task.Run(() => usuario));

            _mediatorMock
                .Setup(mediator => mediator.Send(It.IsAny<ObterAmbientePorIdQuery>(), CancellationToken.None))
                .Returns(async () => await Task.Run(() => ambiente));

            _mediatorMock
                .Setup(mediator => mediator.Send(It.IsAny<ObterNivelPorIdQuery>(), CancellationToken.None))
                .Returns(async () => await Task.Run(() => nivel));
            
            var command = new AdicionarNovoLogCommand(usuario.Id, ambiente.Id, nivel.Id, titulo, descricao, origem);
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            (await _context.Logs.CountAsync()).Should().Be(1);
            var log = await _context.Logs.FirstOrDefaultAsync();
            log.Usuario.Should().BeEquivalentTo(usuario);
            log.Ambiente.Should().BeEquivalentTo(ambiente);
            log.Nivel.Should().BeEquivalentTo(nivel);
            log.UsuarioId.Should().Be(usuario.Id);
            log.AmbienteId.Should().Be(ambiente.Id);
            log.NivelId.Should().Be(nivel.Id);
            log.Titulo.Should().Be(titulo);
            log.Descricao.Should().Be(descricao);
            log.Origem.Should().Be(origem);
        }

        [Fact]
        public async Task HandlerAdicionar_DeveLancarNotificacaoQuandoUsuarioNaoExistir()
        {
            var command = new AdicionarNovoLogCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "título" , "descrição", "0.0.0.0");
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }

        [Fact]
        public async Task HandlerAdicionar_DeveLancarNotificacaoQuandoAmbienteNaoExistir()
        {
            var usuario = UsuarioFactory.Create();
            _mediatorMock
                .Setup(mediator => mediator.Send(It.IsAny<ObterUsuarioPorIdQuery>(), CancellationToken.None))
                .Returns(async () => await Task.Run(() => usuario));

            var command = new AdicionarNovoLogCommand(usuario.Id, Guid.NewGuid(), Guid.NewGuid(), "título" , "descrição", "0.0.0.0");
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }

        [Fact]
        public async Task HandlerAdicionar_DeveLancarNotificacaoQuandoNivelNaoExistir()
        {
            var usuario = UsuarioFactory.Create();
            var ambiente = AmbienteFactory.Create();
            _mediatorMock
                .Setup(mediator => mediator.Send(It.IsAny<ObterUsuarioPorIdQuery>(), CancellationToken.None))
                .Returns(async () => await Task.Run(() => usuario));

            _mediatorMock
                .Setup(mediator => mediator.Send(It.IsAny<ObterAmbientePorIdQuery>(), CancellationToken.None))
                .Returns(async () => await Task.Run(() => ambiente));
            
            var command = new AdicionarNovoLogCommand(usuario.Id, ambiente.Id, Guid.NewGuid(), "título" , "descrição", "0.0.0.0");
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }

        [Fact]
        public async Task HandlerAdicionar_DeveLancarNotificacaoQuandoCommandForInvalido()
        {
            var command = new AdicionarNovoLogCommand(Guid.Empty, Guid.NewGuid(), Guid.NewGuid(), "título" , "descrição", "0.0.0.0");
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);
            
            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }
        
        [Fact]
        public async Task HandlerAdicionar_DeveInvalidarCommandQuandoTituloForNullOuVazio()
        {
            var commandNull = new AdicionarNovoLogCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, "descrição", "0.0.0.0");
            var commandVazio = new AdicionarNovoLogCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "", "descrição", "0.0.0.0");
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(commandNull, CancellationToken.None);
            await commandHandler.Handle(commandVazio, CancellationToken.None);

            commandNull.ValidationResult.IsValid.Should().Be(false);
            commandNull.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "'Título' deve ser informado.");

            commandVazio.ValidationResult.IsValid.Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "'Título' deve ser informado.");
        }
        
        [Fact]
        public async Task HandlerAdicionar_DeveInvalidarCommandQuandoDescricaoForNullOuVazio()
        {
            var commandNull = new AdicionarNovoLogCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "título", null, "0.0.0.0");
            var commandVazio = new AdicionarNovoLogCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "título", "", "0.0.0.0");
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(commandNull, CancellationToken.None);
            await commandHandler.Handle(commandVazio, CancellationToken.None);

            commandNull.ValidationResult.IsValid.Should().Be(false);
            commandNull.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "'Descrição' deve ser informado.");

            commandVazio.ValidationResult.IsValid.Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "'Descrição' deve ser informado.");
        }
        
        [Fact]
        public async Task HandlerAdicionar_DeveInvalidarCommandQuandoOrigemForNullOuVazio()
        {
            var commandNull = new AdicionarNovoLogCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "título", "descrição", null);
            var commandVazio = new AdicionarNovoLogCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "título", "descrição", "");
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(commandNull, CancellationToken.None);
            await commandHandler.Handle(commandVazio, CancellationToken.None);

            commandNull.ValidationResult.IsValid.Should().Be(false);
            commandNull.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "'Origem' deve ser informado.");

            commandVazio.ValidationResult.IsValid.Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "'Origem' deve ser informado.");
        }

        [Fact]
        public async Task HandlerArquivar_DeveAtualizarCampoArquivadoEm()
        {
            var log = LogFactory.Create();
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            var command = new ArquivarLogCommand(log.Id);
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);
            
            var logSalvo = await _context.Logs.FirstOrDefaultAsync();
            logSalvo.ArquivadoEm.Should().BeCloseTo(DateTime.UtcNow, 1000);
        }

        [Fact]
        public async Task HandlerArquivar_DeveLancarNotificacaoQuandoLogJaEstiverArquivado()
        {
            var log = LogFactory.Create();
            log.Arquivar();
            var command = new ArquivarLogCommand(log.Id);
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();

            await commandHandler.Handle(command, CancellationToken.None);
            
            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }

        [Fact]
        public async Task HandlerArquivar_DeveLancarNotificacaoQuandoLogNaoExistir()
        {
            var command = new ArquivarLogCommand(Guid.NewGuid());
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);
            
            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }
        
        [Fact]
        public async Task HandlerArquivar_DeveInvalidarCommandQuandoIdForVazio()
        {
            var commandVazio = new ArquivarLogCommand(Guid.Empty);
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(commandVazio, CancellationToken.None);

            commandVazio.ValidationResult.IsValid.Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Id deve possuir conteúdo");
        }

        [Fact]
        public async Task HandlerDeletar_DeveDeletarLog()
        {
            var log = LogFactory.Create();
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            var command = new DeletarLogCommand(log.Id);
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);
            
            (await _context.Logs.ToListAsync()).Should().HaveCount(0);
        }

        [Fact]
        public async Task HandlerDeletar_DeveLancarNotificacaoQuandoLogNaoExistir()
        {
            var command = new DeletarLogCommand(Guid.NewGuid());
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);
            
            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }
        
        [Fact]
        public async Task HandlerDeletar_DeveInvalidarCommandQuandoIdForVazio()
        {
            var commandVazio = new DeletarLogCommand(Guid.Empty);
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(commandVazio, CancellationToken.None);

            commandVazio.ValidationResult.IsValid.Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Id deve possuir conteúdo");
        }
    }
}