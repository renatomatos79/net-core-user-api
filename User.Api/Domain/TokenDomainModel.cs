using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Domain
{
    public class TokenDomainModel : Entity
    {
        public string Name { get; set; }
        public virtual EnumTokenType TokenType { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual DateTime ExpiresOn { get; set; }
        public virtual bool IsAutoRefresh { get; set; }
        public virtual IEnumerable<RoleDomainModel> Roles { get; set; }
        
    }
}