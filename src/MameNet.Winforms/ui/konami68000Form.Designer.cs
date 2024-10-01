namespace ui
{
    partial class konami68000Form
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbT0 = new System.Windows.Forms.CheckBox();
            this.cbT1 = new System.Windows.Forms.CheckBox();
            this.cbT2 = new System.Windows.Forms.CheckBox();
            this.cbSprite = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbSprite = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(133, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 256);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(12, 201);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(70, 21);
            this.btnDraw.TabIndex = 1;
            this.btnDraw.Text = "draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLocation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 463);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(683, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslLocation
            // 
            this.tsslLocation.Name = "tsslLocation";
            this.tsslLocation.Size = new System.Drawing.Size(17, 17);
            this.tsslLocation.Text = "...";
            // 
            // cbT0
            // 
            this.cbT0.AutoSize = true;
            this.cbT0.Location = new System.Drawing.Point(12, 41);
            this.cbT0.Name = "cbT0";
            this.cbT0.Size = new System.Drawing.Size(36, 16);
            this.cbT0.TabIndex = 3;
            this.cbT0.Text = "t0";
            this.cbT0.UseVisualStyleBackColor = true;
            // 
            // cbT1
            // 
            this.cbT1.AutoSize = true;
            this.cbT1.Location = new System.Drawing.Point(12, 63);
            this.cbT1.Name = "cbT1";
            this.cbT1.Size = new System.Drawing.Size(36, 16);
            this.cbT1.TabIndex = 3;
            this.cbT1.Text = "t1";
            this.cbT1.UseVisualStyleBackColor = true;
            // 
            // cbT2
            // 
            this.cbT2.AutoSize = true;
            this.cbT2.Location = new System.Drawing.Point(12, 85);
            this.cbT2.Name = "cbT2";
            this.cbT2.Size = new System.Drawing.Size(36, 16);
            this.cbT2.TabIndex = 3;
            this.cbT2.Text = "t2";
            this.cbT2.UseVisualStyleBackColor = true;
            // 
            // cbSprite
            // 
            this.cbSprite.AutoSize = true;
            this.cbSprite.Location = new System.Drawing.Point(12, 107);
            this.cbSprite.Name = "cbSprite";
            this.cbSprite.Size = new System.Drawing.Size(60, 16);
            this.cbSprite.TabIndex = 3;
            this.cbSprite.Text = "sprite";
            this.cbSprite.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 286);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 21);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbSprite
            // 
            this.tbSprite.Location = new System.Drawing.Point(12, 129);
            this.tbSprite.Name = "tbSprite";
            this.tbSprite.Size = new System.Drawing.Size(100, 21);
            this.tbSprite.TabIndex = 5;
            // 
            // konami68000Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 485);
            this.Controls.Add(this.tbSprite);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbSprite);
            this.Controls.Add(this.cbT2);
            this.Controls.Add(this.cbT1);
            this.Controls.Add(this.cbT0);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.pictureBox1);
            this.Name = "konami68000Form";
            this.Text = "konami68000Form";
            this.Load += new System.EventHandler(this.konami68000Form_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.konami68000Form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslLocation;
        private System.Windows.Forms.CheckBox cbT0;
        private System.Windows.Forms.CheckBox cbT1;
        private System.Windows.Forms.CheckBox cbT2;
        private System.Windows.Forms.CheckBox cbSprite;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.TextBox tbSprite;
    }
}