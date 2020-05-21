using System;
using FluentValidation;
using PolarisLog.Domain.QuerySide.Queries.Usuario;

namespace PolarisLog.Domain.QuerySide.Validations.Usuario
{
    public class ObterUsuarioPorIdQueryValidation : AbstractValidator<ObterUsuarioPorIdQuery>
    {
        public ObterUsuarioPorIdQueryValidation()
        {
            RuleFor(query => query.Id)
                .NotEqual(Guid.Empty).WithMessage("Id deve possuir conteúdo");
        }
    }
}