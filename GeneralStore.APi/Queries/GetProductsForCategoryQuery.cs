using GeneralStore.Api.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Queries
{
    public class GetProductsForCategoryQuery : IRequest<IEnumerable<ProductDto>>
    {
        public Guid CategoryId { get; set; }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        public int PageNumber { get; set; } = 1;
        public const int MaxPageSize = 20;

        private int _pageSize = MaxPageSize;
    }
}
