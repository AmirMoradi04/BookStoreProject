using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Service.RedisServices
{
    public interface IRedisService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task RemoveAsync(string key);
        Task<bool> ExistsAsync(string key);
    }
}
