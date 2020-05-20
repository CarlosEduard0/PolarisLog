using System;
using System.ComponentModel.DataAnnotations;

namespace PolarisLog.WebApi.Payloads.Log
{
    public class CadastrarLogPayload
    {
        [Required(ErrorMessage = "O campo Ambiente é obrigatório")]
        public Guid AmbienteId { get; set; }
        
        [Required(ErrorMessage = "O campo Nível é obrigatório")]
        public Guid NivelId { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Origem { get; set; }
    }
}