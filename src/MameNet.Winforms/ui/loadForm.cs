using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using mame;

namespace ui
{
    public partial class loadForm : Form
    {
        private mainForm _myParentForm;
        private int currentCol = -1;
        private bool sort;
        class ListViewItemComparer : IComparer
        {
            public bool sort_b;
            public SortOrder order = SortOrder.Ascending;
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column, bool sort)
            {
                col = column;
                sort_b = sort;
            }
            public int Compare(object x, object y)
            {
                if (sort_b)
                {
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else
                {
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
                }
            }
        }
        public loadForm(mainForm form)
        {
            this._myParentForm = form;
            InitializeComponent();            
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            ApplyRom();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ApplyRom();
        }
        public void ApplyRom()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                this.Close();
                Mame.exit_pending = true;
                Thread.Sleep(100);
                RomInfo.Rom = RomInfo.GetRomByName(listView1.SelectedItems[0].SubItems[2].Text);
                this._myParentForm.LoadRom();
                if (Machine.bRom)
                {
                    m68000Form.iStatus = 0;
                    m68000Form.iValue = 0;
                    Mame.exit_pending = false;
                    this._myParentForm.resetToolStripMenuItem.Enabled = true;
                    this._myParentForm.gameStripMenuItem.Enabled = true;
                    UI.ui_init(this._myParentForm);
                    mainForm.t1 = new Thread(Mame.mame_execute);
                    mainForm.t1.Start();
                }
            }
        }
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            string Asc = ((char)0x25bc).ToString().PadLeft(2, ' ');
            string Des = ((char)0x25b2).ToString().PadLeft(2, ' ');
            if (sort == false)
            {
                sort = true;
                string oldStr = this.listView1.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.listView1.Columns[e.Column].Text = oldStr + Des;
            }
            else if (sort == true)
            {
                sort = false;
                string oldStr = this.listView1.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.listView1.Columns[e.Column].Text = oldStr + Asc;
            }
            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, sort);
            int rowCount = this.listView1.Items.Count;
            if (currentCol != -1)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    this.listView1.Items[i].UseItemStyleForSubItems = false;
                    this.listView1.Items[i].SubItems[currentCol].BackColor = Color.White;
                    if (e.Column != currentCol)
                        this.listView1.Columns[currentCol].Text = this.listView1.Columns[currentCol].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                }
            }
            for (int i = 0; i < rowCount; i++)
            {
                this.listView1.Items[i].UseItemStyleForSubItems = false;
                this.listView1.Items[i].SubItems[e.Column].BackColor = Color.WhiteSmoke;
                currentCol = e.Column;
            }
        }
    }
}
