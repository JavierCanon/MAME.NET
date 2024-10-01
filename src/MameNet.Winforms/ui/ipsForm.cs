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

namespace ui
{
    public partial class ipsForm : Form
    {
        private mainForm _myParentForm;
        private string[] sde6 = new string[1] { "," }, sde7 = new string[1] { ";" }, sde9 = new string[1] { "$" }, sde10 = new string[] { "+" };
        private Label[] llb;
        private TextBox[] ttb;
        public ipsForm(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void ipsForm_Load(object sender, EventArgs e)
        {
            int i, n;
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":                    
                    n = 3;
                    llb = new Label[n];
                    ttb = new TextBox[n];
                    for (i = 0; i < n; i++)
                    {
                        llb[i] = new Label();
                        llb[i].Location = new Point(12, 347 + 27 * i);
                        llb[i].Size = new Size(41, 12);
                        Controls.Add(llb[i]);
                        ttb[i] = new TextBox();
                        ttb[i].Location = new Point(59, 344 + 27 * i);
                        ttb[i].Size = new Size(70, 21);
                        ttb[i].TabIndex = 13 + i;
                        Controls.Add(ttb[i]);
                    }
                    llb[0].Text = "sw(a):";
                    llb[1].Text = "sw(b):";
                    llb[2].Text = "sw(c):";
                    ttb[0].Text = Convert.ToString(CPS.dswa, 16);
                    ttb[1].Text = Convert.ToString(CPS.dswb, 16);
                    ttb[2].Text = Convert.ToString(CPS.dswc, 16);
                    break;
                case "Tehkan":
                    n = 2;
                    llb = new Label[n];
                    ttb = new TextBox[n];
                    for (i = 0; i < n; i++)
                    {
                        llb[i] = new Label();
                        llb[i].Location = new Point(12, 347 + 27 * i);
                        llb[i].Size = new Size(41, 12);
                        Controls.Add(llb[i]);
                        ttb[i] = new TextBox();
                        ttb[i].Location = new Point(59, 344 + 27 * i);
                        ttb[i].Size = new Size(70, 21);
                        ttb[i].TabIndex = 13 + i;
                        Controls.Add(ttb[i]);
                    }
                    llb[0].Text = "DSW1:";
                    llb[1].Text = "DSW2:";
                    ttb[0].Text = Convert.ToString(Tehkan.dsw1, 16);
                    ttb[1].Text = Convert.ToString(Tehkan.dsw2, 16);
                    break;
                case "Neo Geo":
                    n = 1;
                    llb = new Label[n];
                    ttb = new TextBox[n];
                    for (i = 0; i < n; i++)
                    {
                        llb[i] = new Label();
                        llb[i].Location = new Point(12, 347 + 27 * i);
                        llb[i].Size = new Size(41, 12);
                        Controls.Add(llb[i]);
                        ttb[i] = new TextBox();
                        ttb[i].Location = new Point(59, 344 + 27 * i);
                        ttb[i].Size = new Size(70, 21);
                        ttb[i].TabIndex = 13 + i;
                        Controls.Add(ttb[i]);
                    }
                    llb[0].Text = "sw:";
                    ttb[0].Text = Convert.ToString(Neogeo.dsw, 16);
                    break;
                case "Namco System 1":
                    n = 1;
                    llb = new Label[n];
                    ttb = new TextBox[n];
                    for (i = 0; i < n; i++)
                    {
                        llb[i] = new Label();
                        llb[i].Location = new Point(12, 347 + 27 * i);
                        llb[i].Size = new Size(41, 12);
                        Controls.Add(llb[i]);
                        ttb[i] = new TextBox();
                        ttb[i].Location = new Point(59, 344 + 27 * i);
                        ttb[i].Size = new Size(70, 21);
                        ttb[i].TabIndex = 13 + i;
                        Controls.Add(ttb[i]);
                    }
                    llb[0].Text = "dipsw:";
                    ttb[0].Text = Convert.ToString(Namcos1.dipsw, 16);
                    break;
                case "IGS011":
                case "PGM":
                case "M72":
                case "M92":
                    break;
                case "Taito B":
                case "Konami 68000":
                case "Capcom":
                    break;
            }
        }
        private void cbCht_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewControl1.LoadSetting("ips/" + cbCht.Text + ".cht");
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            int i1, i2, i3, iAddress, iOffsetAddress1, iOffsetAddress2, iValue1, iValue2, n3;
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
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                    CPS.dswa = byte.Parse(ttb[0].Text, NumberStyles.HexNumber);
                    CPS.dswb = byte.Parse(ttb[1].Text, NumberStyles.HexNumber);
                    CPS.dswc = byte.Parse(ttb[2].Text, NumberStyles.HexNumber);
                    break;
                case "Tehkan":
                    Tehkan.dsw1 = byte.Parse(ttb[0].Text, NumberStyles.HexNumber);
                    Tehkan.dsw2 = byte.Parse(ttb[1].Text, NumberStyles.HexNumber);
                    break;
                case "Neo Geo":
                    Neogeo.dsw = byte.Parse(ttb[0].Text, NumberStyles.HexNumber);
                    break;
                case "Namco System 1":
                    Namcos1.dipsw = byte.Parse(ttb[0].Text, NumberStyles.HexNumber);
                    break;
                case "IGS011":
                case "PGM":
                case "M72":
                case "M92":
                    break;
                case "Taito B":
                case "Konami 68000":
                case "Capcom":
                    break;
            }
        }
    }
}
