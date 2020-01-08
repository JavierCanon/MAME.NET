using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class PGM
    {
        public static void loop_inputports_pgm_standard()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                short2 &= ~0x0001;
            }
            else
            {
                short2 |= 0x0001;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                short2 &= ~0x0002;
            }
            else
            {
                short2 |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                short0 &= ~0x0001;
            }
            else
            {
                short0 |= 0x0001;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                short0 &= ~0x0100;
            }
            else
            {
                short0 |= 0x0100;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                short0 &= ~0x0010;
            }
            else
            {
                short0 |= 0x0010;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                short0 &= ~0x0008;
            }
            else
            {
                short0 |= 0x0008;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                short0 &= ~0x0004;
            }
            else
            {
                short0 |= 0x0004;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                short0 &= ~0x0002;
            }
            else
            {
                short0 |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                short0 &= ~0x0020;
            }
            else
            {
                short0 |= 0x0020;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                short0 &= ~0x0040;
            }
            else
            {
                short0 |= 0x0040;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                short0 &= ~0x0080;
            }
            else
            {
                short0 |= 0x0080;
            }
            if (Keyboard.IsPressed(Key.U))
            {
                short2 &= ~0x0100;
            }
            else
            {
                short2 |= 0x0100;
            }
            if (Keyboard.IsPressed(Key.I))
            {
                
            }
            else
            {
                
            }
            if (Keyboard.IsPressed(Key.O))
            {

            }
            else
            {

            }
            if (Keyboard.IsPressed(Key.Right))
            {
                short0 &= ~0x1000;
            }
            else
            {
                short0 |= 0x1000;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                short0 &= ~0x0800;
            }
            else
            {
                short0 |= 0x0800;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                short0 &= ~0x0400;
            }
            else
            {
                short0 |= 0x0400;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                short0 &= ~0x0200;
            }
            else
            {
                short0 |= 0x0200;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                short0 &= ~0x2000;
            }
            else
            {
                short0 |= 0x2000;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                short0 &= ~0x4000;
            }
            else
            {
                short0 |= 0x4000;
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                short0 &= unchecked((short)~0x8000);
            }
            else
            {
                short0 |= unchecked((short)0x8000);
            }
            if (Keyboard.IsPressed(Key.NumPad4))
            {
                short2 &= ~0x0200;
            }
            else
            {
                short2 |= 0x0200;
            }
            if (Keyboard.IsPressed(Key.NumPad5))
            {
                
            }
            else
            {
                
            }
            if (Keyboard.IsPressed(Key.NumPad6))
            {

            }
            else
            {

            }
            if (Keyboard.IsPressed(Key.R))
            {
                short2 &= ~0x0020;
            }
            else
            {
                short2 |= 0x0020;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                short2 &= ~0x0080;
            }
            else
            {
                short2 |= 0x0080;
            }
        }
        public static void record_port()
        {
            if (short0 != short0_old || short1 != short1_old || short2 != short2_old || short3 != short3_old || short4 != short4_old)
            {
                short0_old = short0;
                short1_old = short1;
                short2_old = short2;
                short3_old = short3;
                short4_old = short4;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(short0);
                Mame.bwRecord.Write(short1);
                Mame.bwRecord.Write(short2);
                Mame.bwRecord.Write(short3);
                Mame.bwRecord.Write(short4);
            }
        }
        public static void replay_port()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    short0_old = Mame.brRecord.ReadInt16();
                    short1_old = Mame.brRecord.ReadInt16();
                    short2_old = Mame.brRecord.ReadInt16();
                    short3_old = Mame.brRecord.ReadInt16();
                    short4_old = Mame.brRecord.ReadInt16();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                short0 = short0_old;
                short1 = short1_old;
                short2 = short2_old;
                short3 = short3_old;
                short4 = short4_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
    }
}
