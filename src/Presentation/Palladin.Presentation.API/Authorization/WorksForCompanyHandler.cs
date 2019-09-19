﻿using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Palladin.Presentation.API.Authorization
{
    public class WorksForCompanyHandler : AuthorizationHandler<WorksForCompanyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WorksForCompanyRequirement requirement)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
            //Remove those lines to work with DomainValidation

            var userEmailAddress = context.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            if(userEmailAddress.EndsWith(requirement.DomainName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
