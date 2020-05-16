using System;
using FluentAssertions;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Exceptions;
using Xunit;

namespace PolarisLog.Tests.Domain.Entities
{
    public class LogTest
    {
        [Fact]
        public void Log_DeveAdicionarLevelDescricaoEOrigem()
        {
            var level = Level.Verbose;
            var descricao = "descrição";
            var origem = "0.0.0.0";
            var log = new Log(level, descricao, origem);
            
            log.Level.Should().Be(level);
            log.Descricao.Should().Be(descricao);
            log.Origem.Should().Be(origem);
        }

        [Fact]
        public void Log_DeveLancarExcecaoQuandoDescricaoForNullOuVazia()
        {
            Action actionNull = () => new Log(Level.Verbose, null, "0.0.0.0");
            Action actionVazio = () => new Log(Level.Verbose, "", "0.0.0.0");

            actionNull.Should().Throw<DomainException>().WithMessage("Descrição deve possuir conteúdo");
            actionVazio.Should().Throw<DomainException>().WithMessage("Descrição deve possuir conteúdo");
        }

        [Fact]
        public void Log_DeveLancarExcecaoQuandoOrigemForNullOuVazia()
        {
            Action actionNull = () => new Log(Level.Verbose, "descrição", null);
            Action actionVazio = () => new Log(Level.Verbose, "descrição", "");

            actionNull.Should().Throw<DomainException>().WithMessage("Origem deve possuir conteúdo");
            actionVazio.Should().Throw<DomainException>().WithMessage("Origem deve possuir conteúdo");
        }

        [Fact]
        public void Arquivar_DeveAdicionarArquivadoEm()
        {
            var log = new Log(Level.Verbose, "descrição", "0.0.0.0");
            
            log.Arquivar();

            log.ArquivadoEm.Should().BeCloseTo(DateTime.Now.ToUniversalTime());
        }

        [Fact]
        public void Arquivar_DeveLancarExcecaoQuandoLogJaEstiverArquivado()
        {
            var log = new Log(Level.Verbose, "descrição", "0.0.0.0");
            log.Arquivar();
            Action arquivar = () => log.Arquivar();

            arquivar.Should().Throw<DomainException>().WithMessage("Log já foi arquivado");
        }

        [Fact]
        public void Deletar_DeveAdicionarDeletadoEm()
        {
            var log = new Log(Level.Verbose, "descrição", "0.0.0.0");
            
            log.Deletar();

            log.DeletadoEm.Should().BeCloseTo(DateTime.Now.ToUniversalTime());
        }
    }
}