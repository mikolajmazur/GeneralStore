using GeneralStore.Api.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Queries
{
    public class GetOrderByIdQuery : GetEntityByIdBaseQuery<OrderDto>
    {
    }
}
