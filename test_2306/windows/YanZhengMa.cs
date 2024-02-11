using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test_2306.windows;

namespace test_2306
{
    public partial class YanZhengMa : Form
    {
        DengLu frm;
        public YanZhengMa()
        {
            InitializeComponent();
        }
        public YanZhengMa(DengLu form)
        {
            InitializeComponent();
            frm = form;

        }

        private void button_YanZhengMaQueDing_Click(object sender, EventArgs e)
        {
            frm.randCode = textBox_YanZhengMa.Text;
            this.Close();
        }
    }
}
