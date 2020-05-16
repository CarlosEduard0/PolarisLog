using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide.Queries.Log;

namespace PolarisLog.Domain.QuerySide.QueryHandlers
{
    public class LogQueryHandler : IRequestHandler<ObterTodosOsLogsQuery, Log[]>
    {
        private readonly ILogRepository _logRepository;

        public LogQueryHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<Log[]> Handle(ObterTodosOsLogsQuery request, CancellationToken cancellationToken)
        {
            return await _logRepository.ObterTodos();
        }
    }
}