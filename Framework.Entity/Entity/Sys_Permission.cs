using System;

namespace Framework.Entity
{
    public partial class Sys_Permission:IFullAudited<string>
    {
        /// <summary>
        /// 主键
        /// </summary>
         public string Id { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
         public string ParentId { get; set; }
        /// <summary>
        /// 层次
        /// </summary>
         public int? Layer { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 事件
        /// </summary>
        public string JsEvent { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 模块类型：1-菜单 2-按钮
        /// </summary>
        public int? Type { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        public bool? IsPublic { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 允许编辑
        /// </summary>
        public bool? IsEdit { get; set; }
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
