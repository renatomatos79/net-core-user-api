using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Domain;
using User.Api.Repository.App;

namespace User.Api.Repository.Token
{
    public interface ITokenRepository : IBaseRepository<TokenDomainModel>
    {
    }
}
