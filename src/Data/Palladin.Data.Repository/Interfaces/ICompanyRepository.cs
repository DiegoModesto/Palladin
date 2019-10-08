using Palladin.Data.Entity;
using System;

namespace Palladin.Data.Repository.Interfaces
{
    public interface ICompanyRepository : IRepository<CompanyEntity>
    {
        bool IsMaster(Guid companyId);
    }
}
