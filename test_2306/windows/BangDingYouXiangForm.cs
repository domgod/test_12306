﻿using System;
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
            try
            {
                frm.YouXiang = textBox_YouXiang.Text;
                frm.YouXiangMiMa = textBox_MiMa.Text;
                frm.BangDingFlag = true;
                frm.button_BangDingYouXiang.Text = "修改绑定邮箱";
                frm.BangDingFlag = true;
                frm.inter_Form1.SendMailInfo(frm.YouXiang, frm.YouXiangMiMa, frm.YouXiang, "邮箱绑定成功!!!!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("绑定失败，请检查授权码是否正确");
            }
           

        }
    }
}
