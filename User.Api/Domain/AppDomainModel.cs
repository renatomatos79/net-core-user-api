using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Domain
{
    public class AppDomainModel : Entity
    {
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int WrongPassword { get; set; }
        public bool IsBlocked { get; set; }
        public IEnumerable<RoleDomainModel> Roles { get; internal set; }
    }
}
