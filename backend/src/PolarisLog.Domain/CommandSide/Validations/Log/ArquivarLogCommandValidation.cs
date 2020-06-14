using FluentValidation;
using PolarisLog.Domain.CommandSide.Commands.Log;

namespace PolarisLog.Domain.CommandSide.Validations.Log
{
    public class ArquivarLogCommandValidation : AbstractValidator<ArquivarLogCommand>
    {
        public ArquivarLogCommandValidation()
        {
            RuleFor(command => command.Ids).NotEmpty();
        }
    }
}