using Palladin.Services.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Palladin.Presentation.API.Helpers
{
    public class JwtServices : IJwtServices
    {

        public TokenViewModel GenerateToken(string secret, int expiration, IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var jwt = new JwtSecurityToken(
                audience: "Everyone",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: System.DateTime.UtcNow.AddMinutes(expiration),
                signingCredentials:new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new TokenViewModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                RefreshToken = this.GenerateRefreshToken()
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
