using System;
using System.Web;

namespace Framework.Infrastructure
{
    public static class WebHelper
    {
        #region Session操作
        /// <summary>
        /// 设置Session。
        /// </summary>
        /// <typeparam name="T">键值类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        public static void SetSession<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            HttpContext.Current.Session[key] = value;
        }
        /// <summary>
        /// 设置Session。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="value">过期时间(分钟)</param>
        public static void SetSession<T>(string key, T value, int expires)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            HttpContext.Current.Session[key] = value;
            HttpContext.Current.Session.Timeout = expires;
        }

        /// <summary>
        /// 设置Session。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        public static void SetSession(string key, string value)
        {
            SetSession<string>(key, value);
        }
        /// <summary>
        /// 设置Session。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="value">过期时间(分钟)</param>
        public static void SetSession(string key, string value, int expires)
        {
            SetSession<string>(key, value, expires);
        }
        /// <summary>
        /// 读取Session。
        /// </summary>
        /// <param name="key">键名</param>        
        public static string GetSession(string key)
        {
            return GetSession<string>(key);
        }
        /// <summary>
        /// 读取Session。
        /// </summary>
        /// <typeparam name="T">键值类型</typeparam>
        /// <param name="key">键名</param>
        public static T GetSession<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }
            return (T)HttpContext.Current.Session[key];
        }
        /// <summary>
        /// 删除Session。
        /// </summary>
        /// <param name="key">键名</param>
        public static void RemoveSession(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            HttpContext.Current.Session.Contents.Remove(key);
        }
        #endregion

        #region Cookie操作
        /// <summary>
        /// 设置Cookie。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetCookie(string key, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie == null)
            {
                cookie = new HttpCookie(key);
            }
            cookie.Value = value;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 设置Cookie。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="value">过期时间(分钟)</param>
        public static void SetCookie(string key, string value, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie == null)
            {
                cookie = new HttpCookie(key);
            }
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 读取Cookie。
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[key] != null)
            {
                return HttpContext.Current.Request.Cookies[key].Value.ToString();
            }
            return "";
        }
        /// <summary>
        /// 删除Cookie。
        /// </summary>
        /// <param name="key">Cookie对象名称</param>
        public static void RemoveCookie(string key)
        {
            HttpCookie objCookie = new HttpCookie(key.Trim());
            objCookie.Expires = DateTime.Now.AddYears(-5);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        #endregion

    }
}
