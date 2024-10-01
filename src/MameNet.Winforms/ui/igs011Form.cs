using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using mame;

namespace ui
{
    public partial class igs011Form : Form
    {
        private mainForm _myParentForm;
        private int locationX, locationY;
        public igs011Form(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void igs011Form_Load(object sender, EventArgs e)
        {
            cbLayer.SelectedIndex = 0;
        }
        private void igs011Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            string layer = cbLayer.Text;
            Bitmap bm1 = null;
            bm1 = IGS011.GetAllGDI(layer);
            pictureBox1.Image = bm1;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            locationX = e.Location.X;
            locationY = e.Location.Y;
            tsslLocation.Text = locationX + "," + locationY;
            Application.DoEvents();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("1.png", ImageFormat.Png);
        }
    }
}
