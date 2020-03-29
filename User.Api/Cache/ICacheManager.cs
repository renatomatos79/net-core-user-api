using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Cache
{
    public interface ICacheManager
    {
        string CreateTokenKey(string clientId, string clientSecret);
        Task<CacheItem<T>> Save<T>(string key, T value, int expirationInSeconds) where T : class;
        Task<CacheItem<T>> Save<T>(string key, T value) where T : class;
        Task<CacheItem<T>> Find<T>(string key) where T : class;
    }
}
