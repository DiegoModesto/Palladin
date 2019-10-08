using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Palladin.Presentation.API.Extensions;
using Palladin.Services.ApiContract.V1;
using Palladin.Services.ApiContract.V1.Request;
using Palladin.Services.ApiContract.V1.Request.Queries;
using Palladin.Services.LogicService.Contracts;
using System.Threading.Tasks;

namespace Palladin.Presentation.API.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectController : ApplicationController
    {
        private IProjectService _projectService { get; }

        public ProjectController(IProjectService projectService)
        {
            this._projectService = projectService;
        }

        [HttpGet(ApiRoutes.Project.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await this._projectService.GetAllAsync(this.HttpContext.GetCompanyId()));
        }
    }
}