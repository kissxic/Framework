using System;

namespace Framework.Entity
{
    public partial class Sys_User : IFullAudited<string>
    {
        /// <summary>
        /// 主键id
        /// </summary>
         public string Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
         public string Account { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
         public string EnName { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
         public string ChName { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
         public string UserImg { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool? Gender { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 微信id
        /// </summary>
         public string WeiCharId { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 上级id
        /// </summary>
        public string BossId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
         public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
         public string ModifyUser { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
         public DateTime ModifyTime { get; set; }
    }
}
