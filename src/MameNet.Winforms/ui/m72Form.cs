using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using mame;

namespace ui
{
    public partial class m72Form : Form
    {
        private mainForm _myParentForm;
        private int locationX, locationY;
        public m72Form(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void m72Form_Load(object sender, EventArgs e)
        {
            tbInput.Text = "0-200";
        }
        private void m72Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            M72.bBg = cbBg.Checked;
            M72.bFg = cbFg.Checked;
            M72.bSprite = cbSprite.Checked;
            int n1, n2;
            string[] ss1 = tbInput.Text.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            n1 = Convert.ToInt32(ss1[0], 16);
            n2 = Convert.ToInt32(ss1[1], 16);
            Bitmap bm1 = M72.GetAllGDI(n1, n2);
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
