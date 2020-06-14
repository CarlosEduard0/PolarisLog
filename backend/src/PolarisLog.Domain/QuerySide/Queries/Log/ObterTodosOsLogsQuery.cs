namespace PolarisLog.Domain.QuerySide.Queries.Log
{
    public class ObterTodosOsLogsQuery : Query<PagedList<Entities.Log>>
    {
        public string Origem { get; }
        public string Descricao { get; }
        public bool? Arquivado { get; }

        public ObterTodosOsLogsQuery(int pageNumber, int pageSize, string origem, string descricao, bool? arquivado)
            : base(pageNumber, pageSize)
        {
            Origem = origem;
            Descricao = descricao;
            Arquivado = arquivado;
        }
    }
}