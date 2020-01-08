namespace ui
{
    partial class mainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cheatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cheatsearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neogeoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.namcos1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pgmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m72ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m92ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m68000ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.z80ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m6809ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(534, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.resetToolStripMenuItem.Text = "&Reset picturebox";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gameStripMenuItem
            // 
            this.gameStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cheatToolStripMenuItem,
            this.cheatsearchToolStripMenuItem,
            this.ipsToolStripMenuItem,
            this.boardToolStripMenuItem,
            this.m68000ToolStripMenuItem,
            this.z80ToolStripMenuItem,
            this.m6809ToolStripMenuItem});
            this.gameStripMenuItem.Name = "gameStripMenuItem";
            this.gameStripMenuItem.Size = new System.Drawing.Size(54, 21);
            this.gameStripMenuItem.Text = "&Game";
            // 
            // cheatToolStripMenuItem
            // 
            this.cheatToolStripMenuItem.Name = "cheatToolStripMenuItem";
            this.cheatToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.cheatToolStripMenuItem.Text = "&Cheat";
            this.cheatToolStripMenuItem.Click += new System.EventHandler(this.cheatToolStripMenuItem_Click);
            // 
            // cheatsearchToolStripMenuItem
            // 
            this.cheatsearchToolStripMenuItem.Name = "cheatsearchToolStripMenuItem";
            this.cheatsearchToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.cheatsearchToolStripMenuItem.Text = "Cheat sea&rch";
            this.cheatsearchToolStripMenuItem.Click += new System.EventHandler(this.cheatsearchToolStripMenuItem_Click);
            // 
            // ipsToolStripMenuItem
            // 
            this.ipsToolStripMenuItem.Name = "ipsToolStripMenuItem";
            this.ipsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.ipsToolStripMenuItem.Text = "&IPS";
            this.ipsToolStripMenuItem.Click += new System.EventHandler(this.ipsToolStripMenuItem_Click);
            // 
            // boardToolStripMenuItem
            // 
            this.boardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cpsToolStripMenuItem,
            this.neogeoToolStripMenuItem,
            this.namcos1ToolStripMenuItem,
            this.pgmToolStripMenuItem,
            this.m72ToolStripMenuItem,
            this.m92ToolStripMenuItem});
            this.boardToolStripMenuItem.Name = "boardToolStripMenuItem";
            this.boardToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.boardToolStripMenuItem.Text = "&Board debugger";
            // 
            // cpsToolStripMenuItem
            // 
            this.cpsToolStripMenuItem.Name = "cpsToolStripMenuItem";
            this.cpsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.cpsToolStripMenuItem.Text = "CPS debugger";
            this.cpsToolStripMenuItem.Click += new System.EventHandler(this.cpsToolStripMenuItem_Click);
            // 
            // neogeoToolStripMenuItem
            // 
            this.neogeoToolStripMenuItem.Name = "neogeoToolStripMenuItem";
            this.neogeoToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.neogeoToolStripMenuItem.Text = "Neogeo debugger";
            this.neogeoToolStripMenuItem.Click += new System.EventHandler(this.neogeoToolStripMenuItem_Click);
            // 
            // namcos1ToolStripMenuItem
            // 
            this.namcos1ToolStripMenuItem.Name = "namcos1ToolStripMenuItem";
            this.namcos1ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.namcos1ToolStripMenuItem.Text = "Namcos1 debugger";
            this.namcos1ToolStripMenuItem.Click += new System.EventHandler(this.namcos1ToolStripMenuItem_Click);
            // 
            // pgmToolStripMenuItem
            // 
            this.pgmToolStripMenuItem.Name = "pgmToolStripMenuItem";
            this.pgmToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.pgmToolStripMenuItem.Text = "Pgm debugger";
            this.pgmToolStripMenuItem.Click += new System.EventHandler(this.pgmToolStripMenuItem_Click);
            // 
            // m72ToolStripMenuItem
            // 
            this.m72ToolStripMenuItem.Name = "m72ToolStripMenuItem";
            this.m72ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.m72ToolStripMenuItem.Text = "M72 debugger";
            this.m72ToolStripMenuItem.Click += new System.EventHandler(this.m72ToolStripMenuItem_Click);
            // 
            // m92ToolStripMenuItem
            // 
            this.m92ToolStripMenuItem.Name = "m92ToolStripMenuItem";
            this.m92ToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.m92ToolStripMenuItem.Text = "M92 debugger";
            this.m92ToolStripMenuItem.Click += new System.EventHandler(this.m92ToolStripMenuItem_Click);
            // 
            // m68000ToolStripMenuItem
            // 
            this.m68000ToolStripMenuItem.Name = "m68000ToolStripMenuItem";
            this.m68000ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.m68000ToolStripMenuItem.Text = "&M68000 debugger";
            this.m68000ToolStripMenuItem.Click += new System.EventHandler(this.m68000ToolStripMenuItem_Click);
            // 
            // z80ToolStripMenuItem
            // 
            this.z80ToolStripMenuItem.Name = "z80ToolStripMenuItem";
            this.z80ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.z80ToolStripMenuItem.Text = "&Z80 debugger";
            this.z80ToolStripMenuItem.Click += new System.EventHandler(this.z80ToolStripMenuItem_Click);
            // 
            // m6809ToolStripMenuItem
            // 
            this.m6809ToolStripMenuItem.Name = "m6809ToolStripMenuItem";
            this.m6809ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.m6809ToolStripMenuItem.Text = "M680&9 debugger";
            this.m6809ToolStripMenuItem.Click += new System.EventHandler(this.m6809ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 560);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(534, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(17, 17);
            this.tsslStatus.Text = "...";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 582);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainForm";
            this.Text = "...";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem gameStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cheatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cheatsearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m68000ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem z80ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ToolStripMenuItem m6809ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neogeoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem namcos1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pgmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m72ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m92ToolStripMenuItem;
    }
}