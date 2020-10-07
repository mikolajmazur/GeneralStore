using GeneralStore.Api.Dtos;
using GeneralStore.Api.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Queries
{
    public class GetOrdersQuery : GetPagedResultBaseQuery<OrderDto>
    {
        public GetOrdersQuery(int? pageNumber, int? pageSize) : base(pageNumber, pageSize)
        {
        }
    }
}
