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
        public dataeastForm dataeastform;
        public tehkanForm tehkanform;
        public neogeoForm neogeoform;
        public igs011Form igs011form;
        public namcos1Form namcos1form;
        public pgmForm pgmform;
        public m72Form m72form;
        public m92Form m92form;
        public taitoForm taitoform;
        public taitobForm taitobform;
        public konami68000Form konami68000form;
        public capcomForm capcomform;
        public string sSelect;
        private DSDevice dev;
        private BufferDescription desc1;
        public static Thread t1;
        public string handle1;
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
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
            Mame.handle1 = this.Handle;
            Mame.handle3 = this.pictureBox1.Handle;
            Mame.handle4 = GetDesktopWindow();
            RomInfo.Rom = new RomInfo();
            dev = new DSDevice();
            dev.SetCooperativeLevel(this, CooperativeLevel.Normal);
            desc1 = new BufferDescription();
            desc1.Format = CreateWaveFormat();
            desc1.BufferBytes = 0x9400;
            desc1.ControlVolume = true;
            desc1.GlobalFocus = true;
            Keyboard.InitializeInput(this);
            Mouse.InitialMouse(this);
            Sound.buf2 = new SecondaryBuffer(desc1, dev);
            InitLoadForm();
            InitCheatForm();
            InitCheatsearchForm();
            InitIpsForm();
            InitM68000Form();
            InitZ80Form();
            InitM6809Form();
            InitCpsForm();
            InitDataeastForm();
            InitTehkanForm();
            InitNeogeoForm();
            InitSunA8Form();
            InitNamcos1Form();
            InitIGS011Form();
            InitPgmForm();
            InitM72Form();
            InitM92Form();
            InitTaitoForm();
            InitTaitobForm();
            InitKonami68000Form();
            InitCapcomForm();
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
            dataeastToolStripMenuItem.Enabled = false;
            tehkanToolStripMenuItem.Enabled = false;
            neogeoToolStripMenuItem.Enabled = false;
            suna8ToolStripMenuItem.Enabled = false;
            namcos1ToolStripMenuItem.Enabled = false;
            igs011ToolStripMenuItem.Enabled = false;
            pgmToolStripMenuItem.Enabled = false;
            m72ToolStripMenuItem.Enabled = false;
            m92ToolStripMenuItem.Enabled = false;
            taitoToolStripMenuItem.Enabled = false;
            taitobToolStripMenuItem.Enabled = false;
            konami68000ToolStripMenuItem.Enabled = false;
            capcomToolStripMenuItem.Enabled = false;
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
                case "Data East":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "256x224";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    dataeastToolStripMenuItem.Enabled = true;
                    Dataeast.DataeastInit();
                    Dataeast.GDIInit();
                    break;
                case "Tehkan":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "256x224";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    tehkanToolStripMenuItem.Enabled = true;
                    Tehkan.PbactionInit();
                    Tehkan.GDIInit();
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
                case "SunA8":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "256x224";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    suna8ToolStripMenuItem.Enabled = true;
                    SunA8.SunA8Init();
                    SunA8.GDIInit();
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
                    igs011ToolStripMenuItem.Enabled = true;
                    IGS011.IGS011Init();
                    IGS011.GDIInit();
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
                case "Taito":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    switch (Machine.sName)
                    {
                        case "tokio":
                        case "tokioo":
                        case "tokiou":
                        case "tokiob":
                        case "bublbobl":
                        case "bublbobl1":
                        case "bublboblr":
                        case "bublboblr1":
                        case "boblbobl":
                        case "sboblbobl":
                        case "sboblbobla":
                        case "sboblboblb":
                        case "sboblbobld":
                        case "sboblboblc":
                        case "bub68705":
                        case "dland":
                        case "bbredux":
                        case "bublboblb":
                        case "bublcave":
                        case "boblcave":
                        case "bublcave11":
                        case "bublcave10":
                            itemSize[0].Text = "256x224";
                            break;
                        case "opwolf":
                        case "opwolfa":
                        case "opwolfj":
                        case "opwolfu":
                        case "opwolfb":
                        case "opwolfp":
                            itemSize[0].Text = "320x240";
                            break;
                    }
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    taitoToolStripMenuItem.Enabled = true;
                    Taito.TaitoInit();
                    Taito.GDIInit();
                    break;
                case "Taito B":
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
                    taitobToolStripMenuItem.Enabled = true;
                    Taitob.TaitobInit();
                    Taitob.GDIInit();
                    break;
                case "Konami 68000":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
                    itemSize[0].Text = "288*224";
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    konami68000ToolStripMenuItem.Enabled = true;
                    Konami68000.Konami68000Init();
                    Konami68000.GDIInit();
                    break;
                case "Capcom":
                    Video.nMode = 1;
                    itemSize = new ToolStripMenuItem[Video.nMode];
                    for (i = 0; i < Video.nMode; i++)
                    {
                        itemSize[i] = new ToolStripMenuItem();
                        itemSize[i].Size = new Size(152, 22);
                        itemSize[i].Click += new EventHandler(itemsizeToolStripMenuItem_Click);
                    }
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
                            itemSize[0].Text = "256*224";
                            break;
                        case "sf":
                        case "sfua":
                        case "sfj":
                        case "sfjan":
                        case "sfan":
                        case "sfp":
                            itemSize[0].Text = "384*224";
                            break;
                    }
                    resetToolStripMenuItem.DropDownItems.Clear();
                    resetToolStripMenuItem.DropDownItems.AddRange(itemSize);
                    Video.iMode = 0;
                    itemSelect();
                    capcomToolStripMenuItem.Enabled = true;
                    Capcom.CapcomInit();
                    Capcom.GDIInit();
                    break;
            }
            if (Machine.bRom)
            {
                this.Text = "MAME.NET: " + Machine.sDescription + " [" + Machine.sName + "]";
                Mame.init_machine(this);
                Generic.nvram_load();
            }
            else
            {
                MessageBox.Show("error rom");
            }
        }
        private void InitCheatForm()
        {
            cheatform = new cheatForm(this);

            if (!System.IO.Directory.Exists("cht")) {
                System.IO.Directory.CreateDirectory("cht");

            }

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

            if (!System.IO.Directory.Exists("ips"))
            {
                System.IO.Directory.CreateDirectory("ips");

            }

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
        private void InitDataeastForm()
        {
            dataeastform = new dataeastForm(this);
        }
        private void InitTehkanForm()
        {
            tehkanform = new tehkanForm(this);
        }
        private void InitNeogeoForm()
        {
            neogeoform = new neogeoForm(this);
        }
        private void InitSunA8Form()
        {

        }
        private void InitNamcos1Form()
        {
            namcos1form = new namcos1Form(this);
        }
        private void InitIGS011Form()
        {
            igs011form = new igs011Form(this);
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
        private void InitTaitoForm()
        {
            taitoform = new taitoForm(this);
        }
        private void InitTaitobForm()
        {
            taitobform = new taitobForm(this);
        }
        private void InitKonami68000Form()
        {
            konami68000form = new konami68000Form(this);
        }
        private void InitCapcomForm()
        {
            capcomform = new capcomForm(this);
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
                //sw1.WriteLine(rom.Description + " [" + rom.Name + "]");
                //sw1.WriteLine(rom.Name);
            }
            //sw1.Close();
        }
        private WaveFormat CreateWaveFormat()
        {
            Microsoft.DirectX.DirectSound.WaveFormat format = new Microsoft.DirectX.DirectSound.WaveFormat();
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
            if (Machine.bRom)
            {
                UI.cpurun();
                Mame.mame_pause(true);
            }
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
        private void dataeastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataeastform.Show();
        }
        private void tehkanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tehkanform.Show();
        }
        private void neogeoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            neogeoform.Show();
        }
        private void suna8ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void namcos1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            namcos1form.Show();
        }
        private void igs011ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            igs011form.Show();
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
        private void taitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taitoform.Show();
        }
        private void taitobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taitobform.Show();
        }
        private void konami68000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            konami68000form.Show();
        }
        private void capcomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            capcomform.Show();
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
                case "Data East":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 16;
                        Video.width = 256;
                        Video.height = 224;
                    }
                    break;
                case "Tehkan":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 16;
                        Video.width = 256;
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
                case "SunA8":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 16;
                        Video.width = 256;
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
                case "Taito":
                    if (Video.iMode == 0)
                    {
                        switch (Machine.sName)
                        {
                            case "tokio":
                            case "tokioo":
                            case "tokiou":
                            case "tokiob":
                            case "bublbobl":
                            case "bublbobl1":
                            case "bublboblr":
                            case "bublboblr1":
                            case "boblbobl":
                            case "sboblbobl":
                            case "sboblbobla":
                            case "sboblboblb":
                            case "sboblbobld":
                            case "sboblboblc":
                            case "bub68705":
                            case "dland":
                            case "bbredux":
                            case "bublboblb":
                            case "bublcave":
                            case "boblcave":
                            case "bublcave11":
                            case "bublcave10":
                                Video.offsetx = 0;
                                Video.offsety = 16;
                                Video.width = 256;
                                Video.height = 224;
                                break;
                            case "opwolf":
                            case "opwolfa":
                            case "opwolfj":
                            case "opwolfu":
                            case "opwolfb":
                            case "opwolfp":
                                Video.offsetx = 0;
                                Video.offsety = 8;
                                Video.width = 320;
                                Video.height = 240;
                                break;
                        }
                    }
                    break;
                case "Taito B":
                    if (Video.iMode == 0)
                    {
                        Video.offsetx = 0;
                        Video.offsety = 16;
                        Video.width = 320;
                        Video.height = 224;
                    }
                    break;
                case "Konami 68000":
                    if (Video.iMode == 0)
                    {
                        switch (Machine.sName)
                        {
                            case "cuebrick":
                            case "mia":
                            case "mia2":
                            case "tmnt2":
                            case "tmnt2a":
                            case "tmht22pe":
                            case "tmht24pe":
                            case "tmnt22pu":
                            case "qgakumon":
                                Video.offsetx = 104;
                                Video.offsety = 16;
                                Video.width = 304;
                                Video.height = 224;
                                break;
                            case "tmnt":
                            case "tmntu":
                            case "tmntua":
                            case "tmntub":
                            case "tmht":
                            case "tmhta":
                            case "tmhtb":
                            case "tmntj":
                            case "tmnta":
                            case "tmht2p":
                            case "tmht2pa":
                            case "tmnt2pj":
                            case "tmnt2po":
                            case "lgtnfght":
                            case "lgtnfghta":
                            case "lgtnfghtu":
                            case "trigon":
                            case "blswhstl":
                            case "blswhstla":
                            case "detatwin":
                                Video.offsetx = 96;
                                Video.offsety = 16;
                                Video.width = 320;
                                Video.height = 224;
                                break;
                            case "punkshot":
                            case "punkshot2":
                            case "punkshotj":
                            case "glfgreat":
                            case "glfgreatj":
                            case "ssriders":
                            case "ssriderseaa":
                            case "ssridersebd":
                            case "ssridersebc":
                            case "ssridersuda":
                            case "ssridersuac":
                            case "ssridersuab":
                            case "ssridersubc":
                            case "ssridersadd":
                            case "ssridersabd":
                            case "ssridersjad":
                            case "ssridersjac":
                            case "ssridersjbd":
                            case "thndrx2":
                            case "thndrx2a":
                            case "thndrx2j":
                            case "prmrsocr":
                            case "prmrsocrj":
                                Video.offsetx = 112;
                                Video.offsety = 16;
                                Video.width = 288;
                                Video.height = 224;
                                break;
                        }
                    }
                    break;
                case "Capcom":
                    if (Video.iMode == 0)
                    {
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
                                Video.offsetx = 0;
                                Video.offsety = 16;
                                Video.width = 256;
                                Video.height = 224;
                                break;
                            case "sf":
                            case "sfua":
                            case "sfj":
                            case "sfjan":
                            case "sfan":
                            case "sfp":
                                Video.offsetx = 64;
                                Video.offsety = 16;
                                Video.width = 384;
                                Video.height = 224;
                                break;
                        }
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
