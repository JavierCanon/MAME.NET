using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.m68000;

namespace mame
{
    public partial class IGS011
    {
        public static void SaveStateBinary(BinaryWriter writer)
        {
            int i, j;
            for (i = 0; i < 0x800; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Generic.generic_nvram, 0, 0x4000);
            for (i = 0; i < 0x800; i++)
            {
                writer.Write(priority_ram[i]);
            }
            for (i = 0; i < 0x1000; i++)
            {
                writer.Write(paletteram16[i]);
            }
            writer.Write(prot1);
            writer.Write(prot2);
            writer.Write(prot1_swap);
            writer.Write(prot1_addr);
            for (i = 0; i < 2; i++)
            {
                writer.Write(igs003_reg[i]);
            }
            for (i = 0; i < 2; i++)
            {
                writer.Write(vbowl_trackball[i]);
            }
            writer.Write(priority);
            writer.Write(igs_dips_sel);
            writer.Write(igs_input_sel);
            writer.Write(lhb_irq_enable);
            writer.Write(igs012_prot);
            writer.Write(igs012_prot_swap);
            writer.Write(igs012_prot_mode);
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 0x20000; j++)
                {
                    writer.Write(layer[i][j]);
                }
            }
            writer.Write(lhb2_pen_hi);
            writer.Write(blitter.x);
            writer.Write(blitter.y);
            writer.Write(blitter.w);
            writer.Write(blitter.h);
            writer.Write(blitter.gfx_lo);
            writer.Write(blitter.gfx_hi);
            writer.Write(blitter.depth);
            writer.Write(blitter.pen);
            writer.Write(blitter.flags);
            MC68000.m1.SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            writer.Write(Video.screenstate.frame_number);
            writer.Write(Sound.last_update_second);
            writer.Write(Cpuexec.cpu[0].localtime.seconds);
            writer.Write(Cpuexec.cpu[0].localtime.attoseconds);
            Timer.SaveStateBinary(writer);
            OKI6295.SaveStateBinary(writer);
            for (i = 0; i < 9; i++)
            {
                writer.Write(FMOpl.OPL.P_CH[i].block_fnum);
                writer.Write(FMOpl.OPL.P_CH[i].kcode);
                for (j = 0; j < 2; j++)
                {
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].ar);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].dr);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].rr);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].KSR);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].ksl);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].ksr);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].mul);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].Cnt);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].FB);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].op1_out[0]);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].op1_out[1]);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].CON);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].eg_type);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].state);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].TL);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].volume);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].sl);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].key);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].AMmask);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].vib);
                    writer.Write(FMOpl.OPL.P_CH[i].SLOT[j].wavetable);
                }
            }
            writer.Write(FMOpl.OPL.eg_cnt);
            writer.Write(FMOpl.OPL.eg_timer);
            writer.Write(FMOpl.OPL.rhythm);
            writer.Write(FMOpl.OPL.lfo_am_depth);
            writer.Write(FMOpl.OPL.lfo_pm_depth_range);
            writer.Write(FMOpl.OPL.lfo_am_cnt);
            writer.Write(FMOpl.OPL.lfo_pm_cnt);
            writer.Write(FMOpl.OPL.noise_rng);
            writer.Write(FMOpl.OPL.noise_p);
            writer.Write(FMOpl.OPL.wavesel);
            for (i = 0; i < 2; i++)
            {
                writer.Write(FMOpl.OPL.T[i]);
            }
            for (i = 0; i < 2; i++)
            {
                writer.Write(FMOpl.OPL.st[i]);
            }
            writer.Write(FMOpl.OPL.address);
            writer.Write(FMOpl.OPL.status);
            writer.Write(FMOpl.OPL.statusmask);
            writer.Write(FMOpl.OPL.mode);
            writer.Write(Sound.okistream.output_sampindex);
            writer.Write(Sound.okistream.output_base_sampindex);
            writer.Write(Sound.ym3812stream.output_sampindex);
            writer.Write(Sound.ym3812stream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary(BinaryReader reader)
        {
            int i, j;
            for (i = 0; i < 0x800; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Generic.generic_nvram = reader.ReadBytes(0x4000);
            for (i = 0; i < 0x800; i++)
            {
                priority_ram[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x1000; i++)
            {
                paletteram16[i] = reader.ReadUInt16();
            }
            prot1 = reader.ReadByte();
            prot2 = reader.ReadByte();
            prot1_swap = reader.ReadByte();
            prot1_addr = reader.ReadUInt32();
            for (i = 0; i < 2; i++)
            {
                igs003_reg[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 2; i++)
            {
                vbowl_trackball[i] = reader.ReadUInt16();
            }
            priority = reader.ReadUInt16();
            igs_dips_sel = reader.ReadUInt16();
            igs_input_sel = reader.ReadUInt16();
            lhb_irq_enable = reader.ReadUInt16();
            igs012_prot = reader.ReadByte();
            igs012_prot_swap = reader.ReadByte();
            igs012_prot_mode = reader.ReadBoolean();
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 0x20000; j++)
                {
                    layer[i][j] = reader.ReadByte();
                }
            }
            lhb2_pen_hi= reader.ReadByte();
            blitter.x= reader.ReadUInt16();
            blitter.y= reader.ReadUInt16();
            blitter.w= reader.ReadUInt16();
            blitter.h= reader.ReadUInt16();
            blitter.gfx_lo= reader.ReadUInt16();
            blitter.gfx_hi= reader.ReadUInt16();
            blitter.depth= reader.ReadUInt16();
            blitter.pen= reader.ReadUInt16();
            blitter.flags = reader.ReadUInt16();
            MC68000.m1.LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.screenstate.frame_number = reader.ReadInt64();
            Sound.last_update_second = reader.ReadInt32();
            Cpuexec.cpu[0].localtime.seconds = reader.ReadInt32();
            Cpuexec.cpu[0].localtime.attoseconds = reader.ReadInt64();
            Timer.LoadStateBinary(reader);
            OKI6295.LoadStateBinary(reader);
            for (i = 0; i < 9; i++)
            {
                FMOpl.OPL.P_CH[i].block_fnum = reader.ReadUInt32();
                FMOpl.OPL.P_CH[i].kcode = reader.ReadByte();
                for (j = 0; j < 2; j++)
                {
                    FMOpl.OPL.P_CH[i].SLOT[j].ar = reader.ReadUInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].dr = reader.ReadUInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].rr = reader.ReadUInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].KSR = reader.ReadByte();
                    FMOpl.OPL.P_CH[i].SLOT[j].ksl = reader.ReadByte();
                    FMOpl.OPL.P_CH[i].SLOT[j].ksr = reader.ReadByte();
                    FMOpl.OPL.P_CH[i].SLOT[j].mul = reader.ReadByte();
                    FMOpl.OPL.P_CH[i].SLOT[j].Cnt = reader.ReadUInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].FB = reader.ReadByte();
                    FMOpl.OPL.P_CH[i].SLOT[j].op1_out[0] = reader.ReadInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].op1_out[1] = reader.ReadInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].CON = reader.ReadByte();
                    FMOpl.OPL.P_CH[i].SLOT[j].eg_type = reader.ReadByte();
                    FMOpl.OPL.P_CH[i].SLOT[j].state = reader.ReadByte();
                    FMOpl.OPL.P_CH[i].SLOT[j].TL = reader.ReadUInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].volume = reader.ReadInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].sl = reader.ReadUInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].key = reader.ReadUInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].AMmask = reader.ReadUInt32();
                    FMOpl.OPL.P_CH[i].SLOT[j].vib = reader.ReadByte();
                    FMOpl.OPL.P_CH[i].SLOT[j].wavetable = reader.ReadUInt16();
                }
            }
            FMOpl.OPL.eg_cnt = reader.ReadUInt32();
            FMOpl.OPL.eg_timer = reader.ReadUInt32();
            FMOpl.OPL.rhythm = reader.ReadByte();
            FMOpl.OPL.lfo_am_depth = reader.ReadByte();
            FMOpl.OPL.lfo_pm_depth_range = reader.ReadByte();
            FMOpl.OPL.lfo_am_cnt = reader.ReadUInt32();
            FMOpl.OPL.lfo_pm_cnt = reader.ReadUInt32();
            FMOpl.OPL.noise_rng = reader.ReadUInt32();
            FMOpl.OPL.noise_p = reader.ReadUInt32();
            FMOpl.OPL.wavesel = reader.ReadByte();
            for (i = 0; i < 2; i++)
            {
                FMOpl.OPL.T[i] = reader.ReadUInt32();
            }
            for (i = 0; i < 2; i++)
            {
                FMOpl.OPL.st[i] = reader.ReadByte();
            }
            FMOpl.OPL.address = reader.ReadByte();
            FMOpl.OPL.status = reader.ReadByte();
            FMOpl.OPL.statusmask = reader.ReadByte();
            FMOpl.OPL.mode = reader.ReadByte();
            Sound.okistream.output_sampindex = reader.ReadInt32();
            Sound.okistream.output_base_sampindex = reader.ReadInt32();
            Sound.ym3812stream.output_sampindex = reader.ReadInt32();
            Sound.ym3812stream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
    }
}
