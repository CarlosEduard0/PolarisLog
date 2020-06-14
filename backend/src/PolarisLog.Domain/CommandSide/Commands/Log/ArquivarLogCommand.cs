using System;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.CommandSide.Validations.Log;

namespace PolarisLog.Domain.CommandSide.Commands.Log
{
    public class ArquivarLogCommand : Command<Unit>
    {
        public Guid[] Ids { get; }

        public ArquivarLogCommand(params Guid[] ids)
        {
            Ids = ids;
        }

        public override async Task<bool> EhValido()
        {
            ValidationResult = await new ArquivarLogCommandValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}