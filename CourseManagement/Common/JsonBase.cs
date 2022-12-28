using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
