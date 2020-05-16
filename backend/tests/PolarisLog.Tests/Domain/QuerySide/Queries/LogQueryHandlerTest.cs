using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
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
        private readonly Mock<IMediator> _mediatorMock;

        public LogQueryHandlerTest()
        {
            _context = ContextFactory.Create();
            _logRepository = new LogRepository(_context);
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task HandlerObterTodos_DeveRetornarTodosOsLogsSalvosNoBanco()
        {
            var log1 = LogFactory.Create();
            var log2 = LogFactory.Create();
            await _context.Logs.AddRangeAsync(log1, log2);
            await _context.SaveChangesAsync();
            
            var query = new ObterTodosOsLogsQuery();
            var queryHandler = new LogQueryHandler(_logRepository);

            var logs = await queryHandler.Handle(query, CancellationToken.None);

            logs.Should().HaveCount(2);
        }
    }
}