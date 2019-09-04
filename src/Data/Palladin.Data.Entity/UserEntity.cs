using System;

namespace Palladin.Data.Entity
{
    public class UserEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Enums.UserType UserType { get; set; }

        public ProjectEntity Project { get; set; }
        public MenuEntity Menu { get; set; }
    }
}
