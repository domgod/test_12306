using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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
using System.Xml.Linq;

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
        //用来存储不同城市对应的编码的字典
        Dictionary<string, string> ZhanTai = new Dictionary<string, string>();
        Dictionary<string, string> BianMa = new Dictionary<string, string>();
        //用来存放每一列火车的信息
        Dictionary<string, string> HuoChePiaoInfo = new Dictionary<string, string>();
        
        
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
                    BianMa.Add(VALUE, KEY);
                }
            }
        }

        
        private void ChaXun_Click(object sender, EventArgs e)
        {
            //清除表格残留数据
            listView1.Columns.Clear();
            listView1.Items.Clear();
           
            
            string FormAddress = ZhanTai[textBox_ChuFaDi.Text] ;
            string ToAddress = ZhanTai[textBox_MuDiDi.Text];
            string uri = "https://kyfw.12306.cn/otn/leftTicket/queryE?leftTicketDTO.train_date=" + dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd") + "&leftTicketDTO.from_station=" + FormAddress + "&leftTicketDTO.to_station=" + ToAddress + "&purpose_codes=ADULT";
            string refererUri = uri;
            string encodingName = "utf-8";

            string html = string.Empty;
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
           // 
            JObject obj = (JObject)JsonConvert.DeserializeObject(html);//将刚才一大串字符串转换成一个大对象
            string data = obj["data"]["result"].ToString();
            string[] HuoCheInfos=data.Split(',');
            string s = "";
            //j用来标志是第一次循环吗
            for(int i = 0,j=1;i < HuoCheInfos.Length;i++,j++)
            {
                if(i%2==1)
                {
                    continue;
                }
                string[] HuoChePiaos = HuoCheInfos[i].Split('|');
                HuoChePiaoInfo.Add("车次", HuoChePiaos[3]);
                HuoChePiaoInfo.Add("火车始发地", BianMa[HuoChePiaos[4]]);
                HuoChePiaoInfo.Add("火车终点站", BianMa[HuoChePiaos[5]]);
                HuoChePiaoInfo.Add("上车站", BianMa[HuoChePiaos[6]]);
                HuoChePiaoInfo.Add("下车站", BianMa[HuoChePiaos[7]]);
                HuoChePiaoInfo.Add("出发时间", HuoChePiaos[8]);
                HuoChePiaoInfo.Add("到达时间", HuoChePiaos[9]);
                HuoChePiaoInfo.Add("历时", HuoChePiaos[10]);
               // HuoChePiaoInfo.Add("软座", HuoChePiaos[25]);
               // HuoChePiaoInfo.Add("动卧", HuoChePiaos[27]);
                HuoChePiaoInfo.Add("高级软卧", HuoChePiaos[21]==""?"无": HuoChePiaos[21]);
                HuoChePiaoInfo.Add("软卧（一等卧）", HuoChePiaos[23] == "" ? "无" : HuoChePiaos[23]);
                HuoChePiaoInfo.Add("无座", HuoChePiaos[26] == "" ? "无" : HuoChePiaos[26]);
                HuoChePiaoInfo.Add("硬卧（二等座）", HuoChePiaos[28] == "" ? "无" : HuoChePiaos[28]);
                HuoChePiaoInfo.Add("硬座", HuoChePiaos[29] == "" ? "无" : HuoChePiaos[29]);
                HuoChePiaoInfo.Add("二等座（二等包座）", HuoChePiaos[30] == "" ? "无" : HuoChePiaos[30]);   
                HuoChePiaoInfo.Add("一等座", HuoChePiaos[31] == "" ? "无" : HuoChePiaos[31]); 
                HuoChePiaoInfo.Add("商务座", HuoChePiaos[32] == "" ? "无" : HuoChePiaos[32]);
                foreach (string key in HuoChePiaoInfo.Keys)
                {
                    s = s +" "+ key + "=" +HuoChePiaoInfo[key]+";";
                   

                }
                //将火车票数据写入表格中
                ////添加表格行表头
                var ListView1_Item= listView1.Items.Add(HuoChePiaoInfo["车次"]);
                foreach (string key in HuoChePiaoInfo.Keys)
                {
                    //添加表格列表头
                    if (j == 1) { listView1.Columns.Add(key); }
                    //添加表格行数据
                    if (key == "车次") { continue; }
                    ListView1_Item.SubItems.Add(HuoChePiaoInfo[key]);

                }
                s =s + "\r\n\r\n";
                HuoChePiaoInfo.Clear();

            }
            textBox1.Text = s;

        }
    }
}
