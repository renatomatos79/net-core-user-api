using Microsoft.Extensions.Configuration;
using User.Api.Domain;
using User.Api.Repository.App;

namespace User.Api.Repository.Token
{
    public class TokenRepository : BaseRepository<TokenDomainModel>, ITokenRepository
    {
        public TokenRepository(string connectionStringName) : base(connectionStringName) { }
        public TokenRepository(IConfiguration configuration) : base(configuration) { }
    }
}
