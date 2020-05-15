using System;

namespace Framework.Entity
{
    public interface IHasCreationTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }
}
