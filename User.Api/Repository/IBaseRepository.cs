using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Domain;

namespace User.Api.Repository.App
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> FindAll();
        Task<T> Find(string id);
        Task<T> Save(T model);
        Task<T> Create(T model);
        Task<T> Update(T model);
        Task<T> Remove(string id);
    }
}
