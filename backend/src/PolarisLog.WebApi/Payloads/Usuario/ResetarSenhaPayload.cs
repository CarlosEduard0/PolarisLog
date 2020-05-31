using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PolarisLog.WebApi.Payloads.Usuario
{
    public class ResetarSenhaPayload
    {
        [Required(ErrorMessage = "'{0}' deve ser informado")]
        [EmailAddress(ErrorMessage = "'{0}' inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "'{0}' deve ser informada")]
        public string Senha { get;set; }
        
        [Required(ErrorMessage = "'{0}' deve ser informado")]
        public string Token { get;set; }
    }
}