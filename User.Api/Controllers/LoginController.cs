using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using User.Api.Cache;
using User.Api.Domain;
using User.Api.Helper;
using User.Api.Models.Request.Login;
using User.Api.Models.Response.Login;
using User.Api.Repository.App;
using User.Api.Repository.Log;
using User.Api.Repository.Token;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAppRepository appRepository;
        private readonly ITokenRepository tokenRepository;
        private readonly ILogRepository logRepository;
        private readonly ICacheManager cacheManager;
        private readonly IConfiguration configuration;

        public LoginController(IAppRepository appRepository, 
            ITokenRepository tokenRepository, 
            ILogRepository logRepository,
            ICacheManager memoryCache, 
            IConfiguration configuration) 
        {
            this.appRepository = appRepository;
            this.tokenRepository = tokenRepository;
            this.logRepository = logRepository;
            this.cacheManager = memoryCache;
            this.configuration = configuration;
        }

        [HttpPost("application")]
        public async Task<IActionResult> Authenticate([FromBody] AppLoginRequest request)
        {
            var result = new LoginResponse();
            try
            {
                #region .: request validation :.

                if (request == null || request.Data == null)
                {
                    return result.ToBadRequest("Request and Request.Data can not be null!");
                }
                if (string.IsNullOrEmpty(request.Data.ClientId))
                {
                    return result.ToBadRequest("ClientId can not be null");
                }
                if (string.IsNullOrEmpty(request.Data.ClientSecret))
                {
                    return result.ToBadRequest("ClientSecret can not be null");
                }
                if (request.Data.ExpiresInSeconds <= 0)
                {
                    return result.ToBadRequest("ExpiresInSeconds is not valid");
                }

                var cachedToken = cacheManager.CreateTokenKey(request.Data.ClientId, request.Data.ClientSecret);
                var cache = await cacheManager.Find<LoginResponse>(cachedToken);
                if (cache != null)
                {
                    return cache.ToOk(this.Response);
                }

                var app = await appRepository.FindByClientId(request.Data.ClientId);
                if (app == null)
                {
                    return result.ToNotFound($"App {request.Data.ClientId} not found!");
                }
                if (!app.ClientSecret.Equals(request.Data.ClientSecret))
                {
                    return result.ToNotFound($"App {request.Data.ClientId} not found!");
                }

                #endregion

                var token = new TokenDomainModel
                {
                    Name = app.Name,
                    CreatedOn = DateTimeHelper.Now(),
                    ExpiresOn = DateTimeHelper.Now().AddSeconds(request.Data.ExpiresInSeconds),
                    IsAutoRefresh = request.Data.IsAutoRefresh,
                    TokenType = EnumTokenType.App,
                    Roles = app.Roles
                };

                await tokenRepository.Create(token);

                var jwtSecret = configuration.GetAppSettings().JwtSecret;
                var jwtContent = TokenHelper.CreateStringSecurityToken(token, jwtSecret);

                result.Data = new LoginResponseData
                {
                    Token = token.Id,
                    JwtContent = jwtContent,
                    Name = app.Name,
                    ExpiresOn = token.ExpiresOn
                };

                await cacheManager.Save(cachedToken, result);

                return result.ToOk();
            }
            catch (UnauthorizedAccessException ex)
            {
                await logRepository.AddError("Login.Authenticate", ex);
                return result.ToUnauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                await logRepository.AddError("Login.Authenticate", ex);
                return result.ToInternalServerError(ex.Message);
            }
        }        
    }
}