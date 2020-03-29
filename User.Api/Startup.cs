using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using User.Api.Cache;
using User.Api.Helper;
using User.Api.Repository.App;
using User.Api.Repository.Log;
using User.Api.Repository.Role;
using User.Api.Repository.Token;

namespace User.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region .: repositories :.

            services.AddScoped<IAppRepository>(svc =>
            {
                return new AppRepository(Configuration);
            });

            services.AddScoped<IRoleRepository>(svc =>
            {
                return new RoleRepository(Configuration);
            });

            services.AddScoped<ITokenRepository>(svc =>
            {
                return new TokenRepository(Configuration);
            });

            services.AddScoped<ILogRepository>(svc =>
            {
                return new LogRepository(Configuration);
            });

            #endregion

            #region .: cache :.
            // Setup REDIS
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = Configuration.GetConnectionString("REDIS");
            //    options.InstanceName = "master";
            //});

            // Cache Strategy
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager>(svc => {
                var memoryCache = svc.GetService<IMemoryCache>();
                return new MemoryCacheManager(memoryCache);
            });
            #endregion

            #region .: jwt authentication :.

            var appSettings = Configuration.GetAppSettings();
            var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
