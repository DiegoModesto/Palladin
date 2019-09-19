using Palladin.Services.ApiContract.V1.Results;
using System.Threading.Tasks;

namespace Palladin.Services.LogicService.Contracts
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(string login, string password);
        Task<AuthenticationResult> RefreshToken(string token, string refreshToken);
    }
}
