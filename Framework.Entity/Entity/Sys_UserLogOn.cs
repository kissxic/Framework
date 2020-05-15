using System;

namespace Framework.Entity
{
    public partial class Sys_UserLogOn
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 上一次登录时间
        /// </summary>
        public DateTime? PrevVisitTime { get; set; }
        /// <summary>
        /// 最近一次登录时间
        /// </summary>
        public DateTime? LastVisitTime { get; set; }
        /// <summary>
        /// 修改密码时间
        /// </summary>
        public DateTime? ChangePwdTime { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }
        /// <summary>
        /// 是否允许多次登录
        /// </summary>
        public bool? AllowMultiUserOnline { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool? IsOnLine { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public string AnswerQuestion { get; set; }
        public bool? CheckIPAddress { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
    }
}
