using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class Dataeast
    {
        public class fr1
        {
            public int fr;
            public byte by;
            public fr1(int i1, byte b1)
            {
                fr = i1;
                by = b1;
            }
        }
        public static int i3 = 70;
        public static List<fr1> lfr = new List<fr1>();
        public static void loop_inputports_dataeast_pcktgal()
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
                byte2 &= unchecked((byte)~0x20);
            }
            else
            {
                byte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                byte1 &= unchecked((byte)~0x10);
            }
            else
            {
                byte1 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                byte1 &= unchecked((byte)~0x20);
            }
            else
            {
                byte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                byte1 &= unchecked((byte)~0x01);
            }
            else
            {
                byte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                byte1 &= unchecked((byte)~0x02);
            }
            else
            {
                byte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                byte1 &= unchecked((byte)~0x04);
            }
            else
            {
                byte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                byte1 &= unchecked((byte)~0x08);
            }
            else
            {
                byte1 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                byte1 &= unchecked((byte)~0x80);
            }
            else
            {
                byte1 |= 0x80;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                byte1 &= unchecked((byte)~0x40);
            }
            else
            {
                byte1 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                byte2 &= unchecked((byte)~0x01);
            }
            else
            {
                byte2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                byte2 &= unchecked((byte)~0x02);
            }
            else
            {
                byte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                byte2 &= unchecked((byte)~0x04);
            }
            else
            {
                byte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                byte2 &= unchecked((byte)~0x08);
            }
            else
            {
                byte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                byte2 &= unchecked((byte)~0x80);
            }
            else
            {
                byte2 |= 0x80;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                byte2 &= unchecked((byte)~0x40);
            }
            else
            {
                byte2 |= 0x40;
            }
            if (Keyboard.IsTriggered(Key.N))
            {
                lfr = new List<fr1>();
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 1), 0x7f));
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 2), 0xff));
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 2+i3), 0x7f));
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 2+i3+1), 0xff));
            }
            if (Keyboard.IsTriggered(Key.U))
            {
                lfr = new List<fr1>();
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 1), 0xf7));
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 2), 0xff));
            }
            if (Keyboard.IsTriggered(Key.I))
            {
                lfr = new List<fr1>();
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 1), 0xfb));
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 2), 0xff));
            }
            if (Keyboard.IsTriggered(Key.V))
            {
                lfr = new List<fr1>();
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 1), 0xfd));
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 2), 0xff));
            }
            if (Keyboard.IsTriggered(Key.B))
            {
                lfr = new List<fr1>();
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 1), 0xfe));
                lfr.Add(new fr1((int)(Video.screenstate.frame_number + 2), 0xff));
            }
            foreach (fr1 f in lfr)
            {
                if (Video.screenstate.frame_number == f.fr)
                {
                    byte1 = f.by;
                    lfr.Remove(f);
                    break;
                }
            }
        }
        public static void record_port_pcktgal()
        {
            if (byte1 != byte1_old || byte2 != byte2_old)
            {
                byte1_old = byte1;
                byte2_old = byte2;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(byte1);
                Mame.bwRecord.Write(byte2);
            }
        }
        public static void replay_port_pcktgal()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
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
