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
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        public QiangPiao(Form1 frm)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
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
            //创建Thread对象
            Thread thread = new Thread(QiangPiao_Task);
            //设为后台线程
            thread.IsBackground = true;
            //启动线程
            thread.Start();
        }

        private void QiangPiao_Task()
        {
            //获取选择的乘车人
            for (int i = 0; i < checkedListBox_ChengKe.Items.Count; i++)
            {
                if (checkedListBox_ChengKe.GetItemChecked(i))
                {
                    ChengKeSureInfos.Add(List_ChengKes[i]);
                }
            }
            //获取需要购买的座位类型
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
                foreach (var st in SeatTypes_BianMa)
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
                for (int i = 0; i < List_HuoChePiao.Count; i++)
                {
                    //当前火车信息
                    HuoChePiao HuoCheInfo = List_HuoChePiao[i];
                    //判断当前火车票能否预定
                    if (HuoCheInfo.KeFouYuDing == "NO") continue;


                    //开始买票
                    for (int j = 0; j < SeatTypesSure.Count; j++)
                    {
                        HuoCheInfo.SetSeat_Type(SeatTypesSure[j]);

                        #region 查询登陆状态
                        bool DengLuQueRen = frm.inter_Form1.post_checkUser();
                        if (!DengLuQueRen) { this.Close(); return; }
                        #endregion
                        #region 预定火车票
                        string SecretStr = HuoCheInfo.BianMa;
                        string Train_ToDate = dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd");
                        string Train_BackDate = dateTimePicker_FanHuiShiJian.Value.ToString("yyyy-MM-dd");
                        string FromStationName = textBox_ChuFaDi.Text;
                        string ToStationName = textBox_MuDiDi.Text;
                        string BedLevelInfo = HuoCheInfo.Bed_Level_Info;
                        string SeatDiscountInfo = HuoCheInfo.Seat_Discount_Info;
                        bool obj_submitOrderRequest = frm.inter_Form1.post_submitOrderRequest(SecretStr, Train_ToDate, Train_BackDate, FromStationName, ToStationName, BedLevelInfo, SeatDiscountInfo);
                        if (obj_submitOrderRequest) { textBox_XinXiTiShi.AppendText(DateTime.Now.ToString() + $"正在预定火车票...." + "\r\n"); }
                        else { textBox_XinXiTiShi.AppendText(DateTime.Now.ToString() + "火车票预定失败，正在重新预定...." + "\r\n"); continue; }
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
                        bool obj_checkorderInfo = frm.inter_Form1.post_checkorderInfo(HuoCheInfo, ChengKeSureInfos, globalRepeatSubmitToken);
                        if (!obj_checkorderInfo) { textBox_XinXiTiShi.AppendText(DateTime.Now.ToString() + "乘客信息上传错误，请检查是否选择乘客...." + "\r\n"); return; }
                        #endregion
                        #region 提交订单，获取订单实际情况
                        string FromAddressBianMa = frm.ZhanTai[textBox_ChuFaDi.Text];
                        string ToAddressBianMa = frm.ZhanTai[textBox_MuDiDi.Text];
                        DateTime ToTime = dateTimePicker_ChuFaShiJian.Value;
                        string htmlinfo1 = frm.inter_Form1.post_getQueueCount(HuoCheInfo, FromAddressBianMa, ToAddressBianMa, ToTime, globalRepeatSubmitToken);
                        textBox_XinXiTiShi.AppendText(DateTime.Now.ToString() + htmlinfo1 + "\r\n");
                        #endregion
                        #region 确认订单
                        string obj_confirmSingleForQueue = frm.inter_Form1.post_confirmSingleForQueue(HuoCheInfo, ChengKeSureInfos, key_check_isChange, globalRepeatSubmitToken);
                        textBox_XinXiTiShi.AppendText(DateTime.Now.ToString() + obj_confirmSingleForQueue + "\r\n");
                        #endregion
                        #region 等待购票结果
                        string orderId = frm.inter_Form1.post_queryOrderWaitTime(globalRepeatSubmitToken);
                        #endregion
                        #region 确认购票结果
                        GouPiaoChengGong = frm.inter_Form1.post_resultOrderForDcQueue(orderId, globalRepeatSubmitToken);
                        if (GouPiaoChengGong) { textBox_XinXiTiShi.AppendText(DateTime.Now.ToString() + "：购票成功...\r\n"); break; }
                        else textBox_XinXiTiShi.AppendText(DateTime.Now.ToString() + "购票失败，重新尝试中...\r\n");
                        #endregion
                        Thread.Sleep(200);
                    }

                }

            }
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

        private void dateTimePicker_ChuFaShiJian_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker_FanHuiShiJian.Value = dateTimePicker_ChuFaShiJian.Value;
        }
    }
}
