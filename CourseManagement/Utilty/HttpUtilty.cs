using System.Net;

namespace StudentManagementSystem;

public class HttpUtilty
{
    public static string HttpPost(string url, Dictionary<string, string> pairs)
    {
        try
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "application/json, text/javascript, */*"; 
            request.ContentType = "application/x-www-form-urlencoded";

            byte[] buffer = encoding.GetBytes(fomateParams(pairs));
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                return reader.ReadToEnd();
            }
        }
        catch (WebException ex)
        {
            var res = (HttpWebResponse)ex.Response;
            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            sb.Append(sr.ReadToEnd());
            //string ssb = sb.ToString();
            //throw new Exception(sb.ToString());
            return null;
        }
    }

    /// <summary>  
    /// GET Method  
    /// </summary>  
    /// <returns></returns>  
    public static string HttpGet(string url, Dictionary<string, string> pairs)
    {
        if (pairs.Count() > 0)
        {
            url = url + "?" + fomateParams(pairs);
        }
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
        myRequest.Method = "GET";

        HttpWebResponse myResponse = null;
        try
        {
            myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            return content;
        }
        //异常请求   
        catch (WebException e)
        {
            myResponse = (HttpWebResponse)e.Response;
            using (Stream errData = myResponse.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(errData))
                {
                    string text = reader.ReadToEnd();

                    return text;
                }
            }
        }
    }

    public static string HttpReDistribute(string url, Dictionary<string, string> pairs, int retData, string type)
    {
        if (retData == 1 && type == "get")
        {
            HttpGet(url, pairs);
        }
        if (retData == 0 && type == "get")
        {
            Task.Run(() => HttpGet(url, pairs, type));
        }
        if (retData == 1 && type == "post")
        {
            HttpPost(url, pairs);
        }
        if (retData == 0 && type == "post")
        {
            Task.Run(() => HttpPost(url, pairs, type));
        }
        return null;
    }

    /// <summary>
    /// Get Method No Return
    /// </summary>
    /// <param name="url"></param>
    /// <param name="pairs"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static async Task HttpGet(string url, Dictionary<string, string> pairs, string type)
    {
        if (pairs.Count() > 0)
        {
            url = url + "?" + fomateParams(pairs);
        }
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
        myRequest.Method = "GET";
        await myRequest.GetResponseAsync();
    }
    public static async Task HttpPost(string url, Dictionary<string, string> pairs, string type)
    {
        //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
        Encoding encoding = Encoding.UTF8;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.Accept = "application/json, text/javascript, */*"; //"text/html, application/xhtml+xml, */*";
        request.ContentType = "application/x-www-form-urlencoded";

        byte[] buffer = encoding.GetBytes(fomateParams(pairs));
        request.ContentLength = buffer.Length;
        request.GetRequestStream().Write(buffer, 0, buffer.Length);
        await request.GetResponseAsync();
    }
    /// <summary>  
    /// GET Method  
    /// </summary>  
    /// <returns></returns>  
    public static string HttpGet(string url)
    {
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
        myRequest.Method = "GET";

        HttpWebResponse myResponse = null;
        try
        {
            myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            return content;
        }
        //异常请求  
        catch (WebException e)
        {
            myResponse = (HttpWebResponse)e.Response;
            using (Stream errData = myResponse.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(errData))
                {
                    string text = reader.ReadToEnd();

                    return text;
                }
            }
        }
    }
    private static string fomateParams(Dictionary<string, string> pairs)
    {
        string body = string.Empty;
        if (pairs.Count > 0)
        {
            var pararr = pairs.Select(a => a.Key + "=" + a.Value).ToArray();
            body = string.Join("&", pararr);
        }
        return body;
    }
    /// <summary>
    /// 使用Post方法获取字符串结果
    /// </summary>
    /// <param name="url"></param>
    /// <param name="formItems">Post表单内容</param>
    /// <param name="cookieContainer"></param>
    /// <param name="timeOut">默认20秒</param>
    /// <param name="encoding">响应内容的编码类型（默认utf-8）</param>
    /// <returns></returns>
    public static string PostFormWithFile(string url, List<FormItemModel> formItems, CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 20000000)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        #region 初始化请求对象
        request.Method = "POST";
        request.Timeout = timeOut;
        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        request.KeepAlive = true;
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
        if (!string.IsNullOrEmpty(refererUrl))
            request.Referer = refererUrl;
        if (cookieContainer != null)
            request.CookieContainer = cookieContainer;
        #endregion

        string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符
        request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
        //请求流
        var postStream = new MemoryStream();
        #region 处理Form表单请求内容
        //是否用Form上传文件
        var formUploadFile = formItems != null && formItems.Count > 0;
        if (formUploadFile)
        {
            //文件数据模板
            string fileFormdataTemplate =
                "\r\n--" + boundary +
                "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                "\r\nContent-Type: application/octet-stream" +
                "\r\n\r\n";
            //文本数据模板
            string dataFormdataTemplate =
                "\r\n--" + boundary +
                "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                "\r\n\r\n{1}";
            foreach (var item in formItems)
            {
                string formdata = null;
                if (item.IsFile)
                {
                    //上传文件
                    formdata = string.Format(
                        fileFormdataTemplate,
                        item.Key, //表单键
                        item.FileName);
                }
                else
                {
                    //上传文本
                    formdata = string.Format(
                        dataFormdataTemplate,
                        item.Key,
                        item.Value);
                }

                //统一处理
                byte[] formdataBytes = null;
                //第一行不需要换行
                if (postStream.Length == 0)
                    formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                else
                    formdataBytes = Encoding.UTF8.GetBytes(formdata);
                postStream.Write(formdataBytes, 0, formdataBytes.Length);

                //写入文件内容
                if (item.FileContent != null && item.FileContent.Length > 0)
                {
                    using (var stream = item.FileContent)
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            postStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
            //结尾
            var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            postStream.Write(footer, 0, footer.Length);

        }
        else
        {
            request.ContentType = "application/x-www-form-urlencoded";
        }
        #endregion

        request.ContentLength = postStream.Length;

        #region 输入二进制流
        if (postStream != null)
        {
            postStream.Position = 0;
            //直接写入流
            Stream requestStream = request.GetRequestStream();

            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                requestStream.Write(buffer, 0, bytesRead);
            }

            ////debug
            //postStream.Seek(0, SeekOrigin.Begin);
            //StreamReader sr = new StreamReader(postStream);
            //var postStr = sr.ReadToEnd();
            postStream.Close();//关闭文件访问
        }
        #endregion

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        if (cookieContainer != null)
        {
            response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
        }
        //Guid name = Guid.NewGuid();
        //string path = AppDomain.CurrentDomain.BaseDirectory + @"\tmp" + @"\" + name.ToString() + ".mxml";
        using (Stream responseStream = response.GetResponseStream())
        {
            using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
            {
                //string tempPath = AppDomain.CurrentDomain.BaseDirectory+ @"\musicxmltemp";
                //System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
                //string tempFile = tempPath + @"\" + name.ToString() + ".temp"; //临时文件
                //if (System.IO.File.Exists(tempFile))
                //{
                //    System.IO.File.Delete(tempFile);    //存在则删除
                //}
                //try
                //{
                //    FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                //    byte[] bArr = new byte[1024];
                //    int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                //    while (size > 0)
                //    {
                //        //stream.Write(bArr, 0, size);
                //        fs.Write(bArr, 0, size);
                //        size = responseStream.Read(bArr, 0, (int)bArr.Length);
                //    }
                //    //stream.Close();
                //    fs.Close();
                //    responseStream.Close();
                //    System.IO.File.Move(tempFile, path);
                //}
                //catch (Exception ex)
                //{
                //}
                string retString = myStreamReader.ReadToEnd();
                return retString;
            }
        }
        //return path;
    }

    /// <summary>
    /// Http方式下载文件
    /// </summary>
    /// <param name="url">http地址</param>
    /// <param name="localfile">本地文件</param>
    /// <returns></returns>
    public static bool Download(string url, string localfile)
    {
        bool flag = false;
        long startPosition = 0; // 上次下载的文件起始位置
        FileStream writeStream; // 写入本地文件流对象

        long remoteFileLength = GetHttpLength(url);// 取得远程文件长度
        System.Console.WriteLine("remoteFileLength=" + remoteFileLength);
        if (remoteFileLength == 745)
        {
            System.Console.WriteLine("远程文件不存在.");
            return false;
        }

        // 判断要下载的文件夹是否存在
        if (File.Exists(localfile))
        {

            writeStream = File.OpenWrite(localfile);             // 存在则打开要下载的文件
            startPosition = writeStream.Length;                  // 获取已经下载的长度

            if (startPosition >= remoteFileLength)
            {
                System.Console.WriteLine("本地文件长度" + startPosition + "已经大于等于远程文件长度" + remoteFileLength);
                writeStream.Close();

                return false;
            }
            else
            {
                writeStream.Seek(startPosition, SeekOrigin.Current); // 本地文件写入位置定位
            }
        }
        else
        {
            writeStream = new FileStream(localfile, FileMode.Create);// 文件不保存创建一个文件
            startPosition = 0;
        }


        try
        {
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);// 打开网络连接

            if (startPosition > 0)
            {
                myRequest.AddRange((int)startPosition);// 设置Range值,与上面的writeStream.Seek用意相同,是为了定义远程文件读取位置
            }


            Stream readStream = myRequest.GetResponse().GetResponseStream();// 向服务器请求,获得服务器的回应数据流


            byte[] btArray = new byte[512];// 定义一个字节数据,用来向readStream读取内容和向writeStream写入内容
            int contentSize = readStream.Read(btArray, 0, btArray.Length);// 向远程文件读第一次

            long currPostion = startPosition;

            while (contentSize > 0)// 如果读取长度大于零则继续读
            {
                currPostion += contentSize;
                int percent = (int)(currPostion * 100 / remoteFileLength);
                System.Console.WriteLine("percent=" + percent + "%");

                writeStream.Write(btArray, 0, contentSize);// 写入本地文件
                contentSize = readStream.Read(btArray, 0, btArray.Length);// 继续向远程文件读取
            }

            //关闭流
            writeStream.Close();
            readStream.Close();

            flag = true;        //返回true下载成功
        }
        catch (Exception)
        {
            writeStream.Close();
            flag = false;       //返回false下载失败
        }

        return flag;
    }

    // 从文件头得到远程文件的长度
    private static long GetHttpLength(string url)
    {
        long length = 0;

        try
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);// 打开网络连接
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();

            if (rsp.StatusCode == HttpStatusCode.OK)
            {
                length = rsp.ContentLength;// 从文件头得到远程文件的长度
            }

            rsp.Close();
            return length;
        }
        catch (Exception)
        {
            return length;
        }

    }
    public static MemoryStream GetHttpImageByte(string url)
    {
        WebRequest quest = WebRequest.Create(url);
        WebResponse response = quest.GetResponse();
        Stream responseStream = response.GetResponseStream();
        int buffsize = 1024;
        byte[] buffer = new byte[buffsize];
        MemoryStream ms = new MemoryStream();
        int count = responseStream.Read(buffer, 0, buffsize);
        while (count > 0)
        {

            ms.WriteAsync(buffer, 0, count);
            count = responseStream.Read(buffer, 0, buffsize);
        }
        ms.Seek(0, SeekOrigin.Begin);
        return ms;

    }
}
public class FormItemModel
{
    /// <summary>  
    /// 表单键，request["key"]  
    /// </summary>  
    public string Key { set; get; }
    /// <summary>  
    /// 表单值,上传文件时忽略，request["key"].value  
    /// </summary>  
    public string Value { set; get; }
    /// <summary>  
    /// 是否是文件  
    /// </summary>  
    public bool IsFile
    {
        get
        {
            if (FileContent == null || FileContent.Length == 0)
                return false;

            if (FileContent != null && FileContent.Length > 0 && string.IsNullOrWhiteSpace(FileName))
                throw new Exception("上传文件时 FileName 属性值不能为空");
            return true;
        }
    }
    /// <summary>  
    /// 上传的文件名  
    /// </summary>  
    public string FileName { set; get; }
    /// <summary>  
    /// 上传的文件内容  
    /// </summary>  
    public Stream FileContent { set; get; }
}