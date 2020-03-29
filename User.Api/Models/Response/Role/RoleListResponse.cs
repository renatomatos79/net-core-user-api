using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Models.Response.Role
{
    public class RoleListResponse : ListResponse<RoleListResponseData>
    {

    }

    public class RoleListResponseData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
