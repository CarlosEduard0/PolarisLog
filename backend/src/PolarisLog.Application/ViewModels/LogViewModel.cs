using System;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Application.ViewModels
{
    public class LogViewModel
    {
        public Level? Level { get; set; }
        public string Descricao { get; set; }
        public string Origem { get; set; }
        public DateTime CadastradoEm { get; set; }
    }
}