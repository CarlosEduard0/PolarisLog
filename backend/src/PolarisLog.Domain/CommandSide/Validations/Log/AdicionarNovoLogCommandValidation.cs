using System;
using FluentValidation;
using PolarisLog.Domain.CommandSide.Commands.Log;

namespace PolarisLog.Domain.CommandSide.Validations.Log
{
    public class AdicionarNovoLogCommandValidation : AbstractValidator<AdicionarNovoLogCommand>
    {
        public AdicionarNovoLogCommandValidation()
        {
            RuleFor(log => log.UsuarioId).NotEmpty().WithName("Usuário");
            RuleFor(log => log.AmbienteId).NotEmpty().WithName("Ambiente");
            RuleFor(log => log.NivelId).NotEmpty().WithName("Nível");
            RuleFor(log => log.Titulo).NotEmpty().WithName("Título");
            RuleFor(log => log.Descricao).NotEmpty().WithName("Descrição");
            RuleFor(log => log.Origem).NotEmpty();
        }
    }
}