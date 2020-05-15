using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Infrastructure
{
    public static class JsonSerializeExtension
    {
        /// <summary>
        /// DefaultSerializerSettings
        /// </summary>
        public static readonly JsonSerializerSettings DefaultSerializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };

        /// <summary>
        /// 将object对象转换为Json数据
        /// </summary>
        /// <param name="obj">object对象</param>
        /// <returns>转换后的json字符串</returns>
        public static string ToJson(this object obj)
            => obj.ToJson(false, null);

        /// <summary>
        /// 将object对象转换为Json数据
        /// </summary>
        /// <param name="obj">object对象</param>
        /// <param name="serializerSettings">序列化设置</param>
        /// <returns>转换后的json字符串</returns>
        public static string ToJson(this object obj, JsonSerializerSettings serializerSettings)
            => obj.ToJson(false, serializerSettings);

        /// <summary>
        /// 将object对象转换为Json数据
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="isConvertToSingleQuotes">是否将双引号转成单引号</param>
        public static string ToJson(this object obj, bool isConvertToSingleQuotes)
            => obj.ToJson(isConvertToSingleQuotes, null);

        /// <summary>
        /// 将object对象转换为Json数据
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="isConvertToSingleQuotes">是否将双引号转成单引号</param>
        /// <param name="settings">serializerSettings</param>
        public static string ToJson(this object obj, bool isConvertToSingleQuotes, JsonSerializerSettings settings)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            var result = JsonConvert.SerializeObject(obj, settings ?? DefaultSerializerSettings);
            if (isConvertToSingleQuotes)
            {
                result = result.Replace("\"", "'");
            }
            return result;
        }
        /// <summary>
        /// 对象序列化成JSON字符串。
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <param name="ignoreProperties">设置需要忽略的属性</param>
        /// <returns></returns>
        public static string ToJson(this object obj, params string[] ignoreProperties)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new JsonPropertyContractResolver(ignoreProperties)
            };
            return JsonConvert.SerializeObject(obj, settings);
        }
        
        /// <summary>
        /// 将Json对象转换为T对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="jsonString">json对象字符串</param>
        /// <returns>由字符串转换得到的T对象</returns>
        public static T JsonToType<T>([NotNull]this string jsonString)
            => jsonString.JsonToType<T>(null);

        /// <summary>
        /// 将Json对象转换为T对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="jsonString">json对象字符串</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns>由字符串转换得到的T对象</returns>
        public static T JsonToType<T>([NotNull]this string jsonString, JsonSerializerSettings settings)
            => jsonString.IsNullOrWhiteSpace() ? default(T) : JsonConvert.DeserializeObject<T>(jsonString, settings ?? DefaultSerializerSettings);

        /// <summary>
        /// JSON字符串序列化成集合。
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="jsonString">json对象字符串</param>
        /// <returns></returns>
        public static List<T> JsonToList<T>([NotNull]this string jsonString)
             => jsonString.JsonToList<T>(null);

        /// <summary>
        /// JSON字符串序列化成集合。
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="jsonString">json对象字符串</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public static List<T> JsonToList<T>([NotNull]this string jsonString, JsonSerializerSettings settings)
            => jsonString.IsNullOrWhiteSpace() ? null : JsonConvert.DeserializeObject<List<T>>(jsonString, settings ?? DefaultSerializerSettings);

        /// <summary>
        /// 对象转换为string，如果是基元类型直接ToString(),如果是Entity则Json序列化
        /// </summary>
        /// <param name="obj">要操作的对象</param>
        /// <returns></returns>
        public static string ToJsonOrString(this object obj)
        {
            if (null == obj)
            {
                return string.Empty;
            }
            if (obj.GetType().IsBasicType())
            {
                return Convert.ToString(obj);
            }
            return obj.ToJson();
        }

        /// <summary>
        /// 字符串数据转换为相应类型的对象，如果是基元类型则转换类型，是Entity则Json反序列化
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="jsonString">字符串</param>
        /// <returns></returns>
        public static T StringToType<T>(this string jsonString)
        {
            if (null == jsonString)
            {
                return default(T);
            }
            if (typeof(T).IsBasicType())
            {
                return jsonString.ToOrDefault<T>();
            }
            return jsonString.JsonToType<T>();
        }

        /// <summary>
        /// 字符串数据转换为相应类型的对象，如果是基元类型则转换类型，是Entity则Json反序列化
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="jsonString">字符串</param>
        /// <param name="defaultValue">defaultValue</param>
        /// <returns></returns>
        public static T StringToType<T>(this string jsonString, T defaultValue)
        {
            if (null == jsonString)
            {
                return defaultValue;
            }
            if (defaultValue.IsBasicType())
            {
                return jsonString.ToOrDefault(defaultValue);
            }
            return jsonString.JsonToType<T>();
        }
    }
    /// <summary>
    /// JSON分解器-设置。
    /// </summary>
    public class JsonPropertyContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// 需要排除的属性。
        /// </summary>
        private IEnumerable<string> _listExclude;

        public JsonPropertyContractResolver(params string[] ignoreProperties)
        {
            this._listExclude = ignoreProperties;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            //设置需要输出的属性。
            return base.CreateProperties(type, memberSerialization).ToList().FindAll(p => !_listExclude.Contains(p.PropertyName));
        }
    }
}
