using System;
using System.Collections.Generic;

namespace Palladin.Data.Entity
{
    public class UserEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public Enums.UserType UserType { get; set; }
        public ProjectEntity Project { get; set; }
        public MenuEntity Menu { get; set; }

        public Guid CompanyId { get; set; }
        public CompanyEntity Company { get; set; }

        public virtual ICollection<UserRoleEntity> Roles { get; set; }
    }
}
