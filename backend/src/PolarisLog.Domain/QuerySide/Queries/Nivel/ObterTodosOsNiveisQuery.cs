namespace PolarisLog.Domain.QuerySide.Queries.Nivel
{
    public class ObterTodosOsNiveisQuery : Query<PagedList<Entities.Nivel>>
    {
        public ObterTodosOsNiveisQuery(int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
        }
    }
}