using System;
using System.Threading.Tasks;
using PolarisLog.Domain.CommandSide.Validations.Log;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Domain.CommandSide.Commands.Log
{
    public class AdicionarNovoLogCommand : Command<Guid>
    {
        public Guid UsuarioId { get; }
        public Level? Level { get; }
        public string Descricao { get; }
        public string Origem { get; set; }

        public AdicionarNovoLogCommand(Guid usuarioId, Level? level, string descricao, string origem)
        {
            UsuarioId = usuarioId;
            Level = level;
            Descricao = descricao;
            Origem = origem;
        }

        public override async Task<bool> EhValido()
        {
            ValidationResult = await new AdicionarNovoLogCommandValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}