using System;

namespace Framework.Entity
{
    public interface IModificationAudited
    {
        /// <summary>
        /// 修改人
        /// </summary>
        string ModifyUser { get; set; }
    }
}
