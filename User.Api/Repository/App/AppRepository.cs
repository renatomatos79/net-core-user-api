using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Domain;

namespace User.Api.Repository.App
{
    public class AppRepository : BaseRepository<AppDomainModel>, IAppRepository
    {
        private readonly List<AppDomainModel> apps = new List<AppDomainModel>
        {
            new AppDomainModel
            { 
                ClientId = "1234", ClientSecret = "123456", Name = "Web", Roles = new List<RoleDomainModel> 
                {
                    new RoleDomainModel(){ Code = "USER_CREATE", Name = "Create an user" },
                    new RoleDomainModel(){ Code = "USER_UPDATE", Name = "Update any user" },
                    new RoleDomainModel(){ Code = "USER_DELETE", Name = "Delete any user" },
                    new RoleDomainModel(){ Code = "USER_SELECT", Name = "View all users" },
                }
            },
            new AppDomainModel
            {
                ClientId = "4758", ClientSecret = "123456", Name = "Mobile", Roles = new List<RoleDomainModel>
                {
                    new RoleDomainModel(){ Code = "USER_SELECT", Name = "View all users" }
                }
            }
        };

        public AppRepository(string connectionStringName) : base(connectionStringName) { }
        public AppRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<AppDomainModel> FindByClientId(string clientId)
        {
            return await Task.Run(() => { return apps.Where(f => f.ClientId == clientId).FirstOrDefault(); });
        }        
    }
}
