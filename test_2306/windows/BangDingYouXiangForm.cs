using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_2306.windows
{
    public partial class BangDingYouXiangForm : Form
    {
        Form1 frm;
        public BangDingYouXiangForm()
        {
            InitializeComponent();
        }
        public BangDingYouXiangForm(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void button_BangDing_Click(object sender, EventArgs e)
        {
            frm.YouXiang=textBox_YouXiang.Text;
            frm.YouXiangMiMa=textBox_MiMa.Text;
            frm.BangDingFlag=true;
            frm.button_BangDingYouXiang.Text = "修改绑定邮箱";
            this.Close();

        }
    }
}
