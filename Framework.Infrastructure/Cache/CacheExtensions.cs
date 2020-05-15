using System;

namespace Framework.Infrastructure
{
    public static class CacheExtensions
    {
        /// <summary>
        /// 获取数据并设置缓存 默认缓存时间120分钟
        /// 当缓存不存在是执行 acquire方法获取数据并设置缓存。
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="cacheManager">缓存提供程序</param>
        /// <param name="key">键值</param>
        /// <param name="acquire">缓存为空时获取缓存的方法</param>
        /// <returns></returns>
        public static T Get<T>(this ObjCacheProvider cacheManager, string key, Func<T> acquire) => Get(cacheManager, key, CacheTimes.TwoHours, acquire);
        /// <summary>
        /// 获取数据并设置缓存。
        /// 当缓存不存在是执行 acquire方法获取数据并设置缓存。
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="cacheManager">缓存提供程序</param>
        /// <param name="key">键值</param>
        /// <param name="cacheTime">缓存时间</param>
        /// <param name="acquire">缓存为空时获取缓存的方法</param>
        /// <returns></returns>
        public static T Get<T>(this ObjCacheProvider cacheManager, string key, CacheTimes cacheTime, Func<T> acquire)
        {
            if (cacheManager.IsSet(key))
            {
                var value = cacheManager.GetCache<T>(key);
                if (value != null)
                    return value;
            }
            var result = acquire();
            if (result != null)
            {
                cacheManager.Set(key, result, cacheTime);
            }
            return result;
        }
    }
}
