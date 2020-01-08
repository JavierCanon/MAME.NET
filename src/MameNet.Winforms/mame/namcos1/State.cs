using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.m6809;
using cpu.m6800;

namespace mame
{
    public partial class Namcos1
    {
        public static void SaveStateBinary(BinaryWriter writer)
        {
            int i, j;
            writer.Write(dipsw);
            for (i = 0; i < 0x2000; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(bank_ram20, 0, 0x2000);
            writer.Write(bank_ram30, 0, 0x80);
            writer.Write(namcos1_videoram, 0, 0x8000);
            writer.Write(namcos1_cus116, 0, 0x10);
            writer.Write(namcos1_spriteram, 0, 0x1000);
            writer.Write(namcos1_playfield_control, 0, 0x20);
            writer.Write(copy_sprites);
            writer.Write(s1ram, 0, 0x8000);
            writer.Write(namcos1_triram, 0, 0x800);
            writer.Write(namcos1_paletteram, 0, 0x8000);
            writer.Write(key, 0, 8);
            writer.Write(audiocpurom_offset);
            writer.Write(mcu_patch_data);
            writer.Write(mcurom_offset);
            writer.Write(namcos1_reset);
            writer.Write(wdog);
            writer.Write(dac0_value);
            writer.Write(dac1_value);
            writer.Write(dac0_gain);
            writer.Write(dac1_gain);
            writer.Write(Generic.generic_nvram, 0, 0x800);
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    writer.Write(cus117_offset[i, j]);
                }
            }
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    writer.Write(user1rom_offset[i, j]);
                }
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(M6809.mm1[i].PC.LowWord);
                writer.Write(M6809.mm1[i].PPC.LowWord);
                writer.Write(M6809.mm1[i].D.LowWord);
                writer.Write(M6809.mm1[i].DP.LowWord);
                writer.Write(M6809.mm1[i].U.LowWord);
                writer.Write(M6809.mm1[i].S.LowWord);
                writer.Write(M6809.mm1[i].X.LowWord);
                writer.Write(M6809.mm1[i].Y.LowWord);
                writer.Write(M6809.mm1[i].CC);
                writer.Write((byte)M6809.mm1[i].irq_state[0]);
                writer.Write((byte)M6809.mm1[i].irq_state[1]);
                writer.Write(M6809.mm1[i].int_state);
                writer.Write((byte)M6809.mm1[i].nmi_state);
            }
            writer.Write(M6800.m1.PPC.LowWord);
            writer.Write(M6800.m1.PC.LowWord);
            writer.Write(M6800.m1.S.LowWord);
            writer.Write(M6800.m1.X.LowWord);
            writer.Write(M6800.m1.D.LowWord);
            writer.Write(M6800.m1.cc);
            writer.Write(M6800.m1.wai_state);
            writer.Write((byte)M6800.m1.nmi_state);
            writer.Write((byte)M6800.m1.irq_state[0]);
            writer.Write((byte)M6800.m1.irq_state[1]);
            writer.Write(M6800.m1.ic_eddge);
            writer.Write(M6800.m1.port1_ddr);
            writer.Write(M6800.m1.port2_ddr);
            writer.Write(M6800.m1.port3_ddr);
            writer.Write(M6800.m1.port4_ddr);
            writer.Write(M6800.m1.port1_data);
            writer.Write(M6800.m1.port2_data);
            writer.Write(M6800.m1.port3_data);
            writer.Write(M6800.m1.port4_data);
            writer.Write(M6800.m1.tcsr);
            writer.Write(M6800.m1.pending_tcsr);
            writer.Write(M6800.m1.irq2);
            writer.Write(M6800.m1.ram_ctrl);
            writer.Write(M6800.m1.counter.d);
            writer.Write(M6800.m1.output_compare.d);
            writer.Write(M6800.m1.input_capture);
            writer.Write(M6800.m1.timer_over.d);
            writer.Write(M6800.m1.clock);
            writer.Write(M6800.m1.trcsr);
            writer.Write(M6800.m1.rmcr);
            writer.Write(M6800.m1.rdr);
            writer.Write(M6800.m1.tdr);
            writer.Write(M6800.m1.rsr);
            writer.Write(M6800.m1.tsr);
            writer.Write(M6800.m1.rxbits);
            writer.Write(M6800.m1.txbits);
            writer.Write((int)M6800.m1.txstate);
            writer.Write(M6800.m1.trcsr_read);
            writer.Write(M6800.m1.tx);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            writer.Write(Video.screenstate.vblank_start_time.seconds);
            writer.Write(Video.screenstate.vblank_start_time.attoseconds);
            writer.Write(Video.screenstate.frame_number);
            writer.Write(Sound.last_update_second);
            for (i = 0; i < 4; i++)
            {
                writer.Write(Cpuexec.cpu[i].suspend);
                writer.Write(Cpuexec.cpu[i].nextsuspend);
                writer.Write(Cpuexec.cpu[i].eatcycles);
                writer.Write(Cpuexec.cpu[i].nexteatcycles);
                writer.Write(Cpuexec.cpu[i].localtime.seconds);
                writer.Write(Cpuexec.cpu[i].localtime.attoseconds);
                writer.Write(Cpuexec.cpu[i].TotalExecutedCycles);
                writer.Write(Cpuexec.cpu[i].PendingCycles);
            }
            Timer.SaveStateBinary(writer);
            YM2151.SaveStateBinary(writer);
            writer.Write(Namco.nam1.num_voices);
            writer.Write(Namco.nam1.sound_enable);
            for (i = 0; i < 16; i++)
            {
                for (j = 0; j < 32 * 16; j++)
                {
                    writer.Write(Namco.nam1.waveform[i][j]);
                }
            }
            for (i = 0; i < 8; i++)
            {
                writer.Write(Namco.nam1.channel_list[i].frequency);
                writer.Write(Namco.nam1.channel_list[i].counter);
                writer.Write(Namco.nam1.channel_list[i].volume[0]);
                writer.Write(Namco.nam1.channel_list[i].volume[1]);
                writer.Write(Namco.nam1.channel_list[i].noise_sw);
                writer.Write(Namco.nam1.channel_list[i].noise_state);
                writer.Write(Namco.nam1.channel_list[i].noise_seed);
                writer.Write(Namco.nam1.channel_list[i].noise_hold);
                writer.Write(Namco.nam1.channel_list[i].noise_counter);
                writer.Write(Namco.nam1.channel_list[i].waveform_select);
            }
            writer.Write(Namco.namco_wavedata, 0, 0x400);
            writer.Write(DAC.dac1.output);
            writer.Write(Sound.ym2151stream.output_sampindex);
            writer.Write(Sound.ym2151stream.output_base_sampindex);
            writer.Write(Sound.namcostream.output_sampindex);
            writer.Write(Sound.namcostream.output_base_sampindex);
            writer.Write(Sound.dacstream.output_sampindex);
            writer.Write(Sound.dacstream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary(BinaryReader reader)
        {
            int i,j;
            dipsw = reader.ReadByte();
            for (i = 0; i < 0x2000; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            bank_ram20 = reader.ReadBytes(0x2000);
            bank_ram30 = reader.ReadBytes(0x80);
            namcos1_videoram = reader.ReadBytes(0x8000);
            namcos1_cus116 = reader.ReadBytes(0x10);
            namcos1_spriteram = reader.ReadBytes(0x1000);
            namcos1_playfield_control = reader.ReadBytes(0x20);
            copy_sprites = reader.ReadInt32();
            s1ram = reader.ReadBytes(0x8000);
            namcos1_triram = reader.ReadBytes(0x800);
            namcos1_paletteram = reader.ReadBytes(0x8000);
            key = reader.ReadBytes(8);
            audiocpurom_offset = reader.ReadInt32();
            mcu_patch_data = reader.ReadInt32();
            mcurom_offset = reader.ReadInt32();
            namcos1_reset = reader.ReadInt32();
            wdog = reader.ReadInt32();
            dac0_value = reader.ReadInt32();
            dac1_value = reader.ReadInt32();
            dac0_gain = reader.ReadInt32();
            dac1_gain = reader.ReadInt32();
            Generic.generic_nvram = reader.ReadBytes(0x800);
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    cus117_offset[i, j] = reader.ReadInt32();
                }
            }
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    user1rom_offset[i, j] = reader.ReadInt32();
                }
            }
            for (i = 0; i < 3; i++)
            {
                M6809.mm1[i].PC.LowWord = reader.ReadUInt16();
                M6809.mm1[i].PPC.LowWord = reader.ReadUInt16();
                M6809.mm1[i].D.LowWord = reader.ReadUInt16();
                M6809.mm1[i].DP.LowWord = reader.ReadUInt16();
                M6809.mm1[i].U.LowWord = reader.ReadUInt16();
                M6809.mm1[i].S.LowWord = reader.ReadUInt16();
                M6809.mm1[i].X.LowWord = reader.ReadUInt16();
                M6809.mm1[i].Y.LowWord = reader.ReadUInt16();
                M6809.mm1[i].CC = reader.ReadByte();
                M6809.mm1[i].irq_state[0] = (LineState)reader.ReadByte();
                M6809.mm1[i].irq_state[1] = (LineState)reader.ReadByte();
                M6809.mm1[i].int_state = reader.ReadByte();
                M6809.mm1[i].nmi_state = (LineState)reader.ReadByte();
            }
            M6800.m1.PPC.LowWord = reader.ReadUInt16();
            M6800.m1.PC.LowWord = reader.ReadUInt16();
            M6800.m1.S.LowWord = reader.ReadUInt16();
            M6800.m1.X.LowWord = reader.ReadUInt16();
            M6800.m1.D.LowWord = reader.ReadUInt16();
            M6800.m1.cc = reader.ReadByte();
            M6800.m1.wai_state = reader.ReadByte();
            M6800.m1.nmi_state = (LineState)reader.ReadByte();
            M6800.m1.irq_state[0] = (LineState)reader.ReadByte();
            M6800.m1.irq_state[1] = (LineState)reader.ReadByte();
            M6800.m1.ic_eddge = reader.ReadByte();
            M6800.m1.port1_ddr = reader.ReadByte();
            M6800.m1.port2_ddr = reader.ReadByte();
            M6800.m1.port3_ddr = reader.ReadByte();
            M6800.m1.port4_ddr = reader.ReadByte();
            M6800.m1.port1_data = reader.ReadByte();
            M6800.m1.port2_data = reader.ReadByte();
            M6800.m1.port3_data = reader.ReadByte();
            M6800.m1.port4_data = reader.ReadByte();
            M6800.m1.tcsr = reader.ReadByte();
            M6800.m1.pending_tcsr = reader.ReadByte();
            M6800.m1.irq2 = reader.ReadByte();
            M6800.m1.ram_ctrl = reader.ReadByte();
            M6800.m1.counter.d = reader.ReadUInt32();
            M6800.m1.output_compare.d = reader.ReadUInt32();
            M6800.m1.input_capture = reader.ReadUInt16();
            M6800.m1.timer_over.d = reader.ReadUInt32();
            M6800.m1.clock = reader.ReadInt32();
            M6800.m1.trcsr = reader.ReadByte();
            M6800.m1.rmcr = reader.ReadByte();
            M6800.m1.rdr = reader.ReadByte();
            M6800.m1.tdr = reader.ReadByte();
            M6800.m1.rsr = reader.ReadByte();
            M6800.m1.tsr = reader.ReadByte();
            M6800.m1.rxbits=reader.ReadInt32();
            M6800.m1.txbits = reader.ReadInt32();
            M6800.m1.txstate = (M6800.M6800_TX_STATE)reader.ReadInt32();
            M6800.m1.trcsr_read = reader.ReadInt32();
            M6800.m1.tx = reader.ReadInt32();
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.screenstate.vblank_start_time.seconds = reader.ReadInt32();
            Video.screenstate.vblank_start_time.attoseconds = reader.ReadInt64();
            Video.screenstate.frame_number = reader.ReadInt64();
            Sound.last_update_second = reader.ReadInt32();
            for (i = 0; i < 4; i++)
            {
                Cpuexec.cpu[i].suspend = reader.ReadByte();
                Cpuexec.cpu[i].nextsuspend = reader.ReadByte();
                Cpuexec.cpu[i].eatcycles = reader.ReadByte();
                Cpuexec.cpu[i].nexteatcycles = reader.ReadByte();
                Cpuexec.cpu[i].localtime.seconds = reader.ReadInt32();
                Cpuexec.cpu[i].localtime.attoseconds = reader.ReadInt64();
                Cpuexec.cpu[i].TotalExecutedCycles = reader.ReadUInt64();
                Cpuexec.cpu[i].PendingCycles = reader.ReadInt32();
            }
            Timer.LoadStateBinary(reader);
            YM2151.LoadStateBinary(reader);
            Namco.nam1.num_voices = reader.ReadInt32();
            Namco.nam1.sound_enable = reader.ReadInt32();
            for (i = 0; i < 16; i++)
            {
                for (j = 0; j < 32 * 16; j++)
                {
                    Namco.nam1.waveform[i][j] = reader.ReadInt16();
                }
            }
            for (i = 0; i < 8; i++)
            {
                Namco.nam1.channel_list[i].frequency = reader.ReadInt32();
                Namco.nam1.channel_list[i].counter = reader.ReadInt32();
                Namco.nam1.channel_list[i].volume[0] = reader.ReadInt32();
                Namco.nam1.channel_list[i].volume[1] = reader.ReadInt32();
                Namco.nam1.channel_list[i].noise_sw = reader.ReadInt32();
                Namco.nam1.channel_list[i].noise_state = reader.ReadInt32();
                Namco.nam1.channel_list[i].noise_seed = reader.ReadInt32();
                Namco.nam1.channel_list[i].noise_hold = reader.ReadInt32();
                Namco.nam1.channel_list[i].noise_counter = reader.ReadInt32();
                Namco.nam1.channel_list[i].waveform_select = reader.ReadInt32();
            }
            Namco.namco_wavedata = reader.ReadBytes(0x400);
            DAC.dac1.output = reader.ReadInt16();
            Sound.ym2151stream.output_sampindex = reader.ReadInt32();
            Sound.ym2151stream.output_base_sampindex = reader.ReadInt32();
            Sound.namcostream.output_sampindex = reader.ReadInt32();
            Sound.namcostream.output_base_sampindex = reader.ReadInt32();
            Sound.dacstream.output_sampindex = reader.ReadInt32();
            Sound.dacstream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
    }
}
