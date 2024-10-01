using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace mame
{
    partial class Video
    {
        public delegate Bitmap drawcrosshairdelegate(Bitmap bm1);
        public static drawcrosshairdelegate drawcrosshair;
        public static Bitmap drawcrosshair_null(Bitmap bm1)
        {
            Bitmap bm2 = bm1;
            return bm2;
        }
        public static Bitmap drawcrosshair_opwolf(Bitmap bm1)
        {
            Bitmap bm2 = bm1;
            Graphics g = Graphics.FromImage(bm2);
            g.DrawImage(MultiplyAlpha(Crosshair.global.bitmap[0], (float)Crosshair.global.fade / 0xff), new Rectangle(Crosshair.global.x[0] - 10, Crosshair.global.y[0] - 10, 20, 20), new Rectangle(0, 0, 100, 100), GraphicsUnit.Pixel);
            g.Dispose();
            return bm2;
        }
        public static Bitmap MultiplyAlpha(Bitmap bitmap, float factor)
        {
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                colorMatrix.Matrix33 = factor;
                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix);
                graphics.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            return result;
        }
        public static void GDIDraw()
        {
            try
            {
                bitmapData = bitmapGDI.LockBits(new Rectangle(0, 0, Video.fullwidth, Video.fullheight), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                Marshal.Copy(Video.bitmapcolor, 0, bitmapData.Scan0, Video.fullwidth * Video.fullheight);
                bitmapGDI.UnlockBits(bitmapData);
                if (Wintime.osd_ticks() < popup_text_end)
                {
                    Machine.FORM.tsslStatus.Text = sDrawText;
                }
                else
                {
                    popup_text_end = 0;
                    if (Mame.paused)
                    {
                        Machine.FORM.tsslStatus.Text = "pause";
                    }
                    else
                    {
                        switch (Mame.playState)
                        {
                            case Mame.PlayState.PLAY_RECORDRUNNING:
                                Machine.FORM.tsslStatus.Text = "record";
                                break;
                            case Mame.PlayState.PLAY_REPLAYRUNNING:
                                Machine.FORM.tsslStatus.Text = "replay";
                                break;
                            default:
                                Machine.FORM.tsslStatus.Text = "run";
                                break;
                        }
                    }
                }
                bbmp[iMode] = drawcrosshair((Bitmap)bitmapGDI.Clone(new Rectangle(offsetx, offsety, width, height), PixelFormat.Format32bppArgb));
                switch (Machine.sDirection)
                {
                    case "":
                        break;
                    case "90":
                        bbmp[iMode].RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case "180":
                        bbmp[iMode].RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case "270":
                        bbmp[iMode].RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
                Machine.FORM.pictureBox1.Image = bbmp[iMode];
            }
            catch
            {
                
            }
        }
    }
}