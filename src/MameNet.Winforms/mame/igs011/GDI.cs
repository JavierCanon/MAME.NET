using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace mame
{
    public partial class IGS011
    {
        public static void GDIInit()
        {

        }
        public static Bitmap GetBmp(string layer1)
        {
            int width = 0x200, height = 0xf0;
            int x, y, l, scr_addr, pri_addr;
            int pri_ram_offset;
            pri_ram_offset = (priority & 7) * 0x100;
            Color c1 = new Color();
            Bitmap bm1;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (y = 0; y < 0xef; y++)
                {
                    for (x = 0; x < 0x1ff; x++)
                    {
                        scr_addr = x + y * 0x200;
                        pri_addr = 0xff;
                        for (l = 0; l < 8; l++)
                        {
                            if (layer[l][scr_addr] != 0xff)
                            {
                                pri_addr &= ~(1 << l);
                            }
                        }
                        if (layer1 == "normal")
                        {
                            l = priority_ram[pri_ram_offset + pri_addr] & 7;
                        }
                        else
                        {
                            l = int.Parse(layer1);
                        }
                        c1 = Color.FromArgb((int)Palette.entry_color[layer[l][scr_addr] | (l << 8)]);
                        ptr2 = ptr + (y * width + x) * 4;
                        *ptr2 = c1.B;
                        *(ptr2 + 1) = c1.G;
                        *(ptr2 + 2) = c1.R;
                        *(ptr2 + 3) = c1.A;
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetAllGDI(string layer)
        {
            Bitmap bm1 = new Bitmap(0x200, 0xf0), bm2;
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(Color.Transparent);
            bm2 = GetBmp(layer);
            g.DrawImage(bm2, 0, 0);
            /*if (bBg)
            {
                bm2 = GetBg_sf();
                g.DrawImage(bm2, -bg_scrollx, 0);
            }
            if (bFg)
            {
                bm2 = GetFg_sf();
                g.DrawImage(bm2, -fg_scrollx, 0);
            }
            if (bTx)
            {
                bm2 = GetTx_sf();
                g.DrawImage(bm2, 0, 0);
            }
            if (bSprite)
            {
                bm2 = GetSprite_sf();
                g.DrawImage(bm2, 0, 0);
            }*/
            return bm1;
        }
    }
}
