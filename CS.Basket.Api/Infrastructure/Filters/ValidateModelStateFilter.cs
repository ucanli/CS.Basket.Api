using CS.Basket.Api.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.Basket.Api.Infrastructure.Filters
{
    public class ValidateModelStateFilter : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var modelState = context.ModelState;
                var errorModel =
                        from x in modelState.Keys
                        where modelState[x].Errors.Count > 0
                        select new
                        {
                            key = x,
                            error = modelState[x].Errors.FirstOrDefault().ErrorMessage
                        };

                context.Result = new JsonResult(new ApiResponse
                {
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessageList = errorModel.Select(x => x.error).ToList()
                });
            }

            await next();
        }
    }
}
