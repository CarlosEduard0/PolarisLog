using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;

namespace PolarisLog.Domain.CommandSide
{
    public abstract class Command<T> : IRequest<T>
    {
        public ValidationResult ValidationResult { get; set; }

        public abstract Task<bool> EhValido();
    }
}