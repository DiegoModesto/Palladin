using Palladin.Data.Entity;
using System;

namespace Palladin.Data.Repository.Interfaces
{
    public interface ITokenRepository : IRepository<TokenEntity>
    {
        TokenEntity FirstNotDeleted(Guid userId);
        void Remove(string token);
        void Add(Guid userId, string token);
        void RemoveAllFromUserId(Guid userId);
    }
}
