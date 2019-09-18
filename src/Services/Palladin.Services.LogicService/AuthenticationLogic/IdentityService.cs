using Palladin.Data.Entity;
using Palladin.Services.LogicService.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Palladin.Services.LogicService.AuthenticationLogic
{
    public class IdentityService : IIdentityService
    {
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly JwtSettings _jwtSettings;
        //private readonly TokenValidationParameters _tokenValidationParameters;
        //private readonly DataContext _context;

        public Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        #region [Private Method's]
        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("");

            throw new NotImplementedException();
        }
        #endregion
    }
}
