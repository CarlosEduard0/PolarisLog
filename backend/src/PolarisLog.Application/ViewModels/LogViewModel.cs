using System;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Application.ViewModels
{
    public class LogViewModel
    {
        public Guid UsuarioId { get; set; }
        public Level? Level { get; set; }
        public string Descricao { get; set; }
        public string Origem { get; set; }
    }
}