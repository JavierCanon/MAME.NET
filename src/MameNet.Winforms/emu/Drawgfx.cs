using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public class Drawgfx
    {
        private static void setpixelcolorC(int offsety, int offsetx, int n, uint pmask)
        {
            if (((1 << (Tilemap.priority_bitmap[offsety, offsetx] & 0x1f)) & pmask) == 0)
            {
                Video.bitmapbase[Video.curbitmap][offsety * 0x200 + offsetx] = (ushort)n;
            }
            Tilemap.priority_bitmap[offsety, offsetx] = (byte)((Tilemap.priority_bitmap[offsety, offsetx] & 0x7f) | 0x1f);
        }
        private static void setpixelcolorNa(int offsety, int offsetx, int n)
        {
            int i1 = Tilemap.priority_bitmap[offsety, offsetx];
            int i2 = Namcos1.namcos1_pri;
            if (i1 != 0x1f && i1 <= i2)
            {
                Video.bitmapbase[Video.curbitmap][offsety * 0x200 + offsetx] = (ushort)n;
            }
            Tilemap.priority_bitmap[offsety, offsetx] = 0x1f;
        }
        private static void blockmove_4toN_transpen_pri16(int code, int srcmodulo, int leftskip, int topskip, int flipx, int flipy, int dstwidth, int dstheight, int colorbase, int offsety, int offsetx,uint primask)
        {
            int ydir;
            int offset = code * 0x80;
            if (flipy != 0)
            {
                offsety += (dstheight - 1);
                offset += (0x10 - dstheight - topskip) * srcmodulo;
                ydir = -1;
            }
            else
            {
                offset += topskip * srcmodulo;
                ydir = 1;
            }
            if (flipx != 0)
            {
                offsetx += dstwidth - 1;
                offset += (0x10 - dstwidth - leftskip) / 2;
                leftskip = (0x10 - dstwidth - leftskip) & 1;
            }
            else
            {
                offset += leftskip / 2;
                leftskip &= 1;
            }
            srcmodulo -= (dstwidth + leftskip) / 2;
            if (flipx != 0)
            {
                int endoffset;
                while (dstheight != 0)
                {
                    int col;
                    endoffset = offsety * 0x200 + offsetx - dstwidth;
                    if (leftskip != 0)
                    {
                        col = CPS.gfx1rom[offset * 2 + 1];
                        offset++;
                        if (col != 0x0f)
                            setpixelcolorC(offsety, offsetx, colorbase + col,primask);
                        offsetx += (-1);
                    }
                    while (offsety * 0x200 + offsetx > endoffset)
                    {
                        col = CPS.gfx1rom[offset * 2];
                        if (col != 0x0f)
                            setpixelcolorC(offsety, offsetx, colorbase + col, primask);
                        offsetx += (-1);
                        if (offsety * 0x200 + offsetx > endoffset)
                        {
                            col = CPS.gfx1rom[offset * 2 + 1];
                            offset++;
                            if (col != 0x0f)
                                setpixelcolorC(offsety, offsetx, colorbase + col, primask);
                            offsetx += (-1);
                        }
                    }
                    offset += srcmodulo;
                    offsety += ydir;
                    offsetx += dstwidth;
                    dstheight--;
                }
            }
            else
            {
                int endoffset;
                while (dstheight != 0)
                {
                    int col;
                    endoffset = offsety * 0x200 + offsetx + dstwidth;
                    if (leftskip!=0)
                    {
                        col = CPS.gfx1rom[offset * 2 + 1];
                        offset++;
                        if (col != 0x0f)
                            setpixelcolorC(offsety, offsetx, colorbase + col, primask);
                        offsetx++;
                    }
                    while (offsety * 0x200 + offsetx < endoffset)
                    {
                        col = CPS.gfx1rom[offset * 2];
                        if (col != 0x0f)
                            setpixelcolorC(offsety, offsetx, colorbase + col, primask);
                        offsetx++;
                        if (offsety * 0x200 + offsetx < endoffset)
                        {
                            col = CPS.gfx1rom[offset * 2 + 1];
                            offset++;
                            if (col != 0x0f)
                                setpixelcolorC(offsety, offsetx, colorbase + col, primask);
                            offsetx++;
                        }
                    }
                    offset += srcmodulo;
                    offsety += ydir;
                    offsetx += (-dstwidth);
                    dstheight--;
                }
            }
        }
        public static void blockmove_8toN_transpen_pri16(int tx, int ty, int code, int srcwidth, int srcheight,
                int leftskip, int topskip, int flipx, int flipy,
                int dstwidth, int dstheight, int colorbase, int offsety, int offsetx)
        {
            int xdir,ydir;
            int src_offset;
            int iwidth, iheight;
            int col;
            src_offset = code * 0x400 + tx + ty * 0x20;
            if (flipy!=0)
            {
                offsety += dstheight - 1;
                src_offset += (srcheight - dstheight - topskip) * 0x20;
                ydir = -1;
            }
            else
            {
                src_offset += topskip * 0x20;
                ydir = 1;
            }
            if (flipx!=0)
            {
                offsetx += dstwidth - 1;
                src_offset += (srcwidth - dstwidth - leftskip);
                xdir = -1;
            }
            else
            {
                src_offset += leftskip;
                xdir = 1;
            }
            for (iheight = 0; iheight < dstheight; iheight++)
            {
                for (iwidth = 0; iwidth < dstwidth; iwidth++)
                {
                    col = Namcos1.gfx3rom[src_offset + iheight * 0x20 + iwidth];
                    if (col != 0x0f)
                    {
                        setpixelcolorNa(offsety + iheight * ydir, offsetx + iwidth * xdir, colorbase + col);
                    }
                }
            }
        }
        public static void drawgfx_core16(int sizex, int sizey, int tx, int ty, int code, int color, int flipx, int flipy, int sx, int sy, RECT clip)
        {
            int ox;
            int oy;
            int ex;
            int ey;
            ox = sx;
            oy = sy;
            ex = sx + sizex - 1;
            if (sx < 0)
                sx = 0;
            if (sx < clip.min_x)
                sx = clip.min_x;
            if (ex >= 0x200)
                ex = 0x200 - 1;
            if (ex > clip.max_x)
                ex = clip.max_x;
            if (sx > ex)
                return;
            ey = sy + sizey - 1;
            if (sy < 0)
                sy = 0;
            if (sy < clip.min_y)
                sy = clip.min_y;
            if (ey >= 0x200)
                ey = 0x200 - 1;
            if (ey > clip.max_y)
                ey = clip.max_y;
            if (sy > ey)
                return;
            int sw = sizex;
            int sh = sizey;
            int ls = sx - ox;
            int ts = sy - oy;
            int dw = ex - sx + 1;
            int dh = ey - sy + 1;
            int colorbase = 0x10 * color;
            blockmove_8toN_transpen_pri16(tx,ty,code,sw,sh,ls,ts,flipx,flipy,dw,dh,colorbase,sy,sx);
        }
        public static void common_drawgfx_na(int sizex,int sizey,int tx,int ty,int code,int color,int flipx,int flipy,int sx,int sy,RECT cliprect)
        {
	        drawgfx_core16(sizex,sizey,tx,ty,code,color,flipx,flipy,sx,sy,cliprect);
        }
        public static void common_drawgfx_c(int code, int color, int flipx, int flipy, int sx, int sy, uint primask)
        {
            int ox;
            int oy;
            int ex;
            int ey;
            int width, height;

            width = Tilemap.videovisarea.max_x + 1;
            height = Tilemap.videovisarea.max_y + 1;

            /* check bounds */
            ox = sx;
            oy = sy;

            ex = sx + 0x0f;
            if (sx < 0)
                sx = 0;
            if (sx < Tilemap.videovisarea.min_x)
                sx = Tilemap.videovisarea.min_x;
            if (ex >= width)
                ex = width - 1;
            if (ex > Tilemap.videovisarea.max_x)
                ex = Tilemap.videovisarea.max_x;
            if (sx > ex)
                return;

            ey = sy + 0x0f;
            if (sy < 0)
                sy = 0;
            if (sy < Tilemap.videovisarea.min_y)
                sy = Tilemap.videovisarea.min_y;
            if (ey >= height)
                ey = height - 1;
            if (ey > Tilemap.videovisarea.max_y)
                ey = Tilemap.videovisarea.max_y;
            if (sy > ey)
                return;

            int ls = sx - ox;											/* left skip */
            int ts = sy - oy;											/* top skip */
            int dw = ex - sx + 1;										/* dest width */
            int dh = ey - sy + 1;										/* dest height */
            int colorbase = 0x10 * color;
            blockmove_4toN_transpen_pri16(code, 0x08, ls, ts, flipx, flipy, dw, dh, colorbase, sy, sx,primask);
        }
        public static void common_drawgfx_m72(byte[] bb1, int code, int color, int flipx, int flipy, int sx, int sy, RECT clip)
        {
            int ox;
            int oy;
            int ex;
            int ey;
            ox = sx;
            oy = sy;
            ex = sx + 0x10 - 1;
            if (sx < 0)
            {
                sx = 0;
            }
            if (sx < clip.min_x)
            {
                sx = clip.min_x;
            }
            if (ex >= 0x200)
            {
                ex = 0x200 - 1;
            }
            if (ex > clip.max_x)
            {
                ex = clip.max_x;
            }
            if (sx > ex)
            {
                return;
            }
            ey = sy + 0x10 - 1;
            if (sy < 0)
            {
                sy = 0;
            }
            if (sy < clip.min_y)
            {
                sy = clip.min_y;
            }
            if (ey >= 0x11c)
            {
                ey = 0x11c - 1;
            }
            if (ey > clip.max_y)
            {
                ey = clip.max_y;
            }
            if (sy > ey)
            {
                return;
            }
            int sw = 0x10;
            int sh = 0x10;
            int ls = sx - ox;
            int ts = sy - oy;
            int dw = ex - sx + 1;
            int dh = ey - sy + 1;
            int colorbase = 0x10 * color;
            blockmove_8toN_transpen16_m72(bb1,code, sw, sh, 0x10, ls, ts, flipx, flipy, dw, dh, colorbase, sy, sx);
        }
        public static void blockmove_8toN_transpen16_m72(byte[] bb1,int code, int srcwidth, int srcheight,int srcmodulo,
                int leftskip, int topskip, int flipx, int flipy,
                int dstwidth, int dstheight, int colorbase, int offsety, int offsetx)
        {
            int ydir,xdir,col,i,j;
            int srcdata_offset = code * 0x100;
            if (flipy!=0)
            {
                offsety += (dstheight - 1);
                srcdata_offset += (srcheight - dstheight - topskip) * 0x10;
                ydir = -1;
            }
            else
            {
                srcdata_offset += topskip * 0x10;
                ydir = 1;
            }
            if (flipx != 0)
            {
                offsetx += (dstwidth - 1);
                srcdata_offset += (srcwidth - dstwidth - leftskip);
                xdir = -1;
            }
            else
            {
                srcdata_offset += leftskip;
                xdir = 1;
            }
            srcmodulo -= dstwidth;
            for (i = 0; i < dstheight; i++)
            {
                for (j = 0; j < dstwidth; j++)
                {
                    col = bb1[srcdata_offset + (srcmodulo + dstwidth) * i + j];
                    if (col != 0)
                    {
                        Video.bitmapbase[Video.curbitmap][(offsety + ydir * i) * 0x200 + offsetx + xdir * j] = (ushort)(colorbase + col);
                    }
                }
            }
        }
        public static void common_drawgfx_m92(byte[] bb1, int code, int color, int flipx, int flipy, int sx, int sy, RECT clip,uint primask)
        {
            int ox;
            int oy;
            int ex;
            int ey;
            ox = sx;
            oy = sy;
            ex = sx + 0x10 - 1;
            if (sx < 0)
            {
                sx = 0;
            }
            if (sx < clip.min_x)
            {
                sx = clip.min_x;
            }
            if (ex >= 0x200)
            {
                ex = 0x200 - 1;
            }
            if (ex > clip.max_x)
            {
                ex = clip.max_x;
            }
            if (sx > ex)
            {
                return;
            }
            ey = sy + 0x10 - 1;
            if (sy < 0)
            {
                sy = 0;
            }
            if (sy < clip.min_y)
            {
                sy = clip.min_y;
            }
            if (ey >= 0x100)
            {
                ey = 0x100 - 1;
            }
            if (ey > clip.max_y)
            {
                ey = clip.max_y;
            }
            if (sy > ey)
            {
                return;
            }
            int sw = 0x10;
            int sh = 0x10;
            int ls = sx - ox;
            int ts = sy - oy;
            int dw = ex - sx + 1;
            int dh = ey - sy + 1;
            int colorbase = 0x10 * color;
            blockmove_8toN_transpen_pri16_m92(bb1, code, sw, sh, 0x10, ls, ts, flipx, flipy, dw, dh, colorbase, sy, sx,primask);
        }
        public static void blockmove_8toN_transpen_pri16_m92(byte[] bb1, int code, int srcwidth, int srcheight, int srcmodulo,
                int leftskip, int topskip, int flipx, int flipy,
                int dstwidth, int dstheight, int colorbase, int sy, int sx,uint primask)
        {
            int ydir, xdir, col, i, j;
            int offsetx = sx, offsety = sy;
            int srcdata_offset = code * 0x100;
            if (flipy != 0)
            {
                offsety += (dstheight - 1);
                srcdata_offset += (srcheight - dstheight - topskip) * 0x10;
                ydir = -1;
            }
            else
            {
                srcdata_offset += topskip * 0x10;
                ydir = 1;
            }
            if (flipx != 0)
            {
                offsetx += (dstwidth - 1);
                srcdata_offset += (srcwidth - dstwidth - leftskip);
                xdir = -1;
            }
            else
            {
                srcdata_offset += leftskip;
                xdir = 1;
            }
            for (i = 0; i < dstheight; i++)
            {
                for (j = 0; j < dstwidth; j++)
                {
                    col = bb1[srcdata_offset + srcmodulo * i + j];
                    if (col != 0)
                    {
                        if ((1 << (Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] & 0x1f) & primask) == 0)
                        {
                            if ((Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] & 0x80) != 0)
                            {
                                Video.bitmapbase[Video.curbitmap][(offsety + ydir * i) * 0x200 + offsetx + xdir * j] = 0x800;
                            }
                            else
                            {
                                Video.bitmapbase[Video.curbitmap][(offsety + ydir * i) * 0x200 + offsetx + xdir * j] = (ushort)(colorbase + col);
                            }
                            Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] = (byte)((Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] & 0x7f) | 0x1f);
                        }
                    }
                }
            }
        }
    }
}