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
    public class IdentityController : ControllerBase
    {
        private IIdentityService _identityService { get; }

        public IdentityController(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody]UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}