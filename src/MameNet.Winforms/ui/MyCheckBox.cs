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
    public class MyCheckBox : CheckBox
    {
        private System.ComponentModel.Container components = null;
        public TextBox TB;
        public string str;
        public static TextBox TBResult;
        public MyCheckBox()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();            
            Click += new EventHandler(MyCheckBox_Click);
        }
        private void MyCheckBox_Click(object sender, EventArgs e)
        {
            if (Checked == true)
            {
                TBResult.AppendText(str + TB.Text + "\r\n");
            }
        }
    }
}
