using System.Collections.Generic;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public abstract class ICacheContext
    {
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>缓存对象</returns>
        public abstract T Get<T>(string key);

        /// <summary>
        /// 设置缓存项
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="data">缓存对象</param>
        /// <param name="minutesToCache">缓存时间</param>
        public abstract void Set(string key, object data, CacheTimes minutesToCache);

        /// <summary>
        /// 检查缓存是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public abstract bool IsSet(string key);
        /// <summary>
        /// 移除一个缓存项
        /// </summary>
        /// <param name="key">键</param>
        public abstract void Invalidate(string key);
        /// <summary>
        /// 清除所有的缓存
        /// </summary>
        public abstract void Clear();
        /// <summary>
        /// 按条件移除缓存
        /// </summary>
        /// <param name="keyStartsWith">键起始名</param>
        public abstract void ClearStartsWith(string keyStartsWith);
        /// <summary>
        /// 批量移除缓存
        /// </summary>
        /// <param name="keysStartsWith">键起始名集合</param>
        public abstract void ClearStartsWith(List<string> keysStartsWith);
    }
}
