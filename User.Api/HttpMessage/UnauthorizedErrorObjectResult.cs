using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Helper;
using User.Api.Models.Response;

namespace User.Api.HttpMesssage
{
    public class UnauthorizedErrorObjectResult : BadRequestObjectResult
    {
        public UnauthorizedErrorObjectResult(BaseResponse response, string message) : base(response)
        {
            response.AddUnauthorized(message);
            response.EndResponseTime = DateTimeHelper.Now();
        }
    }    
}
