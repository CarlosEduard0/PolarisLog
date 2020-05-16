using System;
using System.Threading.Tasks;
using PolarisLog.Domain.CommandSide.Validations.Log;

namespace PolarisLog.Domain.CommandSide.Commands.Log
{
    public class ArquivarLogCommand : Command
    {
        public Guid Id { get; }

        public ArquivarLogCommand(Guid id)
        {
            Id = id;
        }

        public override async Task<bool> EhValido()
        {
            ValidationResult = await new ArquivarLogCommandValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}