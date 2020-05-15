using FluentValidation;
using FluentValidation.Validators;
using PolarisLog.Domain.CommandSide.Commands.Usuario;

namespace PolarisLog.Domain.CommandSide.Validations.Usuario
{
    public class AdicionarNovoUsuarioCommandValidation : AbstractValidator<AdicionarNovoUsuarioCommand>
    {
        public AdicionarNovoUsuarioCommandValidation()
        {
            RuleFor(usuario => usuario.Nome)
                .NotEmpty().WithMessage("Nome deve possuir conteúdo")
                .MaximumLength(50).WithMessage("Nome deve ter no máximo {MaxLength} caracteres");

            RuleFor(usuario => usuario.Email)
                .NotEmpty().WithMessage("E-mail deve possuir conteúdo")
                .MaximumLength(50).WithMessage("E-mail deve ter no máximo {MaxLength} caracteres")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("E-mail inválido");

            RuleFor(usuario => usuario.Senha)
                .NotEmpty().WithMessage("Senha deve possuir conteúdo")
                .Equal(usuario => usuario.SenhaConfirmacao).WithMessage("As senhas não coincidem");
        }
    }
}