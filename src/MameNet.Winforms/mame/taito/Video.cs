using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Taito
    {
        public static byte[] gfx1rom, gfx2rom, gfx12rom, gfx22rom, prom;
        public static int bublbobl_objectram_size = 0x300;
        public static RECT cliprect;
        public static ushort[] uuFF;
        public static void video_start_bublbobl()
        {
            int i;
            uuFF = new ushort[0x100 * 0x100];
            for (i = 0; i < 0x10000; i++)
            {
                uuFF[i] = 0xff;
            }
            cliprect = new RECT();
            cliprect.min_x = 0;
            cliprect.max_x = 255;
            cliprect.min_y = 16;
            cliprect.max_y = 239;
        }
        public static void video_update_bublbobl()
        {
            int offs;
            int sx, sy, xc, yc;
            int gfx_num, gfx_attr, gfx_offs;
            int prom_line_offset;
            Array.Copy(uuFF, Video.bitmapbase[Video.curbitmap], 0x10000);
            if (bublbobl_video_enable == 0)
            {
                return;
            }
            sx = 0;
            if (videoram[0xe86] == 0x7b)
            {
                int i1 = 1;
            }
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
                    if ((prom[prom_line_offset+ yc / 2] & 0x08) != 0)
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
                        Drawgfx.common_drawgfx_bublbobl(gfx1rom, code, color, flipx, flipy, x, y, cliprect);
                    }
                }
                sx += 16;
            }
        }
        public static void video_eof_taito()
        {

        }
        public static void video_start_opwolf()
        {
            cliprect = new RECT();
            cliprect.min_x = 0;
            cliprect.max_x = 319;
            cliprect.min_y = 8;
            cliprect.max_y = 247;
            PC080SN_vh_start(1, 1, 0, 0, 0, 0, 0);
            PC090OJ_vh_start(0, 0, 0, 0);
        }
        public static void video_update_opwolf()
        {
            int[] layer = new int[2];
            PC080SN_tilemap_update();
            layer[0] = 0;
            layer[1] = 1;
            Array.Clear(Tilemap.priority_bitmap, 0, 0x14000);
            PC080SN_tilemap_draw(0, layer[0], 0, 1);
            PC080SN_tilemap_draw(0, layer[1], 0x10, 2);
            PC090OJ_draw_sprites(1);
        }
        public static void opwolf_spritectrl_w(int offset, ushort data)
        {
            if (offset == 0)
            {
                PC090OJ_sprite_ctrl = (ushort)((data & 0xe0) >> 5);
            }
        }
        public static void opwolf_spritectrl_w2(int offset, byte data)
        {
            if (offset == 0)
            {
                PC090OJ_sprite_ctrl = (ushort)((data & 0xe0) >> 5);
            }
        }
    }
}