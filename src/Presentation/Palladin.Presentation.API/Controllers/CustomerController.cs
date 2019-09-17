using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Palladin.Presentation.API.Helpers;
using Palladin.Services.LogicService.AuthenticationLogic;
using Palladin.Services.ViewModel;
using Palladin.Services.ViewModel.User;
using System;
using System.Collections.Generic;

namespace Palladin.Presentation.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IUserLogic _userService { get; }
        private readonly AppSettings _appSettings;

        public CustomerController(IUserLogic userLogic, IOptions<AppSettings> appSettings)
        {
            this._userService = userLogic;
            this._appSettings = appSettings.Value;

            this._userService.ConnectionString = this._appSettings.ConString;
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            try
            {
                return Ok(new ResultResponseViewModel<IEnumerable<CustomerViewModel>>
                {
                    IsSuccess = true,
                    Response = this._userService.GetCustomers()
                });

            }
            catch (Exception e)
            {
                return Ok(new ResultResponseViewModel<string>() { IsSuccess = false, Message = e.Message, Response = e.Message });
            }
        }
    }
}