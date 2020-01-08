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
                bbmp[iMode] = (Bitmap)bitmapGDI.Clone(new Rectangle(offsetx, offsety, width, height), PixelFormat.Format32bppArgb);
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