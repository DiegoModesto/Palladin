using Palladin.Data.Entity;
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
         private readonly MenuItemRepository _menuItemRepository;

        public MenuRepository() : base(null) { }
        public MenuRepository(PalladinContext ctx) : base(ctx)
        {
            this._menuItemRepository = new MenuItemRepository();
        }

        public MenuItemEntity GetMenuByUserId(Guid Id)
        {
            return null;//var menus = this.Find(x => x.)
        }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
