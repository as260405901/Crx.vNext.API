using System;
using System.Collections.Generic;
using System.Text;

namespace Crx.vNext.Common.Cache
{
    public interface ICache
    {
        T Get<T>(string key, Func<T> func);

        T Get<T>(string key);

        void Set(string key, object value);
    }
}
