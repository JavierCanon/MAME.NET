using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace mame
{
    public partial class CPS
    {
        private static string[] sde2;
        private static int base_cps1_objG;
        private static int[] maskG;
        private static int[] mapsizeG;
        public static Color[] cc1G;
        public static Color m_ColorG;
        public static int nColorG, nSpriteG;
        private static byte[,] flagsmap0G, flagsmap1G, flagsmap2G;
        private static byte[,] pen_to_flags0G, pen_to_flags1G, pen_to_flags2G;
        public static byte[,] priority_bitmapG;
        private static int videocontrolG;
        public static int scrollx0, scrolly0;
        public static int scrollx1, scrolly1;
        public static int scrollx2, scrolly2;
        public static int scrollxSG, scrollySG;
        public static int[] iiCutColorG;
        public static int l0G, l1G, l2G, l3G;
        public static bool bRender0G, bRender1G, bRender2G, bRender3G;
        private static bool enable0G, enable1G, enable2G;
        private static int baseTilemap0G, baseTilemap1G, baseTilemap2G, basePaletteG;
        public static byte[] bbPaletteG;
        private static int layercontrolG, scrollrows1G;
        private static int[] rowscroll1G;
        public static int[] cps1_scrollxG, cps1_scrollyG;
        public delegate Bitmap gettileDelegateG();
        public static gettileDelegateG[] gettileDelegatesG;
        public delegate void gethighDelegateG();
        public static gethighDelegateG[] gethighDelegatesG;
        public static void GDIInit()
        {
            maskG = new int[4];
            mapsizeG = new int[3] { 0x200, 0x400, 0x800 };
            nColorG=0xc00;
            sde2 = new string[] { "," };
            scrollxSG = 0;
            scrollySG = 0;
            bbPaletteG = new byte[nColorG * 2];
            rowscroll1G = new int[1024];
            cps1_scrollxG = new int[3];
            cps1_scrollyG = new int[3];
            gettileDelegatesG = new gettileDelegateG[]{
                new gettileDelegateG(GetSpriteGDI),
                new gettileDelegateG(GetTilemapGDI0),
                new gettileDelegateG(GetTilemapGDI1),
                new gettileDelegateG(GetTilemapGDI2)
            };
            gethighDelegatesG = new gethighDelegateG[]{
                null,
                new gethighDelegateG(GetHighGDI0),
                new gethighDelegateG(GetHighGDI1),
                new gethighDelegateG(GetHighGDI2)
            };
            flagsmap0G = new byte[0x200, 0x200];
            flagsmap1G = new byte[0x400, 0x400];
            flagsmap2G = new byte[0x800, 0x800];
            pen_to_flags0G = new byte[4, 16];
            pen_to_flags1G = new byte[4, 16];
            pen_to_flags2G = new byte[4, 16];
            priority_bitmapG = new byte[0x200, 0x200];
            cc1G = new Color[nColorG];
            m_ColorG = Color.Magenta;
        }
        public static void GetData()
        {
            string[] ss1 = Machine.FORM.cpsform.tbInput.Text.Split(sde2, StringSplitOptions.RemoveEmptyEntries);
            int n1 = ss1.Length;
            int i1, iR, iG, iB;
            int bright;
            int i;
            int group, pen;
            byte fgbits, bgbits;
            iiCutColorG = new int[n1];
            for (i1 = 0; i1 < n1; i1++)
            {
                iiCutColorG[i1] = int.Parse(ss1[i1]);
            }       
            base_cps1_objG = (cps_a_regs[0] * 0x100) & 0x3ffff;
            baseTilemap0G = (cps_a_regs[1] * 0x100) & 0x3ffff;
            baseTilemap1G = (cps_a_regs[2] * 0x100) & 0x3ffff;
            baseTilemap2G = (cps_a_regs[3] * 0x100) & 0x3ffff;
            basePaletteG = (cps_a_regs[5] * 0x100) & 0x3ffff;
            if (!Machine.FORM.cpsform.cbLockpal.Checked)
            {
                for (i1 = 0; i1 < nColorG * 2; i1++)
                {
                    bbPaletteG[i1] = gfxram[basePaletteG + i1];
                }
            }
            int ctrl = cps_b_regs[palette_control / 2];
            int page, startpage = 0;
            for (page = 0; page < 6; ++page)
            {
                if (((ctrl >> page) & 1) != 0)
                {
                    startpage = page;
                    break;
                }
            }
            for (i1 = 0; i1 < nColorG - startpage * 0x200; i1++)
            {
                bright = bbPaletteG[i1 * 2] / 16 * 2 + 0x0f;
                iR = bbPaletteG[i1 * 2] % 16 * 0x11 * bright / 0x2d;
                iG = bbPaletteG[i1 * 2 + 1] / 16 * 0x11 * bright / 0x2d;
                iB = bbPaletteG[i1 * 2 + 1] % 16 * 0x11 * bright / 0x2d;
                if (i1 % 16 == 15)
                {
                    cc1G[startpage * 0x200 + i1] = Color.Transparent;
                }
                else
                {
                    cc1G[startpage * 0x200 + i1] = Color.FromArgb(iR, iG, iB);
                }
            }
            if (Machine.FORM.cpsform.cbRowscroll.Checked == true)
            {
                videocontrolG = cps_a_regs[0x22 / 2];
            }
            else
            {
                videocontrolG = cps_a_regs[0x22 / 2] & 0xfffe;
            }
            cps1_scrollxG[0] = cps_a_regs[0x0c / 2] + scroll1xoff - scrollxoff;
            cps1_scrollyG[0] = cps_a_regs[0x0e / 2] - scrollyoff;
            cps1_scrollxG[1] = cps_a_regs[0x10 / 2] + scroll2xoff - scrollxoff;
            cps1_scrollyG[1] = cps_a_regs[0x12 / 2] - scrollyoff;
            cps1_scrollxG[2] = cps_a_regs[0x14 / 2] + scroll3xoff - scrollxoff;
            cps1_scrollyG[2] = cps_a_regs[0x16 / 2] - scrollyoff;
            scrollx0 = (-scroll1x) & 0x1ff;
            scrolly0 = (0x100 - scroll1y) & 0x1ff;
            scrollx1 = (-scroll2x) & 0x3ff;
            scrolly1 = (0x100 - scroll2y) & 0x3ff;
            scrollx2 = (-scroll3x) & 0x7ff;
            scrolly2 = (0x100 - scroll3y) & 0x7ff;
            layercontrolG = cps_b_regs[layer_control / 2];
            enable0G = ((layercontrolG & layer_enable_mask[0]) != 0);
            enable1G = ((layercontrolG & layer_enable_mask[1]) != 0 && (videocontrolG & 4) != 0);
            enable2G = ((layercontrolG & layer_enable_mask[2]) != 0 && (videocontrolG & 8) != 0);
            l0G = (layercontrolG >> 0x06) & 03;
            l1G = (layercontrolG >> 0x08) & 03;
            l2G = (layercontrolG >> 0x0a) & 03;
            l3G = (layercontrolG >> 0x0c) & 03;
            for (group = 0; group < 4; group++)
            {
                if (priority[group] != -1)
                {
                    maskG[group] = cps_b_regs[priority[group] / 2] ^ 0xffff;
                }
                else
                {
                    if ((layercontrolG & (1 << group)) != 0)
                    {
                        maskG[group] = 0x8000;
                    }
                    else
                    {
                        maskG[group] = 0xffff;
                    }
                }
                for (pen = 0; pen < 16; pen++)
                {
                    fgbits = ((maskG[group] >> pen) % 2 == 1) ? (byte)0x00 : (byte)0x10;
                    if (pen < 15)
                    {
                        bgbits = 0x20;
                    }
                    else
                    {
                        bgbits = 0x00;
                    }
                    pen_to_flags0G[group, pen] = (byte)(fgbits | bgbits);
                    pen_to_flags1G[group, pen] = (byte)(fgbits | bgbits);
                    pen_to_flags2G[group, pen] = (byte)(fgbits | bgbits);
                }
            }
            if ((videocontrolG & 0x01) != 0)
            {
                int scrly = -scroll2y;
                scrollrows1G = 1024;
                int otheroffs = cps_a_regs[CPS1_ROWSCROLL_OFFS];
                for (i = 0; i < 0x400; i++)
                {
                    rowscroll1G[(i - scrly) & 0x3ff] = gfxram[(other + otheroffs + i) * 2] * 0x100 + gfxram[(other + otheroffs + i) * 2 + 1];
                }
            }
            else
            {
                scrollrows1G = 1;
                rowscroll1G[0] = scroll2x;
                for (i = 1; i < 1024; i++)
                {
                    rowscroll1G[i] = 0;
                }
            }
        }
        public static Bitmap GetTilemapGDI0()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int scanheight = 0x100, scanrows;
            int tilewidth, tileheight;
            int gfxset;
            tilewidth = 8;
            tileheight = tilewidth;
            rows = 0x40;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            scanrows = scanheight / tileheight;
            int iByte;
            int iCode, iAttr;
            int iColor, iFlag, iGroup;
            int idx = 0;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0, match;
            ushort[] uuVRam0;
            uuVRam0 = new ushort[0x2000];
            for (i1 = 0; i1 < 0x2000; i1++)
            {
                uuVRam0[i1] = (ushort)(gfxram[baseTilemap0G + i1 * 2] * 0x100 + gfxram[baseTilemap0G + i1 * 2 + 1]);
            }
            Color c1 = new Color();
            Bitmap bm1;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                if (enable0G)
                {
                    byte* ptr = (byte*)(bmData.Scan0);
                    byte* ptr2 = (byte*)0;
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = (i3 * scanrows + i4 % scanrows) * 2 + i4 / scanrows * 0x1000;
                            iCode = uuVRam0[iOffset3];
                            iAttr = uuVRam0[iOffset3 + 1];
                            iColor = iAttr % 0x20 + 0x20;
                            iFlag = ((iAttr & 0x60) >> 5) & 3;
                            iGroup = (iAttr & 0x0180) >> 7;
                            match = 0;
                            foreach (gfx_range r in lsRange0)
                            {
                                if (iCode >= r.start && iCode <= r.end)
                                {
                                    iCode += r.add;
                                    match = 1;
                                    break;
                                }
                            }
                            if (match == 0)
                            {
                                continue;
                            }
                            else
                            {
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
                                gfxset = i3 & 1;
                                for (i1 = 0; i1 < tilewidth; i1++)
                                {
                                    for (i2 = 0; i2 < tileheight; i2++)
                                    {
                                        iOffset = iCode * 0x80 + gfxset * 8 + i1 + i2 * 0x10;
                                        iByte = gfx1rom[iOffset];
                                        idx = iColor * 0x10 + iByte;
                                        c1 = cc1G[idx];
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
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetTilemapGDI1()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int scanheight = 0x100, scanrows;
            int tilewidth, tileheight;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0, match;
            tilewidth = 16;
            tileheight = tilewidth;
            rows = 0x40;
            cols = rows;
            width = tilewidth * cols;
            height = width;
            scanrows = scanheight / tileheight;
            int iByte;
            int iCode, iAttr;
            int iColor, iFlag, iGroup;
            int idx;
            ushort[] uuVRam1;
            uuVRam1 = new ushort[0x2000];
            for (i1 = 0; i1 < 0x2000; i1++)
            {
                uuVRam1[i1] = (ushort)(gfxram[baseTilemap1G + i1 * 2] * 0x100 + gfxram[baseTilemap1G + i1 * 2 + 1]);
            }
            Color c1 = new Color();
            Bitmap bm1;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                if (enable1G)
                {
                    byte* ptr = (byte*)(bmData.Scan0);
                    byte* ptr2 = (byte*)0;
                    if (scrollrows1G == 1)
                    {
                        for (i3 = 0; i3 < cols; i3++)
                        {
                            for (i4 = 0; i4 < rows; i4++)
                            {
                                iOffset3 = (i3 * scanrows + i4 % scanrows) * 2 + i4 / scanrows * 0x1000 / 2;
                                iCode = uuVRam1[iOffset3];
                                iAttr = uuVRam1[iOffset3 + 1];
                                iColor = iAttr % 0x20 + 0x40;
                                iFlag = ((iAttr & 0x60) >> 5) & 3;
                                iGroup = (iAttr & 0x0180) >> 7;
                                match = 0;
                                foreach (gfx_range r in lsRange1)
                                {
                                    if (iCode >= r.start && iCode <= r.end)
                                    {
                                        iCode += r.add;
                                        match = 1;
                                        break;
                                    }
                                }
                                if (match == 0)
                                {
                                    continue;
                                }
                                else
                                {
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
                                            iOffset = iCode * 0x40 * 4 + i1 + i2 * 16;
                                            iByte = gfx1rom[iOffset];
                                            idx = iColor * 0x10 + iByte;
                                            c1 = cc1G[idx];
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
                    }
                    else
                    {
                        for (i3 = 0; i3 < cols; i3++)
                        {
                            for (i4 = 0; i4 < rows; i4++)
                            {
                                iOffset3 = (i3 * scanrows + i4 % scanrows) * 2 + i4 / scanrows * 0x1000 / 2;
                                iCode = uuVRam1[iOffset3];
                                iAttr = uuVRam1[iOffset3 + 1];
                                iColor = iAttr % 0x20 + 0x40;
                                iFlag = ((iAttr & 0x60) >> 5) & 3;
                                iGroup = (iAttr & 0x0180) >> 7;
                                match = 0;
                                foreach (gfx_range r in lsRange1)
                                {
                                    if (iCode >= r.start && iCode <= r.end)
                                    {
                                        iCode += r.add;
                                        match = 1;
                                        break;
                                    }
                                }
                                if (match == 0)
                                {
                                    continue;
                                }
                                if (iCode == -1)
                                {
                                    continue;
                                }
                                else
                                {
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
                                            iOffset = iCode * 0x100 + i1 + i2 * 0x10;
                                            iByte = gfx1rom[iOffset];
                                            idx = iColor * 0x10 + iByte;
                                            c1 = cc1G[idx];
                                            ptr2 = ptr + ((y0 + dy0 * i2) * width + ((-rowscroll1G[y0 + dy0 * i2] + x0 + dx0 * i1) & 0x3ff)) * 4;
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
            bm1.UnlockBits(bmData);
            return bm1;
        }
        public static Bitmap GetTilemapGDI2()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, width, height;
            int scanheight = 0x100, scanrows;
            int tilewidth, tileheight;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0, match;
            tilewidth = 0x20;
            tileheight = tilewidth;
            cols = 0x40;
            rows = cols;
            width = tilewidth * cols;
            height = width;
            scanrows = scanheight / tileheight;
            int iByte;
            int iCode, iAttr;
            int iColor, iFlag, iGroup;
            int idx;
            ushort[] uuVRam2;
            uuVRam2 = new ushort[0x2000];
            for (i1 = 0; i1 < 0x2000; i1++)
            {
                uuVRam2[i1] = (ushort)(gfxram[baseTilemap2G + i1 * 2] * 0x100 + gfxram[baseTilemap2G + i1 * 2 + 1]);
            }
            Color c1 = new Color();
            Bitmap bm1;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                if (enable2G)
                {
                    byte* ptr = (byte*)(bmData.Scan0);
                    byte* ptr2 = (byte*)0;
                    for (i3 = 0; i3 < cols; i3++)
                    {
                        for (i4 = 0; i4 < rows; i4++)
                        {
                            iOffset3 = (i3 * scanrows + i4 % scanrows) * 2 + i4 / scanrows * 0x1000 / 4;
                            iCode = uuVRam2[iOffset3];
                            iAttr = uuVRam2[iOffset3 + 1];
                            iColor = iAttr % 0x20 + 0x60;
                            iFlag = ((iAttr & 0x60) >> 5) & 3;
                            iGroup = (iAttr & 0x0180) >> 7;
                            match = 0;
                            foreach (gfx_range r in lsRange2)
                            {
                                if (iCode >= r.start && iCode <= r.end)
                                {
                                    iCode += r.add;
                                    match = 1;
                                    break;
                                }
                            }
                            if (match == 0)
                            {
                                continue;
                            }
                            else
                            {
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
                                        iOffset = iCode * 0x400 + i1 + i2 * 0x20;
                                        iByte = gfx1rom[iOffset];
                                        idx = iColor * 0x10 + iByte;
                                        c1 = cc1G[idx];
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
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        private static Bitmap GetSpriteGDI()
        {
            int i1, iX, iY, iXin, iYin, iCode, iAttr, iColor, iFlag, iOffset, iByte, cols, rows;
            Bitmap bm1 = null;
            ushort[] VRAMS;
            List<int> lsColor = new List<int>();
            int iSprite, i3, i4, i5, i6, match;
            int tilewidth = 16, tileheight = 16;
            Color c1 = new Color();
            bm1 = new Bitmap(512, 512);
            VRAMS = new ushort[0x400];
            nSpriteG = 0x100;
            for (i1 = 0; i1 < 0x400; i1++)
            {
                VRAMS[i1] = (ushort)(gfxram[base_cps1_objG + i1 * 2] * 0x100 + gfxram[base_cps1_objG + i1 * 2 + 1]);
                if (i1 % 4 == 3 && (VRAMS[i1] & 0xff00) == 0xff00)
                {
                    nSpriteG = i1 / 4;
                    break;
                }
            }
            Machine.FORM.cpsform.tbResult.Clear();
            Bitmap[] bbm2 = new Bitmap[nSpriteG];
            Graphics g = Graphics.FromImage(bm1);
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0;
            int baseSprite = 0, addSprite = 0;
            if (bootleg_kludge == 1 || bootleg_kludge == 2 || bootleg_kludge == 3)
            {
                baseSprite = 0;
                addSprite = 1;
            }
            else
            {
                baseSprite = nSpriteG - 1;
                addSprite = -1;
            }
            for (i1 = 0; i1 < nSpriteG; i1++)
            {
                iSprite = baseSprite + addSprite * i1;
                iX = VRAMS[iSprite * 4];
                iY = VRAMS[iSprite * 4 + 1];
                iCode = VRAMS[iSprite * 4 + 2];
                iAttr = VRAMS[iSprite * 4 + 3];
                if (iX == 0 && iY == 0 && iCode == 0 && iAttr == 0)
                {
                    continue;
                }
                match = 0;
                foreach (gfx_range r in lsRangeS)
                {
                    if (iCode >= r.start && iCode <= r.end)
                    {
                        iCode += r.add;
                        match = 1;
                        break;
                    }
                }
                if (match == 0)
                {
                    continue;
                }
                iY = (iY + 0x100) & 0x1ff;
                iColor = iAttr % 0x20;
                iFlag = ((iAttr & 0x60) >> 5) & 3;
                cols = iAttr / 256 % 16 + 1;
                rows = iAttr / 256 / 16 + 1;
                if (lsColor.IndexOf(iColor) < 0)
                {
                    Machine.FORM.cpsform.tbResult.AppendText(iColor.ToString() + ",");
                    lsColor.Add(iColor);
                }
                if (Array.IndexOf(iiCutColorG, iColor) >= 0)
                {
                    continue;
                }
                bbm2[iSprite] = new Bitmap(16 * cols, 16 * rows);
                for (i3 = 0; i3 < cols; i3++)
                {
                    for (i4 = 0; i4 < rows; i4++)
                    {
                        iOffset = (iCode + i3 + i4 * 16) * 0x100;
                        if (iFlag == 0)
                        {
                            x0 = tilewidth * i3;
                            y0 = tileheight * i4;
                            dx0 = 1;
                            dy0 = 1;
                        }
                        else if (iFlag == 1)
                        {
                            x0 = tilewidth * (cols - 1 - i3) + 15;
                            y0 = tileheight * i4;
                            dx0 = -1;
                            dy0 = 1;
                        }
                        else if (iFlag == 2)
                        {
                            x0 = tilewidth * i3;
                            y0 = tileheight * (rows - 1 - i4) * 16 + 15;
                            dx0 = 1;
                            dy0 = -1;
                        }
                        else if (iFlag == 3)
                        {
                            x0 = tilewidth * (cols - 1 - i3) + 15;
                            y0 = tileheight * (rows - 1 - i4) * 16 + 15;
                            dx0 = -1;
                            dy0 = -1;
                        }
                        for (i5 = 0; i5 < 0x10; i5++)
                        {
                            for (i6 = 0; i6 < 0x10; i6++)
                            {
                                iByte = gfx1rom[iOffset + i5 + i6 * 0x10];
                                c1 = cc1G[iColor * 0x10 + iByte];
                                iXin = (iX + scrollxSG + x0 + dx0 * i5) & 0x1ff;
                                iYin = (iY + scrollySG + y0 + dy0 * i6) & 0x1ff;
                                if ((priority_bitmapG[iXin, iYin] & 0x1f) == 0x00)
                                {
                                    bbm2[iSprite].SetPixel(x0 + dx0 * i5, y0 + dy0 * i6, c1);
                                }
                            }
                        }
                    }
                }
                g.DrawImage(bbm2[iSprite], iX + scrollxSG, iY + scrollySG);
                //g.DrawImage(bbm2[iSprite], iX + scrollxSG - 0x200, iY + scrollySG);
                //g.DrawImage(bbm2[iSprite], iX + scrollxSG, iY + scrollySG - 0x200);
            }
            g.Dispose();
            return bm1;
        }
        public static void GetHighGDI0()
        {
            int iPen = 0;
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, tilewidth, tileheight, width, height;
            int scanheight = 0x100, scanrows;
            int xpos, ypos, match;
            rows = 0x40;
            cols = rows;
            tilewidth = 8;
            tileheight = tilewidth;
            width = tilewidth * cols;
            height = width;
            scanrows = scanheight / tileheight;
            int iCode, iAttr;
            int iColor, iFlag, iGroup;
            int palette_base;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0;
            //byte andmask, ormask;
            byte data, pen = 0, map;
            ushort[] uuVRam0 = new ushort[0x2000];
            for (i1 = 0; i1 < 0x2000; i1++)
            {
                uuVRam0[i1] = (ushort)(gfxram[baseTilemap0G + i1 * 2] * 0x100 + gfxram[baseTilemap0G + i1 * 2 + 1]);
            }
            for (i3 = 0; i3 < cols; i3++)
            {
                for (i4 = 0; i4 < rows; i4++)
                {
                    iOffset3 = (i3 * scanrows + i4 % scanrows) * 2 + i4 / scanrows * 0x1000;
                    iCode = uuVRam0[iOffset3];
                    iAttr = uuVRam0[iOffset3 + 1];
                    iColor = iAttr % 0x20 + 0x20;
                    iFlag = ((iAttr & 0x60) >> 5) & 3;
                    iGroup = (iAttr & 0x0180) >> 7;
                    iPen = iAttr % 0x20;
                    palette_base = iColor * 0x10;
                    match = 0;
                    foreach (gfx_range r in lsRange0)
                    {
                        if (iCode >= r.start && iCode <= r.end)
                        {
                            iCode += r.add;
                            match = 1;
                            break;
                        }
                    }
                    //andmask = 0xff;
                    //ormask = 0;
                    if (match == 0)
                    {
                        continue;
                    }
                    else
                    {
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
                        for (i2 = 0; i2 < tileheight; i2++)
                        {
                            for (i1 = 0; i1 < tilewidth; i1 ++)
                            {
                                iOffset = iCode * 0x80 + i1 + i2 * 0x10;
                                pen = gfx1rom[iOffset];
                                map = pen_to_flags0G[iGroup, pen];
                                //andmask &= map;
                                //ormask |= map;
                                flagsmap0G[x0 + dx0 * i1, y0 + dy0 * i2] = map;
                            }
                        }
                        //tileflags0G[i3, i4] = (byte)(andmask ^ ormask);
                    }
                }
            }
            for (ypos = scrolly0 - height; ypos <= 0x1ff; ypos += height)
            {
                for (xpos = scrollx0 - width; xpos <= 0x1ff; xpos += width)
                {
                    for (i1 = 0; i1 < 0x200; i1++)
                    {
                        for (i2 = 0; i2 < 0x200; i2++)
                        {
                            if (xpos + i1 >= 0 && xpos + i1 < 0x200 && ypos + i2 >= 0 && ypos + i2 < 0x200)
                            {
                                priority_bitmapG[xpos + i1, ypos + i2] = flagsmap0G[(0x200 + xpos + i1 - scrolly1) & 0x1ff, (0x200 + ypos + i2 + scrolly0) & 0x1ff];
                            }
                        }
                    }
                }
            }
        }
        public static void GetHighGDI1()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, tilewidth, tileheight, width, height;
            int scanheight = 0x100, scanrows;
            int xpos, ypos, match;
            rows = 0x40;
            cols = rows;
            tilewidth = 0x10;
            tileheight = tilewidth;
            width = tilewidth * cols;
            height = width;
            scanrows = scanheight / tileheight;
            int iCode, iAttr;
            int iColor, iFlag, iGroup;
            int palette_base;
            //byte andmask, ormask;
            byte data, pen = 0, map;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0;
            ushort[] uuVRam1;
            uuVRam1 = new ushort[0x2000];
            for (i1 = 0; i1 < 0x2000; i1++)
            {
                uuVRam1[i1] = (ushort)(CPS.gfxram[baseTilemap1G + i1 * 2] * 0x100 + CPS.gfxram[baseTilemap1G + i1 * 2 + 1]);
            }
            for (i3 = 0; i3 < cols; i3++)
            {
                for (i4 = 0; i4 < rows; i4++)
                {
                    iOffset3 = (i3 * scanrows + i4 % scanrows) * 2 + i4 / scanrows * 0x1000 / 2;
                    iCode = uuVRam1[iOffset3];
                    iAttr = uuVRam1[iOffset3 + 1];
                    iColor = iAttr % 0x20 + 0x40;
                    iFlag = ((iAttr & 0x60) >> 5) & 3;
                    iGroup = (iAttr & 0x0180) >> 7;
                    palette_base = iColor * 0x10;
                    match = 0;
                    foreach (gfx_range r in lsRange1)
                    {
                        if (iCode >= r.start && iCode <= r.end)
                        {
                            iCode += r.add;
                            match = 1;
                            break;
                        }
                    }
                    //andmask = 0xff;
                    //ormask = 0;
                    if (match == 0)
                    {
                        continue;
                    }
                    else
                    {
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
                        for (i2 = 0; i2 < tileheight; i2++)
                        {
                            for (i1 = 0; i1 < tilewidth; i1 ++)
                            {
                                iOffset = iCode * 0x100 + i1 + i2 * 0x10;
                                pen = gfx1rom[iOffset];
                                map = pen_to_flags1G[iGroup, pen];
                                //andmask &= map;
                                //ormask |= map;
                                flagsmap1G[x0 + dx0 * i1, y0 + dy0 * i2] = map;
                            }
                        }
                        //tileflags1G[i3, i4] = (byte)(andmask ^ ormask);
                    }
                }
            }
            if (scrollrows1G == 1)
            {
                //Tilemap.effective_rowscroll1(0);
                //Tilemap.effective_colscroll1();
                for (ypos = scrolly1 - height; ypos <= 0x1ff; ypos += height)
                {
                    for (xpos = -width; xpos <= 0x1ff; xpos += width)
                    {
                        for (i1 = 0; i1 < 0x400; i1++)
                        {
                            for (i2 = 0; i2 < 0x400; i2++)
                            {
                                if (xpos + i1 >= 0 && xpos + i1 < 0x200 && ypos + i2 >= 0 && ypos + i2 < 0x200)
                                {
                                    priority_bitmapG[xpos + i1, ypos + i2] = flagsmap1G[(xpos + i1 + scroll2x) & 0x3ff, (ypos + i2 - scrolly1) & 0x3ff];
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                int scrollx;
                for (ypos = scrolly1 - height; ypos <= 0x1ff; ypos += height)
                {
                    scrollx = (-scroll2x) & 0x3ff;
                    for (xpos = scrollx - width; xpos <= 0x1ff; xpos += width)
                    {
                        for (i1 = 0; i1 < 0x400; i1++)
                        {
                            for (i2 = 0; i2 <= 0x400; i2++)
                            {
                                if (xpos + i1 >= 0 && xpos + i1 < 0x200 && ypos + i2 >= 0 && ypos + i2 < 0x200)
                                {
                                    priority_bitmapG[xpos + i1, ypos + i2] = flagsmap1G[(rowscroll1G[(ypos + i2 - scrolly1) & 0x3ff] - scrollx + xpos + i1) & 0x3ff, (ypos + i2 - scrolly1) & 0x3ff];
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void GetHighGDI2()
        {
            int i1, i2, iOffset, i3, i4, iOffset3 = 0;
            int rows, cols, tilewidth, tileheight, width, height;
            int scanheight = 0x100, scanrows;
            int xpos, ypos, match;
            cols = 0x40;
            rows = cols;
            tilewidth = 0x20;
            tileheight = tilewidth;
            width = tilewidth * cols;
            height = width;
            scanrows = scanheight / tileheight;
            int iCode, iAttr;
            int iColor, iFlag, iGroup;
            int palette_base;
            byte pen = 0, map;
            int x0 = 0, y0 = 0, dx0 = 0, dy0 = 0;
            ushort[] uuVRam2 = new ushort[0x2000];
            for (i1 = 0; i1 < 0x2000; i1++)
            {
                uuVRam2[i1] = (ushort)(gfxram[baseTilemap2G + i1 * 2] * 0x100 + gfxram[baseTilemap2G + i1 * 2 + 1]);
            }
            for (i3 = 0; i3 < cols; i3++)
            {
                for (i4 = 0; i4 < rows; i4++)
                {
                    iOffset3 = (i3 * scanrows + i4 % scanrows) * 2 + i4 / scanrows * 0x1000 / 4;
                    iCode = uuVRam2[iOffset3];
                    iAttr = uuVRam2[iOffset3 + 1];
                    iColor = iAttr % 0x20 + 0x60;
                    iFlag = ((iAttr & 0x60) >> 5) & 3;
                    iGroup = (iAttr & 0x0180) >> 7;
                    palette_base = iColor * 0x10;
                    match = 0;
                    foreach (gfx_range r in lsRange2)
                    {
                        if (iCode >= r.start && iCode <= r.end)
                        {
                            iCode += r.add;
                            match = 1;
                            break;
                        }
                    }
                    if (match == 0)
                    {
                        continue;
                    }
                    else
                    {
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
                        for (i2 = 0; i2 < tileheight; i2++)
                        {
                            for (i1 = 0; i1 < tilewidth; i1 ++)
                            {
                                iOffset = iCode * 0x400 + i1 + i2 * 0x20;
                                pen = gfx1rom[iOffset];
                                map = pen_to_flags2G[iGroup, pen];
                                flagsmap2G[x0 + dx0 * i1, y0 + dy0 * i2] = map;
                            }
                        }
                    }
                }
            }
            for (ypos = scrolly2 - height; ypos <= 0x1ff; ypos += height)
            {
                for (xpos = scrollx2 - width; xpos <= 0x1ff; xpos += width)
                {
                    for (i1 = 0; i1 < 0x800; i1++)
                    {
                        for (i2 = 0; i2 < 0x800; i2++)
                        {
                            if (xpos + i1 >= 0 && xpos + i1 < 0x200 && ypos + i2 >= 0 && ypos + i2 < 0x200)
                            {
                                priority_bitmapG[xpos + i1, ypos + i2] = flagsmap2G[(0x800 + xpos + i1 - scrollx2) & 0x7ff, (0x800 + ypos + i2 - scrolly2) & 0x7ff];
                            }
                        }
                    }
                }
            }
        }
        public static Bitmap GetAllGDI()
        {
            string[] ss1 = Machine.FORM.cpsform.tbPoint.Text.Split(sde2, StringSplitOptions.RemoveEmptyEntries);
            int width, height;
            width = int.Parse(ss1[0]);
            height = int.Parse(ss1[1]);
            Bitmap bm1 = new Bitmap(width, height), bm2;
            Graphics g = Graphics.FromImage(bm1);
            g.Clear(m_ColorG);
            if (bRender0G)
            {
                if (l0G == 0)
                {
                    bm2 = gettileDelegatesG[l0G]();
                    g.DrawImage(bm2, 0, 0);
                }
                else
                {
                    bm2 = gettileDelegatesG[l0G]();
                    g.DrawImage(bm2, -cps1_scrollxG[l0G - 1] % mapsizeG[l0G - 1], -cps1_scrollyG[l0G - 1] % mapsizeG[l0G - 1]);
                    g.DrawImage(bm2, mapsizeG[l0G - 1] - cps1_scrollxG[l0G - 1] % mapsizeG[l0G - 1], -cps1_scrollyG[l0G - 1] % mapsizeG[l0G - 1]);
                    g.DrawImage(bm2, -cps1_scrollxG[l0G - 1] % mapsizeG[l0G - 1], mapsizeG[l0G - 1] - cps1_scrollyG[l0G - 1] % mapsizeG[l0G - 1]);
                    g.DrawImage(bm2, mapsizeG[l0G - 1] - cps1_scrollxG[l0G - 1] % mapsizeG[l0G - 1], mapsizeG[l0G - 1] - cps1_scrollyG[l0G - 1] % mapsizeG[l0G - 1]);
                }
            }
            if (bRender1G)
            {
                if (l1G == 0)
                {
                    gethighDelegatesG[l0G]();
                    bm2 = gettileDelegatesG[l1G]();
                    g.DrawImage(bm2, 0, 0);
                }
                else
                {
                    bm2 = gettileDelegatesG[l1G]();
                    g.DrawImage(bm2, -cps1_scrollxG[l1G - 1] % mapsizeG[l1G - 1], -cps1_scrollyG[l1G - 1] % mapsizeG[l1G - 1]);
                    g.DrawImage(bm2, mapsizeG[l1G - 1] - cps1_scrollxG[l1G - 1] % mapsizeG[l1G - 1], -cps1_scrollyG[l1G - 1] % mapsizeG[l1G - 1]);
                    g.DrawImage(bm2, -cps1_scrollxG[l1G - 1] % mapsizeG[l1G - 1], mapsizeG[l1G - 1] - cps1_scrollyG[l1G - 1] % mapsizeG[l1G - 1]);
                    g.DrawImage(bm2, mapsizeG[l1G - 1] - cps1_scrollxG[l1G - 1] % mapsizeG[l1G - 1], mapsizeG[l1G - 1] - cps1_scrollyG[l1G - 1] % mapsizeG[l1G - 1]);
                }
            }
            if (bRender2G)
            {
                if (l2G == 0)
                {
                    gethighDelegatesG[l1G]();
                    bm2 = gettileDelegatesG[l2G]();
                    g.DrawImage(bm2, 0, 0);
                }
                else
                {
                    bm2 = gettileDelegatesG[l2G]();
                    g.DrawImage(bm2, -cps1_scrollxG[l2G - 1] % mapsizeG[l2G - 1], -cps1_scrollyG[l2G - 1] % mapsizeG[l2G - 1]);
                    g.DrawImage(bm2, mapsizeG[l2G - 1] - cps1_scrollxG[l2G - 1] % mapsizeG[l2G - 1], -cps1_scrollyG[l2G - 1] % mapsizeG[l2G - 1]);
                    g.DrawImage(bm2, -cps1_scrollxG[l2G - 1] % mapsizeG[l2G - 1], mapsizeG[l2G - 1] - cps1_scrollyG[l2G - 1] % mapsizeG[l2G - 1]);
                    g.DrawImage(bm2, mapsizeG[l2G - 1] - cps1_scrollxG[l2G - 1] % mapsizeG[l2G - 1], mapsizeG[l2G - 1] - cps1_scrollyG[l2G - 1] % mapsizeG[l2G - 1]);
                }
            }
            if (bRender3G)
            {
                if (l3G == 0)
                {
                    gethighDelegatesG[l2G]();
                    bm2 = gettileDelegatesG[l3G]();
                    g.DrawImage(bm2, 0, 0);
                }
                else
                {
                    bm2 = gettileDelegatesG[l3G]();
                    g.DrawImage(bm2, -cps1_scrollxG[l3G - 1] % mapsizeG[l3G - 1], -cps1_scrollyG[l3G - 1] % mapsizeG[l3G - 1]);
                    g.DrawImage(bm2, mapsizeG[l3G - 1] - cps1_scrollxG[l3G - 1] % mapsizeG[l3G - 1], -cps1_scrollyG[l3G - 1] % mapsizeG[l3G - 1]);
                    g.DrawImage(bm2, -cps1_scrollxG[l3G - 1] % mapsizeG[l3G - 1], mapsizeG[l3G - 1] - cps1_scrollyG[l3G - 1] % mapsizeG[l3G - 1]);
                    g.DrawImage(bm2, mapsizeG[l3G - 1] - cps1_scrollxG[l3G - 1] % mapsizeG[l3G - 1], mapsizeG[l3G - 1] - cps1_scrollyG[l3G - 1] % mapsizeG[l3G - 1]);
                }
            }
            g.Dispose();
            switch (Machine.sDirection)
            {
                case "":
                    break;
                case "270":
                    bm1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            return bm1;
        }
    }
}
