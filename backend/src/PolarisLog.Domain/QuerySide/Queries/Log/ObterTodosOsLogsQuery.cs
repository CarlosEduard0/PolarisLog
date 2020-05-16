using MediatR;

namespace PolarisLog.Domain.QuerySide.Queries.Log
{
    public class ObterTodosOsLogsQuery : IRequest<Entities.Log[]>
    {
    }
}