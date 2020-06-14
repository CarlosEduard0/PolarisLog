using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide.Queries.Log;
using PolarisLog.Domain.QuerySide.QueryHandlers;
using PolarisLog.Infra;
using PolarisLog.Infra.Repositories;
using PolarisLog.Tests.Helpers.Factories;
using Xunit;

namespace PolarisLog.Tests.Domain.QuerySide.Queries
{
    public class LogQueryHandlerTest
    {
        private readonly Context _context;
        private readonly ILogRepository _logRepository;

        public LogQueryHandlerTest()
        {
            _context = ContextFactory.Create();
            _logRepository = new LogRepository(_context);
        }

        [Fact]
        public async Task HandlerObterTodos_DeveRetornarTodosOsLogsSalvosNoBanco()
        {
            var usuario = UsuarioFactory.Create();
            var ambiente = AmbienteFactory.Create();
            var nivel = NivelFactory.Create();
            var log1 = new Log(usuario.Id, ambiente.Id, nivel.Id, "título", "descrição", "0.0.0.0");
            var log2 = new Log(usuario.Id, ambiente.Id, nivel.Id, "título", "descrição", "0.0.0.0");
            await _context.AddRangeAsync(usuario, ambiente, nivel, log1, log2);
            await _context.SaveChangesAsync();
            
            var query = new ObterTodosOsLogsQuery(1, 20, Guid.Empty, null, null, null);
            var queryHandler = new LogQueryHandler(_logRepository);

            var logs = await queryHandler.Handle(query, CancellationToken.None);

            logs.Should().HaveCount(2);
        }
    }
}