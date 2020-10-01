using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralStore.Api.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AvailableUnits { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
        public Guid ManufacturerId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
