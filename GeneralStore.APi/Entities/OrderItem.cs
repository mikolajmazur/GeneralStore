using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid ItemId { get; set; }
        public decimal Price { get; set; }
        public string ItemName { get; set; }
        public int Amount { get; set; }
        public Guid OrderId { get; set; }
    }
}
