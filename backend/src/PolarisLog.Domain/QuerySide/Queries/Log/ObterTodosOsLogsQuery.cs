using System;

namespace PolarisLog.Domain.QuerySide.Queries.Log
{
    public class ObterTodosOsLogsQuery : Query<PagedList<Entities.Log>>
    {
        public Guid AmbienteId { get; set; }
        public string Origem { get; }
        public string Descricao { get; }
        public bool? Arquivado { get; }

        public ObterTodosOsLogsQuery(int pageNumber, int pageSize, Guid ambienteId, string origem, string descricao, bool? arquivado)
            : base(pageNumber, pageSize)
        {
            AmbienteId = ambienteId;
            Origem = origem;
            Descricao = descricao;
            Arquivado = arquivado;
        }
    }
}