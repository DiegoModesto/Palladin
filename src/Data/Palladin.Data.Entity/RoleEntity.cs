using System;
using System.Collections.Generic;

namespace Palladin.Data.Entity
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public short Level { get; set; }

        public virtual ICollection<UserRoleEntity> Users { get; set; }
    }
}
