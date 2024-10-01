using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using mame;
using cpu.z80;

namespace ui
{
    public enum CPUState
    {
        NONE = 0,
        RUN,
        STEP,
        STEP2,
        STEP3,
        STOP,
    }
    public partial class z80Form : Form
    {
        private mainForm _myParentForm;
        private Disassembler disassembler;
        private bool bLogNew;
        public static int iStatus;
        private int PPCTill;
        private ulong CyclesTill;
        private List<ushort> lPPC = new List<ushort>();
        public enum Z80AState
        {
            Z80A_NONE = 0,
            Z80A_RUN,
            Z80A_STEP,
            Z80A_STEP2,
            Z80A_STEP3,
            Z80A_STOP,
        }
        public static Z80AState z80State, z80FState;
        public z80Form(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
            disassembler= new Disassembler();
            Disassembler.GenerateOpcodeSizes();
        }
        private void z80Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
            z80FState = z80State;
            z80State = Z80AState.Z80A_STOP;
            GetData();
            z80State = z80FState;
        }
        public void GetData()
        {
            string sDisassemble;
            tbAF.Text = Z80A.zz1[0].RegisterAF.ToString("X4");
            tbBC.Text = Z80A.zz1[0].RegisterBC.ToString("X4");
            tbDE.Text = Z80A.zz1[0].RegisterDE.ToString("X4");
            tbHL.Text = Z80A.zz1[0].RegisterHL.ToString("X4");
            tbShadowAF.Text = Z80A.zz1[0].RegisterShadowAF.ToString("X4");
            tbShadowBC.Text = Z80A.zz1[0].RegisterShadowBC.ToString("X4");
            tbShadowDE.Text = Z80A.zz1[0].RegisterShadowDE.ToString("X4");
            tbShadowHL.Text = Z80A.zz1[0].RegisterShadowHL.ToString("X4");
            tbI.Text = Z80A.zz1[0].RegisterI.ToString("X2");
            tbR.Text = Z80A.zz1[0].RegisterR.ToString("X2");
            tbIX.Text = Z80A.zz1[0].RegisterIX.ToString("X4");
            tbIY.Text = Z80A.zz1[0].RegisterIY.ToString("X4");
            tbSP.Text = Z80A.zz1[0].RegisterSP.ToString("X4");
            tbRPC.Text = Z80A.zz1[0].RegisterPC.ToString("X4");
            tbPPC.Text = Z80A.zz1[0].PPC.ToString("X4");
            tbR2.Text = Z80A.zz1[0].RegisterR2.ToString("X2");
            tbWZ.Text = Z80A.zz1[0].RegisterWZ.ToString("X4");
            tbCycles.Text = Z80A.zz1[0].TotalExecutedCycles.ToString("X16");
            sDisassemble = disassembler.GetDisassembleInfo(Z80A.zz1[0].PPC);
            tbDisassemble.Text = sDisassemble;
            tbResult.AppendText(sDisassemble + "\r\n");
        }
        private void btnStep_Click(object sender, EventArgs e)
        {
            if (z80State == Z80AState.Z80A_RUN)
            {
                z80State = Z80AState.Z80A_STOP;
                tsslStatus.Text = "z80 stop";
            }
            else
            {
                z80State = Z80AState.Z80A_STEP;
                tsslStatus.Text = "z80 step";
            }
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            z80State = Z80AState.Z80A_RUN;
            tsslStatus.Text = "z80 run";
        }
        private void btnStep2_Click(object sender, EventArgs e)
        {
            try
            {
                PPCTill = int.Parse(tbPPCTill.Text, NumberStyles.HexNumber);
                z80State = Z80AState.Z80A_STEP2;
                tsslStatus.Text = "z80 step2";
            }
            catch
            {
                tsslStatus.Text = "error PPC";
            }
        }
        private void btnStep3_Click(object sender, EventArgs e)
        {
            try
            {
                CyclesTill = ulong.Parse(tbCyclesTill.Text, NumberStyles.HexNumber);
                z80State = Z80AState.Z80A_STEP3;
                tsslStatus.Text = "z80 step3";
            }
            catch
            {
                tsslStatus.Text = "error TotalExecutedCycles";
            }
        }
        private void btnGetNew_Click(object sender, EventArgs e)
        {
            if (btnGetNew.Text == "get new")
            {
                btnGetNew.Text = "stop";
                lPPC = new List<ushort>();
                bLogNew = true;
            }
            else if (btnGetNew.Text == "stop")
            {
                btnGetNew.Text = "get new";
                bLogNew = false;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbResult.Clear();
        }
        public void z80_start_debug()
        {
            if (bLogNew && lPPC.IndexOf(Z80A.zz1[0].PPC) < 0)
            {
                z80FState = z80State;
                z80State = Z80AState.Z80A_STOP;
                lPPC.Add(Z80A.zz1[0].PPC);
                tbResult.AppendText(Z80A.zz1[0].PPC.ToString("X4") + ": " + disassembler.GetDisassembleInfo(Z80A.zz1[0].PPC) + "\r\n");
                z80State = z80FState;
            }
            if (z80State == Z80AState.Z80A_STEP2)
            {
                if (Z80A.zz1[0].PPC == PPCTill)
                {
                    z80State = Z80AState.Z80A_STOP;
                }
            }
            if (z80State == Z80AState.Z80A_STEP3)
            {
                if (Z80A.zz1[0].TotalExecutedCycles >= CyclesTill)
                {
                    z80State = Z80AState.Z80A_STOP;
                }
            }
            if (z80State == Z80AState.Z80A_STOP)
            {
                GetData();
                tsslStatus.Text = "z80 stop";
            }
            while (z80State == Z80AState.Z80A_STOP)
            {
                
            }
        }
        public void z80_stop_debug()
        {
            if (z80State == Z80AState.Z80A_STEP)
            {
                z80State = Z80AState.Z80A_STOP;
                tsslStatus.Text = "z80 stop";
            }
        }
    }
}
