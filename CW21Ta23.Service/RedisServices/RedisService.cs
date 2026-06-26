using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BookStore.Service.RedisServices
{
    public class RedisService : IRedisService 
    {
        private readonly IDistributedCache _cache;

        public RedisService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<bool> ExistsAsync(string key)
        {
          var value = await _cache.GetStringAsync(key);
            if(value == null)
            {
                return false;
            }

            return true;


        }

        public async Task<T?> GetAsync<T>(string key)
        {
          var get = await _cache.GetStringAsync(key);
            if(get == null)
            {
                return default(T?);
            }

            return JsonSerializer.Deserialize<T>(get);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var options = new DistributedCacheEntryOptions();

            if (expiry.HasValue)
                options.AbsoluteExpirationRelativeToNow = expiry;

            var jsonData = JsonSerializer.Serialize(value);

            await _cache.SetStringAsync(key, jsonData, options);
        }
    }
}
