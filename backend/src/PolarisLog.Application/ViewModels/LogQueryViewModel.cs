using System;

namespace PolarisLog.Application.ViewModels
{
    public class LogQueryViewModel : QueryViewModel
    {
        public Guid AmbienteId { get; set; }
        public string Origem { get; set; }
        public string Descricao { get; set; }
        public bool? Arquivado { get; set; }
    }
}