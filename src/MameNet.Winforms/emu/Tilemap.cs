﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    enum trans_t
    {
        WHOLLY_TRANSPARENT,
        WHOLLY_OPAQUE,
        MASKED
    }
    public struct RECT
    {
        public int min_x;
        public int min_y;
        public int max_x;
        public int max_y;
    }
    public partial class Tmap
    {
        public int laynum;
        public int rows;
        public int cols;
        public int tilewidth;
        public int tileheight;
        public int width;
        public int height;
        public int videoram_offset;
        public bool enable;
        public byte attributes;
        public bool all_tiles_dirty;
        public int palette_offset;
        public byte priority;
        public int scrollrows;
        public int scrollcols;
        public int[] rowscroll;
        public int[] colscroll;
        public int dx;
        public int dx_flipped;
        public int dy;
        public int dy_flipped;
        public ushort[] pixmap;
        public byte[,] flagsmap;
        public byte[,] tileflags;
        public byte[,] pen_to_flags;
        public byte[] pen_data;
        public int mask, value;
        public int total_elements;
        public Action<int, int> tile_update3;
        public Action<RECT, int, int> tilemap_draw_instance3;
        public Action<RECT, int, int> tilemap_draw_instance4;                
        private int effective_rowscroll(int index)
        {
            int value;
            if ((attributes & Tilemap.TILEMAP_FLIPY)!=0)
                index = scrollrows - 1 - index;
            if ((attributes & Tilemap.TILEMAP_FLIPX)==0)
                value = dx - rowscroll[index];
            else
                value = Tilemap.screen_width - width - (dx_flipped - rowscroll[index]);
            if (value < 0)
                value = width - (-value) % width;
            else
                value %= width;
            return value;
        }
        private int effective_colscroll(int index)
        {
            int value;
            if ((attributes & Tilemap.TILEMAP_FLIPX)!=0)
                index = scrollcols - 1 - index;
            if ((attributes & Tilemap.TILEMAP_FLIPY)==0)
                value = dy - colscroll[index];
            else
                value = Tilemap.screen_height - height - (dy_flipped - colscroll[index]);
            if (value < 0)
                value = height - (-value) % height;
            else
                value %= height;
            return value;
        }
        public void tilemap_set_palette_offset(int offset)
        {
            palette_offset = offset;
        }
        /*public static void tilemap_set_flip(byte attributes)
        {
            foreach(Tmap t1 in Tmap.ttmap)
            {
                if (t1.attributes != attributes)
                {
                    t1.attributes = attributes;
                }
            }
        }*/
        public void get_row_col(int index, out int row, out int col)
        {
            if ((attributes & Tilemap.TILEMAP_FLIPX) != 0)
            {
                col = cols - 1 - index % cols;
            }
            else
            {
                col = index % cols;
            }
            if ((attributes & Tilemap.TILEMAP_FLIPY) != 0)
            {
                row = rows - 1 - index / cols;
            }
            else
            {
                row = index / cols;
            }
        }
        public void tilemap_mark_tile_dirty(int row, int col)
        {
            tileflags[row, col] = Tilemap.TILE_FLAG_DIRTY;
        }
        public void tilemap_set_scroll_rows(int scroll_rows)
        {
            scrollrows = scroll_rows;
        }
        public void tilemap_set_scrolldx(int _dx,int _dx2)
        {
            dx = _dx;
            dx_flipped = _dx2;
        }
        public void tilemap_set_scrolldy(int _dy,int _dy2)
        {
            dy = _dy;
            dy_flipped = _dy2;
        }
        public void tilemap_set_scrollx(int which, int value)
        {
            if (which < scrollrows)
                rowscroll[which] = value;
        }
        public void tilemap_set_scrolly(int which, int value)
        {
            if (which < scrollcols)
                colscroll[which] = value;
        }
        private RECT sect_rect(RECT dst, RECT src)
        {
            RECT dst2 = dst;
            if (src.min_x > dst.min_x) dst2.min_x = src.min_x;
            if (src.max_x < dst.max_x) dst2.max_x = src.max_x;
            if (src.min_y > dst.min_y) dst2.min_y = src.min_y;
            if (src.max_y < dst.max_y) dst2.max_y = src.max_y;
            return dst2;
        }
        public void tilemap_draw_primask(RECT cliprect, int flags, byte _priority)
        {
            int xpos, ypos;
            if (!enable)
                return;
            mask = 0x0f | flags;
            value = flags;
            priority = _priority;
            if (all_tiles_dirty)
            {
                Array.Copy(Tilemap.bbFF, tileflags, cols * rows);
                all_tiles_dirty = false;
            }
            if (scrollrows == 1 && scrollcols == 1)
            {
                int scrollx = effective_rowscroll(0);
                int scrolly = effective_colscroll(0);
                for (ypos = scrolly - height; ypos <= cliprect.max_y; ypos += height)
                    for (xpos = scrollx - width; xpos <= cliprect.max_x; xpos += width)
                    {
                        tilemap_draw_instance3(cliprect, xpos, ypos);
                    }
            }
            else if (scrollcols == 1)
            {
                RECT rect = cliprect;
                int rowheight = height / scrollrows;
                int scrolly = effective_colscroll(0);
                int currow, nextrow;
                for (ypos = scrolly - height; ypos <= cliprect.max_y; ypos += height)
                {
                    int firstrow = Math.Max((cliprect.min_y - ypos) / rowheight, 0);
                    int lastrow = Math.Min((cliprect.max_y - ypos) / rowheight, scrollrows - 1);
                    for (currow = firstrow; currow <= lastrow; currow = nextrow)
                    {
                        int scrollx = effective_rowscroll(currow);
                        for (nextrow = currow + 1; nextrow <= lastrow; nextrow++)
                            if (effective_rowscroll(nextrow) != scrollx)
                                break;
                        rect.min_y = currow * rowheight + ypos;
                        rect.max_y = nextrow * rowheight - 1 + ypos;
                        rect = sect_rect(rect, cliprect);
                        for (xpos = scrollx - width; xpos <= cliprect.max_x; xpos += width)
                        {
                            tilemap_draw_instance3(rect, xpos, ypos);
                        }
                    }
                }
            }
            else if (scrollrows == 1)
            {

            }
        }
        public void tilemap_draw_primask4(RECT cliprect)
        {
            int xpos, ypos;
            if (!enable)
                return;
            if (all_tiles_dirty)
            {
                Array.Copy(Tilemap.bbFF, tileflags,cols*rows);
                all_tiles_dirty = false;
            }
            if (scrollrows == 1 && scrollcols == 1)
            {
                int scrollx = effective_rowscroll(0);
                int scrolly = effective_colscroll(0);
                for (ypos = scrolly - height; ypos <= cliprect.max_y; ypos += height)
                    for (xpos = scrollx - width; xpos <= cliprect.max_x; xpos += width)
                    {
                        tilemap_draw_instance4(cliprect, xpos, ypos);
                    }
            }
            else if (scrollcols == 1)
            {
                RECT rect = cliprect;
                int rowheight = height / scrollrows;
                int scrolly = effective_colscroll(0);
                int currow, nextrow;
                for (ypos = scrolly - height; ypos <= cliprect.max_y; ypos += height)
                {
                    int firstrow = Math.Max((cliprect.min_y - ypos) / rowheight, 0);
                    int lastrow = Math.Min((cliprect.max_y - ypos) / rowheight, scrollrows - 1);
                    for (currow = firstrow; currow <= lastrow; currow = nextrow)
                    {
                        int scrollx = effective_rowscroll(currow);
                        for (nextrow = currow + 1; nextrow <= lastrow; nextrow++)
                            if (effective_rowscroll(nextrow) != scrollx)
                                break;
                        rect.min_y = currow * rowheight + ypos;
                        rect.max_y = nextrow * rowheight - 1 + ypos;
                        rect = sect_rect(rect, cliprect);
                        for (xpos = scrollx - width; xpos <= cliprect.max_x; xpos += width)
                        {
                            tilemap_draw_instance4(rect, xpos, ypos);
                        }
                    }
                }
            }
            else if (scrollrows == 1)
            {

            }
        }
    }
    public class Tilemap
    {        
        public static byte[,] priority_bitmap;
        public static byte[,] bb01, bbFF;
        public static byte[] empty_tile;        
        public static int screen_width, screen_height;
        public static RECT videovisarea, rect;
        private static int INVALID_LOGICAL_INDEX = -1;
        public static byte TILEMAP_PIXEL_TRANSPARENT = 0x00;
        private static byte TILEMAP_PIXEL_CATEGORY_MASK = 0x0f;		/* category is stored in the low 4 bits */
        public static byte TILE_FLAG_DIRTY = 0xff;
        public static byte TILEMAP_PIXEL_LAYER0 = 0x10;
        public static byte TILE_FLIPX = 0x01;		/* draw this tile horizontally flipped */
        public static byte TILE_FLIPY = 0x02;		/* draw this tile vertically flipped */
        public static byte TILEMAP_FLIPX = TILE_FLIPX;	/* draw the tilemap horizontally flipped */
        public static byte TILEMAP_FLIPY = TILE_FLIPY;	/* draw the tilemap vertically flipped */
        public static void tilemap_init()
        {
            int i, j;
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                    CPS.tilemap_init();
                    break;
                case "Namco System 1":                    
                    Namcos1.tilemap_init();
                    break;
                case "PGM":
                    PGM.tilemap_init();
                    break;
                case "M72":
                    M72.tilemap_init();
                    break;
                case "M92":
                    M92.tilemap_init();
                    break;
            }
            screen_width = 0x200;
            screen_height = 0x200;
            priority_bitmap = new byte[0x200, 0x200];
            bb01 = new byte[1, 0x200];
            bbFF = new byte[0x80, 0x40];
            empty_tile = new byte[0x400];
            for (i = 0; i < 0x200; i++)
            {
                bb01[0, i] = 0x01;
            }
            for (i = 0; i < 0x80; i++)
            {
                for (j = 0; j < 0x40; j++)
                {
                    bbFF[i, j] = 0xff;
                }
            }
            for (i = 0; i < 0x400; i++)
            {
                empty_tile[i] = 0x0f;
            }
            videovisarea.min_x = 0;
            videovisarea.max_x = 0x1ff;
            videovisarea.min_y = 0;
            videovisarea.max_y = 0x1ff;
        }
    }
}