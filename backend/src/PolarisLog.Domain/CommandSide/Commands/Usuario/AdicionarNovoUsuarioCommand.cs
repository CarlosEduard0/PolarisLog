using System;
using System.Threading.Tasks;
using PolarisLog.Domain.CommandSide.Validations.Usuario;

namespace PolarisLog.Domain.CommandSide.Commands.Usuario
{
    public class AdicionarNovoUsuarioCommand : Command<Guid>
    {
        public string Nome { get; }
        public string Email { get; }
        public string Senha { get; }
        public string SenhaConfirmacao { get; }

        public AdicionarNovoUsuarioCommand(string nome, string email, string senha, string senhaConfirmacao)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            SenhaConfirmacao = senhaConfirmacao;
        }

        public override async Task<bool> EhValido()
        {
            ValidationResult = await new AdicionarNovoUsuarioCommandValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}