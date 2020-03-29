using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Helper;
using User.Api.Models.Response;

namespace User.Api.HttpMesssage
{
    public class BadRequestErrorObjectResult : BadRequestObjectResult
    {
        public BadRequestErrorObjectResult(BaseResponse response, string message) : base(response)
        {
            response.AddBadRequest(message);
            response.EndResponseTime = DateTimeHelper.Now();
        }
    }    
}
