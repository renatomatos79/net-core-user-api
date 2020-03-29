using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Helper;

namespace User.Api.Cache
{
    public class CacheItem<T> where T : class
    {
        public CacheItem()
        {
            this.CreatedOn = DateTimeHelper.Now();
        }

        public T Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
