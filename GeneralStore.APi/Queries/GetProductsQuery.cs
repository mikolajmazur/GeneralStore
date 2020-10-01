using GeneralStore.Api.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductSimplifiedDto>>
    {
		private int _pageNumber;
		private int _pageSize;

		public int MaxPageSize => 20;

		public int PageNumber
		{
			get { return _pageNumber; }
			set { _pageNumber = value > 0 ? value : 1; }
		}

		public int PageSize 
		{ 
			get { return _pageSize; }
			set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
		}

        public Guid? CategoryId { get; set; }
    }
}
