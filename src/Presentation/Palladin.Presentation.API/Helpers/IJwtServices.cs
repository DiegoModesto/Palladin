using Palladin.Services.ViewModel.User;
using System.Collections.Generic;
using System.Security.Claims;

namespace Palladin.Presentation.API.Helpers
{
    public interface IJwtServices
    {
        TokenViewModel GenerateToken(string secret, int expiration, IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}
