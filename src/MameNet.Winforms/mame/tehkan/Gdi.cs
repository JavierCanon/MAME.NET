using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace mame
{
    public partial class Tehkan
    {
        public static bool bBg, bFg, bSprite;
        public static void GDIInit()
        {

        }
        public static Bitmap GetBg()
        {
            int i1, i2, iOffset, i3, i4;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 0x8;
            tileheight = tilewidth;
            rows = 0x20;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            int iByte;
            int tile_index, attr, color, flipyx, code, flags;
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
                        attr = Generic.colorram[tile_index];
                        code = Generic.videoram[tile_index] + 0x10 * (attr & 0x70);
                        color = attr & 0x07;
                        flags = (attr & 0x80) != 0 ? Tilemap.TILE_FLIPY : 0;
                        palette_base = 0x80 + 0x10 * color;
                        pen_data_offset = code * 0x40;
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
                                iOffset = pen_data_offset + i2 * 8 + i1;
                                iByte = Tehkan.gfx2rom[iOffset];
                                c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                ptr2 = ptr + ((y0 + dy0 * i2) * width + (x0 + dx0 * i1)) * 4;
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
            int i1, i2, iOffset, i3, i4;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            tilewidth = 0x8;
            tileheight = tilewidth;
            rows = 0x20;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            int iByte;
            int tile_index, attr, color, flipyx, code, flags;
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
                        attr = Tehkan.pbaction_colorram2[tile_index];
                        code = Tehkan.pbaction_videoram2[tile_index] + 0x10 * (attr & 0x30);
                        color = attr & 0x0f;
                        flags = ((attr & 0x40) != 0 ? Tilemap.TILE_FLIPX : 0) | ((attr & 0x80) != 0 ? Tilemap.TILE_FLIPY : 0);
                        palette_base = 0x08 * color;
                        pen_data_offset = code * 0x40;
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
                                iOffset = pen_data_offset + i2 * 8 + i1;
                                iByte = Tehkan.gfx1rom[iOffset];
                                if (iByte == 0)
                                {
                                    c1 = Color.Transparent;
                                }
                                else
                                {
                                    c1 = Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                }
                                ptr2 = ptr + ((y0 + dy0 * i2) * width + (x0 + dx0 * i1)) * 4;
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
        public static Bitmap GetSprite()
        {
            int i, j,offsetx,offsety,xdir,ydir,sx1,code=0,color=0;
            Bitmap bm1;
            Color c1 = new Color();
            bm1 = new Bitmap(0x100, 0x100);
            int offs;
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (offs = 0x80 - 4; offs >= 0; offs -= 4)
                {
                    int sx, sy, flipx, flipy;
                    if (offs > 0 && (Generic.spriteram[offs - 4] & 0x80) != 0)
                    {
                        continue;
                    }
                    sx = Generic.spriteram[offs + 3];
                    if ((Generic.spriteram[offs] & 0x80) != 0)
                    {
                        sy = 225 - Generic.spriteram[offs + 2];
                    }
                    else
                    {
                        sy = 241 - Generic.spriteram[offs + 2];
                    }
                    flipx = Generic.spriteram[offs + 1] & 0x40;
                    flipy = Generic.spriteram[offs + 1] & 0x80;
                    if (Generic.flip_screen_get() != 0)
                    {
                        if ((Generic.spriteram[offs] & 0x80) != 0)
                        {
                            sx = 224 - sx;
                            sy = 225 - sy;
                        }
                        else
                        {
                            sx = 240 - sx;
                            sy = 241 - sy;
                        }
                        flipx = (flipx == 0 ? 1 : 0);
                        flipy = (flipy == 0 ? 1 : 0);
                    }                    
                    if ((Generic.spriteram[offs] & 0x80) != 0)
                    {
                        if (flipx != 0)
                        {
                            offsetx = 0x1f;
                            xdir = -1;
                        }
                        else
                        {
                            offsetx = 0;
                            xdir = 1;
                        }
                        if (flipy != 0)
                        {
                            offsety = 0x1f;
                            ydir = -1;
                        }
                        else
                        {
                            offsety = 0;
                            ydir = 1;
                        }
                        sx1 = sx + (Generic.flip_screen_get() != 0 ? scroll : -scroll);
                        code = Generic.spriteram[offs] % 0x20;
                        color = Generic.spriteram[offs + 1] & 0x0f;
                        for (i = 0; i < 0x20; i++)
                        {
                            for (j = 0; j < 0x20; j++)
                            {
                                if (sx1 + offsetx + xdir * j >= 0 && sx1 + offsetx + xdir * j < 0x100 && sy + offsety + ydir * i >= 0 && sy + offsety + ydir * i < 0x100)
                                {
                                    ushort c = gfx32rom[code * 0x400 + 0x20 * i + j];
                                    if (c != 0)
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[color * 8 + c]);
                                        ptr2 = ptr + ((sy + offsety + ydir * i) * 0x100 + (sx1 + offsetx + xdir * j)) * 4;
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
                        if (flipx != 0)
                        {
                            offsetx = 0x1f;
                            xdir = -1;
                        }
                        else
                        {
                            offsetx = 0;
                            xdir = 1;
                        }
                        if (flipy != 0)
                        {
                            offsety = 0x1f;
                            ydir = -1;
                        }
                        else
                        {
                            offsety = 0;
                            ydir = 1;
                        }
                        sx1 = sx + (Generic.flip_screen_get() != 0 ? scroll : -scroll);
                        code = Generic.spriteram[offs] % 0x80;
                        color = Generic.spriteram[offs + 1] & 0x0f;
                        for (i = 0; i < 0x10; i++)
                        {
                            for (j = 0; j < 0x10; j++)
                            {
                                if (sx1 + offsetx + xdir * j >= 0 && sx1 + offsetx + xdir * j < 0x100 && sy + offsety + ydir * i >= 0 && sy + offsety + ydir * i < 0x100)
                                {
                                    ushort c = gfx3rom[code * 0x100 + 0x10 * i + j];
                                    if (c != 0)
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[color * 8 + c]);
                                        ptr2 = ptr + ((sy + offsety + ydir * i) * 0x100 + (sx1 + offsetx + xdir * j)) * 4;
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
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetAllGDI()
        {
            Bitmap bm1 = new Bitmap(0x100, 0x100), bm2;
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(Color.Transparent);
            if (bBg)
            {
                bm2 = GetBg();
                g.DrawImage(bm2, 0, 0);
            }
            if (bSprite)
            {
                bm2 = GetSprite();
                g.DrawImage(bm2, 0, 0);
            }
            if (bFg)
            {
                bm2 = GetFg();
                g.DrawImage(bm2, 0, 0);
            }
            switch (Machine.sDirection)
            {
                case "":
                    break;
                case "90":
                    bm1.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
            }
            return bm1;
        }
    }
}
