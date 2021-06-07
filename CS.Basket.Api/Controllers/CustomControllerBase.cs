using CS.Basket.Api.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.Basket.Api.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult ResponseOk(object value)
        {
            var response = new ApiResponse<object>();
            response.IsSuccess = true;
            response.Data = value;
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;

            return base.Ok(response);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult ResponseOk()
        {
            var response = new ApiResponse();
            response.IsSuccess = true;
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;

            return base.Ok();
        }
    }
}
