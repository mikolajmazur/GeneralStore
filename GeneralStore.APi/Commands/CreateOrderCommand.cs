using GeneralStore.Api.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Commands
{
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        public IEnumerable<OrderItemCreateDto> Items { get; set; }
    }
}
