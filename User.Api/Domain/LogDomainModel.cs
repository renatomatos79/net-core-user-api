using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Domain
{
    public class LogDomainModel : Entity
    {
        public string Module { get; set; }
        public string Content { get; set; }
        public EnumLogType LogType { get; set; }
    }
}
