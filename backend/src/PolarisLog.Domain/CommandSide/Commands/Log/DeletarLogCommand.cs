using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PolarisLog.Domain.CommandSide.Validations.Log;

namespace PolarisLog.Domain.CommandSide.Commands.Log
{
    public class DeletarLogCommand : Command<Unit>
    {
        public Guid[] Ids { get; }

        public DeletarLogCommand(params Guid[] ids)
        {
            Ids = ids;
        }

        public override async Task<bool> EhValido()
        {
            ValidationResult = await new DeletarLogCommandValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}