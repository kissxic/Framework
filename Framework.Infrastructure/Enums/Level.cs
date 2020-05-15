using System.ComponentModel;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 错误级别
    /// </summary>
    public enum Level
    {
        [Description("普通输出")]
        Trace,
        [Description("一般调试")]
        Debug,
        [Description("普通消息")]
        Info,
        [Description("警告信息")]
        Warn,
        [Description("一般错误")]
        Error,
        [Description("致命错误")]
        Fatal
    }
}
