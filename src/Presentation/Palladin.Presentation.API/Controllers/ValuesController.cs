using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Palladin.Presentation.API.Helpers;
using Palladin.Services.LogicService.AuthenticationLogic;
using Palladin.Services.LogicService.Interfaces;

namespace Palladin.Presentation.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IOptions<AppSettings> appSettings;
        public IUserLogic userService { get; set; }
        public ValuesController(IOptions<AppSettings> settings, IUserLogic userLogic)
        {
            this.appSettings = settings;
            this.userService = userLogic;

            this.userService.ConnectionString = this.appSettings.Value.ConString;
        }
        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Access", "Denied" };
        }
    }
}
