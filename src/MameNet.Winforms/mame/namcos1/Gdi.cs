using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace mame
{
    public partial class Namcos1
    {
        public static void GDIInit()
        {

        }
        public static Bitmap GetLayer(int n)
        {
            int i, j, i1;
            uint u1;
            Color c1;
            Bitmap bm1 = new Bitmap(512, 512);
            for (i = 0; i < 0x200; i++)
            {
                for (j = 0; j < 0x200; j++)
                {
                    i1 = Namcos1.ttmap[n].pixmap[i + j * 0x200] + Namcos1.ttmap[n].palette_offset;
                    u1 = Palette.entry_color[i1];
                    c1 = Color.FromArgb((int)Palette.entry_color[Namcos1.ttmap[n].pixmap[i + j * 0x200] + Namcos1.ttmap[n].palette_offset]);
                    bm1.SetPixel(i, j, c1);
                }
            }
            return bm1;
        }
        public static Bitmap GetPri()
        {
            int i, j;
            byte priority;
            Color c1;
            Bitmap bm1 = new Bitmap(512, 512);
            RECT new_clip = new RECT();
            new_clip.min_x = 0x49;
            new_clip.max_x = 0x168;
            new_clip.min_y = 0x10;
            new_clip.max_y = 0xef;
            for (priority = 0; priority < 8; priority++)
            {
                for (i = 0; i < 6; i++)
                {
                    if (Namcos1.namcos1_playfield_control[16 + i] == priority)
                    {
                        Namcos1.ttmap[i].tilemap_draw_primask(new_clip, 0x10, priority);
                    }
                }
            }
            for (i = 0; i < 0x200; i++)
            {
                for (j = 0; j < 0x200; j++)
                {
                    if (Tilemap.priority_bitmap[i, j] == 0)
                    {
                        c1 = Color.Black;
                    }
                    else if (Tilemap.priority_bitmap[i, j] == 0x01)
                    {
                        c1 = Color.Red;
                    }
                    else if (Tilemap.priority_bitmap[i, j] == 0x03)
                    {
                        c1 = Color.Orange;
                    }
                    else if (Tilemap.priority_bitmap[i, j] == 0x04)
                    {
                        c1 = Color.Yellow;
                    }
                    else if (Tilemap.priority_bitmap[i, j] == 0x07)
                    {
                        c1 = Color.Green;
                    }
                    else if (Tilemap.priority_bitmap[i, j] == 0x1f)
                    {
                        c1 = Color.Blue;
                    }
                    else
                    {
                        c1 = Color.White;
                    }
                    bm1.SetPixel(j, i, c1);
                }
            }
            return bm1;
        }
    }
}
