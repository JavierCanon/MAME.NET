using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using Microsoft.DirectX.DirectSound;
using DSDevice = Microsoft.DirectX.DirectSound.Device;
using mame;
using cpu.nec;

namespace ui
{
    public partial class mainForm : Form
    {
        private ToolStripMenuItem[] itemSize;
        private loadForm loadform;
        public cheatForm cheatform;
        private cheatsearchForm cheatsearchform;
        private ipsForm ipsform;        
        public m68000Form m68000form;
        public z80Form z80form;
        public m6809Form m6809form;
        public cpsForm cpsform;
        public neogeoForm neogeoform;
        public namcos1Form namcos1form;
        public pgmForm pgmform;
        public m72Form m72form;
        public m92Form m92form;
        public string sSelect;
        private DSDevice dev;
        private BufferDescription desc1;
        public static Thread t1;
        public string handle1;
        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]
        public static extern int ShowCursor(bool bShow);
        internal static void ShowCursor()
        {
            while (ShowCursor(true) < 0)
            {
                ShowCursor(true);
            }
        }
        internal static void HideCursor()
        {
            while (ShowCursor(false) >= 0)
            {
                ShowCursor(false);
            }
        }
        public mainForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader sr1 = new StreamReader("mame.ini");
            sr1.ReadLine();
            sSelect = sr1.ReadLine();
            sr1.Close();
            this.Text = Version.build_version;
            resetToolStripMenuItem.Enabled = false;
            gameStripMenuItem.Enabled = false;
            Mame.sHandle1 = this.Handle.ToString();
            RomInfo.Rom=new RomInfo();
            dev = new DSDevice();
            dev.SetCooperativeLevel(this, CooperativeLevel.Normal);
            desc1 = new BufferDescription();
            desc1.Format = CreateWaveFormat();
            desc1.BufferBytes = 0x9400;
            desc1.ControlVolume = true;
            desc1.GlobalFocus = true;
            Keyboard.InitializeInput(this);
            Sound.buf2 = new SecondaryBuffer(desc1, dev);
            InitLoadForm();
            InitCheatForm();
            InitCheatsearchForm();
            InitIpsForm();
            InitM68000Form();
            InitZ80Form();
            InitM6809Form();
            InitCpsForm();
            InitNeogeoForm();
            InitNamcos1Form();
            InitPgmForm();
            InitM72Form();
            InitM92Form();
        }
        public void LoadRom()
        {
            mame.Timer.lt = new List<mame.Timer.emu_timer>();
            sSelect = RomInfo.Rom.Name;
            Machine.FORM = this;
            Machine.rom = RomInfo.Rom;
            Machine.sName = Machine.rom.Name;
            Machine.sParent = Machine.rom.Parent;
            Machine.sBoard = Machine.rom.Board;
            Machine.sDirection = Machine.rom.Direction;
            Machine.sDescription = Machine.rom.Description;
            Machine.sManufacturer = Machine.rom.Manufacturer;
            Machine.lsParents = RomInfo.GetParents(Machine.sName);
            int i;
            cpsToolStripMenuItem.Enabled = false;
            neogeoToolStripMenuItem.Enabled = false;
            namcos1ToolStripMenuItem.Enabled = false;
            pgmToolStripMenuItem.Enabled = false;
            m72ToolStripMenuItem.Enabled = false;
            m92ToolStripMenuItem.Enabled = false;
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                    Video.nMode = 3;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "512x512";
                    itemSize[1].Text = "512x256";
                    itemSize[2].Text = "384x224";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);                    
                    itemSelect();
                    cpsToolStripMenuItem.Enabled = true;
                    CPS.CPSInit();
                    CPS.GDIInit();
                    break;
                case "Neo Geo":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "320x224";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);                    
                    Video.iMode = 0;
                    itemSelect();
                    neogeoToolStripMenuItem.Enabled = true;
                    Neogeo.NeogeoInit();
                    Neogeo.GDIInit();
                    break;
                case "Namco System 1":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "288x224";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    namcos1ToolStripMenuItem.Enabled = true;
                    Namcos1.Namcos1Init();
                    Namcos1.GDIInit();
                    break;
                case "IGS011":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "512x240";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    IGS011.GDIInit();
                    IGS011.IGS011Init();
                    break;
                case "PGM":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "448x224";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    pgmToolStripMenuItem.Enabled = true;
                    PGM.PGMInit();
                    PGM.GDIInit();
                    break;
                case "M72":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "384x256";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    m72ToolStripMenuItem.Enabled = true;
                    M72.M72Init();
                    M72.GDIInit();
                    break;
                case "M92":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "320x240";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    m92ToolStripMenuItem.Enabled = true;
                    M92.M92Init();
                    M92.GDIInit();
                    break;
            }
            if (Machine.bRom)
            {                
                this.Text = "MAME.NET: " + Machine.sDescription + " [" + Machine.sName + "]";
                Mame.init_machine();
                Generic.nvram_load();
            }
            else
            {
                MessageBox.Show("error rom");
            }
        }
        public void HandleMouse()
        {
            switch (Machine.sName)
            {
                case "drgnwrld":
                    if (UI.single_step || Mame.paused)
                    {
                        Screen[] screen = Screen.AllScreens;
                        this.Cursor = new Cursor(Cursor.Current.Handle);
                        Cursor.Clip = screen[0].Bounds;
                        ShowCursor();
                    }
                    else
                    {
                        Cursor.Clip = new Rectangle(new Point(this.Location.X + 7 + this.pictureBox1.Location.X, this.Location.Y + 29 + this.pictureBox1.Location.Y), this.pictureBox1.Size);
                        HideCursor();
                    }
                    break;
            }
        }
        private void InitCheatForm()
        {
            cheatform = new cheatForm(this);
            foreach (string sFile in Directory.GetFiles("cht"))
            {
                if (Path.GetExtension(sFile).ToLower() == ".cht")
                {
                    cheatform.cbCht.Items.Add(Path.GetFileNameWithoutExtension(sFile));
                }
            }
            if (cheatform.cbCht.Items.Count > 0)
            {
                cheatform.cbCht.SelectedIndex = 0;
            }
        }
        private void InitIpsForm()
        {
            ipsform = new ipsForm(this);
            foreach (string sFile in Directory.GetFiles("ips"))
            {
                if (Path.GetExtension(sFile).ToLower() == ".cht")
                {
                    ipsform.cbCht.Items.Add(Path.GetFileNameWithoutExtension(sFile));
                }
            }
            if (ipsform.cbCht.Items.Count > 0)
            {
                ipsform.cbCht.SelectedIndex = 0;
            }
        }
        private void InitCheatsearchForm()
        {
            cheatsearchform = new cheatsearchForm(this);
        }
        private void InitM68000Form()
        {
            m68000form = new m68000Form(this);
        }
        private void InitZ80Form()
        {
            z80form = new z80Form(this);
        }
        private void InitM6809Form()
        {
            m6809form = new m6809Form(this);
        }
        private void InitCpsForm()
        {
            cpsform = new cpsForm(this);
        }
        private void InitNeogeoForm()
        {
            neogeoform = new neogeoForm(this);
        }
        private void InitNamcos1Form()
        {
            namcos1form = new namcos1Form(this);
        }
        private void InitPgmForm()
        {
            pgmform = new pgmForm(this);
        }
        private void InitM72Form()
        {
            m72form = new m72Form(this);
        }
        private void InitM92Form()
        {
            m92form = new m92Form(this);
        }
        private void InitLoadForm()
        {
            loadform = new loadForm(this);
            ColumnHeader columnheader;
            columnheader = new ColumnHeader();
            columnheader.Text = "Title";
            columnheader.Width = 350;
            loadform.listView1.Columns.Add(columnheader);
            columnheader = new ColumnHeader();
            columnheader.Text = "Year";
            columnheader.Width = 60;
            loadform.listView1.Columns.Add(columnheader);
            columnheader = new ColumnHeader();
            columnheader.Text = "ROM";
            columnheader.Width = 90;
            loadform.listView1.Columns.Add(columnheader);
            columnheader = new ColumnHeader();
            columnheader.Text = "Parent";
            columnheader.Width = 60;
            loadform.listView1.Columns.Add(columnheader);
            columnheader = new ColumnHeader();
            columnheader.Text = "Direction";
            columnheader.Width = 70;
            loadform.listView1.Columns.Add(columnheader);
            columnheader = new ColumnHeader();
            columnheader.Text = "Manufacturer";
            columnheader.Width = 120;
            loadform.listView1.Columns.Add(columnheader);
            columnheader = new ColumnHeader();
            columnheader.Text = "Board";
            columnheader.Width = 120;
            loadform.listView1.Columns.Add(columnheader);
            XElement xe = XElement.Parse(mame.Properties.Resources.mame);
            IEnumerable<XElement> elements = from ele in xe.Elements("game") select ele;
            showInfoByElements(elements);
        }
        private void showInfoByElements(IEnumerable<XElement> elements)
        {
            RomInfo.romList = new List<RomInfo>();
            //StreamWriter sw1 = new StreamWriter("1.txt", false);
            foreach (var ele in elements)
            {
                RomInfo rom = new RomInfo();
                rom.Name = ele.Attribute("name").Value;
                rom.Board = ele.Attribute("board").Value;
                rom.Parent = ele.Element("parent").Value;
                rom.Direction = ele.Element("direction").Value;
                rom.Description = ele.Element("description").Value;
                rom.Year = ele.Element("year").Value;
                rom.Manufacturer = ele.Element("manufacturer").Value;
                RomInfo.romList.Add(rom);
                loadform.listView1.Items.Add(new ListViewItem(new string[] { rom.Description, rom.Year, rom.Name, rom.Parent, rom.Direction, rom.Manufacturer, rom.Board }));
                //sw1.WriteLine(rom.Name + "\t" + rom.Board + "\t" + rom.Parent + "\t" + rom.Direction + "\t" + rom.Description + "\t" + rom.Year + "\t" + rom.Manufacturer);
            }
            //sw1.Close();
        }
        private WaveFormat CreateWaveFormat()
        {
            WaveFormat format = new Microsoft.DirectX.DirectSound.WaveFormat();
            format.AverageBytesPerSecond = 192000;
            format.BitsPerSample = 16;
            format.BlockAlign = 4;//(short)(format.Channels * (format.BitsPerSample / 8));
            format.Channels = 2;
            format.FormatTag = WaveFormatTag.Pcm;
            format.SamplesPerSecond = 48000;
            return format;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Machine.bRom)
            {
                UI.cpurun();
            }
            Mame.exit_pending = true;
            Thread.Sleep(100);
            Generic.nvram_save();
            if (Keyboard.dIDevice != null)
            {
                Keyboard.dIDevice.Dispose();
                Keyboard.dIDevice = null;
            }
            StreamWriter sw1 = new StreamWriter("mame.ini", false);
            sw1.WriteLine("[select]");
            sw1.WriteLine(sSelect);
            sw1.Close();
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Mame.exit_pending = true;
            //if (TrackInfo.CurTrack != null)
            {
                //CPS1.StopDelegate(RomInfo.IStop);
            }
            if (Machine.bRom)
            {
                UI.cpurun();
                Mame.mame_pause(true);
            }
            //bRunning = false;
            //pState = PlayState.PLAY_STOPPED;
            //OnPlaying(pState);
            //OnRunning(false);
            foreach (ListViewItem lvi in loadform.listView1.Items)
            {
                if (sSelect == lvi.SubItems[2].Text)
                {
                    loadform.listView1.FocusedItem = lvi;
                    lvi.Selected = true;
                    loadform.listView1.TopItem = lvi;
                    break;
                }
            }
            //StopTimer();
            loadform.ShowDialog();
        }
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetPicturebox();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cheatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cheatform.Show();
        }
        private void cheatsearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cheatsearchform.Show();
        }
        private void ipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ipsform.ShowDialog();
        }
        private void cpsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cpsform.Show();
        }
        private void neogeoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            neogeoform.Show();
        }
        private void namcos1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            namcos1form.Show();
        }
        private void pgmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pgmform.Show();
        }
        private void m72ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m72form.Show();
        }
        private void m92ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m92form.Show();
        }
        private void m68000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m68000form.Show();
        }
        private void z80ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            z80form.Show();
        }
        private void m6809ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m6809form.Show();
        }
        public void ResetPicturebox()
        {
            pictureBox1.Dispose();
            pictureBox1 = null;
            pictureBox1 = new PictureBox();
            pictureBox1.Location = new System.Drawing.Point(12, 37);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(Video.fullwidth, Video.fullheight);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Add(this.pictureBox1);
            ResizeMain();
        }
        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == 0x0112)
            {
                if (msg.WParam.ToString("X4") == "F100")
                {
                    if (Keyboard.bF10)
                    {
                        Keyboard.bF10 = false;
                        return;
                    }
                }
            }
            // Pass message to default handler.
            base.WndProc(ref msg);
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutForm about1 = new aboutForm();
            about1.ShowDialog();
        }
        private void mainForm_Resize(object sender, EventArgs e)
        {
            ResizeMain();
        }
        private void ResizeMain()
        {
            int deltaX, deltaY;
            switch (Machine.sDirection)
            {
                case "":
                case "180":
                    deltaX = this.Width - (Video.width + 38);
                    deltaY = this.Height - (Video.height + 108);
                    pictureBox1.Width = Video.width + deltaX;
                    pictureBox1.Height = Video.height + deltaY;
                    break;
                case "90":
                case "270":
                    deltaX = this.Width - (Video.height + 38);
                    deltaY = this.Height - (Video.width + 108);
                    pictureBox1.Width = Video.height + deltaX;
                    pictureBox1.Height = Video.width + deltaY;
                    break;
            }
        }
        private void itemsizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i, n;
            n = itemSize.Length;
            for (i = 0; i < n; i++)
            {
                itemSize[i].Checked = false;
            }
            for (i = 0; i < n; i++)
            {
                if (itemSize[i] == (ToolStripItem)sender)
                {
                    Video.iMode = i;
                    itemSelect();
                    break;
                }
            }
        }
        private void itemSelect()
        {
            itemSize[Video.iMode].Checked = true;
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 0;
                        Video.width = 512;
                        Video.height = 512;
                    }
                    else if (Video.iMode == 1)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 256;
                        Video.width = 512;
                        Video.height = 256;
                    }
                    else if (Video.iMode == 2)
                    {
                        Video.offsetx = 64;
                        Video.offsety = 272;
                        Video.width = 384;
                        Video.height = 224;
                    }
                    break;
                case "Neo Geo":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 30;
                        Video.offsety = 16;
                        Video.width = 320;
                        Video.height = 224;
                    }
                    break;
                case "Namco System 1":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 73;
                        Video.offsety = 16;
                        Video.width = 288;
                        Video.height = 224;
                    }
                    break;
                case "IGS011":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 0;
                        Video.width = 512;
                        Video.height = 240;
                    }
                    break;
                case "PGM":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 0;
                        Video.width = 448;
                        Video.height = 224;
                    }
                    break;
                case "M72":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 64;
                        Video.offsety = 0;
                        Video.width = 384;
                        Video.height = 256;
                    }
                    break;
                case "M92":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 80;
                        Video.offsety = 8;
                        Video.width = 320;
                        Video.height = 240;
                    }
                    break;
            }
            switch (Machine.sDirection)
            {
                case "":
                case "180":
                    this.Width = Video.width + 38;
                    this.Height = Video.height + 108;
                    break;
                case "90":
                case "270":
                    this.Width = Video.height + 38;
                    this.Height = Video.width + 108;
                    break;
            }
            ResizeMain();
        }           
    }
}