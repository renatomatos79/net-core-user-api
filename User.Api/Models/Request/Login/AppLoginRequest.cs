using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Models.Request.Login
{
    public class AppLoginRequest : BaseRequest<AppLoginRequestData>
    {
    }

    public class AppLoginRequestData
    { 
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int ExpiresInSeconds { get; set; }
        public bool IsAutoRefresh { get; set; }
    }
}
