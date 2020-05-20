namespace PolarisLog.Domain.QuerySide.Queries.Log
{
    public class ObterTodosOsLogsQuery : Query<PagedList<Entities.Log>>
    {
        public string Origem { get; }
        public string Descricao { get; }

        public ObterTodosOsLogsQuery(int pageNumber, int pageSize, string origem, string descricao)
            : base(pageNumber, pageSize)
        {
            Origem = origem;
            Descricao = descricao;
        }
    }
}