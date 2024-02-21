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

namespace test_2306.windows
{
    public partial class QiangPiao : Form
    {
        Form1 frm;
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
                string[] HuoCheInfos = data.Split(',');

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
                this.Close();
            }
            #endregion
            #region 查询登陆状态
            try
            {
                string uri_CheckUser = "https://kyfw.12306.cn/otn/login/checkUser?_json_att=";
                var html_CheckUser = frm.inter_Form1.post(uri_CheckUser);
                JObject obj_CheckUser = (JObject)JsonConvert.DeserializeObject(html_CheckUser);//将刚才一大串字符串转换成一个大对象
                string status = obj_CheckUser["status"].ToString();
                if (status != "true") { MessageBox.Show("系统没有登陆，请登陆"); this.Close(); }
            }
            catch
            {
                MessageBox.Show("系统登陆状态错误，请重新登陆");
               // frm.cookieContainer = null;
                this.Close();

            }

            #endregion
            #region 预定火车票
            try
            {
                string uri_submitOrderRequest = "https://kyfw.12306.cn/otn/leftTicket/submitOrderRequest?secretStr=" + List_HuoChePiao[0].BianMa + "&train_date=" + dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd") + "&back_train_date=" + dateTimePicker_FanHuiShiJian.Value.ToString("yyyy-MM-dd") + "tour_flag=dc&purpose_codes=ADULT&query_from_station_name=" + textBox_ChuFaDi + "&query_to_station_name=" + textBox_MuDiDi + "&bed_level_info=" + List_HuoChePiao[1].Bed_Level_Info + "&seat_discount_info=" + List_HuoChePiao[1].Seat_Discount_Info + "&undefined";
                var html_submitOrderRequest = frm.inter_Form1.post(uri_submitOrderRequest);
                JObject obj_submitOrderRequest = (JObject)JsonConvert.DeserializeObject(html_submitOrderRequest);//将刚才一大串字符串转换成一个大对象
                string status = obj_submitOrderRequest["status"].ToString();
                if (status != "true") { MessageBox.Show("无可用火车票，请重试");this.Close(); }
            }
            catch 
            {
                MessageBox.Show("预定火车票出错，请检查程序是否出错");
                this.Close();
            }

            #endregion
            #region 订单初始化 
            string globalRepeatSubmitToken=null;
            try
            {
                string uri_initDc = "https://kyfw.12306.cn/otn/confirmPassenger/initDc?_json_att=";
                var obj_initDc = frm.inter_Form1.post(uri_initDc);
                var index = obj_initDc.IndexOf("globalRepeatSubmitToken");
                if (index > -1) { index += 27; }
                globalRepeatSubmitToken = obj_initDc.Substring(index, 32);
            }
            catch
            {
                MessageBox.Show("订单初始化时出错，请检查");
                this.Close();
            }

            #endregion
            #region 获取12306账户中已经有的乘车人信息
            try
            {
                string uri_getPassengerDTOs = "https://kyfw.12306.cn/otn/confirmPassenger/getPassengerDTOs?_json_att=&REPEAT_SUBMIT_TOKEN=" + globalRepeatSubmitToken;
                var obj_getPassengerDTOs = frm.inter_Form1.post(uri_getPassengerDTOs);
            }
            catch
            {
                MessageBox.Show("获取乘车人信息时出错，请检查");
                this.Close();
            }
            #endregion
            #region 检查选票人信息
           // string uri_1 = "https://kyfw.12306.cn/otn/confirmPassenger/checkOrderInfo?cancel_flag=2&bed_level_order_num=000000000000000000000000000000&passengerTicketStr=";

            #endregion
        }
    }
}
