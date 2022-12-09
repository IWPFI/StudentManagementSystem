using System.Net;
using System.Net.Http;

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
        }
        catch (Exception ex)
        {
            retString = ex.Message;
        }
        return retString;
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

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream myResponseStream = response.GetResponseStream();
            //StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            //retString = myStreamReader.ReadToEnd();
            //myStreamReader.Close();
            //myResponseStream.Close();

            //if (response != null)
            //{
            //    response.Close();
            //}
            //if (request != null)
            //{
            //    request.Abort();
            //}
        }
        catch (Exception ex)
        {
            retString = ex.Message;
        }
        return retString;
    }
    public static string HttpGet(string Url)
    {
        string retString;

        Url = RootUrl + Url;
        HttpClient http = new HttpClient();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
        request.Proxy = null;
        request.KeepAlive = false;
        request.Method = "GET";
        request.ContentType = "application/json; charset=UTF-8";
        request.Headers["apikey"] = apikey; //添加头
                                            //request.Headers["Authorization"] = Authorization);
        request.AutomaticDecompression = DecompressionMethods.GZip;
        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }
        }
        catch (Exception ex)
        {
            retString = ex.Message;
        }
        return retString;
    }
}

