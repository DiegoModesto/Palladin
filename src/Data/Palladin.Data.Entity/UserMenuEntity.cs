using System;

namespace Palladin.Data.Entity
{
    public class UserMenuEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public Guid MenuId { get; set; }
        public MenuEntity Menu { get; set; }
    }
}
