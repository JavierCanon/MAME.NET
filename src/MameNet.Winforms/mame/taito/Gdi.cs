using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace mame
{
    public partial class Taito
    {
        public static bool bBg, bFg, bSprite;
        private static byte[,] flagsmapBG, flagsmapFG;
        private static byte[,] pen_to_flagsBG, pen_to_flagsFG;
        public static byte[,] priority_bitmapG;
        public static void GDIInit()
        {
            flagsmapBG = new byte[0x200, 0x200];
            flagsmapFG = new byte[0x200, 0x200];
            priority_bitmapG = new byte[0x140, 0x100];
        }
        public static Bitmap GetDraw_bublbobl()
        {
            int i, j,x0,y0,dx0,dy0,iOffset,iByte;
            int offs;
            int sx, sy, xc, yc;
            int gfx_num, gfx_attr, gfx_offs;
            int prom_line_offset;
            Color c1 = new Color();
            Bitmap bm1;
            bm1 = new Bitmap(256, 256);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                if (bublbobl_video_enable != 0)
                {
                    sx = 0;
                    for (offs = 0; offs < bublbobl_objectram_size; offs += 4)
                    {
                        if (bublbobl_objectram[offs] == 0 && bublbobl_objectram[offs + 1] == 0 && bublbobl_objectram[offs + 2] == 0 && bublbobl_objectram[offs + 3] == 0)
                        {
                            continue;
                        }
                        gfx_num = bublbobl_objectram[offs + 1];
                        gfx_attr = bublbobl_objectram[offs + 3];
                        prom_line_offset = 0x80 + ((gfx_num & 0xe0) >> 1);
                        gfx_offs = ((gfx_num & 0x1f) * 0x80);
                        if ((gfx_num & 0xa0) == 0xa0)
                        {
                            gfx_offs |= 0x1000;
                        }
                        sy = -bublbobl_objectram[offs + 0];
                        for (yc = 0; yc < 32; yc++)
                        {
                            if ((prom[prom_line_offset + yc / 2] & 0x08) != 0)
                            {
                                continue;
                            }
                            if ((prom[prom_line_offset + yc / 2] & 0x04) == 0)
                            {
                                sx = bublbobl_objectram[offs + 2];
                                if ((gfx_attr & 0x40) != 0)
                                {
                                    sx -= 256;
                                }
                            }
                            for (xc = 0; xc < 2; xc++)
                            {
                                int goffs, code, color, flipx, flipy, x, y;
                                goffs = gfx_offs + xc * 0x40 + (yc & 7) * 0x02 + (prom[prom_line_offset + yc / 2] & 0x03) * 0x10;
                                code = videoram[goffs] + 256 * (videoram[goffs + 1] & 0x03) + 1024 * (gfx_attr & 0x0f);
                                color = (videoram[goffs + 1] & 0x3c) >> 2;
                                flipx = videoram[goffs + 1] & 0x40;
                                flipy = videoram[goffs + 1] & 0x80;
                                x = sx + xc * 8;
                                y = (sy + yc * 8) & 0xff;
                                if (Generic.flip_screen_get() != 0)
                                {
                                    x = 248 - x;
                                    y = 248 - y;
                                    flipx = (flipx == 0) ? 1 : 0;
                                    flipy = (flipy == 0) ? 1 : 0;
                                }
                                if (flipx != 0)
                                {
                                    x0 = 7;
                                    dx0 = -1;
                                }
                                else
                                {
                                    x0 = 0;
                                    dx0 = 1;
                                }
                                if (flipy != 0)
                                {
                                    y0 = 7;
                                    dy0 = -1;
                                }
                                else
                                {
                                    y0 = 0;
                                    dy0 = 1;
                                }
                                for (i = 0; i < 8; i++)
                                {
                                    for (j = 0; j < 8; j++)
                                    {
                                        if (x + x0 + dx0 * i >= 0 && x + x0 + dx0 * i <= 255 && y + y0 + dy0 * j > 0 && y + y0 + dy0 * j < 255)
                                        {
                                            iOffset = code * 0x40 + j * 8 + i;
                                            iByte = Taito.gfx1rom[iOffset];
                                            if (iByte != 0xf)
                                            {
                                                c1 = Color.FromArgb((int)Palette.entry_color[color * 0x10 + iByte]);
                                                ptr2 = ptr + ((y + y0 + dy0 * j) * 0x100 + x + x0 + dx0 * i) * 4;
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
                        sx += 16;
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetBg_opwolf()
        {
            int i1, i2, iOffset, i3, i4,tile_index;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x40;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            int iByte;
            int code,attr, color,group,flags,attributes=0;
            int pen_data_offset, palette_base;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0;
            byte map;
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
                        tile_index = i4 * cols + i3;
                        code = (Taito.PC080SN_ram[0][Taito.PC080SN_bg_ram_offset[0][0] + 2 * tile_index + 1] & 0x3fff);
                        attr = Taito.PC080SN_ram[0][Taito.PC080SN_bg_ram_offset[0][0] + 2 * tile_index];
                        color = attr & 0x1ff;
                        code = code % Taito.PC080SN_tilemap[0][0].total_elements;
                        pen_data_offset = code * 0x40;
                        palette_base = 0x10 * color;
                        group = 0;
                        flags = (((attr & 0xc000) >> 14) & 3) ^ (attributes & 0x03);
                        if (flags == 0)
                        {
                            x0 = tilewidth * i3;
                            y0 = tileheight * i4;
                            dx0 = 1;
                            dy0 = 1;
                        }
                        else if (flags == 1)
                        {
                            x0 = tilewidth * i3 + tilewidth - 1;
                            y0 = tileheight * i4;
                            dx0 = -1;
                            dy0 = 1;
                        }
                        else if (flags == 2)
                        {
                            x0 = tilewidth * i3;
                            y0 = tileheight * i4 + tileheight - 1;
                            dx0 = 1;
                            dy0 = -1;
                        }
                        else if (flags == 3)
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
                                iOffset = pen_data_offset + i2 * tilewidth + i1;
                                iByte = Taito.gfx1rom[iOffset];
                                if (iByte == 0)
                                {
                                    c1 = Color.Black;
                                }
                                else
                                {
                                    c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                }
                                ptr2 = ptr + (((-PC080SN_bgscrolly[0][0] + y0 + dy0 * i2) % 0x200) * width + (-0x10 - PC080SN_bgscrollx[0][0] + PC080SN_ram[0][PC080SN_bgscroll_ram_offset[0][0] - PC080SN_bgscrolly[0][0] + y0 + dy0 * i2] + x0 + dx0 * i1) % 0x200) * 4;
                                //ptr2 = ptr + ((y0 + dy0 * i2) * width + (x0 + dx0 * i1) % 0x200) * 4;
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
        public static void GetHighBg()
        {
            int i1, i2, iOffset, i3, i4, tile_index;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x40;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            int iByte;
            int code, attr, color, group, flags, attributes = 0;
            int pen_data_offset, palette_base;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0;
            byte map;
            for (i3 = 0; i3 < cols; i3++)
            {
                for (i4 = 0; i4 < rows; i4++)
                {
                    tile_index = i4 * cols + i3;
                    code = (Taito.PC080SN_ram[0][Taito.PC080SN_bg_ram_offset[0][0] + 2 * tile_index + 1] & 0x3fff);
                    attr = Taito.PC080SN_ram[0][Taito.PC080SN_bg_ram_offset[0][0] + 2 * tile_index];
                    color = attr & 0x1ff;
                    code = code % Taito.PC080SN_tilemap[0][0].total_elements;
                    pen_data_offset = code * 0x40;
                    palette_base = 0x10 * color;
                    group = 0;
                    flags = (((attr & 0xc000) >> 14) & 3) ^ (attributes & 0x03);
                    if (flags == 0)
                    {
                        x0 = tilewidth * i3;
                        y0 = tileheight * i4;
                        dx0 = 1;
                        dy0 = 1;
                    }
                    else if (flags == 1)
                    {
                        x0 = tilewidth * i3 + tilewidth - 1;
                        y0 = tileheight * i4;
                        dx0 = -1;
                        dy0 = 1;
                    }
                    else if (flags == 2)
                    {
                        x0 = tilewidth * i3;
                        y0 = tileheight * i4 + tileheight - 1;
                        dx0 = 1;
                        dy0 = -1;
                    }
                    else if (flags == 3)
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
                            iOffset = pen_data_offset + i2 * tilewidth + i1;
                            iByte = Taito.gfx1rom[iOffset];
                            map = PC080SN_tilemap[0][0].pen_to_flags[group, iByte];
                            //andmask &= map;
                            //ormask |= map;
                            flagsmapBG[x0 + dx0 * i1, y0 + dy0 * i2] = map;
                        }
                    }
                }
            }
            int xpos, ypos, scrollx = 0, scrolly1 = (-PC080SN_bgscrolly[0][0]) & 0x1ff;
            for (ypos = scrolly1 - height; ypos <= 0x1ff; ypos += height)
            {
                scrollx = PC080SN_bgscrollx[0][0] & 0x1ff;
                for (xpos = -0x10 - PC080SN_bgscrollx[0][0] - width; xpos <= 0x1ff; xpos += width)
                {
                    for (i1 = 0; i1 < 0x200; i1++)
                    {
                        for (i2 = 0; i2 <= 0x200; i2++)
                        {
                            if (xpos + i1 >= 0 && xpos + i1 < 0x140 && ypos + i2 >= 0 && ypos + i2 < 0x100)
                            {
                                priority_bitmapG[xpos + i1, ypos + i2] = flagsmapBG[(PC080SN_tilemap[0][0].rowscroll[(ypos + i2 - scrolly1) & 0x1ff] - scrollx + xpos + i1) & 0x1ff, (ypos + i2 - scrolly1) & 0x1ff];
                            }
                        }
                    }
                }
            }
        }
        public static Bitmap GetFg_opwolf()
        {
            int i1, i2, iOffset, i3, i4, tile_index;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x40;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            int iByte;
            int code, attr, color, group, flags, attributes = 0;
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
                        tile_index = i4 * cols + i3;
                        code = (Taito.PC080SN_ram[0][Taito.PC080SN_bg_ram_offset[0][1] + 2 * tile_index + 1] & 0x3fff);
                        attr = Taito.PC080SN_ram[0][Taito.PC080SN_bg_ram_offset[0][1] + 2 * tile_index];
                        color = attr & 0x1ff;
                        code = code % Taito.PC080SN_tilemap[0][1].total_elements;
                        pen_data_offset = code * 0x40;
                        palette_base = 0x10 * color;
                        group = 0;
                        flags = (((attr & 0xc000) >> 14) & 3) ^ (attributes & 0x03);
                        if (flags == 0)
                        {
                            x0 = tilewidth * i3;
                            y0 = tileheight * i4;
                            dx0 = 1;
                            dy0 = 1;
                        }
                        else if (flags == 1)
                        {
                            x0 = tilewidth * i3 + tilewidth - 1;
                            y0 = tileheight * i4;
                            dx0 = -1;
                            dy0 = 1;
                        }
                        else if (flags == 2)
                        {
                            x0 = tilewidth * i3;
                            y0 = tileheight * i4 + tileheight - 1;
                            dx0 = 1;
                            dy0 = -1;
                        }
                        else if (flags == 3)
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
                                iOffset = pen_data_offset + i2 * tilewidth + i1;
                                iByte = Taito.gfx1rom[iOffset];
                                if (iByte == 0)
                                {
                                    c1 = Color.Transparent;
                                }
                                else
                                {
                                    c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                }
                                ptr2 = ptr + (((-PC080SN_bgscrolly[0][1] + y0 + dy0 * i2) % 0x200) * width + (-0x10 - PC080SN_bgscrollx[0][1] + PC080SN_ram[0][PC080SN_bgscroll_ram_offset[0][1] + y0 + dy0 * i2 - PC080SN_bgscrolly[0][1]] + x0 + dx0 * i1) % 0x200) * 4;
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
        public static void GetHighFg()
        {
            int i1, i2, iOffset, i3, i4, tile_index;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x40;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            int iByte;
            int code, attr, color, group, flags, attributes = 0;
            int pen_data_offset, palette_base;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0;
            byte map;
            for (i3 = 0; i3 < cols; i3++)
            {
                for (i4 = 0; i4 < rows; i4++)
                {
                    tile_index = i4 * cols + i3;
                    code = (Taito.PC080SN_ram[0][Taito.PC080SN_bg_ram_offset[0][1] + 2 * tile_index + 1] & 0x3fff);
                    attr = Taito.PC080SN_ram[0][Taito.PC080SN_bg_ram_offset[0][1] + 2 * tile_index];
                    color = attr & 0x1ff;
                    code = code % Taito.PC080SN_tilemap[0][1].total_elements;
                    pen_data_offset = code * 0x40;
                    palette_base = 0x10 * color;
                    group = 0;
                    flags = (((attr & 0xc000) >> 14) & 3) ^ (attributes & 0x03);
                    if (flags == 0)
                    {
                        x0 = tilewidth * i3;
                        y0 = tileheight * i4;
                        dx0 = 1;
                        dy0 = 1;
                    }
                    else if (flags == 1)
                    {
                        x0 = tilewidth * i3 + tilewidth - 1;
                        y0 = tileheight * i4;
                        dx0 = -1;
                        dy0 = 1;
                    }
                    else if (flags == 2)
                    {
                        x0 = tilewidth * i3;
                        y0 = tileheight * i4 + tileheight - 1;
                        dx0 = 1;
                        dy0 = -1;
                    }
                    else if (flags == 3)
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
                            iOffset = pen_data_offset + i2 * tilewidth + i1;
                            iByte = Taito.gfx1rom[iOffset];
                            map = PC080SN_tilemap[0][1].pen_to_flags[group, iByte];
                            //andmask &= map;
                            //ormask |= map;
                            flagsmapFG[x0 + dx0 * i1, y0 + dy0 * i2] = map;
                        }
                    }
                }
            }
            int xpos, ypos, scrollx = 0, scrolly1 = (-PC080SN_bgscrolly[0][1]) & 0x1ff;
            for (ypos = scrolly1 - height; ypos <= 0x1ff; ypos += height)
            {
                scrollx = PC080SN_bgscrollx[0][1] & 0x1ff;
                for (xpos = -0x10 - PC080SN_bgscrollx[0][1] - width; xpos <= 0x1ff; xpos += width)
                {
                    for (i1 = 0; i1 < 0x200; i1++)
                    {
                        for (i2 = 0; i2 <= 0x200; i2++)
                        {
                            if (xpos + i1 >= 0 && xpos + i1 < 0x140 && ypos + i2 >= 0 && ypos + i2 < 0x100)
                            {
                                priority_bitmapG[xpos + i1, ypos + i2] = flagsmapFG[(PC080SN_tilemap[0][1].rowscroll[(ypos + i2 - scrolly1) & 0x1ff] - scrollx + xpos + i1) & 0x1ff, (ypos + i2 - scrolly1) & 0x1ff];
                            }
                        }
                    }
                }
            }
        }
        public static Bitmap GetSprinte_opwolf()
        {
            Color c1 = new Color();
            Bitmap bm1;
            bm1 = new Bitmap(0x140, 0x100);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                int offs, x, y, i, j;
                int flipx, flipy, offsetx, offsety, xdir, ydir;
                int data, code, color;
                int sprite_colbank = (PC090OJ_sprite_ctrl & 0xf) << 4;
                //for (offs = 0; offs < 0x800 / 2; offs += 4)
                for (offs = 0x7f8 / 2; offs >= 0; offs -= 4)
                {
                    data = PC090OJ_ram_buffered[offs + 0];
                    flipy = (data & 0x8000) >> 15;
                    flipx = (data & 0x4000) >> 14;
                    color = (data & 0x000f) | sprite_colbank;
                    code = PC090OJ_ram_buffered[offs + 2] & 0x1fff;
                    x = PC090OJ_ram_buffered[offs + 3] & 0x1ff;
                    y = PC090OJ_ram_buffered[offs + 1] & 0x1ff;
                    if (x > 0x140)
                    {
                        x -= 0x200;
                    }
                    if (y > 0x140)
                    {
                        y -= 0x200;
                    }
                    if ((PC090OJ_ctrl & 1) == 0)
                    {
                        x = 320 - x - 16;
                        y = 256 - y - 16;
                        flipx = (flipx == 0 ? 1 : 0);
                        flipy = (flipy == 0 ? 1 : 0);
                    }
                    x += PC090OJ_xoffs;
                    y += PC090OJ_yoffs;
                    if (flipx != 0)
                    {
                        offsetx = 0x0f;
                        xdir = -1;
                    }
                    else
                    {
                        offsetx = 0;
                        xdir = 1;
                    }
                    if (flipy != 0)
                    {
                        offsety = 0x0f;
                        ydir = -1;
                    }
                    else
                    {
                        offsety = 0;
                        ydir = 1;
                    }
                    for (i = 0; i < 0x10; i++)
                    {
                        for (j = 0; j < 0x10; j++)
                        {
                            if (x + offsetx + xdir * j >= 0 && x + offsetx + xdir * j < 0x140 && y + offsety + ydir * i >= 0 && y + offsety + ydir * i < 0x100 && priority_bitmapG[x + offsetx + xdir * j,y + offsety + ydir * i] == 0)
                            {
                                ushort c = gfx2rom[code * 0x100 + 0x10 * i + j];
                                if (c != 0)
                                {
                                    c1 = Color.FromArgb((int)Palette.entry_color[color * 0x10 + c]);
                                    ptr2 = ptr + ((y + offsety + ydir * i) * 0x140 + (x + offsetx + xdir * j)) * 4;
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
        public static Bitmap GetAllGDI()
        {
            Bitmap bm1 = new Bitmap(0x200, 0x200), bm2 = null;
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(Color.Transparent);
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
                    bm2 = GetDraw_bublbobl();
                    g.DrawImage(bm2, 0, 0);
                    break;
                case "opwolf":
                case "opwolfa":
                case "opwolfj":
                case "opwolfu":
                case "opwolfb":
                case "opwolfp":
                    if (bBg)
                    {
                        bm2 = GetBg_opwolf();
                        g.DrawImage(bm2, 0, 0);
                        GetHighBg();
                    }
                    if (bFg)
                    {
                        bm2 = GetFg_opwolf();
                        g.DrawImage(bm2, 0, 0);
                        GetHighFg();
                    }
                    if (bSprite)
                    {
                        bm2 = GetSprinte_opwolf();
                        g.DrawImage(bm2, 0, 0);
                    }          
                    break;
            }            
            return bm1;
        }
    }
}