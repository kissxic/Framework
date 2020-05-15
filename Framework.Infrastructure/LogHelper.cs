using NLog;
using NLog.Config;
using System;

namespace Framework.Infrastructure
{
    /// <summary>
    /// NLog日志框架辅助类。
    /// </summary>
    public static class LogHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 注册日志配置文件。
        /// </summary>
        public static void RegisterConfig()
        {
            string configPath = ApplicationHelper.AppRoot + @"\Configs\NLog.config";
            LogManager.Configuration = new XmlLoggingConfiguration(configPath);
        }

        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="operation">动作</param>
        /// <param name="message">消息</param>
        /// <param name="account">操作者</param>
        /// <param name="realName">真实姓名</param>
        public static void Write(Level level, string operation, string message, string account, string realName)
        {
            LogEventInfo logEvent = new LogEventInfo
            {
                Message = message
            };
            switch (level)
            {
                case Level.Trace:
                    logEvent.Level = LogLevel.Trace;
                    break;
                case Level.Debug:
                    logEvent.Level = LogLevel.Debug;
                    break;
                case Level.Info:
                    logEvent.Level = LogLevel.Info;
                    break;
                case Level.Warn:
                    logEvent.Level = LogLevel.Warn;
                    break;
                case Level.Error:
                    logEvent.Level = LogLevel.Error;
                    break;
                case Level.Fatal:
                    logEvent.Level = LogLevel.Fatal;
                    break;
            }
            logEvent.Properties["Id"] = Guid.NewGuid().ToString();
            logEvent.Properties["Account"] = account;
            logEvent.Properties["RealName"] = realName;
            logEvent.Properties["Operation"] = operation;
            logEvent.Properties["IP"] = Net.Ip;
            logEvent.Properties["IPAddress"] = Net.GetAddress(Net.Ip);
            logEvent.Properties["Browser"] = Net.Browser;
            logger.Log(logEvent);
        }

        /// <summary>
        /// 记录信息，用于普通输出。
        /// </summary>
        /// <param name="message"></param>
        public static void Trace(string message)
        {
            logger.Trace(message);
        }

        /// <summary>
        /// 记录信息，用于调试程序。
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// 信息类型的消息。
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// 警告信息。
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// 错误信息。
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// 致命异常信息。一般来讲，发生致命异常之后程序将无法继续执行。
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(string message)
        {
            logger.Fatal(message);
        }
    }
}
