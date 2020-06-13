using FluentValidation;
using PolarisLog.Domain.CommandSide.Commands.Log;

namespace PolarisLog.Domain.CommandSide.Validations.Log
{
    public class DeletarLogCommandValidation : AbstractValidator<DeletarLogCommand>
    {
        public DeletarLogCommandValidation()
        {
            RuleFor(command => command.Ids).NotEmpty();
        }
    }
}