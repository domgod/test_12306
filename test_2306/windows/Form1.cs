﻿using Newtonsoft.Json;
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

        //用来存储不同城市对应的编码的字典
        Dictionary<string, string> ZhanTai = new Dictionary<string, string>();
        Dictionary<string, string> BianMa = new Dictionary<string, string>();
        //用来存放每一列火车的信息
        Dictionary<string, string> HuoChePiaoInfo = new Dictionary<string, string>();
        //创建cookie
        CookieContainer cookieContainer = new CookieContainer();
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
            listView1.Visible = false;
            button_ZhuXiao.Visible = false;
        }

        
        private void ChaXun_Click(object sender, EventArgs e)
        {
            //清除表格残留数据
            listView1.Columns.Clear();
            listView1.Items.Clear();
            string FormAddress = ZhanTai[textBox_ChuFaDi.Text] ;
            string ToAddress = ZhanTai[textBox_MuDiDi.Text];
            string uri = "https://kyfw.12306.cn/otn/leftTicket/queryE?leftTicketDTO.train_date=" + dateTimePicker_ChuFaShiJian.Value.ToString("yyyy-MM-dd") + "&leftTicketDTO.from_station=" + FormAddress + "&leftTicketDTO.to_station=" + ToAddress + "&purpose_codes=ADULT";
           string html = string.Empty;
            html = inter_Form1.post(uri);
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
            textBox1.Visible = false;
            listView1.Visible = true;

        }
      

        private void textBox_ShenFenZheng_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShenFenZheng_Click(object sender, EventArgs e)
        {

        }

        private void textBox_MiMa_TextChanged(object sender, EventArgs e)
        {

        }

        private void MiMa_Click(object sender, EventArgs e)
        {

        }

        private void ZhangHao_Click(object sender, EventArgs e)
        {

        }

        private void textBox_ZhangHaoMing_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_ZhuXiao_Click(object sender, EventArgs e)
        {
            string uri_LogOut = "https://kyfw.12306.cn/otn/login/loginOut";
            var html_LogOut = inter_Form1.post(uri_LogOut);
            MessageBox.Show("已注销");
            button_ZhuXiao.Visible=false;
            DengLu.Visible = true;
        }
    }
}