using System;
using FluentAssertions;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Exceptions;
using PolarisLog.Tests.Helpers.Factories;
using Xunit;

namespace PolarisLog.Tests.Domain.Entities
{
    public class LogTest
    {
        [Fact]
        public void Log_DeveAdicionarUsuarioAmbienteNivelTituloDescricaoEOrigem()
        {
            var usuarioId = Guid.NewGuid();
            var ambienteId = Guid.NewGuid();
            var nivelId = Guid.NewGuid();
            var titulo = "título";
            var descricao = "descrição";
            var origem = "0.0.0.0";
            
            var log = new Log(usuarioId, ambienteId, nivelId, titulo,descricao, origem);
            
            log.UsuarioId.Should().Be(usuarioId);
            log.AmbienteId.Should().Be(ambienteId);
            log.NivelId.Should().Be(nivelId);
            log.Titulo.Should().Be(titulo);
            log.Descricao.Should().Be(descricao);
            log.Origem.Should().Be(origem);
            log.CadastradoEm.Should().BeCloseTo(DateTime.UtcNow, 1000);
        }

        [Fact]
        public void Log_DeveLancarExcecaoQuandoUsuarioIdForVazio()
        {
            Action action = () => new Log(Guid.Empty, Guid.NewGuid(), Guid.NewGuid(), "título", "descrição", "0.0.0.0");

            action.Should().Throw<DomainException>().WithMessage("Id Usuário deve possuir conteúdo");
        }

        [Fact]
        public void Log_DeveLancarExcecaoQuandoAmbienteIdForVazio()
        {
            Action action = () => new Log(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), "título", "descrição", "0.0.0.0");

            action.Should().Throw<DomainException>().WithMessage("Id Ambiente deve possuir conteúdo");
        }

        [Fact]
        public void Log_DeveLancarExcecaoQuandoNivelIdForVazio()
        {
            Action action = () => new Log(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "título", "descrição", "0.0.0.0");

            action.Should().Throw<DomainException>().WithMessage("Id Nível deve possuir conteúdo");
        }

        [Fact]
        public void Log_DeveLancarExcecaoQuandoTituloForNullOuVazio()
        {
            Action actionNull = () => new Log(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, "descriçaõ", "0.0.0.0");
            Action actionVazio = () => new Log(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "", "descriçaõ", "0.0.0.0");

            actionNull.Should().Throw<DomainException>().WithMessage("Título deve possuir conteúdo");
            actionVazio.Should().Throw<DomainException>().WithMessage("Título deve possuir conteúdo");
        }

        [Fact]
        public void Log_DeveLancarExcecaoQuandoDescricaoForNullOuVazia()
        {
            Action actionNull = () => new Log(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "título", null, "0.0.0.0");
            Action actionVazio = () => new Log(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "título", "", "0.0.0.0");

            actionNull.Should().Throw<DomainException>().WithMessage("Descrição deve possuir conteúdo");
            actionVazio.Should().Throw<DomainException>().WithMessage("Descrição deve possuir conteúdo");
        }

        [Fact]
        public void Log_DeveLancarExcecaoQuandoOrigemForNullOuVazia()
        {
            Action actionNull = () => new Log(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "título", "descrição", null);
            Action actionVazio = () => new Log(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "título", "descrição", "");

            actionNull.Should().Throw<DomainException>().WithMessage("Origem deve possuir conteúdo");
            actionVazio.Should().Throw<DomainException>().WithMessage("Origem deve possuir conteúdo");
        }

        [Fact]
        public void Arquivar_DeveAdicionarArquivadoEm()
        {
            var log = LogFactory.GerarLog();
            
            log.Arquivar();

            log.ArquivadoEm.Should().BeCloseTo(DateTime.UtcNow);
        }

        [Fact]
        public void Arquivar_DeveLancarExcecaoQuandoLogJaEstiverArquivado()
        {
            var log = LogFactory.GerarLog();
            log.Arquivar();
            
            Action arquivar = () => log.Arquivar();

            arquivar.Should().Throw<DomainException>().WithMessage("Log já foi arquivado");
        }
    }
}