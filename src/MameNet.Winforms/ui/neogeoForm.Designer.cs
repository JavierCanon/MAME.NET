namespace ui
{
    partial class neogeoForm
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
            this.cbL0 = new System.Windows.Forms.CheckBox();
            this.cbL1 = new System.Windows.Forms.CheckBox();
            this.tbPoint = new System.Windows.Forms.TextBox();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDraw = new System.Windows.Forms.Button();
            this.tbSprite = new System.Windows.Forms.TextBox();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.tbSOffset = new System.Windows.Forms.TextBox();
            this.btnDraw2 = new System.Windows.Forms.Button();
            this.tbPensoffset = new System.Windows.Forms.TextBox();
            this.btnDumpRam = new System.Windows.Forms.Button();
            this.btnWriteRam = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(214, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(384, 264);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // cbL0
            // 
            this.cbL0.AutoSize = true;
            this.cbL0.Location = new System.Drawing.Point(12, 12);
            this.cbL0.Name = "cbL0";
            this.cbL0.Size = new System.Drawing.Size(36, 16);
            this.cbL0.TabIndex = 1;
            this.cbL0.Text = "l0";
            this.cbL0.UseVisualStyleBackColor = true;
            // 
            // cbL1
            // 
            this.cbL1.AutoSize = true;
            this.cbL1.Location = new System.Drawing.Point(12, 34);
            this.cbL1.Name = "cbL1";
            this.cbL1.Size = new System.Drawing.Size(36, 16);
            this.cbL1.TabIndex = 1;
            this.cbL1.Text = "l1";
            this.cbL1.UseVisualStyleBackColor = true;
            // 
            // tbPoint
            // 
            this.tbPoint.Location = new System.Drawing.Point(12, 309);
            this.tbPoint.Name = "tbPoint";
            this.tbPoint.Size = new System.Drawing.Size(120, 21);
            this.tbPoint.TabIndex = 10;
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(12, 282);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(120, 21);
            this.tbFile.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(138, 309);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 21);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLocation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 378);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(614, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslLocation
            // 
            this.tsslLocation.Name = "tsslLocation";
            this.tsslLocation.Size = new System.Drawing.Size(17, 17);
            this.tsslLocation.Text = "...";
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(138, 83);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(70, 21);
            this.btnDraw.TabIndex = 13;
            this.btnDraw.Text = "draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // tbSprite
            // 
            this.tbSprite.Location = new System.Drawing.Point(12, 83);
            this.tbSprite.Name = "tbSprite";
            this.tbSprite.Size = new System.Drawing.Size(120, 21);
            this.tbSprite.TabIndex = 14;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(12, 164);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(120, 112);
            this.tbResult.TabIndex = 14;
            // 
            // tbSOffset
            // 
            this.tbSOffset.Location = new System.Drawing.Point(12, 110);
            this.tbSOffset.Name = "tbSOffset";
            this.tbSOffset.Size = new System.Drawing.Size(120, 21);
            this.tbSOffset.TabIndex = 14;
            // 
            // btnDraw2
            // 
            this.btnDraw2.Location = new System.Drawing.Point(138, 109);
            this.btnDraw2.Name = "btnDraw2";
            this.btnDraw2.Size = new System.Drawing.Size(70, 21);
            this.btnDraw2.TabIndex = 13;
            this.btnDraw2.Text = "draw2";
            this.btnDraw2.UseVisualStyleBackColor = true;
            this.btnDraw2.Click += new System.EventHandler(this.btnDraw2_Click);
            // 
            // tbPensoffset
            // 
            this.tbPensoffset.Location = new System.Drawing.Point(12, 137);
            this.tbPensoffset.Name = "tbPensoffset";
            this.tbPensoffset.Size = new System.Drawing.Size(120, 21);
            this.tbPensoffset.TabIndex = 14;
            // 
            // btnDumpRam
            // 
            this.btnDumpRam.Location = new System.Drawing.Point(138, 183);
            this.btnDumpRam.Name = "btnDumpRam";
            this.btnDumpRam.Size = new System.Drawing.Size(70, 21);
            this.btnDumpRam.TabIndex = 15;
            this.btnDumpRam.Text = "dump ram";
            this.btnDumpRam.UseVisualStyleBackColor = true;
            this.btnDumpRam.Click += new System.EventHandler(this.btnDumpRam_Click);
            // 
            // btnWriteRam
            // 
            this.btnWriteRam.Location = new System.Drawing.Point(138, 210);
            this.btnWriteRam.Name = "btnWriteRam";
            this.btnWriteRam.Size = new System.Drawing.Size(70, 21);
            this.btnWriteRam.TabIndex = 15;
            this.btnWriteRam.Text = "write ram";
            this.btnWriteRam.UseVisualStyleBackColor = true;
            this.btnWriteRam.Click += new System.EventHandler(this.btnWriteRam_Click);
            // 
            // neogeoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 400);
            this.Controls.Add(this.btnWriteRam);
            this.Controls.Add(this.btnDumpRam);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.tbPensoffset);
            this.Controls.Add(this.tbSOffset);
            this.Controls.Add(this.tbSprite);
            this.Controls.Add(this.btnDraw2);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbPoint);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbL1);
            this.Controls.Add(this.cbL0);
            this.Controls.Add(this.pictureBox1);
            this.Name = "neogeoForm";
            this.Text = "neogeo debugger";
            this.Load += new System.EventHandler(this.neogeoForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.neogeoForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox cbL0;
        private System.Windows.Forms.CheckBox cbL1;
        public System.Windows.Forms.TextBox tbPoint;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslLocation;
        private System.Windows.Forms.Button btnDraw;
        public System.Windows.Forms.TextBox tbSprite;
        public System.Windows.Forms.TextBox tbResult;
        public System.Windows.Forms.TextBox tbSOffset;
        private System.Windows.Forms.Button btnDraw2;
        public System.Windows.Forms.TextBox tbPensoffset;
        private System.Windows.Forms.Button btnDumpRam;
        private System.Windows.Forms.Button btnWriteRam;
    }
}