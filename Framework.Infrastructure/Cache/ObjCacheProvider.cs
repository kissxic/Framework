using System.Collections.Generic;

namespace Framework.Infrastructure
{
    /// <summary>
    ///  缓存工厂实现
    /// </summary>
    public class ObjCacheProvider : CacheProvider
    {
        public ObjCacheProvider()
        {
            //默认使用MemoryCache作为缓存容器
            SetCacheInstance(new CacheContext());
        }
        /// <summary>
        /// 创建缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="val">缓存值</param>
        /// <param name="expire">缓存时间</param>
        /// <returns></returns>
        public void Set(string key, object val, CacheTimes expire) => CacheContext.Set(key, val, expire);

        /// <summary>
        /// 根据缓存键获取缓存
        /// </summary>
        /// <param name="key">The key.</param>
        public T GetCache<T>(string key) => CacheContext.Get<T>(key);
        /// <summary>
        /// 检查缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsSet(string key) => CacheContext.IsSet(key);
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public void Remove(string key) => CacheContext.Invalidate(key);
        /// <summary>
        /// 清理所有缓存
        /// </summary>
        public void Clear() => CacheContext.Clear();
        /// <summary>
        /// 按条件移除缓存
        /// </summary>
        /// <param name="keyStartsWith">键起始名</param>
        public void ClearStartsWith(string keyStartsWith) => CacheContext.ClearStartsWith(keyStartsWith);
        /// <summary>
        /// 批量移除缓存
        /// </summary>
        /// <param name="keysStartsWith">键起始名集合</param>
        public void ClearStartsWith(List<string> keysStartsWith) => CacheContext.ClearStartsWith(keysStartsWith);
    }
}
