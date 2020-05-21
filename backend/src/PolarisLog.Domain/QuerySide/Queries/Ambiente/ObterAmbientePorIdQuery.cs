using System;
using System.Threading.Tasks;
using PolarisLog.Domain.QuerySide.Validations.Ambiente;

namespace PolarisLog.Domain.QuerySide.Queries.Ambiente
{
    public class ObterAmbientePorIdQuery : Query<Entities.Ambiente>
    {
        public Guid Id { get; }

        public ObterAmbientePorIdQuery(Guid id)
        {
            Id = id;
        }

        public virtual async Task<bool> EhValido()
        {
            ValidationResult = await new ObterAmbientePorIdQueryValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}