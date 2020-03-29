using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Domain
{
    public class Entity
    {
        public Entity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsActive = true;
        }

        public virtual string Id { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
