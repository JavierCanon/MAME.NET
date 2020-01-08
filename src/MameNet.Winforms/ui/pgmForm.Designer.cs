namespace ui
{
    partial class pgmForm
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
            this.cbTx = new System.Windows.Forms.CheckBox();
            this.cbBg = new System.Windows.Forms.CheckBox();
            this.cbSprite0 = new System.Windows.Forms.CheckBox();
            this.cbSprite1 = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslLocation = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(303, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(12, 231);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(70, 21);
            this.btnDraw.TabIndex = 1;
            this.btnDraw.Text = "draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // cbTx
            // 
            this.cbTx.AutoSize = true;
            this.cbTx.Location = new System.Drawing.Point(12, 114);
            this.cbTx.Name = "cbTx";
            this.cbTx.Size = new System.Drawing.Size(36, 16);
            this.cbTx.TabIndex = 2;
            this.cbTx.Text = "tx";
            this.cbTx.UseVisualStyleBackColor = true;
            // 
            // cbBg
            // 
            this.cbBg.AutoSize = true;
            this.cbBg.Location = new System.Drawing.Point(12, 70);
            this.cbBg.Name = "cbBg";
            this.cbBg.Size = new System.Drawing.Size(36, 16);
            this.cbBg.TabIndex = 2;
            this.cbBg.Text = "bg";
            this.cbBg.UseVisualStyleBackColor = true;
            // 
            // cbSprite0
            // 
            this.cbSprite0.AutoSize = true;
            this.cbSprite0.Location = new System.Drawing.Point(12, 92);
            this.cbSprite0.Name = "cbSprite0";
            this.cbSprite0.Size = new System.Drawing.Size(66, 16);
            this.cbSprite0.TabIndex = 2;
            this.cbSprite0.Text = "sprite0";
            this.cbSprite0.UseVisualStyleBackColor = true;
            // 
            // cbSprite1
            // 
            this.cbSprite1.AutoSize = true;
            this.cbSprite1.Location = new System.Drawing.Point(12, 48);
            this.cbSprite1.Name = "cbSprite1";
            this.cbSprite1.Size = new System.Drawing.Size(66, 16);
            this.cbSprite1.TabIndex = 2;
            this.cbSprite1.Text = "sprite1";
            this.cbSprite1.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLocation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 591);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(852, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslLocation
            // 
            this.tsslLocation.Name = "tsslLocation";
            this.tsslLocation.Size = new System.Drawing.Size(17, 17);
            this.tsslLocation.Text = "...";
            // 
            // pgmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 613);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cbSprite1);
            this.Controls.Add(this.cbBg);
            this.Controls.Add(this.cbSprite0);
            this.Controls.Add(this.cbTx);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.pictureBox1);
            this.Name = "pgmForm";
            this.Text = "pgm debugger";
            this.Load += new System.EventHandler(this.pgmForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.pgmForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.CheckBox cbTx;
        private System.Windows.Forms.CheckBox cbBg;
        private System.Windows.Forms.CheckBox cbSprite0;
        private System.Windows.Forms.CheckBox cbSprite1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslLocation;
    }
}