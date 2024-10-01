using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace mame
{
    public partial class Konami68000
    {
        private static string[] sde2 = new string[] { "," }, sde6 = new string[] { "-" };
        public static bool bTile0, bTile1, bTile2, bSprite;
        public delegate Bitmap GetBitmap();
        public static GetBitmap[] GetTilemaps;
        public static GetBitmap GetSprite;
        private static List<int> lSprite;
        public static int min_priority,max_priority;
        public static void GDIInit()
        {
            lSprite = new List<int>();
            GetTilemaps = new GetBitmap[3] { GetTilemap0_K052109, GetTilemap1_K052109, GetTilemap2_K052109 };
            switch (Machine.sName)
            {
                case "cuebrick":
                case "mia":
                case "mia2":
                case "tmnt":
                case "tmntu":
                case "tmntua":
                case "tmntub":
                case "tmht":
                case "tmhta":
                case "tmhtb":
                case "tmntj":
                case "tmnta":
                case "tmht2p":
                case "tmht2pa":
                case "tmnt2pj":
                case "tmnt2po":
                    min_priority = 0;
                    max_priority = 0;
                    GetSprite = GetSprite_K051960;
                    break;
                case "lgtnfght":
                case "lgtnfghta":
                case "lgtnfghtu":
                case "trigon":
                case "tmnt2":
                case "tmnt2a":
                case "tmht22pe":
                case "tmht24pe":
                case "tmnt22pu":
                case "qgakumon":
                case "ssriders":
                case "ssriderseaa":
                case "ssridersebd":
                case "ssridersebc":
                case "ssridersuda":
                case "ssridersuac":
                case "ssridersuab":
                case "ssridersubc":
                case "ssridersadd":
                case "ssridersabd":
                case "ssridersjad":
                case "ssridersjac":
                case "ssridersjbd":
                    GetSprite = GetSprite_K053245;
                    break;
                case "thndrx2":
                case "thndrx2a":
                case "thndrx2j":
                    min_priority = -1;
                    max_priority = -1;
                    GetSprite = GetSprite_K051960;
                    break;
                default:
                    GetSprite = GetNull;
                    break;
            }
        }
        public static Bitmap GetNull()
        {
            Bitmap bm1;
            bm1 = new Bitmap(512, 256);
            return bm1;
        }
        public static Bitmap GetTilemap0_K052109()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int tilewidth, tileheight;
            int bank, priority;
            int code2, color2,flags2;
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
                        iTile = iOffset3;
                        iCode = K052109_ram[K052109_videoram_F_offset + iTile] + 256 * K052109_ram[K052109_videoram2_F_offset + iTile]; ;
                        iColor = K052109_ram[K052109_colorram_F_offset + iTile];
                        bank = K052109_charrombank[(iColor & 0x0c) >> 2];
                        priority = 0;                        
                        iFlag = 0;
                        iColor = (iColor & 0xf3) | ((bank & 0x03) << 2);
                        bank >>= 2;
                        K052109_callback(0, bank, iCode, iColor, iFlag, priority, out code2, out color2,out flags2);
                        iCode = code2;
                        iColor = color2;
                        iFlag = flags2;
                        iCode1 = iCode % K052109_tilemap[0].total_elements;
                        pen_data_offset = iCode1 * 0x40;
                        palette_base = 0x10 * iColor;
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
                                iOffset = pen_data_offset + i2 * 0x08 + i1;
                                iByte = gfx12rom[iOffset];
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
        public static Bitmap GetTilemap1_K052109()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;            
            int bank, priority;
            int code2, color2,flags2;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x20;
            cols = 0x40;
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
                        iTile = iOffset3;
                        iCode = K052109_ram[K052109_videoram_A_offset + iTile] + 256 * K052109_ram[K052109_videoram2_A_offset + iTile]; ;
                        iColor = K052109_ram[K052109_colorram_A_offset + iTile];
                        bank = K052109_charrombank[(iColor & 0x0c) >> 2];
                        priority = 0;
                        iFlag = 0;
                        iColor = (iColor & 0xf3) | ((bank & 0x03) << 2);
                        bank >>= 2;
                        K052109_callback(1, bank, iCode, iColor, iFlag, priority, out code2, out color2, out flags2);
                        iCode = code2;
                        iColor = color2;
                        iFlag = flags2;
                        iCode1 = iCode % K052109_tilemap[1].total_elements;
                        pen_data_offset = iCode1 * 0x40;
                        palette_base = 0x10 * iColor;
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
                                iOffset = pen_data_offset + i2 * 0x08 + i1;
                                iByte = gfx12rom[iOffset];
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
        public static Bitmap GetTilemap2_K052109()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int bank, priority;
            int code2, color2,flags2;
            int tilewidth, tileheight;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x20;
            cols = 0x40;
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
                        iTile = iOffset3;
                        iCode = K052109_ram[K052109_videoram_B_offset + iTile] + 256 * K052109_ram[K052109_videoram2_B_offset + iTile]; ;
                        iColor = K052109_ram[K052109_colorram_B_offset + iTile];
                        bank = K052109_charrombank[(iColor & 0x0c) >> 2];
                        priority = 0;
                        iFlag = 0;
                        iColor = (iColor & 0xf3) | ((bank & 0x03) << 2);
                        bank >>= 2;
                        K052109_callback(2, bank, iCode, iColor, iFlag, priority, out code2, out color2,out flags2);
                        iCode = code2;
                        iColor = color2;
                        iFlag = flags2;
                        iCode1 = iCode % K052109_tilemap[2].total_elements;
                        pen_data_offset = iCode1 * 0x40;
                        palette_base = 0x10 * iColor;
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
                                iOffset = pen_data_offset + i2 * 0x08 + i1;
                                iByte = gfx12rom[iOffset];
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
        public static Bitmap GetSprite_K053245()
        {
            Color c1 = new Color();
            byte col;
            Bitmap bm1;
            bm1 = new Bitmap(512, 256);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int offs, pri_code, i;
            int[] sortedlist = new int[128];
            int flipscreenX, flipscreenY, spriteoffsX, spriteoffsY;
            flipscreenX = K053244_regs[0][5] & 0x01;
            flipscreenY = K053244_regs[0][5] & 0x02;
            spriteoffsX = (K053244_regs[0][0] << 8) | K053244_regs[0][1];
            spriteoffsY = (K053244_regs[0][2] << 8) | K053244_regs[0][3];
            for (offs = 0; offs < 128; offs++)
            {
                sortedlist[offs] = -1;
            }
            i = K053245_ramsize[0] / 2;
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                int x1, y1;
                for (offs = 0; offs < i; offs += 8)
                {
                    pri_code = K053245_buffer[0][offs];
                    if ((pri_code & 0x8000) != 0)
                    {
                        pri_code &= 0x007f;
                        if (offs != 0 && pri_code == K05324x_z_rejection)
                        {
                            continue;
                        }
                        if (sortedlist[pri_code] == -1)
                        {
                            sortedlist[pri_code] = offs;
                        }
                    }
                }
                for(pri_code=0;pri_code<128;pri_code++)
                //for(pri_code=127;pri_code>=0;pri_code--)
                {
                    int ox, oy, color, color2, code,code2, size, w, h, x, y, flipx, flipy, mirrorx, mirrory, shadow, zoomx, zoomy, pri, pri2;
                    offs = sortedlist[pri_code];
                    if (offs == -1)
                    {
                        continue;
                    }
                    code = K053245_buffer[0][offs + 1];
                    code = ((code & 0xffe1) + ((code & 0x0010) >> 2) + ((code & 0x0008) << 1) + ((code & 0x0004) >> 1) + ((code & 0x0002) << 2));
                    color = K053245_buffer[0][offs + 6] & 0x00ff;
                    pri = 0;
                    K053245_callback(code, color,out code2, out color2, out pri2);
                    size = (K053245_buffer[0][offs] & 0x0f00) >> 8;
                    w = 1 << (size & 0x03);
                    h = 1 << ((size >> 2) & 0x03);
                    zoomy = K053245_buffer[0][offs + 4];
                    if (zoomy > 0x2000)
                    {
                        continue;
                    }
                    if (zoomy != 0)
                    {
                        zoomy = (0x400000 + zoomy / 2) / zoomy;
                    }
                    else
                    {
                        zoomy = 2 * 0x400000;
                    }
                    if ((K053245_buffer[0][offs] & 0x4000) == 0)
                    {
                        zoomx = K053245_buffer[0][offs + 5];
                        if (zoomx > 0x2000)
                        {
                            continue;
                        }
                        if (zoomx != 0)
                        {
                            zoomx = (0x400000 + zoomx / 2) / zoomx;
                        }
                        else
                        {
                            zoomx = 2 * 0x400000;
                        }
                    }
                    else
                    {
                        zoomx = zoomy;
                    }
                    ox = K053245_buffer[0][offs + 3] + spriteoffsX;
                    oy = K053245_buffer[0][offs + 2];
                    ox += K053245_dx[0];
                    oy += K053245_dy[0];
                    flipx = K053245_buffer[0][offs] & 0x1000;
                    flipy = K053245_buffer[0][offs] & 0x2000;
                    mirrorx = K053245_buffer[0][offs + 6] & 0x0100;
                    if (mirrorx != 0)
                    {
                        flipx = 0;
                    }
                    mirrory = K053245_buffer[0][offs + 6] & 0x0200;
                    shadow = K053245_buffer[0][offs + 6] & 0x0080;
                    if (flipscreenX != 0)
                    {
                        ox = 512 - ox;
                        if (mirrorx == 0)
                        {
                            flipx = (flipx == 0) ? 1 : 0;
                        }
                    }
                    if (flipscreenY != 0)
                    {
                        oy = -oy;
                        if (mirrory == 0)
                        {
                            flipy = (flipy == 0) ? 1 : 0;
                        }
                    }
                    ox = (ox + 0x5d) & 0x3ff;
                    if (ox >= 768)
                    {
                        ox -= 1024;
                    }
                    oy = (-(oy + spriteoffsY + 0x07)) & 0x3ff;
                    if (oy >= 640)
                    {
                        oy -= 1024;
                    }
                    ox -= (zoomx * w) >> 13;
                    oy -= (zoomy * h) >> 13;
                    for (y = 0; y < h; y++)
                    {
                        int sx, sy, zw, zh;
                        sy = oy + ((zoomy * y + (1 << 11)) >> 12);
                        zh = (oy + ((zoomy * (y + 1) + (1 << 11)) >> 12)) - sy;
                        for (x = 0; x < w; x++)
                        {
                            int c, fx, fy;
                            sx = ox + ((zoomx * x + (1 << 11)) >> 12);
                            zw = (ox + ((zoomx * (x + 1) + (1 << 11)) >> 12)) - sx;
                            c = code2;
                            if (mirrorx != 0)
                            {
                                if ((flipx == 0) ^ (2 * x < w))
                                {
                                    c += (w - x - 1);
                                    fx = 1;
                                }
                                else
                                {
                                    c += x;
                                    fx = 0;
                                }
                            }
                            else
                            {
                                if (flipx != 0)
                                {
                                    c += w - 1 - x;
                                }
                                else
                                {
                                    c += x;
                                }
                                fx = flipx;
                            }
                            if (mirrory != 0)
                            {
                                if ((flipy == 0) ^ (2 * y >= h))
                                {
                                    c += 8 * (h - y - 1);
                                    fy = 1;
                                }
                                else
                                {
                                    c += 8 * y;
                                    fy = 0;
                                }
                            }
                            else
                            {
                                if (flipy != 0)
                                {
                                    c += 8 * (h - 1 - y);
                                }
                                else
                                {
                                    c += 8 * y;
                                }
                                fy = flipy;
                            }
                            c = (c & 0x3f) | (code2 & ~0x3f);
                            if (lSprite.IndexOf(c) < 0)
                            {
                                continue;
                            }
                            if (zoomx == 0x10000 && zoomy == 0x10000)
                            {
                                //common_drawgfx_konami68000(gfx22rom, c, color2, fx, fy, sx, sy, cliprect, (uint)(pri2 | (1 << 31)));
                                int xdir, ydir, offx, offy;
                                if (fy != 0)
                                {
                                    ydir = -1;
                                    offy = 0x0f;
                                }
                                else
                                {
                                    ydir = 1;
                                    offy = 0;
                                }
                                if (fx != 0)
                                {
                                    xdir = -1;
                                    offx = 0x0f;
                                }
                                else
                                {
                                    xdir = 1;
                                    offx = 0;
                                }
                                for (y1 = 0; y1 < 0x10; y1++)
                                {
                                    for (x1 = 0; x1 < 0x10; x1++)
                                    {
                                        col = gfx22rom[c * 0x100 + 0x10 * y1 + x1];
                                        if (col == 0)
                                        {
                                            c1 = Color.Transparent;
                                        }
                                        else
                                        {
                                            c1 = Color.FromArgb((int)Palette.entry_color[color2 * 0x10 + col]);
                                            ptr2 = ptr + ((sy + offy + y1 * ydir) * 0x200 + (sx + offx + x1 * xdir)) * 4;
                                            *ptr2 = c1.B;
                                            *(ptr2 + 1) = c1.G;
                                            *(ptr2 + 2) = c1.R;
                                            *(ptr2 + 3) = c1.A;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                int scalex, scaley, x2, y2;
                                scalex = (zw << 16) / 16;
                                scaley = (zh << 16) / 16;
                                int colorbase = 0x10 * (color2 % 0x80);
                                int source_baseoffset = (c % sprite_totel_element) * 0x100;
                                int sprite_screen_height = (scaley * 0x10 + 0x8000) >> 16;
                                int sprite_screen_width = (scalex * 0x10 + 0x8000) >> 16;
                                int countx, county, srcoffset, dstoffset;
                                if (sprite_screen_width != 0 && sprite_screen_height != 0)
                                {
                                    int dx = (0x10 << 16) / sprite_screen_width;
                                    int dy = (0x10 << 16) / sprite_screen_height;
                                    int ex = sx + sprite_screen_width;
                                    int ey = sy + sprite_screen_height;
                                    int x_index_base;
                                    int y_index;
                                    if (flipx != 0)
                                    {
                                        x_index_base = (sprite_screen_width - 1) * dx;
                                        dx = -dx;
                                    }
                                    else
                                    {
                                        x_index_base = 0;
                                    }
                                    if (flipy != 0)
                                    {
                                        y_index = (sprite_screen_height - 1) * dy;
                                        dy = -dy;
                                    }
                                    else
                                    {
                                        y_index = 0;
                                    }
                                    countx = ex - sx;
                                    county = ey - sy;
                                    for (y2 = 0; y2 < county; y2++)
                                    {
                                        for (x2 = 0; x2 < countx; x2++)
                                        {
                                            srcoffset = ((y_index + dy * y2) >> 16) * 0x10 + ((x_index_base + dx * x2) >> 16);
                                            dstoffset = (sy + y2) * 0x200 + sx + x2;
                                            col = gfx22rom[source_baseoffset + srcoffset];
                                            if (col == 0)
                                            {
                                                c1 = Color.Transparent;
                                            }
                                            else
                                            {
                                                c1 = Color.FromArgb((int)Palette.entry_color[color2 * 0x10 + col]);
                                                if (sy + y2 >= 0 && sy + y2 < 0x100 && sx + x2 >= 0 && sx + x2 < 0x200)
                                                {
                                                    ptr2 = ptr + ((sy + y2) * 0x200 + (sx + x2)) * 4;
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
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetSprite_K051960()
        {
            Color c1 = new Color();
            byte col;
            Bitmap bm1;
            bm1 = new Bitmap(512, 256);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int offs, pri_code;
            int[] sortedlist = new int[128];
            int[] xoffset = new int[8] { 0, 1, 4, 5, 16, 17, 20, 21 };
            int[] yoffset = new int[8] { 0, 2, 8, 10, 32, 34, 40, 42 };
            int[] width = new int[8] { 1, 2, 1, 2, 4, 2, 4, 8 };
            int[] height = new int[8] { 1, 1, 2, 2, 2, 4, 4, 8 };
            for (offs = 0; offs < 128; offs++)
            {
                sortedlist[offs] = -1;
            }
            for (offs = 0; offs < 0x400; offs += 8)
            {
                if ((K051960_ram[offs] & 0x80) != 0)
                {
                    if (max_priority == -1)
                    {
                        sortedlist[(K051960_ram[offs] & 0x7f) ^ 0x7f] = offs;
                    }
                    else
                    {
                        sortedlist[K051960_ram[offs] & 0x7f] = offs;
                    }
                }
            }
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                int x1, y1,fx,fy;
                for (pri_code = 127; pri_code >=0; pri_code--)
                {
                    int ox, oy, code, code2, color, color2, pri, pri2, shadow, shadow2, size, w, h, x, y, flipx, flipy, zoomx, zoomy;
                    offs = sortedlist[pri_code];
                    if (offs == -1)
                    {
                        continue;
                    }
                    code = K051960_ram[offs + 2] + ((K051960_ram[offs + 1] & 0x1f) << 8);
                    color = K051960_ram[offs + 3] & 0xff;
                    pri = 0;
                    shadow = color & 0x80;
                    K051960_callback(code, color, pri, shadow, out code2, out color2, out pri2);
                    code = code2;
                    color = color2;
                    pri = pri2;
                    if (max_priority != -1)
                    {
                        if (pri < min_priority || pri > max_priority)
                        {
                            continue;
                        }
                    }
                    size = (K051960_ram[offs + 1] & 0xe0) >> 5;
                    w = width[size];
                    h = height[size];
                    if (w >= 2) code &= ~0x01;
                    if (h >= 2) code &= ~0x02;
                    if (w >= 4) code &= ~0x04;
                    if (h >= 4) code &= ~0x08;
                    if (w >= 8) code &= ~0x10;
                    if (h >= 8) code &= ~0x20;
                    ox = (256 * K051960_ram[offs + 6] + K051960_ram[offs + 7]) & 0x01ff;
                    oy = 256 - ((256 * K051960_ram[offs + 4] + K051960_ram[offs + 5]) & 0x01ff);
                    ox += K051960_dx;
                    oy += K051960_dy;
                    flipx = K051960_ram[offs + 6] & 0x02;
                    flipy = K051960_ram[offs + 4] & 0x02;
                    zoomx = (K051960_ram[offs + 6] & 0xfc) >> 2;
                    zoomy = (K051960_ram[offs + 4] & 0xfc) >> 2;
                    zoomx = 0x10000 / 128 * (128 - zoomx);
                    zoomy = 0x10000 / 128 * (128 - zoomy);                    
                    if (K051960_spriteflip != 0)
                    {
                        ox = 512 - (zoomx * w >> 12) - ox;
                        oy = 256 - (zoomy * h >> 12) - oy;
                        flipx = (flipx == 0) ? 1 : 0;
                        flipy = (flipy == 0) ? 1 : 0;
                    }
                    fx = flipx;
                    fy = flipy;
                    if (zoomx == 0x10000 && zoomy == 0x10000)
                    {
                        int sx, sy;
                        for (y = 0; y < h; y++)
                        {
                            sy = oy + 16 * y;
                            for (x = 0; x < w; x++)
                            {
                                int c = code;
                                if (lSprite.IndexOf(c) < 0)
                                {
                                    continue;
                                }
                                sx = ox + 16 * x;
                                if (flipx != 0)
                                {
                                    c += xoffset[(w - 1 - x)];
                                }
                                else
                                {
                                    c += xoffset[x];
                                }
                                if (flipy != 0)
                                {
                                    c += yoffset[(h - 1 - y)];
                                }
                                else
                                {
                                    c += yoffset[y];
                                }
                                if (max_priority == -1)
                                {
                                    int xdir, ydir, offx, offy;
                                    if (fy != 0)
                                    {
                                        ydir = -1;
                                        offy = 0x0f;
                                    }
                                    else
                                    {
                                        ydir = 1;
                                        offy = 0;
                                    }
                                    if (fx != 0)
                                    {
                                        xdir = -1;
                                        offx = 0x0f;
                                    }
                                    else
                                    {
                                        xdir = 1;
                                        offx = 0;
                                    }
                                    for (y1 = 0; y1 < 0x10; y1++)
                                    {
                                        for (x1 = 0; x1 < 0x10; x1++)
                                        {
                                            col = gfx22rom[c * 0x100 + 0x10 * y1 + x1];
                                            if (col == 0)
                                            {
                                                c1 = Color.Transparent;
                                            }
                                            else
                                            {
                                                c1 = Color.FromArgb((int)Palette.entry_color[color * 0x10 + col]);
                                                ptr2 = ptr + ((sy + offy + y1 * ydir) * 0x200 + (sx + offx + x1 * xdir)) * 4;
                                                *ptr2 = c1.B;
                                                *(ptr2 + 1) = c1.G;
                                                *(ptr2 + 2) = c1.R;
                                                *(ptr2 + 3) = c1.A;
                                            }
                                        }
                                    }
                                    /*pdrawgfx(bitmap, K051960_gfx,
                                            c,
                                            color,
                                            flipx, flipy,
                                            sx & 0x1ff, sy,
                                            cliprect, shadow ? TRANSPARENCY_PEN_TABLE : TRANSPARENCY_PEN, 0, pri);*/
                                }
                                else
                                {
                                    int i11 = 1;
                                    /*drawgfx(bitmap, K051960_gfx,
                                            c,
                                            color,
                                            flipx, flipy,
                                            sx & 0x1ff, sy,
                                            cliprect, shadow ? TRANSPARENCY_PEN_TABLE : TRANSPARENCY_PEN, 0);*/
                                }
                            }
                        }
                    }
                    else
                    {
                        int sx, sy, zw, zh;
                        for (y = 0; y < h; y++)
                        {
                            sy = oy + ((zoomy * y + (1 << 11)) >> 12);
                            zh = (oy + ((zoomy * (y + 1) + (1 << 11)) >> 12)) - sy;
                            for (x = 0; x < w; x++)
                            {
                                int c = code;
                                sx = ox + ((zoomx * x + (1 << 11)) >> 12);
                                zw = (ox + ((zoomx * (x + 1) + (1 << 11)) >> 12)) - sx;
                                if (flipx != 0)
                                {
                                    c += xoffset[(w - 1 - x)];
                                }
                                else
                                {
                                    c += xoffset[x];
                                }
                                if (flipy != 0)
                                {
                                    c += yoffset[(h - 1 - y)];
                                }
                                else
                                {
                                    c += yoffset[y];
                                }
                                if (max_priority == -1)
                                {
                                    int i11 = 1;
                                    /*pdrawgfxzoom(bitmap,K051960_gfx,
                                            c,
                                            color,
                                            flipx,flipy,
                                            sx & 0x1ff,sy,
                                            cliprect,shadow ? TRANSPARENCY_PEN_TABLE : TRANSPARENCY_PEN,0,
                                            (zw << 16) / 16,(zh << 16) / 16,pri);*/
                                }
                                else
                                {
                                    int i11 = 1;
                                    /*drawgfxzoom(bitmap, K051960_gfx,
                                            c,
                                            color,
                                            flipx, flipy,
                                            sx & 0x1ff, sy,
                                            cliprect, shadow ? TRANSPARENCY_PEN_TABLE : TRANSPARENCY_PEN, 0,
                                            (zw << 16) / 16, (zh << 16) / 16);*/
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetFlag1()
        {
            int i1, i2, i3, i4, i5, i6, iOffset, iTile, iCode, iColor, iByte;
            int pen_data_offset, palette_base;
            Color c1 = new Color();
            Bitmap bm1 = new Bitmap(0x200, 0x100), bm2;
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(Color.Transparent);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                int x, y;
                for (i3 = 0; i3 < 0x20; i3++)
                {
                    for (i4 = 0; i4 < 0x40; i4++)
                    {
                        iCode = K052109_tilemap[1].tileflags[i3, i4];
                        for (i1 = 0; i1 < 8; i1++)
                        {
                            for (i2 = 0; i2 < 8; i2++)
                            {
                                iByte = iCode;
                                if (iByte == 0)
                                {
                                    c1 = Color.Transparent;
                                }
                                else
                                {
                                    c1 = Color.Black;// Color.FromArgb((int)Palette.entry_color[palette_base + iByte]);
                                }
                                ptr2 = ptr + ((8 * i3 + i2) * 0x200 + 8 * i4 + i1) * 4;
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
        public static Bitmap GetTile()
        {
            int i1,i2,i3,i4,i5,i6,iOffset,iTile,iCode,iColor,iByte;
            int pen_data_offset, palette_base;
            Color c1 = new Color();
            Bitmap bm1 = new Bitmap(0x200, 0x100), bm2;
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(Color.Transparent);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                int x, y;
                for (i3 = 0; i3 < 0x10; i3++)
                {
                    for (i4 = 0; i4 < 0x10; i4++)
                    {
                        iCode = 0x5300+ i4 * 0x10 + i3;
                        iColor = Konami68000.K052109_ram[Konami68000.K052109_colorram_B_offset + 0];
                        pen_data_offset = iCode * 0x40;
                        palette_base = 0x10 * iColor;
                        for (i1 = 0; i1 < 8; i1++)
                        {
                            for (i2 = 0; i2 < 8; i2++)
                            {
                                iOffset = pen_data_offset + i2 * 0x08 + i1;
                                iByte = gfx12rom[iOffset];
                                if (iByte == 0)
                                {
                                    c1 = Color.Transparent;
                                }
                                else
                                {
                                    c1 = Color.Black;
                                }
                                ptr2 = ptr + ((8 * i4 + i2) * 0x200 + 8 * i3 + i1) * 4;
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
        public static Bitmap GetAllGDI()
        {
            string[] ss1, ss2, ss3;
            int i1, i2, i3, i4, n1, n2;
            lSprite.Clear();
            ss1 = Machine.FORM.konami68000form.tbSprite.Text.Split(sde2, StringSplitOptions.RemoveEmptyEntries);
            n1 = ss1.Length;
            for (i1 = 0; i1 < n1; i1++)
            {
                ss2 = ss1[i1].Split(sde6, StringSplitOptions.RemoveEmptyEntries);
                n2 = ss2.Length;
                if (n2 == 1)
                {
                    i3 = Convert.ToInt32(ss2[0], 16);
                    i4 = i3;
                }
                else
                {
                    i3 = Convert.ToInt32(ss2[0], 16);
                    i4 = Convert.ToInt32(ss2[1], 16);
                }
                for (i2 = i3; i2 <= i4; i2++)
                {
                    lSprite.Add(i2);
                }
            }
            ss3 = Machine.FORM.neogeoform.tbPoint.Text.Split(sde2, StringSplitOptions.RemoveEmptyEntries);
            Bitmap bm1 = new Bitmap(0x200, 0x100), bm2;
            Graphics g = Graphics.FromImage(bm1);
            Color c1 = Color.FromArgb((int)Palette.entry_color[16 * bg_colorbase]);
            g.Clear(c1);
            if (bTile0)
            {
                bm2 = GetTilemaps[sorted_layer[0]]();
                short scrollx, scrolly;
                scrollx = (short)(-K052109_tilemap[sorted_layer[0]].rowscroll[0]);
                if (scrollx < 0)
                {
                    scrollx = (short)(0x200 - (-scrollx) % 0x200);
                }
                else
                {
                    scrollx %= 0x200;
                }
                scrolly = (short)(-K052109_tilemap[sorted_layer[0]].colscroll[0]);
                if (scrolly < 0)
                {
                    scrolly = (short)(0x100 - (-scrolly) % 0x100);
                }
                else
                {
                    scrolly %= 0x100;
                }
                g.DrawImage(bm2, scrollx - 0x200, scrolly);
                g.DrawImage(bm2, scrollx, scrolly);
            }
            if (bTile1)
            {
                bm2 = GetTilemaps[sorted_layer[1]]();
                short scrollx, scrolly;
                scrollx = (short)(-K052109_tilemap[sorted_layer[1]].rowscroll[0]);
                if (scrollx < 0)
                {
                    scrollx = (short)(0x200 - (-scrollx) % 0x200);
                }
                else
                {
                    scrollx %= 0x200;
                }
                scrolly = (short)(-K052109_tilemap[sorted_layer[1]].colscroll[0]);
                if (scrolly < 0)
                {
                    scrolly = (short)(0x100 - (-scrolly) % 0x100);
                }
                else
                {
                    scrolly %= 0x100;
                }
                g.DrawImage(bm2, scrollx - 0x200, scrolly);
                g.DrawImage(bm2, scrollx, scrolly);
            }
            if (bTile2)
            {
                bm2 = GetTilemaps[sorted_layer[2]]();
                short scrollx, scrolly;
                scrollx = (short)(-K052109_tilemap[sorted_layer[2]].rowscroll[0]);
                if (scrollx < 0)
                {
                    scrollx = (short)(0x200 - (-scrollx) % 0x200);
                }
                else
                {
                    scrollx %= 0x200;
                }
                scrolly = (short)(-K052109_tilemap[sorted_layer[2]].colscroll[0]);
                if (scrolly < 0)
                {
                    scrolly = (short)(0x100 - (-scrolly) % 0x100);
                }
                else
                {
                    scrolly %= 0x100;
                }
                g.DrawImage(bm2, scrollx - 0x200, scrolly);
                g.DrawImage(bm2, scrollx, scrolly);
            }
            if (bSprite)
            {
                bm2 = GetSprite();
                g.DrawImage(bm2, 0, 0);
            }
            return bm1;
        }
    }
}
