using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public IEnumerable<CategoryDto> SubCategories { get; set; }
    }
}
