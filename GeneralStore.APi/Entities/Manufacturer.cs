using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Entities
{
    public class Manufacturer : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<Product> ManufacturedProducts { get; } = new List<Product>();
    }
}
