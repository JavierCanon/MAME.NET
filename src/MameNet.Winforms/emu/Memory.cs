using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public class Memory
    {
        public static byte[] mainrom, audiorom, mainram, audioram;
        public static void memory_reset()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                    CPS.sbyte0 = -1;
                    CPS.short1 = -1;
                    CPS.short2 = -1;
                    CPS.sbyte3 = -1;
                    break;
                case "CPS2":                    
                    CPS.short0 = -1;
                    CPS.short1 = -1;
                    CPS.short2 = -1;
                    break;
                case "Neo Geo":
                    Neogeo.short0 = unchecked((short)0xff00);
                    Neogeo.short1 = -1;
                    Neogeo.short2 = -1;
                    Neogeo.short3 = 0x3f;
                    Neogeo.short4 = unchecked((short)0xffc0);
                    Neogeo.short5 = unchecked((short)0xffff);
                    Neogeo.short6 = 0x03;
                    break;
                case "Namco System 1":
                    Namcos1.byte0 = 0xff;
                    Namcos1.byte1 = 0xff;                    
                    Namcos1.byte2 = 0xff;                    
                    switch (Machine.sName)
                    {
                        case "faceoff":
                            Namcos1.byte00 = 0xff;
                            Namcos1.byte01 = 0xff;
                            Namcos1.byte02 = 0xff;
                            Namcos1.byte03 = 0xff;
                            break;
                    }
                    break;
                case "IGS011":
                    IGS011.sbyte0 = -1;
                    IGS011.sbyte1 = -1;
                    IGS011.sbyte2 = -1;
                    IGS011.sbytec = -1;
                    break;
                case "PGM":
                    PGM.short0 = -1;
                    PGM.short1 = -1;
                    PGM.short2 = -1;
                    PGM.short3 = 0xff;
                    PGM.short4 = 0;
                    break;
                case "M72":
                    M72.ushort0 = 0xffff;
                    M72.ushort1 = 0xffff;
                    break;
                case "M92":
                    M92.ushort0 = 0xffff;
                    M92.ushort1 = 0xff7f;
                    M92.ushort2 = 0xffff;
                    break;
            }
        }
        public static void memory_reset2()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                    CPS.sbyte0_old = 0;
                    CPS.short1_old = 0;
                    CPS.short2_old = 0;
                    CPS.sbyte3_old = 0;
                    break;
                case "CPS2":
                    CPS.short0_old = 0;
                    CPS.short1_old = 0;
                    CPS.short2_old = 0;
                    break;
                case "Neo Geo":
                    Neogeo.short0_old = 0;
                    Neogeo.short1_old = 0;
                    Neogeo.short2_old = 0;
                    Neogeo.short3_old = 0;
                    Neogeo.short4_old = 0;
                    break;
                case "Namco System 1":
                    Namcos1.byte0_old = 0;
                    Namcos1.byte1_old = 0;
                    Namcos1.byte2_old = 0;
                    break;
                case "IGS011":
                    IGS011.sbyte0_old = 0;
                    IGS011.sbyte1_old = 0;
                    IGS011.sbyte2_old = 0;
                    IGS011.sbytec_old = 0;
                    break;
                case "PGM":
                    PGM.short0_old = 0;
                    PGM.short1_old = 0;
                    PGM.short2_old = 0;
                    PGM.short3_old = 0;
                    PGM.short4_old = 0;
                    break;
                case "M72":
                    M72.ushort0_old = 0;
                    M72.ushort1_old = 0;
                    break;
                case "M92":
                    M92.ushort0_old = 0;
                    M92.ushort1_old = 0;
                    M92.ushort2_old = 0;
                    break;
            }
        }
    }
}