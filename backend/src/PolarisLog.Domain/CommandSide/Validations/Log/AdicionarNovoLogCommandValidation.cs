using System;
using FluentValidation;
using PolarisLog.Domain.CommandSide.Commands.Log;

namespace PolarisLog.Domain.CommandSide.Validations.Log
{
    public class AdicionarNovoLogCommandValidation : AbstractValidator<AdicionarNovoLogCommand>
    {
        public AdicionarNovoLogCommandValidation()
        {
            RuleFor(log => log.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("Usuário deve possuir conteúdo");
            
            RuleFor(log => log.AmbienteId)
                .NotEqual(Guid.Empty).WithMessage("Ambiente deve possuir conteúdo");
            
            RuleFor(log => log.NivelId)
                .NotEqual(Guid.Empty).WithMessage("Nível deve possuir conteúdo");
            
            RuleFor(log => log.Titulo)
                .NotEmpty().WithMessage("Título deve possuir conteúdo");
            
            RuleFor(log => log.Descricao)
                .NotEmpty().WithMessage("Descrição deve possuir conteúdo");

            RuleFor(log => log.Origem)
                .NotEmpty().WithMessage("Origem deve possuir conteúdo");
        }
    }
}