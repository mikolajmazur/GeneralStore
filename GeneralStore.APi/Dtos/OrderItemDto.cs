using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Dtos
{
    public class OrderItemDto
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public Guid ItemId { get; set; }
        public int Amount { get; set; }
    }
}
