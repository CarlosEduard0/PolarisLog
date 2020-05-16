using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using PolarisLog.Domain.CommandSide.CommandHandlers;
using PolarisLog.Domain.CommandSide.Commands.Log;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.Notifications;
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
        public async Task HandlerAdicionar_DeveAdicionarLogComLevelDescricaoEOrigem()
        {
            var level = Level.Verbose;
            var descricao = "descrição";
            var origem = "0.0.0.0";
            var command = new AdicionarNovoLogCommand(level, descricao, origem);
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            (await _context.Logs.CountAsync()).Should().Be(1);
            var log = await _context.Logs.FirstOrDefaultAsync();
            log.Level.Should().Be(level);
            log.Descricao.Should().Be(descricao);
            log.Origem.Should().Be(origem);
        }
        
        

        [Fact]
        public async Task HandlerAdicionar_DeveLancarNotificacaoQuandoCommandForInvalido()
        {
            var command = new AdicionarNovoLogCommand(Level.Verbose, null, "0.0.0.0");
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);
            
            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<DomainNotification>(), CancellationToken.None));
        }
        
        [Fact]
        public async Task HandlerAdicionar_DeveInvalidarCommandQuandoDescricaoForNullOuVazio()
        {
            var commandNull = new AdicionarNovoLogCommand(Level.Verbose, null, "0.0.0.0");
            var commandVazio = new AdicionarNovoLogCommand(Level.Verbose, "", "0.0.0.0");
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(commandNull, CancellationToken.None);
            await commandHandler.Handle(commandVazio, CancellationToken.None);

            commandNull.ValidationResult.IsValid.Should().Be(false);
            commandNull.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Descrição deve possuir conteúdo");

            commandVazio.ValidationResult.IsValid.Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Descrição deve possuir conteúdo");
        }
        
        [Fact]
        public async Task HandlerAdicionar_DeveInvalidarCommandQuandoOrigemForNullOuVazio()
        {
            var commandNull = new AdicionarNovoLogCommand(Level.Verbose, "descrição", null);
            var commandVazio = new AdicionarNovoLogCommand(Level.Verbose, "descrição", "");
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(commandNull, CancellationToken.None);
            await commandHandler.Handle(commandVazio, CancellationToken.None);

            commandNull.ValidationResult.IsValid.Should().Be(false);
            commandNull.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Origem deve possuir conteúdo");

            commandVazio.ValidationResult.IsValid.Should().Be(false);
            commandVazio.ValidationResult.Errors.Should()
                .Contain(error => error.ErrorMessage == "Origem deve possuir conteúdo");
        }

        [Fact]
        public async Task HandlerArquivar_DeveAtualizarCampoArquivadoEm()
        {
            var log = new Log(Level.Verbose, "descrição", "0.0.0.0");
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            var command = new ArquivarLogCommand(log.Id);
            var commandHandler = new LogCommandHandler(_mediatorMock.Object, _logRepository);

            await commandHandler.Handle(command, CancellationToken.None);
            
            var logSalvo = await _context.Logs.FirstOrDefaultAsync();
            logSalvo.ArquivadoEm.Should().BeCloseTo(DateTime.Now, 1000);
        }

        [Fact]
        public async Task HandlerArquivar_DeveLancarNotificacaoQuandoLogJaEstiverArquivado()
        {
            var log = new Log(Level.Verbose, "descrição", "0.0.0.0");
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
    }
}