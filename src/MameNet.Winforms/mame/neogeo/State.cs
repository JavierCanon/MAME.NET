using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.m68000;
using cpu.z80;

namespace mame
{
    public partial class Neogeo
    {
        public static void SaveStateBinary(BinaryWriter writer)
        {
            int i, j;
            writer.Write(dsw);
            writer.Write(display_position_interrupt_control);
            writer.Write(display_counter);
            writer.Write(vblank_interrupt_pending);
            writer.Write(display_position_interrupt_pending);
            writer.Write(irq3_pending);
            writer.Write(controller_select);
            writer.Write(main_cpu_bank_address);
            writer.Write(main_cpu_vector_table_source);
            writer.Write(audio_cpu_banks, 0, 4);
            writer.Write(save_ram_unlocked);
            writer.Write(audio_cpu_nmi_enabled);
            writer.Write(audio_cpu_nmi_pending);
            writer.Write(mainram2, 0, 0x10000);
            writer.Write(pvc_cartridge_ram, 0, 0x2000);
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 0x1000; j++)
                {
                    writer.Write(palettes[i, j]);
                }
            }
            for (i = 0; i < 0x10000; i++)
            {
                writer.Write(neogeo_videoram[i]);
            }
            writer.Write(videoram_read_buffer);
            writer.Write(videoram_modulo);
            writer.Write(videoram_offset);
            writer.Write(fixed_layer_source);
            writer.Write(screen_dark);
            writer.Write(palette_bank);
            writer.Write(auto_animation_speed);
            writer.Write(auto_animation_disabled);
            writer.Write(auto_animation_counter);
            writer.Write(auto_animation_frame_counter);
            writer.Write(Memory.mainram, 0, 0x10000);
            MC68000.m1.SaveStateBinary(writer);
            writer.Write(Memory.audioram, 0, 0x800);
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
            writer.Write(AY8910.ay8910info.register_latch);
            writer.Write(AY8910.ay8910info.regs, 0, 16);
            for (i = 0; i < 3; i++)
            {
                writer.Write(AY8910.ay8910info.count[i]);
            }
            writer.Write(AY8910.ay8910info.output, 0, 3);
            writer.Write(AY8910.ay8910info.output_noise);
            writer.Write(AY8910.ay8910info.count_noise);
            writer.Write(AY8910.ay8910info.count_env);
            writer.Write(AY8910.ay8910info.env_step);
            writer.Write(AY8910.ay8910info.env_volume);
            writer.Write(AY8910.ay8910info.hold);
            writer.Write(AY8910.ay8910info.alternate);
            writer.Write(AY8910.ay8910info.attack);
            writer.Write(AY8910.ay8910info.holding);
            writer.Write(AY8910.ay8910info.rng);
            writer.Write(AY8910.ay8910info.vol_enabled, 0, 3);
            writer.Write(FM.F2610.REGS, 0, 512);
            writer.Write(FM.F2610.addr_A1);
            writer.Write(FM.F2610.adpcmTL);
            writer.Write(FM.F2610.adpcmreg, 0, 0x30);
            writer.Write(FM.F2610.adpcm_arrivedEndAddress);
            writer.Write(FM.ST.freqbase);
            writer.Write(FM.ST.timer_prescaler);
            writer.Write(FM.ST.busy_expiry_time.seconds);
            writer.Write(FM.ST.busy_expiry_time.attoseconds);
            writer.Write(FM.ST.address);
            writer.Write(FM.ST.irq);
            writer.Write(FM.ST.irqmask);
            writer.Write(FM.ST.status);
            writer.Write(FM.ST.mode);
            writer.Write(FM.ST.prescaler_sel);
            writer.Write(FM.ST.fn_h);
            writer.Write(FM.ST.TA);
            writer.Write(FM.ST.TAC);
            writer.Write(FM.ST.TB);
            writer.Write(FM.ST.TBC);
            for (i = 0; i < 12; i++)
            {
                writer.Write(FM.OPN.pan[i]);
            }
            writer.Write(FM.OPN.eg_cnt);
            writer.Write(FM.OPN.eg_timer);
            writer.Write(FM.OPN.eg_timer_add);
            writer.Write(FM.OPN.eg_timer_overflow);
            writer.Write(FM.OPN.lfo_cnt);
            writer.Write(FM.OPN.lfo_inc);
            for (i = 0; i < 8; i++)
            {
                writer.Write(FM.OPN.lfo_freq[i]);
            }
            for (i = 0; i < 6; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    writer.Write(FM.SLOT[i, j].KSR);
                    writer.Write(FM.SLOT[i, j].ar);
                    writer.Write(FM.SLOT[i, j].d1r);
                    writer.Write(FM.SLOT[i, j].d2r);
                    writer.Write(FM.SLOT[i, j].rr);
                    writer.Write(FM.SLOT[i, j].ksr);
                    writer.Write(FM.SLOT[i, j].mul);
                    writer.Write(FM.SLOT[i, j].phase);
                    writer.Write(FM.SLOT[i, j].Incr);
                    writer.Write(FM.SLOT[i, j].state);
                    writer.Write(FM.SLOT[i, j].tl);
                    writer.Write(FM.SLOT[i, j].volume);
                    writer.Write(FM.SLOT[i, j].sl);
                    writer.Write(FM.SLOT[i, j].vol_out);
                    writer.Write(FM.SLOT[i, j].eg_sh_ar);
                    writer.Write(FM.SLOT[i, j].eg_sel_ar);
                    writer.Write(FM.SLOT[i, j].eg_sh_d1r);
                    writer.Write(FM.SLOT[i, j].eg_sel_d1r);
                    writer.Write(FM.SLOT[i, j].eg_sh_d2r);
                    writer.Write(FM.SLOT[i, j].eg_sel_d2r);
                    writer.Write(FM.SLOT[i, j].eg_sh_rr);
                    writer.Write(FM.SLOT[i, j].eg_sel_rr);
                    writer.Write(FM.SLOT[i, j].ssg);
                    writer.Write(FM.SLOT[i, j].ssgn);
                    writer.Write(FM.SLOT[i, j].key);
                    writer.Write(FM.SLOT[i, j].AMmask);
                }
            }
            for (i = 0; i < 6; i++)
            {
                writer.Write(FM.adpcm[i].flag);
                writer.Write(FM.adpcm[i].flagMask);
                writer.Write(FM.adpcm[i].now_data);
                writer.Write(FM.adpcm[i].now_addr);
                writer.Write(FM.adpcm[i].now_step);
                writer.Write(FM.adpcm[i].step);
                writer.Write(FM.adpcm[i].start);
                writer.Write(FM.adpcm[i].end);
                writer.Write(FM.adpcm[i].IL);
                writer.Write(FM.adpcm[i].adpcm_acc);
                writer.Write(FM.adpcm[i].adpcm_step);
                writer.Write(FM.adpcm[i].adpcm_out);
                writer.Write(FM.adpcm[i].vol_mul);
                writer.Write(FM.adpcm[i].vol_shift);
            }
            for (i = 0; i < 6; i++)
            {
                writer.Write(FM.CH[i].ALGO);
                writer.Write(FM.CH[i].FB);
                writer.Write(FM.CH[i].op1_out0);
                writer.Write(FM.CH[i].op1_out1);
                writer.Write(FM.CH[i].mem_value);
                writer.Write(FM.CH[i].pms);
                writer.Write(FM.CH[i].ams);
                writer.Write(FM.CH[i].fc);
                writer.Write(FM.CH[i].kcode);
                writer.Write(FM.CH[i].block_fnum);
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(FM.SL3.fc[i]);
            }
            writer.Write(FM.SL3.fn_h);
            writer.Write(FM.SL3.kcode, 0, 3);
            for (i = 0; i < 3; i++)
            {
                writer.Write(FM.SL3.block_fnum[i]);
            }
            writer.Write(YMDeltat.DELTAT.portstate);
            writer.Write(YMDeltat.DELTAT.now_addr);
            writer.Write(YMDeltat.DELTAT.now_step);
            writer.Write(YMDeltat.DELTAT.acc);
            writer.Write(YMDeltat.DELTAT.prev_acc);
            writer.Write(YMDeltat.DELTAT.adpcmd);
            writer.Write(YMDeltat.DELTAT.adpcml);
            for (i = 0; i < 2; i++)
            {
                writer.Write(Sound.latched_value[i]);
            }
            for (i = 0; i < 2; i++)
            {
                writer.Write(Sound.utempdata[i]);
            }
            writer.Write(Sound.ay8910stream.output_sampindex);
            writer.Write(Sound.ay8910stream.output_base_sampindex);
            writer.Write(Sound.ym2610stream.output_sampindex);
            writer.Write(Sound.ym2610stream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
            writer.Write(Pd4900a.pd4990a.seconds);
            writer.Write(Pd4900a.pd4990a.minutes);
            writer.Write(Pd4900a.pd4990a.hours);
            writer.Write(Pd4900a.pd4990a.days);
            writer.Write(Pd4900a.pd4990a.month);
            writer.Write(Pd4900a.pd4990a.year);
            writer.Write(Pd4900a.pd4990a.weekday);
            writer.Write(Pd4900a.shiftlo);
            writer.Write(Pd4900a.shifthi);
            writer.Write(Pd4900a.retraces);
            writer.Write(Pd4900a.testwaits);
            writer.Write(Pd4900a.maxwaits);
            writer.Write(Pd4900a.testbit);
            writer.Write(Pd4900a.outputbit);
            writer.Write(Pd4900a.bitno);
            writer.Write(Pd4900a.reading);
            writer.Write(Pd4900a.writting);
            writer.Write(Pd4900a.clock_line);
            writer.Write(Pd4900a.command_line);
        }
        public static void LoadStateBinary(BinaryReader reader)
        {
            int i, j;
            dsw = reader.ReadByte();
            display_position_interrupt_control = reader.ReadByte();
            display_counter = reader.ReadUInt32();
            vblank_interrupt_pending = reader.ReadInt32();
            display_position_interrupt_pending = reader.ReadInt32();
            irq3_pending = reader.ReadInt32();
            controller_select = reader.ReadByte();
            main_cpu_bank_address = reader.ReadInt32();
            main_cpu_vector_table_source = reader.ReadByte();
            audio_cpu_banks = reader.ReadBytes(4);
            save_ram_unlocked = reader.ReadByte();
            audio_cpu_nmi_enabled = reader.ReadBoolean();
            audio_cpu_nmi_pending = reader.ReadBoolean();
            mainram2 = reader.ReadBytes(0x10000);
            pvc_cartridge_ram = reader.ReadBytes(0x2000);
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 0x1000; j++)
                {
                    palettes[i, j] = reader.ReadUInt16();
                }
            }
            for (i = 0; i < 0x10000; i++)
            {
                neogeo_videoram[i] = reader.ReadUInt16();
            }
            videoram_read_buffer = reader.ReadUInt16();
            videoram_modulo = reader.ReadUInt16();
            videoram_offset = reader.ReadUInt16();
            fixed_layer_source = reader.ReadByte();
            screen_dark = reader.ReadByte();
            palette_bank = reader.ReadByte();
            auto_animation_speed = reader.ReadByte();
            auto_animation_disabled = reader.ReadByte();
            auto_animation_counter = reader.ReadInt32();
            auto_animation_frame_counter = reader.ReadInt32();
            Memory.mainram = reader.ReadBytes(0x10000);
            MC68000.m1.LoadStateBinary(reader);
            Memory.audioram = reader.ReadBytes(0x800);
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
            AY8910.ay8910info.register_latch = reader.ReadInt32();
            AY8910.ay8910info.regs = reader.ReadBytes(16);
            for (i = 0; i < 3; i++)
            {
                AY8910.ay8910info.count[i] = reader.ReadInt32();
            }
            AY8910.ay8910info.output = reader.ReadBytes(3);
            AY8910.ay8910info.output_noise = reader.ReadByte();
            AY8910.ay8910info.count_noise = reader.ReadInt32();
            AY8910.ay8910info.count_env = reader.ReadInt32();
            AY8910.ay8910info.env_step = reader.ReadSByte();
            AY8910.ay8910info.env_volume = reader.ReadInt32();
            AY8910.ay8910info.hold = reader.ReadByte();
            AY8910.ay8910info.alternate = reader.ReadByte();
            AY8910.ay8910info.attack = reader.ReadByte();
            AY8910.ay8910info.holding = reader.ReadByte();
            AY8910.ay8910info.rng = reader.ReadInt32();
            AY8910.ay8910info.vol_enabled = reader.ReadBytes(3);
            FM.F2610.REGS = reader.ReadBytes(512);
            FM.F2610.addr_A1 = reader.ReadByte();
            FM.F2610.adpcmTL = reader.ReadByte();
            FM.F2610.adpcmreg = reader.ReadBytes(0x30);
            FM.F2610.adpcm_arrivedEndAddress = reader.ReadByte();
            FM.ST.freqbase = reader.ReadDouble();
            FM.ST.timer_prescaler = reader.ReadInt32();
            FM.ST.busy_expiry_time.seconds = reader.ReadInt32();
            FM.ST.busy_expiry_time.attoseconds = reader.ReadInt64();
            FM.ST.address = reader.ReadByte();
            FM.ST.irq = reader.ReadByte();
            FM.ST.irqmask = reader.ReadByte();
            FM.ST.status = reader.ReadByte();
            FM.ST.mode = reader.ReadByte();
            FM.ST.prescaler_sel = reader.ReadByte();
            FM.ST.fn_h = reader.ReadByte();
            FM.ST.TA = reader.ReadInt32();
            FM.ST.TAC = reader.ReadInt32();
            FM.ST.TB = reader.ReadByte();
            FM.ST.TBC = reader.ReadInt32();
            for (i = 0; i < 12; i++)
            {
                FM.OPN.pan[i] = reader.ReadUInt32();
            }
            FM.OPN.eg_cnt = reader.ReadUInt32();
            FM.OPN.eg_timer = reader.ReadUInt32();
            FM.OPN.eg_timer_add = reader.ReadUInt32();
            FM.OPN.eg_timer_overflow = reader.ReadUInt32();
            FM.OPN.lfo_cnt = reader.ReadInt32();
            FM.OPN.lfo_inc = reader.ReadInt32();
            for (i = 0; i < 8; i++)
            {
                FM.OPN.lfo_freq[i] = reader.ReadInt32();
            }
            for (i = 0; i < 6; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    FM.SLOT[i, j].KSR = reader.ReadByte();
                    FM.SLOT[i, j].ar = reader.ReadInt32();
                    FM.SLOT[i, j].d1r = reader.ReadInt32();
                    FM.SLOT[i, j].d2r = reader.ReadInt32();
                    FM.SLOT[i, j].rr = reader.ReadInt32();
                    FM.SLOT[i, j].ksr = reader.ReadByte();
                    FM.SLOT[i, j].mul = reader.ReadInt32();
                    FM.SLOT[i, j].phase = reader.ReadUInt32();
                    FM.SLOT[i, j].Incr = reader.ReadInt32();
                    FM.SLOT[i, j].state = reader.ReadByte();
                    FM.SLOT[i, j].tl = reader.ReadInt32();
                    FM.SLOT[i, j].volume = reader.ReadInt32();
                    FM.SLOT[i, j].sl = reader.ReadInt32();
                    FM.SLOT[i, j].vol_out = reader.ReadUInt32();
                    FM.SLOT[i, j].eg_sh_ar = reader.ReadByte();
                    FM.SLOT[i, j].eg_sel_ar = reader.ReadByte();
                    FM.SLOT[i, j].eg_sh_d1r = reader.ReadByte();
                    FM.SLOT[i, j].eg_sel_d1r = reader.ReadByte();
                    FM.SLOT[i, j].eg_sh_d2r = reader.ReadByte();
                    FM.SLOT[i, j].eg_sel_d2r = reader.ReadByte();
                    FM.SLOT[i, j].eg_sh_rr = reader.ReadByte();
                    FM.SLOT[i, j].eg_sel_rr = reader.ReadByte();
                    FM.SLOT[i, j].ssg = reader.ReadByte();
                    FM.SLOT[i, j].ssgn = reader.ReadByte();
                    FM.SLOT[i, j].key = reader.ReadUInt32();
                    FM.SLOT[i, j].AMmask = reader.ReadUInt32();
                }
            }
            for (i = 0; i < 6; i++)
            {
                FM.adpcm[i].flag = reader.ReadByte();
                FM.adpcm[i].flagMask = reader.ReadByte();
                FM.adpcm[i].now_data = reader.ReadByte();
                FM.adpcm[i].now_addr = reader.ReadUInt32();
                FM.adpcm[i].now_step = reader.ReadUInt32();
                FM.adpcm[i].step = reader.ReadUInt32();
                FM.adpcm[i].start = reader.ReadUInt32();
                FM.adpcm[i].end = reader.ReadUInt32();
                FM.adpcm[i].IL = reader.ReadByte();
                FM.adpcm[i].adpcm_acc = reader.ReadInt32();
                FM.adpcm[i].adpcm_step = reader.ReadInt32();
                FM.adpcm[i].adpcm_out = reader.ReadInt32();
                FM.adpcm[i].vol_mul = reader.ReadSByte();
                FM.adpcm[i].vol_shift = reader.ReadByte();
            }
            for (i = 0; i < 6; i++)
            {
                FM.CH[i].ALGO = reader.ReadByte();
                FM.CH[i].FB = reader.ReadByte();
                FM.CH[i].op1_out0 = reader.ReadInt32();
                FM.CH[i].op1_out1 = reader.ReadInt32();
                FM.CH[i].mem_value = reader.ReadInt32();
                FM.CH[i].pms = reader.ReadInt32();
                FM.CH[i].ams = reader.ReadByte();
                FM.CH[i].fc = reader.ReadUInt32();
                FM.CH[i].kcode = reader.ReadByte();
                FM.CH[i].block_fnum = reader.ReadUInt32();
            }
            for (i = 0; i < 3; i++)
            {
                FM.SL3.fc[i] = reader.ReadUInt32();
            }
            FM.SL3.fn_h = reader.ReadByte();
            FM.SL3.kcode = reader.ReadBytes(3);
            for (i = 0; i < 3; i++)
            {
                FM.SL3.block_fnum[i] = reader.ReadUInt32();
            }
            YMDeltat.DELTAT.portstate = reader.ReadByte();
            YMDeltat.DELTAT.now_addr = reader.ReadInt32();
            YMDeltat.DELTAT.now_step = reader.ReadInt32();
            YMDeltat.DELTAT.acc = reader.ReadInt32();
            YMDeltat.DELTAT.prev_acc = reader.ReadInt32();
            YMDeltat.DELTAT.adpcmd = reader.ReadInt32();
            YMDeltat.DELTAT.adpcml = reader.ReadInt32();
            for (i = 0; i < 2; i++)
            {
                Sound.latched_value[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 2; i++)
            {
                Sound.utempdata[i] = reader.ReadUInt16();
            }
            Sound.ay8910stream.output_sampindex = reader.ReadInt32();
            Sound.ay8910stream.output_base_sampindex = reader.ReadInt32();
            Sound.ym2610stream.output_sampindex = reader.ReadInt32();
            Sound.ym2610stream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
            Pd4900a.pd4990a.seconds = reader.ReadInt32();
            Pd4900a.pd4990a.minutes = reader.ReadInt32();
            Pd4900a.pd4990a.hours = reader.ReadInt32();
            Pd4900a.pd4990a.days = reader.ReadInt32();
            Pd4900a.pd4990a.month = reader.ReadInt32();
            Pd4900a.pd4990a.year = reader.ReadInt32();
            Pd4900a.pd4990a.weekday = reader.ReadInt32();
            Pd4900a.shiftlo = reader.ReadUInt32();
            Pd4900a.shifthi = reader.ReadUInt32();
            Pd4900a.retraces = reader.ReadInt32();
            Pd4900a.testwaits = reader.ReadInt32();
            Pd4900a.maxwaits = reader.ReadInt32();
            Pd4900a.testbit = reader.ReadInt32();
            Pd4900a.outputbit = reader.ReadInt32();
            Pd4900a.bitno = reader.ReadInt32();
            Pd4900a.reading = reader.ReadByte();
            Pd4900a.writting = reader.ReadByte();
            Pd4900a.clock_line = reader.ReadInt32();
            Pd4900a.command_line = reader.ReadInt32();
        }
    }
}
