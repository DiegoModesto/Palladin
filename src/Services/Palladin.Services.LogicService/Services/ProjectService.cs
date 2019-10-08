using AutoMapper;
using Palladin.Data.Entity;
using Palladin.Data.Repository;
using Palladin.Services.ApiContract.V1.Request.Queries;
using Palladin.Services.ApiContract.V1.Responses;
using Palladin.Services.LogicService.Contracts;
using Palladin.Services.LogicService.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Palladin.Services.LogicService.Services
{
    public class ProjectService : BaseLogic, IProjectService
    {
        private readonly IMapper _mapp;
        private readonly AppSettings _appSettings;

        public ProjectService(IMapper mapp, AppSettings appSettings)
        {
            this._mapp = mapp;
            this._appSettings = appSettings;
        }

        public async Task<IEnumerable<ProjectResponse>> GetAllAsync(Guid companyId)
        {
            using(var uow = new UnitOfWork(this._appSettings.ConString))
            {
                IEnumerable<ProjectEntity> projects;
                if (!uow._companyR.IsMaster(companyId))
                    projects = uow._projectR.GetAllByCompanyId(companyId); 
                else
                    projects = uow._projectR.GetAll();

                return _mapp.Map<IEnumerable<ProjectEntity>, IEnumerable<ProjectResponse>>(projects);
            }
        }

        public Task<PagedResponse<ProjectResponse>> GetAllAsync(PaginationQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
