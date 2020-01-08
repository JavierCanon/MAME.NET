namespace ui
{
    partial class cheatForm
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
            this.btnLockF = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.listViewControl1 = new ui.ListViewControl();
            this.btnLockS = new System.Windows.Forms.Button();
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
            // btnLockF
            // 
            this.btnLockF.Location = new System.Drawing.Point(248, 340);
            this.btnLockF.Name = "btnLockF";
            this.btnLockF.Size = new System.Drawing.Size(70, 21);
            this.btnLockF.TabIndex = 13;
            this.btnLockF.Text = "lock frm";
            this.btnLockF.UseVisualStyleBackColor = true;
            this.btnLockF.Click += new System.EventHandler(this.btnLockF_Click);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(350, 16);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(220, 200);
            this.tbResult.TabIndex = 14;
            // 
            // listViewControl1
            // 
            this.listViewControl1.Location = new System.Drawing.Point(6, 16);
            this.listViewControl1.Name = "listViewControl1";
            this.listViewControl1.Size = new System.Drawing.Size(338, 264);
            this.listViewControl1.TabIndex = 10;
            // 
            // btnLockS
            // 
            this.btnLockS.Location = new System.Drawing.Point(248, 313);
            this.btnLockS.Name = "btnLockS";
            this.btnLockS.Size = new System.Drawing.Size(70, 21);
            this.btnLockS.TabIndex = 13;
            this.btnLockS.Text = "lock sec";
            this.btnLockS.UseVisualStyleBackColor = true;
            this.btnLockS.Click += new System.EventHandler(this.btnLockS_Click);
            // 
            // cheatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.btnLockS);
            this.Controls.Add(this.btnLockF);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbCht);
            this.Controls.Add(this.listViewControl1);
            this.Name = "cheatForm";
            this.Text = "Cheat";
            this.Load += new System.EventHandler(this.cheatForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.cheatForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewControl listViewControl1;
        public System.Windows.Forms.ComboBox cbCht;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnLockF;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnLockS;
    }
}