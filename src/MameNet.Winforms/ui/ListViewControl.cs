using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ui
{
    public partial class ListViewControl : UserControl
    {
        private string[] sde1 = new string[] { "\r\n" }, sde2 = new string[] { "\r\n\r\n" }, sde3 = new string[] { "=" };
        public string[][] ssCItem, ssCValue;
        private ListViewItem lvItem;
        public ListViewControl()
        {
            InitializeComponent();
            ColumnHeader columnheader;
            columnheader = new ColumnHeader();
            columnheader.Text = "function";
            myListView.Columns.Add(columnheader);
            columnheader = new ColumnHeader();
            columnheader.Text = "setting";
            myListView.Columns.Add(columnheader);
            foreach (ColumnHeader ch in myListView.Columns)
            {
                ch.Width = 150;
            }
            myListView.CheckBoxes = true;
            myListView.GridLines = true;
            myListView.View = View.Details;
            myListView.FullRowSelect = true;
            cbCombo.Visible = false;
        }
        public void LoadSetting(string sFile)
        {
            cbCombo.Visible = false;
            myListView.Items.Clear();
            string[] ss1, ss2, ss3;
            int i1, i2, n1, n2;
            ListViewItem listviewitem;
            StreamReader sr1 = new StreamReader(sFile, Encoding.GetEncoding("GB2312"));
            ss1 = sr1.ReadToEnd().Split(sde2, StringSplitOptions.RemoveEmptyEntries);
            sr1.Close();
            n1 = ss1.Length;
            ssCItem = new string[n1 - 1][];
            ssCValue = new string[n1 - 1][];
            for (i1 = 0; i1 < n1 - 1; i1++)
            {
                ss2 = ss1[i1].Split(sde1, StringSplitOptions.RemoveEmptyEntries);
                n2 = ss2.Length;
                ssCItem[i1] = new string[n2 - 1];
                ssCValue[i1] = new string[n2 - 1];
                for (i2 = 0; i2 < n2 - 1; i2++)
                {
                    ss3 = ss2[i2 + 1].Split(sde3, StringSplitOptions.RemoveEmptyEntries);
                    ssCItem[i1][i2] = ss3[0];
                    ssCValue[i1][i2] = ss3[1];
                }
                listviewitem = new ListViewItem(ss2[0].Substring(1, ss2[0].Length - 2));
                listviewitem.SubItems.Add(ssCItem[i1][0]);
                myListView.Items.Add(listviewitem);
            }
        }
        private void MyComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the user presses ESC.
            switch (e.KeyChar)
            {
                case (char)(int)Keys.Escape:
                    {
                        // Reset the original text value, and then hide the ComboBox.
                        cbCombo.Text = lvItem.Text;
                        cbCombo.Visible = false;
                        break;
                    }
                case (char)(int)Keys.Enter:
                    {
                        // Hide the ComboBox.
                        cbCombo.Visible = false;
                        break;
                    }
            }
        }
        private void MyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvItem != null)
            {
                lvItem.SubItems[1].Text = cbCombo.Text;
            }
        }
        private void MyListView_MouseDown(object sender, MouseEventArgs e)
        {
            lvItem = myListView.GetItemAt(e.X, e.Y);
            if (lvItem != null)
            {
                // Get the bounds of the item that is clicked.
                Rectangle ClickedItem = lvItem.Bounds;
                int i1 = lvItem.Index;
                // Verify that the column is completely scrolled off to the left.
                if ((ClickedItem.Left + myListView.Columns[0].Width) < 0)
                {
                    // If the cell is out of view to the left, do nothing.
                    return;
                }// Verify that the column is partially scrolled off to the left.
                else if (ClickedItem.Left < 0)
                {
                    // Determine if column extends beyond right side of ListView.
                    if ((ClickedItem.Left + myListView.Columns[0].Width) > myListView.Width)
                    {
                        // Set width of column to match width of ListView.
                        ClickedItem.Width = myListView.Width;
                        ClickedItem.X = 0;
                    }
                    else
                    {
                        // Right side of cell is in view.
                        ClickedItem.Width = myListView.Columns[0].Width + ClickedItem.Left;
                        ClickedItem.X = 2 + myListView.Columns[0].Width;
                    }
                }
                else if (myListView.Columns[0].Width > myListView.Width)
                {
                    ClickedItem.Width = myListView.Width;
                }
                else
                {
                    ClickedItem.Width = myListView.Columns[1].Width;
                    ClickedItem.X = 2 + myListView.Columns[0].Width;
                }
                // Adjust the top to account for the location of the ListView.
                ClickedItem.Y += myListView.Top;
                ClickedItem.X += myListView.Left;
                // Assign calculated bounds to the ComboBox.
                cbCombo.Bounds = ClickedItem;
                cbCombo.Items.Clear();
                cbCombo.Items.AddRange(ssCItem[i1]);
                // Set default text for ComboBox to match the item that is clicked.
                cbCombo.Text = lvItem.SubItems[1].Text;
                // Display the ComboBox, and make sure that it is on top with focus.
                cbCombo.Visible = true;
                cbCombo.BringToFront();
                cbCombo.Focus();
            }
        }
    }
}
