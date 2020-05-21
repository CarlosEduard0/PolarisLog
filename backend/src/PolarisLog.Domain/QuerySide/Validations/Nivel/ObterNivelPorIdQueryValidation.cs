using System;
using FluentValidation;
using PolarisLog.Domain.QuerySide.Queries.Nivel;

namespace PolarisLog.Domain.QuerySide.Validations.Nivel
{
    public class ObterNivelPorIdQueryValidation : AbstractValidator<ObterNivelPorIdQuery>
    {
        public ObterNivelPorIdQueryValidation()
        {
            RuleFor(query => query.Id)
                .NotEqual(Guid.Empty).WithMessage("Id deve possuir conteúdo");
        }
    }
}