using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Framework.Infrastructure
{
    /// <summary>
    /// MemoryCache缓存
    /// </summary>
    public class CacheContext : ICacheContext
    {
        private static ObjectCache Cache => MemoryCache.Default;

        private static IDictionaryEnumerator GetCacheToEnumerate()
        {
            return (IDictionaryEnumerator)((IEnumerable)Cache).GetEnumerator();
        }
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>缓存对象</returns>
        public override T Get<T>(string key)
        {
            var objectToReturn = Cache[key];
            if (objectToReturn != null)
            {
                if (objectToReturn is T)
                {
                    return (T)objectToReturn;
                }
                try
                {
                    return (T)Convert.ChangeType(objectToReturn, typeof(T));
                }
                catch (InvalidCastException)
                {
                    return default(T);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 设置缓存项
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="data">缓存对象</param>
        /// <param name="minutesToCache">缓存时间</param>
        public override void Set(string key, object data, CacheTimes minutesToCache)
        {
            if (data != null)
            {
                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTime.UtcNow + TimeSpan.FromMinutes((int)minutesToCache)
                };

                Cache.Add(new CacheItem(key, data), policy);
            }
        }
        /// <summary>
        /// 检查缓存是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public override bool IsSet(string key)
        {
            return Cache[key] != null;
        }
        /// <summary>
        /// 移除一个缓存项
        /// </summary>
        /// <param name="key">键</param>
        public override void Invalidate(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// 清除所有的缓存
        /// </summary>
        public override void Clear()
        {
            var keys = new List<string>();
            var enumerator = GetCacheToEnumerate();

            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }

            foreach (var t in keys)
            {
                Cache.Remove(t);
            }
        }
        /// <summary>
        /// 按条件移除缓存
        /// </summary>
        /// <param name="keyStartsWith">键起始名</param>
        public override void ClearStartsWith(string keyStartsWith)
        {
            var keys = new List<string>();
            var enumerator = GetCacheToEnumerate();

            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }

            foreach (var t in keys.Where(x => x.StartsWith(keyStartsWith)))
            {
                Cache.Remove(t);
            }
        }
        /// <summary>
        /// 批量移除缓存
        /// </summary>
        /// <param name="keysStartsWith">键起始名集合</param>
        public override void ClearStartsWith(List<string> keysStartsWith)
        {
            var keys = new List<string>();
            var enumerator = GetCacheToEnumerate();

            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }
            foreach (var keyStartsWith in keysStartsWith)
            {
                var startsWith = keyStartsWith;
                foreach (var t in keys.Where(x => x.StartsWith(startsWith)))
                {
                    Cache.Remove(t);
                }
            }
        }
    }
}
