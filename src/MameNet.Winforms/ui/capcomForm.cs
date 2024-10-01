using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mame;
using cpu.m68000;

namespace ui
{
    public partial class capcomForm : Form
    {
        private mainForm _myParentForm;
        private int locationX, locationY;
        public capcomForm(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();
        }
        private void capcomForm_Load(object sender, EventArgs e)
        {
            
        }
        private void capcomForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            Capcom.bBg = cbBg.Checked;
            Capcom.bFg = cbFg.Checked;
            Capcom.bTx = cbTx.Checked;
            Capcom.bSprite = cbSprite.Checked;
            Bitmap bm1 = null;
            switch (Machine.sName)
            {
                case "gng":
                case "gnga":
                case "gngbl":
                case "gngprot":
                case "gngblita":
                case "gngc":
                case "gngt":
                case "makaimur":
                case "makaimurc":
                case "makaimurg":
                case "diamond":
                    bm1 = Capcom.GetAllGDI_gng();
                    break;
                case "sf":
                case "sfua":
                case "sfj":
                case "sfjan":
                case "sfan":
                case "sfp":
                    bm1 = Capcom.GetAllGDI_sf();
                    break;
            }
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
