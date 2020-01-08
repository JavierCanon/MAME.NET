namespace ui
{
    partial class z80Form
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
            this.btnGet = new System.Windows.Forms.Button();
            this.tbDisassemble = new System.Windows.Forms.TextBox();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.tbAF = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBC = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDE = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbHL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbShadowAF = new System.Windows.Forms.TextBox();
            this.tbShadowBC = new System.Windows.Forms.TextBox();
            this.tbShadowDE = new System.Windows.Forms.TextBox();
            this.tbShadowHL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbI = new System.Windows.Forms.TextBox();
            this.tbR = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbIX = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbIY = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbSP = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbRPC = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnStep2 = new System.Windows.Forms.Button();
            this.btnStep3 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbPPCTill = new System.Windows.Forms.TextBox();
            this.tbCyclesTill = new System.Windows.Forms.TextBox();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btnGetNew = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.tbPPC = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tbCycles = new System.Windows.Forms.TextBox();
            this.tbR2 = new System.Windows.Forms.TextBox();
            this.tbWZ = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(14, 348);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(70, 21);
            this.btnGet.TabIndex = 0;
            this.btnGet.Text = "get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // tbDisassemble
            // 
            this.tbDisassemble.Location = new System.Drawing.Point(90, 317);
            this.tbDisassemble.Name = "tbDisassemble";
            this.tbDisassemble.Size = new System.Drawing.Size(383, 21);
            this.tbDisassemble.TabIndex = 1;
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(14, 375);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(70, 21);
            this.btnStep.TabIndex = 2;
            this.btnStep.Text = "step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(14, 402);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(70, 21);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // tbAF
            // 
            this.tbAF.Location = new System.Drawing.Point(88, 12);
            this.tbAF.Name = "tbAF";
            this.tbAF.Size = new System.Drawing.Size(70, 21);
            this.tbAF.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "RegisterAF";
            // 
            // tbBC
            // 
            this.tbBC.Location = new System.Drawing.Point(88, 39);
            this.tbBC.Name = "tbBC";
            this.tbBC.Size = new System.Drawing.Size(70, 21);
            this.tbBC.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "RegisterBC";
            // 
            // tbDE
            // 
            this.tbDE.Location = new System.Drawing.Point(88, 66);
            this.tbDE.Name = "tbDE";
            this.tbDE.Size = new System.Drawing.Size(70, 21);
            this.tbDE.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "RegisterDE";
            // 
            // tbHL
            // 
            this.tbHL.Location = new System.Drawing.Point(88, 93);
            this.tbHL.Name = "tbHL";
            this.tbHL.Size = new System.Drawing.Size(70, 21);
            this.tbHL.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "RegisterHL";
            // 
            // tbShadowAF
            // 
            this.tbShadowAF.Location = new System.Drawing.Point(271, 12);
            this.tbShadowAF.Name = "tbShadowAF";
            this.tbShadowAF.Size = new System.Drawing.Size(70, 21);
            this.tbShadowAF.TabIndex = 4;
            // 
            // tbShadowBC
            // 
            this.tbShadowBC.Location = new System.Drawing.Point(271, 39);
            this.tbShadowBC.Name = "tbShadowBC";
            this.tbShadowBC.Size = new System.Drawing.Size(70, 21);
            this.tbShadowBC.TabIndex = 4;
            // 
            // tbShadowDE
            // 
            this.tbShadowDE.Location = new System.Drawing.Point(271, 66);
            this.tbShadowDE.Name = "tbShadowDE";
            this.tbShadowDE.Size = new System.Drawing.Size(70, 21);
            this.tbShadowDE.TabIndex = 4;
            // 
            // tbShadowHL
            // 
            this.tbShadowHL.Location = new System.Drawing.Point(271, 93);
            this.tbShadowHL.Name = "tbShadowHL";
            this.tbShadowHL.Size = new System.Drawing.Size(70, 21);
            this.tbShadowHL.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "RegisterShadowAF";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "RegisterShadowBC";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(164, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "RegisterShadowDE";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(164, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "RegisterShadowHL";
            // 
            // tbI
            // 
            this.tbI.Location = new System.Drawing.Point(88, 120);
            this.tbI.Name = "tbI";
            this.tbI.Size = new System.Drawing.Size(70, 21);
            this.tbI.TabIndex = 4;
            // 
            // tbR
            // 
            this.tbR.Location = new System.Drawing.Point(88, 147);
            this.tbR.Name = "tbR";
            this.tbR.Size = new System.Drawing.Size(70, 21);
            this.tbR.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "RegisterI";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "RegisterR";
            // 
            // tbIX
            // 
            this.tbIX.Location = new System.Drawing.Point(88, 174);
            this.tbIX.Name = "tbIX";
            this.tbIX.Size = new System.Drawing.Size(70, 21);
            this.tbIX.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 5;
            this.label11.Text = "RegisterIX";
            // 
            // tbIY
            // 
            this.tbIY.Location = new System.Drawing.Point(88, 201);
            this.tbIY.Name = "tbIY";
            this.tbIY.Size = new System.Drawing.Size(70, 21);
            this.tbIY.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 204);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 5;
            this.label12.Text = "RegisterIY";
            // 
            // tbSP
            // 
            this.tbSP.Location = new System.Drawing.Point(88, 228);
            this.tbSP.Name = "tbSP";
            this.tbSP.Size = new System.Drawing.Size(70, 21);
            this.tbSP.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 231);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 5;
            this.label13.Text = "RegisterSP";
            // 
            // tbRPC
            // 
            this.tbRPC.Location = new System.Drawing.Point(88, 255);
            this.tbRPC.Name = "tbRPC";
            this.tbRPC.Size = new System.Drawing.Size(70, 21);
            this.tbRPC.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 258);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 5;
            this.label14.Text = "RegisterPC";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 440);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(17, 17);
            this.tsslStatus.Text = "...";
            // 
            // btnStep2
            // 
            this.btnStep2.Location = new System.Drawing.Point(146, 375);
            this.btnStep2.Name = "btnStep2";
            this.btnStep2.Size = new System.Drawing.Size(70, 21);
            this.btnStep2.TabIndex = 7;
            this.btnStep2.Text = "step till";
            this.btnStep2.UseVisualStyleBackColor = true;
            this.btnStep2.Click += new System.EventHandler(this.btnStep2_Click);
            // 
            // btnStep3
            // 
            this.btnStep3.Location = new System.Drawing.Point(146, 402);
            this.btnStep3.Name = "btnStep3";
            this.btnStep3.Size = new System.Drawing.Size(70, 21);
            this.btnStep3.TabIndex = 7;
            this.btnStep3.Text = "step till";
            this.btnStep3.UseVisualStyleBackColor = true;
            this.btnStep3.Click += new System.EventHandler(this.btnStep3_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(318, 379);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 8;
            this.label15.Text = "PPC:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(222, 406);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(125, 12);
            this.label16.TabIndex = 8;
            this.label16.Text = "TotalExecutedCycles:";
            // 
            // tbPPCTill
            // 
            this.tbPPCTill.Location = new System.Drawing.Point(353, 376);
            this.tbPPCTill.Name = "tbPPCTill";
            this.tbPPCTill.Size = new System.Drawing.Size(70, 21);
            this.tbPPCTill.TabIndex = 9;
            // 
            // tbCyclesTill
            // 
            this.tbCyclesTill.Location = new System.Drawing.Point(353, 402);
            this.tbCyclesTill.Name = "tbCyclesTill";
            this.tbCyclesTill.Size = new System.Drawing.Size(120, 21);
            this.tbCyclesTill.TabIndex = 9;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(400, 12);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(372, 204);
            this.tbResult.TabIndex = 10;
            // 
            // btnGetNew
            // 
            this.btnGetNew.Location = new System.Drawing.Point(324, 195);
            this.btnGetNew.Name = "btnGetNew";
            this.btnGetNew.Size = new System.Drawing.Size(70, 21);
            this.btnGetNew.TabIndex = 11;
            this.btnGetNew.Text = "get new";
            this.btnGetNew.UseVisualStyleBackColor = true;
            this.btnGetNew.Click += new System.EventHandler(this.btnGetNew_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(324, 222);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 21);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbPPC
            // 
            this.tbPPC.Location = new System.Drawing.Point(14, 317);
            this.tbPPC.Name = "tbPPC";
            this.tbPPC.Size = new System.Drawing.Size(68, 21);
            this.tbPPC.TabIndex = 4;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 302);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 12);
            this.label17.TabIndex = 12;
            this.label17.Text = "PPC";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(90, 302);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 12);
            this.label19.TabIndex = 12;
            this.label19.Text = "Disassembly Info";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(170, 258);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(125, 12);
            this.label20.TabIndex = 8;
            this.label20.Text = "TotalExecutedCycles:";
            // 
            // tbCycles
            // 
            this.tbCycles.Location = new System.Drawing.Point(301, 254);
            this.tbCycles.Name = "tbCycles";
            this.tbCycles.Size = new System.Drawing.Size(120, 21);
            this.tbCycles.TabIndex = 9;
            // 
            // tbR2
            // 
            this.tbR2.Location = new System.Drawing.Point(271, 120);
            this.tbR2.Name = "tbR2";
            this.tbR2.Size = new System.Drawing.Size(70, 21);
            this.tbR2.TabIndex = 4;
            // 
            // tbWZ
            // 
            this.tbWZ.Location = new System.Drawing.Point(271, 147);
            this.tbWZ.Name = "tbWZ";
            this.tbWZ.Size = new System.Drawing.Size(70, 21);
            this.tbWZ.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(164, 123);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 5;
            this.label18.Text = "RegisterR2";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(164, 150);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 5;
            this.label21.Text = "RegisterWZ";
            // 
            // z80Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnGetNew);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.tbCycles);
            this.Controls.Add(this.tbCyclesTill);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.tbPPCTill);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnStep3);
            this.Controls.Add(this.btnStep2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbPPC);
            this.Controls.Add(this.tbRPC);
            this.Controls.Add(this.tbSP);
            this.Controls.Add(this.tbIY);
            this.Controls.Add(this.tbIX);
            this.Controls.Add(this.tbWZ);
            this.Controls.Add(this.tbR);
            this.Controls.Add(this.tbShadowHL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbShadowDE);
            this.Controls.Add(this.tbR2);
            this.Controls.Add(this.tbI);
            this.Controls.Add(this.tbHL);
            this.Controls.Add(this.tbShadowBC);
            this.Controls.Add(this.tbDE);
            this.Controls.Add(this.tbShadowAF);
            this.Controls.Add(this.tbBC);
            this.Controls.Add(this.tbAF);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.tbDisassemble);
            this.Controls.Add(this.btnGet);
            this.Name = "z80Form";
            this.Text = "Z80 debugger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.z80Form_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.TextBox tbDisassemble;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox tbAF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbBC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbHL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbShadowAF;
        private System.Windows.Forms.TextBox tbShadowBC;
        private System.Windows.Forms.TextBox tbShadowDE;
        private System.Windows.Forms.TextBox tbShadowHL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbI;
        private System.Windows.Forms.TextBox tbR;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbIX;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbIY;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbSP;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbRPC;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.Button btnStep2;
        private System.Windows.Forms.Button btnStep3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbPPCTill;
        private System.Windows.Forms.TextBox tbCyclesTill;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnGetNew;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox tbPPC;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbCycles;
        private System.Windows.Forms.TextBox tbR2;
        private System.Windows.Forms.TextBox tbWZ;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
    }
}