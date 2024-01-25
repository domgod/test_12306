using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_2306
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void DengLu_Click(object sender, EventArgs e)
        {
            //方式1:简单方法
            //string strHTML = "";
            //WebClient myWebClient = new WebClient();
            //Stream myStream = myWebClient.OpenRead("https://kyfw.12306.cn/otn/leftTicket/queryE?leftTicketDTO.train_date="+DateTime.Now.ToString("yyyy-MM-dd")+" &leftTicketDTO.from_station=SZQ&leftTicketDTO.to_station=HDP&purpose_codes=ADULT");
            //StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
            //strHTML = sr.ReadToEnd();
            //myStream.Close();
            //textBox3.Text= strHTML;

            //方式二：中等方法
            //string urll = "https://kyfw.12306.cn/otn/leftTicket/queryE?leftTicketDTO.train_date=2024-01-23&leftTicketDTO.from_station=SZQ&leftTicketDTO.to_station=HDP&purpose_codes=ADULT";
            //Uri uri = new Uri(urll);
            //WebRequest myReq = WebRequest.Create(urll);
            //WebResponse result = myReq.GetResponse();
            //Stream receviceStream = result.GetResponseStream();
            //StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
            //string strHTML = readerOfStream.ReadToEnd();
            //readerOfStream.Close();
            //receviceStream.Close();
            //result.Close();
            //textBox3.Text = strHTML;
            //复杂方法：可以带cookie
            /// <summary>
            /// 获取页面html
            /// </summary>
            /// <param name="uri">访问url</param>
            /// <param name="refererUri">来源url</param>
            /// <param name="encodingName">编码名称 例如：gb2312</param>
            /// <returns></returns>
           
               // string uri = "https://kyfw.12306.cn/";
               
                string encodingName = "utf-8";
                string refererUri = "https://kyfw.12306.cn/otn/leftTicket/queryE?leftTicketDTO.train_date=2024-01-23&leftTicketDTO.from_station=SZQ&leftTicketDTO.to_station=HDP&purpose_codes=ADULT";
                string uri = refererUri;

                string html = string.Empty;
                CookieContainer cookieContainer = new CookieContainer();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                cookieContainer.Add(new Cookie("BAIDUID", "67017F5C6A5EE8351192F7D34E7A221E:FG=1", "", "kyfw.12306.cn"));
                cookieContainer.Add(new Cookie("PSTM", "1523879243", "", "kyfw.12306.cn"));
                cookieContainer.Add(new Cookie("BIDUPSID", "A29EA919049CED566C183C7ED175C6AB", "", "kyfw.12306.cn"));
                cookieContainer.Add(new Cookie("BD_UPN", "1a314353", "", "kyfw.12306.cn"));
                cookieContainer.Add(new Cookie("BDUSS", "1F4Wk1EUUxEWkNEZS1lUWdSNkFWOW5IbThoYXNYcktMWmhmRkE5MkxvQU9Jd0piQVFBQUFBJCQAAAAAAAAAAAEAAAD9qTIYw867wzGw19K5AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA6W2loOltpaY", "", "kyfw.12306.cn"));
                request.ContentType = "text/html;charset=" + encodingName;
                request.Method = "Get";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.26 Safari/537.36 Core/1.63.5221.400 QQBrowser/10.0.1125.400";
                request.CookieContainer = cookieContainer;

                if (!string.IsNullOrEmpty(refererUri))
                    request.Referer = refererUri;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream streamResponse = response.GetResponseStream())
                    {
                        using (StreamReader streamResponseReader = new StreamReader(streamResponse, Encoding.GetEncoding(encodingName)))
                        {
                            html = streamResponseReader.ReadToEnd();
                        }
                    }
                }

            textBox1.Text = html.ToString();





        }
        Dictionary<string, string> ZhanTai = new Dictionary<string, string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            ///////////////////////////////////////获取站台编码
            /// <summary>
            /// 获取页面html
            /// </summary>
            /// <param name="uri">访问url</param>
            /// <param name="refererUri">来源url</param>
            /// <param name="encodingName">编码名称 例如：gb2312</param>
            /// <returns></returns>
            //获取站台对应编码
            // string uri = "https://kyfw.12306.cn/otn/leftTicket/queryE?leftTicketDTO.train_date=2024-01-24&leftTicketDTO.from_station=SZQ&leftTicketDTO.to_station=HDP&purpose_codes=ADULT";
            string uri = "https://kyfw.12306.cn/otn/resources/js/framework/station_name.js?station_version=1.9297";
            string refererUri = uri;
            string encodingName = "utf-8";

            string html_DiZhiBianMa = string.Empty;
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            cookieContainer.Add(new Cookie("_uab_collina", "170554335525961696602851", "/otn/leftTicket", "kyfw.12306.cn"));
            cookieContainer.Add(new Cookie("JSESSIONID", "7635D7764CCF7C700C622EFF8E92552E", "/otn", "kyfw.12306.cn"));
            cookieContainer.Add(new Cookie("BIGipServerpassport", "904397066.50215.0000", "/", "kyfw.12306.cn"));
            cookieContainer.Add(new Cookie("guidesStatus", "off", "/", ".12306.cn"));
            cookieContainer.Add(new Cookie("highContrastMode", "defaltMode", "/", ".12306.cn"));
            cookieContainer.Add(new Cookie("route", "c5c62a339e7744272a54643b3be5bf64", "/", "kyfw.12306.cn"));
            cookieContainer.Add(new Cookie("BIGipServerotn", "2564227338.24610.0000", "/", "kyfw.12306.cn"));
            cookieContainer.Add(new Cookie("BIGipServerpassport", "904397066.50215.0000", "/", "kyfw.12306.cn"));


            //cookieContainer.Add(new Cookie("PSTM", "1523879243", "", "www.baidu.com"));
            //cookieContainer.Add(new Cookie("BIDUPSID", "A29EA919049CED566C183C7ED175C6AB", "", "www.baidu.com"));
            //cookieContainer.Add(new Cookie("BD_UPN", "1a314353", "", "www.baidu.com"));
            //cookieContainer.Add(new Cookie("BDUSS", "1F4Wk1EUUxEWkNEZS1lUWdSNkFWOW5IbThoYXNYcktMWmhmRkE5MkxvQU9Jd0piQVFBQUFBJCQAAAAAAAAAAAEAAAD9qTIYw867wzGw19K5AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA6W2loOltpaY", "", "www.baidu.com"));
            request.ContentType = "text/html;charset=" + encodingName;
            request.Method = "Get";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.26 Safari/537.36 Core/1.63.5221.400 QQBrowser/10.0.1125.400";
            request.CookieContainer = cookieContainer;

            if (!string.IsNullOrEmpty(refererUri))
                request.Referer = refererUri;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream streamResponse = response.GetResponseStream())
                {
                    using (StreamReader streamResponseReader = new StreamReader(streamResponse, Encoding.GetEncoding(encodingName)))
                    {
                        html_DiZhiBianMa = streamResponseReader.ReadToEnd();
                    }
                }
            }
            //textBox1.Text = html_DiZhiBianMa;

            string[] ZhanTai_BianMas = html_DiZhiBianMa.Split('@');
            foreach (string ZhanTai_BianMa in ZhanTai_BianMas)
            {
                string[] str = ZhanTai_BianMa.Split('|');
                if (str.Length > 1)
                {
                    string KEY = str[1];
                    string VALUE = str[2];
                    ZhanTai.Add(KEY, VALUE);
                }
            }
        }

        
        private void ChaXun_Click(object sender, EventArgs e)
        {

           
            
            string FormAddress = ZhanTai[textBox_ChuFaDi.Text] ;
            string ToAddress = ZhanTai[textBox_MuDiDi.Text];

            #region
            /*
             int FormAddress_Length = textBox_ChuFaDi.TextLength;
             int ToAddress_Length = textBox_MuDiDi.TextLength;
             string BianMa1 = html_DiZhiBianMa;
             while (true)
             {
                 BianMa1 = BianMa1.Substring(BianMa1.IndexOf(textBox_ChuFaDi.Text + "|") + FormAddress_Length + 1);
                 if (Regex.IsMatch(BianMa1.Substring(0, 1), "[A-Z{3}]"))
                 {
                     break;
                 }
             }
             FormAddress = BianMa1.Substring(0, 3);

             string BianMa2 = html_DiZhiBianMa;
             while (true)
             {
                 BianMa2 = BianMa2.Substring(BianMa2.IndexOf(textBox_MuDiDi.Text + "|") + ToAddress_Length + 1);
                 if (Regex.IsMatch(BianMa2.Substring(0, 1), "[A-Z{3}]"))
                 {
                     break;
                 }
             }
             ToAddress = BianMa2.Substring(0, 3);
            */
            #endregion
            string uri = "https://kyfw.12306.cn/otn/leftTicket/queryE?leftTicketDTO.train_date=" + dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd") + "&leftTicketDTO.from_station=" + FormAddress + "&leftTicketDTO.to_station=" + ToAddress + "&purpose_codes=ADULT";
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(uri);
            StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            textBox1.Text= strHTML;


        }
    }
}
