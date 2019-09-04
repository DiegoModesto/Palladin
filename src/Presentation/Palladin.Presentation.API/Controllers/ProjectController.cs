using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Palladin.Presentation.API.Helpers;
using Palladin.Services.LogicService.ProjectLogic;
using Palladin.Services.ViewModel;
using Palladin.Services.ViewModel.Project;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Palladin.Presentation.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public IProjectLogic _projectService { get; set; }
        private readonly AppSettings _appSettings;

        public ProjectController(IProjectLogic projectService, IOptions<AppSettings> appSettings)
        {
            this._projectService = projectService;
            this._appSettings = appSettings.Value;

            this._projectService.ConnectionString = this._appSettings.ConString;
        }

        [HttpGet("getList")]
        public IActionResult GetAllGeneralList()
        {
            try
            {
                return Ok(new ResultResponseViewModel<IEnumerable<ProjectViewModel>>
                {
                    IsSuccess = true,
                    Response = this._projectService.GetProjectsGeneralList()
                });
            }
            catch (Exception e)
            {
                return Ok(new ResultResponseViewModel<string>() { IsSuccess = false, Message = e.Message, Response = string.Empty });
            }
        }

        [HttpGet("getById")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(new ResultResponseViewModel<ProjectViewModel>
                {
                    IsSuccess = true,
                    Response = this._projectService.GetDetailProjectById(id)
                });
            }
            catch (Exception e)
            {
                return Ok(new ResultResponseViewModel<string>() { IsSuccess = false, Message = e.Message, Response = string.Empty });
            }
        }

        [HttpPost("edit")]
        public IActionResult EditProjectByObject([FromBody]ProjectViewModel model)
        {
            try
            {
                this._projectService.UpdateProject(model);

                return Ok(new ResultResponseViewModel<ProjectViewModel>
                {
                    IsSuccess = true,
                    Message = "Dados atualizados com sucesso."
                });
            }
            catch (Exception e)
            {
                return Ok(new ResultResponseViewModel<string>() { IsSuccess = false, Message = e.Message, Response = string.Empty });
            }
        }

        [HttpPost("add")]
        public IActionResult CreateProjectByObject([FromBody]ProjectViewModel model)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();

                model.UserId = Guid.Parse(handler.ReadJwtToken(token).Claims.ToList()[0].Value);

                this._projectService.CreateProject(model);

                return Ok(new ResultResponseViewModel<ProjectViewModel>
                {

                    IsSuccess = true,
                    Message = "Projeto criado com sucesso."
                });
            }
            catch (Exception e)
            {
                return Ok(new ResultResponseViewModel<string>() { IsSuccess = false, Message = e.Message, Response = string.Empty });
            }
        }

        [HttpGet("remove")]
        public IActionResult RemoveById(Guid id)
        {
            try
            {
                this._projectService.RemoveById(id);
                return Ok(new ResultResponseViewModel<ProjectViewModel> { IsSuccess = true, Message = "Projeto removido com sucesso." });
            }
            catch (Exception e)
            {
                return Ok(new ResultResponseViewModel<string>() { IsSuccess = false, Message = e.Message, Response = string.Empty });
            }
        }
    }
}