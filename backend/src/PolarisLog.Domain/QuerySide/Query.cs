using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;

namespace PolarisLog.Domain.QuerySide
{
    public abstract class Query<T> : IRequest<T>
    {
        public ValidationResult ValidationResult { get; set; }

        public abstract Task<bool> EhValido();
    }
}