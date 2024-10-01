namespace ui
{
    partial class m68000Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.tbPPC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.cbS = new System.Windows.Forms.CheckBox();
            this.cbX = new System.Windows.Forms.CheckBox();
            this.cbN = new System.Windows.Forms.CheckBox();
            this.cbZ = new System.Windows.Forms.CheckBox();
            this.cbV = new System.Windows.Forms.CheckBox();
            this.cbC = new System.Windows.Forms.CheckBox();
            this.tbOP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStep2 = new System.Windows.Forms.Button();
            this.tbPPCTill = new System.Windows.Forms.TextBox();
            this.tbIML = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbUSP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSSP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDisassemble = new System.Windows.Forms.TextBox();
            this.btnStep3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbCyclesTill = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnGetNew = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbCycles = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbLog = new System.Windows.Forms.CheckBox();
            this.cbM = new System.Windows.Forms.CheckBox();
            this.tbPC = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnGN2 = new System.Windows.Forms.Button();
            this.btnStep4 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "D";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "A";
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(90, 397);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(70, 21);
            this.btnGet.TabIndex = 3;
            this.btnGet.Text = "get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(14, 431);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(70, 21);
            this.btnStep.TabIndex = 4;
            this.btnStep.Text = "step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // tbPPC
            // 
            this.tbPPC.Location = new System.Drawing.Point(14, 235);
            this.tbPPC.Name = "tbPPC";
            this.tbPPC.Size = new System.Drawing.Size(70, 21);
            this.tbPPC.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "PPC";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(14, 458);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(70, 21);
            this.btnRun.TabIndex = 7;
            this.btnRun.Text = "run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // cbS
            // 
            this.cbS.AutoSize = true;
            this.cbS.Location = new System.Drawing.Point(14, 262);
            this.cbS.Name = "cbS";
            this.cbS.Size = new System.Drawing.Size(30, 16);
            this.cbS.TabIndex = 8;
            this.cbS.Text = "S";
            this.cbS.UseVisualStyleBackColor = true;
            // 
            // cbX
            // 
            this.cbX.AutoSize = true;
            this.cbX.Location = new System.Drawing.Point(14, 306);
            this.cbX.Name = "cbX";
            this.cbX.Size = new System.Drawing.Size(30, 16);
            this.cbX.TabIndex = 8;
            this.cbX.Text = "X";
            this.cbX.UseVisualStyleBackColor = true;
            // 
            // cbN
            // 
            this.cbN.AutoSize = true;
            this.cbN.Location = new System.Drawing.Point(14, 328);
            this.cbN.Name = "cbN";
            this.cbN.Size = new System.Drawing.Size(30, 16);
            this.cbN.TabIndex = 8;
            this.cbN.Text = "N";
            this.cbN.UseVisualStyleBackColor = true;
            // 
            // cbZ
            // 
            this.cbZ.AutoSize = true;
            this.cbZ.Location = new System.Drawing.Point(14, 350);
            this.cbZ.Name = "cbZ";
            this.cbZ.Size = new System.Drawing.Size(30, 16);
            this.cbZ.TabIndex = 8;
            this.cbZ.Text = "Z";
            this.cbZ.UseVisualStyleBackColor = true;
            // 
            // cbV
            // 
            this.cbV.AutoSize = true;
            this.cbV.Location = new System.Drawing.Point(14, 372);
            this.cbV.Name = "cbV";
            this.cbV.Size = new System.Drawing.Size(30, 16);
            this.cbV.TabIndex = 8;
            this.cbV.Text = "V";
            this.cbV.UseVisualStyleBackColor = true;
            // 
            // cbC
            // 
            this.cbC.AutoSize = true;
            this.cbC.Location = new System.Drawing.Point(14, 394);
            this.cbC.Name = "cbC";
            this.cbC.Size = new System.Drawing.Size(30, 16);
            this.cbC.TabIndex = 8;
            this.cbC.Text = "C";
            this.cbC.UseVisualStyleBackColor = true;
            // 
            // tbOP
            // 
            this.tbOP.Location = new System.Drawing.Point(90, 235);
            this.tbOP.Name = "tbOP";
            this.tbOP.Size = new System.Drawing.Size(70, 21);
            this.tbOP.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(88, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "OP";
            // 
            // btnStep2
            // 
            this.btnStep2.Location = new System.Drawing.Point(166, 431);
            this.btnStep2.Name = "btnStep2";
            this.btnStep2.Size = new System.Drawing.Size(70, 21);
            this.btnStep2.TabIndex = 9;
            this.btnStep2.Text = "step till";
            this.btnStep2.UseVisualStyleBackColor = true;
            this.btnStep2.Click += new System.EventHandler(this.btnStep2_Click);
            // 
            // tbPPCTill
            // 
            this.tbPPCTill.Location = new System.Drawing.Point(373, 432);
            this.tbPPCTill.Name = "tbPPCTill";
            this.tbPPCTill.Size = new System.Drawing.Size(70, 21);
            this.tbPPCTill.TabIndex = 10;
            // 
            // tbIML
            // 
            this.tbIML.Location = new System.Drawing.Point(90, 282);
            this.tbIML.Name = "tbIML";
            this.tbIML.Size = new System.Drawing.Size(70, 21);
            this.tbIML.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(88, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "InterruptMaskLevel";
            // 
            // tbUSP
            // 
            this.tbUSP.Location = new System.Drawing.Point(90, 326);
            this.tbUSP.Name = "tbUSP";
            this.tbUSP.Size = new System.Drawing.Size(70, 21);
            this.tbUSP.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "USP";
            // 
            // tbSSP
            // 
            this.tbSSP.Location = new System.Drawing.Point(90, 370);
            this.tbSSP.Name = "tbSSP";
            this.tbSSP.Size = new System.Drawing.Size(70, 21);
            this.tbSSP.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(88, 351);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "SSP";
            // 
            // tbDisassemble
            // 
            this.tbDisassemble.Location = new System.Drawing.Point(166, 235);
            this.tbDisassemble.Name = "tbDisassemble";
            this.tbDisassemble.Size = new System.Drawing.Size(552, 21);
            this.tbDisassemble.TabIndex = 11;
            // 
            // btnStep3
            // 
            this.btnStep3.Location = new System.Drawing.Point(166, 458);
            this.btnStep3.Name = "btnStep3";
            this.btnStep3.Size = new System.Drawing.Size(70, 21);
            this.btnStep3.TabIndex = 12;
            this.btnStep3.Text = "step till";
            this.btnStep3.UseVisualStyleBackColor = true;
            this.btnStep3.Click += new System.EventHandler(this.btnStep3_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(338, 435);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "PPC:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(242, 462);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "TotalExecutedCycles:";
            // 
            // tbCyclesTill
            // 
            this.tbCyclesTill.Location = new System.Drawing.Point(373, 458);
            this.tbCyclesTill.Name = "tbCyclesTill";
            this.tbCyclesTill.Size = new System.Drawing.Size(120, 21);
            this.tbCyclesTill.TabIndex = 10;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 490);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(17, 17);
            this.tsslStatus.Text = "...";
            // 
            // btnGetNew
            // 
            this.btnGetNew.Location = new System.Drawing.Point(224, 27);
            this.btnGetNew.Name = "btnGetNew";
            this.btnGetNew.Size = new System.Drawing.Size(70, 21);
            this.btnGetNew.TabIndex = 18;
            this.btnGetNew.Text = "get new";
            this.btnGetNew.UseVisualStyleBackColor = true;
            this.btnGetNew.Click += new System.EventHandler(this.btnGetNew_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(224, 54);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 21);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(300, 12);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(472, 204);
            this.tbResult.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(164, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "Disassembly Info";
            // 
            // tbCycles
            // 
            this.tbCycles.Location = new System.Drawing.Point(222, 282);
            this.tbCycles.Name = "tbCycles";
            this.tbCycles.Size = new System.Drawing.Size(120, 21);
            this.tbCycles.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(220, 263);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "TotalExecutedCycles";
            // 
            // cbLog
            // 
            this.cbLog.AutoSize = true;
            this.cbLog.Location = new System.Drawing.Point(224, 174);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(42, 16);
            this.cbLog.TabIndex = 23;
            this.cbLog.Text = "log";
            this.cbLog.UseVisualStyleBackColor = true;
            // 
            // cbM
            // 
            this.cbM.AutoSize = true;
            this.cbM.Location = new System.Drawing.Point(14, 284);
            this.cbM.Name = "cbM";
            this.cbM.Size = new System.Drawing.Size(30, 16);
            this.cbM.TabIndex = 8;
            this.cbM.Text = "M";
            this.cbM.UseVisualStyleBackColor = true;
            // 
            // tbPC
            // 
            this.tbPC.Location = new System.Drawing.Point(222, 326);
            this.tbPC.Name = "tbPC";
            this.tbPC.Size = new System.Drawing.Size(70, 21);
            this.tbPC.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(220, 311);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "PC";
            // 
            // btnGN2
            // 
            this.btnGN2.Location = new System.Drawing.Point(382, 281);
            this.btnGN2.Name = "btnGN2";
            this.btnGN2.Size = new System.Drawing.Size(70, 21);
            this.btnGN2.TabIndex = 18;
            this.btnGN2.Text = "get";
            this.btnGN2.UseVisualStyleBackColor = true;
            this.btnGN2.Click += new System.EventHandler(this.btnGN2_Click);
            // 
            // btnStep4
            // 
            this.btnStep4.Location = new System.Drawing.Point(382, 370);
            this.btnStep4.Name = "btnStep4";
            this.btnStep4.Size = new System.Drawing.Size(75, 21);
            this.btnStep4.TabIndex = 24;
            this.btnStep4.Text = "step frame";
            this.btnStep4.UseVisualStyleBackColor = true;
            this.btnStep4.Click += new System.EventHandler(this.btnStep4_Click);
            // 
            // m68000Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 512);
            this.Controls.Add(this.btnStep4);
            this.Controls.Add(this.cbLog);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnGN2);
            this.Controls.Add(this.btnGetNew);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnStep3);
            this.Controls.Add(this.tbDisassemble);
            this.Controls.Add(this.tbCyclesTill);
            this.Controls.Add(this.tbPPCTill);
            this.Controls.Add(this.btnStep2);
            this.Controls.Add(this.cbC);
            this.Controls.Add(this.cbV);
            this.Controls.Add(this.cbZ);
            this.Controls.Add(this.cbN);
            this.Controls.Add(this.cbX);
            this.Controls.Add(this.cbM);
            this.Controls.Add(this.cbS);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSSP);
            this.Controls.Add(this.tbUSP);
            this.Controls.Add(this.tbCycles);
            this.Controls.Add(this.tbIML);
            this.Controls.Add(this.tbOP);
            this.Controls.Add(this.tbPC);
            this.Controls.Add(this.tbPPC);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "m68000Form";
            this.Text = "M68000 debugger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.m68000Form_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.TextBox tbPPC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.CheckBox cbS;
        private System.Windows.Forms.CheckBox cbX;
        private System.Windows.Forms.CheckBox cbN;
        private System.Windows.Forms.CheckBox cbZ;
        private System.Windows.Forms.CheckBox cbV;
        private System.Windows.Forms.CheckBox cbC;
        private System.Windows.Forms.TextBox tbOP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStep2;
        private System.Windows.Forms.TextBox tbPPCTill;
        private System.Windows.Forms.TextBox tbIML;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbUSP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSSP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDisassemble;
        private System.Windows.Forms.Button btnStep3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbCyclesTill;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.Button btnGetNew;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbCycles;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.CheckBox cbLog;
        private System.Windows.Forms.CheckBox cbM;
        private System.Windows.Forms.TextBox tbPC;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnGN2;
        private System.Windows.Forms.Button btnStep4;
    }
}