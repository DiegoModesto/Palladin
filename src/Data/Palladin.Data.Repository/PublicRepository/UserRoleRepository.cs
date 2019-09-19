using Palladin.Data.Entity;
using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Palladin.Data.Repository.PublicRepository
{
    public class UserRoleRepository : Repository<UserRoleEntity>, IUserRoleRepository, IDisposable
    {
        public PalladinContext Context
        {
            get { return _context as PalladinContext; }
        }

        public UserRoleRepository() : base(null) { }
        public UserRoleRepository(PalladinContext ctx) : base(ctx)
        {
        }

        public Dictionary<Guid, string> GetRolesByUserId(Guid userId)
        {
            return (from ur in _context.Set<UserRoleEntity>()
                         join u in _context.Set<UserEntity>() on ur.UserId equals u.Id
                         join r in _context.Set<RoleEntity>() on ur.RoleId equals r.Id
                         select new
                         {
                             r.Id,
                             r.Name
                         })
                         .ToDictionary(x => x.Id, y => y.Name);
        }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
