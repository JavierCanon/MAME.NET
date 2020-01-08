using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.m68000;
using cpu.z80;

namespace mame
{
    public partial class PGM
    {
        public static void SaveStateBinary(BinaryWriter writer)
        {
            int i, j;
            writer.Write(pgm_tx_videoram, 0, 0x2000);
            writer.Write(pgm_bg_videoram, 0, 0x4000);
            writer.Write(pgm_rowscrollram, 0, 0x800);
            writer.Write(pgm_videoregs, 0, 0x10000);
            writer.Write(CalVal);
            writer.Write(CalMask);
            writer.Write(CalCom);
            writer.Write(CalCnt);
            writer.Write(asic3_reg);
            writer.Write(asic3_x);
            for(i=0;i<3;i++)
            {
                writer.Write(asic3_latch[i]);
            }
            writer.Write(asic3_hold);
            writer.Write(asic3_hilo);
            for (i = 0; i < 0x900; i++)
            {
                writer.Write(Generic.paletteram16[i]);
            }
            for (i = 0; i < 0x901; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x20000);
            MC68000.m1.SaveStateBinary(writer);
            writer.Write(Memory.audioram, 0, 0x10000);
            Z80A.z1.SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            writer.Write(Video.screenstate.vblank_start_time.seconds);
            writer.Write(Video.screenstate.vblank_start_time.attoseconds);
            writer.Write(Video.screenstate.frame_number);
            writer.Write(Sound.last_update_second);
            for (i = 0; i < 2; i++)
            {
                writer.Write(Cpuexec.cpu[i].suspend);
                writer.Write(Cpuexec.cpu[i].nextsuspend);
                writer.Write(Cpuexec.cpu[i].eatcycles);
                writer.Write(Cpuexec.cpu[i].nexteatcycles);
                writer.Write(Cpuexec.cpu[i].localtime.seconds);
                writer.Write(Cpuexec.cpu[i].localtime.attoseconds);
            }
            Timer.SaveStateBinary(writer);
            ICS2115.SaveStateBinary(writer);
            for (i = 0; i < 3; i++)
            {
                writer.Write(Sound.latched_value[i]);
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(Sound.utempdata[i]);
            }
            writer.Write(Sound.ics2115stream.output_sampindex);
            writer.Write(Sound.ics2115stream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary(BinaryReader reader)
        {
            int i, j;
            pgm_tx_videoram = reader.ReadBytes(0x2000);
            pgm_bg_videoram = reader.ReadBytes(0x4000);
            pgm_rowscrollram = reader.ReadBytes(0x800);
            pgm_videoregs = reader.ReadBytes(0x10000);
            CalVal = reader.ReadByte();
            CalMask = reader.ReadByte();
            CalCom = reader.ReadByte();
            CalCnt = reader.ReadByte();
            asic3_reg = reader.ReadByte();
            asic3_x = reader.ReadByte();
            for (i = 0; i < 3; i++)
            {
                asic3_latch[i] = reader.ReadByte();
            }
            asic3_hold = reader.ReadUInt16();
            asic3_hilo = reader.ReadUInt16();
            for (i = 0; i < 0x900; i++)
            {
                Generic.paletteram16[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x901; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x20000);
            MC68000.m1.LoadStateBinary(reader);
            Memory.audioram = reader.ReadBytes(0x10000);
            Z80A.z1.LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.screenstate.vblank_start_time.seconds = reader.ReadInt32();
            Video.screenstate.vblank_start_time.attoseconds = reader.ReadInt64();
            Video.screenstate.frame_number = reader.ReadInt64();
            Sound.last_update_second = reader.ReadInt32();
            for (i = 0; i < 2; i++)
            {
                Cpuexec.cpu[i].suspend = reader.ReadByte();
                Cpuexec.cpu[i].nextsuspend = reader.ReadByte();
                Cpuexec.cpu[i].eatcycles = reader.ReadByte();
                Cpuexec.cpu[i].nexteatcycles = reader.ReadByte();
                Cpuexec.cpu[i].localtime.seconds = reader.ReadInt32();
                Cpuexec.cpu[i].localtime.attoseconds = reader.ReadInt64();
            }
            Timer.LoadStateBinary(reader);
            ICS2115.LoadStateBinary(reader);
            for (i = 0; i < 3; i++)
            {
                Sound.latched_value[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                Sound.utempdata[i] = reader.ReadUInt16();
            }
            Sound.ics2115stream.output_sampindex = reader.ReadInt32();
            Sound.ics2115stream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
    }
}
