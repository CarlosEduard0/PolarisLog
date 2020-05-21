using System;
using System.Threading.Tasks;
using PolarisLog.Domain.QuerySide.Validations.Nivel;

namespace PolarisLog.Domain.QuerySide.Queries.Nivel
{
    public class ObterNivelPorIdQuery : Query<Entities.Nivel>
    {
        public Guid Id { get; }

        public ObterNivelPorIdQuery(Guid id)
        {
            Id = id;
        }

        public virtual async Task<bool> EhValido()
        {
            ValidationResult = await new ObterNivelPorIdQueryValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}