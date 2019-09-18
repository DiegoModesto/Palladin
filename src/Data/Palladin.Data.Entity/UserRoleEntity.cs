using System;

namespace Palladin.Data.Entity
{
    public class UserRoleEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public Guid RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
