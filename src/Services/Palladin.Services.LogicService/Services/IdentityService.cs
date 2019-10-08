using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Palladin.Data.Entity;
using Palladin.Data.Repository;
using Palladin.Services.ApiContract.V1.Results;
using Palladin.Services.LogicService.Contracts;
using Palladin.Services.LogicService.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Palladin.Services.LogicService.Services
{
    public class IdentityService : BaseLogic, IIdentityService
    {
        private readonly IMapper _mapp;
        private readonly AppSettings _appSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public IdentityService(IMapper mapp, AppSettings appSettings, TokenValidationParameters tokenValidationParameters)
        {
            this._mapp = mapp;
            this._appSettings = appSettings;
            this._tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<AuthenticationResult> LoginAsync(string login, string password)
        {
            using (var uow = new UnitOfWork(this._appSettings.ConString))
            {
                var user = uow._userR
                                    .SingleOrDefault(x =>
                                        x.Login.Equals(login));

                if (user == null)
                    return DefaultErrorReturn(ErrorsString.UserPasswordNotMatch);
                if (!user.Password.Equals(Crypto.Encrypt(password)))
                    return DefaultErrorReturn(ErrorsString.UserPasswordNotMatch);
                if (user.IsBlocked)
                    return DefaultErrorReturn(ErrorsString.UserHasBlocked);
                if (user.IsDeleted)
                    return DefaultErrorReturn(ErrorsString.UserHasDeleted);
                

                return GenerateAuthenticationResultForUser(user);
            }
        }
        public async Task<AuthenticationResult> RefreshToken(string token, string refreshToken)
        {
            var valitedToken = GetPrincipalFromToken(token);
            var userId = Guid.Empty;

            using (var uow = new UnitOfWork(this._appSettings.ConString))
            {
                RefreshTokenEntity storedRefreshToken = uow._refreshTokenR.SingleOrDefault(x => x.Token.Equals(refreshToken));

                if (valitedToken == null)
                    return DefaultErrorReturn(ErrorsString.InvalidToken);

                var expiryDateUnix =
                    long.Parse(valitedToken.Claims.Single(x => x.Type.Equals(JwtRegisteredClaimNames.Exp)).Value);

                var expiryDateTimeUtc = new DateTime(1940, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddSeconds(expiryDateUnix);

                if (expiryDateTimeUtc > DateTime.UtcNow)
                    return DefaultErrorReturn(ErrorsString.TokenHasntExpiredYet);

                var jti = valitedToken.Claims.Single(x => x.Type.Equals(JwtRegisteredClaimNames.Jti)).Value;
                userId = Guid.Parse(valitedToken.Claims.Single(x => x.Type.Equals("Id")).Value);

                if (storedRefreshToken == null)
                    return DefaultErrorReturn(ErrorsString.TokenDoesNotExist);
                if (DateTime.UtcNow > storedRefreshToken.ExpirationDate)
                    return DefaultErrorReturn(ErrorsString.TokenHasExpired);
                if (storedRefreshToken.IsInvalided)
                    return DefaultErrorReturn(ErrorsString.TokenInvalidated);
                if (storedRefreshToken.IsUsed)
                    return DefaultErrorReturn(ErrorsString.TokenIsUsed);
                if (storedRefreshToken.JwtId != jti)
                    return DefaultErrorReturn(ErrorsString.TokenDoesNotMatchWithJWT);

                storedRefreshToken.IsUsed = true;
                uow._refreshTokenR.Update(storedRefreshToken);

                uow.Complete();
            }

            return GenerateAuthenticationResultForUser(userId);
        }
        public async Task<Guid> GetCompanyIdByUserId(string token)
        {
            var valitedToken = GetPrincipalFromToken(token);
            var userId = Guid.Empty;
            if (valitedToken == null)
                return userId;

            var jti = valitedToken.Claims.Single(x => x.Type.Equals(JwtRegisteredClaimNames.Jti)).Value;
            userId = Guid.Parse(valitedToken.Claims.Single(x => x.Type.Equals("Id")).Value);

            using (var uow = new UnitOfWork(this._appSettings.ConString))
            {
                return uow._userR.GetById(userId).CompanyId;
            }
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, this._tokenValidationParameters, out var validatedToken);
                if(!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private IEnumerable<Claim> GetUserClaims(Guid userId)
        {
            using (var uow = new UnitOfWork(this._appSettings.ConString))
            {
                var ret = new List<Claim>();
                foreach (var x in uow._userRoleR.GetRolesByUserId(userId))
                {
                    yield return new Claim(x.Key.ToString(), x.Value);
                }
            }
        }
        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                    jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase);
        }
        private AuthenticationResult DefaultErrorReturn(string message)
        {
            return new AuthenticationResult { Errors = new[] { message } };
        }
        private AuthenticationResult GenerateAuthenticationResultForUser(Guid userId)
        {
            UserEntity user;
            using (var uow = new UnitOfWork(this._appSettings.ConString))
                user = uow._userR.SingleOrDefault(x => x.Id.Equals(userId));

            return GenerateAuthenticationResultForUser(user);
        }
        private AuthenticationResult GenerateAuthenticationResultForUser(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._appSettings.Secret);
            var claims = new List<Claim>
            {
                new Claim(type: "Id", value: user.Id.ToString()),
                new Claim(type: "Company", value: user.CompanyId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(this.GetUserClaims(user.Id));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(this._appSettings.TokenLifeTime),
                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = new RefreshTokenEntity
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(30)
            };

            using(var uow = new UnitOfWork(this._appSettings.ConString))
            {
                uow._refreshTokenR.Add(refreshToken);
                uow.Complete();
            }

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
