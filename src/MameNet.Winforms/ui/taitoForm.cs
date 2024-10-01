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
    public partial class taitoForm : Form
    {
        private mainForm _myParentForm;
        private int locationX, locationY;
        public taitoForm(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void taitoForm_Load(object sender, EventArgs e)
        {

        }
        private void taitoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            Taito.bBg = cbBg.Checked;
            Taito.bFg = cbFg.Checked;
            Taito.bSprite = cbSprite.Checked;
            Bitmap bm1 = Taito.GetAllGDI();
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
