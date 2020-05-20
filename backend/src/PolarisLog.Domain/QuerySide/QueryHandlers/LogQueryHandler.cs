using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Interfaces;
using PolarisLog.Domain.QuerySide.Queries.Log;

namespace PolarisLog.Domain.QuerySide.QueryHandlers
{
    public class LogQueryHandler : IRequestHandler<ObterTodosOsLogsQuery, PagedList<Log>>
    {
        private readonly ILogRepository _logRepository;

        public LogQueryHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public Task<PagedList<Log>> Handle(ObterTodosOsLogsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Log, bool>> filtro = log =>
                (log.Origem.Contains(request.Origem) || string.IsNullOrWhiteSpace(request.Origem)) &&
                (log.Descricao.Contains(request.Descricao) || string.IsNullOrWhiteSpace(request.Descricao));
            return Task.FromResult(_logRepository.ObterTodos(request.PageNumber, request.PageSize, filtro));
        }
    }
}