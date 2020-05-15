using System.Threading.Tasks;
using PolarisLog.Domain.CommandSide.Validations.Log;
using PolarisLog.Domain.Entities;

namespace PolarisLog.Domain.CommandSide.Commands.Log
{
    public class AdicionarNovoLogCommand : Command
    {
        public Level Level { get; }
        public string Descricao { get; }
        public string Origem { get; set; }

        public AdicionarNovoLogCommand(Level level, string descricao, string origem)
        {
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