using System;

namespace Palladin.Data.Entity
{
    public class MenuItemEntity
    {
        public Guid Id { get; set; }
        public short Order { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public Guid MenuId { get; set; }
        public MenuEntity Menu { get; set; }
    }
}
