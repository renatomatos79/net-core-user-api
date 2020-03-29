using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Helper;

namespace User.Api.Cache
{
    public class MemoryCacheManager : ICacheManager
    {
        public const string ROLES_ALL = "ALL_ROLES";
        public const int SIXTY_SECONDS = 60;
        private IMemoryCache memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public string CreateTokenKey(string clientId, string clientSecret)
        {
            return $"TOKEN_{clientId}_{clientSecret}";
        }

        public async Task<CacheItem<T>> Save<T>(string key, T value, int expirationInSeconds) where T : class
        {
            return await Task.Run(() => {
                var expiresOn = DateTimeHelper.Now().AddSeconds(expirationInSeconds);
                var options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = expiresOn;
                var item = new CacheItem<T> { ExpiresOn = expiresOn, Content = value };
                memoryCache.Set(key, item, options);
                return item;
            });
        }

        public async Task<CacheItem<T>> Save<T>(string key, T value) where T : class
        {
            return await Save(key, value, SIXTY_SECONDS);
        }

        public async Task<CacheItem<T>> Find<T>(string key) where T : class
        {
            return await Task.Run(() => {
                CacheItem<T> temp;
                if (memoryCache.TryGetValue(key, out temp))
                {
                    return temp;
                }
                return null;
            });
        }

        public void Remove(string key)
        {
            memoryCache.Remove(key);
        }
    }


}
