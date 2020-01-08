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
using cpu.m6809;

namespace ui
{
    public partial class m6809Form : Form
    {
        private mainForm _myParentForm;
        //private Disassembler disassembler;
        private bool bLogNew;
        public static int iStatus;
        private int PPCTill;
        private ulong CyclesTill;
        private List<ushort> lPPC = new List<ushort>();
        public static CPUState m6809State, m6809FState;
        public m6809Form(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();            
        }
        private void m6809Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
            m6809FState = m6809State;
            m6809State = CPUState.STOP;
            GetData();
            m6809State = m6809FState;
        }
        public void GetData()
        {
            int reg, offset;
            reg = M6809.mm1[0].PPC.LowWord / 0x2000;
            offset = M6809.mm1[0].PPC.LowWord & 0x1fff;
            tbD.Text = M6809.mm1[0].D.LowWord.ToString("X4");
            tbDp.Text = M6809.mm1[0].DP.LowWord.ToString("X4");
            tbU.Text = M6809.mm1[0].U.LowWord.ToString("X4");
            tbS.Text = M6809.mm1[0].S.LowWord.ToString("X4");
            tbX.Text = M6809.mm1[0].X.LowWord.ToString("X4");
            tbY.Text = M6809.mm1[0].Y.LowWord.ToString("X4");
            tbCc.Text = M6809.mm1[0].CC.ToString("X2");
            tbIrqstate0.Text = M6809.mm1[0].irq_state[0].ToString("X2");
            tbIrqstate1.Text = M6809.mm1[0].irq_state[1].ToString("X2");
            tbIntstate.Text = M6809.mm1[0].int_state.ToString("X2");
            tbNmistate.Text = M6809.mm1[0].nmi_state.ToString("X2");
            tbPPC.Text = (Namcos1.user1rom_offset[0, reg] + offset).ToString("X6");
            tbCycles.Text = M6809.mm1[0].TotalExecutedCycles.ToString("X16");
            tbDisassemble.Text = M6809.mm1[0].m6809_dasm(M6809.mm1[0].PPC.LowWord);

        }
        private void btnStep_Click(object sender, EventArgs e)
        {
            if (m6809State == CPUState.RUN)
            {
                m6809State = CPUState.STOP;
                tsslStatus.Text = "m6809 stop";
            }
            else
            {
                m6809State = CPUState.STEP;
                tsslStatus.Text = "m6809 step";
            }
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            m6809State = CPUState.RUN;
            tsslStatus.Text = "m6809 run";
        }
        private void btnStep2_Click(object sender, EventArgs e)
        {
            try
            {
                PPCTill = int.Parse(tbPPCTill.Text, NumberStyles.HexNumber);
                m6809State = CPUState.STEP2;
                tsslStatus.Text = "m6809 step2";
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
                m6809State = CPUState.STEP3;
                tsslStatus.Text = "m6809 step3";
            }
            catch
            {
                tsslStatus.Text = "error TotalExecutedCycles";
            }
        }
        public void m6809_start_debug()
        {
            if (bLogNew && lPPC.IndexOf(M6809.mm1[0].PPC.LowWord) < 0)
            {
                m6809FState = m6809State;
                m6809State = CPUState.STOP;
                lPPC.Add(M6809.mm1[0].PPC.LowWord);
                tbResult.AppendText(M6809.mm1[0].PPC.LowWord.ToString("X4") + ": " );
                m6809State = m6809FState;
            }
            if (m6809State == CPUState.STEP2)
            {
                if (M6809.mm1[0].PPC.LowWord == PPCTill)
                {
                    m6809State = CPUState.STOP;
                }
            }
            if (m6809State == CPUState.STEP3)
            {
                if (M6809.mm1[0].TotalExecutedCycles >= CyclesTill)
                {
                    m6809State = CPUState.STOP;
                }
            }
            if (m6809State == CPUState.STOP)
            {
                GetData();
                tsslStatus.Text = "m6809 stop";
            }
            while (m6809State == CPUState.STOP)
            {
                Video.video_frame_update();
                Application.DoEvents();
            }
        }
        public void m6809_stop_debug()
        {
            if (m6809State == CPUState.STEP)
            {
                m6809State = CPUState.STOP;
                tsslStatus.Text = "m6809 stop";
            }
        }
    }
}
