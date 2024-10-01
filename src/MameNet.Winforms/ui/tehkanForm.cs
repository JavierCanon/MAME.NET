using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mame;

namespace ui
{
    public partial class tehkanForm : Form
    {
        private mainForm _myParentForm;
        private int locationX, locationY;
        public tehkanForm(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void tehkanForm_Load(object sender, EventArgs e)
        {

        }
        private void tehkanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            Tehkan.bBg = cbBg.Checked;
            Tehkan.bSprite = cbSprite.Checked;
            Tehkan.bFg = cbFg.Checked;
            Bitmap bm1 = Tehkan.GetAllGDI();
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
