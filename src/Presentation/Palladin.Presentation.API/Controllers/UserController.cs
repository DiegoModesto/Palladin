using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Palladin.Presentation.API.Helpers;
using Palladin.Services.LogicService.AuthenticationLogic;
using Palladin.Services.LogicService.MenusLogic;
using Palladin.Services.ViewModel;
using Palladin.Services.ViewModel.User;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Palladin.Presentation.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserLogic _userService { get; }
        private IMenuLogic _menuService { get; }
        private readonly AppSettings _appSettings;
        private IJwtServices _jwtServices { get; }

        public UserController(IUserLogic userLogic, IMenuLogic menuLogic, IJwtServices jwt, IOptions<AppSettings> appSettings)
        {
            this._jwtServices = jwt;
            this._userService = userLogic;
            this._menuService = menuLogic;
            this._appSettings = appSettings.Value;

            this._userService.ConnectionString = this._appSettings.ConString;
            this._menuService.ConnectionString = this._appSettings.ConString;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginPasswordViewModel userViewModel)
        {
            try
            {
                var user = _userService.Authenticate(userViewModel);
                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                var menuViewModel = _menuService.GetMenuByUserId(user.Id);
                var userClaims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                var token = new JwtServices().GenerateToken(_appSettings.Secret, _appSettings.ExpirationTokenTime, userClaims);

                this._userService.SaveRefreshToken(user.Id, token.RefreshToken);

                return Ok(new ResultResponseViewModel<object>(){
                    IsSuccess = true,
                    Response = new {
                        token.Token,
                        token.RefreshToken,
                        Menu = menuViewModel
                    }
                });
            }
            catch (Exception e)
            {
                return Ok(new ResultResponseViewModel<Exception>() { IsSuccess = false, Message = "Usuário e/ou Senha inválidos", Response = e });
            }
        }

        [AllowAnonymous]
        [HttpPost("checkRefreshToken")]
        public IActionResult CheckRefreshTokenIsActive([FromBody]RefreshTokenViewModel model)
        {
            try
            {
                var principal = GetPrincipalFromExpiredToken(model.Token);
                var savedRefreshToken = _userService.GetRefreshTokenByUserName(principal.Identity.Name);
                if (savedRefreshToken != model.RefreshToken)
                    throw new SecurityTokenException("Invalid refresh token");

                if(this.TokenExpired(model.Token))
                {
                    return Ok(new ResultResponseViewModel<object>()
                    {
                        IsSuccess = true,
                        Response = this.RefreshToken(model.Token, model.RefreshToken)
                    });
                }

                return Ok(new ResultResponseViewModel<string>() { IsSuccess = true, Message = string.Empty });
            }
            catch (Exception e)
            {
                return Ok(new ResultResponseViewModel<string>() { IsSuccess = false, Message = "Usuário e/ou Senha inválidos", Response = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody]RefreshTokenViewModel model)
        {
            return Ok(new ResultResponseViewModel<object>() {
                IsSuccess = true,
                Response = this.RefreshToken(model.Token, model.RefreshToken)
            });
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret)),
                ValidateLifetime = false
            };

            SecurityToken securityToken;
            var principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private object RefreshToken(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var savedRefreshToken = _userService.GetRefreshTokenByUserName(principal.Identity.Name);
            if (savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newJwtToken = new JwtServices().GenerateToken(_appSettings.Secret, _appSettings.ExpirationTokenTime, null);
            _userService.DeleteRefreshToken(refreshToken);
            _userService.SaveRefreshToken(principal.Identity.Name, newJwtToken.RefreshToken);

            return newJwtToken;
        }

        private bool TokenExpired(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).ValidTo <= DateTime.UtcNow;
        }
    }
}