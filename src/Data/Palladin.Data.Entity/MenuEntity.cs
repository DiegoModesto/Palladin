using System;

namespace Palladin.Data.Entity
{
    public class MenuEntity
    {
        public Guid Id { get; set; }
        public short Order { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public Guid? ParentId { get; set; }
        public MenuEntity Parent { get; set; }
    }
}
