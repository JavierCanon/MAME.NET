using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public class Eeprom
    {
        public static int serial_count;
        public static byte[] serial_buffer = new byte[40];
        public static byte[] eeprom_data = new byte[0x80];
        private static byte[] cmd_read = new byte[] { (byte)'0', (byte)'1', (byte)'1', (byte)'0' };
        private static byte[] cmd_write = new byte[] { (byte)'0', (byte)'1', (byte)'0', (byte)'1' };
        private static byte[] cmd_erase = new byte[] { (byte)'0', (byte)'1', (byte)'1', (byte)'1' };
        public static int eeprom_data_bits;
        public static int latch, sending;
        public static int address_bits, data_bits;
        public static LineState reset_line, clock_line;
        public static int locked;
        private static bool eeprom_command_match(byte[] buf, byte[] cmd, int len)
        {
            int ibuf = 0, idx = 0;
            for (; len > 0; )
            {
                byte b = buf[ibuf];
                byte c = cmd[idx];
                if ((b == 0) || (c == 0))
                    return (b == c);
                switch (c)
                {
                    case (byte)'0':
                    case (byte)'1':
                        if (b != c)
                        {
                            return false;
                        }
                        ibuf++;
                        len--;
                        idx++;
                        break;
                    case (byte)'X':
                    case (byte)'x':
                        ibuf++;
                        len--;
                        idx++;
                        break;
                    case (byte)'*':
                        break;
                }
            }
            return (idx >= cmd.Length);
        }
        public static void eeprom_init()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                    break;
                case "CPS-1(QSound)":
                    for (int i = 0; i < 0x80; i++)
                    {
                        eeprom_data[i] = 0xff;
                    }
                    serial_count = 0;
                    latch = 0;
                    reset_line = LineState.ASSERT_LINE;
                    clock_line = LineState.ASSERT_LINE;
                    sending = 0;
                    locked = 0;
                    address_bits = 7;
                    data_bits = 8;
                    break;
                case "CPS2":
                    for (int i = 0; i < 0x80; i++)
                    {
                        eeprom_data[i] = 0xff;
                    }
                    serial_count = 0;
                    latch = 0;
                    reset_line = LineState.ASSERT_LINE;
                    clock_line = LineState.ASSERT_LINE;
                    sending = 0;
                    locked = 0;
                    address_bits = 6;
                    data_bits = 16;
                    break;
            }
            switch (Machine.sName)
            {
                case "pang3":
                case "pang3r1":
                case "pang3j":
                case "pang3b":
                    for (int i = 0; i < 0x80; i++)
                    {
                        eeprom_data[i] = 0xff;
                    }
                    serial_count = 0;
                    latch = 0;
                    reset_line = LineState.ASSERT_LINE;
                    clock_line = LineState.ASSERT_LINE;
                    sending = 0;
                    locked = 0;
                    address_bits = 6;
                    data_bits = 16;
                    break;
            }
        }
        private static void eeprom_write(int bit)
        {
            if (serial_count >= 40 - 1)
            {
                return;
            }
            serial_buffer[serial_count++] = (bit != 0 ? (byte)'1' : (byte)'0');
            serial_buffer[serial_count] = 0;	/* nul terminate so we can treat it as a string */
            if ((serial_count > address_bits) && eeprom_command_match(serial_buffer, cmd_read, serial_count - address_bits))
            {
                int i, address;
                address = 0;
                for (i = serial_count - address_bits; i < serial_count; i++)
                {
                    address <<= 1;
                    if (serial_buffer[i] == (byte)'1')
                        address |= 1;
                }
                if (data_bits == 16)
                {
                    eeprom_data_bits = (eeprom_data[2 * address + 0] << 8) + eeprom_data[2 * address + 1];
                }
                else
                {
                    eeprom_data_bits = eeprom_data[address];
                    if (eeprom_data_bits == 0xff)
                    {
                        int i1 = 1;
                    }
                }
                sending = 1;
                serial_count = 0;
            }
            else if ((serial_count > address_bits) && eeprom_command_match(serial_buffer, cmd_erase, serial_count - address_bits))
            {
                int i, address;
                address = 0;
                for (i = serial_count - address_bits; i < serial_count; i++)
                {
                    address <<= 1;
                    if (serial_buffer[i] == '1')
                        address |= 1;
                }
                if (locked == 0)
                {
                    if (data_bits == 16)
                    {
                        eeprom_data[2 * address + 0] = 0x00;
                        eeprom_data[2 * address + 1] = 0x00;
                    }
                    else
                    {
                        eeprom_data[address] = 0x00;
                    }
                }
                serial_count = 0;
            }
            else if ((serial_count > (address_bits + data_bits)) && eeprom_command_match(serial_buffer, cmd_write, serial_count - (address_bits + data_bits)))
            {
                int i, address, data;
                address = 0;
                for (i = serial_count - data_bits - address_bits; i < (serial_count - data_bits); i++)
                {
                    address <<= 1;
                    if (serial_buffer[i] == '1')
                        address |= 1;
                }
                data = 0;
                for (i = serial_count - data_bits; i < serial_count; i++)
                {
                    data <<= 1;
                    if (serial_buffer[i] == '1')
                        data |= 1;
                }
                if (locked == 0)
                {
                    if (data_bits == 16)
                    {
                        eeprom_data[2 * address + 0] = (byte)(data >> 8);
                        eeprom_data[2 * address + 1] = (byte)(data & 0xff);
                    }
                    else
                    {
                        eeprom_data[address] = (byte)data;
                    }
                }
                serial_count = 0;
            }
        }
        private static void eeprom_reset()
        {
            serial_count = 0;
            sending = 0;
        }
        public static void eeprom_write_bit(int bit)
        {
            latch = bit;
        }
        public static int eeprom_read_bit()
        {
            int res;
            if (sending == 1)
            {
                res = (eeprom_data_bits >> data_bits) & 1;
            }
            else
            {
                res = 1;
            }
            return res;
        }
        public static int eeprom_bit_r()
        {
            return eeprom_read_bit();
        }
        public static void eeprom_set_cs_line(LineState state)
        {
            reset_line = state;
            if (reset_line != LineState.CLEAR_LINE)
                eeprom_reset();
        }
        public static void eeprom_set_clock_line(LineState state)
        {
            if (state == LineState.PULSE_LINE || (clock_line == LineState.CLEAR_LINE && state != LineState.CLEAR_LINE))
            {
                if (reset_line == LineState.CLEAR_LINE)
                {
                    if (sending == 1)
                    {
                        eeprom_data_bits = (eeprom_data_bits << 1) | 1;
                    }
                    else
                    {
                        eeprom_write(latch);
                    }
                }
            }
            clock_line = state;
        }
    }
}
