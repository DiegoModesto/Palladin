using Palladin.Services.LogicService.Interfaces;
using Palladin.Services.ViewModel.User;
using System;

namespace Palladin.Services.LogicService.MenusLogic
{
    public interface IMenuLogic : IBaseLogic
    {
        MenuViewModel GetMenuByUserId(Guid id);
    }
}
