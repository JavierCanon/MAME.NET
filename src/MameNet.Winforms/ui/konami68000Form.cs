using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mame;

namespace ui
{
    public partial class konami68000Form : Form
    {
        private mainForm _myParentForm;
        private int locationX, locationY;
        public konami68000Form(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void konami68000Form_Load(object sender, EventArgs e)
        {
            tbSprite.Text= "0000-4000";
        }
        private void konami68000Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            Konami68000.bTile0 = cbT0.Checked;
            Konami68000.bTile1 = cbT1.Checked;
            Konami68000.bTile2 = cbT2.Checked;
            Konami68000.bSprite = cbSprite.Checked;
            Bitmap bm1 = Konami68000.GetAllGDI();
            pictureBox1.Image = bm1;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("1.png", ImageFormat.Png);
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
