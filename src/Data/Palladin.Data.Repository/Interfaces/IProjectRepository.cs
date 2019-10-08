using Palladin.Data.Entity;
using System;
using System.Collections.Generic;

namespace Palladin.Data.Repository.Interfaces
{
    public interface IProjectRepository : IRepository<ProjectEntity>
    {
        IEnumerable<ProjectEntity> GetAllWithUsersName();
        IEnumerable<ProjectEntity> GetAllByCompanyId(Guid companyId);
    }
}
