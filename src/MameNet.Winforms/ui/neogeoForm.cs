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

namespace ui
{
    public partial class neogeoForm : Form
    {
        private mainForm _myParentForm;
        private string[] sde2 = new string[] { "," };
        private int locationX, locationY;
        public neogeoForm(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void neogeoForm_Load(object sender, EventArgs e)
        {
            cbL0.Checked = true;
            cbL1.Checked = true;
            tbSprite.Text = "000-17C";
            tbFile.Text = "00";
            tbPoint.Text = "350,240,30,16";
            tbSOffset.Text = "01cb0600";
            tbPensoffset.Text = "ed0";
        }
        private void neogeoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            locationX = e.Location.X;
            locationY = e.Location.Y;
            tsslLocation.Text = locationX + "," + locationY;
            Application.DoEvents();
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            Neogeo.bRender0G = cbL0.Checked;
            Neogeo.bRender1G = cbL1.Checked;
            Bitmap bm1 = Neogeo.GetAllGDI();
            pictureBox1.Width = bm1.Width;
            pictureBox1.Height = bm1.Height;
            pictureBox1.Image = bm1;
        }
        private void btnDraw2_Click(object sender, EventArgs e)
        {
            int offset = int.Parse(tbSOffset.Text, NumberStyles.HexNumber);
            int pensoffset = int.Parse(tbPensoffset.Text, NumberStyles.HexNumber);
            Bitmap bm1 = Neogeo.DrawSprite(offset,pensoffset);
            pictureBox1.Width = bm1.Width;
            pictureBox1.Height = bm1.Height;
            pictureBox1.Image = bm1;
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
            int i, j;
            BinaryWriter bw1 = new BinaryWriter(new FileStream("dump1.dat", FileMode.Create));
            bw1.Write(Memory.mainram, 0, 0x10000);
            bw1.Close();
            BinaryWriter bw2 = new BinaryWriter(new FileStream("dump2.dat", FileMode.Create));
            bw2.Write(Neogeo.mainram2, 0, 0x10000);
            bw2.Close();
            BinaryWriter bw3 = new BinaryWriter(new FileStream("dump3.dat", FileMode.Create));            
            for (i = 0; i < 0x10000; i++)
            {
                bw3.Write(Neogeo.neogeo_videoram[i]);
            }
            BinaryWriter bw4 = new BinaryWriter(new FileStream("dump4.dat", FileMode.Create));
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 0x1000; j++)
                {
                    bw4.Write(Neogeo.palettes[i, j]);
                }
            }            
        }
        private void WriteRam()
        {
            int i, j;
            byte[] bb1 = new byte[0x10000], bb2 = new byte[0x10000], bb3 = new byte[0x20000],bb4=new byte[0x4000];
            BinaryReader br1 = new BinaryReader(new FileStream("dump1.dat", FileMode.Open));
            br1.Read(bb1, 0, 0x10000);
            br1.Close();
            Array.Copy(bb1, Memory.mainram, 0x10000);
            BinaryReader br2 = new BinaryReader(new FileStream("dump2.dat", FileMode.Open));
            br2.Read(bb2, 0, 0x10000);
            br2.Close();
            Array.Copy(bb2, Neogeo.mainram2, 0x10000);
            BinaryReader br3 = new BinaryReader(new FileStream("dump3.dat", FileMode.Open));
            br3.Read(bb3, 0, 0x20000);
            br3.Close();            
            for (i = 0; i < 0x10000; i++)
            {
                Neogeo.neogeo_videoram[i]=(ushort)(bb3[i * 2] + bb3[i * 2 + 1] * 0x100);
            }
            BinaryReader br4 = new BinaryReader(new FileStream("dump4.dat", FileMode.Open));
            br4.Read(bb4, 0, 0x4000);
            br4.Close();
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 0x1000; j++)
                {
                    Neogeo.palettes[i, j] = (ushort)(bb4[i * 0x2000 + j * 2] + bb4[i * 0x2000 + j * 2 + 1] * 0x100);
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Bitmap bm1, bm3;
            int width1, width2, height1, height2, widthall, heightall;
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
    }
}
