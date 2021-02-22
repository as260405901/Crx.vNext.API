using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crx.vNext.Common.Cache
{
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache _cache;

        public MemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key, Func<T> func)
        {
            T list = _cache.Get<T>(key);
            if (list == null)
            {
                list = func();
                _cache.Set(key, list);
            }
            return list;
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public void Set(string key, object value)
        {
            _cache.Set(key, value);
        }
    }
}
