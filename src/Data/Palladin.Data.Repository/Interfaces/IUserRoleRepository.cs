using Palladin.Data.Entity;
using System;
using System.Collections.Generic;

namespace Palladin.Data.Repository.Interfaces
{
    public interface IUserRoleRepository : IRepository<UserRoleEntity>
    {
        Dictionary<Guid, string> GetRolesByUserId(Guid userId);
    }
}
