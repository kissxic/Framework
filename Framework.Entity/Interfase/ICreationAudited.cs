using System;

namespace Framework.Entity
{
    public interface ICreationAudited
    {
        /// <summary>
        /// 创建人
        /// </summary>
        string CreateUser { get; set; }
    }
}
