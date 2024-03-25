namespace test_2306.windows
{
    partial class QiangPiao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_QiangPiao = new System.Windows.Forms.Button();
            this.textBox_ChuFaDi = new System.Windows.Forms.TextBox();
            this.textBox_MuDiDi = new System.Windows.Forms.TextBox();
            this.dateTimePicker_FanHuiShiJian = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_ChuFaShiJian = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkedListBox_ChengKe = new System.Windows.Forms.CheckedListBox();
            this.checkedListBox_SeatType = new System.Windows.Forms.CheckedListBox();
            this.textBox_XinXiTiShi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_QiangPiao
            // 
            this.button_QiangPiao.Location = new System.Drawing.Point(296, 40);
            this.button_QiangPiao.Name = "button_QiangPiao";
            this.button_QiangPiao.Size = new System.Drawing.Size(75, 23);
            this.button_QiangPiao.TabIndex = 23;
            this.button_QiangPiao.Text = "抢票";
            this.button_QiangPiao.UseVisualStyleBackColor = true;
            this.button_QiangPiao.Click += new System.EventHandler(this.button_QiangPiao_Click);
            // 
            // textBox_ChuFaDi
            // 
            this.textBox_ChuFaDi.Location = new System.Drawing.Point(74, 42);
            this.textBox_ChuFaDi.Name = "textBox_ChuFaDi";
            this.textBox_ChuFaDi.Size = new System.Drawing.Size(63, 21);
            this.textBox_ChuFaDi.TabIndex = 22;
            // 
            // textBox_MuDiDi
            // 
            this.textBox_MuDiDi.Location = new System.Drawing.Point(210, 42);
            this.textBox_MuDiDi.Name = "textBox_MuDiDi";
            this.textBox_MuDiDi.Size = new System.Drawing.Size(65, 21);
            this.textBox_MuDiDi.TabIndex = 21;
            // 
            // dateTimePicker_FanHuiShiJian
            // 
            this.dateTimePicker_FanHuiShiJian.Location = new System.Drawing.Point(261, 12);
            this.dateTimePicker_FanHuiShiJian.Name = "dateTimePicker_FanHuiShiJian";
            this.dateTimePicker_FanHuiShiJian.Size = new System.Drawing.Size(110, 21);
            this.dateTimePicker_FanHuiShiJian.TabIndex = 20;
            // 
            // dateTimePicker_ChuFaShiJian
            // 
            this.dateTimePicker_ChuFaShiJian.Location = new System.Drawing.Point(74, 12);
            this.dateTimePicker_ChuFaShiJian.Name = "dateTimePicker_ChuFaShiJian";
            this.dateTimePicker_ChuFaShiJian.Size = new System.Drawing.Size(113, 21);
            this.dateTimePicker_ChuFaShiJian.TabIndex = 19;
            this.dateTimePicker_ChuFaShiJian.ValueChanged += new System.EventHandler(this.dateTimePicker_ChuFaShiJian_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "起始地点";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "目标地点";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(206, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "返回时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "出发时间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "选择乘客";
            // 
            // checkedListBox_ChengKe
            // 
            this.checkedListBox_ChengKe.FormattingEnabled = true;
            this.checkedListBox_ChengKe.Location = new System.Drawing.Point(20, 96);
            this.checkedListBox_ChengKe.Name = "checkedListBox_ChengKe";
            this.checkedListBox_ChengKe.Size = new System.Drawing.Size(86, 308);
            this.checkedListBox_ChengKe.TabIndex = 26;
            // 
            // checkedListBox_SeatType
            // 
            this.checkedListBox_SeatType.FormattingEnabled = true;
            this.checkedListBox_SeatType.Location = new System.Drawing.Point(119, 96);
            this.checkedListBox_SeatType.Name = "checkedListBox_SeatType";
            this.checkedListBox_SeatType.Size = new System.Drawing.Size(90, 308);
            this.checkedListBox_SeatType.TabIndex = 28;
            // 
            // textBox_XinXiTiShi
            // 
            this.textBox_XinXiTiShi.Location = new System.Drawing.Point(218, 71);
            this.textBox_XinXiTiShi.Multiline = true;
            this.textBox_XinXiTiShi.Name = "textBox_XinXiTiShi";
            this.textBox_XinXiTiShi.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_XinXiTiShi.Size = new System.Drawing.Size(153, 333);
            this.textBox_XinXiTiShi.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 30;
            this.label6.Text = "选择座位类型";
            // 
            // QiangPiao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 404);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_XinXiTiShi);
            this.Controls.Add(this.checkedListBox_SeatType);
            this.Controls.Add(this.checkedListBox_ChengKe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_QiangPiao);
            this.Controls.Add(this.textBox_ChuFaDi);
            this.Controls.Add(this.textBox_MuDiDi);
            this.Controls.Add(this.dateTimePicker_FanHuiShiJian);
            this.Controls.Add(this.dateTimePicker_ChuFaShiJian);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "QiangPiao";
            this.Text = "QiangPiao";
            this.Load += new System.EventHandler(this.QiangPiao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_QiangPiao;
        private System.Windows.Forms.TextBox textBox_ChuFaDi;
        private System.Windows.Forms.TextBox textBox_MuDiDi;
        private System.Windows.Forms.DateTimePicker dateTimePicker_FanHuiShiJian;
        private System.Windows.Forms.DateTimePicker dateTimePicker_ChuFaShiJian;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox checkedListBox_ChengKe;
        private System.Windows.Forms.CheckedListBox checkedListBox_SeatType;
        public System.Windows.Forms.TextBox textBox_XinXiTiShi;
        private System.Windows.Forms.Label label6;
    }
}