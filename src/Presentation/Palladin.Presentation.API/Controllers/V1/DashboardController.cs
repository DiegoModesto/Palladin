using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Palladin.Services.ApiContract.V1;
using Palladin.Services.ApiContract.V1.Request;
using Palladin.Services.ApiContract.V1.Responses;
using Palladin.Services.LogicService.Contracts;
using System.Threading.Tasks;

namespace Palladin.Presentation.API.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DashboardController : ControllerBase
    {
        private IDashboardService _dashboardService { get; }

        public DashboardController(IDashboardService dashboardService)
        {
            this._dashboardService = dashboardService;
        }

        [HttpPost(ApiRoutes.Dashboard.GetCriticalSummary)]
        public async Task<IActionResult> GetCriticalSymmary()
        {
            return Ok(await _dashboardService.GetCriticalSummaryAsync());
        }

        [HttpPost(ApiRoutes.Dashboard.GetHighSummary)]
        public async Task<IActionResult> GetHighSummary()
        {
            return Ok(await _dashboardService.GetHighSummaryAsync());
        }

        [HttpPost(ApiRoutes.Dashboard.GetMiddleSummary)]
        public async Task<IActionResult> GetMiddleSummary()
        {
            return Ok(await _dashboardService.GetMiddleSummaryAsync());
        }

        [HttpPost(ApiRoutes.Dashboard.GetLowSummary)]
        public async Task<IActionResult> GetLowSummary()
        {
            return Ok(await _dashboardService.GetLowSummaryAsync());
        }
    }
}
