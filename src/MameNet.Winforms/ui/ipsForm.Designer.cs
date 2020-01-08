namespace ui
{
    partial class ipsForm
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
            this.cbCht = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.listViewControl1 = new ui.ListViewControl();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbCht
            // 
            this.cbCht.FormattingEnabled = true;
            this.cbCht.Location = new System.Drawing.Point(12, 286);
            this.cbCht.Name = "cbCht";
            this.cbCht.Size = new System.Drawing.Size(230, 20);
            this.cbCht.TabIndex = 11;
            this.cbCht.SelectedIndexChanged += new System.EventHandler(this.cbCht_SelectedIndexChanged);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(248, 286);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(70, 21);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // listViewControl1
            // 
            this.listViewControl1.Location = new System.Drawing.Point(6, 16);
            this.listViewControl1.Name = "listViewControl1";
            this.listViewControl1.Size = new System.Drawing.Size(338, 264);
            this.listViewControl1.TabIndex = 10;
            // 
            // btnSwitch
            // 
            this.btnSwitch.Location = new System.Drawing.Point(135, 396);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(70, 21);
            this.btnSwitch.TabIndex = 15;
            this.btnSwitch.Text = "switch";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(350, 16);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(220, 200);
            this.tbResult.TabIndex = 16;
            // 
            // ipsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 484);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbCht);
            this.Controls.Add(this.listViewControl1);
            this.Name = "ipsForm";
            this.Text = "IPS";
            this.Load += new System.EventHandler(this.ipsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewControl listViewControl1;
        public System.Windows.Forms.ComboBox cbCht;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.TextBox tbResult;
    }
}