using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using mame;
using cpu.m68000;

namespace ui
{
    public partial class cpsForm : Form
    {
        private mainForm _myParentForm;
        private string[] sde2 = new string[] { "," };
        private int locationX, locationY;
        public cpsForm(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void cpsForm_Load(object sender, EventArgs e)
        {
            cbL0.Checked = true;
            cbL1.Checked = true;
            cbL2.Checked = true;
            cbL3.Checked = true;
            tbFile.Text = "00";
            tbPoint.Text = "512,512,0,256";
            cbLayer.SelectedIndex = 0;
            tbCode.Text = "0000";
            tbColor.Text = "00";
        }
        private void cpsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void GetData()
        {
            tbScroll1x.Text = CPS.scroll1x.ToString("X4");
            tbScroll1y.Text = CPS.scroll1y.ToString("X4");
            tbScroll2x.Text = CPS.scroll2x.ToString("X4");
            tbScroll2y.Text = CPS.scroll2y.ToString("X4");
            tbScroll3x.Text = CPS.scroll3x.ToString("X4");
            tbScroll3y.Text = CPS.scroll3y.ToString("X4");
            tbScrollsx.Text = CPS.scrollxSG.ToString();
            tbScrollsy.Text = CPS.scrollySG.ToString();
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            GetData();
            CPS.bRender0G = cbL0.Checked;
            CPS.bRender1G = cbL1.Checked;
            CPS.bRender2G = cbL2.Checked;
            CPS.bRender3G = cbL3.Checked;
            CPS.GetData();
            Bitmap bm1 = CPS.GetAllGDI();
            pictureBox1.Width = bm1.Width;
            pictureBox1.Height = bm1.Height;
            pictureBox1.Image = bm1;
            tbL0.Text = CPS.l0G.ToString();
            tbL1.Text = CPS.l1G.ToString();
            tbL2.Text = CPS.l2G.ToString();
            tbL3.Text = CPS.l3G.ToString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Bitmap bm1, bm3;
            int width1,width2, height1, height2,widthall,heightall;
            string[] ss1 = tbPoint.Text.Split(sde2, StringSplitOptions.RemoveEmptyEntries);
            bm1 = (Bitmap)pictureBox1.Image;
            widthall = int.Parse(ss1[0]);
            heightall = int.Parse(ss1[1]);
            width1 = int.Parse(ss1[2]);
            width2 = widthall - width1;
            height1 = int.Parse(ss1[3]);
            height2 = heightall - height1;
            bm3 = new Bitmap(width2, height2);
            Graphics g3 = Graphics.FromImage(bm3);
            Point[] destPoints3 = { new Point(-width1, -height1), new Point(width2, -height1), new Point(-width1, height2) };
            g3.DrawImage(bm1, destPoints3);
            g3.Dispose();
            bm3.Save(tbFile.Text + ".png", ImageFormat.Png);
        }        
        private void btnSet1X_Click(object sender, EventArgs e)
        {
            CPS.scroll1x = int.Parse(tbScroll1x.Text, NumberStyles.HexNumber);
            CPS.cps_a_regs[CPS.CPS1_SCROLL1_SCROLLX] = ushort.Parse(tbScroll1x.Text, NumberStyles.HexNumber);
        }
        private void btnSet1Y_Click(object sender, EventArgs e)
        {
            CPS.scroll1y = int.Parse(tbScroll1y.Text, NumberStyles.HexNumber);
            CPS.cps_a_regs[CPS.CPS1_SCROLL1_SCROLLY] = ushort.Parse(tbScroll1y.Text, NumberStyles.HexNumber);
        }
        private void btnSet2X_Click(object sender, EventArgs e)
        {
            CPS.scroll2x = int.Parse(tbScroll2x.Text, NumberStyles.HexNumber);
            CPS.cps_a_regs[CPS.CPS1_SCROLL2_SCROLLX] = ushort.Parse(tbScroll2x.Text, NumberStyles.HexNumber);
        }
        private void btnSet2Y_Click(object sender, EventArgs e)
        {
            CPS.scroll2y = int.Parse(tbScroll2y.Text, NumberStyles.HexNumber);
            CPS.cps_a_regs[CPS.CPS1_SCROLL2_SCROLLY] = ushort.Parse(tbScroll2y.Text, NumberStyles.HexNumber);
        }
        private void btnSet3X_Click(object sender, EventArgs e)
        {
            CPS.scroll3x = int.Parse(tbScroll3x.Text, NumberStyles.HexNumber);
            CPS.cps_a_regs[CPS.CPS1_SCROLL3_SCROLLX] = ushort.Parse(tbScroll3x.Text, NumberStyles.HexNumber);
        }
        private void btnSet3Y_Click(object sender, EventArgs e)
        {
            CPS.scroll3y = int.Parse(tbScroll3y.Text, NumberStyles.HexNumber);
            CPS.cps_a_regs[CPS.CPS1_SCROLL3_SCROLLY] = ushort.Parse(tbScroll3y.Text, NumberStyles.HexNumber);
        }
        private void btnSetSX_Click(object sender, EventArgs e)
        {
            CPS.scrollxSG = int.Parse(tbScrollsx.Text);
        }
        private void btnSetSY_Click(object sender, EventArgs e)
        {
            CPS.scrollySG = int.Parse(tbScrollsy.Text);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            locationX = e.Location.X;
            locationY = e.Location.Y;
            tsslLocation.Text = locationX + "," + locationY;            
            Application.DoEvents();
        }
        private void btnGetpal_Click(object sender, EventArgs e)
        {
            int i, base1;
            base1 = (CPS.cps_a_regs[5] * 0x100) & 0x3ffff;
            for (i = 0; i < 0xc00 * 2; i++)
            {
                CPS.bbPaletteG[i] = CPS.gfxram[base1 + i];
            }
        }
        private void btnDrawpri_Click(object sender, EventArgs e)
        {
            int i, j;
            Bitmap bm1 = new Bitmap(0x200, 0x200);
            for (i = 0; i < 0x200; i++)
            {
                for (j = 0; j < 0x200; j++)
                {
                    if (CPS.priority_bitmapG[j, i] == 0x00)
                    {
                        bm1.SetPixel(j, i, Palette.trans_color);
                    }
                    else if (CPS.priority_bitmapG[j, i] == 0x10)
                    {
                        bm1.SetPixel(j, i, Color.Red);
                    }
                    else if (CPS.priority_bitmapG[j, i] == 0x20)
                    {
                        bm1.SetPixel(j, i, Color.Green);
                    }
                    else if (CPS.priority_bitmapG[j, i] == 0x30)
                    {
                        bm1.SetPixel(j, i, Color.Blue);
                    }
                    else if (CPS.priority_bitmapG[j, i] != 0)
                    {
                        bm1.SetPixel(j, i, Color.Black);
                    }
                }
            }
            pictureBox1.Image = bm1;
        }
        private void btnDrawtile_Click(object sender, EventArgs e)
        {
            int i1;
            int code, color;
            try
            {
                Bitmap bm1 = null;
                code = int.Parse(tbCode.Text, NumberStyles.HexNumber);
                color = int.Parse(tbColor.Text, NumberStyles.HexNumber);
                CPS.GetData();
                for (i1 = 0; i1 < CPS.nColorG; i1++)
                {
                    if (i1 % 16 == 15)
                    {
                        CPS.cc1G[i1] = Palette.trans_color;
                    }
                }
                if (cbLayer.SelectedIndex == 0)
                {
                    bm1 = GetTile0(0, code, color);
                }
                else if (cbLayer.SelectedIndex == 1)
                {
                    bm1 = GetTile0(1, code, color);
                }
                else if (cbLayer.SelectedIndex == 2)
                {
                    bm1 = GetTile1(code, color);
                }
                else if (cbLayer.SelectedIndex == 3)
                {
                    bm1 = GetTile2(code, color);
                }
                pictureBox1.Image = bm1;
            }
            catch
            {
                MessageBox.Show("error input");
            }
        }
        private Bitmap GetTile0(int gfxset, int code1, int iColor)
        {
            int i1, i2, i3, i4, i5, i6;
            int iCode, iByte, cols, rows;
            int tilewidth, tileheight;
            int iOffset;
            int idx = 0;
            Color c1;
            Bitmap bm1;
            int width, height;
            int x0, y0, dx0, dy0;
            int ratio = 4;
            width = 0x200;
            height = 0x200;
            tilewidth = 8;
            tileheight = 8;
            cols = width / tilewidth / ratio;
            rows = height / tileheight / ratio;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (i3 = 0; i3 < cols; i3++)
                {
                    for (i4 = 0; i4 < rows; i4++)
                    {
                        iCode = code1 + i4 * 0x10 + i3;
                        x0 = tilewidth * i3;
                        y0 = tileheight * i4;
                        dx0 = 1;
                        dy0 = 1;
                        for (i1 = 0; i1 < tilewidth; i1++)
                        {
                            for (i2 = 0; i2 < tileheight; i2++)
                            {
                                iOffset = iCode * 0x80 + gfxset * 8 + i1 + i2 * 16;
                                iByte = CPS.gfx1rom[iOffset];
                                idx = iColor * 0x10 + iByte;
                                c1 = CPS.cc1G[idx];
                                for (i5 = 0; i5 < ratio; i5++)
                                {
                                    for (i6 = 0; i6 < ratio; i6++)
                                    {
                                        ptr2 = ptr + (((y0 + dy0 * i2) * ratio + i6) * width + ((x0 + dx0 * i1) * ratio + i5)) * 4;
                                        *ptr2 = c1.B;
                                        *(ptr2 + 1) = c1.G;
                                        *(ptr2 + 2) = c1.R;
                                        *(ptr2 + 3) = c1.A;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        private Bitmap GetTile1(int code1, int iColor)
        {
            int i1, i2, i3, i4, i5, i6;
            int iCode, iByte, cols, rows;
            int tilewidth, tileheight;
            int iOffset;
            int idx = 0;
            Color c1;
            Bitmap bm1;
            int width, height;
            int x0, y0, dx0, dy0;
            int ratio = 2;
            width = 0x200;
            height = 0x200;
            tilewidth = 16;
            tileheight = 16;
            cols = width / tilewidth / ratio;
            rows = height / tileheight / ratio;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (i3 = 0; i3 < cols; i3++)
                {
                    for (i4 = 0; i4 < rows; i4++)
                    {
                        iCode = code1 + i4 * 0x10 + i3;
                        x0 = tilewidth * i3;
                        y0 = tileheight * i4;
                        dx0 = 1;
                        dy0 = 1;
                        for (i1 = 0; i1 < tilewidth; i1++)
                        {
                            for (i2 = 0; i2 < tileheight; i2++)
                            {
                                iOffset = iCode * 0x40 * 4 + i1 + i2 * 16;
                                iByte = CPS.gfx1rom[iOffset];
                                idx = iColor * 0x10 + iByte;
                                c1 = CPS.cc1G[idx];
                                for (i5 = 0; i5 < ratio; i5++)
                                {
                                    for (i6 = 0; i6 < ratio; i6++)
                                    {
                                        ptr2 = ptr + (((y0 + dy0 * i2) * ratio + i6) * width + ((x0 + dx0 * i1) * ratio + i5)) * 4;
                                        *ptr2 = c1.B;
                                        *(ptr2 + 1) = c1.G;
                                        *(ptr2 + 2) = c1.R;
                                        *(ptr2 + 3) = c1.A;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        private Bitmap GetTile2(int code1, int iColor)
        {
            int i1, i2, i3, i4, i5, i6;
            int iCode, iByte, cols, rows;
            int tilewidth, tileheight;
            int iOffset;
            int idx = 0;
            Color c1;
            Bitmap bm1;
            int width, height;
            int x0, y0, dx0, dy0;
            int ratio = 1;
            width = 0x200;
            height = 0x200;
            tilewidth = 32;
            tileheight = 32;
            cols = width / tilewidth / ratio;
            rows = height / tileheight / ratio;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (i3 = 0; i3 < cols; i3++)
                {
                    for (i4 = 0; i4 < rows; i4++)
                    {
                        iCode = code1 + i4 * 0x10 + i3;
                        x0 = tilewidth * i3;
                        y0 = tileheight * i4;
                        dx0 = 1;
                        dy0 = 1;
                        for (i1 = 0; i1 < tilewidth; i1++)
                        {
                            for (i2 = 0; i2 < tileheight; i2++)
                            {
                                iOffset = iCode * 0x40 * 16 + i1 + i2 * 16 * 2;
                                iByte = CPS.gfx1rom[iOffset];
                                idx = iColor * 0x10 + iByte;
                                c1 = CPS.cc1G[idx];
                                for (i5 = 0; i5 < ratio; i5++)
                                {
                                    for (i6 = 0; i6 < ratio; i6++)
                                    {
                                        ptr2 = ptr + (((y0 + dy0 * i2) * ratio + i6) * width + ((x0 + dx0 * i1) * ratio + i5)) * 4;
                                        *ptr2 = c1.B;
                                        *(ptr2 + 1) = c1.G;
                                        *(ptr2 + 2) = c1.R;
                                        *(ptr2 + 3) = c1.A;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        private void btnDumpRam_Click(object sender, EventArgs e)
        {
            DumpRam();
        }
        private void btnWriteRam_Click(object sender, EventArgs e)
        {
            WriteRam();
        }
        private void DumpRam()
        {
            BinaryWriter bw1 = new BinaryWriter(new FileStream("dump1.dat", FileMode.Create));
            bw1.Write(Memory.mainram, 0, 0x10000);
            bw1.Close();
            BinaryWriter bw2 = new BinaryWriter(new FileStream("dump2.dat", FileMode.Create));
            bw2.Write(CPS.gfxram, 0, 0x30000);
            bw2.Close();
            BinaryWriter bw3 = new BinaryWriter(new FileStream("dump3.dat", FileMode.Create));
            int i;
            for (i = 0; i < 0x20; i++)
            {
                bw3.Write(CPS.cps_a_regs[i]);
            }
            for (i = 0; i < 0x20; i++)
            {
                bw3.Write(CPS.cps_b_regs[i]);
            }
            bw3.Close();
        }
        private void WriteRam()
        {
            byte[] bb1 = new byte[0x10000], bb2 = new byte[0x30000], bb3 = new byte[0x80];
            BinaryReader br1 = new BinaryReader(new FileStream("dump1.dat", FileMode.Open));
            br1.Read(bb1, 0, 0x10000);
            br1.Close();
            Array.Copy(bb1, Memory.mainram, 0x10000);
            BinaryReader br2 = new BinaryReader(new FileStream("dump2.dat", FileMode.Open));
            br2.Read(bb2, 0, 0x30000);
            br2.Close();
            Array.Copy(bb2, CPS.gfxram, 0x30000);
            BinaryReader br3 = new BinaryReader(new FileStream("dump3.dat", FileMode.Open));
            br3.Read(bb3, 0, 0x80);
            br3.Close();
            int i;
            for (i = 0; i < 0x20; i++)
            {
                CPS.cps_a_regs[i] = (ushort)(bb3[i * 2] + bb3[i * 2 + 1] * 0x100);
            }
            for (i = 0; i < 0x20; i++)
            {
                CPS.cps_b_regs[i] = (ushort)(bb3[0x40 + i * 2] + bb3[0x40 + i * 2 + 1] * 0x100);
            }
        }
    }
}
