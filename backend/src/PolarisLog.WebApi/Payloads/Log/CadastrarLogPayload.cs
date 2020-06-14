using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PolarisLog.WebApi.Payloads.Log
{
    public class CadastrarLogPayload
    {
        [DisplayName("Ambiente")]
        [Required(ErrorMessage = "'{0}' deve ser informado")]
        public string AmbienteId { get; set; }

        [DisplayName("Nível")]
        [Required(ErrorMessage = "'{0}' deve ser informado")]
        public string NivelId { get; set; }
        
        [DisplayName("Título")]
        [Required(ErrorMessage = "'{0}' deve ser informado")]
        public string Titulo { get; set; }
        
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "'{0}' deve ser informada")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "'{0}' deve ser informado")]
        public string Origem { get; set; }
    }
}