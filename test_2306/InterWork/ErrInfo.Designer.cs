namespace test_2306.InterWork
{
    partial class ErrInfo
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
            this.textBox_ErrInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_ErrInfo
            // 
            this.textBox_ErrInfo.Location = new System.Drawing.Point(0, 1);
            this.textBox_ErrInfo.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.textBox_ErrInfo.Multiline = true;
            this.textBox_ErrInfo.Name = "textBox_ErrInfo";
            this.textBox_ErrInfo.Size = new System.Drawing.Size(243, 449);
            this.textBox_ErrInfo.TabIndex = 0;
            // 
            // ErrInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 457);
            this.Controls.Add(this.textBox_ErrInfo);
            this.Location = new System.Drawing.Point(640, 240);
            this.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Name = "ErrInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ErrInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_ErrInfo;
    }
}