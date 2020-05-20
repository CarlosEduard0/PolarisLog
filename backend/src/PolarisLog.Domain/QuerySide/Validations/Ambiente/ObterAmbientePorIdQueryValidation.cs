using System;
using FluentValidation;
using PolarisLog.Domain.QuerySide.Queries.Ambiente;

namespace PolarisLog.Domain.QuerySide.Validations.Ambiente
{
    public class ObterAmbientePorIdQueryValidation : AbstractValidator<ObterAmbientePorIdQuery>
    {
        public ObterAmbientePorIdQueryValidation()
        {
            RuleFor(query => query.Id)
                .NotEqual(Guid.Empty).WithMessage("Id deve possuir conteúdo");
        }
    }
}