using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using test_2306.data;
using test_2306.InterWork;
using test_2306.windows;

namespace test_2306
{
    public partial class Form1 : Form
    {
        //存放登陆界面的数据
        public DengLu DengLu1;
        //存放验证码页面的数据
        public YanZhengMa YanZhengMa1;
        //存放抢票页面的数据
        public QiangPiao QiangPiao1;
        //存放绑定邮箱的flag
        public bool BangDingFlag=false;
        //存放绑定邮箱页面的数据
        public BangDingYouXiangForm BangDingYouXiangForm1;
        //要绑定的邮箱名
        public string YouXiang;
        //要绑定的邮箱的授权码/密码
        public string YouXiangMiMa;

        //用来存放账户中乘车人的信息；
        public List<ChengKe> List_ChengKes=new List<ChengKe>();

        //用来存储不同城市对应的编码的字典
        public  Dictionary<string, string> ZhanTai = new Dictionary<string, string>();
        //用来存储不同编码对应的城市的字典
        public Dictionary<string, string> BianMa = new Dictionary<string, string>();
        ////用来存放每一列火车的信息
        //Dictionary<string, string> HuoChePiaoInfo = new Dictionary<string, string>();
        //创建cookie
        public CookieContainer cookieContainer = new CookieContainer();
        public inter inter_Form1;
        public Form1()
        {
            InitializeComponent();
        }

       
        private void DengLu_Click(object sender, EventArgs e)
        {
            var form3 = new DengLu(this);
            DengLu1 = form3;
            form3.ShowDialog();
                   
        }

        
        
        
        private void Form1_Load(object sender, EventArgs e)
        {
            inter_Form1 = new inter(cookieContainer);
            //获取站台对应编码
            string uri = "https://kyfw.12306.cn/otn/resources/js/framework/station_name.js?station_version=1.9297";
            

            string html_DiZhiBianMa = string.Empty;
            html_DiZhiBianMa =inter_Form1.post(uri);
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
           
            button_ZhuXiao.Visible = false;
        }

        private void button_ZhuXiao_Click(object sender, EventArgs e)
        {
            string uri_LogOut = "https://kyfw.12306.cn/otn/login/loginOut";
            var html_LogOut = inter_Form1.post(uri_LogOut);
            MessageBox.Show("已注销");
            button_ZhuXiao.Visible=false;
            DengLu.Visible = true;
        }

        private void button_QiangPiao_Click(object sender, EventArgs e)
        {
            try
            {
                string uri_query = "https://kyfw.12306.cn/otn/passengers/query?pageIndex=1&pageSize=10";
                string html_query = inter_Form1.post(uri_query);
                JObject obj_query = (JObject)JsonConvert.DeserializeObject(html_query);//将刚才一大串字符串转换成一个大对象
                var normal_passengers = obj_query["data"]["datas"];

                foreach (var passenger in normal_passengers)
                {
                    ChengKe ck = new ChengKe();
                    ck.Passenger_Name = passenger["passenger_name"].ToString();
                    ck.Passenger_Id_Type_Code = passenger["passenger_id_type_code"].ToString();
                    ck.Passenger_Id_No = passenger["passenger_id_no"].ToString();
                    ck.Mobile_No = passenger["mobile_no"].ToString();
                    ck.AllEncStr = passenger["allEncStr"].ToString();
                    List_ChengKes.Add(ck);
                }
                QiangPiao1 = new QiangPiao(this);
                QiangPiao1.ShowDialog();
            }catch
            {
                MessageBox.Show("请登陆后再抢票");
            }
           

        }

        private void button_BangDingYouXiang_Click(object sender, EventArgs e)
        {
            BangDingYouXiangForm1 = new BangDingYouXiangForm(this);
            
            BangDingYouXiangForm1.ShowDialog();
        }
    }
}
