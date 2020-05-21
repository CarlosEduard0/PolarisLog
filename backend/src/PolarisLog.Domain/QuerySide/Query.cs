using FluentValidation.Results;
using MediatR;

namespace PolarisLog.Domain.QuerySide
{
    public abstract class Query<T> : IRequest<T>
    {
        private const int MaxPageSize = 20;
        public int PageNumber { get; }

        private int _pageSize = 20;
        public int PageSize { 
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? value : _pageSize;
        }

        protected Query()
        {
        }
        
        protected Query(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        
        public ValidationResult ValidationResult { get; set; }
    }
}