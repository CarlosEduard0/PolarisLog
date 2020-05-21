using System.ComponentModel.DataAnnotations;

namespace PolarisLog.WebApi.Payloads.Usuario
{
    public class RecuperarSenhaPayload
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
        public string Email { get; set; }
    }
}