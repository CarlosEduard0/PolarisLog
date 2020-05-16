using System;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.CommandSide.Validations.Log;

namespace PolarisLog.Domain.CommandSide.Commands.Log
{
    public class DeletarLogCommand : Command<Unit>
    {
        public Guid Id { get; }

        public DeletarLogCommand(Guid id)
        {
            Id = id;
        }

        public override async Task<bool> EhValido()
        {
            ValidationResult = await new DeletarLogCommandValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}