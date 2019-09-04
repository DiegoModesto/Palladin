using Palladin.Data.Entity;
using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Palladin.Data.Repository.PublicRepository
{
    internal class TokenRepository : Repository<TokenEntity>, ITokenRepository, IDisposable
    {
        public PalladinContext Context
        {
            get { return _context as PalladinContext; }
        }

        public TokenRepository() : base(null) { }
        public TokenRepository(PalladinContext ctx) : base(ctx) { }

        public TokenEntity FirstNotDeleted(Guid userId)
        {
            return this._context.Set<TokenEntity>().FirstOrDefault(x => 
                                                                    x.UserId.Equals(userId) && 
                                                                    x.IsDeleted.Equals(false) &&
                                                                    x.ExpirationDate <= DateTime.UtcNow.AddHours(1));
        }

        public void Remove(string token)
        {
            var tokenEntity = this.SingleOrDefault(x => x.Token.Equals(token));
            base.Remove(tokenEntity);
        }

        public void Add(Guid userId, string token)
        {
            var tokenEntity = new TokenEntity()
            {
                UserId = userId,
                Token = token,
                ExpirationDate = DateTime.UtcNow.AddHours(1)
            };
            base.Add(tokenEntity);
        }

        public void RemoveAllFromUserId(Guid userId)
        {
            var x = from t in _context.Set<TokenEntity>()
                    where t.UserId.Equals(userId)
                    select t;
            base.RemoveRange(x);
        }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
