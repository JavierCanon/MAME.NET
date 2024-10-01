using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace mame
{
    public partial class Dataeast
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
                        code = Generic.videoram[tile_index * 2 + 1] + ((Generic.videoram[tile_index * 2] & 0x0f) << 8);
                        color = Generic.videoram[tile_index * 2] >> 4;
                        flags = 0;
                        palette_base = 0x100 + 0x10 * color;
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
                                iByte = Dataeast.gfx1rom[iOffset];
                                if (palette_base + iByte < 0x200)
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
            int i, j, offsetx, offsety, xdir, ydir, sx1, code = 0, color = 0;
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
                for (offs = 0; offs < 0x200; offs += 4)
                {
                    if (Generic.spriteram[offs] != 0xf8)
                    {
                        int sx, sy, flipx, flipy;
                        sx = 240 - Generic.spriteram[offs + 2];
                        sy = 240 - Generic.spriteram[offs];
                        flipx = Generic.spriteram[offs + 1] & 0x04;
                        flipy = Generic.spriteram[offs + 1] & 0x02;
                        if (Generic.flip_screen_get() != 0)
                        {
                            sx = 240 - sx;
                            sy = 240 - sy;
                            if (flipx != 0)
                            {
                                flipx = 0;
                            }
                            else
                            {
                                flipx = 1;
                            }
                            if (flipy != 0)
                            {
                                flipy = 0;
                            }
                            else
                            {
                                flipy = 1;
                            }
                        }
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
                        sx1 = sx;
                        code = Generic.spriteram[offs + 3] + ((Generic.spriteram[offs + 1] & 1) << 8);
                        color = (Generic.spriteram[offs + 1] & 0x70) >> 4;
                        for (i = 0; i < 0x10; i++)
                        {
                            for (j = 0; j < 0x10; j++)
                            {
                                if (sx1 + offsetx + xdir * j >= 0 && sx1 + offsetx + xdir * j < 0x100 && sy + offsety + ydir * i >= 0 && sy + offsety + ydir * i < 0x100)
                                {
                                    ushort c = gfx2rom[code * 0x100 + 0x10 * i + j];
                                    if (c != 0)
                                    {
                                        c1 = Color.FromArgb((int)Palette.entry_color[color * 4 + c]);
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
