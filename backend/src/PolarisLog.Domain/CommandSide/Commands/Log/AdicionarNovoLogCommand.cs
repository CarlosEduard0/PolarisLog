using System;
using System.Threading.Tasks;
using PolarisLog.Domain.CommandSide.Validations.Log;

namespace PolarisLog.Domain.CommandSide.Commands.Log
{
    public class AdicionarNovoLogCommand : Command<Guid>
    {
        public Guid UsuarioId { get; }
        public Guid AmbienteId { get; }
        public Guid NivelId { get; }
        public string Titulo { get; }
        public string Descricao { get; }
        public string Origem { get; set; }

        public AdicionarNovoLogCommand(Guid usuarioId, Guid ambienteId, Guid nivelId, string titulo,  string descricao, string origem)
        {
            UsuarioId = usuarioId;
            AmbienteId = ambienteId;
            NivelId = nivelId;
            Titulo = titulo;
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