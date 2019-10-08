using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Palladin.Presentation.API.Controllers.V1
{
    public abstract class ApplicationController : ControllerBase
    {
        protected Guid GetCompanyIdByHeaderToken()
        {
            throw new NotImplementedException();
        }
        protected Guid GetCompanyIdByUserId()
        {
            throw new NotImplementedException();
        }
    }
}
