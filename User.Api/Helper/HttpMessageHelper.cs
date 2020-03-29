using Microsoft.AspNetCore.Mvc;
using User.Api.Cache;
using User.Api.HttpMessage;
using User.Api.HttpMesssage;
using User.Api.Models.Response;

namespace User.Api.Helper
{
    public static class HttpMessageHelper
    {
        public static BadRequestErrorObjectResult ToBadRequest(this BaseResponse response, string message)
        {
            return new BadRequestErrorObjectResult(response, message);
        }

        public static UnauthorizedErrorObjectResult ToUnauthorized(this BaseResponse response, string message)
        {
            return new UnauthorizedErrorObjectResult(response, message);
        }

        public static InternalServerErrorObjectResult ToInternalServerError(this BaseResponse response, string message)
        {
            return new InternalServerErrorObjectResult(response, message);
        }

        public static NotFoundErrorObjectResult ToNotFound(this BaseResponse response, string message)
        {
            return new NotFoundErrorObjectResult(response, message);
        }

        public static SuccessObjectResult ToOk(this BaseResponse response)
        {
            return new SuccessObjectResult(response);
        }

        public static SuccessObjectResult ToOk<T>(this CacheItem<T> cache, Microsoft.AspNetCore.Http.HttpResponse context) where T : BaseResponse
        {
            context.Headers.Add("X-CUSTOM-CACHE", cache.ExpiresOn.ToDateTimeString());
            return new SuccessObjectResult(cache.Content);
        }
    }
}
