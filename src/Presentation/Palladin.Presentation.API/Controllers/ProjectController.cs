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

                model.UserId = Guid.Parse(handler.ReadJwtToken(token).Claims.ToList().Find(x => x.Type.Contains("nameidentifier")).Value);

                return Ok(new ResultResponseViewModel<ProjectViewModel>
                {
                    IsSuccess = true,
                    Message = "Projeto criado com sucesso.",
                    Response = this._projectService.CreateProject(model)
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
                return Ok(new ResultResponseViewModel<string>() { IsSuccess = false, Message = "Falha ao remover projeto, tente novamente ou contate o adminsitrador.", Response = e.Message });
            }
        }

        [HttpGet("getTypeAhead")]
        public IActionResult GetTypeAheadList()
        {
            try
            {
                return Ok(new ResultResponseViewModel<IEnumerable<TypeAheadProjectViewModel>>
                {
                    IsSuccess = true,
                    Response = this._projectService.GetTypeAheadList()
                });
            }
            catch (Exception e)
            {
                return Ok(new ResultResponseViewModel<string>() { IsSuccess = false, Message = "Não foi possível listar os projetos, tente novamente mais tarde ou contate o adminsitrador.", Response = e.Message });
            }
        }

        [HttpPost("join-project")]
        public IActionResult JoinProjectWithVulnerability([FromBody] JoinProjectViewModel model)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();

                model.UserId = Guid.Parse(handler.ReadJwtToken(token).Claims.ToList().Find(x => x.Type.Contains("nameidentifier")).Value);

                return Ok(new ResultResponseViewModel<List<string>>()
                {
                    IsSuccess = true,
                    Message = "Vinculação criada com sucesso.",
                    Response = this._projectService.JoinProjectWithVulnerability(model).ToList()
                });
            }
            catch (Exception e)
            {
                return Ok(new ResultResponseViewModel<string>() { IsSuccess = false, Message = e.Message, Response = string.Empty });
            }
        }
    }
}