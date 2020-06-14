using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PolarisLog.WebApi.Payloads.Usuario
{
    public class RecuperarSenhaPayload
    {
        [Required(ErrorMessage = "'{0}' deve ser informado")]
        [EmailAddress(ErrorMessage = "'{0}' inválido")]
        public string Email { get; set; }
    }
}