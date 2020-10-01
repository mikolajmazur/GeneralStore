using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Dtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public Guid? ParentCategory { get; set; }
    }
}
