using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide.Queries.Ambiente;
using PolarisLog.Domain.QuerySide.QueryHandlers;
using PolarisLog.Infra;
using PolarisLog.Infra.Repositories;
using PolarisLog.Tests.Helpers.Factories;
using Xunit;

namespace PolarisLog.Tests.Domain.QuerySide.Queries
{
    public class AmbienteQueryHandlerTest
    {
        private readonly Context _context;
        private readonly IAmbienteRepository _ambienteRepository;

        public AmbienteQueryHandlerTest()
        {
            _context = ContextFactory.Create();
            _ambienteRepository = new AmbienteRepository(_context);
        }
        
        [Fact]
        public async Task HandlerObterTodos_DeveRetornarTodosOsAmbientesSalvosNoBanco()
        {
            var ambiente1 = AmbienteFactory.Create();
            var ambiente2 = AmbienteFactory.Create();
            await _context.Ambientes.AddRangeAsync(ambiente1, ambiente2);
            await _context.SaveChangesAsync();
            
            var query = new ObterTodosOsAmbientesQuery(1, 20);
            var queryHandler = new AmbienteQueryHandler(_ambienteRepository);

            var ambientes = await queryHandler.Handle(query, CancellationToken.None);

            ambientes.Should().HaveCount(2);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarAmbienteQuandoForEncontradoNoBanco()
        {
            var ambiente = AmbienteFactory.Create();
            await _context.Ambientes.AddAsync(ambiente);
            await _context.SaveChangesAsync();

            var query = new ObterAmbientePorIdQuery(ambiente.Id);
            var queryHandler = new AmbienteQueryHandler(_ambienteRepository);

            var ambienteBanco = await queryHandler.Handle(query, CancellationToken.None);

            ambienteBanco.Should().BeEquivalentTo(ambiente);
        }

        [Fact]
        public async Task ObterPorId_DeveInvalidarQueryQuandoIdForVazio()
        {
            var query = new ObterAmbientePorIdQuery(Guid.Empty);
            var queryHandler = new AmbienteQueryHandler(_ambienteRepository);

            await queryHandler.Handle(query, CancellationToken.None);

            (await query.EhValido()).Should().Be(false);
            query.ValidationResult.Errors
                .Should()
                .Contain(error => error.ErrorMessage == "Id deve possuir conteúdo");
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNullQuandoNaoForEncontradoNoBanco()
        {
            var query = new ObterAmbientePorIdQuery(Guid.NewGuid());
            var queryHandler = new AmbienteQueryHandler(_ambienteRepository);

            var ambienteBanco = await queryHandler.Handle(query, CancellationToken.None);

            ambienteBanco.Should().BeNull();
        }
    }
}