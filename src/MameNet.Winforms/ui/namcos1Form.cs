using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using mame;

namespace ui
{
    public partial class namcos1Form : Form
    {
        private mainForm _myParentForm;
        public namcos1Form(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void namcos1Form_Load(object sender, EventArgs e)
        {
            tbLayer.Text = "1";
        }
        private void namcos1Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            int i, j, i1,i2,n;
            uint u1;
            Color c1;
            Bitmap bm1 = new Bitmap(512, 512);
            n = int.Parse(tbLayer.Text);
            for (i = 0; i < 0x200; i++)
            {
                for (j = 0; j < 0x200; j++)
                {
                    i1 = Namcos1.ttmap[n].pixmap[i + j * 0x200] + Namcos1.ttmap[n].palette_offset;
                    u1 = Palette.entry_color[i1];
                    if (i1 >= 0x800)
                    {
                        i2 = 1;
                    }
                    c1 = Color.FromArgb((int)Palette.entry_color[Namcos1.ttmap[n].pixmap[i + j * 0x200] + Namcos1.ttmap[n].palette_offset]);
                    bm1.SetPixel(i, j, c1);
                }
            }
            pictureBox1.Image = bm1;
        }
        private void btnDraw2_Click(object sender, EventArgs e)
        {
            int i, j, i1, i2, n;
            uint u1;
            byte priority;
            Color c1;
            Bitmap bm1 = new Bitmap(512, 512);
            RECT new_clip = new RECT();
            new_clip.min_x = 0x49;
            new_clip.max_x = 0x168;
            new_clip.min_y = 0x10;
            new_clip.max_y = 0xef;
            for (priority = 0; priority < 8; priority++)
            {
                for (i = 0; i < 6; i++)
                {
                    if (Namcos1.namcos1_playfield_control[16 + i] == priority)
                    {
                        Namcos1.ttmap[i].tilemap_draw_primask(new_clip, 0x10, priority);
                    }
                }
            }
            for (i = 0; i < 0x200; i++)
            {
                for (j = 0; j < 0x200; j++)
                {
                    if (Tilemap.priority_bitmap[i, j] == 0)
                    {
                        c1 = Color.Black;
                    }
                    else if (Tilemap.priority_bitmap[i, j] == 0x01)
                    {
                        c1 = Color.Red;
                    }
                    else if (Tilemap.priority_bitmap[i, j] == 0x03)
                    {
                        c1 = Color.Orange;
                    }
                    else if (Tilemap.priority_bitmap[i, j] == 0x04)
                    {
                        c1 = Color.Yellow;
                    }
                    else if (Tilemap.priority_bitmap[i, j] == 0x07)
                    {
                        c1 = Color.Green;
                    }
                    else if (Tilemap.priority_bitmap[i, j] == 0x1f)
                    {
                        c1 = Color.Blue;
                    }
                    else
                    {
                        c1 = Color.White;
                    }
                    bm1.SetPixel(j, i, c1);
                }
            }
            pictureBox1.Image = bm1;
        }
        private void btnDraw3_Click(object sender, EventArgs e)
        {
            FileStream fs1 = new FileStream("pri.dat", FileMode.Open);
            BinaryReader br1 = new BinaryReader(fs1);
            byte[] bb1 = new byte[0x40000];
            br1.Read(bb1, 0, 0x40000);
            br1.Close();
            fs1.Close();
            int i, j;
            Color c1;
            Bitmap bm1 = new Bitmap(512, 512);
            for (i = 0; i < 0x200; i++)
            {
                for (j = 0; j < 0x200; j++)
                {
                    if (bb1[i * 0x200 + j] == 0)
                    {
                        c1 = Color.Black;
                    }
                    else if (bb1[i * 0x200 + j] == 0x01)
                    {
                        c1 = Color.Red;
                    }
                    else if (bb1[i * 0x200 + j] == 0x03)
                    {
                        c1 = Color.Orange;
                    }
                    else if (bb1[i * 0x200 + j] == 0x04)
                    {
                        c1 = Color.Yellow;
                    }
                    else if (bb1[i * 0x200 + j] == 0x07)
                    {
                        c1 = Color.Green;
                    }
                    else if (bb1[i * 0x200 + j] == 0x1f)
                    {
                        c1 = Color.Blue;
                    }
                    else
                    {
                        c1 = Color.White;
                    }
                    bm1.SetPixel(j, i, c1);
                }
            }
            pictureBox1.Image = bm1;
        }
    }
}
