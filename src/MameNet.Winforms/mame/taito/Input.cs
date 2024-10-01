using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class Taito
    {
        public static void loop_inputports_taito_common()
        {

        }
        public static void loop_inputports_taito_bublbobl()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                sbyte0 |= 0x04;
            }
            else
            {
                sbyte0 &= ~0x04;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                sbyte0 |= 0x08;
            }
            else
            {
                sbyte0 &= ~0x08;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                sbyte1 &= ~0x40;
            }
            else
            {
                sbyte1 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                sbyte2 &= ~0x40;
            }
            else
            {
                sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                sbyte1 &= ~0x02;
            }
            else
            {
                sbyte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                sbyte1 &= ~0x01;
            }
            else
            {
                sbyte1 |= 0x01;
            }
            /*if (Keyboard.IsPressed(Key.S))
            {
                sbyte2 &= ~0x02;
            }
            else
            {
                sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                sbyte2 &= ~0x01;
            }
            else
            {
                sbyte2 |= 0x01;
            }*/
            if (Keyboard.IsPressed(Key.J))
            {
                sbyte1 &= ~0x20;
            }
            else
            {
                sbyte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                sbyte1 &= ~0x10;
            }
            else
            {
                sbyte1 |= 0x10;
            }
            /*if (Keyboard.IsPressed(Key.L))
            {
                sbyte1 &= ~0x04;
            }
            else
            {
                sbyte1 |= 0x04;
            }*/
            if (Keyboard.IsPressed(Key.Right))
            {
                sbyte2 &= ~0x02;
            }
            else
            {
                sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                sbyte2 &= ~0x01;
            }
            else
            {
                sbyte2 |= 0x01;
            }
            /*if (Keyboard.IsPressed(Key.Down))
            {
                sbyte2 &= ~0x20;
            }
            else
            {
                sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                sbyte2 &= ~0x10;
            }
            else
            {
                sbyte2 |= 0x10;
            }*/
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                sbyte2 &= ~0x20;
            }
            else
            {
                sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                sbyte2 &= ~0x10;
            }
            else
            {
                sbyte2 |= 0x10;
            }
            /*if (Keyboard.IsPressed(Key.NumPad3))
            {
                sbyte1 &= ~0x40;
            }
            else
            {
                sbyte1 |= 0x40;
            }*/
            if (Keyboard.IsPressed(Key.R))
            {
                sbyte0 &= ~0x02;
            }
            else
            {
                sbyte0 |= 0x02;
            }
            /*if (Keyboard.IsPressed(Key.T))
            {
                sbyte0 &= ~0x02;
            }
            else
            {
                sbyte0 |= 0x02;
            }*/
        }
        public static void loop_inputports_taito_tokio()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                sbyte0 |= 0x04;
            }
            else
            {
                sbyte0 &= ~0x04;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                sbyte0 |= 0x08;
            }
            else
            {
                sbyte0 &= ~0x08;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                sbyte1 &= ~0x40;
            }
            else
            {
                sbyte1 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                sbyte2 &= ~0x40;
            }
            else
            {
                sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                sbyte1 &= ~0x02;
            }
            else
            {
                sbyte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                sbyte1 &= ~0x01;
            }
            else
            {
                sbyte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                sbyte1 &= ~0x04;
            }
            else
            {
                sbyte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                sbyte1 &= ~0x08;
            }
            else
            {
                sbyte1 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                sbyte1 &= ~0x20;
            }
            else
            {
                sbyte1 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                sbyte1 &= ~0x10;
            }
            else
            {
                sbyte1 |= 0x10;
            }
            /*if (Keyboard.IsPressed(Key.L))
            {
                sbyte1 &= ~0x04;
            }
            else
            {
                sbyte1 |= 0x04;
            }*/
            if (Keyboard.IsPressed(Key.Right))
            {
                sbyte2 &= ~0x02;
            }
            else
            {
                sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                sbyte2 &= ~0x01;
            }
            else
            {
                sbyte2 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                sbyte2 &= ~0x04;
            }
            else
            {
                sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                sbyte2 &= ~0x08;
            }
            else
            {
                sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                sbyte2 &= ~0x20;
            }
            else
            {
                sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                sbyte2 &= ~0x10;
            }
            else
            {
                sbyte2 |= 0x10;
            }
            /*if (Keyboard.IsPressed(Key.NumPad3))
            {
                sbyte1 &= ~0x40;
            }
            else
            {
                sbyte1 |= 0x40;
            }*/
            if (Keyboard.IsPressed(Key.R))
            {
                sbyte0 &= ~0x02;
            }
            else
            {
                sbyte0 |= 0x02;
            }
            /*if (Keyboard.IsPressed(Key.T))
            {
                sbyte0 &= ~0x02;
            }
            else
            {
                sbyte0 |= 0x02;
            }*/
        }
        public static void loop_inputports_taito_boblbobl()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                sbyte0 |= 0x08;
            }
            else
            {
                sbyte0 &= ~0x08;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                sbyte0 |= 0x04;
            }
            else
            {
                sbyte0 &= ~0x04;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                sbyte0 &= ~0x40;
            }
            else
            {
                sbyte0 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                sbyte1 &= ~0x40;
            }
            else
            {
                sbyte1 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                sbyte0 &= ~0x02;
            }
            else
            {
                sbyte0 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                sbyte0 &= ~0x01;
            }
            else
            {
                sbyte0 |= 0x01;
            }
            /*if (Keyboard.IsPressed(Key.S))
            {
                sbyte1 &= ~0x04;
            }
            else
            {
                sbyte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                sbyte1 &= ~0x08;
            }
            else
            {
                sbyte1 |= 0x08;
            }*/
            if (Keyboard.IsPressed(Key.J))
            {
                sbyte0 &= ~0x20;
            }
            else
            {
                sbyte0 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                sbyte0 &= ~0x10;
            }
            else
            {
                sbyte0 |= 0x10;
            }
            /*if (Keyboard.IsPressed(Key.L))
            {
                sbyte1 &= ~0x04;
            }
            else
            {
                sbyte1 |= 0x04;
            }*/
            if (Keyboard.IsPressed(Key.Right))
            {
                sbyte1 &= ~0x02;
            }
            else
            {
                sbyte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                sbyte1 &= ~0x01;
            }
            else
            {
                sbyte1 |= 0x01;
            }
            /*if (Keyboard.IsPressed(Key.Down))
            {
                sbyte2 &= ~0x04;
            }
            else
            {
                sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                sbyte2 &= ~0x08;
            }
            else
            {
                sbyte2 |= 0x08;
            }*/
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
                sbyte1 &= ~0x10;
            }
            else
            {
                sbyte1 |= 0x10;
            }
            /*if (Keyboard.IsPressed(Key.NumPad3))
            {
                sbyte1 &= ~0x40;
            }
            else
            {
                sbyte1 |= 0x40;
            }*/
            if (Keyboard.IsPressed(Key.R))
            {
                sbyte1 &= ~0x08;
            }
            else
            {
                sbyte1 |= 0x08;
            }
            /*if (Keyboard.IsPressed(Key.T))
            {
                sbyte0 &= ~0x02;
            }
            else
            {
                sbyte0 |= 0x02;
            }*/
        }
        public static void loop_inputports_taito_opwolf()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                sbyte0 |= 0x01;
            }
            else
            {
                sbyte0 &= ~0x01;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                sbyte0 |= 0x02;
            }
            else
            {
                sbyte0 &= ~0x02;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                sbyte1 &= ~0x10;
            }
            else
            {
                sbyte1 |= 0x10;
            }
            /*if (Keyboard.IsPressed(Key.D2))
            {
                sbyte2 &= ~0x40;
            }
            else
            {
                sbyte2 |= 0x40;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                sbyte1 &= ~0x02;
            }
            else
            {
                sbyte1 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                sbyte1 &= ~0x01;
            }
            else
            {
                sbyte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                sbyte2 &= ~0x02;
            }
            else
            {
                sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                sbyte2 &= ~0x01;
            }
            else
            {
                sbyte2 |= 0x01;
            }*/
            if (Keyboard.IsPressed(Key.J)|| Mouse.buttons[0] != 0)
            {
                sbyte1 &= ~0x01;
            }
            else
            {
                sbyte1 |= 0x01;
            }
            if (Keyboard.IsPressed(Key.K)|| Mouse.buttons[1] != 0)
            {
                sbyte1 &= ~0x02;
            }
            else
            {
                sbyte1 |= 0x02;
            }
            /*if (Keyboard.IsPressed(Key.L))
            {
                sbyte1 &= ~0x04;
            }
            else
            {
                sbyte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                sbyte2 &= ~0x02;
            }
            else
            {
                sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                sbyte2 &= ~0x01;
            }
            else
            {
                sbyte2 |= 0x01;
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
                sbyte2 &= ~0x10;
            }
            else
            {
                sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                sbyte2 &= ~0x20;
            }
            else
            {
                sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                sbyte2 &= ~0x10;
            }
            else
            {
                sbyte2 |= 0x10;
            }
            if (Keyboard.IsPressed(Key.NumPad3))
            {
                sbyte1 &= ~0x40;
            }
            else
            {
                sbyte1 |= 0x40;
            }*/
            if (Keyboard.IsPressed(Key.R))
            {
                sbyte1 &= ~0x04;
            }
            else
            {
                sbyte1 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                sbyte1 &= ~0x08;
            }
            else
            {
                sbyte1 |= 0x08;
            }
            Inptport.frame_update_analog_field_opwolf_p1x(Inptport.analog_p1x);
            Inptport.frame_update_analog_field_opwolf_p1y(Inptport.analog_p1y);
            p1x = (byte)Inptport.input_port_read_direct(Inptport.analog_p1x);
            p1y = (byte)Inptport.input_port_read_direct(Inptport.analog_p1y);
        }
        public static void loop_inputports_taito_opwolfp()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                sbyte3 |= 0x02;
            }
            else
            {
                sbyte3 &= ~0x02;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                sbyte3 |= 0x04;
            }
            else
            {
                sbyte3 &= ~0x04;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                sbyte2 &= ~0x20;
            }
            else
            {
                sbyte2 |= 0x20;
            }
            if (Keyboard.IsPressed(Key.J) || Mouse.buttons[0] != 0)
            {
                sbyte2 &= ~0x02;
            }
            else
            {
                sbyte2 |= 0x02;
            }
            if (Keyboard.IsPressed(Key.K) || Mouse.buttons[1] != 0)
            {
                sbyte2 &= ~0x04;
            }
            else
            {
                sbyte2 |= 0x04;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                sbyte2 &= ~0x08;
            }
            else
            {
                sbyte2 |= 0x08;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                sbyte2 &= ~0x10;
            }
            else
            {
                sbyte2 |= 0x10;
            }
            Inptport.frame_update_analog_field_opwolf_p1x(Inptport.analog_p1x);
            Inptport.frame_update_analog_field_opwolf_p1y(Inptport.analog_p1y);
            p1x = (byte)Inptport.input_port_read_direct(Inptport.analog_p1x);
            p1y = (byte)Inptport.input_port_read_direct(Inptport.analog_p1y);
        }
        public static void record_port_bublbobl()
        {
            if (sbyte0 != sbyte0_old || sbyte1 != sbyte1_old || sbyte2 != sbyte2_old)
            {
                sbyte0_old = sbyte0;
                sbyte1_old = sbyte1;
                sbyte2_old = sbyte2;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(sbyte0);
                Mame.bwRecord.Write(sbyte1);
                Mame.bwRecord.Write(sbyte2);
            }
        }
        public static void replay_port_bublbobl()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    sbyte0_old = Mame.brRecord.ReadSByte();
                    sbyte1_old = Mame.brRecord.ReadSByte();
                    sbyte2_old = Mame.brRecord.ReadSByte();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                    //Mame.mame_pause(true);
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                sbyte0 = sbyte0_old;
                sbyte1 = sbyte1_old;
                sbyte2 = sbyte2_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
        public static void record_port_opwolf()
        {
            if (sbyte0 != sbyte0_old || sbyte1 != sbyte1_old || Inptport.analog_p1x.accum != p1x_accum_old || Inptport.analog_p1x.previous != p1x_previous_old || Inptport.analog_p1y.accum != p1y_accum_old || Inptport.analog_p1y.previous != p1y_previous_old)
            {
                sbyte0_old = sbyte0;
                sbyte1_old = sbyte1;
                p1x_accum_old = Inptport.analog_p1x.accum;
                p1x_previous_old = Inptport.analog_p1x.previous;
                p1y_accum_old = Inptport.analog_p1y.accum;
                p1y_previous_old = Inptport.analog_p1y.previous;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(sbyte0);
                Mame.bwRecord.Write(sbyte1);
                Mame.bwRecord.Write(Inptport.analog_p1x.accum);
                Mame.bwRecord.Write(Inptport.analog_p1x.previous);
                Mame.bwRecord.Write(Inptport.analog_p1y.accum);
                Mame.bwRecord.Write(Inptport.analog_p1y.previous);
            }
        }
        public static void replay_port_opwolf()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    sbyte0_old = Mame.brRecord.ReadSByte();
                    sbyte1_old = Mame.brRecord.ReadSByte();
                    p1x_accum_old = Mame.brRecord.ReadInt32();
                    p1x_previous_old = Mame.brRecord.ReadInt32();
                    p1y_accum_old = Mame.brRecord.ReadInt32();
                    p1y_previous_old = Mame.brRecord.ReadInt32();
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
                Inptport.analog_p1x.accum = p1x_accum_old;
                Inptport.analog_p1x.previous = p1x_previous_old;
                Inptport.analog_p1y.accum = p1y_accum_old;
                Inptport.analog_p1y.previous = p1y_previous_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
        public static void record_port_opwolfp()
        {
            if (sbyte2 != sbyte2_old || sbyte3 != sbyte3_old || Inptport.analog_p1x.accum != p1x_accum_old || Inptport.analog_p1x.previous != p1x_previous_old || Inptport.analog_p1y.accum != p1y_accum_old || Inptport.analog_p1y.previous != p1y_previous_old)
            {
                sbyte2_old = sbyte2;
                sbyte3_old = sbyte3;
                p1x_accum_old = Inptport.analog_p1x.accum;
                p1x_previous_old = Inptport.analog_p1x.previous;
                p1y_accum_old = Inptport.analog_p1y.accum;
                p1y_previous_old = Inptport.analog_p1y.previous;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(sbyte2);
                Mame.bwRecord.Write(sbyte3);
                Mame.bwRecord.Write(Inptport.analog_p1x.accum);
                Mame.bwRecord.Write(Inptport.analog_p1x.previous);
                Mame.bwRecord.Write(Inptport.analog_p1y.accum);
                Mame.bwRecord.Write(Inptport.analog_p1y.previous);
            }
        }
        public static void replay_port_opwolfp()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    sbyte2_old = Mame.brRecord.ReadSByte();
                    sbyte3_old = Mame.brRecord.ReadSByte();
                    p1x_accum_old = Mame.brRecord.ReadInt32();
                    p1x_previous_old = Mame.brRecord.ReadInt32();
                    p1y_accum_old = Mame.brRecord.ReadInt32();
                    p1y_previous_old = Mame.brRecord.ReadInt32();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                sbyte2 = sbyte2_old;
                sbyte3 = sbyte3_old;
                Inptport.analog_p1x.accum = p1x_accum_old;
                Inptport.analog_p1x.previous = p1x_previous_old;
                Inptport.analog_p1y.accum = p1y_accum_old;
                Inptport.analog_p1y.previous = p1y_previous_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
    }
}
