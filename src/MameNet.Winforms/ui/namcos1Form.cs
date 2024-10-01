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
            int n;
            Bitmap bm1;
            n = int.Parse(tbLayer.Text);
            bm1 = Namcos1.GetLayer(n);
            pictureBox1.Image = bm1;
        }
        private void btnDraw2_Click(object sender, EventArgs e)
        {
            Bitmap bm1;
            bm1 = Namcos1.GetPri();
            pictureBox1.Image = bm1;
        }
    }
}
