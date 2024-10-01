namespace ui
{
    partial class namcos1Form
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
            this.tbLayer = new System.Windows.Forms.TextBox();
            this.btnDraw2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(12, 177);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(70, 21);
            this.btnDraw.TabIndex = 0;
            this.btnDraw.Text = "draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(88, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tbLayer
            // 
            this.tbLayer.Location = new System.Drawing.Point(12, 95);
            this.tbLayer.Name = "tbLayer";
            this.tbLayer.Size = new System.Drawing.Size(70, 21);
            this.tbLayer.TabIndex = 2;
            // 
            // btnDraw2
            // 
            this.btnDraw2.Location = new System.Drawing.Point(12, 204);
            this.btnDraw2.Name = "btnDraw2";
            this.btnDraw2.Size = new System.Drawing.Size(70, 21);
            this.btnDraw2.TabIndex = 0;
            this.btnDraw2.Text = "draw2";
            this.btnDraw2.UseVisualStyleBackColor = true;
            this.btnDraw2.Click += new System.EventHandler(this.btnDraw2_Click);
            // 
            // namcos1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 562);
            this.Controls.Add(this.tbLayer);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDraw2);
            this.Controls.Add(this.btnDraw);
            this.Name = "namcos1Form";
            this.Text = "namcos1Form";
            this.Load += new System.EventHandler(this.namcos1Form_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.namcos1Form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbLayer;
        private System.Windows.Forms.Button btnDraw2;
    }
}