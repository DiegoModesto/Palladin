using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Palladin.Services.ApiContract.V1.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Palladin.Presentation.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext ctx, ActionExecutionDelegate next)
        {
            if(!ctx.ModelState.IsValid)
            {
                var errorsInModelState = ctx.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value.Errors
                                .Select(y => y.ErrorMessage))
                    .ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        errorResponse.Errors.Add(
                            new ErrorModel
                            {
                                FieldName = error.Key,
                                Message = subError
                            }
                        );
                    }
                }

                ctx.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}
