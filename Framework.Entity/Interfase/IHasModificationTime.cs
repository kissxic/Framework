using System;

namespace Framework.Entity
{
    public interface IHasModificationTime
    {
        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime ModifyTime { get; set; }
    }
}
