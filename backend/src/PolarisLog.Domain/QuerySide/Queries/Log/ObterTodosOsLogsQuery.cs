namespace PolarisLog.Domain.QuerySide.Queries.Log
{
    public class ObterTodosOsLogsQuery : Query<PagedList<Entities.Log>>
    {
        public ObterTodosOsLogsQuery(int pageNumber = 1, int pageSize = 20) : base(pageNumber, pageSize)
        {
        }
    }
}