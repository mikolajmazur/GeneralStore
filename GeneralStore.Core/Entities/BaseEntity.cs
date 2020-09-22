using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralStore.Core.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; protected set; }
    }
}
