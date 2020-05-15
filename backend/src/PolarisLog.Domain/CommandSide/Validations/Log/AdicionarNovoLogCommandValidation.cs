using FluentValidation;
using PolarisLog.Domain.CommandSide.Commands.Log;

namespace PolarisLog.Domain.CommandSide.Validations.Log
{
    public class AdicionarNovoLogCommandValidation : AbstractValidator<AdicionarNovoLogCommand>
    {
        public AdicionarNovoLogCommandValidation()
        {
            RuleFor(log => log.Level)
                .NotNull().WithMessage("Level deve possuir conteúdo");

            RuleFor(log => log.Descricao)
                .NotEmpty().WithMessage("Descrição deve possuir conteúdo");

            RuleFor(log => log.Origem)
                .NotEmpty().WithMessage("Origem deve possuir conteúdo");
        }
    }
}