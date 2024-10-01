namespace ui
{
    partial class capcomForm
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
            this.btnDraw = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbSprite = new System.Windows.Forms.CheckBox();
            this.cbTx = new System.Windows.Forms.CheckBox();
            this.cbFg = new System.Windows.Forms.CheckBox();
            this.cbBg = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(12, 201);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(70, 21);
            this.btnDraw.TabIndex = 0;
            this.btnDraw.Text = "draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(111, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 256);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLocation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 409);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(674, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslLocation
            // 
            this.tsslLocation.Name = "tsslLocation";
            this.tsslLocation.Size = new System.Drawing.Size(17, 17);
            this.tsslLocation.Text = "...";
            // 
            // cbSprite
            // 
            this.cbSprite.AutoSize = true;
            this.cbSprite.Location = new System.Drawing.Point(12, 107);
            this.cbSprite.Name = "cbSprite";
            this.cbSprite.Size = new System.Drawing.Size(60, 16);
            this.cbSprite.TabIndex = 6;
            this.cbSprite.Text = "sprite";
            this.cbSprite.UseVisualStyleBackColor = true;
            // 
            // cbTx
            // 
            this.cbTx.AutoSize = true;
            this.cbTx.Location = new System.Drawing.Point(12, 85);
            this.cbTx.Name = "cbTx";
            this.cbTx.Size = new System.Drawing.Size(36, 16);
            this.cbTx.TabIndex = 7;
            this.cbTx.Text = "tx";
            this.cbTx.UseVisualStyleBackColor = true;
            // 
            // cbFg
            // 
            this.cbFg.AutoSize = true;
            this.cbFg.Location = new System.Drawing.Point(12, 63);
            this.cbFg.Name = "cbFg";
            this.cbFg.Size = new System.Drawing.Size(36, 16);
            this.cbFg.TabIndex = 4;
            this.cbFg.Text = "fg";
            this.cbFg.UseVisualStyleBackColor = true;
            // 
            // cbBg
            // 
            this.cbBg.AutoSize = true;
            this.cbBg.Location = new System.Drawing.Point(12, 41);
            this.cbBg.Name = "cbBg";
            this.cbBg.Size = new System.Drawing.Size(36, 16);
            this.cbBg.TabIndex = 5;
            this.cbBg.Text = "bg";
            this.cbBg.UseVisualStyleBackColor = true;
            // 
            // capcomForm
            // 
            this.ClientSize = new System.Drawing.Size(674, 431);
            this.Controls.Add(this.cbSprite);
            this.Controls.Add(this.cbTx);
            this.Controls.Add(this.cbFg);
            this.Controls.Add(this.cbBg);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDraw);
            this.Name = "capcomForm";
            this.Text = "capcom debugger";
            this.Load += new System.EventHandler(this.capcomForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.capcomForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslLocation;
        private System.Windows.Forms.CheckBox cbSprite;
        private System.Windows.Forms.CheckBox cbTx;
        private System.Windows.Forms.CheckBox cbFg;
        private System.Windows.Forms.CheckBox cbBg;
    }
}
