using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using cpu.m68000;
using cpu.z80;
using cpu.m6800;
using cpu.m6809;
using cpu.nec;
using ui;

namespace mame
{
    public class cpuexec_data
    {
        public int cpunum;
        public byte suspend;
        public byte nextsuspend;
        public byte eatcycles;
        public byte nexteatcycles;
        public int trigger;
        public ulong totalcycles;			// total CPU cycles executed
        public Atime localtime;				// local time, relative to the timer system's global time
        public int cycles_per_second;
        public long attoseconds_per_cycle;
        public int cycles_running;
        public int cycles_stolen;
        public int icount;
        public Timer.emu_timer partial_frame_timer;
        public Atime partial_frame_period;
        public virtual ulong TotalExecutedCycles { get; set; }
        public virtual int PendingCycles { get; set; }        
        public virtual int ExecuteCycles(int cycles) { return 0; }        
        public virtual void Reset() { }
        public virtual void set_irq_line(int irqline, LineState state) { }
        public virtual void set_input_line_and_vector(int line, LineState state, int vector) { }
        public virtual void cpunum_set_input_line_and_vector(int cpunum, int line, LineState state, int vector) { }
    }
    public class Cpuexec
    {
        public static byte SUSPEND_REASON_HALT = 0x01, SUSPEND_REASON_RESET = 0x02, SUSPEND_REASON_SPIN = 0x04, SUSPEND_REASON_TRIGGER = 0x08, SUSPEND_REASON_DISABLE = 0x10, SUSPEND_ANY_REASON = 0xff;
        public static int iloops, activecpu, icpu, ncpu;
        public static cpuexec_data[] cpu;
        public static Timer.emu_timer timedint_timer;
        public static Atime timedint_period;
        public delegate void vblank_delegate();
        public static vblank_delegate vblank_interrupt;
        public static Timer.emu_timer interleave_boost_timer;
        public static Timer.emu_timer interleave_boost_timer_end;
        public static Atime perfect_interleave;
        public static int vblank_interrupts_per_frame;        
        public static void cpuexec_init()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                    MC68000.m1 = new MC68000();
                    Z80A.z1 = new Z80A();
                    MC68000.m1.cpunum = 0;
                    Z80A.z1.cpunum = 1;
                    ncpu = 2;
                    cpu = new cpuexec_data[ncpu];
                    cpu[0] = MC68000.m1;
                    cpu[1] = Z80A.z1;
                    cpu[0].cycles_per_second = 10000000;
                    switch (Machine.sName)
                    {
                        case "daimakair":
                        case "striderjr":
                        case "dynwarjr":
                        case "area88r":
                        case "sf2ce":
                        case "sf2ceea":
                        case "sf2ceua":
                        case "sf2ceub":
                        case "sf2ceuc":
                        case "sf2ceja":
                        case "sf2cejb":
                        case "sf2cejc":
                        case "sf2bhh":
                        case "sf2rb":
                        case "sf2rb2":
                        case "sf2rb3":
                        case "sf2red":
                        case "sf2v004":
                        case "sf2acc":
                        case "sf2acca":
                        case "sf2accp2":
                        case "sf2amf2":
                        case "sf2dkot2":
                        case "sf2cebltw":
                        case "sf2m2":
                        case "sf2m3":
                        case "sf2m4":
                        case "sf2m5":
                        case "sf2m6":
                        case "sf2m7":
                        case "sf2m8":
                        case "sf2m10":
                        case "sf2yyc":
                        case "sf2koryu":
                        case "sf2dongb":
                        case "cworld2j":
                        case "cworld2ja":
                        case "cworld2jb":
                        case "varth":
                        case "varthr1":
                        case "varthu":
                        case "varthj":
                        case "varthjr":
                        case "qad":
                        case "qadjr":
                        case "wofhfh":
                        case "sf2hf":
                        case "sf2hfu":
                        case "sf2hfj":
                        case "dinohunt":
                        case "punisherbz":
                        case "pnickj":
                        case "qtono2j":
                        case "megaman":
                        case "megamana":
                        case "rockmanj":
                        case "pang3":
                        case "pang3r1":
                        case "pang3j":
                        case "pang3b":
                        case "sfzch":
                        case "sfach":
                        case "sfzbch":
                            cpu[0].cycles_per_second = 12000000;
                            break;
                    }
                    cpu[1].cycles_per_second = 3579545;
                    cpu[0].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[0].cycles_per_second;
                    cpu[1].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[1].cycles_per_second;
                    break;
                case "CPS-1(QSound)":
                    MC68000.m1 = new MC68000();
                    Z80A.z1 = new Z80A();
                    MC68000.m1.cpunum = 0;
                    Z80A.z1.cpunum = 1;
                    ncpu = 2;
                    cpu = new cpuexec_data[ncpu];
                    cpu[0] = MC68000.m1;
                    cpu[1] = Z80A.z1;
                    cpu[0].cycles_per_second = 12000000;
                    cpu[1].cycles_per_second = 8000000;
                    cpu[0].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[0].cycles_per_second;
                    cpu[1].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[1].cycles_per_second;
                    vblank_interrupts_per_frame = 1;
                    break;
                case "CPS2":
                    MC68000.m1 = new MC68000();
                    Z80A.z1 = new Z80A();
                    MC68000.m1.cpunum = 0;
                    Z80A.z1.cpunum = 1;
                    ncpu = 2;
                    cpu = new cpuexec_data[ncpu];
                    cpu[0] = MC68000.m1;
                    cpu[1] = Z80A.z1;
                    cpu[0].cycles_per_second = (int)(16000000 * 0.7375f);
                    cpu[1].cycles_per_second = 8000000;
                    cpu[0].attoseconds_per_cycle = (long)((double)Attotime.ATTOSECONDS_PER_SECOND / (16000000 * 0.7375f));
                    cpu[1].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[1].cycles_per_second;
                    vblank_interrupts_per_frame = 262;
                    vblank_interrupt = CPS.cps2_interrupt;
                    break;
                case "Neo Geo":
                    MC68000.m1 = new MC68000();
                    Z80A.z1 = new Z80A();
                    MC68000.m1.cpunum = 0;
                    Z80A.z1.cpunum = 1;
                    ncpu = 2;
                    cpu = new cpuexec_data[ncpu];
                    cpu[0] = MC68000.m1;
                    cpu[1] = Z80A.z1;
                    cpu[0].cycles_per_second = 12000000;
                    cpu[1].cycles_per_second = 4000000;
                    cpu[0].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[0].cycles_per_second;
                    cpu[1].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[1].cycles_per_second;
                    vblank_interrupts_per_frame = 1;
                    break;
                case "Namco System 1":
                    M6809.mm1 = new M6809[3];
                    M6809.mm1[0] = new M6809();
                    M6809.mm1[1] = new M6809();
                    M6809.mm1[2] = new M6809();
                    M6800.m1 = new M6800();
                    M6800.action_rx = M6800.m1.m6800_rx_tick;
                    M6800.action_tx = M6800.m1.m6800_tx_tick;
                    M6809.mm1[0].cpunum = 0;
                    M6809.mm1[1].cpunum = 1;
                    M6809.mm1[2].cpunum = 2;
                    M6800.m1.cpunum = 3;
                    ncpu = 4;
                    cpu = new cpuexec_data[ncpu];
                    cpu[0] = M6809.mm1[0];
                    cpu[1] = M6809.mm1[1];
                    cpu[2] = M6809.mm1[2];
                    cpu[3] = M6800.m1;
                    cpu[0].cycles_per_second = 1536000;
                    cpu[1].cycles_per_second = 1536000;
                    cpu[2].cycles_per_second = 1536000;
                    cpu[3].cycles_per_second = 1536000;
                    cpu[0].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[0].cycles_per_second;
                    cpu[1].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[1].cycles_per_second;
                    cpu[2].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[2].cycles_per_second;
                    cpu[3].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[3].cycles_per_second;
                    vblank_interrupts_per_frame = 1;
                    break;
                case "IGS011":
                    MC68000.m1 = new MC68000();
                    MC68000.m1.cpunum = 0;
                    ncpu = 1;
                    cpu = new cpuexec_data[ncpu];
                    cpu[0] = MC68000.m1;
                    cpu[0].cycles_per_second = 7333333;
                    cpu[0].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[0].cycles_per_second;
                    vblank_interrupts_per_frame = 5;
                    vblank_interrupt = IGS011.lhb2_interrupt;
                    break;
                case "PGM":
                    MC68000.m1 = new MC68000();
                    Z80A.z1 = new Z80A();
                    MC68000.m1.cpunum = 0;
                    Z80A.z1.cpunum = 1;
                    ncpu = 2;
                    cpu = new cpuexec_data[ncpu];
                    cpu[0] = MC68000.m1;
                    cpu[1] = Z80A.z1;
                    cpu[0].cycles_per_second = 20000000;
                    cpu[1].cycles_per_second = 8468000;
                    cpu[0].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[0].cycles_per_second;
                    cpu[1].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[1].cycles_per_second;
                    vblank_interrupts_per_frame = 2;
                    vblank_interrupt = PGM.drgw_interrupt;
                    break;
                case "M72":
                    Nec.nn1 = new Nec[1];
                    Nec.nn1[0] = new V30();                    
                    Z80A.z1 = new Z80A();
                    Nec.nn1[0].cpunum = 0;
                    Z80A.z1.cpunum = 1;
                    ncpu = 2;
                    cpu = new cpuexec_data[ncpu];
                    cpu[0] = Nec.nn1[0];
                    cpu[1] = Z80A.z1;
                    cpu[0].cycles_per_second = 8000000;
                    cpu[1].cycles_per_second = 3579545;
                    cpu[0].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[0].cycles_per_second;
                    cpu[1].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[1].cycles_per_second;
                    vblank_interrupts_per_frame = 128;
                    switch (Machine.sName)
                    {
                        case "airduel":
                        case "airduelm72":
                            vblank_interrupt = M72.fake_nmi;
                            break;
                        case "ltswords":
                        case "kengo":
                        case "kengoa":
                            vblank_interrupt = Generic.nmi_line_pulse;
                            break;
                    }
                    break;
                case "M92":
                    Nec.nn1 = new Nec[2];
                    Nec.nn1[0] = new V33();
                    Nec.nn1[1] = new V30();
                    Nec.nn1[0].cpunum = 0;
                    Nec.nn1[1].cpunum = 1;
                    ncpu = 2;
                    cpu = new cpuexec_data[ncpu];
                    cpu[0] = Nec.nn1[0];
                    cpu[1] = Nec.nn1[1];
                    cpu[0].cycles_per_second = 9000000;
                    cpu[1].cycles_per_second = 7159090;
                    cpu[0].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[0].cycles_per_second;
                    cpu[1].attoseconds_per_cycle = Attotime.ATTOSECONDS_PER_SECOND / cpu[1].cycles_per_second;
                    vblank_interrupts_per_frame = 0;
                    break;
            }
            activecpu = -1;
            for (icpu = 0; icpu < ncpu; icpu++)
            {
                cpu[icpu].suspend = SUSPEND_REASON_RESET;
                cpu[icpu].localtime = Attotime.ATTOTIME_ZERO;
                cpu[icpu].TotalExecutedCycles = 0;
                cpu[icpu].PendingCycles = 0;
            }
            compute_perfect_interleave();
        }
        public static void cpuexec_reset()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                    MC68000.m1.ReadOpByte = CPS.MCReadOpByte;
                    MC68000.m1.ReadByte = CPS.MCReadByte;
                    MC68000.m1.ReadOpWord = CPS.MCReadOpWord;
                    MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = CPS.MCReadWord;
                    MC68000.m1.ReadOpLong = CPS.MCReadOpLong;
                    MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = CPS.MCReadLong;
                    MC68000.m1.WriteByte = CPS.MCWriteByte;
                    MC68000.m1.WriteWord = CPS.MCWriteWord;
                    MC68000.m1.WriteLong = CPS.MCWriteLong;
                    Z80A.z1.ReadOp = CPS.ZCReadOp;
                    Z80A.z1.ReadOpArg = CPS.ZCReadMemory;
                    Z80A.z1.ReadMemory = CPS.ZCReadMemory;
                    Z80A.z1.WriteMemory = CPS.ZCWriteMemory;
                    Z80A.z1.ReadHardware = CPS.ZCReadHardware;
                    Z80A.z1.WriteHardware = CPS.ZCWriteHardware;
                    Z80A.z1.IRQCallback = CPS.ZIRQCallback;                    
                    switch (Machine.sName)
                    {
                        case "forgottn":
                        case "forgottna":
                        case "forgottnu":
                        case "forgottnue":
                        case "forgottnuc":
                        case "forgottnua":
                        case "forgottnuaa":
                        case "lostwrld":
                        case "lostwrldo":
                            MC68000.m1.ReadByte = CPS.MCReadByte_forgottn;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = CPS.MCReadWord_forgottn;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = CPS.MCReadLong_forgottn;
                            MC68000.m1.WriteByte = CPS.MCWriteByte_forgottn;
                            MC68000.m1.WriteWord = CPS.MCWriteWord_forgottn;
                            MC68000.m1.WriteLong = CPS.MCWriteLong_forgottn;
                            break;
                        case "sf2ee":
                        case "sf2ue":
                        case "sf2thndr":
                            MC68000.m1.ReadByte = CPS.MCReadByte_sf2thndr;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = CPS.MCReadWord_sf2thndr;
                            MC68000.m1.WriteWord = CPS.MCWriteWord_sf2thndr;
                            break;
                        case "sf2ceblp":
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = CPS.MCReadWord_sf2ceblp;
                            MC68000.m1.WriteWord = CPS.MCWriteWord_sf2ceblp;
                            break;
                        case "sf2m3":
                        case "sf2m8":
                            MC68000.m1.ReadByte = CPS.MCReadByte_sf2m3;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = CPS.MCReadWord_sf2m3;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = CPS.MCReadLong_sf2m3;
                            MC68000.m1.WriteByte = CPS.MCWriteByte_sf2m3;
                            MC68000.m1.WriteWord = CPS.MCWriteWord_sf2m3;
                            MC68000.m1.WriteLong = CPS.MCWriteLong_sf2m3;
                            break;
                        case "sf2m10":
                            CPS.mainram2 = new byte[0x100000];
                            CPS.mainram3 = new byte[0x100];
                            MC68000.m1.ReadByte = CPS.MCReadByte_sf2m10;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = CPS.MCReadWord_sf2m10;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = CPS.MCReadLong_sf2m10;
                            MC68000.m1.WriteByte = CPS.MCWriteByte_sf2m10;
                            MC68000.m1.WriteWord = CPS.MCWriteWord_sf2m10;
                            MC68000.m1.WriteLong = CPS.MCWriteLong_sf2m10;
                            break;
                        case "sf2dongb":
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = CPS.MCReadWord_sf2dongb;
                            break;
                        case "dinohunt":
                            MC68000.m1.ReadByte = CPS.MCReadByte_dinohunt;
                            break;
                        case "pang3":
                        case "pang3r1":
                        case "pang3j":
                        case "pang3b":
                            MC68000.m1.ReadByte = CPS.MCReadByte_pang3;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = CPS.MCReadWord_pang3;
                            MC68000.m1.WriteByte = CPS.MCWriteByte_pang3;
                            MC68000.m1.WriteWord = CPS.MCWriteWord_pang3;
                            break;
                    }
                    break;
                case "CPS-1(QSound)":
                    MC68000.m1.ReadOpByte = CPS.MQReadOpByte;
                    MC68000.m1.ReadByte = CPS.MQReadByte;
                    MC68000.m1.ReadOpWord = CPS.MQReadOpWord;
                    MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = CPS.MQReadWord;
                    MC68000.m1.ReadOpLong = CPS.MQReadOpLong;
                    MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = CPS.MQReadLong;
                    MC68000.m1.WriteByte = CPS.MQWriteByte;
                    MC68000.m1.WriteWord = CPS.MQWriteWord;
                    MC68000.m1.WriteLong = CPS.MQWriteLong;
                    Z80A.z1.ReadOp = CPS.ZQReadOp;
                    Z80A.z1.ReadOpArg = CPS.ZQReadMemory;
                    Z80A.z1.ReadMemory = CPS.ZQReadMemory;
                    Z80A.z1.WriteMemory = CPS.ZQWriteMemory;
                    Z80A.z1.ReadHardware = CPS.ZCReadHardware;
                    Z80A.z1.WriteHardware = CPS.ZCWriteHardware;
                    Z80A.z1.IRQCallback = CPS.ZIRQCallback;                    
                    cpu_inittimers();
                    break;
                case "CPS2":
                    MC68000.m1.ReadOpByte = CPS.MC2ReadOpByte;
                    MC68000.m1.ReadByte = CPS.MC2ReadByte;
                    MC68000.m1.ReadOpWord = CPS.MC2ReadOpWord;
                    MC68000.m1.ReadPcrelWord = CPS.MC2ReadPcrelWord;
                    MC68000.m1.ReadWord = CPS.MC2ReadWord;
                    MC68000.m1.ReadOpLong = CPS.MC2ReadOpLong;
                    MC68000.m1.ReadPcrelLong = CPS.MC2ReadPcrelLong;
                    MC68000.m1.ReadLong = CPS.MC2ReadLong;
                    MC68000.m1.WriteByte = CPS.MC2WriteByte;
                    MC68000.m1.WriteWord = CPS.MC2WriteWord;
                    MC68000.m1.WriteLong = CPS.MC2WriteLong;
                    switch (Machine.sName)
                    {
                        case "ddtodd":
                        case "ecofghtrd":
                        case "ssf2ud":
                        case "ssf2tbd":
                        case "armwar1d":
                        case "avspd":
                        case "dstlku1d":
                        case "ringdstd":
                        case "ssf2tad":
                        case "ssf2xjr1d":
                        case "xmcotar1d":
                        case "mshud":
                        case "cybotsud":
                        case "cybotsjd":
                        case "nwarrud":
                        case "sfad":
                        case "19xxd":
                        case "ddsomud":
                        case "gigaman2":
                        case "megamn2d":
                        case "sfz2ad":
                        case "sfz2jd":
                        case "spf2td":
                        case "spf2xjd":
                        case "sfz2ald":
                        case "xmvsfu1d":
                        case "batcird":
                        case "csclub1d":
                        case "mshvsfu1d":
                        case "sgemfd":
                        case "vsavd":
                        case "vhunt2d":
                        case "vsav2d":
                        case "mvscud":
                        case "sfa3ud":
                        case "sfz3jr2d":
                        case "gigawingd":
                        case "gigawingjd":
                        case "1944d":
                        case "dimahoud":
                        case "mmatrixd":
                        case "progearud":
                        case "progearjd":
                        case "progearjbl":
                        case "hsf2d":
                            MC68000.m1.ReadOpByte = CPS.MC2ReadOpByte_dead;
                            MC68000.m1.ReadByte = CPS.MC2ReadByte_dead;
                            MC68000.m1.ReadOpWord = CPS.MC2ReadOpWord_dead;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = CPS.MC2ReadWord_dead;
                            MC68000.m1.ReadOpLong = CPS.MC2ReadOpLong_dead;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = CPS.MC2ReadLong_dead;
                            MC68000.m1.WriteByte = CPS.MC2WriteByte_dead;
                            MC68000.m1.WriteWord = CPS.MC2WriteWord_dead;
                            MC68000.m1.WriteLong = CPS.MC2WriteLong_dead;
                            break;
                    }
                    Z80A.z1.ReadOp = CPS.ZQReadOp;
                    Z80A.z1.ReadOpArg = CPS.ZQReadMemory;
                    Z80A.z1.ReadMemory = CPS.ZQReadMemory;
                    Z80A.z1.WriteMemory = CPS.ZQWriteMemory;
                    Z80A.z1.ReadHardware = CPS.ZCReadHardware;
                    Z80A.z1.WriteHardware = CPS.ZCWriteHardware;
                    Z80A.z1.IRQCallback = CPS.ZIRQCallback;                    
                    cpu_inittimers();
                    break;
                case "Neo Geo":
                    MC68000.m1.ReadOpByte = Neogeo.MReadOpByte;
                    MC68000.m1.ReadByte = Neogeo.MReadByte;
                    MC68000.m1.ReadOpWord = Neogeo.MReadOpWord;
                    MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord;
                    MC68000.m1.ReadOpLong = Neogeo.MReadOpLong;
                    MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong;
                    MC68000.m1.WriteByte = Neogeo.MWriteByte;
                    MC68000.m1.WriteWord = Neogeo.MWriteWord;
                    MC68000.m1.WriteLong = Neogeo.MWriteLong;
                    Z80A.z1.ReadOp = Neogeo.ZReadOp;
                    Z80A.z1.ReadOpArg = Neogeo.ZReadOp;
                    Z80A.z1.ReadMemory = Neogeo.ZReadMemory;
                    Z80A.z1.WriteMemory = Neogeo.ZWriteMemory;
                    Z80A.z1.ReadHardware = Neogeo.ZReadHardware;
                    Z80A.z1.WriteHardware = Neogeo.ZWriteHardware;
                    Z80A.z1.IRQCallback = Neogeo.ZIRQCallback;
                    switch (Machine.sName)
                    {
                        case "fatfury2":
                        case "ssideki":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_fatfury2;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_fatfury2;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_fatfury2;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_fatfury2;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_fatfury2;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_fatfury2;
                            break;
                        case "irrmaze":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_irrmaze;
                            break;
                        case "kof98":
                        case "kof98a":
                        case "kof98k":
                        case "kof98ka":
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_kof98;
                            break;
                        case "kof99":
                        case "kof99h":
                        case "kof99e":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_kof99;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_kof99;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_kof99;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_kof99;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_kof99;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_kof99;
                            break;
                        case "garou":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_garou;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_garou;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_garou;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_garou;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_garou;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_garou;
                            break;
                        case "garouh":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_garou;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_garou;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_garou;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_garouh;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_garouh;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_garouh;
                            break;
                        case "mslug3":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_mslug3;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_mslug3;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_mslug3;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_mslug3;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_mslug3;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_mslug3;
                            break;
                        case "kof2000":
                            MC68000.m1.ReadOpByte = MC68000.m1.ReadByte = Neogeo.MReadByte_kof2000;
                            MC68000.m1.ReadOpWord = MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_kof2000;
                            MC68000.m1.ReadOpLong = MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_kof2000;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_kof2000;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_kof2000;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_kof2000;
                            break;
                        case "mslug5":
                        case "mslug5h":
                        case "svc":
                        case "kof2003":
                        case "kof2003h":
                        case "svcboot":
                        case "svcsplus":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_pvc;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_pvc;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_pvc;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_pvc;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_pvc;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_pvc;
                            break;
                        case "cthd2003":
                        case "ct2k3sp":
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_cthd2003;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_cthd2003;
                            break;
                        case "ms5plus":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_ms5plus;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_ms5plus;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_ms5plus;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_ms5plus;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_ms5plus;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_ms5plus;
                            break;
                        case "kog":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_kog;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_kog;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_kog;
                            break;
                        case "kf2k3bl":
                        case "kf2k3upl":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_kf2k3bl;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_kf2k3bl;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_kf2k3bl;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_kf2k3bl;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_kf2k3bl;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_kf2k3bl;
                            break;
                        case "kf2k3bla":
                        case "kf2k3pl":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_kf2k3bl;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_kf2k3bl;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_kf2k3bl;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_kf2k3pl;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_kf2k3pl;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_kf2k3pl;
                            break;
                        case "sbp":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_sbp;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_sbp;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_sbp;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_sbp;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_sbp;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_sbp;
                            break;
                        case "kof10th":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_kof10th;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_kof10th;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_kof10th;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_kof10th;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_kof10th;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_kof10th;
                            break;
                        case "jockeygp":
                        case "jockeygpa":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_jockeygp;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_jockeygp;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_jockeygp;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_jockeygp;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_jockeygp;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_jockeygp;
                            break;
                        case "vliner":
                            MC68000.m1.ReadByte = Neogeo.MReadByte_vliner;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = Neogeo.MReadWord_vliner;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = Neogeo.MReadLong_vliner;
                            MC68000.m1.WriteByte = Neogeo.MWriteByte_jockeygp;
                            MC68000.m1.WriteWord = Neogeo.MWriteWord_jockeygp;
                            MC68000.m1.WriteLong = Neogeo.MWriteLong_jockeygp;
                            break;
                    }                    
                    cpu_inittimers();
                    break;
                case "Namco System 1":
                    M6809.mm1[0].ReadOp = Namcos1.N0ReadOpByte;
                    M6809.mm1[0].ReadOpArg = Namcos1.N0ReadOpByte;
                    M6809.mm1[0].RM = Namcos1.N0ReadMemory;
                    M6809.mm1[0].WM = Namcos1.N0WriteMemory;
                    M6809.mm1[1].ReadOp = Namcos1.N1ReadOpByte;
                    M6809.mm1[1].ReadOpArg = Namcos1.N1ReadOpByte;
                    M6809.mm1[1].RM = Namcos1.N1ReadMemory;
                    M6809.mm1[1].WM = Namcos1.N1WriteMemory;
                    M6809.mm1[2].ReadOp = Namcos1.N2ReadOpByte;
                    M6809.mm1[2].ReadOpArg = Namcos1.N2ReadOpByte;
                    M6809.mm1[2].RM = Namcos1.N2ReadMemory;
                    M6809.mm1[2].WM = Namcos1.N2WriteMemory;
                    M6800.m1.ReadOp = Namcos1.N3ReadOpByte;
                    M6800.m1.ReadOpArg = Namcos1.N3ReadOpByte;
                    M6800.m1.ReadMemory = Namcos1.N3ReadMemory;
                    M6800.m1.ReadIO = Namcos1.N3ReadIO;
                    M6800.m1.WriteMemory = Namcos1.N3WriteMemory;
                    M6800.m1.WriteIO = Namcos1.N3WriteIO;
                    switch (Machine.sName)
                    {
                        case "quester":
                        case "questers":
                            M6800.m1.ReadMemory = Namcos1.N3ReadMemory_quester;
                            break;
                        case "berabohm":
                            M6800.m1.ReadMemory = Namcos1.N3ReadMemory_berabohm;
                            break;
                        case "faceoff":
                        case "tankfrce4":
                            M6800.m1.ReadMemory = Namcos1.N3ReadMemory_faceoff;
                            break;
                    }                    
                    break;
                case "IGS011":
                    MC68000.m1.ReadOpByte = IGS011.MReadOpByte;
                    MC68000.m1.ReadByte = IGS011.MReadByte;
                    MC68000.m1.ReadOpWord = IGS011.MReadOpWord;
                    MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = IGS011.MReadWord;
                    MC68000.m1.ReadOpLong = IGS011.MReadOpLong;
                    MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = IGS011.MReadLong;
                    MC68000.m1.WriteByte = IGS011.MWriteByte;
                    MC68000.m1.WriteWord = IGS011.MWriteWord;
                    MC68000.m1.WriteLong = IGS011.MWriteLong;
                    switch (Machine.sName)
                    {
                        case "drgnwrldv21":
                            MC68000.m1.ReadOpByte = IGS011.MReadByte_igs012;
                            MC68000.m1.ReadByte = IGS011.MReadByte_igs012;
                            MC68000.m1.ReadOpWord = IGS011.MReadWord_igs012_drgnwrldv21;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = IGS011.MReadWord_igs012_drgnwrldv21;
                            MC68000.m1.ReadOpLong = IGS011.MReadLong_igs012;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = IGS011.MReadLong_igs012;
                            MC68000.m1.WriteByte = IGS011.MWriteByte_igs012;
                            MC68000.m1.WriteWord = IGS011.MWriteWord_igs012;
                            MC68000.m1.WriteLong = IGS011.MWriteLong_igs012;
                            break;
                        case "drgnwrldv21j":
                        case "drgnwrldv20j":
                        case "drgnwrldv40k":
                            MC68000.m1.ReadOpByte = IGS011.MReadByte_igs012;
                            MC68000.m1.ReadByte = IGS011.MReadByte_igs012;
                            MC68000.m1.ReadOpWord = IGS011.MReadWord_igs012;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = IGS011.MReadWord_igs012;
                            MC68000.m1.ReadOpLong = IGS011.MReadLong_igs012;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = IGS011.MReadLong_igs012;
                            MC68000.m1.WriteByte = IGS011.MWriteByte_igs012;
                            MC68000.m1.WriteWord = IGS011.MWriteWord_igs012;
                            MC68000.m1.WriteLong = IGS011.MWriteLong_igs012;
                            break;
                    }                    
                    cpu_inittimers();
                    break;
                case "PGM":
                    MC68000.m1.ReadOpByte = PGM.MReadOpByte;
                    MC68000.m1.ReadByte = PGM.MReadByte;
                    MC68000.m1.ReadOpWord = PGM.MReadOpWord;
                    MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord = PGM.MReadWord;
                    MC68000.m1.ReadOpLong = PGM.MReadOpLong;
                    MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong = PGM.MReadLong;
                    MC68000.m1.WriteByte = PGM.MWriteByte;
                    MC68000.m1.WriteWord = PGM.MWriteWord;
                    MC68000.m1.WriteLong = PGM.MWriteLong;
                    Z80A.z1.ReadOp = PGM.ZReadMemory;
                    Z80A.z1.ReadOpArg = PGM.ZReadMemory;
                    Z80A.z1.ReadMemory = PGM.ZReadMemory;
                    Z80A.z1.WriteMemory = PGM.ZWriteMemory;
                    Z80A.z1.ReadHardware = PGM.ZReadHardware;
                    Z80A.z1.WriteHardware = PGM.ZWriteHardware;
                    Z80A.z1.IRQCallback = PGM.ZIRQCallback;
                    switch (Machine.sName)
                    {
                        case "orlegend":
                        case "orlegende":
                        case "orlegendc":
                        case "orlegendca":
                        case "orlegend111c":
                        case "orlegend111t":
                        case "orlegend111k":
                        case "orlegend105k":
                            MC68000.m1.ReadByte = PGM.MPReadByte_orlegend;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord= PGM.MPReadWord_orlegend;
                            MC68000.m1.WriteByte = PGM.MPWriteByte_orlegend;
                            MC68000.m1.WriteWord = PGM.MPWriteWord_orlegend;
                            break;
                        /*case "drgw2":
                            MC68000.m1.ReadByte = PGM.MPReadByte_drgw2;
                            MC68000.m1.ReadWord = MC68000.m1.ReadPcrelWord= PGM.MPReadWord_drgw2;
                            MC68000.m1.ReadLong = MC68000.m1.ReadPcrelLong= PGM.MPReadLong_drgw2;
                            MC68000.m1.WriteByte = PGM.MPWriteByte_drgw2;
                            MC68000.m1.WriteWord = PGM.MPWriteWord_drgw2;
                            MC68000.m1.WriteLong = PGM.MPWriteLong_drgw2;
                            break;*/
                    }                    
                    cpu_inittimers();
                    break;
                case "M72":
                    Nec.nn1[0].ReadOp = Nec.nn1[0].ReadOpArg = M72.NReadOpByte;
                    Nec.nn1[0].ReadByte = M72.NReadByte_m72;
                    Nec.nn1[0].ReadWord = M72.NReadWord_m72;
                    Nec.nn1[0].WriteByte = M72.NWriteByte_m72;
                    Nec.nn1[0].WriteWord = M72.NWriteWord_m72;
                    Nec.nn1[0].ReadIOByte = M72.NReadIOByte;
                    Nec.nn1[0].ReadIOWord = M72.NReadIOWord;
                    Nec.nn1[0].WriteIOByte = M72.NWriteIOByte_m72;
                    Nec.nn1[0].WriteIOWord = M72.NWriteIOWord_m72;
                    Z80A.z1.ReadOp = M72.ZReadMemory_ram;
                    Z80A.z1.ReadOpArg = M72.ZReadMemory_ram;
                    Z80A.z1.ReadMemory = M72.ZReadMemory_ram;
                    Z80A.z1.WriteMemory = M72.ZWriteMemory_ram;
                    Z80A.z1.ReadHardware = M72.ZReadHardware;
                    Z80A.z1.WriteHardware = M72.ZWriteHardware;
                    Z80A.z1.IRQCallback = M72.ZIRQCallback;
                    switch (Machine.sName)
                    {
                        case "airduel":
                        case "airduelm72":
                            Nec.nn1[0].ReadOp = Nec.nn1[0].ReadOpArg = M72.NReadOpByte_airduel;
                            Nec.nn1[0].ReadByte = M72.NReadByte_m72_airduel;
                            Nec.nn1[0].ReadWord = M72.NReadWord_m72_airduel;
                            Nec.nn1[0].WriteByte = M72.NWriteByte_m72_airduel;
                            Nec.nn1[0].WriteWord = M72.NWriteWord_m72_airduel;
                            Nec.nn1[0].WriteIOByte = M72.NWriteIOByte_m72_airduel;
                            Nec.nn1[0].WriteIOWord = M72.NWriteIOWord_m72_airduel;
                            break;
                        case "ltswords":
                        case "kengo":
                        case "kengoa":
                            Nec.nn1[0].ReadByte = M72.NReadByte_kengo;
                            Nec.nn1[0].ReadWord = M72.NReadWord_kengo;
                            Nec.nn1[0].WriteByte = M72.NWriteByte_kengo;
                            Nec.nn1[0].WriteWord = M72.NWriteWord_kengo;
                            Nec.nn1[0].WriteIOByte = M72.NWriteIOByte_kengo;
                            Nec.nn1[0].WriteIOWord = M72.NWriteIOWord_kengo;
                            Z80A.z1.ReadOp = M72.ZReadMemory_rom;
                            Z80A.z1.ReadOpArg = M72.ZReadMemory_rom;
                            Z80A.z1.ReadMemory = M72.ZReadMemory_rom;
                            Z80A.z1.WriteMemory = M72.ZWriteMemory_rom;
                            Z80A.z1.ReadHardware = M72.ZReadHardware_rtype2;
                            Z80A.z1.WriteHardware = M72.ZWriteHardware_rtype2;
                            Nec.nn1[0].v25v35_decryptiontable = M72.gunforce_decryption_table;
                            break;
                        default:
                            Nec.nn1[0].v25v35_decryptiontable = null;
                            break;
                    }
                    break;
                case "M92":
                    Nec.nn1[0].ReadOp = Nec.nn1[0].ReadOpArg = M92.N0ReadOpByte;
                    Nec.nn1[0].ReadByte = M92.N0ReadByte_m92;
                    Nec.nn1[0].ReadWord = M92.N0ReadWord_m92;
                    Nec.nn1[0].WriteByte = M92.N0WriteByte_m92;
                    Nec.nn1[0].WriteWord = M92.N0WriteWord_m92;
                    Nec.nn1[0].ReadIOByte = M92.N0ReadIOByte_m92;
                    Nec.nn1[0].ReadIOWord = M92.N0ReadIOWord_m92;
                    Nec.nn1[0].WriteIOByte = M92.N0WriteIOByte_m92;
                    Nec.nn1[0].WriteIOWord = M92.N0WriteIOWord_m92;
                    Nec.nn1[1].ReadOp = Nec.nn1[1].ReadOpArg = M92.N1ReadOpByte;
                    Nec.nn1[1].ReadByte = M92.N1ReadByte;
                    Nec.nn1[1].ReadWord = M92.N1ReadWord;
                    Nec.nn1[1].WriteByte = M92.N1WriteByte;
                    Nec.nn1[1].WriteWord = M92.N1WriteWord;
                    Nec.nn1[1].ReadIOByte = null_callback1;
                    Nec.nn1[1].ReadIOWord = null_callback2;
                    Nec.nn1[1].WriteIOByte = null_callback;
                    Nec.nn1[1].WriteIOWord = null_callback;
                    Nec.nn1[0].v25v35_decryptiontable = null;
                    switch (Machine.sName)
                    {
                        case "gunforce":
                        case "gunforcej":
                        case "gunforceu":
                            Nec.nn1[1].v25v35_decryptiontable = M92.gunforce_decryption_table;
                            break;
                        case "bmaster":
                        case "crossbld":
                            Nec.nn1[1].v25v35_decryptiontable = M92.bomberman_decryption_table;
                            break;
                        case "lethalth":
                        case "thndblst":
                            Nec.nn1[0].ReadByte = M92.N0ReadByte_lethalth;
                            Nec.nn1[0].ReadWord = M92.N0ReadWord_lethalth;
                            Nec.nn1[0].WriteByte = M92.N0WriteByte_lethalth;
                            Nec.nn1[0].WriteWord = M92.N0WriteWord_lethalth;
                            Nec.nn1[0].WriteIOByte = M92.N0WriteIOByte_lethalth;
                            Nec.nn1[0].WriteIOWord = M92.N0WriteIOWord_lethalth;
                            Nec.nn1[1].v25v35_decryptiontable = M92.lethalth_decryption_table;
                            break;
                        case "uccops":
                        case "uccopsu":
                        case "uccopsar":
                        case "uccopsj":
                            Nec.nn1[1].v25v35_decryptiontable = M92.dynablaster_decryption_table;
                            break;
                        case "mysticri":
                        case "gunhohki":
                        case "mysticrib":
                            Nec.nn1[1].v25v35_decryptiontable = M92.mysticri_decryption_table;
                            break;
                        case "majtitl2":
                        case "majtitl2a":
                        case "majtitl2b":
                        case "majtitl2j":
                        case "skingame":
                        case "skingame2":
                            Nec.nn1[0].ReadByte = M92.N0ReadByte_majtitl2;
                            Nec.nn1[0].ReadWord = M92.N0ReadWord_majtitl2;
                            Nec.nn1[0].WriteByte = M92.N0WriteByte_majtitl2;
                            Nec.nn1[0].WriteWord = M92.N0WriteWord_majtitl2;
                            Nec.nn1[1].v25v35_decryptiontable = M92.majtitl2_decryption_table;
                            break;
                        case "hook":
                        case "hooku":
                        case "hookj":
                            Nec.nn1[1].v25v35_decryptiontable = M92.hook_decryption_table;
                            break;
                        case "rtypeleo":
                        case "rtypeleoj":
                            Nec.nn1[1].v25v35_decryptiontable = M92.rtypeleo_decryption_table;
                            break;
                        case "inthunt":
                        case "inthuntu":
                        case "kaiteids":
                            Nec.nn1[1].v25v35_decryptiontable = M92.inthunt_decryption_table;
                            break;
                        case "nbbatman":
                        case "leaguemn":
                            Nec.nn1[1].v25v35_decryptiontable = M92.leagueman_decryption_table;
                            break;
                        case "ssoldier":
                        case "psoldier":
                            Nec.nn1[1].v25v35_decryptiontable = M92.psoldier_decryption_table;
                            break;
                        case "gunforc2":
                        case "geostorm":
                            Nec.nn1[1].v25v35_decryptiontable = M92.lethalth_decryption_table;
                            break;
                        default:
                            Nec.nn1[1].v25v35_decryptiontable = null;
                            break;
                    }
                    break;
            }
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                case "Neo Geo":
                case "PGM":
                    m68000Form.m68000State = m68000Form.M68000State.M68000_RUN;
                    MC68000.m1.debugger_start_cpu_hook_callback = Machine.FORM.m68000form.m68000_start_debug;
                    MC68000.m1.debugger_stop_cpu_hook_callback = Machine.FORM.m68000form.m68000_stop_debug;
                    z80Form.z80State = z80Form.Z80AState.Z80A_RUN;
                    Z80A.z1.debugger_start_cpu_hook_callback = Machine.FORM.z80form.z80_start_debug;
                    Z80A.z1.debugger_stop_cpu_hook_callback = Machine.FORM.z80form.z80_stop_debug;
                    break;                
                case "IGS011":
                    m68000Form.m68000State = m68000Form.M68000State.M68000_RUN;
                    MC68000.m1.debugger_start_cpu_hook_callback = Machine.FORM.m68000form.m68000_start_debug;
                    MC68000.m1.debugger_stop_cpu_hook_callback = Machine.FORM.m68000form.m68000_stop_debug;
                    break;
                case "Namco System 1":
                    m6809Form.m6809State = CPUState.RUN;
                    M6809.mm1[0].DisassemblerInit();
                    M6809.mm1[0].debugger_start_cpu_hook_callback = Machine.FORM.m6809form.m6809_start_debug;
                    M6809.mm1[0].debugger_stop_cpu_hook_callback = Machine.FORM.m6809form.m6809_stop_debug;
                    M6809.mm1[1].debugger_start_cpu_hook_callback = null_callback;
                    M6809.mm1[1].debugger_stop_cpu_hook_callback = null_callback;
                    M6809.mm1[2].debugger_start_cpu_hook_callback = null_callback;
                    M6809.mm1[2].debugger_stop_cpu_hook_callback = null_callback;
                    break;
                case "M72":
                    z80Form.z80State = z80Form.Z80AState.Z80A_RUN;
                    Z80A.z1.debugger_start_cpu_hook_callback = Machine.FORM.z80form.z80_start_debug;
                    Z80A.z1.debugger_stop_cpu_hook_callback = Machine.FORM.z80form.z80_stop_debug;
                    break;
                case "M92":
                    break;
            }
            for (icpu = 0; icpu < ncpu; icpu++)
            {
                cpu[icpu].Reset();
            }
        }
        public static void null_callback()
        {

        }
        public static byte null_callback1(int address)
        {
            return 0;
        }
        public static ushort null_callback2(int address)
        {
            return 0;
        }
        public static void null_callback(int address, byte value)
        {

        }
        public static void null_callback(int address, ushort value)
        {

        }
        private static void cpu_inittimers()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1(QSound)":
                case "CPS2":
                    timedint_period = new Atime(0, (long)(1e18 / 250));
                    timedint_timer = Timer.timer_alloc_common(Generic.irq_1_0_line_hold, "irq_1_0_line_hold", false);
                    Timer.timer_adjust_periodic(timedint_timer, timedint_period, timedint_period);
                    break;
                case "Neo Geo":
                    interleave_boost_timer = Timer.timer_alloc_common(null_callback, "boost_callback", false);
                    interleave_boost_timer_end = Timer.timer_alloc_common(end_interleave_boost, "end_interleave_boost", false);
                    break;
                case "CPS1":
                case "Namco System 1":                
                case "IGS011":
                case "PGM":
                case "M72":
                case "M92":
                    break;
            }
        }        
        public static void cpuexec_timeslice()
        {
            StreamWriter sw2 = null;
            Atime target = Timer.lt[0].expire;
            Atime tbase = Timer.global_basetime;
            int ran;
            Atime at;
            for (icpu = 0; icpu < ncpu; icpu++)
            {
                cpu[icpu].suspend = cpu[icpu].nextsuspend;
                cpu[icpu].eatcycles = cpu[icpu].nexteatcycles;
            }
            for (icpu = 0; icpu < ncpu; icpu++)
            {
                if (cpu[icpu].suspend == 0)
                {
                    at = Attotime.attotime_sub(target, cpu[icpu].localtime);
                    cpu[icpu].cycles_running = (int)(at.seconds * cpu[icpu].cycles_per_second + at.attoseconds / cpu[icpu].attoseconds_per_cycle);
                    if (cpu[icpu].cycles_running > 0)
                    {
                        cpu[icpu].cycles_stolen = 0;
                        activecpu = icpu;
                        ran = cpu[icpu].ExecuteCycles(cpu[icpu].cycles_running);
                        activecpu = -1;
                        ran -= cpu[icpu].cycles_stolen;
                        cpu[icpu].totalcycles += (ulong)ran;
                        cpu[icpu].localtime = Attotime.attotime_add(cpu[icpu].localtime, new Atime(ran / cpu[icpu].cycles_per_second, ran * cpu[icpu].attoseconds_per_cycle));
                        if (Attotime.attotime_compare(cpu[icpu].localtime, target) < 0)
                        {
                            if (Attotime.attotime_compare(cpu[icpu].localtime, tbase) > 0)
                                target = cpu[icpu].localtime;
                            else
                                target = tbase;
                        }
                    }
                }
            }
            for (icpu = 0; icpu < ncpu; icpu++)
            {
                if (cpu[icpu].suspend != 0 && cpu[icpu].eatcycles != 0 && Attotime.attotime_compare(cpu[icpu].localtime, target) < 0)
                {
                    at = Attotime.attotime_sub(target, cpu[icpu].localtime);
                    cpu[icpu].cycles_running = (int)(at.seconds * cpu[icpu].cycles_per_second + at.attoseconds / cpu[icpu].attoseconds_per_cycle);
                    cpu[icpu].totalcycles += (ulong)cpu[icpu].cycles_running;
                    cpu[icpu].localtime = Attotime.attotime_add(cpu[icpu].localtime, new Atime(cpu[icpu].cycles_running / cpu[icpu].cycles_per_second, cpu[icpu].cycles_running * cpu[icpu].attoseconds_per_cycle));
                }
                cpu[icpu].suspend = cpu[icpu].nextsuspend;
                cpu[icpu].eatcycles = cpu[icpu].nexteatcycles;
            }
            Timer.timer_set_global_time(target);
            if (Timer.global_basetime.attoseconds == 0 && Machine.FORM.cheatform.lockState == cheatForm.LockState.LOCK_SECOND)
            {
                Machine.FORM.cheatform.ApplyCheat();
            }
        }
        public static void cpu_boost_interleave(Atime timeslice_time, Atime boost_duration)
        {
            if (Attotime.attotime_compare(timeslice_time, perfect_interleave) < 0)
                timeslice_time = perfect_interleave;
            Timer.timer_adjust_periodic(interleave_boost_timer, timeslice_time, timeslice_time);
            if (!Timer.timer_enabled(interleave_boost_timer_end) || Attotime.attotime_compare(Timer.timer_timeleft(interleave_boost_timer_end), boost_duration) < 0)
                Timer.timer_adjust_periodic(interleave_boost_timer_end, boost_duration, Attotime.ATTOTIME_NEVER);
        }
        public static void activecpu_abort_timeslice(int cpunum)
        {
            int current_icount;
            current_icount = cpu[cpunum].PendingCycles + 1;
            cpu[cpunum].cycles_stolen += current_icount;
            cpu[cpunum].cycles_running -= current_icount;
            cpu[cpunum].PendingCycles = -1;
        }
        public static void cpunum_suspend(int cpunum, byte reason, byte eatcycles)
        {
            cpu[cpunum].nextsuspend |= reason;
            cpu[cpunum].nexteatcycles = eatcycles;
            if (Cpuexec.activecpu >= 0)
            {
                activecpu_abort_timeslice(Cpuexec.activecpu);
            }
        }
        public static void cpunum_resume(int cpunum, byte reason)
        {
            cpu[cpunum].nextsuspend &= (byte)(~reason);
            if (Cpuexec.activecpu >= 0)
            {
                activecpu_abort_timeslice(Cpuexec.activecpu);
            }
        }
        public static bool cpunum_is_suspended(int cpunum, byte reason)
        {
            return ((cpu[cpunum].nextsuspend & reason) != 0);
        }
        public static Atime cpunum_get_localtime(int cpunum)
        {
            Atime result;
            result = cpu[cpunum].localtime;
            int cycles;
            cycles = cpu[cpunum].cycles_running - cpu[cpunum].PendingCycles;
            result = Attotime.attotime_add(result, new Atime(cycles / cpu[cpunum].cycles_per_second, cycles * cpu[cpunum].attoseconds_per_cycle));
            return result;
        }
        public static void cpunum_suspend_until_trigger(int cpunum, int trigger, int eatcycles)
        {
            cpunum_suspend(cpunum, (byte)SUSPEND_REASON_TRIGGER, (byte)eatcycles);
            cpu[cpunum].trigger = trigger;
        }
        public static void on_vblank()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                    CPS.cps1_interrupt();
                    break;
                case "CPS2":
                    iloops = 0;
                    CPS.cps2_interrupt();
                    Timer.timer_adjust_periodic(Cpuexec.cpu[0].partial_frame_timer, Cpuexec.cpu[0].partial_frame_period, Attotime.ATTOTIME_NEVER);
                    break;
                case "Neo Geo":
                    break;
                case "Namco System 1":
                    for (int cpunum = 0; cpunum < ncpu; cpunum++)
                    {
                        //if (!cpunum_is_suspended(cpunum, (byte)(SUSPEND_REASON_HALT | SUSPEND_REASON_RESET | SUSPEND_REASON_DISABLE)))
                        {
                            Cpuint.cpunum_set_input_line(cpunum, 0, LineState.ASSERT_LINE);
                        }
                    }
                    break;
                case "IGS011":
                    iloops = 0;
                    IGS011.lhb2_interrupt();
                    Timer.timer_adjust_periodic(Cpuexec.cpu[0].partial_frame_timer, Cpuexec.cpu[0].partial_frame_period, Attotime.ATTOTIME_NEVER);
                    break;
                case "PGM":
                    PGM.drgw_interrupt();
                    break;
                case "M72":
                    iloops = 0;
                    vblank_interrupt();
                    Cpuexec.cpu[1].partial_frame_period = Attotime.attotime_div(Video.frame_update_time, (uint)vblank_interrupts_per_frame);
                    Timer.timer_adjust_periodic(Cpuexec.cpu[1].partial_frame_timer, Cpuexec.cpu[1].partial_frame_period, Attotime.ATTOTIME_NEVER);
                    break;
                case "M92":
                    break;
            }
        }
        public static void trigger_partial_frame_interrupt()
        {
            switch (Machine.sBoard)
            {
                case "CPS2":
                case "IGS011":
                    if (iloops == 0)
                    {
                        iloops = vblank_interrupts_per_frame;
                    }
                    iloops--;
                    vblank_interrupt();
                    if (iloops > 1)
                    {
                        Timer.timer_adjust_periodic(Cpuexec.cpu[0].partial_frame_timer, Cpuexec.cpu[0].partial_frame_period, Attotime.ATTOTIME_NEVER);
                    }
                    break;
                case "M72":
                    if (iloops == 0)
                    {
                        iloops = vblank_interrupts_per_frame;
                    }
                    iloops--;
                    vblank_interrupt();
                    if (iloops > 1)
                    {
                        Timer.timer_adjust_periodic(Cpuexec.cpu[1].partial_frame_timer, Cpuexec.cpu[1].partial_frame_period, Attotime.ATTOTIME_NEVER);
                    }
                    break;
            }
        }
        public static void end_interleave_boost()
        {
            Timer.timer_adjust_periodic(interleave_boost_timer, Attotime.ATTOTIME_NEVER, Attotime.ATTOTIME_NEVER);
        }
        public static void compute_perfect_interleave()
        {
            long smallest = cpu[0].attoseconds_per_cycle;
            int cpunum;
            perfect_interleave = Attotime.ATTOTIME_ZERO;
            perfect_interleave.attoseconds = Attotime.ATTOSECONDS_PER_SECOND - 1;
            for (cpunum = 1; cpunum < ncpu; cpunum++)
            {
                if (cpu[cpunum].attoseconds_per_cycle < smallest)
                {
                    perfect_interleave.attoseconds = smallest;
                    smallest = cpu[cpunum].attoseconds_per_cycle;
                }
                else if (cpu[cpunum].attoseconds_per_cycle < perfect_interleave.attoseconds)
                    perfect_interleave.attoseconds = cpu[cpunum].attoseconds_per_cycle;
            }
            if (perfect_interleave.attoseconds == Attotime.ATTOSECONDS_PER_SECOND - 1)
                perfect_interleave.attoseconds = cpu[0].attoseconds_per_cycle;
        }
    }
}
