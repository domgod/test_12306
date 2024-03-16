using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test_2306.data;
using System.Threading;

namespace test_2306.InterWork
{
    public class inter
    {
        int UserAgentInfoIndex ;
        CookieContainer cookieContainer;
        string[] UserAgentInfos = new string[]
        {
            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; Hot Lingo 2.0)",
            "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.70 Safari/537.36",
            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; MATP; InfoPath.2; .NET4.0C; CIBA; Maxthon 2.0)",
            "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.71 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.70 Safari/537.36"
        };


        public inter()
        {
            Random rd = new Random();
            UserAgentInfoIndex = rd.Next(0, 5);
        }
        public inter(CookieContainer cookieContainer)
        {
            this.cookieContainer = cookieContainer;
            Random rd = new Random();
            UserAgentInfoIndex = rd.Next(0, 5);

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
            request.UserAgent= UserAgentInfos[UserAgentInfoIndex];
            //// request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.26 Safari/537.36 Core/1.63.5221.400 QQBrowser/10.0.1125.400";
            //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 SLBrowser/9.0.0.10191 SLBChan/112";

            if (cookieContainer != null) request.CookieContainer=cookieContainer;

            if (!string.IsNullOrEmpty(refererUri))
                request.Referer = refererUri;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                foreach (Cookie cook in response.Cookies)
                {
                    #region 获取详细cookie信息
                    Console.WriteLine(uri);
                    Console.WriteLine("Cookie:");
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

                    // Show the string representation of the cookie.
                    Console.WriteLine($"String: {cook}");
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
        public string post(string uri,string RefererUri)
        {
            /// <summary>
            /// 获取页面html
            /// </summary>
            /// <param name="uri">访问url</param>
            /// <param name="refererUri">来源url</param>
            /// <param name="encodingName">编码名称 例如：gb2312</param>
            /// <returns></returns>
            string encodingName = "utf-8";
            string refererUri = RefererUri;
            string Date = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "text/html;charset=" + encodingName;
            request.Method = "Get";
            request.UserAgent = UserAgentInfos[UserAgentInfoIndex];
            if (cookieContainer != null) request.CookieContainer = cookieContainer;

            if (!string.IsNullOrEmpty(refererUri))
                request.Referer = refererUri;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                foreach (Cookie cook in response.Cookies)
                {
                    #region 获取详细cookie信息
                    Console.WriteLine(uri);
                    Console.WriteLine("Cookie:");
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

                    // Show the string representation of the cookie.
                    Console.WriteLine($"String: {cook}");
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
            request.UserAgent = UserAgentInfos[UserAgentInfoIndex];
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

        //查询登陆状态
        public bool post_checkUser()
        {
            string html_CheckUser = null;
           
            string uri_CheckUser = "https://kyfw.12306.cn/otn/login/checkUser?_json_att=";
            html_CheckUser = this.post(uri_CheckUser);
            JObject obj_CheckUser = (JObject)JsonConvert.DeserializeObject(html_CheckUser);//将刚才一大串字符串转换成一个大对象
            string flag = obj_CheckUser["data"]["flag"].ToString();
            if (flag != "True") { MessageBox.Show("系统没有登陆，请登陆");return false; }
            else { return true; }
           
            
        }

        //获取火车信息
        public List<HuoChePiao> post_queryE(string FormAddress,string ToAddress,string Time)
        {
            string html = string.Empty;
            List<HuoChePiao> List_HuoChePiao = new List<HuoChePiao>();
               
            string uri =$"https://kyfw.12306.cn/otn/leftTicket/queryE?leftTicketDTO.train_date={Time}&leftTicketDTO.from_station={FormAddress}&leftTicketDTO.to_station={ToAddress}&purpose_codes=ADULT";
            html = this.post(uri);
            JObject obj = (JObject)JsonConvert.DeserializeObject(html);//将刚才一大串字符串转换成一个大对象
            string data = obj["data"]["result"].ToString();
            string[] HuoCheInfos = data.Substring(1).Split(',');
            for (int i = 0; i < HuoCheInfos.Length; i++)
            {
                //用来防止一个火车票中间的逗号影响
                if (i % 2 == 0)
                {
                    continue;
                }
                HuoCheInfos[i] = HuoCheInfos[i - 1] + HuoCheInfos[i];
                HuoChePiao hcp = new HuoChePiao(HuoCheInfos[i]);
                List_HuoChePiao.Add(hcp);
            }
            return List_HuoChePiao;
           
        }

        //预定火车票
        public bool  post_submitOrderRequest(string secretStr,string GoDate,string BackDate,string FromStationName,string ToStationName,string BedLevelInfo,string SeatDiscountInfo)
        {
            string html_submitOrderRequest = null;
            
            string uri_submitOrderRequest = $"https://kyfw.12306.cn/otn/leftTicket/submitOrderRequest?secretStr={secretStr}&train_date={GoDate}&back_train_date={BackDate}&tour_flag=dc&purpose_codes=ADULT&query_from_station_name={FromStationName}&query_to_station_name={ToStationName}&bed_level_info={BedLevelInfo}&seat_discount_info={SeatDiscountInfo}&undefined";
            html_submitOrderRequest = this.post(uri_submitOrderRequest);
            JObject obj_submitOrderRequest = (JObject)JsonConvert.DeserializeObject(html_submitOrderRequest);//将刚才一大串字符串转换成一个大对象
            string status = obj_submitOrderRequest["status"].ToString();
            if (status != "True") { return false;  /*return "火车票预定失败，正在重新预定...";*/ }
            else return true;  /*"正在预定火车票...."*/;
            
        }
       
        //订单初始化
        public string[] post_initDc()
        {
            string[] Info=new string[2];
            string obj_initDc = null;
           
                string uri_initDc = "https://kyfw.12306.cn/otn/confirmPassenger/initDc?_json_att=";
                obj_initDc = this.post(uri_initDc);
                var index = obj_initDc.IndexOf("globalRepeatSubmitToken");
                if (index > -1) { index += 27; }
                //globalRepeatSubmitToken = obj_initDc.Substring(index, 32);
                Info[0] = obj_initDc.Substring(index, 32);
                index = obj_initDc.IndexOf("key_check_isChange");
                if (index > -1) { index += 21; }
                // key_check_isChange = obj_initDc.Substring(index, 56);
                Info[1]= obj_initDc.Substring(index, 56);
                return Info;
        }
       
        // 获取账号中所有乘车人信息
        public void post_getPassengerDTOs(string globalRepeatSubmitToken)
        {
            string html_getPassengerDTOs = null;
            
            string uri_getPassengerDTOs = $"https://kyfw.12306.cn/otn/confirmPassenger/getPassengerDTOs?_json_att=&REPEAT_SUBMIT_TOKEN={globalRepeatSubmitToken}";
            html_getPassengerDTOs = this.post(uri_getPassengerDTOs);
            JObject obj_getPassengerDTOs = (JObject)JsonConvert.DeserializeObject(html_getPassengerDTOs);//将刚才一大串字符串转换成一个大对象
            var status_getPassengerDTOs = obj_getPassengerDTOs["status"].ToString();
            if (status_getPassengerDTOs != "True") { MessageBox.Show("在订票过程中获取不到乘车人信息出错，请重试"); return; }
                //foreach(var passenger in normal_passengers)
                //{
                //    ChengKe ck=new ChengKe();
                //    ck.Passenger_Name = passenger["passenger_name"].ToString();
                //    ck.Passenger_Id_Type_Code = passenger["passenger_id_type_code"].ToString();
                //    ck.Passenger_Id_No = passenger["passenger_id_no"].ToString();
                //    ck.Mobile_No = passenger["mobile_no"].ToString();
                //    ck.AllEncStr = passenger["allEncStr"].ToString();

                //    checkedListBox_ChengKe.Items.Add(ck.Passenger_Name + "\r\n");
                //}

           
        }

        //检查选票人信息
        public bool post_checkorderInfo(HuoChePiao HuoChePiaoInfo,List<ChengKe> ChengKeInfo,string globalRepeatSubmitToken)
        {
            string html_checkorderInfo=null;
            string uri_checkOrderInfo_1 = "https://kyfw.12306.cn/otn/confirmPassenger/checkOrderInfo?cancel_flag=2&bed_level_order_num=000000000000000000000000000000";
            string uri_checkOrderInfo_2 = null;
            string uri_checkOrderInfo_3 = null;
            string uri_checkOrderInfo_4 = "&oldPassengerStr=";//存oldPassengerStr
            string uri_checkorderInfo_5 = null;
            string uri_checkOrderInfo_6 = "&passengerTicketStr="; //存passengerTicketStr
            for (int i = 0; i < ChengKeInfo.Count; i++)
            {
                switch (HuoChePiaoInfo.seat_type)
                {
                    case "9": uri_checkOrderInfo_2 = "9,0,1,"; break;
                    case "M": uri_checkOrderInfo_2 = "M,0,1,"; break;
                    case "O": uri_checkOrderInfo_2 = "O,0,1,"; break;
                    case "2": uri_checkOrderInfo_2 = "2,0,1,"; break;
                    case "1": uri_checkOrderInfo_2 = "1,0,1,"; break;
                    case "4": uri_checkOrderInfo_2 = "4,0,1,"; break;
                    case "3": uri_checkOrderInfo_2 = "3,0,1,"; break;
                    case "F": uri_checkOrderInfo_2 = "F,0,1,"; break;
                    default: break;
                }
                if (ChengKeInfo.Count ==1|| i==ChengKeInfo.Count-1)
                {
                    uri_checkOrderInfo_3 = ChengKeInfo[i].Passenger_Name + "," + ChengKeInfo[i].Passenger_Id_Type_Code + "," + ChengKeInfo[i].Passenger_Id_No + "," + ChengKeInfo[i].Mobile_No + ",N," + ChengKeInfo[i].AllEncStr;
                }
                else
                {
                    uri_checkOrderInfo_3 = ChengKeInfo[i].Passenger_Name + "," + ChengKeInfo[i].Passenger_Id_Type_Code + "," + ChengKeInfo[i].Passenger_Id_No + "," + ChengKeInfo[i].Mobile_No + ",N," + ChengKeInfo[i].AllEncStr+"_";

                }
                uri_checkOrderInfo_6 += uri_checkOrderInfo_2 + uri_checkOrderInfo_3;

            }
            for(int i=0;i<ChengKeInfo.Count;i++)
            {
                uri_checkOrderInfo_4 +=  ChengKeInfo[i].Passenger_Name + "," + ChengKeInfo[i].Passenger_Id_Type_Code + "," + ChengKeInfo[i].Passenger_Id_No + ",1_";

            }

            uri_checkorderInfo_5 = "&tour_flag=dc&whatsSelect=1&sessionId=&sig=&scene=nc_login&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
            string uri_checkorderInfo = uri_checkOrderInfo_1 + uri_checkOrderInfo_6 + uri_checkOrderInfo_4 + uri_checkorderInfo_5;
            Thread.Sleep(900);
            html_checkorderInfo = this.post1(uri_checkorderInfo);
            JObject obj_checkorderInfo = (JObject)JsonConvert.DeserializeObject(html_checkorderInfo);//将刚才一大串字符串转换成一个大对象
            var obj_submitStatus = obj_checkorderInfo["status"]["submitStatus"].ToString();
            if (obj_submitStatus != "True") {  return false; }
            else { return true; }
           
        }

        //提交订单，获取订单实际情况
        public string post_getQueueCount(HuoChePiao HuoChePiaoInfo, string FromAddressBianMa, string ToAddressBianMa, DateTime ToTime,string globalRepeatSubmitToken)
        {
            #region 提交订单，获取订单实际情况
            //不知道访问这个有没有用
            string uri_oynqujj = "https://kyfw.12306.cn/otn/resources/route/kyfw.12306.cn/otn/confirmPassenger/initDc.js";
            string html_oynqujj = this.post(uri_oynqujj);

            //用来存储访问网站的返回信息
            string html_getQueueCount = null;
            
            string[] Month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var S_Week = ToTime.DayOfWeek.ToString().Substring(0, 3);
            var S_Month = Month[ToTime.Month - 1];
            var S_Day = ToTime.Day.ToString();
            if (S_Day.Length < 2) S_Day = "0" + S_Day;
            var S_Year =ToTime.Year.ToString();

            string url_getQueueCount2 = null;
            string url_getQueueCount1 = "https://kyfw.12306.cn/otn/confirmPassenger/getQueueCount?train_date=" + S_Week + "+" + S_Month + "+" + S_Day + "+" + S_Year + "+00%3A00%3A00+GMT%2B0800+(%E4%B8%AD%E5%9B%BD%E6%A0%87%E5%87%86%E6%97%B6%E9%97%B4)&train_no=" +HuoChePiaoInfo.train_no + "&stationTrainCode=" + HuoChePiaoInfo.CheCi;

            switch (HuoChePiaoInfo.seat_type)
            {
                case "9": url_getQueueCount2 = "&seatType=9"; break;
                case "M": url_getQueueCount2 = "&seatType=M"; break;
                case "O": url_getQueueCount2 = "&seatType=O"; break;
                case "2": url_getQueueCount2 = "&seatType=2"; break;
                case "1": url_getQueueCount2 = "&seatType=1"; break;
                case "4": url_getQueueCount2 = "&seatType=4"; break;
                case "3": url_getQueueCount2 = "&seatType=3"; break;
                case "F": url_getQueueCount2 = "&seatType=F"; break;
                default: break;
            }

            string url_getQueueCount3 = "&fromStationTelecode=" + FromAddressBianMa + "&toStationTelecode=" + ToAddressBianMa + "&leftTicket=" + HuoChePiaoInfo.leftTicket + "&purpose_codes=00&train_location=" + HuoChePiaoInfo.train_location + "&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
            string url_getQueueCount = url_getQueueCount1 + url_getQueueCount2 + url_getQueueCount3;

            Cookie cook1 = new Cookie("_jc_save_toDate", DateTime.Now.ToString("yyyy-MM-dd"), "/", "kyfw.12306.cn");
            Cookie cook2 = new Cookie("_jc_save_fromStation", FromAddressBianMa, "/", "kyfw.12306.cn");
            Cookie cook3 = new Cookie("_jc_save_toStation", ToAddressBianMa, "/", "kyfw.12306.cn");
            Cookie cook4 = new Cookie("_jc_save_toDate", ToTime.ToString("yyyy-MM-dd"), "/", "kyfw.12306.cn");
            this.cookieContainer.Add(cook1);
            this.cookieContainer.Add(cook2);
            this.cookieContainer.Add(cook3);
            this.cookieContainer.Add(cook4);
            html_getQueueCount = this.post(url_getQueueCount);
            JObject obj_getQueueCount = (JObject)JsonConvert.DeserializeObject(html_getQueueCount);//将刚才一大串字符串转换成一个大对象
            var status1 = obj_getQueueCount["status"].ToString();
            if (status1 != "True") { return "提交订单出错，正在重试..."; }
            else return "提交订单成功...";
           
            #endregion
        }

        //确认订单
        public string post_confirmSingleForQueue(HuoChePiao HuoChePiaoInfo,List<ChengKe> ChengKeInfo,string key_check_isChange,string globalRepeatSubmitToken)
        {
            #region 确认订单
            //存储网页返回数据
            string html_confirmSingleForQueue = null;

            string uri_confirmSingleForQueue1 = "https://kyfw.12306.cn/otn/confirmPassenger/confirmSingleForQueue?";
            string uri_confirmSingleForQueue2 = "passengerTicketStr=";  
            string uri_confirmSingleForQueue3 = null;
            string uri_confirmSingleForQueue4 = "&oldPassengerStr=";
            string uri_confirmSingleForQueue5 = null;
            string uri_confirmSingleForQueue6=null;  //用来存放passengerTicketStr 前半段 2+6+3
            

            
            for (int i = 0; i < ChengKeInfo.Count; i++)
            {
                switch (HuoChePiaoInfo.seat_type)
                {
                    case "9": uri_confirmSingleForQueue6 = "9,0,1,"; break;
                    case "M": uri_confirmSingleForQueue6 = "M,0,1,"; break;
                    case "O": uri_confirmSingleForQueue6 = "O,0,1,"; break;
                    case "2": uri_confirmSingleForQueue6 = "2,0,1,"; break;
                    case "1": uri_confirmSingleForQueue6 = "1,0,1,"; break;
                    case "4": uri_confirmSingleForQueue6 = "4,0,1,"; break;
                    case "3": uri_confirmSingleForQueue6 = "3,0,1,"; break;
                    case "F": uri_confirmSingleForQueue6 = "F,0,1,"; break;
                    default: break;
                }
                if (ChengKeInfo.Count == 1 || i == ChengKeInfo.Count - 1)
                {
                    uri_confirmSingleForQueue3 = ChengKeInfo[i].Passenger_Name + "," + ChengKeInfo[i].Passenger_Id_Type_Code + "," + ChengKeInfo[i].Passenger_Id_No + "," + ChengKeInfo[i].Mobile_No + ",N," + ChengKeInfo[i].AllEncStr;
                }
                else
                {
                    uri_confirmSingleForQueue3 = ChengKeInfo[i].Passenger_Name + "," + ChengKeInfo[i].Passenger_Id_Type_Code + "," + ChengKeInfo[i].Passenger_Id_No + "," + ChengKeInfo[i].Mobile_No + ",N," + ChengKeInfo[i].AllEncStr + "_";

                }
                uri_confirmSingleForQueue2 += uri_confirmSingleForQueue6 + uri_confirmSingleForQueue3;

            }
            for (int i = 0; i < ChengKeInfo.Count; i++)
            {
                uri_confirmSingleForQueue4 += ChengKeInfo[i].Passenger_Name + "," + ChengKeInfo[i].Passenger_Id_Type_Code + "," + ChengKeInfo[i].Passenger_Id_No + ",1_";

            }

            uri_confirmSingleForQueue5 = "&purpose_codes=00&key_check_isChange=" + key_check_isChange + "&leftTicketStr=" + HuoChePiaoInfo.leftTicket + "&train_location=" + HuoChePiaoInfo.train_location + "&choose_seats=&seatDetailType=000&is_jy=N&is_cj=Y&encryptedData=&whatsSelect=1&roomType=00&dwAll=N&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
            string uri_confirmSingleForQueue = uri_confirmSingleForQueue1 + uri_confirmSingleForQueue2  + uri_confirmSingleForQueue4 + uri_confirmSingleForQueue5;
            Thread.Sleep(200);
            html_confirmSingleForQueue = this.post(uri_confirmSingleForQueue);
            JObject obj_confirmSingleForQueue = (JObject)JsonConvert.DeserializeObject(html_confirmSingleForQueue);//将刚才一大串字符串转换成一个大对象
            var status2 = obj_confirmSingleForQueue["status"].ToString();
            var subsubmitStatus = obj_confirmSingleForQueue["data"]["submitStatus"].ToString();
            if (status2 != "True") {return"确认订单错误，正在重试..."; }
            else { return "确认订单成功，正在查询购票结果..."; }

           

            #endregion
        }

        //等待购票结果
        public string post_queryOrderWaitTime(string globalRepeatSubmitToken)
        {
            #region 等待购票结果
            string orderId = null;
            string html_queryOrderWaitTime = null;
            
            while (true)
            {
                string uri_queryOrderWaitTime = "https://kyfw.12306.cn/otn/confirmPassenger/queryOrderWaitTime?random=" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 4.ToString() + "&tourFlag=dc&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
                html_queryOrderWaitTime = this.post(uri_queryOrderWaitTime);
                JObject obj_queryOrderWaitTime = (JObject)JsonConvert.DeserializeObject(html_queryOrderWaitTime);//将刚才一大串字符串转换成一个大对象
                orderId = obj_queryOrderWaitTime["data"]["orderId"].ToString();
                int waitTime = Convert.ToInt32(obj_queryOrderWaitTime["waitTime"]);
                if (orderId != null) { return orderId; }
                Thread.Sleep(waitTime);
            }
           

            #endregion
        }

        //确认订票结果
        public bool post_resultOrderForDcQueue(string orderId,string globalRepeatSubmitToken)
        {
            bool GouPiaoChengGong = false;
            string html_resultOrderForDcQueue = null;
            #region 获取订单结果
           
            string uri_resultOrderForDcQueue = "https://kyfw.12306.cn/otn/confirmPassenger/resultOrderForDcQueue?orderSequence_no=" + orderId + "&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
            html_resultOrderForDcQueue = this.post(uri_resultOrderForDcQueue);
            JObject obj_resultOrderForDcQueue = (JObject)JsonConvert.DeserializeObject(html_resultOrderForDcQueue);//将刚才一大串字符串转换成一个大对象
            var obj_isAsync = obj_resultOrderForDcQueue["data"]["isAsync"].ToString();
            if (obj_isAsync == "1") { MessageBox.Show("购票成功"); return GouPiaoChengGong = true; }
            else return GouPiaoChengGong;
           
            #endregion
        }

    }
    
}
