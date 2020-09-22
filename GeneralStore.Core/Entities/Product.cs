using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralStore.Core.Entities
{
    class Product : BaseEntity<int> 
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public Manufacturer Manufacturer { get; private set; }

        public Product(string name, string description, decimal price, Manufacturer manufacturer)
        {
            Name = name;
            Description = description;
            Price = price;
            Manufacturer = manufacturer;
        }
    }
}
