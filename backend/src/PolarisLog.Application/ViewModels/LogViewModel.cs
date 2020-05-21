using System;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Application.ViewModels
{
    public class LogViewModel
    {
        public Guid UsuarioId { get; set; }
        public Guid AmbienteId { get; set; }
        public Guid NivelId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Origem { get; set; }
    }
}