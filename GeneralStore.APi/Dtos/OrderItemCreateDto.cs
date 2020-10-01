using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Dtos
{
    public class OrderItemCreateDto
    {
        public Guid ItemId { get; set; }
        public int Amount { get; set; }
    }
}
