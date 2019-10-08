using Palladin.Data.Entity;
using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Palladin.Data.Repository.PublicRepository
{
    public class CompanyRepository : Repository<CompanyEntity>, ICompanyRepository, IDisposable
    {
        public PalladinContext Context
        {
            get { return _context as PalladinContext; }
        }

        public CompanyRepository() : base(null) { }
        public CompanyRepository(PalladinContext ctx) : base(ctx)
        {
        }

        public bool IsMaster(Guid companyId)
        {
            return this.GetById(companyId).MasterCompany;
        }


        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
