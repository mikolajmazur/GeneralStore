using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AvailableUnits { get; set; }
        public Guid ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
