namespace ui
{
    partial class ListViewControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbCombo = new System.Windows.Forms.ComboBox();
            this.myListView = new MyListView();
            this.SuspendLayout();
            // 
            // cbCombo
            // 
            this.cbCombo.FormattingEnabled = true;
            this.cbCombo.Location = new System.Drawing.Point(3, 259);
            this.cbCombo.Name = "cbCombo";
            this.cbCombo.Size = new System.Drawing.Size(76, 20);
            this.cbCombo.TabIndex = 0;
            this.cbCombo.SelectedIndexChanged += new System.EventHandler(this.MyComboBox_SelectedIndexChanged);
            this.cbCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyComboBox_KeyPress);
            // 
            // myListView
            // 
            this.myListView.Location = new System.Drawing.Point(3, 3);
            this.myListView.Name = "myListView";
            this.myListView.Size = new System.Drawing.Size(330, 250);
            this.myListView.TabIndex = 1;
            this.myListView.UseCompatibleStateImageBehavior = false;
            this.myListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyListView_MouseDown);
            // 
            // ListViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.myListView);
            this.Controls.Add(this.cbCombo);
            this.Name = "ListViewControl";
            this.Size = new System.Drawing.Size(338, 286);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ComboBox cbCombo;
        public MyListView myListView;
    }
}