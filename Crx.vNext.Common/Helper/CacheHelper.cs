using Crx.vNext.Common.Helper;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Crx.vNext.Common.Cache
{
    public static class CacheHelper
    {
        /// <summary>
        /// 缓存为空时执行func方法存至缓存，否则从缓存读取
        /// 使用方法： memoryCache.Get(key, () => "123", 1);
        /// </summary>
        /// <param name="func">原始数据</param>
        /// <param name="minute">数据缓存时长</param>
        public static T Get<T>(this IMemoryCache memoryCache, string key, Func<T> func, int? minute = null)
        {
            T list = memoryCache.Get<T>(key);
            if (list == null && func != null)
            {
                list = func();
                if (minute == null)
                {
                    memoryCache.Set(key, list);
                }
                else
                {
                    memoryCache.Set(key, list, TimeSpan.FromMinutes(minute.Value));
                }
            }
            return list;
        }

        /// <summary>
        /// 缓存为空时执行func方法存至缓存，否则从缓存读取
        /// 使用方法： redis.Get(key, () => "123", 1);
        /// </summary>
        /// <param name="func">原始数据</param>
        public static async Task<T> Get<T>(this IDatabase redis, string key, Func<T> func = null, int? minute = null)
        {
            T obj = default;
            if (await redis.KeyExistsAsync(key))
            {
                obj = JsonHelper.Deserialize<T>(await redis.StringGetAsync(key));
            }
            else if(func != null)
            {
                obj = func();
                await redis.StringSetAsync(key, JsonHelper.Serialize(obj), minute == null ? default(TimeSpan?) : TimeSpan.FromMinutes(minute.Value));
            }
            return obj;
        }
    }
}
