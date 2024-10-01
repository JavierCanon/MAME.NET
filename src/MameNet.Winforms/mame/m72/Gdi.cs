using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace mame
{
    public partial class M72
    {
        public static bool bBg, bFg, bSprite;
        public static void GDIInit()
        {

        }
        public static void GetData()
        {

        }
        public static Bitmap GetBG()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x40;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            int iByte;
            int iCode, iCode1, iAttr;
            int iColor, iFlag, iGroup;
            int idx = 0, pri, pen_data_offset, palette_base;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0, match;
            Color c1 = new Color();
            Bitmap bm1;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (i3 = 0; i3 < cols; i3++)
                {
                    for (i4 = 0; i4 < rows; i4++)
                    {
                        iOffset3 = (i4 * 0x40 + i3) * 2;
                        iCode = m72_videoram2[iOffset3*2]+m72_videoram2[iOffset3*2+1]*0x100;
                        iColor = m72_videoram2[(iOffset3 + 1) * 2];
                        iAttr = m72_videoram2[(iOffset3 + 1) * 2 + 1];
                        if ((iAttr & 0x01) != 0)
                        {
                            pri = 2;
                        }
                        else if ((iColor & 0x80) != 0)
                        {
                            pri = 1;
                        }
                        else
                        {
                            pri = 0;
                        }
                        iCode1 = iCode % bg_tilemap.total_elements;
                        pen_data_offset = iCode1 * 0x40;
                        palette_base = 0x100 + 0x10 * (iColor & 0x0f);
                        iFlag = (((iColor & 0x60) >> 5) & 3) ^ (bg_tilemap.attributes & 0x03);
                        if (iFlag == 0)
                        {
                            x0 = tilewidth * i3;
                            y0 = tileheight * i4;
                            dx0 = 1;
                            dy0 = 1;
                        }
                        else if (iFlag == 1)
                        {
                            x0 = tilewidth * i3 + tilewidth - 1;
                            y0 = tileheight * i4;
                            dx0 = -1;
                            dy0 = 1;
                        }
                        else if (iFlag == 2)
                        {
                            x0 = tilewidth * i3;
                            y0 = tileheight * i4 + tileheight - 1;
                            dx0 = 1;
                            dy0 = -1;
                        }
                        else if (iFlag == 3)
                        {
                            x0 = tilewidth * i3 + tilewidth - 1;
                            y0 = tileheight * i4 + tileheight - 1;
                            dx0 = -1;
                            dy0 = -1;
                        }
                        for (i1 = 0; i1 < tilewidth; i1++)
                        {
                            for (i2 = 0; i2 < tileheight; i2++)
                            {
                                iOffset = pen_data_offset + i2 * 8 + i1;
                                iByte = M72.gfx21rom[iOffset];
                                if (iByte == 0)
                                {
                                    c1 = Color.Transparent;
                                }
                                else
                                {
                                    c1 = Color.FromArgb((int)Palette.entry_color[0x100 + 0x10 * (iColor & 0x0f) + iByte]);
                                }
                                ptr2 = ptr + ((y0 + dy0 * i2) * width + x0 + dx0 * i1) * 4;
                                *ptr2 = c1.B;
                                *(ptr2 + 1) = c1.G;
                                *(ptr2 + 2) = c1.R;
                                *(ptr2 + 3) = c1.A;
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetFg()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x40;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            int iByte;
            int iCode, iCode1, iAttr;
            int iColor, iFlag, iGroup;
            int idx = 0, pri, pen_data_offset, palette_base;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0, match,y0offset;
            Color c1 = new Color();
            Bitmap bm1;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (i3 = 0; i3 < cols; i3++)
                {
                    for (i4 = 0; i4 < rows; i4++)
                    {
                        iOffset3 = (i4 * 0x40 + i3) * 2;
                        iCode = m72_videoram1[iOffset3 * 2] + m72_videoram1[iOffset3 * 2 + 1] * 0x100;
                        iColor = m72_videoram1[(iOffset3 + 1) * 2];
                        iAttr = m72_videoram1[(iOffset3 + 1) * 2 + 1];
                        if ((iAttr & 0x01) != 0)
                        {
                            pri = 2;
                        }
                        else if ((iColor & 0x80) != 0)
                        {
                            pri = 1;
                        }
                        else
                        {
                            pri = 0;
                        }
                        if (pri == 0)
                        {
                            y0offset = 0;// 0x90;
                        }
                        else
                        {
                            y0offset = 0;
                        }
                        iCode1 = iCode % bg_tilemap.total_elements;
                        pen_data_offset = iCode1 * 0x40;
                        palette_base = 0x100 + 0x10 * (iColor & 0x0f);
                        iFlag = (((iColor & 0x60) >> 5) & 3) ^ (bg_tilemap.attributes & 0x03);
                        if (iFlag == 0)
                        {
                            x0 = tilewidth * i3;
                            y0 = y0offset + tileheight * i4;
                            dx0 = 1;
                            dy0 = 1;
                        }
                        else if (iFlag == 1)
                        {
                            x0 = tilewidth * i3 + tilewidth - 1;
                            y0 = y0offset + tileheight * i4;
                            dx0 = -1;
                            dy0 = 1;
                        }
                        else if (iFlag == 2)
                        {
                            x0 = tilewidth * i3;
                            y0 = y0offset+ tileheight * i4 + tileheight - 1;
                            dx0 = 1;
                            dy0 = -1;
                        }
                        else if (iFlag == 3)
                        {
                            x0 = tilewidth * i3 + tilewidth - 1;
                            y0 = y0offset + tileheight * i4 + tileheight - 1;
                            dx0 = -1;
                            dy0 = -1;
                        }
                        for (i1 = 0; i1 < tilewidth; i1++)
                        {
                            for (i2 = 0; i2 < tileheight; i2++)
                            {
                                iOffset = pen_data_offset + i2 * 8 + i1;
                                iByte = M72.gfx21rom[iOffset];
                                if (iByte == 0)
                                {
                                    c1 = Color.Transparent;
                                }
                                else
                                {
                                    c1 = Color.FromArgb((int)Palette.entry_color[0x100 + 0x10 * (iColor & 0x0f) + iByte]);
                                }
                                ptr2 = ptr + (((y0 + dy0 * i2)%0x200) * width + x0 + dx0 * i1) * 4;
                                *ptr2 = c1.B;
                                *(ptr2 + 1) = c1.G;
                                *(ptr2 + 2) = c1.R;
                                *(ptr2 + 3) = c1.A;
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetSprite(int n1,int n2)
        {
            Bitmap bm1 = new Bitmap(512, 512);
            Color c1;
            int offs;
            int x0, y0, dx0, dy0, i5, i6,startx,starty,xdir,ydir;
            offs = 0;
            while (offs < 0x400 / 2)
            {
                int code, color, sx, sy, flipx, flipy, w, h, x, y;
                code = m72_spriteram[offs + 1];
                color = m72_spriteram[offs + 2] & 0x0f;
                sx = -256 + (m72_spriteram[offs + 3] & 0x3ff);
                sy = 384 - (m72_spriteram[offs + 0] & 0x1ff);
                flipx = m72_spriteram[offs + 2] & 0x0800;
                flipy = m72_spriteram[offs + 2] & 0x0400;
                w = 1 << ((m72_spriteram[offs + 2] & 0xc000) >> 14);
                h = 1 << ((m72_spriteram[offs + 2] & 0x3000) >> 12);
                sy -= 16 * h;
                if (offs >= n1 && offs <= n2)
                {
                    /*if (flip_screen_get())
                    {
                        sx = 512 - 16 * w - sx;
                        sy = 284 - 16 * h - sy;
                        flipx = !flipx;
                        flipy = !flipy;
                    }*/
                    if (flipy != 0)
                    {
                        starty = 15;
                        ydir = -1;
                    }
                    else
                    {
                        starty = 0;
                        ydir = 1;
                    }
                    if (flipx != 0)
                    {
                        startx = 15;
                        xdir = -1;
                    }
                    else
                    {
                        startx = 0;
                        xdir = 1;
                    }
                    for (x = 0; x < w; x++)
                    {
                        for (y = 0; y < h; y++)
                        {
                            int c = code;
                            if (flipx != 0)
                            {
                                c += 8 * (w - 1 - x);
                            }
                            else
                            {
                                c += 8 * x;
                            }
                            if (flipy != 0)
                            {
                                c += h - 1 - y;
                            }
                            else
                            {
                                c += y;
                            }
                            for (i5 = 0; i5 < 16; i5++)
                            {
                                for (i6 = 0; i6 < 16; i6++)
                                {
                                    if (sprites1rom[c * 0x100 + i6 * 0x10 + i5] == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        if (sx + 16 * x + i5 >= 0 && sx + 0x10 * x + i5 < 0x200)
                                        {
                                            c1 = Color.FromArgb((int)Palette.entry_color[0x10 * color + sprites1rom[c * 0x100 + i6 * 0x10 + i5]]);
                                            bm1.SetPixel((0x200 + sx + 0x10 * x + startx + i5 * xdir) % 0x200, sy + 0x10 * y + starty + i6 * ydir, c1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                offs += w * 4;
            }
            return bm1;
        }
        public static Bitmap GetAllGDI(int n1, int n2)
        {
            Bitmap bm1 = new Bitmap(0x200, 0x200), bm2;
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(Color.Transparent);
            if (bBg)
            {
                bm2 = GetBG();
                g.DrawImage(bm2, 0, 0);
            }
            if (bFg)
            {
                bm2 = GetFg();
                g.DrawImage(bm2, 0, 0);
            }
            if (bSprite)
            {
                bm2 = GetSprite(n1, n2);
                g.DrawImage(bm2, -0x40, 0);
            }
            return bm1;
        }
    }
}
