using System;
using System.Threading.Tasks;
using PolarisLog.Domain.CommandSide.Validations.Session;

namespace PolarisLog.Domain.CommandSide.Commands.Session
{
    public class LogarCommand : Command<Guid>
    {
        public string Email { get; }
        public string Senha { get; }

        public LogarCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
        
        public override async Task<bool> EhValido()
        {
            ValidationResult = await new LogarCommandValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}