using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using cpu.m68000;
using cpu.nec;
using mame;

namespace ui
{
    public partial class cheatForm : Form
    {
        public enum LockState
        {
            LOCK_NONE = 0,
            LOCK_SECOND,
            LOCK_FRAME,
        }
        private mainForm _myParentForm;
        public LockState lockState = LockState.LOCK_NONE;
        public List<int[]> lsCheatdata1;
        public List<int[]> lsCheatdata2;
        private string[] sde6 = new string[1] { "," }, sde7 = new string[1] { ";" }, sde9 = new string[1] { "$" }, sde10 = new string[] { "+" };
        public Func<int, byte> CheatReadByte;
        public Action<int, byte> CheatWriteByte;
        public cheatForm(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void cheatForm_Load(object sender, EventArgs e)
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                case "Data East":
                case "Tehkan":
                case "Neo Geo":
                case "Taito B":
                case "Konami 68000":
                case "Capcom":
                    CheatReadByte = (int i1) => { return Memory.mainram[i1]; };
                    CheatWriteByte = (int i1, byte b1) => { Memory.mainram[i1] = b1; };
                    break;
                case "Namco System 1":
                    CheatReadByte = (int i1) => { return Namcos1.N0ReadMemory((ushort)i1); };
                    CheatWriteByte = (int i1, byte b1) => { Namcos1.N0WriteMemory((ushort)i1, b1); };
                    break;
                case "IGS011":
                    CheatReadByte = (int i1) => { return (byte)MC68000.m1.ReadByte(i1); };
                    CheatWriteByte = (int i1, byte b1) => { MC68000.m1.WriteByte(i1, (sbyte)b1); };
                    break;
                case "PGM":
                    CheatReadByte = (int i1) => { return (byte)MC68000.m1.ReadByte(i1); };
                    CheatWriteByte = (int i1, byte b1) => { MC68000.m1.WriteByte(i1, (sbyte)b1); };
                    break;
                case "M72":
                case "M92":
                    CheatReadByte = (int i1) => { return Nec.nn1[0].ReadByte(i1); };
                    CheatWriteByte = (int i1, byte b1) => { Nec.nn1[0].WriteByte(i1, b1); };
                    break;                
            }
        }
        private void cbCht_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewControl1.LoadSetting("cht/" + cbCht.Text + ".cht");
        }
        private void GetCheatdata()
        {
            lsCheatdata1 = new List<int[]>();
            lsCheatdata2 = new List<int[]>();
            int i1, i2, i3, iAddress, iOffsetAddress1, iOffsetAddress2, iValue2, n3;
            string[] ss1, ss2, ss3;
            foreach (ListViewItem item1 in listViewControl1.myListView.Items)
            {
                if (item1.Checked)
                {
                    i1 = listViewControl1.myListView.Items.IndexOf(item1);
                    i2 = Array.IndexOf(listViewControl1.ssCItem[i1], item1.SubItems[1].Text);
                    ss1 = listViewControl1.ssCValue[i1][i2].Split(sde7, StringSplitOptions.RemoveEmptyEntries);
                    n3 = ss1.Length;
                    for (i3 = 0; i3 < n3; i3++)
                    {
                        ss3 = ss1[i3].Split(sde6, StringSplitOptions.RemoveEmptyEntries);
                        iValue2 = Convert.ToInt32(ss3[1], 16);
                        if (ss3[0].IndexOf("$") >= 0)
                        {
                            ss2 = ss3[0].Split(sde9, StringSplitOptions.RemoveEmptyEntries);
                            iOffsetAddress1 = Convert.ToInt32(ss2[0], 16);
                            iOffsetAddress2 = Convert.ToInt32(ss2[1], 16);
                            lsCheatdata1.Add(new int[] { iOffsetAddress1, iOffsetAddress2, iValue2 });
                        }
                        else if (ss3[0].IndexOf("+") >= 0)
                        {
                            ss2 = ss3[0].Split(sde10, StringSplitOptions.RemoveEmptyEntries);
                            iOffsetAddress1 = Convert.ToInt32(ss2[0], 16);
                            iOffsetAddress2 = Convert.ToInt32(ss2[1], 16);
                            iAddress = iOffsetAddress1 + iOffsetAddress2;
                            lsCheatdata2.Add(new int[] { iAddress, iValue2 });
                        }
                        else
                        {
                            iAddress = Convert.ToInt32(ss3[0], 16);
                            lsCheatdata2.Add(new int[] { iAddress, iValue2 });
                        }
                    }
                }
            }
        }
        public void ApplyCheat()
        {
            int iAddress, iValue1;
            foreach (int[] ii1 in lsCheatdata1)
            {
                //iAddress = bbMem[ii1[0]] * 0x100 + bbMem[ii1[0] + 1] + ii1[1];
                //iValue1 = bbMem[iAddress];
                //bbMem[iAddress] = (byte)ii1[2];
                iAddress = CheatReadByte(ii1[0]) * 0x100 + CheatReadByte(ii1[0] + 1) + ii1[1];
                iValue1 = CheatReadByte(iAddress);
                CheatWriteByte(iAddress, (byte)ii1[2]);
                if (iValue1 != ii1[2])
                {
                    tbResult.AppendText(iAddress.ToString("X4") + " " + iValue1.ToString("X2") + " " + ii1[2].ToString("X2") + "\r\n");
                }
            }
            foreach (int[] ii1 in lsCheatdata2)
            {
                iAddress = ii1[0];
                //iValue1 = bbMem[iAddress];
                //bbMem[ii1[0]] = (byte)ii1[1];
                iValue1 = CheatReadByte(iAddress);
                CheatWriteByte(ii1[0], (byte)ii1[1]);
                if (iValue1 != ii1[1])
                {
                    tbResult.AppendText(ii1[0].ToString("X4") + " " + iValue1.ToString("X2") + " " + ii1[1].ToString("X2") + "\r\n");
                }
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            GetCheatdata();
            ApplyCheat();
        }
        private void btnLockS_Click(object sender, EventArgs e)
        {
            if (btnLockS.Text == "lock sec")
            {
                GetCheatdata();
                lockState = LockState.LOCK_SECOND;
                btnLockS.Text = "unlock";
                btnLockF.Enabled = false;
            }
            else if (btnLockS.Text == "unlock")
            {
                lockState = LockState.LOCK_NONE;
                btnLockS.Text = "lock sec";
                btnLockF.Enabled = true;
            }
        }
        private void btnLockF_Click(object sender, EventArgs e)
        {
            if (btnLockF.Text == "lock frm")
            {
                GetCheatdata();
                lockState = LockState.LOCK_FRAME;
                btnLockF.Text = "unlock";
                btnLockS.Enabled = false;
            }
            else if (btnLockF.Text == "unlock")
            {
                lockState = LockState.LOCK_NONE;
                btnLockF.Text = "lock frm";
                btnLockS.Enabled = true;
            }
        }
        private void cheatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}