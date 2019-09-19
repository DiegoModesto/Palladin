using Palladin.Data.Entity;
using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Palladin.Data.Repository.PublicRepository
{
    public class ProjectRepository : Repository<ProjectEntity>, IProjectRepository, IDisposable
    {
        public PalladinContext Context
        {
            get { return _context as PalladinContext; }
        }

        public ProjectRepository() : base(null) { }
        public ProjectRepository(PalladinContext ctx) : base(ctx) { }

        public override ProjectEntity GetById(Guid Id)
        {
            return (from p in _context.Set<ProjectEntity>()
                   join cus in _context.Set<UserEntity>() on p.CustomerId equals cus.Id
                   join uss in _context.Set<UserEntity>() on p.UserId equals uss.Id
                   where p.Id.Equals(Id)
                   select new ProjectEntity()
                   {
                       Id = p.Id,
                       Name = p.Name,
                       CreatedDate = p.CreatedDate,
                       Customer = cus,
                       User = uss,
                       CustomerId = cus.Id,
                       UserId = uss.Id,
                       EndDate = p.EndDate,
                       InitialDate = p.InitialDate,
                       IsDeleted = p.IsDeleted,
                       ProjectType = p.ProjectType,
                       Subsidiary = p.Subsidiary
                   })
                   .FirstOrDefault();
        }

        public IEnumerable<ProjectEntity> GetAllWithUsersName()
        {
            return from p in _context.Set<ProjectEntity>()
                    join cus in _context.Set<UserEntity>() on p.CustomerId equals cus.Id
                    join uss in _context.Set<UserEntity>() on p.UserId equals uss.Id
                    select new ProjectEntity()
                    {
                        Id = p.Id,
                        CreatedDate = p.CreatedDate,
                        Customer = cus,
                        User = uss,
                        CustomerId = cus.Id,
                        UserId = uss.Id,
                        EndDate = p.EndDate,
                        InitialDate = p.InitialDate,
                        IsDeleted = p.IsDeleted,
                        ProjectType = p.ProjectType,
                        Subsidiary = p.Subsidiary
                    };
        }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
