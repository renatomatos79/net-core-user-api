using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Models.Response
{
    public class PageResponse
    {
        public int RequestedPage { get; set; }
        public long Records { get; set; }
        public int PageSize { get; set; }
        public int Pages { get; set; }
    }
}
