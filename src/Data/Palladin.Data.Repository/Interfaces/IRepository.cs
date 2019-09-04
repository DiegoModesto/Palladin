using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Palladin.Data.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(Guid Id);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(int skip, int take);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where, int skip, int take);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(Guid id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
