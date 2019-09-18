using AutoMapper;
using Palladin.Data.Entity;
using Palladin.Data.Repository;
using Palladin.Services.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Palladin.Services.LogicService.AuthenticationLogic
{
    public class UserLogic : BaseLogic, IUserLogic
    {
        private IMapper _mapp;
        public UserLogic(IMapper map)
        {
            this._mapp = map;
        }

        public UserViewModel Authenticate(LoginPasswordViewModel viewModel)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                viewModel.Password = Crypto.Encrypt(viewModel.Password);
                var user = this._mapp.Map<LoginPasswordViewModel, UserEntity>(viewModel);
                //user = uow._userR.IsUserAuthenticated(user);

                if (user != null)
                    return this._mapp.Map<UserEntity, UserViewModel>(user);

                throw new System.Exception("Usuário e/ou Senha inválidos");
            }
        }

        public IEnumerable<CustomerViewModel> GetCustomers()
        {
            using(var uow = new UnitOfWork(ConnectionString))
            {
                return _mapp.Map<IEnumerable<UserEntity>, IEnumerable<CustomerViewModel>>(uow._userR.GetAll());
                    //.Where(x => x.UserType.Equals(Enums.UserType.Client)));
            }
        }

        public string Cipher(string pass)
        {
            return Crypto.Encrypt(pass);
        }

        public string CheckRoleByUserId(UserViewModel viewModel)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                var user = uow._userR.GetById(viewModel.Id);
                if (user != null)
                    return string.Empty;// user.UserType.ToString();

                throw new Exception("Usuário não existente.");
            }
        }

        public string GetRefreshTokenById(Guid id)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                var token = uow._refreshTokenR.GetById(id);
                if (token != null)
                    return token.Token;

                throw new Exception("Token expirado");
            }
        }

        public string GetRefreshTokenByUserName(string name)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                var user = uow._userR.SingleOrDefault(x => x.UserName.Equals(name));
                var token = uow._refreshTokenR.FirstNotDeleted(user.Id);
                if (token != null)
                    return token.Token;

                throw new Exception("Token expirado");
            }
        }

        public void DeleteRefreshToken(string token)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                uow._refreshTokenR.Remove(token);
                uow.Complete();
            }
        }

        public void SaveRefreshToken(Guid userId, string token)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                uow._refreshTokenR.RemoveAllFromUserId(userId);
                uow._refreshTokenR.Add(userId, token);
                uow.Complete();
            }
        }

        public void SaveRefreshToken(string user, string token)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                var uss = uow._userR.SingleOrDefault(x => x.UserName.Equals(user));
                uow._refreshTokenR.Add(uss.Id, token);
                uow.Complete();
            }
        }
    }
}
