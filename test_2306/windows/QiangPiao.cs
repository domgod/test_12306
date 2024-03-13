using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using test_2306.data;
using System.Threading;
using System.Net;

namespace test_2306.windows
{
    public partial class QiangPiao : Form
    {
        Form1 frm;
        List<ChengKe> List_ChengKes = new List<ChengKe>();

        string globalRepeatSubmitToken = null;
        string key_check_isChange = null;
        public QiangPiao()
        {
            InitializeComponent();
        }
        public QiangPiao(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;

        }
        //存放火车票信息
        List<HuoChePiao> List_HuoChePiao=new List<HuoChePiao>();

        private void button_QiangPiao_Click(object sender, EventArgs e)
        {
            #region 获取火车票信息
            try
            {
                string FormAddress = frm.ZhanTai[textBox_ChuFaDi.Text];
                string ToAddress = frm.ZhanTai[textBox_MuDiDi.Text];
                string uri = "https://kyfw.12306.cn/otn/leftTicket/queryE?leftTicketDTO.train_date=" + dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd") + "&leftTicketDTO.from_station=" + FormAddress + "&leftTicketDTO.to_station=" + ToAddress + "&purpose_codes=ADULT";
                string html = string.Empty;
                html = frm.inter_Form1.post(uri);
                JObject obj = (JObject)JsonConvert.DeserializeObject(html);//将刚才一大串字符串转换成一个大对象
                string data = obj["data"]["result"].ToString();
                string[] HuoCheInfos = data.Substring(1). Split(',');

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
            }
            catch
            {
                MessageBox.Show("获取火车票信息时出错，请检查");
                
                return;
            }
           
            #endregion
            #region 查询登陆状态
            try
                {
                string uri_CheckUser = "https://kyfw.12306.cn/otn/login/checkUser?_json_att=";
                var html_CheckUser = frm.inter_Form1.post(uri_CheckUser);
                JObject obj_CheckUser = (JObject)JsonConvert.DeserializeObject(html_CheckUser);//将刚才一大串字符串转换成一个大对象
                string status = obj_CheckUser["status"].ToString();
                if (status != "True") { MessageBox.Show("系统没有登陆，请登陆"); this.Close(); return; }
            }
            catch
            {
                MessageBox.Show("系统登陆状态错误，请重新登陆");
               // frm.cookieContainer = null;
                
                return;

            }

            #endregion
            #region 预定火车票
            try
            {
                string uri_submitOrderRequest = "https://kyfw.12306.cn/otn/leftTicket/submitOrderRequest?secretStr=" + List_HuoChePiao[0].BianMa + "&train_date=" + dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd") + "&back_train_date=" + dateTimePicker_FanHuiShiJian.Value.ToString("yyyy-MM-dd") + "&tour_flag=dc&purpose_codes=ADULT&query_from_station_name=" + textBox_ChuFaDi.Text + "&query_to_station_name=" + textBox_MuDiDi.Text + "&bed_level_info=" + List_HuoChePiao[0].Bed_Level_Info + "&seat_discount_info=" + List_HuoChePiao[0].Seat_Discount_Info + "&undefined";
                var html_submitOrderRequest = frm.inter_Form1.post(uri_submitOrderRequest);
                JObject obj_submitOrderRequest = (JObject)JsonConvert.DeserializeObject(html_submitOrderRequest);//将刚才一大串字符串转换成一个大对象
                string status = obj_submitOrderRequest["status"].ToString();
                if (status != "True") { MessageBox.Show("无可用火车票或预定失败，请重试");return; }
            }
            catch 
            {
                MessageBox.Show("预定火车票出错或预定失败，请检查程序是否出错");
               
                return;
            }

            #endregion
            #region 订单初始化 
            
            try
            {
                string uri_initDc = "https://kyfw.12306.cn/otn/confirmPassenger/initDc?_json_att=";
                var obj_initDc = frm.inter_Form1.post(uri_initDc);
                var index = obj_initDc.IndexOf("globalRepeatSubmitToken");
                if (index > -1) { index += 27; }
                globalRepeatSubmitToken = obj_initDc.Substring(index, 32);
                index = obj_initDc.IndexOf("key_check_isChange");
                if (index > -1) { index += 21; }
                key_check_isChange = obj_initDc.Substring(index, 56);


            }
            catch
            {
                MessageBox.Show("订单初始化时出错，请检查");
                return;
            }

            #endregion
            #region 获取12306账户中已经有的乘车人信息
            try
            {
                string uri_getPassengerDTOs = "https://kyfw.12306.cn/otn/confirmPassenger/getPassengerDTOs?_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
                var html_getPassengerDTOs = frm.inter_Form1.post(uri_getPassengerDTOs);
                JObject obj_getPassengerDTOs = (JObject)JsonConvert.DeserializeObject(html_getPassengerDTOs);//将刚才一大串字符串转换成一个大对象
                var status_getPassengerDTOs = obj_getPassengerDTOs["status"].ToString();
                if (status_getPassengerDTOs != "True") { MessageBox.Show("在订票过程中获取不到乘车人信息出错，请重试");return; }
                

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
            catch
            {
                MessageBox.Show("获取乘车人信息时出错，请检查");
                
                return;
            }
            #endregion

            #region 检查选票人信息
            string uri_checkOrderInfo_1 = "https://kyfw.12306.cn/otn/confirmPassenger/checkOrderInfo?cancel_flag=2&bed_level_order_num=000000000000000000000000000000";
            string uri_checkOrderInfo_2;
            string uri_checkOrderInfo_3 = null;
            string uri_checkOrderInfo_4 = null;
            string uri_checkorderInfo_5 = null;

            try
            {
                if (List_HuoChePiao[0].CheCi.Substring(0, 1) == "G")
                {
                    uri_checkOrderInfo_2 = "&passengerTicketStr=9,0,1,";

                }
                else
                {
                    uri_checkOrderInfo_2 = "&passengerTicketStr=1,0,1,";
                }
                for (int i = 0; i < checkedListBox_ChengKe.Items.Count; i++)
                {
                    if (checkedListBox_ChengKe.GetItemChecked(i))
                    {
                        uri_checkOrderInfo_3 = List_ChengKes[i].Passenger_Name + "," + List_ChengKes[i].Passenger_Id_Type_Code + "," + List_ChengKes[i].Passenger_Id_No + "," + List_ChengKes[i].Mobile_No + ",N," + List_ChengKes[i].AllEncStr;
                        uri_checkOrderInfo_4 = "&oldPassengerStr=" + List_ChengKes[i].Passenger_Name + "," + List_ChengKes[i].Passenger_Id_Type_Code + "," + List_ChengKes[i].Passenger_Id_No + ",1_";
                    }
                }
                uri_checkorderInfo_5 = "&tour_flag=dc&whatsSelect=1&sessionId=&sig=&scene=nc_login&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
                string uri_checkorderInfo = uri_checkOrderInfo_1 + uri_checkOrderInfo_2 + uri_checkOrderInfo_3 + uri_checkOrderInfo_4 + uri_checkorderInfo_5;
                Thread.Sleep(2000);
                var html_checkorderInfo = frm.inter_Form1.post1(uri_checkorderInfo);
                JObject obj_checkorderInfo = (JObject)JsonConvert.DeserializeObject(html_checkorderInfo);//将刚才一大串字符串转换成一个大对象
                var status1 = obj_checkorderInfo["status"].ToString();
                if (status1 != "True") { MessageBox.Show("检查选票人错误，请重试"); return; }
            }
            catch
            {
                MessageBox.Show("检查选票人信息时出错，请检查");

                return;
            }

            #endregion

            #region 提交订单，获取订单实际情况
            string uri_oynqujj = "https://kyfw.12306.cn/otn/resources/route/kyfw.12306.cn/otn/confirmPassenger/initDc.js";
            string html_oynqujj = frm.inter_Form1.post(uri_oynqujj);
            string html_getQueueCount = null;
            try
            {
                string[] Month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                var S_Week = dateTimePicker_ChuFaShiJian.Value.DayOfWeek.ToString().Substring(0, 3);
                var S_Month = Month[dateTimePicker_ChuFaShiJian.Value.Month - 1];
                var S_Day = dateTimePicker_ChuFaShiJian.Value.Day.ToString();
                if (S_Day.Length < 2) S_Day = "0" + S_Day;
                var S_Year = dateTimePicker_ChuFaShiJian.Value.Year.ToString();

                string url_getQueueCount2 = null;
                string url_getQueueCount1 = "https://kyfw.12306.cn/otn/confirmPassenger/getQueueCount?train_date=" + S_Week + "+" + S_Month + "+" + S_Day + "+" + S_Year + "+00%3A00%3A00+GMT%2B0800+(%E4%B8%AD%E5%9B%BD%E6%A0%87%E5%87%86%E6%97%B6%E9%97%B4)&train_no=" + List_HuoChePiao[0].train_no + "&stationTrainCode=" + List_HuoChePiao[0].CheCi;
                if (List_HuoChePiao[0].CheCi.Substring(0, 1) == "G")
                {
                    url_getQueueCount2 = "&seatType=9";
                }
                else
                {
                    url_getQueueCount2 = "&seatType=1";
                }

                string url_getQueueCount3 = "&fromStationTelecode=" + frm.ZhanTai[textBox_ChuFaDi.Text] + "&toStationTelecode=" + frm.ZhanTai[textBox_MuDiDi.Text] + "&leftTicket=" + List_HuoChePiao[0].leftTicket + "&purpose_codes=00&train_location=" + List_HuoChePiao[0].train_location + "&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
                string url_getQueueCount = url_getQueueCount1 + url_getQueueCount2 + url_getQueueCount3;

                Cookie cook1 = new Cookie("_jc_save_toDate", DateTime.Now.ToString("yyyy-MM-dd"), "/", "kyfw.12306.cn");
                Cookie cook2 = new Cookie("_jc_save_fromStation", frm.ZhanTai[textBox_ChuFaDi.Text], "/", "kyfw.12306.cn");
                Cookie cook3 = new Cookie("_jc_save_toStation", frm.ZhanTai[textBox_MuDiDi.Text], "/", "kyfw.12306.cn");
                Cookie cook4 = new Cookie("_jc_save_toDate", dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd"), "/", "kyfw.12306.cn");
                frm.cookieContainer.Add(cook1);
                frm.cookieContainer.Add(cook2);
                frm.cookieContainer.Add(cook3);
                frm.cookieContainer.Add(cook4);
                html_getQueueCount = frm.inter_Form1.post(url_getQueueCount);
                JObject obj_getQueueCount = (JObject)JsonConvert.DeserializeObject(html_getQueueCount);//将刚才一大串字符串转换成一个大对象
                var status1 = obj_getQueueCount["status"].ToString();
                if (status1 != "True") { MessageBox.Show("提交订单出错，请重试"); return; }

            }
            catch
            {
                MessageBox.Show("提交订单错误，请检查");
                MessageBox.Show(html_getQueueCount);
                return;
            }

            #endregion

            #region 确认订单
            string uri_confirmSingleForQueue1 = "https://kyfw.12306.cn/otn/confirmPassenger/confirmSingleForQueue?";
            string uri_confirmSingleForQueue2 = null;
            string uri_confirmSingleForQueue3 = null;
            string uri_confirmSingleForQueue4 = null;
            string uri_confirmSingleForQueue5 = null;
            try
            {
               
                //这个判断实际是不对的
                if (List_HuoChePiao[0].CheCi.Substring(0, 1) == "G")
                {
                    uri_confirmSingleForQueue2 = "passengerTicketStr=9,0,1,";
                }
                else
                {
                    uri_confirmSingleForQueue2 = "passengerTicketStr=1,0,1,";
                }

                for (int i = 0; i < checkedListBox_ChengKe.Items.Count; i++)
                {
                    if (checkedListBox_ChengKe.GetItemChecked(i))
                    {
                        if (i > 0)
                        {

                        }
                        else
                        {
                            uri_confirmSingleForQueue3 = List_ChengKes[i].Passenger_Name + "," + List_ChengKes[i].Passenger_Id_Type_Code + "," + List_ChengKes[i].Passenger_Id_No + "," + List_ChengKes[i].Mobile_No + ",N," + List_ChengKes[i].AllEncStr;
                            uri_confirmSingleForQueue4 = "&oldPassengerStr=" + List_ChengKes[i].Passenger_Name + "," + List_ChengKes[i].Passenger_Id_Type_Code + "," + List_ChengKes[i].Passenger_Id_No + ",1_"; ;
                        }
                    }
                }
                uri_confirmSingleForQueue5 = "&purpose_codes=00&key_check_isChange=" + key_check_isChange + "&leftTicketStr=" + List_HuoChePiao[0].leftTicket + "&train_location=" + List_HuoChePiao[0].train_location + "&choose_seats=&seatDetailType=000&is_jy=N&is_cj=Y&encryptedData=&whatsSelect=1&roomType=00&dwAll=N&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
                string uri_confirmSingleForQueue = uri_confirmSingleForQueue1 + uri_confirmSingleForQueue2 + uri_confirmSingleForQueue3 + uri_confirmSingleForQueue4 + uri_confirmSingleForQueue5;
                Thread.Sleep(100);
                var html_confirmSingleForQueue = frm.inter_Form1.post(uri_confirmSingleForQueue);
                JObject obj_confirmSingleForQueue = (JObject)JsonConvert.DeserializeObject(html_confirmSingleForQueue);//将刚才一大串字符串转换成一个大对象
                var status2 = obj_confirmSingleForQueue["status"].ToString();
                var subsubmitStatus = obj_confirmSingleForQueue["data"]["submitStatus"].ToString();
                if (status2 != "True") { MessageBox.Show("确认订单错误，请重试"); }
                
                Thread.Sleep(1000);
            }
            catch
            {
                MessageBox.Show("确认订单出错，请检查程序");
                return;
            }


            #endregion

            #region 等待购票结果
            string requestId = null;
            string orderId = null;
            try
            {
                while (true)
                {
                    string uri_queryOrderWaitTime = "https://kyfw.12306.cn/otn/confirmPassenger/queryOrderWaitTime?random=" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 4.ToString() + "&tourFlag=dc&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
                    var html_queryOrderWaitTime = frm.inter_Form1.post(uri_queryOrderWaitTime);
                    JObject obj_queryOrderWaitTime = (JObject)JsonConvert.DeserializeObject(html_queryOrderWaitTime);//将刚才一大串字符串转换成一个大对象

                    if (obj_queryOrderWaitTime["data"]["orderId"].ToString() == null) { Thread.Sleep(200); continue; }

                    orderId = obj_queryOrderWaitTime["data"]["orderId"].ToString();
                    int waitTime = Convert.ToInt32(obj_queryOrderWaitTime["waitTime"]);
                    if ( orderId != null) { break; }
                    Thread.Sleep(waitTime);
                }
            }
            catch
            {
                MessageBox.Show("等待购票结果过程出错，请检查程序");
                return;

            }

            #endregion

            #region 获取订单结果
            try
            {
                while(true)
                {
                    string uri_resultOrderForDcQueue = "https://kyfw.12306.cn/otn/confirmPassenger/resultOrderForDcQueue?orderSequence_no=" + orderId + "&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
                    var html_resultOrderForDcQueue = frm.inter_Form1.post(uri_resultOrderForDcQueue);
                    JObject obj_resultOrderForDcQueue = (JObject)JsonConvert.DeserializeObject(html_resultOrderForDcQueue);//将刚才一大串字符串转换成一个大对象
                    var status_resultOrderForDcQueue = obj_resultOrderForDcQueue["status"].ToString();
                    if (status_resultOrderForDcQueue == "True") break;
                    Thread.Sleep(200);
                    
                    
                }
               

            }
            catch
            {
                MessageBox.Show("确认订单出错，请检查程序");
                return;
            }

            #endregion

        }

       
        private void QiangPiao_Load(object sender, EventArgs e)
        {
            List_ChengKes = frm.List_ChengKes;
            foreach(var ChengKe in List_ChengKes)
            {
                checkedListBox_ChengKe.Items.Add(ChengKe.Passenger_Name + "\r\n");
            }
        }
    }
}
