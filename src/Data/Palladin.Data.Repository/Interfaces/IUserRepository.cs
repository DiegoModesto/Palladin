using Palladin.Data.Entity;
using System;

namespace Palladin.Data.Repository.Interfaces
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        bool IsUserActiveById(Guid id);
        bool IsUserActiveByLogin(string login);
        string GetNameById(Guid id);
        UserEntity IsUserAuthenticated(UserEntity entity);
    }
}
