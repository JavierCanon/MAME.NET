﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class M72
    {
        public static void loop_inputports_m72_common()
        {
            if (Keyboard.IsPressed(Key.D5))
            {
                ushort1 &= unchecked((ushort)~0x0004);
            }
            else
            {
                ushort1 |= 0x0004;
            }
            if (Keyboard.IsPressed(Key.D6))
            {
                ushort1 &= unchecked((ushort)~0x0008);
            }
            else
            {
                ushort1 |= 0x0008;
            }
            if (Keyboard.IsPressed(Key.D1))
            {
                ushort1 &= unchecked((ushort)~0x0001);
            }
            else
            {
                ushort1 |= 0x0001;
            }
            if (Keyboard.IsPressed(Key.D2))
            {
                ushort1 &= unchecked((ushort)~0x0002);
            }
            else
            {
                ushort1 |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.D))
            {
                ushort0 &= unchecked((ushort)~0x0001);
            }
            else
            {
                ushort0 |= 0x0001;
            }
            if (Keyboard.IsPressed(Key.A))
            {
                ushort0 &= unchecked((ushort)~0x0002);
            }
            else
            {
                ushort0 |= 0x0002;
            }
            if (Keyboard.IsPressed(Key.S))
            {
                ushort0 &= unchecked((ushort)~0x0004);
            }
            else
            {
                ushort0 |= 0x0004;
            }
            if (Keyboard.IsPressed(Key.W))
            {
                ushort0 &= unchecked((ushort)~0x0008);
            }
            else
            {
                ushort0 |= 0x0008;
            }
            if (Keyboard.IsPressed(Key.J))
            {
                ushort0 &= unchecked((ushort)~0x0080);
            }
            else
            {
                ushort0 |= 0x0080;
            }
            if (Keyboard.IsPressed(Key.K))
            {
                ushort0 &= unchecked((ushort)~0x0040);
            }
            else
            {
                ushort0 |= 0x0040;
            }
            if (Keyboard.IsPressed(Key.U))
            {
                ushort0 &= unchecked((ushort)~0x0020);
            }
            else
            {
                ushort0 |= 0x0020;
            }
            if (Keyboard.IsPressed(Key.I))
            {
                ushort0 &= unchecked((ushort)~0x0010);
            }
            else
            {
                ushort0 |= 0x0010;
            }
            if (Keyboard.IsPressed(Key.Right))
            {
                ushort0 &= unchecked((ushort)~0x0100);
            }
            else
            {
                ushort0 |= 0x0100;
            }
            if (Keyboard.IsPressed(Key.Left))
            {
                ushort0 &= unchecked((ushort)~0x0200);
            }
            else
            {
                ushort0 |= 0x0200;
            }
            if (Keyboard.IsPressed(Key.Down))
            {
                ushort0 &= unchecked((ushort)~0x0400);
            }
            else
            {
                ushort0 |= 0x0400;
            }
            if (Keyboard.IsPressed(Key.Up))
            {
                ushort0 &= unchecked((ushort)~0x0800);
            }
            else
            {
                ushort0 |= 0x0800;
            }
            if (Keyboard.IsPressed(Key.NumPad1))
            {
                ushort0 &= unchecked((ushort)~0x8000);
            }
            else
            {
                ushort0 |= 0x8000;
            }
            if (Keyboard.IsPressed(Key.NumPad2))
            {
                ushort0 &= unchecked((ushort)~0x4000);
            }
            else
            {
                ushort0 |= 0x4000;
            }
            if (Keyboard.IsPressed(Key.NumPad4))
            {
                ushort0 &= unchecked((ushort)~0x2000);
            }
            else
            {
                ushort0 |= 0x2000;
            }
            if (Keyboard.IsPressed(Key.NumPad5))
            {
                ushort0 &= unchecked((ushort)~0x1000);
            }
            else
            {
                ushort0 |= 0x1000;
            }
            if (Keyboard.IsPressed(Key.R))
            {
                ushort1 &= unchecked((ushort)~0x0010);
            }
            else
            {
                ushort1 |= 0x0010;
            }
            if (Keyboard.IsPressed(Key.T))
            {
                ushort1 &= unchecked((ushort)~0x0020);
            }
            else
            {
                ushort1 |= 0x0020;
            }
        }
        public static void record_port()
        {
            if (ushort0 != ushort0_old || ushort1 != ushort1_old)
            {
                ushort0_old = ushort0;
                ushort1_old = ushort1;
                Mame.bwRecord.Write(Video.screenstate.frame_number);
                Mame.bwRecord.Write(ushort0);
                Mame.bwRecord.Write(ushort1);
            }
        }
        public static void replay_port()
        {
            if (Inptport.bReplayRead)
            {
                try
                {
                    Video.frame_number_obj = Mame.brRecord.ReadInt64();
                    ushort0_old = Mame.brRecord.ReadUInt16();
                    ushort1_old = Mame.brRecord.ReadUInt16();
                }
                catch
                {
                    Mame.playState = Mame.PlayState.PLAY_REPLAYEND;
                }
                Inptport.bReplayRead = false;
            }
            if (Video.screenstate.frame_number == Video.frame_number_obj)
            {
                ushort0 = ushort0_old;
                ushort1 = ushort1_old;
                Inptport.bReplayRead = true;
            }
            else
            {
                Inptport.bReplayRead = false;
            }
        }
    }
}
