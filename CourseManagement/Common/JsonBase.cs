using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Windows.Markup;

namespace StudentManagementSystem.Common;

public class JsonBase
{
    /// <summary>
    /// 将序列化的json字符串内容写入Json文件，并且保存
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="jsonConents">Json内容</param>
    public static void WriteJsonFile(string path, string jsonConents)
    {
        try
        {
            File.WriteAllText(path, jsonConents, Encoding.UTF8);
        }
        catch { }
    }

    /// <summary>
    /// 获取到本地的Json文件并且解析返回对应的json字符串
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>string</returns>
    private static string JsonToString(string filePath)
    {
        try
        {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>List</returns>
    public static List<T> JsonFileToList<T>(string filePath)
    {
        try
        {
            //File.ReadAllText：打开一个文本文件，将文件中的所有文本读取到一个字符串中，然后关闭此文件
            //https://learn.microsoft.com/zh-cn/dotnet/api/system.io.file.readalltext?f1url=%3FappId%253&view=net-7.0
            string jsonStr = File.ReadAllText(filePath);

            //反序列化
            var list = new List<T>(JsonConvert.DeserializeObject<List<T>>(jsonStr));

            return list;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 对象转换为Json字符串
    /// </summary>
    /// <param name="obj">需要转换的对象</param>
    /// <returns>string</returns>
    public static string ObjectToJson(object obj)
    {
        try
        {
            var strJson = "";
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            //序列化
            using (MemoryStream stream = new())
            {
                json.WriteObject(stream, obj);
                strJson = Encoding.UTF8.GetString(stream.ToArray());
            }
            return strJson;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 对象转换为Json字符串[去除null值]
    /// </summary>
    /// <param name="obj">需要转换的对象</param>
    /// <param name="T">实体类</param>
    /// <returns>string</returns>
    public static string ObjectToJsonNonempty<T>(T obj)
    {
        try
        {
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string messageOut = JsonConvert.SerializeObject(obj, Formatting.Indented, jsonSetting);
            return messageOut;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Json字符串转List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str"></param>
    /// <returns>List</returns>
    public static List<T> JsonToList<T>(string str)
    {
        try
        {
            //反序列化
            var list = new List<T>(JsonConvert.DeserializeObject<List<T>>(str));

            return list;
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// Json字符串转JArray[数组]
    /// </summary>
    /// <param name="str"></param>
    /// <returns>JArray</returns>
    public static dynamic JsonToJArray(string str)
    {
        try
        {
            JArray data = (JArray)JsonConvert.DeserializeObject(str);
            //dynamic data1 = JsonConvert.DeserializeObject(str);
            return data;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Json字符串转Object
    /// </summary>
    /// <param name="str"></param>
    /// <returns>JArray</returns>
    public static T JsonToObject<T>(string str)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        catch
        {
            return default(T);
        }
    }

    /// <summary>
    /// Json字符串转JToken
    /// </summary>
    /// <param name="jsonStr">Json字符串</param>
    /// <returns>JToken</returns>
    public static JToken JsonToJToken(string jsonStr)
    {
        try
        {
            JObject jobj = JObject.Parse(jsonStr);
            JToken result = jobj as JToken;
            return result;
        }
        catch
        {
            return null;
        }
    }
}

/// <summary>
/// Json操作类
/// </summary>
public class JsonOperate
{
    /// <summary>
    /// 遍历所有节点，替换其中某个节点的值
    /// </summary>
    /// <param name="jobj">json数据</param>
    /// <param name="nodeName">节点名</param>
    /// <param name="value">新值</param>
    private static void JsonSetChildNodes(ref JToken jobj, string nodeName, string value)
    {
        try
        {
            JToken result = jobj as JToken;//转换为JToken
            JToken result2 = result.DeepClone();//复制一个返回值，由于遍历的时候JToken的修改回终止遍历，因此需要复制一个新的返回json

            //遍历
            var reader = result.CreateReader();
            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    if (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
                    {
                        Regex reg = new Regex(@"" + nodeName + "$");
                        //SelectToken(Path)方法可查找某路径下的节点
                        if (reg.IsMatch(reader.Path))
                        {
                            result2.SelectToken(reader.Path).Replace(value);
                        }
                    }
                }
            }
            jobj = result2;
        }
        catch { }
    }

    /// <summary>
    /// 获取相应子节点的值
    /// </summary>
    /// <param name="json">json数据</param>
    /// <param name="ReName">节点名称</param>
    /// <returns></returns>
    private static string JsonSeleteNode(JToken json, string ReName)
    {
        try
        {
            string result = string.Empty;
            var node = json.SelectToken("$.." + ReName);
            if (node != null)
            {
                //判断节点类型
                if (node.Type == JTokenType.String || node.Type == JTokenType.Integer || node.Type == JTokenType.Float)
                {
                    //返回string值
                    result = node.Value<object>().ToString();
                }
            }
            return result;
        }
        catch
        {
            return null;
        }
    }
}

/// <summary>
/// Json辅助类（此类依赖Newtonsoft.Json库）
/// </summary>
public static class JsonHelper
{
    private static MethodInfo SerializeObjectMethodInfo;
    private static MethodInfo DeserializeObjectMethodInfo;
    private static MethodInfo JsonParseMethodInfo;
    private static MethodInfo JsonTostringMethodInfo;
    private static Type IsoDateTimeConverterType;
    private static Type JsonConverterType;

    static JsonHelper()
    {
        try
        {
            LoadAssembly("Newtonsoft.Json.dll");
        }
        catch
        {
            Console.WriteLine("未加载【Newtonsoft.Json.dll】");
        }
    }

    /// <summary>
    /// 加载Newtonsoft.Json.dll
    /// </summary>
    /// <param name="assemblyPath"></param>
    /// <returns></returns>
    public static bool LoadAssembly(string assemblyPath)
    {
        var assembly = Assembly.LoadFrom(assemblyPath);
        if (assembly == null) return false;
        var type = assembly.GetType("Newtonsoft.Json.JsonConvert");
        if (type == null) return false;
        JsonConverterType = assembly.GetType("Newtonsoft.Json.JsonConverter");
        var jsonFormattingType = assembly.GetType("Newtonsoft.Json.Formatting");
        SerializeObjectMethodInfo = type.GetMethod("SerializeObject", new[] { typeof(object), JsonConverterType.MakeArrayType() });
        IsoDateTimeConverterType = assembly.GetType("Newtonsoft.Json.Converters.IsoDateTimeConverter");

        DeserializeObjectMethodInfo = type.GetMethod("DeserializeObject", new[] { typeof(string), JsonConverterType.MakeArrayType() });

        type = assembly.GetType("Newtonsoft.Json.Linq.JToken");
        if (type == null) return false;
        JsonParseMethodInfo = type.GetMethod("Parse", new[] { typeof(string) });
        JsonTostringMethodInfo = type.GetMethod("ToString", new[] { jsonFormattingType, JsonConverterType.MakeArrayType() });

        return true;
    }

    /// <summary>
    /// 将对象序列化为JSON格式
    /// </summary>
    /// <param name="o">对象</param>
    /// <param name="dateTimeFormat">时间格式</param>
    /// <returns>json字符串</returns>
    public static string SerializeObject(object o, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
    {
        return SerializeObjectMethodInfo.Invoke(null, new[] { o, GetIsoDateTimeConverterArray(dateTimeFormat) }) as string;
    }

    /// <summary>
    /// 将对象序列化为格式化的JSON
    /// </summary>
    /// <param name="o"></param>
    /// <param name="dateTimeFormat"></param>
    /// <returns></returns>
    public static string SerializeObjectToFormatJson(object o, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
    {
        return FormatJson(SerializeObject(o, dateTimeFormat));
    }

    /// <summary>
    /// 反序列化为对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
    /// <param name="dateTimeFormat">时间格式</param>
    /// <returns>对象实体</returns>
    public static T DeserializeToObject<T>(string json, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss") where T : class
    {
        return DeserializeObjectMethodInfo.MakeGenericMethod(typeof(T))
            .Invoke(null, new object[] { json, GetIsoDateTimeConverterArray(dateTimeFormat) }) as T;
    }

    /// <summary>
    /// 从文件读取并反序列化为对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <param name="dateTimeFormat"></param>
    /// <returns></returns>
    public static T LoadFromFileToObject<T>(string filePath, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss") where T : class
    {
        var data = File.ReadAllText(filePath);
        return DeserializeToObject<T>(data, dateTimeFormat);
    }

    /// <summary>
    /// 反序列化为对象集合
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
    /// <param name="dateTimeFormat">时间格式</param>
    /// <returns>对象实体集合</returns>
    public static List<T> DeserializeToList<T>(string json, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss") where T : class
    {
        return DeserializeObjectMethodInfo.MakeGenericMethod(typeof(List<T>)).Invoke(null, new object[] { json, GetIsoDateTimeConverterArray(dateTimeFormat) }) as List<T>;
    }

    /// <summary>
    /// 从文件读取并反序列化为对象集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <param name="dateTimeFormat"></param>
    /// <returns></returns>
    public static List<T> LoadFromFileToList<T>(string filePath, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss") where T : class
    {
        var data = File.ReadAllText(filePath);
        return DeserializeToList<T>(data, dateTimeFormat);
    }

    /// <summary>
    /// 格式化Json
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static string FormatJson(string json)
    {
        var jt = JsonParseMethodInfo.Invoke(null, new object[] { json });
        return JsonTostringMethodInfo.Invoke(jt, new object[] { 1, null }) as string;
    }

    /// <summary>
    /// 压缩Json
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static string UnFormatJson(string json)
    {
        var jt = JsonParseMethodInfo.Invoke(null, new object[] { json });
        return JsonTostringMethodInfo.Invoke(jt, new object[] { 0, null }) as string;
    }

    private static Array GetIsoDateTimeConverterArray(string dateTimeFormat)
    {
        if (JsonConverterType == null)
            throw new Exception("未加载【Newtonsoft.Json.dll】,请确保添加了该动态库");

        dynamic instance = Activator.CreateInstance(IsoDateTimeConverterType);
        instance.DateTimeFormat = dateTimeFormat;

        var convertArray = Array.CreateInstance(JsonConverterType, 1);
        convertArray.SetValue(instance, 0);
        return convertArray;
    }
}