namespace ui
{
    partial class cheatsearchForm
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
            this.cbRam = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbRamValue = new System.Windows.Forms.TextBox();
            this.tbRamCount = new System.Windows.Forms.TextBox();
            this.searchListView = new System.Windows.Forms.ListView();
            this.cbByte = new System.Windows.Forms.ComboBox();
            this.tbRamRange = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRead = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.tbWrite = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.cbMemory = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbRam
            // 
            this.cbRam.FormattingEnabled = true;
            this.cbRam.Items.AddRange(new object[] {
            "?",
            "!",
            "=",
            "+",
            "-",
            "=?",
            ">?",
            "<?"});
            this.cbRam.Location = new System.Drawing.Point(12, 12);
            this.cbRam.Name = "cbRam";
            this.cbRam.Size = new System.Drawing.Size(70, 20);
            this.cbRam.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(287, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 21);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(363, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 21);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(14, 291);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 21);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbRamValue
            // 
            this.tbRamValue.Location = new System.Drawing.Point(88, 12);
            this.tbRamValue.Name = "tbRamValue";
            this.tbRamValue.Size = new System.Drawing.Size(70, 21);
            this.tbRamValue.TabIndex = 4;
            // 
            // tbRamCount
            // 
            this.tbRamCount.Enabled = false;
            this.tbRamCount.Location = new System.Drawing.Point(88, 38);
            this.tbRamCount.Name = "tbRamCount";
            this.tbRamCount.Size = new System.Drawing.Size(70, 21);
            this.tbRamCount.TabIndex = 5;
            // 
            // searchListView
            // 
            this.searchListView.Location = new System.Drawing.Point(12, 65);
            this.searchListView.Name = "searchListView";
            this.searchListView.Size = new System.Drawing.Size(370, 220);
            this.searchListView.TabIndex = 6;
            this.searchListView.UseCompatibleStateImageBehavior = false;
            // 
            // cbByte
            // 
            this.cbByte.FormattingEnabled = true;
            this.cbByte.Items.AddRange(new object[] {
            "1",
            "2",
            "4"});
            this.cbByte.Location = new System.Drawing.Point(211, 12);
            this.cbByte.Name = "cbByte";
            this.cbByte.Size = new System.Drawing.Size(70, 20);
            this.cbByte.TabIndex = 7;
            // 
            // tbRamRange
            // 
            this.tbRamRange.Location = new System.Drawing.Point(211, 38);
            this.tbRamRange.Name = "tbRamRange";
            this.tbRamRange.Size = new System.Drawing.Size(70, 21);
            this.tbRamRange.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "bytes:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "range:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "count:";
            // 
            // tbRead
            // 
            this.tbRead.Location = new System.Drawing.Point(388, 65);
            this.tbRead.Multiline = true;
            this.tbRead.Name = "tbRead";
            this.tbRead.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbRead.Size = new System.Drawing.Size(182, 72);
            this.tbRead.TabIndex = 13;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(388, 143);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(70, 21);
            this.btnRead.TabIndex = 14;
            this.btnRead.Text = "read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // tbWrite
            // 
            this.tbWrite.Location = new System.Drawing.Point(388, 172);
            this.tbWrite.Multiline = true;
            this.tbWrite.Name = "tbWrite";
            this.tbWrite.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbWrite.Size = new System.Drawing.Size(182, 72);
            this.tbWrite.TabIndex = 13;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(388, 250);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(70, 21);
            this.btnWrite.TabIndex = 14;
            this.btnWrite.Text = "write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(388, 292);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(182, 139);
            this.tbResult.TabIndex = 15;
            // 
            // cbMemory
            // 
            this.cbMemory.FormattingEnabled = true;
            this.cbMemory.Items.AddRange(new object[] {
            "mainram",
            "mainrom"});
            this.cbMemory.Location = new System.Drawing.Point(388, 38);
            this.cbMemory.Name = "cbMemory";
            this.cbMemory.Size = new System.Drawing.Size(70, 20);
            this.cbMemory.TabIndex = 7;
            // 
            // cheatsearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.tbWrite);
            this.Controls.Add(this.tbRead);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRamRange);
            this.Controls.Add(this.cbMemory);
            this.Controls.Add(this.cbByte);
            this.Controls.Add(this.searchListView);
            this.Controls.Add(this.tbRamCount);
            this.Controls.Add(this.tbRamValue);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cbRam);
            this.Name = "cheatsearchForm";
            this.Text = "Cheat search";
            this.Load += new System.EventHandler(this.cheatsearchForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.cheatForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cbRam;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbRamValue;
        private System.Windows.Forms.TextBox tbRamCount;
        private System.Windows.Forms.ListView searchListView;
        private System.Windows.Forms.ComboBox cbByte;
        private System.Windows.Forms.TextBox tbRamRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRead;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox tbWrite;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.ComboBox cbMemory;

    }
}