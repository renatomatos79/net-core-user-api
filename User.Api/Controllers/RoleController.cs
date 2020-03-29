using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using User.Api.Cache;
using User.Api.Helper;
using User.Api.Models.Response.Role;
using User.Api.Repository.Log;
using User.Api.Repository.Role;

namespace User.Api.Controllers
{
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;
        private readonly ICacheManager cacheManager;
        private readonly ILogRepository logRepository;

        public RoleController(IRoleRepository roleRepository,
            ICacheManager cacheManager,
            ILogRepository logRepository) 
        {
            this.roleRepository = roleRepository;
            this.cacheManager = cacheManager;
            this.logRepository = logRepository;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> FindAll()
        {
            var result = new RoleListResponse();
            try
            {
                var cache = await cacheManager.Find<RoleListResponse>(MemoryCacheManager.ROLES_ALL);
                if (cache != null)
                {
                    return cache.ToOk(this.Response);
                }

                var roles = await roleRepository.FindAll();
                result.Data = roles.ToList().Select(s => new RoleListResponseData { Code = s.Code, Name = s.Name }).ToList();

                await cacheManager.Save(MemoryCacheManager.ROLES_ALL, result);

                return result.ToOk();
            }
            catch (ArgumentException ex)
            {
                return result.ToBadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                await logRepository.AddError("RoleController.FindAll", ex);
                return result.ToUnauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                await logRepository.AddError("RoleController.FindAll", ex);
                return result.ToInternalServerError(ex.Message);
            }
        }        
    }
}