using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.m68000;

namespace mame
{
    public class State
    {
        public delegate void savestate_delegate(BinaryWriter sw);
        public delegate void loadstate_delegate(BinaryReader sr);
        public static savestate_delegate savestate_callback;
        public static loadstate_delegate loadstate_callback;
        public static void state_init()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                    savestate_callback = CPS.SaveStateBinaryC;
                    loadstate_callback = CPS.LoadStateBinaryC;
                    break;
                case "CPS-1(QSound)":
                    savestate_callback = CPS.SaveStateBinaryQ;
                    loadstate_callback = CPS.LoadStateBinaryQ;
                    break;
                case "CPS2":
                    savestate_callback = CPS.SaveStateBinaryC2;
                    loadstate_callback = CPS.LoadStateBinaryC2;
                    break;
                case "Neo Geo":
                    savestate_callback = Neogeo.SaveStateBinary;
                    loadstate_callback = Neogeo.LoadStateBinary;
                    break;
                case "Namco System 1":
                    savestate_callback = Namcos1.SaveStateBinary;
                    loadstate_callback = Namcos1.LoadStateBinary;
                    break;
                case "IGS011":
                    savestate_callback = IGS011.SaveStateBinary;
                    loadstate_callback = IGS011.LoadStateBinary;
                    break;
                case "PGM":
                    savestate_callback = PGM.SaveStateBinary;
                    loadstate_callback = PGM.LoadStateBinary;
                    break;
                case "M72":
                    savestate_callback = M72.SaveStateBinary;
                    loadstate_callback = M72.LoadStateBinary;
                    break;
                case "M92":
                    savestate_callback = M92.SaveStateBinary;
                    loadstate_callback = M92.LoadStateBinary;
                    break;
            }
        }
    }
}
