using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ui
{
    public partial class aboutForm : Form
    {
        public aboutForm()
        {
            InitializeComponent();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void aboutForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = mame.Properties.Resources._1;
            lbVersion.Text = Version.build_version;
            lbAuthor.Text = "by " + Version.author;
            tbShow.Text = mame.Properties.Resources.readme;
            tbShow.Visible = false;
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            if (btnShow.Text == "show")
            {
                this.Height = 512;
                tbShow.Visible = true;
                btnShow.Text = "hide";
            }
            else if (btnShow.Text == "hide")
            {
                this.Height = 256;
                tbShow.Visible = false;
                btnShow.Text = "show";
            }
        }
    }
}