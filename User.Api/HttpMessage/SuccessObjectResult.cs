using Microsoft.AspNetCore.Mvc;
using User.Api.Helper;
using User.Api.Models.Response;

namespace User.Api.HttpMessage
{
    public class SuccessObjectResult : OkObjectResult
    {
        public SuccessObjectResult(BaseResponse response) : base(response)
        {
            response.EndResponseTime = DateTimeHelper.Now();
        }
    }
}
