using CS.Basket.Api.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CS.Basket.Api.Infrastructure.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            var response = new ApiResponse();

            response.ErrorMessageList = new List<string> { context.Exception.Message };
            response.IsSuccess = false;
            response.HttpStatusCode = HttpStatusCode.InternalServerError;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(response);

        }
    }
}
