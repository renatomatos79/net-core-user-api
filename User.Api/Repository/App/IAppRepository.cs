using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Domain;

namespace User.Api.Repository.App
{
    public interface IAppRepository : IBaseRepository<AppDomainModel>
    {
        Task<AppDomainModel> FindByClientId(string clientId);
    }
}
