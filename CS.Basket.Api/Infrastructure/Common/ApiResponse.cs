using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CS.Basket.Api.Infrastructure.Common
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessageList { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }
    }
}
