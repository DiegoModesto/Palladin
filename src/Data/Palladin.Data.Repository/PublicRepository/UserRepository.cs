using System;
using System.Linq;
using Palladin.Data.Entity;
using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;

namespace Palladin.Data.Repository.PublicRepository
{
    internal class UserRepository : Repository<UserEntity>, IUserRepository, IDisposable
    {
        public PalladinContext Context
        {
            get { return _context as PalladinContext; }
        }

        public UserRepository() : base(null)
        {

        }
        public UserRepository(PalladinContext ctx) : base(ctx) { }

        public bool IsUserActiveById(Guid id)
        {
            return !this.SingleOrDefault(x => x.Id.Equals(id)).IsDeleted;
        }

        public bool IsUserActiveByLogin(string login)
        {
            return !this.SingleOrDefault(x => x.Login.Equals(login)).IsDeleted;
        }

        public string GetNameById(Guid id)
        {
            return this._context.Set<UserEntity>()
                                .Where(x => x.Id.Equals(id))
                                .Select(x => x.Login)
                                .FirstOrDefault();
        }

        public UserEntity IsUserAuthenticated(UserEntity entity)
        {
            var user = this.SingleOrDefault(x => x.Login.Equals(entity.Login));
            return (user.Password.Equals(entity.Password) ? user : null);
        }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
