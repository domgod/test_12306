using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_2306.data
{
    public class HuoChePiao
    {
        public string BianMa;       //火车编码
        public string train_no;      //火车号（完整）
        public String CheCi;       //车次
        public string leftTicket;
        public string train_location;
        public String ShiFaDi;     //火车始发站
        public string ZhongDian;   //火车终点站
        public string ShangCheDian;//上火车点
        public string XiaCheDian;  //下火车点
        public string ChuFaShiJian;//火车出发时间
        public string DaoDaShiJian;//火车到达时间
        public string ShiChang;    //火车到下车点需要时间
        public string KeFouYuDing; //是否可以预定
        public string RuanZuo;     //软座
        public string DongWo;      //动卧
        public string GaoJiRuanWo; //高级软卧
        public String RuanWo;      //软卧(一等座）
        public string WuZuo;       //无座
        public string YingWo;      //硬卧（二等座）
        public string YingZuo;     //硬座
        public String ErDengZuo;   //二等座（二等包座）
        public string YiDengZuo;   //一等座
        public string ShangWuZuo;  //商务座
        public string Bed_Level_Info;   
        public string Seat_Discount_Info;

        public string seat_type;  //座位类型
        #region 座位类型
        //BUSINESS_SEAT (32, "9", "A9","商务座"),
        //FIRST_SEAT    (31, "M", "M","一等座"),
        //SECOND_SEAT   (30, "O", "O","二等座"),
        //SOFT_SEAT     (24, "2", "A2","软座"),
        //HARD_SEAT     (29, "1", "A1","硬座"),
        //NONE_SEAT     (26, "1", "WZ","无座"),
        //HIGH_SOFT_SLEEP(21, "6", "A6","高级软卧"),
        //SOFT_SLEEP    (23, "4", "A4","软卧"),
        //MOTOR_SLEEP   (27, "F", "F","动卧"),
        //HARD_SLEEP    (28, "3", "A3", "硬卧");
        #endregion
        

        public HuoChePiao(string str )
        {
            try
            {
                string[] HuoChePiaos = str.Substring(5). Split('|');
                this.BianMa = HuoChePiaos[0];
                this.train_no = HuoChePiaos[2];
                this.CheCi = HuoChePiaos[3];
                this.leftTicket = HuoChePiaos[12];
                this.train_location= HuoChePiaos[15];
                this.ShiFaDi = HuoChePiaos[4];
                this.ZhongDian = HuoChePiaos[5];
                this.ShangCheDian = HuoChePiaos[6];
                this.XiaCheDian = HuoChePiaos[7];
                this.ChuFaShiJian = HuoChePiaos[8];
                this.DaoDaShiJian = HuoChePiaos[9];
                this.ShiChang = HuoChePiaos[10];
                this.KeFouYuDing= HuoChePiaos[11]=="Y"?"YES":"NO";
                this.RuanZuo = HuoChePiaos[25];
                this.DongWo = HuoChePiaos[27];
                this.GaoJiRuanWo = HuoChePiaos[21];
                this.RuanWo = HuoChePiaos[23];
                this.WuZuo = HuoChePiaos[26];
                this.YingWo = HuoChePiaos[28];
                this.YingZuo = HuoChePiaos[29];
                this.ErDengZuo = HuoChePiaos[30];
                this.YiDengZuo = HuoChePiaos[31];
                this.ShangWuZuo = HuoChePiaos[32];
                this.Bed_Level_Info = HuoChePiaos[53];
                this.Seat_Discount_Info = HuoChePiaos[54];
                
            }
            catch
            {
                MessageBox.Show("火车票信息处理出错，请处理");
            }
        }
        public HuoChePiao() { }

        public string GetSeat_Type()
        {
            return seat_type;
        }
        public void SetSeat_Type(string seattype)
        {
            seat_type=seattype;
        }

    }
}
