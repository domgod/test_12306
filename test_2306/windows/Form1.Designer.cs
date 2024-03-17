namespace test_2306
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.DengLu = new System.Windows.Forms.Button();
            this.button_ZhuXiao = new System.Windows.Forms.Button();
            this.button_QiangPiao = new System.Windows.Forms.Button();
            this.button_BangDingYouXiang = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DengLu
            // 
            this.DengLu.Location = new System.Drawing.Point(24, 12);
            this.DengLu.Name = "DengLu";
            this.DengLu.Size = new System.Drawing.Size(75, 23);
            this.DengLu.TabIndex = 4;
            this.DengLu.Text = "登录";
            this.DengLu.UseVisualStyleBackColor = true;
            this.DengLu.Click += new System.EventHandler(this.DengLu_Click);
            // 
            // button_ZhuXiao
            // 
            this.button_ZhuXiao.Location = new System.Drawing.Point(24, 12);
            this.button_ZhuXiao.Name = "button_ZhuXiao";
            this.button_ZhuXiao.Size = new System.Drawing.Size(75, 23);
            this.button_ZhuXiao.TabIndex = 16;
            this.button_ZhuXiao.Text = "注销";
            this.button_ZhuXiao.UseVisualStyleBackColor = true;
            this.button_ZhuXiao.Click += new System.EventHandler(this.button_ZhuXiao_Click);
            // 
            // button_QiangPiao
            // 
            this.button_QiangPiao.Location = new System.Drawing.Point(24, 100);
            this.button_QiangPiao.Name = "button_QiangPiao";
            this.button_QiangPiao.Size = new System.Drawing.Size(75, 23);
            this.button_QiangPiao.TabIndex = 17;
            this.button_QiangPiao.Text = "抢票";
            this.button_QiangPiao.UseVisualStyleBackColor = true;
            this.button_QiangPiao.Click += new System.EventHandler(this.button_QiangPiao_Click);
            // 
            // button_BangDingYouXiang
            // 
            this.button_BangDingYouXiang.Location = new System.Drawing.Point(12, 56);
            this.button_BangDingYouXiang.Name = "button_BangDingYouXiang";
            this.button_BangDingYouXiang.Size = new System.Drawing.Size(96, 23);
            this.button_BangDingYouXiang.TabIndex = 18;
            this.button_BangDingYouXiang.Text = "绑定邮箱";
            this.button_BangDingYouXiang.UseVisualStyleBackColor = true;
            this.button_BangDingYouXiang.Click += new System.EventHandler(this.button_BangDingYouXiang_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 135);
            this.Controls.Add(this.button_BangDingYouXiang);
            this.Controls.Add(this.button_QiangPiao);
            this.Controls.Add(this.button_ZhuXiao);
            this.Controls.Add(this.DengLu);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button DengLu;
        public System.Windows.Forms.Button button_ZhuXiao;
        private System.Windows.Forms.Button button_QiangPiao;
        public System.Windows.Forms.Button button_BangDingYouXiang;
    }
}

