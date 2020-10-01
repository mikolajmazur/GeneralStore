using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalValue { get; set; }
        public string CurrentStatus { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
