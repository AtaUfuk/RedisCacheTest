using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RedisCacheTest.RedisLib.Extensions
{
	public static class DistributedCacheExtensions
    {
        public static async Task SetorUpdateRecordAsync<T>(this IDistributedCache cache, string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = unusedExpireTime
            };
            if(IsExistRecord(cache,recordId))
            {
                cache.Remove(recordId);
            }
            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache,string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);
            if(object.Equals(jsonData,null))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public static bool IsExistRecord(IDistributedCache cache,string recordId)
        {
            var jsonData = cache.GetString(recordId);
            bool result = false;
            if(!object.Equals(jsonData,null))
            {
                result = true;
            }
            return result;
        }

        public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = unusedExpireTime
            };
            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        //public static async Remove
    }
}
