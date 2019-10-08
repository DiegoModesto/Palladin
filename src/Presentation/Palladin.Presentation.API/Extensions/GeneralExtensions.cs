using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Palladin.Presentation.API.Extensions
{
    public static class GeneralExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            if(httpContext.User == null)
                return Guid.Empty;

            return Guid.Parse(httpContext.User.Claims.Single(x => x.Type == "Id").Value);
        }

        public static Guid GetCompanyId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return Guid.Empty;

            return Guid.Parse(httpContext.User.Claims.Single(x => x.Type == "Company").Value);
        }
    }
}
