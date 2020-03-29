using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace User.Api.Models.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            this.RequestToken = Guid.NewGuid().ToString();
            this.StartRequestTime = Helper.DateTimeHelper.Now();
            this.Erros = new List<ErrorResponse>();
        }
        public string RequestToken { get; set; }
        public DateTime StartRequestTime { get; set; }
        public DateTime EndResponseTime { get; set; }
        public IList<ErrorResponse> Erros { get; set; }

        public ErrorResponse AddError(int code, string message)
        {
            this.EndResponseTime = Helper.DateTimeHelper.Now();
            var errorResponse = new ErrorResponse { Code = code, Message = message };
            this.Erros.Add(errorResponse);
            return errorResponse;
        }

        public ErrorResponse AddInternalServerError(Exception ex)
        {
            return AddError(HttpStatusCode.InternalServerError.GetHashCode(), ex.ToString());
        }

        public ErrorResponse AddBadRequest(string message)
        {
            return AddError(HttpStatusCode.BadRequest.GetHashCode(), message);
        }

        public ErrorResponse AddUnauthorized(string message)
        {
            return AddError(HttpStatusCode.Forbidden.GetHashCode(), message);
        }

        public ErrorResponse AddNotFound(string message)
        {
            return AddError(HttpStatusCode.NotFound.GetHashCode(), message);
        }
    }
}
