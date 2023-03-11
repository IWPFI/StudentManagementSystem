using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading;

namespace StudentManagementSystem.Common;

public class APIHelp
{
    public APIHelp() { }

    public static string HttpPostHelp(string url, string data)
    {
        string retString;
        try
        {
            string paramData = data;
            url = RootUrl + url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            byte[] bytes = Encoding.UTF8.GetBytes(paramData);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("apikey", apikey);
            request.Headers.Add("Authorization", authorization);
            request.ContentLength = bytes.Length;

            using Stream myResponseStream = request.GetRequestStream();
            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using StreamReader myStreamReader = new(response.GetResponseStream(), Encoding.UTF8);

            myResponseStream.Write(bytes, 0, bytes.Length);
            retString = myStreamReader.ReadToEnd();
            int statusCode = (int)response.StatusCode;

            if (statusCode == 200 || statusCode == 201)
            {
                retString = "添加成功";
            }
            return retString;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static Task<string> HttpPost(string url, string data)
    {
        try
        {
            url = RootUrl + url;
            var client = new RestClient(url);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("apikey", apikey);
            request.AddHeader("Authorization", authorization);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return Task.FromResult(response.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            return Task.FromResult(ex.Message);
        }
    }

    public static Task<string> HttpPatch(string url, string date)
    {
        try
        {
            url = RootUrl + url;
            var client = new RestClient(url);
            var request = new RestRequest("", Method.Patch);
            request.AddHeader("apikey", apikey);
            request.AddHeader("Authorization", authorization);
            request.AddParameter("application/json", date, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }
        catch (Exception ex)
        {
            return Task.FromResult(ex.Message);
        }
    }

    public static string HttpGetHelp(string Url)
    {
        string retString = String.Empty;
        try
        {
            var url = RootUrl + Url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.Headers["apikey"] = apikey;
            request.Headers["Authorization"] = authorization;
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream myResponseStream = response.GetResponseStream();
            using StreamReader myStreamReader = new(myResponseStream, Encoding.UTF8);
            retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            if (response != null)
                response.Close();
            if (request != null)
                request.Abort();
            return retString;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public static Task<string> HttpGet(string Url)
    {
        try
        {
            Url = RootUrl + Url;
            var client = new RestClient(Url);
            var request = new RestRequest("", Method.Get);
            request.AddHeader("apikey", apikey);
            RestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }
        catch (Exception ex)
        {
            return Task.FromResult(ex.Message);
        }
    }

    /// <summary>
    /// 将api信息转换成列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Url"></param>
    /// <returns>List</returns>
    public static List<T> GetListInfo<T>(string Url)
    {
        List<T> vs = new List<T>();
        vs = JsonToList<T>(HttpGetHelp(Url));
        return vs;
    }

    /// <summary>
    /// 将api信息转换成数组
    /// </summary>
    /// <param name="Url"></param>
    /// <returns>JArray</returns>
    public static dynamic GetArrayInfo(string Url)
    {
        var vs = JsonToJArray(HttpGetHelp(Url));
        return vs;
    }

    /// <summary>
    /// 将api信息转换成对象
    /// </summary>
    /// <param name="Url"></param>
    /// <returns>Object</returns>
    public static T GetObjectInfo<T>(string Url)
    {
        var vs = JsonToObject<T>(HttpGetHelp(Url));
        return vs;
    }

    /// <summary>
    /// 将api信息转换成JTOken对象
    /// </summary>
    /// <param name="Url"></param>
    /// <returns>JToken</returns>
    public static JToken GetJTokenInfo(string Url)
    {
        var vs = JsonToJToken(HttpGetHelp(Url));
        return vs;
    }
}

