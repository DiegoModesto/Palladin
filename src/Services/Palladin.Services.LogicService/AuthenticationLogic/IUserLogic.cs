using Palladin.Services.LogicService.Interfaces;
using Palladin.Services.ViewModel.User;
using System;
using System.Collections.Generic;

namespace Palladin.Services.LogicService.AuthenticationLogic
{
    public interface IUserLogic : IAuthentication, IBaseLogic
    {
        string CheckRoleByUserId(UserViewModel viewModel);
        string GetRefreshTokenById(Guid id);
        string GetRefreshTokenByUserName(string user);
        void DeleteRefreshToken(string token);
        void SaveRefreshToken(Guid userId, string token);
        void SaveRefreshToken(string user, string token);
        string Cipher(string pass);
        IEnumerable<CustomerViewModel> GetCustomers();
    }
}
