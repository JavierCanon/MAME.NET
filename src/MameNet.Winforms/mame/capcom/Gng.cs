using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Capcom
    {
        public static byte[] gng_fgvideoram, gng_bgvideoram;
        public static byte[] scrollx, scrolly;
        public static void gng_bankswitch_w(byte data)
        {
            if (data == 4)
            {
                basebankmain = 0x4000;
            }
            else
            {
                basebankmain = 0x10000 + 0x2000 * (data & 0x03);
            }
        }
        public static void gng_coin_counter_w(int offset, byte data)
        {
            Generic.coin_counter_w(offset, data);
        }
        public static void video_start_gng()
        {
            gng_fgvideoram = new byte[0x800];
            gng_bgvideoram = new byte[0x800];
            scrollx = new byte[2];
            scrolly = new byte[2];
        }
        public static void gng_fgvideoram_w(int offset, byte data)
        {
            gng_fgvideoram[offset] = data;
            int row, col;
            row = (offset & 0x3ff) / 0x20;
            col = (offset & 0x3ff) % 0x20;
            fg_tilemap.tilemap_mark_tile_dirty(row, col);
        }
        public static void gng_bgvideoram_w(int offset, byte data)
        {
            gng_bgvideoram[offset] = data;
            int row, col;
            row = (offset & 0x3ff) % 0x20;
            col = (offset & 0x3ff) / 0x20;
            bg_tilemap.tilemap_mark_tile_dirty(row, col);
        }
        public static void gng_bgscrollx_w(int offset, byte data)
        {
            scrollx[offset] = data;
            bg_tilemap.tilemap_set_scrollx(0, scrollx[0] + 256 * scrollx[1]);
        }
        public static void gng_bgscrolly_w(int offset, byte data)
        {
            scrolly[offset] = data;
            bg_tilemap.tilemap_set_scrolly(0, scrolly[0] + 256 * scrolly[1]);
        }
        public static void gng_flipscreen_w(byte data)
        {
            Generic.flip_screen_set(~data & 1);
        }
        public static void draw_sprites_gng(RECT cliprect)
        {
            int offs;
            for (offs = 0x200 - 4; offs >= 0; offs -= 4)
            {
                byte attributes = Generic.buffered_spriteram[offs + 1];
                int sx = Generic.buffered_spriteram[offs + 3] - 0x100 * (attributes & 0x01);
                int sy = Generic.buffered_spriteram[offs + 2];
                int flipx = attributes & 0x04;
                int flipy = attributes & 0x08;
                if (Generic.flip_screen_get() != 0)
                {
                    sx = 240 - sx;
                    sy = 240 - sy;
                    flipx = (flipx == 0 ? 1 : 0);
                    flipy = (flipy == 0 ? 1 : 0);
                }
                Drawgfx.common_drawgfx_gng(gfx3rom, Generic.buffered_spriteram[offs] + ((attributes << 2) & 0x300), (attributes >> 4) & 3, flipx, flipy, sx, sy, cliprect);
            }
        }
        public static void video_update_gng()
        {
            bg_tilemap.tilemap_draw_primask(Video.screenstate.visarea, 0x20, 0);
            draw_sprites_gng(Video.screenstate.visarea);
            bg_tilemap.tilemap_draw_primask(Video.screenstate.visarea, 0x10, 0);
            fg_tilemap.tilemap_draw_primask(Video.screenstate.visarea, 0x10, 0);
        }
        public static void video_eof_gng()
        {
            Generic.buffer_spriteram_w();
        }
    }
}
