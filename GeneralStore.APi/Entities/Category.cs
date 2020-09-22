using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category ParentCategory { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public IEnumerable<Category> SubCategories { get; } = new List<Category>();
        public IEnumerable<Product> Products { get; } = new List<Product>();
    }
}
