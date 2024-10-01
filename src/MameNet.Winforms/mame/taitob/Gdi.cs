using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace mame
{
    public partial class Taitob
    {
        public static bool bBg, bFg, bTx, bSprite;
        public static void GDIInit()
        {

        }
        public static Bitmap GetBg()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 16;
            tileheight = tilewidth;
            rows = 0x40;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            int iByte;
            int iCode, iCode1, iColor;
            int iTile, iFlag;
            int pen_data_offset, palette_base;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0;
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
                        iOffset3 = i4 * cols + i3;
                        iTile = Taitob.TC0180VCU_ram[iOffset3 + Taitob.bg_rambank[0]];
                        iCode = iTile;
                        iCode1 = iCode % Taitob.bg_tilemap.total_elements;
                        iColor = Taitob.TC0180VCU_ram[iOffset3 + Taitob.bg_rambank[1]];
                        pen_data_offset = iCode1 * 0x100;
                        palette_base = 0x10 * (Taitob.b_bg_color_base + (iColor & 0x3f));
                        iFlag = (((iColor & 0x00c0) >> 6) & 0x03) ^ (Taitob.bg_tilemap.attributes & 0x03);
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
                                iOffset = pen_data_offset + i2 * 0x10 + i1;
                                iByte = Taitob.gfx1rom[iOffset];
                                if (palette_base == 0 && iByte == 0)
                                {
                                    c1 = Color.Transparent;
                                }
                                else
                                {
                                    c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
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
            tilewidth = 16;
            tileheight = tilewidth;
            rows = 0x40;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            int iByte;
            int iCode, iCode1, iColor;
            int iTile, iFlag;
            int pen_data_offset, palette_base;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0;
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
                        iOffset3 = i4 * cols + i3;
                        iTile = Taitob.TC0180VCU_ram[iOffset3 + Taitob.fg_rambank[0]];
                        iCode = iTile;
                        iCode1 = iCode % Taitob.bg_tilemap.total_elements;
                        iColor = Taitob.TC0180VCU_ram[iOffset3 + Taitob.fg_rambank[1]];
                        pen_data_offset = iCode1 * 0x100;
                        palette_base = 0x10 * (Taitob.b_fg_color_base + (iColor & 0x3f));
                        iFlag = (((iColor & 0x00c0) >> 6) & 0x03) ^ (Taitob.fg_tilemap.attributes & 0x03);
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
                                iOffset = pen_data_offset + i2 * 0x10 + i1;
                                iByte = Taitob.gfx1rom[iOffset];
                                if (iByte == 0)
                                {
                                    c1 = Color.Transparent;
                                }
                                else
                                {
                                    c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
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
        public static Bitmap GetTx()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x20;
            cols = 0x40;
            width = tilewidth * cols;
            height = tileheight * rows;
            int iByte;
            int iCode, iCode1, iColor;
            int iTile, iFlag;
            int pen_data_offset, palette_base;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0;
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
                        iOffset3 = i4 * cols + i3;
                        iTile = Taitob.TC0180VCU_ram[iOffset3 + Taitob.tx_rambank];
                        iCode = (iTile & 0x07ff) | ((Taitob.TC0180VCU_ctrl[4 + ((iTile & 0x800) >> 11)] >> 8) << 11);
                        iCode1 = iCode % Taitob.tx_tilemap.total_elements;
                        iColor = Taitob.b_tx_color_base + ((iTile >> 12) & 0x0f);
                        pen_data_offset = iCode1 * 0x40;
                        palette_base = 0x10 * iColor;
                        iFlag = Taitob.tx_tilemap.attributes & 0x03;
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
                                iByte = Taitob.gfx0rom[iOffset];
                                if (iByte == 0)
                                {
                                    c1 = Color.Transparent;
                                }
                                else
                                {
                                    c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
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
        public static Bitmap GetSprinte()
        {
            Color c1 = new Color();
            Bitmap bm1;
            bm1 = new Bitmap(512,256);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                int x, y;
                for (y = 0x10; y <= 0xef; y++)
                {
                    for (x = 0; x <= 0x13f; x++)
                    {
                        ushort c = framebuffer[framebuffer_page][y * 512 + x];
                        if (c == 0)
                        {
                            c1 = Color.Transparent;
                        }
                        else
                        {
                            c1 = Color.FromArgb((int)Palette.entry_color[b_sp_color_base + c]);
                            ptr2 = ptr + (y * 0x200 + x) * 4;
                            *ptr2 = c1.B;
                            *(ptr2 + 1) = c1.G;
                            *(ptr2 + 2) = c1.R;
                            *(ptr2 + 3) = c1.A;
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetAllGDI()
        {
            Bitmap bm1 = new Bitmap(0x200, 0x100), bm2;
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(Color.Transparent);
            if (bBg)
            {
                bm2 = GetBg();
                short scrollx, scrolly;
                scrollx = (short)taitob_scroll[0x200];
                scrolly = (short)taitob_scroll[0x201];
                g.DrawImage(bm2, 0, 0x10);
            }
            if (bFg)
            {
                bm2 = GetFg();
                short scrollx, scrolly;
                scrollx = (short)taitob_scroll[0];
                scrolly = (short)taitob_scroll[1];
                g.DrawImage(bm2, scrollx, scrolly);
            }
            if (bTx)
            {
                bm2 = GetTx();
                g.DrawImage(bm2, 0, 0);
            }
            if (bSprite)
            {
                bm2 = GetSprinte();
                g.DrawImage(bm2, 0, 0);
            }
            return bm1;
        }
    }
}
