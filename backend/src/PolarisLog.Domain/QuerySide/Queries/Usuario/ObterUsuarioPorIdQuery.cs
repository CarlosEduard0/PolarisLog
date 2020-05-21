using System;
using System.Threading.Tasks;
using PolarisLog.Domain.QuerySide.Validations.Usuario;

namespace PolarisLog.Domain.QuerySide.Queries.Usuario
{
    public class ObterUsuarioPorIdQuery : Query<Entities.Usuario>
    {
        public Guid Id { get; }

        public ObterUsuarioPorIdQuery(Guid id)
        {
            Id = id;
        }

        public virtual async Task<bool> EhValido()
        {
            ValidationResult = await new ObterUsuarioPorIdQueryValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}