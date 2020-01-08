using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ui
{
    public class MyListView : System.Windows.Forms.ListView
    {
        private System.ComponentModel.Container components = null;
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x020A;
        public MyListView()
        {
            InitializeComponent();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        protected override void WndProc(ref Message msg)
        {
            // Look for the WM_VSCROLL or the WM_HSCROLL or the WM_MOUSEWHEEL messages.
            if ((msg.Msg == WM_VSCROLL) || (msg.Msg == WM_HSCROLL) || (msg.Msg == WM_MOUSEWHEEL))
            {
                // Move focus to the ListView to cause ComboBox to lose focus.
                this.Focus();
                ((ListViewControl)this.Parent).cbCombo.Visible = false;
            }
            // Pass message to default handler.
            base.WndProc(ref msg);
        }
    }
}
