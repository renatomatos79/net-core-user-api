using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Domain;
using User.Api.Repository.App;

namespace User.Api.Repository.Role
{
    public class RoleRepository : BaseRepository<RoleDomainModel>, IRoleRepository
    {
        public RoleRepository(string connectionStringName) : base(connectionStringName)
        {
        }
        public RoleRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual async Task<IEnumerable<RoleDomainModel>> FindAll()
        {
            return await Task.FromResult<IEnumerable<RoleDomainModel>>
            (
                new List<RoleDomainModel>
                {
                    new RoleDomainModel(){ Code = "USER_CREATE", Name = "Create an user" },
                    new RoleDomainModel(){ Code = "USER_UPDATE", Name = "Update any user" },
                    new RoleDomainModel(){ Code = "USER_DELETE", Name = "Delete any user" },
                    new RoleDomainModel(){ Code = "USER_SELECT", Name = "View all users" },
                }
            );
        }
    }
}
