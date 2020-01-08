using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ui;

namespace mame
{
    public class Machine
    {
        public static string sName, sParent, sBoard, sDirection, sDescription, sManufacturer;
        public static List<string> lsParents;
        public static mainForm FORM;
        public static RomInfo rom;
        public static bool bRom;
        public delegate void machine_delegate();
        public static machine_delegate machine_reset_callback;
        public static void machine_start()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                    Palette.palette_init();
                    Tilemap.tilemap_init();
                    Eeprom.eeprom_init();
                    CPS.video_start_cps();
                    machine_reset_callback = CPS.machine_reset_cps;                    
                    break;
                case "Neo Geo":
                    Neogeo.nvram_handler_load_neogeo();
                    Neogeo.machine_start_neogeo();
                    Neogeo.video_start_neogeo();                    
                    machine_reset_callback = Neogeo.machine_reset_neogeo;                    
                    break;
                case "Namco System 1":
                    Palette.palette_init();
                    Tilemap.tilemap_init();
                    Namcos1.driver_init();
                    Namcos1.video_start_namcos1();                    
                    machine_reset_callback = Namcos1.machine_reset_namcos1;
                    break;
                case "IGS011":
                    Palette.palette_init();
                    IGS011.video_start_igs011();
                    machine_reset_callback = IGS011.machine_reset_igs011;
                    break;
                case "PGM":
                    Palette.palette_init();
                    Tilemap.tilemap_init();
                    PGM.device_init();
                    PGM.video_start_pgm();
                    machine_reset_callback = PGM.machine_reset_pgm;
                    break;
                case "M72":
                    Palette.palette_init();
                    Tilemap.tilemap_init();
                    M72.machine_start_m72();
                    M72.video_start_m72();
                    machine_reset_callback = M72.machine_reset_m72;
                    switch (Machine.sName)
                    {
                        case "ltswords":
                        case "kengo":
                        case "kengoa":
                            machine_reset_callback = M72.machine_reset_kengo;
                            break;
                    }
                    break;
                case "M92":
                    Palette.palette_init();
                    Tilemap.tilemap_init();
                    M92.machine_start_m92();
                    M92.video_start_m92();
                    machine_reset_callback = M92.machine_reset_m92;
                    break;
            }
        }
        public static byte[] GetNeogeoRom(string sFile)
        {
            byte[] bb1;
            if (File.Exists("roms\\neogeo\\" + sFile))
            {
                FileStream fs1 = new FileStream("roms\\neogeo\\" + sFile, FileMode.Open);
                int n1 = (int)fs1.Length;
                bb1 = new byte[n1];
                fs1.Read(bb1, 0, n1);
                fs1.Close();
            }
            else
            {
                bb1 = null;
            }
            return bb1;
        }
        public static byte[] GetRom(string sFile)
        {
            byte[] bb1 = null;
            int n1;
            foreach (string s1 in lsParents)
            {
                if (File.Exists("roms\\" + s1 + "\\" + sFile))
                {
                    FileStream fs1 = new FileStream("roms\\" + s1 + "\\" + sFile, FileMode.Open);
                    n1 = (int)fs1.Length;
                    bb1 = new byte[n1];
                    fs1.Read(bb1, 0, n1);
                    fs1.Close();
                    break;
                }
            }
            return bb1;
        }
    }
}
