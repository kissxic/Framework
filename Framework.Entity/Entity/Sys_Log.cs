using System;

namespace Framework.Entity
{
    /// <summary>
    /// 错误日志表
    /// </summary>
    public  class Sys_Log:IBaseEntity<string>, IHasCreationTime
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public string LogLevel { get; set; }
        /// <summary>
        /// 动作
        /// </summary>
        public string Operation { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// IP地址信息
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 浏览器
        /// </summary>
        public string Browser { get; set; }
        /// <summary>
        /// 错误堆栈
        /// </summary>
        public string StackTrace { get; set; }
    }
}
