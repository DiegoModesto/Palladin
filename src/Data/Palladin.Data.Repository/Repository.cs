using Microsoft.EntityFrameworkCore;
using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Palladin.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            this._context = context ?? new PalladinContext();
        }

        public virtual TEntity GetById(Guid Id)
        {
            return _context.Set<TEntity>().Find(Id);
        }
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().SingleOrDefault(filter);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        public virtual IEnumerable<TEntity> GetAll(int skip, int take)
        {
            take = take == 0 ? 10 : take;
            return _context.Set<TEntity>().Skip(skip).Take(take).ToList();
        }
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where, int skip, int take)
        {
            take = take == 0 ? 10 : take;
            return _context.Set<TEntity>().Skip(skip).Take(take).Where(where).ToList();
        }
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().Where(filter);
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry<TEntity>(entity).State = EntityState.Modified;
                _context.Set<TEntity>().Update(entity);
            }
            //_context.Set<TEntity>().UpdateRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public virtual void Remove(Guid id)
        {
            _context.Set<TEntity>().Remove(this.GetById(id));
        }
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().Any(filter);
        }
    }
}
