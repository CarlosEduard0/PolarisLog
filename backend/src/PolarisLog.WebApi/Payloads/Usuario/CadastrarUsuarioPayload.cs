using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PolarisLog.WebApi.Payloads.Usuario
{
    public class CadastrarUsuarioPayload
    {
        [Required(ErrorMessage = "'{0}' deve ser informado")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "'{0}' deve ser informado")]
        [EmailAddress(ErrorMessage = "'{0}' inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "'{0}' deve ser informada")]
        public string Senha { get; set; }
        
        [DisplayName("Senha Confirmação")]
        [Required(ErrorMessage = "'{0}' deve ser informada")]
        [Compare("Senha", ErrorMessage = "Senhas não coincidem")]
        public string SenhaConfirmacao { get; set; }
    }
}