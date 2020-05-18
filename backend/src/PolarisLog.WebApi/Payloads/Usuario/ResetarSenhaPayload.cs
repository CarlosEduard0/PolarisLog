using System.ComponentModel.DataAnnotations;

namespace PolarisLog.WebApi.Payloads.Usuario
{
    public class ResetarSenhaPayload
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Senha { get;set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Token { get;set; }
    }
}