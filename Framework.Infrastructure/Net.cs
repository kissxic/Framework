using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Web;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 网络操作。
    /// </summary>
    public class Net
    {
        #region Ip(获取Ip)
        /// <summary>
        /// 获取IP。
        /// </summary>
        public static string Ip
        {
            get
            {
                var result = string.Empty;
                if (HttpContext.Current != null)
                {
                    result = GetWebClientIp();
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = GetLanIp();
                }
                return result;
            }
        }

        /// <summary>
        /// 获取Web客户端IP。
        /// </summary>
        private static string GetWebClientIp()
        {
            //HttpContext.Current.Request.UserHostAddress
            var ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            foreach (var hostAddress in Dns.GetHostAddresses(ip))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    return hostAddress.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取局域网IP。
        /// </summary>
        private static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    return hostAddress.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取IP地址信息（源：淘宝IP库接口）。
        /// </summary>
        /// <param name="ip">IP</param>
        /// <returns></returns>
        public static string GetAddress(string ip)
        {
            string result = string.Empty;
            var url = "http://ip.taobao.com/service/getIpInfo.php?ip=" + ip;
            try
            {
                using (var client = new WebClient())
                {
                    return client.DownloadString(url).JsonToType<TaoBaoIpEnitiy>().GetAddress();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region Host(获取主机名)

        /// <summary>
        /// 获取主机名。
        /// </summary>
        public static string Host
        {
            get
            {
                return HttpContext.Current == null ? Dns.GetHostName() : GetWebClientHostName();
            }
        }

        /// <summary>
        /// 获取Web客户端主机名。
        /// </summary>
        private static string GetWebClientHostName()
        {
            if (!HttpContext.Current.Request.IsLocal)
                return string.Empty;
            var ip = GetWebClientIp();
            var result = Dns.GetHostEntry(IPAddress.Parse(ip)).HostName;
            if (result == "localhost.localdomain")
                result = Dns.GetHostName();
            return result;
        }

        #endregion

        #region 获取mac地址
        /// <summary>
        /// 返回描述本地计算机上的网络接口的对象(网络接口也称为网络适配器)。
        /// </summary>
        /// <returns></returns>
        public static NetworkInterface[] NetCardInfo()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
        }
        ///<summary>
        /// 通过NetworkInterface读取网卡Mac
        ///</summary>
        ///<returns></returns>
        public static List<string> GetMacByNetworkInterface()
        {
            List<string> macs = new List<string>();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                macs.Add(ni.GetPhysicalAddress().ToString());
            }
            return macs;
        }
        #endregion

        #region Browser(获取浏览器信息)
        /// <summary>
        /// 获取浏览器信息
        /// </summary>
        public static string Browser
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var browser = HttpContext.Current.Request.Browser;
                return string.Format("{0} {1}", browser.Browser, browser.Version);
            }
        }
        #endregion

        #region 淘宝IP地址库接口模型
        /// <summary>
        /// 淘宝IP地址库接口模型。
        /// http://ip.taobao.com/instructions.php
        /// </summary>
        internal class TaoBaoIpEnitiy
        {
            /// <summary>
            /// 响应结果。
            /// </summary>
            [JsonProperty("code")]
            public int Code { get; set; }
            /// <summary>
            ///  响应数据。
            /// </summary>
            [JsonProperty("data")]
            public IpDataEnitiy Data { get; set; }

            public string GetAddress()
            {
                if (Data == null)
                {
                    return string.Empty;
                }
                return string.Format("{0} {1} {2}", Data.Country, Data.Region, Data.City);
            }
        }

        internal class IpDataEnitiy
        {
            /// <summary>
            /// 国家
            /// </summary>
            [JsonProperty("country")]
            public string Country { get; set; }
            /// <summary>
            /// 国家ID
            /// </summary>
            [JsonProperty("country_id")]
            public string Country_id { get; set; }
            /// <summary>
            /// 地区
            /// </summary>
            [JsonProperty("area")]
            public string Area { get; set; }
            /// <summary>
            /// 地区ID
            /// </summary>
            [JsonProperty("area_id")]
            public string Area_id { get; set; }
            /// <summary>
            /// 省份
            /// </summary>
            [JsonProperty("region")]
            public string Region { get; set; }
            /// <summary>
            /// 省份ID
            /// </summary>
            [JsonProperty("region_id")]
            public string Region_id { get; set; }
            /// <summary>
            /// 城市
            /// </summary>
            [JsonProperty("city")]
            public string City { get; set; }
            /// <summary>
            /// 城市ID
            /// </summary>
            [JsonProperty("city_id")]
            public string City_id { get; set; }
            /// <summary>
            /// 地区
            /// </summary>
            [JsonProperty("county")]
            public string County { get; set; }
            /// <summary>
            /// 地区ID
            /// </summary>
            [JsonProperty("county_id")]
            public string County_id { get; set; }
            /// <summary>
            /// 运营商
            /// </summary>
            [JsonProperty("isp")]
            public string Isp { get; set; }
            /// <summary>
            /// 运营商ID
            /// </summary>
            [JsonProperty("isp_id")]
            public string Isp_id { get; set; }
            /// <summary>
            /// IP
            /// </summary>
            [JsonProperty("ip")]
            public string Ip { get; set; }
        }
        #endregion
    }
}
