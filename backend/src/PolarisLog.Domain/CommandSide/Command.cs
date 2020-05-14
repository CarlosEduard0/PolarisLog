using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;

namespace PolarisLog.Domain.CommandSide
{
    public abstract class Command : IRequest
    {
        public ValidationResult ValidationResult { get; set; }

        public virtual Task<bool> EhValido()
        {
            throw new NotImplementedException();
        }
    }
}