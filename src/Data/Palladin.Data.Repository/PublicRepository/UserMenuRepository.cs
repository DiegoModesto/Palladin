﻿using Palladin.Data.Entity;
using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using System;

namespace Palladin.Data.Repository.PublicRepository
{
    internal class UserMenuRepository : Repository<UserMenuEntity>, IUserMenuRepository, IDisposable
    {
        public PalladinContext Context
        {
            get { return _context as PalladinContext; }
        }

        public UserMenuRepository() : base(null) { }
        public UserMenuRepository(PalladinContext ctx) : base(ctx) { }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
