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
using test_2306.InterWork;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Threading;

namespace test_2306.windows
{
    public partial class DengLu : Form
    {
        //存放初始界面的数据
        Form1 frm;
        //存放验证码界面数据
        YanZhengMa YanZhengMa2;

        public DengLu()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        public DengLu(Form1 frm)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.frm = frm;
        }

        public inter inter1=null;
        
        private void DengLu_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            textBox_text_DengLuTiShi.Visible = false;
            

            inter1 = frm.inter_Form1;
        }

        private  void ZhangHaoDengLu_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            textBox_text_DengLuTiShi.Visible = false;

        }

        private  void ErWeiMaDengLu_Click(object sender, EventArgs e)
        {
            textBox_text_DengLuTiShi.Text = "请使用12306app扫描二维码登陆";
            string uuid=null;
            try {
                var html2 = inter1.post("https://kyfw.12306.cn/passport/web/create-qr64?appid=otn");
                JObject obj1 = (JObject)JsonConvert.DeserializeObject(html2);//将刚才一大串字符串转换成一个大对象
                string image_1 = obj1["image"].ToString();
                uuid = obj1["uuid"].ToString();
                //将base64编码转换为图片用pictureBox显示。
                String inputStr = image_1;
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                pictureBox1.Image = bmp;
                pictureBox1.Visible = true;
                textBox_text_DengLuTiShi.Visible = true;
            }
            catch { MessageBox.Show("二维码获取失败，请重新获取或更换登陆方式"); }
            

            string html_checkqr = null;

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        string uri_checkqr = "https://kyfw.12306.cn/passport/web/checkqr?uuid=" + uuid + "&appid=otn";
                        html_checkqr = inter1.post(uri_checkqr);
                        JObject obj_checkqr = (JObject)JsonConvert.DeserializeObject(html_checkqr);//将刚才一大串字符串转换成一个大对象
                        string result_code = obj_checkqr["result_code"].ToString();
                        if (result_code == "2")
                        {
                            string result_message = obj_checkqr["result_message"].ToString();
                            MessageBox.Show(result_message);
                            string uamtk = obj_checkqr["uamtk"].ToString();
                            break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("验证码验证出错");
                    }

                    Thread.Sleep(1000);
                }
                var html_data = "";
                string uri_uamtk = "https://kyfw.12306.cn/passport/web/auth/uamtk?appid=otn";
                var html_uamtk = inter1.post(uri_uamtk);
                html_data = html_uamtk;
                JObject obj_uamtk = (JObject)JsonConvert.DeserializeObject(html_uamtk);//将刚才一大串字符串转换成一个大对象
                string result_code1 = obj_uamtk["result_code"].ToString() ?? "";
                string newapptk1 = obj_uamtk["newapptk"].ToString() ?? "";

                string uri_uamauthclient = "https://kyfw.12306.cn/otn/uamauthclient?tk=" + newapptk1;
                var html_uamauthclient = inter1.post1(uri_uamauthclient);
                html_data = html_uamauthclient;
                JObject obj_uamauthclient = (JObject)JsonConvert.DeserializeObject(html_uamauthclient);//将刚才一大串字符串转换成一个大对象
                string result_code2 = obj_uamauthclient["result_code"].ToString();
                string result_message2 = obj_uamauthclient["result_message"].ToString();
                string result_username = obj_uamauthclient["username"].ToString();
                string apptk = obj_uamauthclient["apptk"].ToString();
                MessageBox.Show(result_username + "\r\n" + result_message2);
                frm.button_ZhuXiao.Visible = true;
                frm.DengLu.Visible = false;
                this.Close();
            });
            thread.Start();
            
            
                      
        }

        //用来接收验证码
        public string randCode = string.Empty;
        private  void button_ZhangHaoDengLu_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                string html_err = null;
                int Err_CiShu = 3;
                try
                {
                    textBox_text_DengLuTiShi.Text = "正在登陆，请稍后...";
                    string url = "https://kyfw.12306.cn/passport/web/checkLoginVerify?username=" + textBox_ZhangHaoMing.Text.ToString() + "&appid=otn";
                    string html_2 = inter1.post(url);
                    //MessageBox.Show(html_2);

                    while (true)
                    {

                        //获取手机验证码
                        string uri = "https://kyfw.12306.cn/passport/web/getMessageCode?appid=otn&username=" + textBox_ZhangHaoMing.Text + "&castNum=" + textBox_ShenFenZheng.Text;
                        string html = string.Empty;
                        html = inter1.post(uri);
                        
                        JObject obj1 = (JObject)JsonConvert.DeserializeObject(html);//将刚才一大串字符串转换成一个大对象
                        string result_code = obj1["result_code"].ToString();
                        string result_message = obj1["result_message"].ToString();
                        html_err= result_message;
                        if (result_code == "0")
                        {
                            var form3 = new YanZhengMa(this);
                            YanZhengMa2 = form3;
                            form3.ShowDialog();
                            break;
                        }
                        Err_CiShu++;
                        if(Err_CiShu > 3) { MessageBox.Show(html_err);return; }
                    }

                    //登陆网站
                    string uri2 = "https://kyfw.12306.cn/passport/web/login?sessionId=&sig=&if_check_slide_passcode_token=&scene=&checkMode=0&randCode=" + randCode + "&username=" + textBox_ZhangHaoMing.Text + "&password=" + textBox_MiMa.Text + "&appid=otn";
                    var html2 = inter1.post(uri2);
                    html_err = html2;
                    JObject obj2 = (JObject)JsonConvert.DeserializeObject(html2);//将刚才一大串字符串转换成一个大对象
                    if (obj2["uamtk"] == null) MessageBox.Show("登陆出错\r\n" + html2);
                    string uamtk = obj2["uamtk"].ToString();
                    string uri3 = "https://kyfw.12306.cn/passport/web/auth/uamtk?appid=otn";
                    var html3 = inter1.post(uri3);
                    html_err = html3;
                    JObject obj3 = (JObject)JsonConvert.DeserializeObject(html3);//将刚才一大串字符串转换成一个大对象
                    if (obj3["newapptk"] == null) MessageBox.Show("登陆出错\r\n" + html3);
                    string newapptk = obj3["newapptk"].ToString();
                    string uri4 = "https://kyfw.12306.cn/otn/uamauthclient?tk=" + newapptk;
                    string html4 = inter1.post1(uri4);
                    html_err = html4;
                    JObject obj4 = (JObject)JsonConvert.DeserializeObject(html4);//将刚才一大串字符串转换成一个大对象
                    string result4_message = obj4["result_message"].ToString();
                    string username = obj4["username"].ToString();
                    MessageBox.Show("用户名：" + username + "\r\n" + "信息：" + result4_message);
                    this.Close();
                    frm.button_ZhuXiao.Visible = true;
                    frm.DengLu.Visible = false;
                }
                catch
                {
                    MessageBox.Show("登陆出错，请重新登陆");
                    MessageBox.Show(html_err);
                    textBox_text_DengLuTiShi.Clear();
                    this.Close();
                }
                
            });
            thread.Start();




            
        }

       
    }
}
