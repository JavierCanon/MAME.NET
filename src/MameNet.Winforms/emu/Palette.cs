using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace mame
{
    public class Palette
    {
        public static uint[] entry_color;
        private static uint trans_uint;
        private static int numcolors;
        public static Color trans_color;
        public static void palette_init()
        {
            int index;            
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0xc00;
                    break;
                case "Namco System 1":
                    trans_color = Color.Black;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x2001;
                    break;
                case "IGS011":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x800;
                    break;
                case "PGM":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x901;
                    break;
                case "M72":
                    trans_color = Color.Black;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x201;
                    break;
                case "M92":
                    trans_color = Color.Black;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x801;
                    break;
            }
            entry_color = new uint[numcolors];
            for (index = 0; index < numcolors; index++)
            {
                palette_entry_set_color(index, make_argb(0xff, pal1bit((byte)(index >> 0)), pal1bit((byte)(index >> 1)), pal1bit((byte)(index >> 2))));
            }
            switch (Machine.sBoard)
            {
                case "Namco System 1":
                    entry_color[0x2000] = trans_uint;
                    break;
                case "PGM":
                    entry_color[0x900] = trans_uint;
                    break;
                case "M72":
                    entry_color[0x200] = trans_uint;
                    break;
                case "M92":
                    entry_color[0x800] = trans_uint;
                    break;
            }
        }
        public static void palette_entry_set_color(int index, uint rgb)
        {
            if (index >= numcolors || entry_color[index] == rgb)
            {
                return;
            }
            if (index % 0x10 == 0x0f && rgb == 0)
            {
                entry_color[index] = trans_uint;
            }
            else
            {
                entry_color[index] = 0xff000000 | rgb;
            }
        }
        public static void palette_entry_set_color2(int index, uint rgb)
        {
            if (index >= numcolors || entry_color[index] == rgb)
            {
                return;
            }
            entry_color[index] = 0xff000000 | rgb;
        }
        public static uint make_rgb(int r, int g, int b)
        {
            return ((((uint)(r) & 0xff) << 16) | (((uint)(g) & 0xff) << 8) | ((uint)(b) & 0xff));
        }
        public static uint make_argb(int a, int r, int g, int b)
        {
            return ((((uint)(a) & 0xff) << 24) | (((uint)(r) & 0xff) << 16) | (((uint)(g) & 0xff) << 8) | ((uint)(b) & 0xff));
        }
        public static byte pal1bit(byte bits)
        {
	        return (byte)(((bits & 1)!=0) ? 0xff : 0x00);
        }
        public static byte pal5bit(byte bits)
        {
            bits &= 0x1f;
            return (byte)((bits << 3) | (bits >> 2));
        }
    }
}