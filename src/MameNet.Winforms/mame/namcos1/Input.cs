using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class Namcos1
    {
        public static void loop_inputports_ns1_3b()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                byte2 &= unchecked((byte)~0x10);
            }
            else
            {
                byte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                byte2 &= unchecked((byte)~0x08);
            }
            else
            {
                byte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                byte0 &= unchecked((byte)~0x80);
            }
            else
            {
                byte0 |= 0x80;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                byte1 &= unchecked((byte)~0x80);
            }
            else
            {
                byte1 |= 0x80;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                byte0 &= unchecked((byte)~0x01);
            }
            else
            {
                byte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                byte0 &= unchecked((byte)~0x02);
            }
            else
            {
                byte0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                byte0 &= unchecked((byte)~0x04);
            }
            else
            {
                byte0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                byte0 &= unchecked((byte)~0x08);
            }
            else
            {
                byte0 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                byte0 &= unchecked((byte)~0x10);
            }
            else
            {
                byte0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                byte0 &= unchecked((byte)~0x20);
            }
            else
            {
                byte0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                byte0 &= unchecked((byte)~0x40);
            }
            else
            {
                byte0 |= 0x40;
            }            
            if (Keyboard.IsPressed(Key.Right))
            {
                byte1 &= unchecked((byte)~0x01);
            }
            else
            {
                byte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                byte1 &= unchecked((byte)~0x02);
            }
            else
            {
                byte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                byte1 &= unchecked((byte)~0x04);
            }
            else
            {
                byte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                byte1 &= unchecked((byte)~0x08);
            }
            else
            {
                byte1 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                byte1 &= unchecked((byte)~0x10);
            }
            else
            {
                byte1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                byte1 &= unchecked((byte)~0x20);
            }
            else
            {
                byte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                byte1 &= unchecked((byte)~0x40);
            }
            else
            {
                byte1 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                byte2 &= unchecked((byte)~0x20);
            }
            else
            {
                byte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                byte2 &= unchecked((byte)~0x40);
            }
            else
            {
                byte2 |= 0x40;
            }
        }
        public static void loop_inputports_ns1_quester()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                byte2 &= unchecked((byte)~0x10);
            }
            else
            {
                byte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                byte2 &= unchecked((byte)~0x08);
            }
            else
            {
                byte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                byte0 &= unchecked((byte)~0x80);
            }
            else
            {
                byte0 |= 0x80;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                byte1 &= unchecked((byte)~0x80);
            }
            else
            {
                byte1 |= 0x80;
            }            
            if (Keyboard.IsPressed(Key.J))
            {
                byte0 &= unchecked((byte)~0x10);
            }
            else
            {
                byte0 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                byte0 &= unchecked((byte)~0x20);
            }
            else
            {
                byte0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.L))
            {
                byte0 &= unchecked((byte)~0x40);
            }
            else
            {
                byte0 |= 0x40;
            }            
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                byte1 &= unchecked((byte)~0x10);
            }
            else
            {
                byte1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                byte1 &= unchecked((byte)~0x20);
            }
            else
            {
                byte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                byte1 &= unchecked((byte)~0x40);
            }
            else
            {
                byte1 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                byte2 &= unchecked((byte)~0x20);
            }
            else
            {
                byte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                byte2 &= unchecked((byte)~0x40);
            }
            else
            {
                byte2 |= 0x40;
            }
            Inptport.frame_update_analog_field_quester_p0(Inptport.analog_p0);
            Inptport.frame_update_analog_field_quester_p1(Inptport.analog_p1);
        }
        public static void loop_inputports_ns1_berabohm()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                byte2 &= unchecked((byte)~0x10);
            }
            else
            {
                byte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                byte2 &= unchecked((byte)~0x08);
            }
            else
            {
                byte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                byte0 &= unchecked((byte)~0x80);
            }
            else
            {
                byte0 |= 0x80;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                byte1 &= unchecked((byte)~0x80);
            }
            else
            {
                byte1 |= 0x80;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                byte0 &= unchecked((byte)~0x01);
            }
            else
            {
                byte0 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                byte0 &= unchecked((byte)~0x02);
            }
            else
            {
                byte0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                byte0 &= unchecked((byte)~0x04);
            }
            else
            {
                byte0 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                byte0 &= unchecked((byte)~0x08);
            }
            else
            {
                byte0 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                byte01 |= 0x01;
            }
            else
            {
                byte01 &= unchecked((byte)~0x01);
            }
            if (Keyboard.IsPressed(Key.K))
            {
                byte01 |= 0x02;
            }
            else
            {
                byte01 &= unchecked((byte)~0x02);
            }
            if (Keyboard.IsPressed(Key.L))
            {
                byte01 |= 0x04;
            }
            else
            {
                byte01 &= unchecked((byte)~0x04);
            }
            if (Keyboard.IsPressed(Key.U))
            {
                byte00 |= 0x01;
            }
            else
            {
                byte00 &= unchecked((byte)~0x01);
            }
            if (Keyboard.IsPressed(Key.I))
            {
                byte00 |= 0x02;
            }
            else
            {
                byte00 &= unchecked((byte)~0x02);
            }
            if (Keyboard.IsPressed(Key.O))
            {
                byte00 |= 0x04;
            }
            else
            {
                byte00 &= unchecked((byte)~0x04);
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                byte1 &= unchecked((byte)~0x01);
            }
            else
            {
                byte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                byte1 &= unchecked((byte)~0x02);
            }
            else
            {
                byte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                byte1 &= unchecked((byte)~0x04);
            }
            else
            {
                byte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                byte1 &= unchecked((byte)~0x08);
            }
            else
            {
                byte1 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                byte03 |= 0x01;
            }
            else
            {
                byte03 &= unchecked((byte)~0x01);
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                byte03 |= 0x02;
            }
            else
            {
                byte03 &= unchecked((byte)~0x02);
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                byte03 |= 0x04;
            }
            else
            {
                byte03 &= unchecked((byte)~0x04);
            }
            if (Keyboard.IsPressed(Key.NumPad4))
            {
                byte02 |= 0x01;
            }
            else
            {
                byte02 &= unchecked((byte)~0x01);
            }
            if (Keyboard.IsPressed(Key.NumPad5))
            {
                byte02 |= 0x02;
            }
            else
            {
                byte02 &= unchecked((byte)~0x02);
            }
            if (Keyboard.IsPressed(Key.NumPad6))
            {
                byte02 |= 0x04;
            }
            else
            {
                byte02 &= unchecked((byte)~0x04);
            }
            if (Keyboard.IsPressed(Key.R))
            {
                byte2 &= unchecked((byte)~0x20);
            }
            else
            {
                byte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                byte2 &= unchecked((byte)~0x40);
            }
            else
            {
                byte2 |= 0x40;
            }
        }
        public static void loop_inputports_ns1_faceoff()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                byte2 &= unchecked((byte)~0x10);
            }
            else
            {
                byte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                byte2 &= unchecked((byte)~0x08);
            }
            else
            {
                byte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                byte0 &= unchecked((byte)~0x80);
            }
            else
            {
                byte0 |= 0x80;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                byte1 &= unchecked((byte)~0x80);
            }
            else
            {
                byte1 |= 0x80;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                byte00 &= unchecked((byte)~0x01);
            }
            else
            {
                byte00 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                byte00 &= unchecked((byte)~0x02);
            }
            else
            {
                byte00 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                byte00 &= unchecked((byte)~0x04);
            }
            else
            {
                byte00 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                byte00 &= unchecked((byte)~0x08);
            }
            else
            {
                byte00 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                byte00 &= unchecked((byte)~0x10);
            }
            else
            {
                byte00 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                byte01 &= unchecked((byte)~0x10);
            }
            else
            {
                byte01 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                byte01 &= unchecked((byte)~0x01);
            }
            else
            {
                byte01 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                byte01 &= unchecked((byte)~0x02);
            }
            else
            {
                byte01 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                byte01 &= unchecked((byte)~0x04);
            }
            else
            {
                byte01 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                byte01 &= unchecked((byte)~0x08);
            }
            else
            {
                byte01 |= 0x08;
            }            
            if (Keyboard.IsPressed(Key.R))
            {
                byte2 &= unchecked((byte)~0x20);
            }
            else
            {
                byte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                byte2 &= unchecked((byte)~0x40);
            }
            else
            {
                byte2 |= 0x40;
            }
        }
        public static void loop_inputports_ns1_tankfrce4()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                byte2 &= unchecked((byte)~0x10);
            }
            else
            {
                byte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                byte2 &= unchecked((byte)~0x08);
            }
            else
            {
                byte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                byte00 &= unchecked((byte)~0x01);
            }
            else
            {
                byte00 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                byte00 &= unchecked((byte)~0x02);
            }
            else
            {
                byte00 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                byte00 &= unchecked((byte)~0x04);
            }
            else
            {
                byte00 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                byte00 &= unchecked((byte)~0x08);
            }
            else
            {
                byte00 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                byte00 &= unchecked((byte)~0x10);
            }
            else
            {
                byte00 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                byte02 &= unchecked((byte)~0x01);
            }
            else
            {
                byte02 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                byte02 &= unchecked((byte)~0x02);
            }
            else
            {
                byte02 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                byte02 &= unchecked((byte)~0x04);
            }
            else
            {
                byte02 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                byte02 &= unchecked((byte)~0x08);
            }
            else
            {
                byte02 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                byte02 &= unchecked((byte)~0x10);
            }
            else
            {
                byte02 |= 0x10;
            }
            /*if (Keyboard.IsPressed(Key.R))
            {
                byte2 &= unchecked((byte)~0x20);
            }
            else
            {
                byte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                byte2 &= unchecked((byte)~0x40);
            }
            else
            {
                byte2 |= 0x40;
            }*/
        }
        public static void record_port()
        {
            if (byte0 != byte0_old || byte1 != byte1_old || byte2 != byte2_old)
            {
                byte0_old = byte0;
                byte1_old = byte1;
                byte2_old = byte2;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(byte0);
                Mame.bwRecord.Write(byte1);
                Mame.bwRecord.Write(byte2);
            }
        }
        public static void replay_port()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    byte0_old = Mame.brRecord.ReadByte();
                    byte1_old = Mame.brRecord.ReadByte();
                    byte2_old = Mame.brRecord.ReadByte();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                byte0 = byte0_old;
                byte1 = byte1_old;
                byte2 = byte2_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
    }
}
