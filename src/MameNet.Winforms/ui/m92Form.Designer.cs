namespace ui
{
    partial class m92Form
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDraw = new System.Windows.Forms.Button();
            this.cb00 = new System.Windows.Forms.CheckBox();
            this.cb01 = new System.Windows.Forms.CheckBox();
            this.cb10 = new System.Windows.Forms.CheckBox();
            this.cb11 = new System.Windows.Forms.CheckBox();
            this.cb20 = new System.Windows.Forms.CheckBox();
            this.cb21 = new System.Windows.Forms.CheckBox();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.cbSprite = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(290, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLocation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 552);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(865, 22);
            this.statusStrip1.TabIndex = 1;
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
            this.btnDraw.Location = new System.Drawing.Point(12, 235);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(70, 21);
            this.btnDraw.TabIndex = 2;
            this.btnDraw.Text = "draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // cb00
            // 
            this.cb00.AutoSize = true;
            this.cb00.Location = new System.Drawing.Point(12, 12);
            this.cb00.Name = "cb00";
            this.cb00.Size = new System.Drawing.Size(36, 16);
            this.cb00.TabIndex = 3;
            this.cb00.Text = "00";
            this.cb00.UseVisualStyleBackColor = true;
            // 
            // cb01
            // 
            this.cb01.AutoSize = true;
            this.cb01.Location = new System.Drawing.Point(12, 34);
            this.cb01.Name = "cb01";
            this.cb01.Size = new System.Drawing.Size(36, 16);
            this.cb01.TabIndex = 3;
            this.cb01.Text = "01";
            this.cb01.UseVisualStyleBackColor = true;
            // 
            // cb10
            // 
            this.cb10.AutoSize = true;
            this.cb10.Location = new System.Drawing.Point(12, 56);
            this.cb10.Name = "cb10";
            this.cb10.Size = new System.Drawing.Size(36, 16);
            this.cb10.TabIndex = 3;
            this.cb10.Text = "10";
            this.cb10.UseVisualStyleBackColor = true;
            // 
            // cb11
            // 
            this.cb11.AutoSize = true;
            this.cb11.Location = new System.Drawing.Point(12, 78);
            this.cb11.Name = "cb11";
            this.cb11.Size = new System.Drawing.Size(36, 16);
            this.cb11.TabIndex = 3;
            this.cb11.Text = "11";
            this.cb11.UseVisualStyleBackColor = true;
            // 
            // cb20
            // 
            this.cb20.AutoSize = true;
            this.cb20.Location = new System.Drawing.Point(12, 100);
            this.cb20.Name = "cb20";
            this.cb20.Size = new System.Drawing.Size(36, 16);
            this.cb20.TabIndex = 3;
            this.cb20.Text = "20";
            this.cb20.UseVisualStyleBackColor = true;
            // 
            // cb21
            // 
            this.cb21.AutoSize = true;
            this.cb21.Location = new System.Drawing.Point(12, 122);
            this.cb21.Name = "cb21";
            this.cb21.Size = new System.Drawing.Size(36, 16);
            this.cb21.TabIndex = 3;
            this.cb21.Text = "21";
            this.cb21.UseVisualStyleBackColor = true;
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(12, 208);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(100, 21);
            this.tbInput.TabIndex = 4;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(12, 316);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(100, 222);
            this.tbResult.TabIndex = 5;
            // 
            // cbSprite
            // 
            this.cbSprite.AutoSize = true;
            this.cbSprite.Location = new System.Drawing.Point(12, 144);
            this.cbSprite.Name = "cbSprite";
            this.cbSprite.Size = new System.Drawing.Size(60, 16);
            this.cbSprite.TabIndex = 3;
            this.cbSprite.Text = "sprite";
            this.cbSprite.UseVisualStyleBackColor = true;
            // 
            // m92Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 574);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.cbSprite);
            this.Controls.Add(this.cb21);
            this.Controls.Add(this.cb20);
            this.Controls.Add(this.cb11);
            this.Controls.Add(this.cb10);
            this.Controls.Add(this.cb01);
            this.Controls.Add(this.cb00);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "m92Form";
            this.Text = "m92Form";
            this.Load += new System.EventHandler(this.m92Form_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.m92Form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslLocation;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.CheckBox cb00;
        private System.Windows.Forms.CheckBox cb01;
        private System.Windows.Forms.CheckBox cb10;
        private System.Windows.Forms.CheckBox cb11;
        private System.Windows.Forms.CheckBox cb20;
        private System.Windows.Forms.CheckBox cb21;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.CheckBox cbSprite;
    }
}