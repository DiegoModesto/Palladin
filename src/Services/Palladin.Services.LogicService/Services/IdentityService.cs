using AutoMapper;
using Palladin.Services.ApiContract.V1.Results;
using Palladin.Services.LogicService.Contracts;
using System;
using System.Threading.Tasks;

namespace Palladin.Services.LogicService.Services
{
    public class IdentityService : BaseLogic, IIdentityService
    {
        private IMapper _mapp;

        public IdentityService(IMapper mapp)
        {
            this._mapp = mapp;
        }

        public Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
