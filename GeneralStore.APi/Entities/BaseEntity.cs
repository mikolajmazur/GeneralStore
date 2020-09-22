using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Api.Entities
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
