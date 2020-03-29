using System.Collections.Generic;

namespace User.Api.Models.Response
{
    public class ListResponse<T> : BaseResponse where T : class
    {
        public ListResponse()
        {
            this.Page = new PageResponse();
        }
        public List<T> Data { get; set; }
        public PageResponse Page { get; set; }
    }
}
