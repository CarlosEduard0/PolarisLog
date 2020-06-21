using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide.Queries.Nivel;
using PolarisLog.Domain.QuerySide.QueryHandlers;
using PolarisLog.Infra;
using PolarisLog.Infra.Repositories;
using PolarisLog.Tests.Helpers.Factories;
using Xunit;

namespace PolarisLog.Tests.Domain.QuerySide.Queries
{
    public class NivelQueryHandlerTest
    {
        private readonly Context _context;
        private readonly INivelRepository _nivelRepository;

        public NivelQueryHandlerTest()
        {
            _context = ContextFactory.Create();
            _nivelRepository = new NivelRepository(_context);
        }
        
        [Fact]
        public async Task HandlerObterTodos_DeveRetornarTodosOsNivelsSalvosNoBanco()
        {
            var nivel1 = NivelFactory.GerarNivel();
            var nivel2 = NivelFactory.GerarNivel();
            await _context.Niveis.AddRangeAsync(nivel1, nivel2);
            await _context.SaveChangesAsync();
            
            var query = new ObterTodosOsNiveisQuery(1, 20);
            var queryHandler = new NivelQueryHandler(_nivelRepository);

            var niveis = await queryHandler.Handle(query, CancellationToken.None);

            niveis.Should().HaveCount(2);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNivelQuandoForEncontradoNoBanco()
        {
            var nivel = NivelFactory.GerarNivel();
            await _context.Niveis.AddAsync(nivel);
            await _context.SaveChangesAsync();

            var query = new ObterNivelPorIdQuery(nivel.Id);
            var queryHandler = new NivelQueryHandler(_nivelRepository);

            var nivelBanco = await queryHandler.Handle(query, CancellationToken.None);

            nivelBanco.Should().BeEquivalentTo(nivel);
        }

        [Fact]
        public async Task ObterPorId_DeveInvalidarQueryQuandoIdForVazio()
        {
            var query = new ObterNivelPorIdQuery(Guid.Empty);
            var queryHandler = new NivelQueryHandler(_nivelRepository);

            await queryHandler.Handle(query, CancellationToken.None);

            (await query.EhValido()).Should().Be(false);
            query.ValidationResult.Errors
                .Should()
                .Contain(error => error.ErrorMessage == "Id deve possuir conteúdo");
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNullQuandoNaoForEncontradoNoBanco()
        {
            var query = new ObterNivelPorIdQuery(Guid.NewGuid());
            var queryHandler = new NivelQueryHandler(_nivelRepository);

            var nivelBanco = await queryHandler.Handle(query, CancellationToken.None);

            nivelBanco.Should().BeNull();
        }
    }
}