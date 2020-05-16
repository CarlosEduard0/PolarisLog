using System;

namespace PolarisLog.WebApi.Payloads
{
    public class LogPayload
    {
        public string UsuarioId { get; set; }
        public string Level { get; set; }
        public string Descricao { get; set; }
        public string Origem { get; set; }
        public DateTime CadastradoEm { get; set; }
    }
}