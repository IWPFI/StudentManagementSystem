using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Text;
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
    /// <returns></returns>
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
    /// <returns></returns>
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
}
