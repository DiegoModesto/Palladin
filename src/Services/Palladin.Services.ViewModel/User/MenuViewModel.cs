using System;
using System.Collections.Generic;

namespace Palladin.Services.ViewModel.User
{
    public class MenuViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<MenuItemViewModel> MenuItems { get; set; }

        public MenuViewModel()
        {
            this.MenuItems = new List<MenuItemViewModel>();
        }
    }

    public class MenuItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
