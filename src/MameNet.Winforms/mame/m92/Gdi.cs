using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace mame
{
    public partial class M92
    {
        public static bool bG00,bG01,bG10,bG11,bG20,bG21, bSprite;
        public static void GDIInit()
        {

        }
        public static Bitmap GetG00()
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
            int iTile, iFlag, iGroup;
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
                if ((pf_master_control[0] & 0x40) != 0)
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[0].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[0].tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[0].tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000 - m92_vram_data[0xf400 / 2 + (y0 + dy0 * i2)] + x0 + dx0 * i1) % width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[0].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[0].tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[0].tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000 - pf_layer[0].control[2] + x0 + dx0 * i1) % width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetG01()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x40;
            cols = 0x80;
            width = tilewidth * cols;
            height = tileheight * rows;
            int iByte;
            int xoffs;
            int iCode, iCode1, iAttr;
            int iTile, iFlag, iGroup;
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
                if ((pf_master_control[0] & 0x40) != 0)
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[0].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[0].wide_tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[0].wide_tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000-m92_vram_data[0xf400 / 2 + (y0 + dy0 * i2)] + x0 + dx0 * i1) % width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[0].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            if (iCode == 0x1041)
                            {
                                int i11 = 1;
                            }
                            iCode1 = iCode % pf_layer[0].wide_tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[0].wide_tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) % height * width + (0x10000- pf_layer[0].control[2] + x0 + dx0 * i1) % width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetG10()
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
            int iCode, iCode1, iAttr,iGroup;
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
                if ((pf_master_control[1] & 0x40) != 0)
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[1].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[1].tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[1].tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000 - m92_vram_data[0xf800 / 2 + (y0 + dy0 * i2)] + x0 + dx0 * i1) % width) * 4;
                                    //ptr2 = ptr + ((y0 + dy0 * i2) * width + (x0 + dx0 * i1)) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[1].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[1].tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[1].tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000 - pf_layer[1].control[2] + x0 + dx0 * i1) % width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetG11()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x40;
            cols = 0x80;
            width = tilewidth * cols;
            height = tileheight * rows;
            int iByte;
            int iCode, iCode1, iAttr;
            int iTile, iFlag, iGroup;
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
                if ((pf_master_control[1] & 0x40) != 0)
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[1].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[1].wide_tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[1].wide_tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000-m92_vram_data[0xf800/2 + (y0 + dy0 * i2)] + x0 + dx0 * i1)%width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[1].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[1].wide_tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[1].wide_tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000 - pf_layer[1].control[2] + x0 + dx0 * i1) % width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetG20()
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
            int iCode, iCode1, iAttr, iGroup;
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
                if ((pf_master_control[2] & 0x40) != 0)
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[2].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[2].tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[2].tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000-m92_vram_data[0xfc00/2 + (y0 + dy0 * i2)] + x0 + dx0 * i1)%width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[2].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[2].tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[2].tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000 - pf_layer[2].control[2] + x0 + dx0 * i1) % width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetG21()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x40;
            cols = 0x80;
            width = tilewidth * cols;
            height = tileheight * rows;
            int iByte;
            int iCode, iCode1, iAttr;
            int iTile, iFlag, iGroup;
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
                if ((pf_master_control[2] & 0x40) != 0)
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[2].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] + ((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[2].wide_tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[2].wide_tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000-m92_vram_data[0xfc00 + (y0 + dy0 * i2)] + x0 + dx0 * i1)%width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = 2 * (i4 * cols + i3) + M92.pf_layer[2].vram_base;
                            iAttr = m92_vram_data[iOffset3 + 1];
                            iTile = m92_vram_data[iOffset3] +((iAttr & 0x8000) << 1);
                            iCode = iTile;
                            if ((iAttr & 0x100) != 0)
                            {
                                iGroup = 2;
                            }
                            else if ((iAttr & 0x80) != 0)
                            {
                                iGroup = 1;
                            }
                            else
                            {
                                iGroup = 0;
                            }
                            iCode1 = iCode % pf_layer[2].wide_tmap.total_elements;
                            pen_data_offset = iCode1 * 0x40;
                            palette_base = 0x10 * (iAttr & 0x7f);
                            iFlag = ((iAttr >> 9) & 3) ^ (pf_layer[2].wide_tmap.attributes & 0x03);
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
                                    iByte = M92.gfx11rom[iOffset];
                                    if (iByte == 0)
                                    {
                                        c1 = Color.Transparent;
                                    }
                                    else
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                    }
                                    ptr2 = ptr + ((y0 + dy0 * i2) * width + (0x10000 - pf_layer[2].control[2] + x0 + dx0 * i1) % width) * 4;
                                    *ptr2 = c1.B;
                                    *(ptr2 + 1) = c1.G;
                                    *(ptr2 + 2) = c1.R;
                                    *(ptr2 + 3) = c1.A;
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetSprite(int n1, int n2)
        {
            Bitmap bm1;
            bm1 = new Bitmap(0x200, 0x200);
            int offs, k, i5, i6, iByte;
            int xdir, ydir;
            Color c1 = new Color();
            for (k = 0; k < 8; k++)
            {
                //for (offs = 0; offs < m92_sprite_list; )
                for(offs=m92_sprite_list-4;offs>=0;)
                {
                    int x, y, sprite, colour, fx, fy, x_multi, y_multi, i, j, s_ptr, pri_back, pri_sprite;
                    y = Generic.buffered_spriteram16[offs + 0] & 0x1ff;
                    x = Generic.buffered_spriteram16[offs + 3] & 0x1ff;
                    if ((Generic.buffered_spriteram16[offs + 2] & 0x0080) != 0)
                    {
                        pri_back = 0;
                    }
                    else
                    {
                        pri_back = 2;
                    }
                    sprite = Generic.buffered_spriteram16[offs + 1];
                    colour = Generic.buffered_spriteram16[offs + 2] & 0x007f;
                    pri_sprite = (Generic.buffered_spriteram16[offs + 0] & 0xe000) >> 13;
                    fx = (Generic.buffered_spriteram16[offs + 2] >> 8) & 1;
                    fy = (Generic.buffered_spriteram16[offs + 2] >> 9) & 1;
                    y_multi = (Generic.buffered_spriteram16[offs + 0] >> 9) & 3;
                    x_multi = (Generic.buffered_spriteram16[offs + 0] >> 11) & 3;
                    y_multi = 1 << y_multi;
                    x_multi = 1 << x_multi;
                    //offs += 4 * x_multi;
                    if (offs < n1 || offs > n2)
                    {
                        offs -= 4 * x_multi;
                        continue;
                    }
                    offs -= 4 * x_multi;
                    if (pri_sprite != k)
                    {
                        continue;
                    }
                    x = x;
                    y = 512 - 16 - y;
                    if (fx != 0)
                    {
                        x += 16 * (x_multi - 1);
                        xdir = -1;
                    }
                    else
                    {
                        xdir = 1;
                    }
                    if (fy != 0)
                    {
                        ydir = -1;
                    }
                    else
                    {
                        ydir = 1;
                    }
                    for (j = 0; j < x_multi; j++)
                    {
                        s_ptr = 8 * j;
                        if (fy == 0)
                        {
                            s_ptr += y_multi - 1;
                        }
                        x &= 0x1ff;
                        for (i = 0; i < y_multi; i++)
                        {
                            for (i5 = 0; i5 < 0x10; i5++)
                            {
                                for (i6 = 0; i6 < 0x10; i6++)
                                {
                                    iByte = gfx21rom[(sprite + s_ptr) * 0x100 + i5 + i6 * 0x10];
                                    if (iByte != 0)
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[0x10 * colour + iByte]);
                                        if (x + xdir * i5 >= 0 && x + xdir * i5 <= 0x1ff && y - i * 16 + ydir * i6 >= 0 && y - i * 16 + ydir * i6 <= 0x1ff)
                                        {
                                            bm1.SetPixel(x + xdir * i5, y - i * 16 + ydir * i6, c1);
                                        }
                                    }
                                }
                            }
                            //Drawgfx.common_drawgfx_m92(gfx21rom, sprite + s_ptr, colour, fx, fy, x, y - i * 16, cliprect, (uint)(pri_back | (1 << 31)));
                            //Drawgfx.common_drawgfx_m92(gfx21rom, sprite + s_ptr, colour, fx, fy, x - 512, y - i * 16, cliprect, (uint)(pri_back | (1 << 31)));
                            if (fy != 0)
                            {
                                s_ptr++;
                            }
                            else
                            {
                                s_ptr--;
                            }
                        }
                        if (fx != 0)
                        {
                            x -= 16;
                        }
                        else
                        {
                            x += 16;
                        }
                    }
                }
            }
            return bm1;
        }
        public static Bitmap GetAllGDI(int n1, int n2)
        {
            Bitmap bm1 = new Bitmap(0x200, 0x200), bm2;
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(Color.Transparent);
            if (bG21 && pf_layer[2].wide_tmap.enable && (((~pf_master_control[2] >> 4) & 1) != 0))
            {
                bm2 = GetG21();
                int y = pf_layer[2].wide_tmap.effective_colscroll(0);
                g.DrawImage(bm2, 0, -0x200 + y);
                g.DrawImage(bm2, 0, y);
            }
            if (bG20 && pf_layer[2].tmap.enable && (((~pf_master_control[2] >> 4) & 1) != 0))
            {
                bm2 = GetG20();
                int y = pf_layer[2].tmap.effective_colscroll(0);
                g.DrawImage(bm2, 0, -0x200 + y);
                g.DrawImage(bm2, 0, y);
            }
            if (bG11 && pf_layer[1].wide_tmap.enable)
            {
                bm2 = GetG11();
                int y = pf_layer[1].wide_tmap.effective_colscroll(0);
                g.DrawImage(bm2, 0, -0x200 + y);
                g.DrawImage(bm2, 0, y);
            }
            if (bG10 && pf_layer[1].tmap.enable)
            {
                bm2 = GetG10();
                int y = pf_layer[1].tmap.effective_colscroll(0);
                g.DrawImage(bm2, 0, -0x200 + y);
                g.DrawImage(bm2, 0, y);
            }
            if (bG01 && pf_layer[0].wide_tmap.enable)
            {
                bm2 = GetG01();
                int y = pf_layer[0].wide_tmap.effective_colscroll(0);
                g.DrawImage(bm2, 0, -0x200 + y);
                g.DrawImage(bm2, 0, y);
            }
            if (bG00 && pf_layer[0].tmap.enable)
            {
                bm2 = GetG00();
                int y = pf_layer[0].tmap.effective_colscroll(0);
                g.DrawImage(bm2, 0, -0x200 + y);
                g.DrawImage(bm2, 0, y);
            }
            if (bSprite)
            {
                bm2 = GetSprite(n1, n2);
                g.DrawImage(bm2, 0, -0x80);
            }
            return bm1;
        }
    }
}
