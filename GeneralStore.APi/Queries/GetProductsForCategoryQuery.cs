using GeneralStore.Api.Dtos;
using GeneralStore.Api.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Queries
{
    public class GetProductsForCategoryQuery : IRequest<PagedList<ProductSimplifiedDto>>
    {
        public Guid CategoryId { get; set; }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize || value < 1 ? MaxPageSize : value; }
        }
        public int PageNumber 
        { 
            get { return _pageNumber; } 
            set { _pageNumber = value > 0 ? value : 1; }
        }
        public const int MaxPageSize = 20;

        public GetProductsForCategoryQuery(int? pageNumber, int? pageSize)
        {
            if (pageNumber.HasValue)
            {
                this.PageNumber = pageNumber.Value;
            }
            if (pageSize.HasValue)
            {
                this.PageSize = pageSize.Value;
            }
        }

        private int _pageSize = MaxPageSize;
        private int _pageNumber = 1;
    }
}
