using Palladin.Services.ApiContract.V1.Request.Queries;
using Palladin.Services.ApiContract.V1.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Palladin.Services.LogicService.Contracts
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponse>> GetAllAsync(Guid companyId);
        Task<PagedResponse<ProjectResponse>> GetAllAsync(PaginationQuery query);
    }
}
