using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Domain;
using User.Api.Helper;

namespace User.Api.Repository.App
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private string connectionStringName;
        private IConfiguration configuration;

        public BaseRepository(string connectionStringName)
        {
            this.connectionStringName = connectionStringName;
        }

        public BaseRepository(IConfiguration configuration) : this(configuration.GetConnectionStringValue("DB"))
        {
            this.configuration = configuration;
        }

        public virtual async Task<IEnumerable<T>> FindAll()
        {
            return await Task.Run(() => { return new List<T>(); });
        }

        public virtual async Task<T> Find(string id)
        {
            return await Task.Run(() => { return default(T); });
        }

        public virtual async Task<T> Save(T model)
        {
            var entity = await Find(model.Id);
            if (entity == null)
                return await Create(model);
            else
                return await Update(model);
        }

        public virtual async Task<T> Create(T model)
        {
            return await Task.Run(() => { return default(T); });
        }

        public virtual async Task<T> Update(T model)
        {
            return await Task.Run(() => { return default(T); });
        }

        public virtual async Task<T> Remove(string id)
        {
            return await Task.Run(() => { return default(T);  });
        }
    }
}
