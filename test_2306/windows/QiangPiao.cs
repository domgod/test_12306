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
        //12306账号中的乘车人
        List<ChengKe> List_ChengKes = new List<ChengKe>();
        //购票成功标志
        bool GouPiaoChengGong=false;

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
        //存放选择的乘车人
        List<ChengKe> ChengKeSureInfos = new List<ChengKe>();

        //车票类型
        static string[] seat_types = new string[] {"商务座","一等座", "二等座", "软座","硬座与无座","高级软卧","软卧","动卧","硬卧" };
        //车票类型编码
        string[] SeatTypes_BianMa = new string[] { "9", "M", "O", "2", "1", "6", "4", "F", "3" };
        //存储选择的火车票类型
        List<string> SeatTypesSure = new List<string>();

        private void button_QiangPiao_Click(object sender, EventArgs e)
        {
            
            //获取选择的乘车人
            for (int i = 0; i < checkedListBox_ChengKe.Items.Count; i++)
            {
                if (checkedListBox_ChengKe.GetItemChecked(i))
                {
                    ChengKeSureInfos.Add(List_ChengKes[i]);
                }
            }
            int seatTypeSureCount = 0;
            for (int j = 0; j < checkedListBox_SeatType.Items.Count; j++)
            {
                if (checkedListBox_SeatType.GetItemChecked(j))
                {
                    SeatTypesSure.Add(SeatTypes_BianMa[j]);
                    seatTypeSureCount++;
                }
            }
            if (seatTypeSureCount == 0)
            {
                foreach(var st in SeatTypes_BianMa)
                {
                    SeatTypesSure.Add(st);
                }
            }
            
            #region 获取火车票信息
            string FormAddress = frm.ZhanTai[textBox_ChuFaDi.Text];
            string ToAddress = frm.ZhanTai[textBox_MuDiDi.Text];
            string Time = dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd");
            List_HuoChePiao = frm.inter_Form1.post_queryE(FormAddress, ToAddress, Time);

            #endregion
            
            while (!GouPiaoChengGong)
            {
                //每一趟火车都尝试买
                for(int i = 0;i<List_HuoChePiao.Count;i++)
                {   
                    //当前火车信息
                    HuoChePiao HuoCheInfo = List_HuoChePiao[i];
                    //判断当前火车票能否预定
                    if (HuoCheInfo.KeFouYuDing == "NO") continue;
                    
                    
                    //开始买票
                    for(int j = 0;j<SeatTypesSure.Count;j++)
                    {
                        HuoCheInfo.SetSeat_Type(SeatTypesSure[j]);
                        
                        #region 查询登陆状态
                        frm.inter_Form1.post_checkUser();
                        #endregion
                        #region 预定火车票
                        string SecretStr = HuoCheInfo.BianMa;
                        string Train_ToDate = dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd");
                        string Train_BackDate = dateTimePicker_FanHuiShiJian.Value.ToString("yyyy-MM-dd");
                        string FromStationName = textBox_ChuFaDi.Text;
                        string ToStationName = textBox_MuDiDi.Text;
                        string BedLevelInfo = HuoCheInfo.Bed_Level_Info;
                        string SeatDiscountInfo = HuoCheInfo.Seat_Discount_Info;
                        string htmlinfo= frm.inter_Form1.post_submitOrderRequest(SecretStr, Train_ToDate, Train_BackDate, FromStationName, ToStationName, BedLevelInfo, SeatDiscountInfo);
                        textBox_XinXiTiShi.AppendText(DateTime.Now.ToString()+ htmlinfo+"\r\n");
                        #endregion
                        #region 订单初始化 
                        string[] info = frm.inter_Form1.post_initDc();
                        globalRepeatSubmitToken = info[0];
                        key_check_isChange = info[1];

                        #endregion
                        #region 获取12306账户中已经有的乘车人信息
                        frm.inter_Form1.post_getPassengerDTOs(globalRepeatSubmitToken);
                        #endregion
                        #region 检查乘车人信息
                        frm.inter_Form1.post_checkorderInfo(HuoCheInfo,ChengKeSureInfos, globalRepeatSubmitToken);
                        #endregion
                        #region 提交订单，获取订单实际情况
                        string FromAddressBianMa = frm.ZhanTai[textBox_ChuFaDi.Text];
                        string ToAddressBianMa = frm.ZhanTai[textBox_MuDiDi.Text];
                        DateTime ToTime = dateTimePicker_ChuFaShiJian.Value;
                        string htmlinfo1= frm.inter_Form1.post_getQueueCount(HuoCheInfo, FromAddressBianMa, ToAddressBianMa, ToTime, globalRepeatSubmitToken);
                        textBox_XinXiTiShi.AppendText(DateTime.Now.ToString() + htmlinfo1+"\r\n");
                        #endregion
                        #region 确认订单
                        frm.inter_Form1.post_confirmSingleForQueue(HuoCheInfo,ChengKeSureInfos,key_check_isChange,globalRepeatSubmitToken);
                        #endregion
                        #region 等待购票结果
                       string orderId= frm.inter_Form1.post_queryOrderWaitTime(globalRepeatSubmitToken);
                        #endregion
                        #region 确认购票结果
                        GouPiaoChengGong= frm.inter_Form1.post_resultOrderForDcQueue(orderId, globalRepeatSubmitToken);
                        if (GouPiaoChengGong) { textBox_XinXiTiShi.AppendText(DateTime.Now.ToString() + "：购票成功...\r\n");break; }
                        else textBox_XinXiTiShi.AppendText(DateTime.Now.ToString() + "购票失败，重新尝试中...\r\n");
                        #endregion
                        Thread.Sleep(200);
                    }



                }

            }


            #region 不用的框架

            #region 获取火车票信息
            //string FormAddress = frm.ZhanTai[textBox_ChuFaDi.Text];
            //string ToAddress = frm.ZhanTai[textBox_MuDiDi.Text];
            //string Time = dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd");
            //List_HuoChePiao= frm.inter_Form1.post_queryE(FormAddress,ToAddress,Time);

            #endregion

            #region 查询登陆状态
            //frm.inter_Form1.post_checkUser();
            #endregion

            #region 预定火车票
            //string SecretStr = List_HuoChePiao[0].BianMa;
            //string Train_ToDate = dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd");
            //string Train_BackDate = dateTimePicker_FanHuiShiJian.Value.ToString("yyyy-MM-dd");
            //string FromStationName = textBox_ChuFaDi.Text;
            //string ToStationName = textBox_MuDiDi.Text;
            //string BedLevelInfo = List_HuoChePiao[0].Bed_Level_Info;
            //string SeatDiscountInfo = List_HuoChePiao[0].Seat_Discount_Info;
            //frm.inter_Form1.post_submitOrderRequest(SecretStr, Train_ToDate, Train_BackDate, FromStationName, ToStationName, BedLevelInfo, SeatDiscountInfo);

            #endregion

            #region 订单初始化 
            //string[] info = frm.inter_Form1.post_initDc();
            //globalRepeatSubmitToken = info[0];
            //key_check_isChange = info[1];

            #endregion

            #region 获取12306账户中已经有的乘车人信息
            //frm.inter_Form1.post_getPassengerDTOs(globalRepeatSubmitToken);
            #endregion

            #region 检查选票人信息

            //string uri_checkOrderInfo_1 = "https://kyfw.12306.cn/otn/confirmPassenger/checkOrderInfo?cancel_flag=2&bed_level_order_num=000000000000000000000000000000";
            //string uri_checkOrderInfo_2 = null;
            //string uri_checkOrderInfo_3 = null;
            //string uri_checkOrderInfo_4 = null;
            //string uri_checkorderInfo_5 = null;

            //try
            //{
            //    if (List_HuoChePiao[0].CheCi.Substring(0, 1) == "G")
            //    {
            //        uri_checkOrderInfo_2 = "&passengerTicketStr=9,0,1,";

            //    }
            //    else
            //    {
            //        uri_checkOrderInfo_2 = "&passengerTicketStr=1,0,1,";
            //    }
            //    for (int i = 0; i < checkedListBox_ChengKe.Items.Count; i++)
            //    {
            //        if (checkedListBox_ChengKe.GetItemChecked(i))
            //        {
            //            uri_checkOrderInfo_3 = List_ChengKes[i].Passenger_Name + "," + List_ChengKes[i].Passenger_Id_Type_Code + "," + List_ChengKes[i].Passenger_Id_No + "," + List_ChengKes[i].Mobile_No + ",N," + List_ChengKes[i].AllEncStr;
            //            uri_checkOrderInfo_4 = "&oldPassengerStr=" + List_ChengKes[i].Passenger_Name + "," + List_ChengKes[i].Passenger_Id_Type_Code + "," + List_ChengKes[i].Passenger_Id_No + ",1_";
            //        }
            //    }
            //    uri_checkorderInfo_5 = "&tour_flag=dc&whatsSelect=1&sessionId=&sig=&scene=nc_login&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
            //    string uri_checkorderInfo = uri_checkOrderInfo_1 + uri_checkOrderInfo_2 + uri_checkOrderInfo_3 + uri_checkOrderInfo_4 + uri_checkorderInfo_5;
            //    Thread.Sleep(3500);
            //    var html_checkorderInfo = frm.inter_Form1.post1(uri_checkorderInfo);
            //    JObject obj_checkorderInfo = (JObject)JsonConvert.DeserializeObject(html_checkorderInfo);//将刚才一大串字符串转换成一个大对象
            //    var status1 = obj_checkorderInfo["status"].ToString();
            //    if (status1 != "True") { MessageBox.Show("检查选票人错误，请重试"); return; }
            //}
            //catch
            //{
            //    MessageBox.Show("检查选票人信息时出错，请检查");

            //    return;
            //}

            #endregion

            #region 提交订单，获取订单实际情况
            //不知道访问这个有没有用
            //string uri_oynqujj = "https://kyfw.12306.cn/otn/resources/route/kyfw.12306.cn/otn/confirmPassenger/initDc.js";
            //string html_oynqujj = frm.inter_Form1.post(uri_oynqujj);

            //string html_getQueueCount = null;
            //try
            //{
            //    string[] Month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            //    var S_Week = dateTimePicker_ChuFaShiJian.Value.DayOfWeek.ToString().Substring(0, 3);
            //    var S_Month = Month[dateTimePicker_ChuFaShiJian.Value.Month - 1];
            //    var S_Day = dateTimePicker_ChuFaShiJian.Value.Day.ToString();
            //    if (S_Day.Length < 2) S_Day = "0" + S_Day;
            //    var S_Year = dateTimePicker_ChuFaShiJian.Value.Year.ToString();

            //    string url_getQueueCount2 = null;
            //    string url_getQueueCount1 = "https://kyfw.12306.cn/otn/confirmPassenger/getQueueCount?train_date=" + S_Week + "+" + S_Month + "+" + S_Day + "+" + S_Year + "+00%3A00%3A00+GMT%2B0800+(%E4%B8%AD%E5%9B%BD%E6%A0%87%E5%87%86%E6%97%B6%E9%97%B4)&train_no=" + List_HuoChePiao[0].train_no + "&stationTrainCode=" + List_HuoChePiao[0].CheCi;
            //    if (List_HuoChePiao[0].CheCi.Substring(0, 1) == "G")
            //    {
            //        url_getQueueCount2 = "&seatType=9";
            //    }
            //    else
            //    {
            //        url_getQueueCount2 = "&seatType=1";
            //    }

            //    string url_getQueueCount3 = "&fromStationTelecode=" + frm.ZhanTai[textBox_ChuFaDi.Text] + "&toStationTelecode=" + frm.ZhanTai[textBox_MuDiDi.Text] + "&leftTicket=" + List_HuoChePiao[0].leftTicket + "&purpose_codes=00&train_location=" + List_HuoChePiao[0].train_location + "&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
            //    string url_getQueueCount = url_getQueueCount1 + url_getQueueCount2 + url_getQueueCount3;

            //    Cookie cook1 = new Cookie("_jc_save_toDate", DateTime.Now.ToString("yyyy-MM-dd"), "/", "kyfw.12306.cn");
            //    Cookie cook2 = new Cookie("_jc_save_fromStation", frm.ZhanTai[textBox_ChuFaDi.Text], "/", "kyfw.12306.cn");
            //    Cookie cook3 = new Cookie("_jc_save_toStation", frm.ZhanTai[textBox_MuDiDi.Text], "/", "kyfw.12306.cn");
            //    Cookie cook4 = new Cookie("_jc_save_toDate", dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd"), "/", "kyfw.12306.cn");
            //    frm.cookieContainer.Add(cook1);
            //    frm.cookieContainer.Add(cook2);
            //    frm.cookieContainer.Add(cook3);
            //    frm.cookieContainer.Add(cook4);
            //    html_getQueueCount = frm.inter_Form1.post(url_getQueueCount);
            //    JObject obj_getQueueCount = (JObject)JsonConvert.DeserializeObject(html_getQueueCount);//将刚才一大串字符串转换成一个大对象
            //    var status1 = obj_getQueueCount["status"].ToString();
            //    if (status1 != "True") { MessageBox.Show("提交订单出错，请重试"); return; }

            //}
            //catch
            //{
            //    MessageBox.Show("提交订单错误，请检查");
            //    MessageBox.Show(html_getQueueCount);
            //    return;
            //}

            #endregion

            #region 确认订单
            //string uri_confirmSingleForQueue1 = "https://kyfw.12306.cn/otn/confirmPassenger/confirmSingleForQueue?";
            //string uri_confirmSingleForQueue2 = null;
            //string uri_confirmSingleForQueue3 = null;
            //string uri_confirmSingleForQueue4 = null;
            //string uri_confirmSingleForQueue5 = null;
            //try
            //{

            //    //这个判断实际是不对的
            //    if (List_HuoChePiao[0].CheCi.Substring(0, 1) == "G")
            //    {
            //        uri_confirmSingleForQueue2 = "passengerTicketStr=9,0,1,";
            //    }
            //    else
            //    {
            //        uri_confirmSingleForQueue2 = "passengerTicketStr=1,0,1,";
            //    }

            //    for (int i = 0; i < checkedListBox_ChengKe.Items.Count; i++)
            //    {
            //        if (checkedListBox_ChengKe.GetItemChecked(i))
            //        {
            //            if (i > 0)
            //            {

            //            }
            //            else
            //            {
            //                uri_confirmSingleForQueue3 = List_ChengKes[i].Passenger_Name + "," + List_ChengKes[i].Passenger_Id_Type_Code + "," + List_ChengKes[i].Passenger_Id_No + "," + List_ChengKes[i].Mobile_No + ",N," + List_ChengKes[i].AllEncStr;
            //                uri_confirmSingleForQueue4 = "&oldPassengerStr=" + List_ChengKes[i].Passenger_Name + "," + List_ChengKes[i].Passenger_Id_Type_Code + "," + List_ChengKes[i].Passenger_Id_No + ",1_"; ;
            //            }
            //        }
            //    }
            //    uri_confirmSingleForQueue5 = "&purpose_codes=00&key_check_isChange=" + key_check_isChange + "&leftTicketStr=" + List_HuoChePiao[0].leftTicket + "&train_location=" + List_HuoChePiao[0].train_location + "&choose_seats=&seatDetailType=000&is_jy=N&is_cj=Y&encryptedData=&whatsSelect=1&roomType=00&dwAll=N&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
            //    string uri_confirmSingleForQueue = uri_confirmSingleForQueue1 + uri_confirmSingleForQueue2 + uri_confirmSingleForQueue3 + uri_confirmSingleForQueue4 + uri_confirmSingleForQueue5;
            //    Thread.Sleep(200);
            //    var html_confirmSingleForQueue = frm.inter_Form1.post(uri_confirmSingleForQueue);
            //    JObject obj_confirmSingleForQueue = (JObject)JsonConvert.DeserializeObject(html_confirmSingleForQueue);//将刚才一大串字符串转换成一个大对象
            //    var status2 = obj_confirmSingleForQueue["status"].ToString();
            //    var subsubmitStatus = obj_confirmSingleForQueue["data"]["submitStatus"].ToString();
            //    if (status2 != "True") { MessageBox.Show("确认订单错误，请重试"); }

            //    Thread.Sleep(1000);
            //}
            //catch
            //{
            //    MessageBox.Show("确认订单出错，请检查程序");
            //    return;
            //}


            #endregion

            #region 等待购票结果
            //string requestId = null;
            //string orderId = null;
            //try
            //{
            //    while (true)
            //    {
            //        string uri_queryOrderWaitTime = "https://kyfw.12306.cn/otn/confirmPassenger/queryOrderWaitTime?random=" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + 4.ToString() + "&tourFlag=dc&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
            //        var html_queryOrderWaitTime = frm.inter_Form1.post(uri_queryOrderWaitTime);
            //        JObject obj_queryOrderWaitTime = (JObject)JsonConvert.DeserializeObject(html_queryOrderWaitTime);//将刚才一大串字符串转换成一个大对象

            //        if (obj_queryOrderWaitTime["data"]["orderId"].ToString() == null) { Thread.Sleep(200); continue; }

            //        orderId = obj_queryOrderWaitTime["data"]["orderId"].ToString();
            //        int waitTime = Convert.ToInt32(obj_queryOrderWaitTime["waitTime"]);
            //        if ( orderId != null) { break; }
            //        Thread.Sleep(waitTime);
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("等待购票结果过程出错，请检查程序");
            //    return;

            //}

            #endregion

            #region 获取订单结果
            //try
            //{

            //    string uri_resultOrderForDcQueue = "https://kyfw.12306.cn/otn/confirmPassenger/resultOrderForDcQueue?orderSequence_no=" + orderId + "&_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
            //    var html_resultOrderForDcQueue = frm.inter_Form1.post(uri_resultOrderForDcQueue);
            //    JObject obj_resultOrderForDcQueue = (JObject)JsonConvert.DeserializeObject(html_resultOrderForDcQueue);//将刚才一大串字符串转换成一个大对象
            //    var status_resultOrderForDcQueue = obj_resultOrderForDcQueue["status"].ToString();
            //    if (status_resultOrderForDcQueue == "True") { MessageBox.Show("购票成功"); }
            //}
            //catch
            //{
            //    MessageBox.Show("确认订单出错，请检查程序");
            //    return;
            //}

            #endregion

            #endregion

        }


        private void QiangPiao_Load(object sender, EventArgs e)
        {
            checkedListBox_ChengKe.Items.Clear();
            checkedListBox_SeatType.Items.Clear();
            List_ChengKes = frm.List_ChengKes;
            foreach(var ChengKe in List_ChengKes)
            {
                checkedListBox_ChengKe.Items.Add(ChengKe.Passenger_Name + "\r\n");
            }
            foreach (var seattype in seat_types)
            {
                checkedListBox_SeatType.Items.Add(seattype + "\r\n");
            }
        }
    }
}
