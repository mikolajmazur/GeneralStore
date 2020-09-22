using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralStore.Data.Entities
{
    public class BaseEntity<T>
    {
        public virtual T Id { get; protected set; }
    }
}
