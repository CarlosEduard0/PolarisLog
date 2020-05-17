using FluentValidation;
using PolarisLog.Domain.CommandSide.Commands.Session;

namespace PolarisLog.Domain.CommandSide.Validations.Session
{
    public class LoginCommandValidation : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidation()
        {
            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("E-mail deve possuir conteúdo")
                .EmailAddress().WithMessage("E-mail deve ser válido");

            RuleFor(command => command.Senha)
                .NotEmpty().WithMessage("Senha deve possuir conteúdo");
        }
    }
}