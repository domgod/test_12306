namespace test_2306
{
    partial class YanZhengMa
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_YanZhengMa = new System.Windows.Forms.TextBox();
            this.button_YanZhengMaQueDing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入手机验证码";
            // 
            // textBox_YanZhengMa
            // 
            this.textBox_YanZhengMa.Location = new System.Drawing.Point(76, 75);
            this.textBox_YanZhengMa.Name = "textBox_YanZhengMa";
            this.textBox_YanZhengMa.Size = new System.Drawing.Size(100, 21);
            this.textBox_YanZhengMa.TabIndex = 1;
            // 
            // button_YanZhengMaQueDing
            // 
            this.button_YanZhengMaQueDing.Location = new System.Drawing.Point(89, 120);
            this.button_YanZhengMaQueDing.Name = "button_YanZhengMaQueDing";
            this.button_YanZhengMaQueDing.Size = new System.Drawing.Size(75, 23);
            this.button_YanZhengMaQueDing.TabIndex = 2;
            this.button_YanZhengMaQueDing.Text = "确定";
            this.button_YanZhengMaQueDing.UseVisualStyleBackColor = true;
            this.button_YanZhengMaQueDing.Click += new System.EventHandler(this.button_YanZhengMaQueDing_Click);
            // 
            // YanZhengMa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 226);
            this.Controls.Add(this.button_YanZhengMaQueDing);
            this.Controls.Add(this.textBox_YanZhengMa);
            this.Controls.Add(this.label1);
            this.Name = "YanZhengMa";
            this.Text = "YanZhengMa";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox_YanZhengMa;
        public System.Windows.Forms.Button button_YanZhengMaQueDing;
    }
}