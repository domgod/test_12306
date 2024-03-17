namespace test_2306.windows
{
    partial class BangDingYouXiangForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_YouXiang = new System.Windows.Forms.TextBox();
            this.textBox_MiMa = new System.Windows.Forms.TextBox();
            this.button_BangDing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "邮箱：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "授权码/密码";
            // 
            // textBox_YouXiang
            // 
            this.textBox_YouXiang.Location = new System.Drawing.Point(15, 24);
            this.textBox_YouXiang.Name = "textBox_YouXiang";
            this.textBox_YouXiang.Size = new System.Drawing.Size(100, 21);
            this.textBox_YouXiang.TabIndex = 2;
            // 
            // textBox_MiMa
            // 
            this.textBox_MiMa.Location = new System.Drawing.Point(15, 78);
            this.textBox_MiMa.Name = "textBox_MiMa";
            this.textBox_MiMa.Size = new System.Drawing.Size(100, 21);
            this.textBox_MiMa.TabIndex = 3;
            // 
            // button_BangDing
            // 
            this.button_BangDing.Location = new System.Drawing.Point(30, 121);
            this.button_BangDing.Name = "button_BangDing";
            this.button_BangDing.Size = new System.Drawing.Size(69, 23);
            this.button_BangDing.TabIndex = 4;
            this.button_BangDing.Text = "绑定";
            this.button_BangDing.UseVisualStyleBackColor = true;
            this.button_BangDing.Click += new System.EventHandler(this.button_BangDing_Click);
            // 
            // BangDingYouXiangForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(133, 156);
            this.Controls.Add(this.button_BangDing);
            this.Controls.Add(this.textBox_MiMa);
            this.Controls.Add(this.textBox_YouXiang);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "BangDingYouXiangForm";
            this.Text = "BangDingYouXiangForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_YouXiang;
        private System.Windows.Forms.TextBox textBox_MiMa;
        private System.Windows.Forms.Button button_BangDing;
    }
}