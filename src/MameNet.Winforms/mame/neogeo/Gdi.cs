using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace mame
{
    public partial class Neogeo
    {
        private static string[] sde2 = new string[] { "," }, sde6 = new string[] { "-" };
        public static Color m_ColorG;
        private static List<int> lSprite;
        public static bool bRender0G, bRender1G;
        public static void GDIInit()
        {
            m_ColorG = Color.Magenta;
            lSprite = new List<int>();
        }
        public static Bitmap GetSprite()
        {
            int i, x, x2, y, rows, zoom_x, zoom_y, scanline, attr_and_code_offs, code, gfx_offset;
            int sprite_y, tile, zoom_x_table_offset, line_pens_offset, sprite_line, zoom_line, x_inc;
            ushort y_control, zoom_control, attr, x_2, code_2;
            byte sprite_y_and_tile;
            bool invert;
            Machine.FORM.neogeoform.tbResult.Clear();
            Bitmap bm1;
            bm1 = new Bitmap(384, 264);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                y = 0;
                x = 0;
                rows = 0;
                zoom_y = 0;
                zoom_x = 0;
                foreach (int sprite_number in lSprite)
                {
                    y_control = Neogeo.neogeo_videoram[0x8200 | sprite_number];
                    zoom_control = Neogeo.neogeo_videoram[0x8000 | sprite_number];
                    x_2 = Neogeo.neogeo_videoram[0x8400 | sprite_number];
                    code_2 = Neogeo.neogeo_videoram[sprite_number << 6];
                    if ((y_control & 0x40) != 0)
                    {
                        x = (x + zoom_x + 1) & 0x01ff;
                        zoom_x = (zoom_control >> 8) & 0x0f;
                    }
                    else
                    {
                        y = 0x200 - (y_control >> 7);
                        x = Neogeo.neogeo_videoram[0x8400 | sprite_number] >> 7;
                        zoom_y = zoom_control & 0xff;
                        zoom_x = (zoom_control >> 8) & 0x0f;
                        rows = y_control & 0x3f;
                    }
                    Machine.FORM.neogeoform.tbResult.AppendText(sprite_number.ToString("X3") + " " + x_2.ToString("X4") + " " + y_control.ToString("X4") + " " + code_2.ToString("X4") + " " + zoom_control.ToString("X4") + "\r\n");
                    if (((x >= 0x140) && (x <= 0x1f0)) || rows == 0)
                        continue;
                    if (x == 0)
                    {
                        int i1 = 1;
                    }
                    for (scanline = 0; scanline < 264; scanline++)
                    {
                        if (Neogeo.sprite_on_scanline(scanline, y, rows))
                        {
                            sprite_line = (scanline - y) & 0x1ff;
                            zoom_line = sprite_line & 0xff;
                            invert = ((sprite_line & 0x100) != 0) ? true : false;
                            if (invert)
                                zoom_line ^= 0xff;
                            if (rows > 0x20)
                            {
                                zoom_line = zoom_line % ((zoom_y + 1) << 1);
                                if (zoom_line > zoom_y)
                                {
                                    zoom_line = ((zoom_y + 1) << 1) - 1 - zoom_line;
                                    invert = !invert;
                                }
                            }
                            sprite_y_and_tile = Neogeo.zoomyrom[(zoom_y << 8) | zoom_line];
                            sprite_y = sprite_y_and_tile & 0x0f;
                            tile = sprite_y_and_tile >> 4;
                            if (invert)
                            {
                                sprite_y ^= 0x0f;
                                tile ^= 0x1f;
                            }
                            attr_and_code_offs = (sprite_number << 6) | (tile << 1);
                            attr = Neogeo.neogeo_videoram[attr_and_code_offs + 1];
                            code = ((attr << 12) & 0x70000) | Neogeo.neogeo_videoram[attr_and_code_offs];
                            if (Neogeo.auto_animation_disabled == 0)
                            {
                                if ((attr & 0x0008) != 0)
                                    code = (code & ~0x07) | (Neogeo.auto_animation_counter & 0x07);
                                else if ((attr & 0x0004) != 0)
                                    code = (code & ~0x03) | (Neogeo.auto_animation_counter & 0x03);
                            }
                            if ((attr & 0x0002) != 0)
                                sprite_y ^= 0x0f;
                            zoom_x_table_offset = 0;
                            gfx_offset = (int)(((code << 8) | (sprite_y << 4)) & Neogeo.sprite_gfx_address_mask);
                            line_pens_offset = attr >> 8 << 4;
                            if ((attr & 0x0001) != 0)
                            {
                                gfx_offset = gfx_offset + 0x0f;
                                x_inc = -1;
                            }
                            else
                            {
                                x_inc = 1;
                            }
                            if (x <= 0x01f0)
                            {
                                x2 = x + 30;
                                for (i = 0; i < 0x10; i++)
                                {
                                    if (Neogeo.zoom_x_tables[zoom_x, zoom_x_table_offset] != 0)
                                    {
                                        if (Neogeo.sprite_gfx[gfx_offset] != 0)
                                        {
                                            ptr2 = ptr + (scanline * 384 + x2) * 4;
                                            *ptr2 = (byte)(Neogeo.pens[line_pens_offset + Neogeo.sprite_gfx[gfx_offset]] & 0xff);
                                            *(ptr2 + 1) = (byte)((Neogeo.pens[line_pens_offset + Neogeo.sprite_gfx[gfx_offset]] & 0xff00) >> 8);
                                            *(ptr2 + 2) = (byte)((Neogeo.pens[line_pens_offset + Neogeo.sprite_gfx[gfx_offset]] & 0xff0000) >> 16);
                                            *(ptr2 + 3) = 0xff;
                                        }
                                        x2++;
                                    }
                                    zoom_x_table_offset++;
                                    gfx_offset += x_inc;
                                }
                            }
                            else
                            {
                                int i1 = 1;
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetFixedlayer()
        {
            byte[] gfx_base;
            int i, j,x,y, addr_mask, code, gfx_offset,char_pens_offset;
            int[] garouoffsets, pix_offsets = new int[] { 0x10, 0x18, 0x00, 0x08 };
            ushort code_and_palette;
            bool banked;
            byte data;
            Bitmap bm1;
            bm1 = new Bitmap(384,264);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                if (fixed_layer_source != 0)
                {
                    gfx_base = fixedrom;
                    addr_mask = fixedrom.Length - 1;
                }
                else
                {
                    gfx_base = fixedbiosrom;
                    addr_mask = fixedbiosrom.Length - 1;
                }
                banked = (fixed_layer_source != 0) && (addr_mask > 0x1ffff);
                garouoffsets = new int[32];
                banked = (fixed_layer_source != 0) && (addr_mask > 0x1ffff);
                if (banked && neogeo_fixed_layer_bank_type == 1)
                {
                    int garoubank = 0;
                    int k = 0;
                    y = 0;
                    while (y < 32)
                    {
                        if (neogeo_videoram[0x7500 + k] == 0x0200 && (neogeo_videoram[0x7580 + k] & 0xff00) == 0xff00)
                        {
                            garoubank = neogeo_videoram[0x7580 + k] & 3;
                            garouoffsets[y++] = garoubank;
                        }
                        garouoffsets[y++] = garoubank;
                        k += 2;
                    }
                }
                for (y = 0; y < 33; y++)
                {
                    for (x = 0; x < 40; x++)
                    {
                        code_and_palette = neogeo_videoram[0x7000 | (y + x * 0x20)];
                        code = code_and_palette & 0x0fff;
                        if (banked)
                        {
                            switch (neogeo_fixed_layer_bank_type)
                            {
                                case 1:
                                    code += 0x1000 * (garouoffsets[(y - 2) & 31] ^ 3);
                                    break;
                                case 2:
                                    code += 0x1000 * (((neogeo_videoram[0x7500 + ((y - 1) & 31) + 32 * (x / 6)] >> (5 - (x % 6)) * 2) & 3) ^ 3);
                                    break;
                            }
                        }
                        data = 0;
                        for (i = 0; i < 8; i++)
                        {
                            gfx_offset = ((code << 5) | i) & addr_mask;
                            char_pens_offset = code_and_palette >> 12 << 4;
                            for (j = 0; j < 8; j++)
                            {
                                if ((j & 0x01) != 0)
                                    data = (byte)(data >> 4);
                                else
                                    data = gfx_base[gfx_offset + pix_offsets[j >> 1]];
                                if ((data & 0x0f) != 0)
                                {
                                    ptr2 = ptr + (((y * 8) + i) * 384 + 30 + x * 8 + j) * 4;
                                    *ptr2 = (byte)(pens[char_pens_offset + (data & 0x0f)] & 0xff);
                                    *(ptr2 + 1) = (byte)((pens[char_pens_offset + (data & 0x0f)] & 0xff00) >> 8);
                                    *(ptr2 + 2) = (byte)((pens[char_pens_offset + (data & 0x0f)] & 0xff0000) >> 16);
                                    *(ptr2 + 3) = 0xff;
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
            string[] ss1, ss2, ss3;
            int i1, i2, i3, i4, n1, n2, width, height;
            lSprite.Clear();
            ss1 = Machine.FORM.neogeoform.tbSprite.Text.Split(sde2, StringSplitOptions.RemoveEmptyEntries);
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
            width = int.Parse(ss3[0]);
            height = int.Parse(ss3[1]);
            Bitmap bm1 = new Bitmap(width, height), bm2;
            m_ColorG = Color.FromArgb(unchecked((int)0xff000000) | pens[0xfff]);
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(m_ColorG);
            if (bRender0G)
            {
                bm2 = GetSprite();
                g.DrawImage(bm2, 0, 0);
            }
            if (bRender1G)
            {
                bm2 = GetFixedlayer();
                g.DrawImage(bm2, 0, 0);
            }
            g.Dispose();
            return bm1;
        }
        public static Bitmap DrawSprite(int SOffset,int pensoffset)
        {
            Bitmap bm1 = new Bitmap(32, 32), bm2;
            m_ColorG = Color.FromArgb(unchecked((int)0xff000000) | pens[0xfff]);
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(m_ColorG);
            bm2 = GetSprite1(SOffset, pensoffset);
            g.DrawImage(bm2, 0, 0);
            g.Dispose();
            return bm1;
        }
        public static Bitmap GetSprite1(int gfx_offset, int line_pens_offset)
        {
            int i, j,col, n1, n2;
            Bitmap bm1;
            n1 = 32;
            n2 = 32;
            bm1 = new Bitmap(n1, n2);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (j = 0; j < n2; j++)
                {
                    for (i = 0; i < n1; i++)
                    {
                        col = i / 16;
                        ptr2 = ptr + (j * n1 + i) * 4;
                        *ptr2 = (byte)(pens[line_pens_offset + sprite_gfx[gfx_offset + 0x200 * col + j * 16 + i % 16]] & 0xff);
                        *(ptr2 + 1) = (byte)((pens[line_pens_offset + sprite_gfx[gfx_offset + 0x200 * col + j * 16 + i % 16]] & 0xff00) >> 8);
                        *(ptr2 + 2) = (byte)((pens[line_pens_offset + sprite_gfx[gfx_offset + 0x200 * col + j * 16 + i % 16]] & 0xff0000) >> 16);
                        *(ptr2 + 3) = 0xff;
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
    }
}
