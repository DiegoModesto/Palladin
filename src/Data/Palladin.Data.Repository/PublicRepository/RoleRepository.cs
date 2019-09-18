using Palladin.Data.Entity;
using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Palladin.Data.Repository.PublicRepository
{
    public class RoleRepository : Repository<RoleEntity>, IRoleRepository, IDisposable
    {
        public PalladinContext Context
        {
            get { return _context as PalladinContext; }
        }

        public RoleRepository() : base(null) { }
        public RoleRepository(PalladinContext ctx) : base(ctx) { }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
