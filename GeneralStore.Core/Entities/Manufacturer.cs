using System;

namespace GeneralStore.Core.Entities
{
    public class Manufacturer : BaseEntity<int>
    {
        public string Name { get; private set; }

        public Manufacturer(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}