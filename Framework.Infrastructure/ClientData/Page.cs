using System;
using System.Collections;
using System.Collections.Generic;

namespace Framework.Infrastructure
{
    /// <summary>
    /// IPageModel
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public interface IPage<out T> : IEnumerable<T>
    {
        /// <summary>
        /// Data
        /// </summary>
        IReadOnlyList<T> Data { get; }
        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 页容量
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalCount { get; set; }
    }
    [Serializable]
    public class Page<T> : IPage<T>
    {
        public IReadOnlyList<T> Data { get; set; }

        private int _pageIndex = 1;
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                if (value > 0)
                {
                    _pageIndex = value;
                }
            }
        }

        private int _pageSize;
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value > 0)
                {
                    _pageSize = value;
                }
            }
        }

        private int _totalCount;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount
        {
            get => _totalCount;
            set
            {
                if (value > 0)
                {
                    _totalCount = value;
                }
            }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => Convert.ToInt32(Math.Ceiling(_totalCount * 1.0 / _pageSize));

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
        /// <summary>
        /// 根据索引获取记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index] => Data[index];
        /// <summary>
        /// 返回的记录数
        /// </summary>
        public int Count => Data.Count;
    }
}
