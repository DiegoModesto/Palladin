﻿using Palladin.Data.Entity;
using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using System;

namespace Palladin.Data.Repository.PublicRepository
{
    internal class MenuRepository : Repository<MenuEntity>, IMenuRepository, IDisposable
    {
        public PalladinContext Context
        {
            get { return _context as PalladinContext; }
        }

        public MenuRepository() : base(null) { }
        public MenuRepository(PalladinContext ctx) : base(ctx)
        {
        }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
