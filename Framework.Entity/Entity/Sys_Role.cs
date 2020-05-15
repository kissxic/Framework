using System;

namespace Framework.Entity
{
    public partial class Sys_Role:IFullAudited<string>
    {
        /// <summary>
        /// 主键
        /// </summary>
         public string Id { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string EnCode { get; set; }
        /// <summary>
        /// 分类：1-角色2-岗位
        /// </summary>
        public short? Type { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool? AllowEdit { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
         public bool IsDeleted { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
         public bool IsEnabled { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? SortCode { get; set; }
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
