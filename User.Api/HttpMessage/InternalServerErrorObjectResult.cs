using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using User.Api.Helper;
using User.Api.Models.Response;

namespace User.Api.HttpMesssage
{
    [DefaultStatusCode(DefaultStatusCode)]
    public class InternalServerErrorObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

        public InternalServerErrorObjectResult(BaseResponse response, string message) : base(response)
        {
            StatusCode = DefaultStatusCode;
            response.AddError(DefaultStatusCode, message);
            response.EndResponseTime = DateTimeHelper.Now();
        }
    }
}
