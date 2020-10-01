using GeneralStore.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Entities
{
    public class Order : BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalValue { get; set; }
        public Status CurrentStatus { get; set; } = Status.Created;
        //public Address ShippingAddress { get; set; }

        public enum Status
        {
            Created,
            ConfirmedByStaff,
            Shipped,
            Cancelled
        }
    }
}
