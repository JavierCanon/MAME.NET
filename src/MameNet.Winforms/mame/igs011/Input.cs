using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class IGS011
    {
        public static void loop_inputports_igs011_drgnwrld()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                sbytec &= ~0x01;
            }
            else
            {
                sbytec |= 0x01;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                sbytec &= ~0x02;
            }
            else
            {
                sbytec |= 0x02;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                sbyte0 &= ~0x01;
            }
            else
            {
                sbyte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                sbyte2 &= ~0x10;
            }
            else
            {
                sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D))// || Mouse.deltaX > 0)
            {
                sbyte0 &= ~0x10;
            }
            else
            {
                sbyte0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.A))// || Mouse.deltaX < 0)
            {
                sbyte2 &= ~0x02;
            }
            else
            {
                sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.S))// || Mouse.deltaY > 0)
            {
                sbyte0 &= ~0x04;
            }
            else
            {
                sbyte0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.W))// || Mouse.deltaY < 0)
            {
                sbyte2 &= ~0x01;
            }
            else
            {
                sbyte2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.J))// || Mouse.buttons[0] != 0)
            {
                sbyte2 &= ~0x04;
            }
            else
            {
                sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.K))// || Mouse.buttons[1] != 0)
            {
                sbyte0 &= ~0x40;
            }
            else
            {
                sbyte0 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                sbyte2 &= ~0x08;
            }
            else
            {
                sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.U))
            {
                
            }
            else
            {

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
                sbyte2 &= ~0x40;
            }
            else
            {
                sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                sbyte1 &= ~0x08;
            }
            else
            {
                sbyte1 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                sbyte2 &= ~0x20;
            }
            else
            {
                sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                sbyte1 &= ~0x02;
            }
            else
            {
                sbyte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                sbyte1 &= ~0x20;
            }
            else
            {
                sbyte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                sbyte2 &= unchecked((sbyte)~0x80);
            }
            else
            {
                sbyte2 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                sbyte1 &= unchecked((sbyte)~0x80);
            }
            else
            {
                sbyte1 |= unchecked((sbyte)0x80);
            }
            if (Keyboard.IsPressed(Key.NumPad4))
            {

            }
            else
            {

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
                sbytec &= ~0x08;
            }
            else
            {
                sbytec |= 0x08;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                sbytec &= ~0x10;
            }
            else
            {
                sbytec |= 0x10;
            }
        }
        public static void loop_inputports_igs011_lhb()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                sbytec &= ~0x10;
            }
            else
            {
                sbytec |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                bkey0 &= unchecked((byte)~0x20);
            }
            else
            {
                bkey0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                bkey1 &= unchecked((byte)~0x20);
            }
            else
            {
                bkey1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.D3))
            {
                bkey4 &= unchecked((byte)~0x10);
            }
            else
            {
                bkey4 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D4))
            {
                bkey4 &= unchecked((byte)~0x20);
            }
            else
            {
                bkey4 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                bkey0 &= unchecked((byte)~0x01);
            }
            else
            {
                bkey0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.B))
            {
                bkey1 &= unchecked((byte)~0x01);
            }
            else
            {
                bkey1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.C))
            {
                bkey2 &= unchecked((byte)~0x01);
            }
            else
            {
                bkey2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                bkey3 &= unchecked((byte)~0x01);
            }
            else
            {
                bkey3 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.E))
            {
                bkey0 &= unchecked((byte)~0x02);
            }
            else
            {
                bkey0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.F))
            {
                bkey1 &= unchecked((byte)~0x02);
            }
            else
            {
                bkey1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.G))
            {
                bkey2 &= unchecked((byte)~0x02);
            }
            else
            {
                bkey2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.H))
            {
                bkey3 &= unchecked((byte)~0x02);
            }
            else
            {
                bkey3 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.I))
            {
                bkey0 &= unchecked((byte)~0x04);
            }
            else
            {
                bkey0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                bkey1 &= unchecked((byte)~0x04);
            }
            else
            {
                bkey1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                bkey2 &= unchecked((byte)~0x04);
            }
            else
            {
                bkey2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                bkey3 &= unchecked((byte)~0x04);
            }
            else
            {
                bkey3 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.M))
            {
                bkey0 &= unchecked((byte)~0x08);
            }
            else
            {
                bkey0 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.N))
            {
                bkey1 &= unchecked((byte)~0x08);
            }
            else
            {
                bkey1 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.O))
            {
                bkey4 &= unchecked((byte)~0x04);
            }
            else
            {
                bkey4 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Q))
            {
                bkey0 &= unchecked((byte)~0x10);
            }
            else
            {
                bkey0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                bkey1 &= unchecked((byte)~0x10);
            }
            else
            {
                bkey1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                bkey2 &= unchecked((byte)~0x08);
            }
            else
            {
                bkey2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.U))
            {
                bkey4 &= unchecked((byte)~0x02);
            }
            else
            {
                bkey4 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                bkey3 &= unchecked((byte)~0x08);
            }
            else
            {
                bkey3 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.Y))
            {
                bkey4 &= unchecked((byte)~0x01);
            }
            else
            {
                bkey4 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.Z))
            {
                bkey2 &= unchecked((byte)~0x10);
            }
            else
            {
                bkey2 |= 0x10;
            }
        }
        public static void loop_inputports_igs011_lhb2()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                sbytec &= ~0x10;
            }
            else
            {
                sbytec |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                bkey0 &= unchecked((byte)~0x20);
            }
            else
            {
                bkey0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                bkey1 &= unchecked((byte)~0x20);
            }
            else
            {
                bkey1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                bkey0 &= unchecked((byte)~0x01);
            }
            else
            {
                bkey0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.B))
            {
                bkey1 &= unchecked((byte)~0x01);
            }
            else
            {
                bkey1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.C))
            {
                bkey2 &= unchecked((byte)~0x01);
            }
            else
            {
                bkey2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                bkey3 &= unchecked((byte)~0x01);
            }
            else
            {
                bkey3 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.E))
            {
                bkey0 &= unchecked((byte)~0x02);
            }
            else
            {
                bkey0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.F))
            {
                bkey1 &= unchecked((byte)~0x02);
            }
            else
            {
                bkey1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.G))
            {
                bkey2 &= unchecked((byte)~0x02);
            }
            else
            {
                bkey2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.H))
            {
                bkey3 &= unchecked((byte)~0x02);
            }
            else
            {
                bkey3 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.I))
            {
                bkey0 &= unchecked((byte)~0x04);
            }
            else
            {
                bkey0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                bkey1 &= unchecked((byte)~0x04);
            }
            else
            {
                bkey1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                bkey2 &= unchecked((byte)~0x04);
            }
            else
            {
                bkey2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                bkey3 &= unchecked((byte)~0x04);
            }
            else
            {
                bkey3 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.M))
            {
                bkey0 &= unchecked((byte)~0x08);
            }
            else
            {
                bkey0 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.N))
            {
                bkey1 &= unchecked((byte)~0x08);
            }
            else
            {
                bkey1 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.Q))
            {
                bkey0 &= unchecked((byte)~0x10);
            }
            else
            {
                bkey0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                bkey1 &= unchecked((byte)~0x10);
            }
            else
            {
                bkey1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                bkey2 &= unchecked((byte)~0x08);
            }
            else
            {
                bkey2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                bkey3 &= unchecked((byte)~0x08);
            }
            else
            {
                bkey3 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.Z))
            {
                bkey2 &= unchecked((byte)~0x10);
            }
            else
            {
                bkey2 |= 0x10;
            }
        }
        public static void record_port_drgnwrld()
        {
            if (sbyte0 != sbyte0_old || sbyte1 != sbyte1_old || sbyte2 != sbyte2_old || sbytec != sbytec_old)
            {
                sbyte0_old = sbyte0;
                sbyte1_old = sbyte1;
                sbyte2_old = sbyte2;
                sbytec_old = sbytec;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(sbyte0);
                Mame.bwRecord.Write(sbyte1);
                Mame.bwRecord.Write(sbyte2);
                Mame.bwRecord.Write(sbytec);
            }
        }
        public static void replay_port_drgnwrld()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    sbyte0_old = Mame.brRecord.ReadSByte();
                    sbyte1_old = Mame.brRecord.ReadSByte();
                    sbyte2_old = Mame.brRecord.ReadSByte();
                    sbytec_old = Mame.brRecord.ReadSByte();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                sbyte0 = sbyte0_old;
                sbyte1 = sbyte1_old;
                sbyte2 = sbyte2_old;
                sbytec = sbytec_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
        public static void record_port_lhb()
        {
            if (sbyte0 != sbyte0_old || sbyte1 != sbyte1_old || sbyte2 != sbyte2_old || sbytec != sbytec_old)
            {
                sbyte0_old = sbyte0;
                sbyte1_old = sbyte1;
                sbyte2_old = sbyte2;
                sbytec_old = sbytec;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(sbyte0);
                Mame.bwRecord.Write(sbyte1);
                Mame.bwRecord.Write(sbyte2);
                Mame.bwRecord.Write(sbytec);
            }
        }
        public static void replay_port_lhb()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    sbyte0_old = Mame.brRecord.ReadSByte();
                    sbyte1_old = Mame.brRecord.ReadSByte();
                    sbyte2_old = Mame.brRecord.ReadSByte();
                    sbytec_old = Mame.brRecord.ReadSByte();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                sbyte0 = sbyte0_old;
                sbyte1 = sbyte1_old;
                sbyte2 = sbyte2_old;
                sbytec = sbytec_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
    }
}
