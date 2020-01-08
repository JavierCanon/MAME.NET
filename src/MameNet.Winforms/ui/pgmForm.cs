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
    public partial class pgmForm : Form
    {
        private mainForm _myParentForm;
        private string[] sde2 = new string[] { "," };
        private int locationX, locationY;
        public pgmForm(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void pgmForm_Load(object sender, EventArgs e)
        {

        }
        private void pgmForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            PGM.bRender0G = cbSprite1.Checked;
            PGM.bRender1G = cbBg.Checked;
            PGM.bRender2G = cbSprite0.Checked;
            PGM.bRender3G = cbTx.Checked;
            Bitmap bm1 = PGM.GetAllGDI();
            pictureBox1.Image = bm1;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            locationX = e.Location.X;
            locationY = e.Location.Y;
            tsslLocation.Text = locationX + "," + locationY;
            Application.DoEvents();
        }
    }
}
