namespace PolarisLog.Domain.QuerySide.Queries.Ambiente
{
    public class ObterTodosOsAmbientesQuery : Query<PagedList<Entities.Ambiente>>
    {
        public ObterTodosOsAmbientesQuery(int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
        }
    }
}