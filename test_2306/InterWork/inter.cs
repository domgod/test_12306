using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace test_2306.InterWork
{
    public class inter
    {
        CookieContainer cookieContainer;
        public inter() { }
        public inter(CookieContainer cookieContainer)
        {
            this.cookieContainer = cookieContainer;
        }

        public string post(string uri)
        {
            /// <summary>
            /// 获取页面html
            /// </summary>
            /// <param name="uri">访问url</param>
            /// <param name="refererUri">来源url</param>
            /// <param name="encodingName">编码名称 例如：gb2312</param>
            /// <returns></returns>
            string encodingName = "utf-8";
            string refererUri = string.Empty;
            string Date = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "text/html;charset=" + encodingName;
            request.Method = "Get";
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.26 Safari/537.36 Core/1.63.5221.400 QQBrowser/10.0.1125.400";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 SLBrowser/9.0.0.10191 SLBChan/112";

            if (cookieContainer != null) request.CookieContainer=cookieContainer;

            if (!string.IsNullOrEmpty(refererUri))
                request.Referer = refererUri;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                foreach (Cookie cook in response.Cookies)
                {
                    #region 获取详细cookie信息
                    //Console.WriteLine("Cookie:");
                    //Console.WriteLine($"{cook.Name} = {cook.Value}");
                    //Console.WriteLine($"Domain: {cook.Domain}");
                    //Console.WriteLine($"Path: {cook.Path}");
                    //Console.WriteLine($"Port: {cook.Port}");
                    //Console.WriteLine($"Secure: {cook.Secure}");

                    //Console.WriteLine($"When issued: {cook.TimeStamp}");
                    //Console.WriteLine($"Expires: {cook.Expires} (expired? {cook.Expired})");
                    //Console.WriteLine($"Don't save: {cook.Discard}");
                    //Console.WriteLine($"Comment: {cook.Comment}");
                    //Console.WriteLine($"Uri for comments: {cook.CommentUri}");
                    //Console.WriteLine($"Version: RFC {(cook.Version == 1 ? 2109 : 2965)}");

                    //// Show the string representation of the cookie.
                    //Console.WriteLine($"String: {cook}");
                    #endregion
                    cookieContainer.Add(cook);
                }
                    #region 获取粗略cookie
                //string st = response.Headers.Get("Set-Cookie");//获取Cookie
                //Console.WriteLine(uri+" cookies="+st);
                //if(st!=null)
                //{
                //    string[] sts = st.Split(';');
                //    foreach (var str in sts)
                //    {
                //        string[] strs = str.Split('=');
                //        Cookie cookie = new Cookie(strs[0], strs[1]);
                //        cookieContainer.Add(cookie);
                //    }
                //}
                #endregion
                using (Stream streamResponse = response.GetResponseStream())
                {
                    using (StreamReader streamResponseReader = new StreamReader(streamResponse, Encoding.GetEncoding(encodingName)))
                    {
                        Date = streamResponseReader.ReadToEnd();
                    }
                }
            }

            return Date;
        }
        public string post1(string uri)
        {
            /// <summary>
            /// 获取页面html
            /// </summary>
            /// <param name="uri">访问url</param>
            /// <param name="refererUri">来源url</param>
            /// <param name="encodingName">编码名称 例如：gb2312</param>
            /// <returns></returns>
            string encodingName = "utf-8";
            string refererUri = string.Empty;
            string Date = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "text/html;charset=" + encodingName;
            request.Method = "Post";
            request.ContentLength = 0;
            request.ServicePoint.Expect100Continue = false;
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.26 Safari/537.36 Core/1.63.5221.400 QQBrowser/10.0.1125.400";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 SLBrowser/9.0.0.10191 SLBChan/112";

            if (cookieContainer != null) request.CookieContainer = cookieContainer;

            if (!string.IsNullOrEmpty(refererUri))
                request.Referer = refererUri;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                foreach (Cookie cook in response.Cookies)
                {
                    #region 获取详细cookie信息
                    //Console.WriteLine("Cookie:");
                    //Console.WriteLine($"{cook.Name} = {cook.Value}");
                    //Console.WriteLine($"Domain: {cook.Domain}");
                    //Console.WriteLine($"Path: {cook.Path}");
                    //Console.WriteLine($"Port: {cook.Port}");
                    //Console.WriteLine($"Secure: {cook.Secure}");

                    //Console.WriteLine($"When issued: {cook.TimeStamp}");
                    //Console.WriteLine($"Expires: {cook.Expires} (expired? {cook.Expired})");
                    //Console.WriteLine($"Don't save: {cook.Discard}");
                    //Console.WriteLine($"Comment: {cook.Comment}");
                    //Console.WriteLine($"Uri for comments: {cook.CommentUri}");
                    //Console.WriteLine($"Version: RFC {(cook.Version == 1 ? 2109 : 2965)}");

                    //// Show the string representation of the cookie.
                    //Console.WriteLine($"String: {cook}");
                    #endregion
                    cookieContainer.Add(cook);
                }
                #region 获取粗略cookie
                //string st = response.Headers.Get("Set-Cookie");//获取Cookie
                //Console.WriteLine(uri+" cookies="+st);
                //if(st!=null)
                //{
                //    string[] sts = st.Split(';');
                //    foreach (var str in sts)
                //    {
                //        string[] strs = str.Split('=');
                //        Cookie cookie = new Cookie(strs[0], strs[1]);
                //        cookieContainer.Add(cookie);
                //    }
                //}
                #endregion
                using (Stream streamResponse = response.GetResponseStream())
                {
                    using (StreamReader streamResponseReader = new StreamReader(streamResponse, Encoding.GetEncoding(encodingName)))
                    {
                        Date = streamResponseReader.ReadToEnd();
                    }
                }
            }

            return Date;
        }




    }
    
}
