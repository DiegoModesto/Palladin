using AutoMapper;
using Palladin.Data.Entity;
using Palladin.Data.Repository;
using Palladin.Services.LogicService.Interfaces;
using Palladin.Services.ViewModel.User;
using System;

namespace Palladin.Services.LogicService.MenusLogic
{
    public class MenuLogic : BaseLogic, IMenuLogic, IBaseLogic
    {
        private IMapper _mapp;
        public MenuLogic(IMapper map)
        {
            this._mapp = map;
        }

        public MenuViewModel GetMenuByUserId(Guid id)
        {
            var menuViewModel = new MenuViewModel();

            try
            {
                using(var uow = new UnitOfWork(ConnectionString))
                {
                    var menuId = uow._userMenuR.SingleOrDefault(x => x.UserId.Equals(id)).MenuId;
                    var menu = uow._menuR.SingleOrDefault(x => x.Id.Equals(menuId));

                    menuViewModel = this._mapp.Map<MenuEntity, MenuViewModel>(menu);
                }

                return menuViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
