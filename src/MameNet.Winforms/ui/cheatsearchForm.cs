using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using mame;
using cpu.z80;

namespace ui
{
    public partial class cheatsearchForm : Form
    {
        private mainForm _myParentForm;
        private string[] sde2 = new string[1] { "," }, sde6 = new string[1] { "-" }, sde7 = new string[1] { ";" }, sde9 = new string[1] { "$" }, sde10 = new string[] { "+" };
        private List<int[]> lSearch1 = new List<int[]>(), lSearch2 = new List<int[]>(), lSearch3 = new List<int[]>();
        private int nRam;
        private byte[] bbSearch1, bbSearch2, bbSearch3;
        private int nRamByte, nOffset;
        public Func<int, byte> CheatReadByte;
        public Action<int, byte> CheatWriteByte;
        private Func<int, int, bool> compFunc;
        private Func<byte[], int, int> GetRamValue;
        public cheatsearchForm(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void cheatsearchForm_Load(object sender, EventArgs e)
        {            
            cbRam.SelectedIndex = 0;
            cbByte.SelectedIndex = 0;
            cbMemory.SelectedIndex = 0;
            ColumnHeader columnheader;
            columnheader = new ColumnHeader();
            columnheader.Text = "address";
            columnheader.Width = 80;
            searchListView.Columns.Add(columnheader);
            columnheader = new ColumnHeader();
            columnheader.Text = "value(hex)";
            columnheader.Width = 80;
            searchListView.Columns.Add(columnheader);
            columnheader = new ColumnHeader();
            columnheader.Text = "value(dec)";
            columnheader.Width = 80;
            searchListView.Columns.Add(columnheader);
            columnheader = new ColumnHeader();
            columnheader.Text = "last value(hex)";
            columnheader.Width = 110;
            searchListView.Columns.Add(columnheader);
            searchListView.View = View.Details;
            searchListView.FullRowSelect = true;
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                case "Neo Geo":
                case "Taito B":
                case "Konami 68000":
                    nRam = 0x10000;
                    tbRamRange.Text = "0000-10000";
                    CheatReadByte = (int i1) => { return Memory.mainram[i1]; };
                    CheatWriteByte = (int i1, byte b1) => { Memory.mainram[i1]= b1; };
                    break;
                case "Data East":
                    nRam = 0x800;
                    tbRamRange.Text = "0000-0800";
                    CheatReadByte = (int i1) => { return Memory.mainram[i1]; };
                    CheatWriteByte = (int i1, byte b1) => { Memory.mainram[i1] = b1; };
                    break;
                case "Tehkan":
                    nRam = 0xd000;
                    tbRamRange.Text = "0000-d000";
                    CheatReadByte = (int i1) => { return Z80A.zz1[0].ReadMemory((ushort)i1); };
                    CheatWriteByte = (int i1, byte b1) => { Z80A.zz1[0].WriteMemory((ushort)i1, b1); };
                    break;
                case "Namco System 1":
                    nRam = 0x10000;
                    tbRamRange.Text = "0000-10000";
                    CheatReadByte = (int i1) => { return Namcos1.N0ReadMemory((ushort)i1); };
                    CheatWriteByte = (int i1, byte b1) => { Namcos1.N0WriteMemory((ushort)i1, b1); };
                    break;
                case "IGS011":
                case "PGM":
                    break;
                case "Capcom":
                    nRam = 0x6000;
                    tbRamRange.Text = "0000-6000";
                    CheatReadByte = (int i1) => { return Memory.mainram[i1]; };
                    CheatWriteByte = (int i1, byte b1) => { Memory.mainram[i1] = b1; };
                    break;
            }
            bbSearch1 = new byte[nRam];
            bbSearch2 = new byte[nRam];
            bbSearch3 = new byte[nRam];
        }
        private void cheatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private int G1Byte(byte[] bb1,int i1)
        {
            return bb1[i1];
        }
        private int G2Byte(byte[] bb1, int i1)
        {
            return (short)(bb1[i1] * 0x100 + bb1[i1 + 1]);
        }
        private int G4Byte(byte[] bb1, int i1)
        {
            return bb1[i1] * 0x1000000 + bb1[i1 + 1] * 0x10000 + bb1[i1 + 2] * 0x100 + bb1[i1 + 3];
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (lSearch3.Count == 0)
            {
                nRamByte = int.Parse(cbByte.Text);
                if (nRamByte == 1)
                {
                    GetRamValue = G1Byte;
                    nOffset = 1;
                }
                else if (nRamByte == 2)
                {
                    GetRamValue = G2Byte;
                    nOffset = 2;
                }
                else if (nRamByte == 4)
                {
                    GetRamValue = G4Byte;
                    nOffset = 2;
                }
                string[] ss1, ss2;
                ss1 = tbRamRange.Text.Split(sde2, StringSplitOptions.RemoveEmptyEntries);
                int i1, i2, i3, i4, n1, n2, n3;
                n2 = ss1.Length;
                lSearch1.Clear();
                lSearch2.Clear();
                cbByte.Enabled = false;
                tbRamRange.Enabled = false;
                for (i1 = 0; i1 < nRam; i1++)
                {
                    bbSearch1[i1] = CheatReadByte(i1);
                }
                for (i1 = 0; i1 < n2; i1++)
                {
                    ss2 = ss1[i1].Split(sde6, StringSplitOptions.RemoveEmptyEntries);
                    n3 = ss2.Length;
                    if (n3 == 1)
                    {
                        i3 = Convert.ToInt32(ss2[0], 16);
                        i4 = i3 + nRamByte;
                    }
                    else
                    {
                        i3 = Convert.ToInt32(ss2[0], 16);
                        i4 = Convert.ToInt32(ss2[1], 16);
                    }
                    for (i2 = i3; i2 <= i4 - nRamByte; i2 += nOffset)
                    {
                        lSearch1.Add(new int[2] { i2, GetRamValue(bbSearch1,i2) });
                    }
                }
                bbSearch3 = new byte[bbSearch1.Length];
                if (lSearch1.Count > 300)
                {
                    n1 = 300;
                }
                else
                {
                    n1 = lSearch1.Count;
                }
                searchListView.Items.Clear();
                ListViewItem listviewitem;
                for (i1 = 0; i1 < n1; i1++)
                {
                    listviewitem = new ListViewItem(lSearch1[i1][0].ToString("X4"));
                    listviewitem.SubItems.Add(Convert.ToInt32(lSearch1[i1][1]).ToString("X" + nRamByte * 2));
                    listviewitem.SubItems.Add(lSearch1[i1][1].ToString());
                    searchListView.Items.Add(listviewitem);
                }
                tbRamCount.Text = lSearch1.Count.ToString();
                bbSearch1.CopyTo(bbSearch3, 0);
                lSearch3 = new List<int[]>(lSearch1.ToArray());
            }
            else
            {
                if (cbRam.SelectedIndex == 0)//?
                {
                    int i1, n1;
                    for (i1 = 0; i1 < nRam; i1++)
                    {
                        bbSearch2[i1] = CheatReadByte(i1);
                    }
                    lSearch2.Clear();
                    for (i1 = 0; i1 < lSearch1.Count; i1++)
                    {
                        lSearch2.Add(new int[3] { lSearch1[i1][0], GetRamValue(bbSearch2, lSearch1[i1][0]), GetRamValue(bbSearch1, lSearch1[i1][0]) });
                    }
                    bbSearch3 = new byte[bbSearch2.Length];
                    if (lSearch2.Count > 300)
                    {
                        n1 = 300;
                    }
                    else
                    {
                        n1 = lSearch2.Count;
                    }
                    searchListView.Items.Clear();
                    ListViewItem listviewitem;
                    for (i1 = 0; i1 < n1; i1++)
                    {
                        listviewitem = new ListViewItem(lSearch2[i1][0].ToString("X4"));
                        listviewitem.SubItems.Add(Convert.ToInt16(lSearch2[i1][1]).ToString("X" + 2 * nRamByte));
                        listviewitem.SubItems.Add(lSearch2[i1][1].ToString());
                        listviewitem.SubItems.Add(Convert.ToInt16(lSearch2[i1][2]).ToString("X" + 2 * nRamByte));
                        searchListView.Items.Add(listviewitem);
                    }
                    tbRamCount.Text = lSearch2.Count.ToString();
                    bbSearch1.CopyTo(bbSearch3, 0);
                    bbSearch2.CopyTo(bbSearch1, 0);
                    lSearch3 = new List<int[]>(lSearch1.ToArray());
                    lSearch1 = new List<int[]>(lSearch2.ToArray());
                }
                else if (cbRam.SelectedIndex >= 1 && cbRam.SelectedIndex <= 4)//! = + -
                {
                    if (cbRam.SelectedIndex == 1)
                    {
                        compFunc = notequal;
                    }
                    else if (cbRam.SelectedIndex == 2)
                    {
                        compFunc = equal;
                    }
                    else if (cbRam.SelectedIndex == 3)
                    {
                        compFunc = great;
                    }
                    else if (cbRam.SelectedIndex == 4)
                    {
                        compFunc = less;
                    }
                    int i1, n2;
                    lSearch2.Clear();
                    for (i1 = 0; i1 < nRam; i1++)
                    {
                        bbSearch2[i1] = CheatReadByte(i1);
                    }
                    for (i1 = 0; i1 < lSearch1.Count; i1++)
                    {
                        if (compFunc(GetRamValue(bbSearch2, lSearch1[i1][0]), GetRamValue(bbSearch1, lSearch1[i1][0])))
                        {
                            lSearch2.Add(new int[3] { lSearch1[i1][0], GetRamValue(bbSearch2, lSearch1[i1][0]), GetRamValue(bbSearch1, lSearch1[i1][0]) });
                        }
                    }
                    if (lSearch2.Count > 300)
                    {
                        n2 = 300;
                    }
                    else
                    {
                        n2 = lSearch2.Count;
                    }
                    searchListView.Items.Clear();
                    ListViewItem listviewitem;
                    for (i1 = 0; i1 < n2; i1++)
                    {
                        listviewitem = new ListViewItem(lSearch2[i1][0].ToString("X4"));
                        listviewitem.SubItems.Add(Convert.ToInt32(lSearch2[i1][1]).ToString("X" + 2 * nRamByte));
                        listviewitem.SubItems.Add(lSearch2[i1][1].ToString());
                        listviewitem.SubItems.Add(Convert.ToInt32(lSearch2[i1][2]).ToString("X" + 2 * nRamByte));
                        searchListView.Items.Add(listviewitem);
                    }
                    tbRamCount.Text = lSearch2.Count.ToString();
                    bbSearch1.CopyTo(bbSearch3, 0);
                    bbSearch2.CopyTo(bbSearch1, 0);
                    lSearch3 = new List<int[]>(lSearch1.ToArray());
                    lSearch1 = new List<int[]>(lSearch2.ToArray());
                }
                else if (cbRam.SelectedIndex >= 5 && cbRam.SelectedIndex <= 7)//=? >? <?
                {
                    if (cbRam.SelectedIndex == 5)
                    {
                        compFunc = equal;
                    }
                    else if (cbRam.SelectedIndex == 6)
                    {
                        compFunc = great;
                    }
                    else if (cbRam.SelectedIndex == 7)
                    {
                        compFunc = less;
                    }
                    int i1, n2;
                    int iCompRam;
                    iCompRam = Convert.ToInt32(tbRamValue.Text, 16);
                    lSearch2.Clear();
                    for (i1 = 0; i1 < nRam; i1++)
                    {
                        bbSearch2[i1] = CheatReadByte(i1);
                    }
                    for (i1 = 0; i1 < lSearch1.Count; i1++)
                    {
                        if (compFunc(Convert.ToInt32(GetRamValue(bbSearch2, lSearch1[i1][0])), iCompRam))
                        {
                            lSearch2.Add(new int[3] { lSearch1[i1][0], GetRamValue(bbSearch2, lSearch1[i1][0]), GetRamValue(bbSearch1, lSearch1[i1][0]) });
                        }
                    }
                    if (lSearch2.Count > 300)
                    {
                        n2 = 300;
                    }
                    else
                    {
                        n2 = lSearch2.Count;
                    }
                    searchListView.Items.Clear();
                    ListViewItem listviewitem;
                    for (i1 = 0; i1 < n2; i1++)
                    {
                        listviewitem = new ListViewItem(lSearch2[i1][0].ToString("X4"));
                        listviewitem.SubItems.Add(Convert.ToInt32(lSearch2[i1][1]).ToString("X" + 2 * nRamByte));
                        listviewitem.SubItems.Add(lSearch2[i1][1].ToString());
                        listviewitem.SubItems.Add(Convert.ToInt32(lSearch2[i1][2]).ToString("X" + 2 * nRamByte));
                        searchListView.Items.Add(listviewitem);
                    }
                    tbRamCount.Text = lSearch2.Count.ToString();
                    bbSearch1.CopyTo(bbSearch3, 0);
                    bbSearch2.CopyTo(bbSearch1, 0);
                    lSearch3 = new List<int[]>(lSearch1.ToArray());
                    lSearch1 = new List<int[]>(lSearch2.ToArray());
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            lSearch1.Clear();
            lSearch2.Clear();
            lSearch3.Clear();
            searchListView.Items.Clear();
            cbByte.Enabled = true;
            tbRamRange.Enabled = true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            StreamWriter sw1 = new StreamWriter("cheat1.txt");
            foreach (int[] ii1 in lSearch2)
            {
                sw1.WriteLine(ii1[0].ToString("X4") + "\t" + ii1[1].ToString("X2") + "\t" + ii1[1] + "\t" + ii1[2].ToString("X2"));
            }
            sw1.Close();
        }
        private bool notequal(int i1, int i2)
        {
            if (i1 != i2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool equal(int i1, int i2)
        {
            if (i1 == i2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool great(int i1, int i2)
        {
            if (i1 > i2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool less(int i1, int i2)
        {
            if (i1 < i2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public byte[] ByteSwap(byte[] bb1)
        {
            int i1, n1;
            n1 = bb1.Length;
            if (bb1.Length % 2 == 1)
            {
                return null;
            }
            else
            {
                byte[] bb2 = new byte[n1];
                for (i1 = 0; i1 < n1; i1 += 2)
                {
                    bb2[i1] = bb1[i1 + 1];
                    bb2[i1 + 1] = bb1[i1];
                }
                return bb2;
            }
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            string[] ss1, ss2;
            int i;
            int iAddress, iOffsetAddress1, iOffsetAddress2, iValue1, n1;
            ss1 = tbRead.Text.Split(sde2, StringSplitOptions.RemoveEmptyEntries);
            n1 = ss1.Length;
            if (cbMemory.SelectedIndex == 0)//mainram
            {
                for (i = 0; i < n1; i++)
                {
                    if (ss1[i].IndexOf("$") >= 0)
                    {
                        ss2 = ss1[i].Split(sde9, StringSplitOptions.RemoveEmptyEntries);
                        iOffsetAddress1 = Convert.ToInt32(ss2[0], 16);
                        iOffsetAddress2 = Convert.ToInt32(ss2[1], 16);
                        //iAddress = Memory.mainram[iOffsetAddress1] * 0x100 + Memory.mainram[iOffsetAddress1 + 1] + iOffsetAddress2;
                        iAddress = CheatReadByte(iOffsetAddress1) * 0x100 + CheatReadByte(iOffsetAddress1 + 1) + iOffsetAddress2;
                    }
                    else if (ss1[i].IndexOf("+") >= 0)
                    {
                        ss2 = ss1[i].Split(sde10, StringSplitOptions.RemoveEmptyEntries);
                        iOffsetAddress1 = Convert.ToInt32(ss2[0], 16);
                        iOffsetAddress2 = Convert.ToInt32(ss2[1], 16);
                        iAddress = iOffsetAddress1 + iOffsetAddress2;
                    }
                    else
                    {
                        iAddress = Convert.ToInt32(ss1[i], 16);
                    }
                    iValue1 = CheatReadByte(iAddress);
                    if (i % 16 == 15)
                    {
                        tbResult.AppendText(iValue1.ToString("X2") + "\r\n");
                    }
                    else
                    {
                        tbResult.AppendText(iValue1.ToString("X2") + " ");
                    }
                }
            }
            else if (cbMemory.SelectedIndex == 1)//mainrom
            {
                for (i = 0; i < n1; i++)
                {
                    if (ss1[i].IndexOf("+") >= 0)
                    {
                        ss2 = ss1[i].Split(sde10, StringSplitOptions.RemoveEmptyEntries);
                        iOffsetAddress1 = Convert.ToInt32(ss2[0], 16);
                        iOffsetAddress2 = Convert.ToInt32(ss2[1], 16);
                        iAddress = iOffsetAddress1 + iOffsetAddress2;
                    }
                    else
                    {
                        iAddress = Convert.ToInt32(ss1[i], 16);
                    }
                    iValue1 = Memory.mainrom[iAddress];
                    if (i % 16 == 15)
                    {
                        tbResult.AppendText(iValue1.ToString("X2") + "\r\n");
                    }
                    else
                    {
                        tbResult.AppendText(iValue1.ToString("X2") + " ");
                    }
                }
            }
            tbResult.AppendText("\r\n");
        }
        private void btnWrite_Click(object sender, EventArgs e)
        {
            string[] ss1, ss2, ss3;
            int i;
            int iAddress, iOffsetAddress1, iOffsetAddress2, iValue1, iValue2, n1;
            ss1 = tbWrite.Text.Split(sde7, StringSplitOptions.RemoveEmptyEntries);
            n1 = ss1.Length;
            if (cbMemory.SelectedIndex == 0)
            {
                for (i = 0; i < n1; i++)
                {
                    ss3 = ss1[i].Split(sde2, StringSplitOptions.RemoveEmptyEntries);
                    if (ss3[0].IndexOf("$") >= 0)
                    {
                        ss2 = ss3[0].Split(sde9, StringSplitOptions.RemoveEmptyEntries);
                        iOffsetAddress1 = Convert.ToInt32(ss2[0], 16);
                        iOffsetAddress2 = Convert.ToInt32(ss2[1], 16);
                        iAddress = CheatReadByte(iOffsetAddress1) * 0x100 + CheatReadByte(iOffsetAddress1 + 1) + iOffsetAddress2;
                    }
                    else if (ss3[0].IndexOf("+") >= 0)
                    {
                        ss2 = ss3[0].Split(sde10, StringSplitOptions.RemoveEmptyEntries);
                        iOffsetAddress1 = Convert.ToInt32(ss2[0], 16);
                        iOffsetAddress2 = Convert.ToInt32(ss2[1], 16);
                        iAddress = iOffsetAddress1 + iOffsetAddress2;
                    }
                    else
                    {
                        iAddress = Convert.ToInt32(ss3[0], 16);
                    }
                    iValue1 = CheatReadByte(iAddress);
                    iValue2 = Convert.ToInt32(ss3[1], 16);
                    CheatWriteByte(iAddress, (byte)iValue2);
                    tbResult.AppendText(iAddress.ToString("X4") + " " + iValue1.ToString("X2") + " " + iValue2.ToString("X2") + "\r\n");
                }
            }
            else if (cbMemory.SelectedIndex == 1)
            {
                for (i = 0; i < n1; i++)
                {
                    ss3 = ss1[i].Split(sde2, StringSplitOptions.RemoveEmptyEntries);
                    if (ss3[0].IndexOf("+") >= 0)
                    {
                        ss2 = ss3[0].Split(sde10, StringSplitOptions.RemoveEmptyEntries);
                        iOffsetAddress1 = Convert.ToInt32(ss2[0], 16);
                        iOffsetAddress2 = Convert.ToInt32(ss2[1], 16);
                        iAddress = iOffsetAddress1 + iOffsetAddress2;
                    }
                    else
                    {
                        iAddress = Convert.ToInt32(ss3[0], 16);
                    }
                    iValue1 = Memory.mainrom[iAddress];
                    iValue2 = Convert.ToInt32(ss3[1], 16);
                    Memory.mainrom[iAddress] = (byte)iValue2;
                    tbResult.AppendText(iAddress.ToString("X4") + " " + iValue1.ToString("X2") + " " + iValue2.ToString("X2") + "\r\n");
                }
            }
        }
    }
}