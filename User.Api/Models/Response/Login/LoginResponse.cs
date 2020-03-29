using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Models.Response.Login
{
    public class LoginResponse : SingleResponse<LoginResponseData>
    {
    }

    public class LoginResponseData
    { 
        public string Token { get; set; }
        public string JwtContent { get; set; }
        public string Name { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
