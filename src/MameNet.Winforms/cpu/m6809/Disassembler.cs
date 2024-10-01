using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cpu.m6809
{
    public partial class M6809
    {
        public class opcodeinfo
        {
            public byte opcode;
            public int length;
            public string name;
            public m6809_addressing_modes mode;
            public uint flags;
            public opcodeinfo(byte _opcode, int _length, string _name, m6809_addressing_modes _mode)
            {
                opcode = _opcode;
                length = _length;
                name = _name;
                mode = _mode;
            }
            public opcodeinfo(byte _opcode, int _length, string _name, m6809_addressing_modes _mode, uint _flags)
            {
                opcode = _opcode;
                length = _length;
                name = _name;
                mode = _mode;
                flags = _flags;
            }
        }
        public static uint DASMFLAG_STEP_OVER = 0x20000000, DASMFLAG_SUPPORTED = 0x80000000;
        public enum m6809_addressing_modes
        {
            INH,				// Inherent
            DIR,				// Direct
            IND,				// Indexed
            REL,				// Relative (8 bit)
            LREL,				// Long relative (16 bit)
            EXT,				// Extended
            IMM,				// Immediate
            IMM_RR,				// Register-to-register
            PG1,				// Switch to page 1 opcodes
            PG2 				// Switch to page 2 opcodes
        }
        public opcodeinfo[] m6809_pg0opcodes = new opcodeinfo[]{
            new opcodeinfo(0x00, 2, "NEG",m6809_addressing_modes.DIR),
            new opcodeinfo(0x03, 2, "COM",m6809_addressing_modes.DIR),
            new opcodeinfo(0x04, 2, "LSR",m6809_addressing_modes.DIR),
            new opcodeinfo(0x06, 2, "ROR",m6809_addressing_modes.DIR),
            new opcodeinfo(0x07, 2, "ASR",m6809_addressing_modes.DIR),
            new opcodeinfo(0x08, 2, "ASL",m6809_addressing_modes.DIR),
            new opcodeinfo(0x09, 2, "ROL",m6809_addressing_modes.DIR),
            new opcodeinfo(0x0A, 2, "DEC",m6809_addressing_modes.DIR),
            new opcodeinfo(0x0C, 2, "INC",m6809_addressing_modes.DIR),
            new opcodeinfo(0x0D, 2, "TST",m6809_addressing_modes.DIR),
            new opcodeinfo(0x0E, 2, "JMP",m6809_addressing_modes.DIR),
            new opcodeinfo(0x0F, 2, "CLR",m6809_addressing_modes.DIR),

            new opcodeinfo(0x10, 1, "page1",m6809_addressing_modes.PG1),
            new opcodeinfo(0x11, 1, "page2",m6809_addressing_modes.PG2),
            new opcodeinfo(0x12, 1, "NOP",m6809_addressing_modes.INH),
            new opcodeinfo(0x13, 1, "SYNC",m6809_addressing_modes.INH),
            new opcodeinfo(0x16, 3, "LBRA",m6809_addressing_modes.LREL),
            new opcodeinfo(0x17, 3, "LBSR",m6809_addressing_modes.LREL,DASMFLAG_STEP_OVER),
            new opcodeinfo(0x19, 1, "DAA",m6809_addressing_modes.INH),
            new opcodeinfo(0x1A, 2, "ORCC",m6809_addressing_modes.IMM),
            new opcodeinfo(0x1C, 2, "ANDCC",m6809_addressing_modes.IMM),
            new opcodeinfo(0x1D, 1, "SEX",m6809_addressing_modes.INH),
            new opcodeinfo(0x1E, 2, "EXG",m6809_addressing_modes.IMM_RR),
            new opcodeinfo(0x1F, 2, "TFR",m6809_addressing_modes.IMM_RR),

            new opcodeinfo(0x20, 2, "BRA",m6809_addressing_modes.REL),
            new opcodeinfo(0x21, 2, "BRN",m6809_addressing_modes.REL),
            new opcodeinfo(0x22, 2, "BHI",m6809_addressing_modes.REL),
            new opcodeinfo(0x23, 2, "BLS",m6809_addressing_modes.REL),
            new opcodeinfo(0x24, 2, "BCC",m6809_addressing_modes.REL),
            new opcodeinfo(0x25, 2, "BCS",m6809_addressing_modes.REL),
            new opcodeinfo(0x26, 2, "BNE",m6809_addressing_modes.REL),
            new opcodeinfo(0x27, 2, "BEQ",m6809_addressing_modes.REL),
            new opcodeinfo(0x28, 2, "BVC",m6809_addressing_modes.REL),
            new opcodeinfo(0x29, 2, "BVS",m6809_addressing_modes.REL),
            new opcodeinfo(0x2A, 2, "BPL",m6809_addressing_modes.REL),
            new opcodeinfo(0x2B, 2, "BMI",m6809_addressing_modes.REL),
            new opcodeinfo(0x2C, 2, "BGE",m6809_addressing_modes.REL),
            new opcodeinfo(0x2D, 2, "BLT",m6809_addressing_modes.REL),
            new opcodeinfo(0x2E, 2, "BGT",m6809_addressing_modes.REL),
            new opcodeinfo(0x2F, 2, "BLE",m6809_addressing_modes.REL),

            new opcodeinfo(0x30, 2, "LEAX",m6809_addressing_modes.IND),
            new opcodeinfo(0x31, 2, "LEAY",m6809_addressing_modes.IND),
            new opcodeinfo(0x32, 2, "LEAS",m6809_addressing_modes.IND),
            new opcodeinfo(0x33, 2, "LEAU",m6809_addressing_modes.IND),
            new opcodeinfo(0x34, 2, "PSHS",m6809_addressing_modes.INH),
            new opcodeinfo(0x35, 2, "PULS",m6809_addressing_modes.INH),
            new opcodeinfo(0x36, 2, "PSHU",m6809_addressing_modes.INH),
            new opcodeinfo(0x37, 2, "PULU",m6809_addressing_modes.INH),
            new opcodeinfo(0x39, 1, "RTS",m6809_addressing_modes.INH),
            new opcodeinfo(0x3A, 1, "ABX",m6809_addressing_modes.INH),
            new opcodeinfo(0x3B, 1, "RTI",m6809_addressing_modes.INH),
            new opcodeinfo(0x3C, 2, "CWAI",m6809_addressing_modes.IMM),
            new opcodeinfo(0x3D, 1, "MUL",m6809_addressing_modes.INH),
            new opcodeinfo(0x3F, 1, "SWI",m6809_addressing_modes.INH),

            new opcodeinfo(0x40, 1, "NEGA",m6809_addressing_modes.INH),
            new opcodeinfo(0x43, 1, "COMA",m6809_addressing_modes.INH),
            new opcodeinfo(0x44, 1, "LSRA",m6809_addressing_modes.INH),
            new opcodeinfo(0x46, 1, "RORA",m6809_addressing_modes.INH),
            new opcodeinfo(0x47, 1, "ASRA",m6809_addressing_modes.INH),
            new opcodeinfo(0x48, 1, "ASLA",m6809_addressing_modes.INH),
            new opcodeinfo(0x49, 1, "ROLA",m6809_addressing_modes.INH),
            new opcodeinfo(0x4A, 1, "DECA",m6809_addressing_modes.INH),
            new opcodeinfo(0x4C, 1, "INCA",m6809_addressing_modes.INH),
            new opcodeinfo(0x4D, 1, "TSTA",m6809_addressing_modes.INH),
            new opcodeinfo(0x4F, 1, "CLRA",m6809_addressing_modes.INH),

            new opcodeinfo(0x50, 1, "NEGB",m6809_addressing_modes.INH),
            new opcodeinfo(0x53, 1, "COMB",m6809_addressing_modes.INH),
            new opcodeinfo(0x54, 1, "LSRB",m6809_addressing_modes.INH),
            new opcodeinfo(0x56, 1, "RORB",m6809_addressing_modes.INH),
            new opcodeinfo(0x57, 1, "ASRB",m6809_addressing_modes.INH),
            new opcodeinfo(0x58, 1, "ASLB",m6809_addressing_modes.INH),
            new opcodeinfo(0x59, 1, "ROLB",m6809_addressing_modes.INH),
            new opcodeinfo(0x5A, 1, "DECB",m6809_addressing_modes.INH),
            new opcodeinfo(0x5C, 1, "INCB",m6809_addressing_modes.INH),
            new opcodeinfo(0x5D, 1, "TSTB",m6809_addressing_modes.INH),
            new opcodeinfo(0x5F, 1, "CLRB",m6809_addressing_modes.INH),

            new opcodeinfo(0x60, 2, "NEG",m6809_addressing_modes.IND),
            new opcodeinfo(0x63, 2, "COM",m6809_addressing_modes.IND),
            new opcodeinfo(0x64, 2, "LSR",m6809_addressing_modes.IND),
            new opcodeinfo(0x66, 2, "ROR",m6809_addressing_modes.IND),
            new opcodeinfo(0x67, 2, "ASR",m6809_addressing_modes.IND),
            new opcodeinfo(0x68, 2, "ASL",m6809_addressing_modes.IND),
            new opcodeinfo(0x69, 2, "ROL",m6809_addressing_modes.IND),
            new opcodeinfo(0x6A, 2, "DEC",m6809_addressing_modes.IND),
            new opcodeinfo(0x6C, 2, "INC",m6809_addressing_modes.IND),
            new opcodeinfo(0x6D, 2, "TST",m6809_addressing_modes.IND),
            new opcodeinfo(0x6E, 2, "JMP",m6809_addressing_modes.IND),
            new opcodeinfo(0x6F, 2, "CLR",m6809_addressing_modes.IND),

            new opcodeinfo(0x70, 3, "NEG",m6809_addressing_modes.EXT),
            new opcodeinfo(0x73, 3, "COM",m6809_addressing_modes.EXT),
            new opcodeinfo(0x74, 3, "LSR",m6809_addressing_modes.EXT),
            new opcodeinfo(0x76, 3, "ROR",m6809_addressing_modes.EXT),
            new opcodeinfo(0x77, 3, "ASR",m6809_addressing_modes.EXT),
            new opcodeinfo(0x78, 3, "ASL",m6809_addressing_modes.EXT),
            new opcodeinfo(0x79, 3, "ROL",m6809_addressing_modes.EXT),
            new opcodeinfo(0x7A, 3, "DEC",m6809_addressing_modes.EXT),
            new opcodeinfo(0x7C, 3, "INC",m6809_addressing_modes.EXT),
            new opcodeinfo(0x7D, 3, "TST",m6809_addressing_modes.EXT),
            new opcodeinfo(0x7E, 3, "JMP",m6809_addressing_modes.EXT),
            new opcodeinfo(0x7F, 3, "CLR",m6809_addressing_modes.EXT),

            new opcodeinfo(0x80, 2, "SUBA",m6809_addressing_modes.IMM),
            new opcodeinfo(0x81, 2, "CMPA",m6809_addressing_modes.IMM),
            new opcodeinfo(0x82, 2, "SBCA",m6809_addressing_modes.IMM),
            new opcodeinfo(0x83, 3, "SUBD",m6809_addressing_modes.IMM),
            new opcodeinfo(0x84, 2, "ANDA",m6809_addressing_modes.IMM),
            new opcodeinfo(0x85, 2, "BITA",m6809_addressing_modes.IMM),
            new opcodeinfo(0x86, 2, "LDA",m6809_addressing_modes.IMM),
            new opcodeinfo(0x88, 2, "EORA",m6809_addressing_modes.IMM),
            new opcodeinfo(0x89, 2, "ADCA",m6809_addressing_modes.IMM),
            new opcodeinfo(0x8A, 2, "ORA",m6809_addressing_modes.IMM),
            new opcodeinfo(0x8B, 2, "ADDA",m6809_addressing_modes.IMM),
            new opcodeinfo(0x8C, 3, "CMPX",m6809_addressing_modes.IMM),
            new opcodeinfo(0x8D, 2, "BSR",m6809_addressing_modes.REL,DASMFLAG_STEP_OVER),
            new opcodeinfo(0x8E, 3, "LDX",m6809_addressing_modes.IMM),

            new opcodeinfo(0x90, 2, "SUBA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x91, 2, "CMPA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x92, 2, "SBCA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x93, 2, "SUBD",m6809_addressing_modes.DIR),
            new opcodeinfo(0x94, 2, "ANDA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x95, 2, "BITA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x96, 2, "LDA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x97, 2, "STA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x98, 2, "EORA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x99, 2, "ADCA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x9A, 2, "ORA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x9B, 2, "ADDA",m6809_addressing_modes.DIR),
            new opcodeinfo(0x9C, 2, "CMPX",m6809_addressing_modes.DIR),
            new opcodeinfo(0x9D, 2, "JSR",m6809_addressing_modes.DIR,DASMFLAG_STEP_OVER),
            new opcodeinfo(0x9E, 2, "LDX",m6809_addressing_modes.DIR),
            new opcodeinfo(0x9F, 2, "STX",m6809_addressing_modes.DIR),

            new opcodeinfo(0xA0, 2, "SUBA",m6809_addressing_modes.IND),
            new opcodeinfo(0xA1, 2, "CMPA",m6809_addressing_modes.IND),
            new opcodeinfo(0xA2, 2, "SBCA",m6809_addressing_modes.IND),
            new opcodeinfo(0xA3, 2, "SUBD",m6809_addressing_modes.IND),
            new opcodeinfo(0xA4, 2, "ANDA",m6809_addressing_modes.IND),
            new opcodeinfo(0xA5, 2, "BITA",m6809_addressing_modes.IND),
            new opcodeinfo(0xA6, 2, "LDA",m6809_addressing_modes.IND),
            new opcodeinfo(0xA7, 2, "STA",m6809_addressing_modes.IND),
            new opcodeinfo(0xA8, 2, "EORA",m6809_addressing_modes.IND),
            new opcodeinfo(0xA9, 2, "ADCA",m6809_addressing_modes.IND),
            new opcodeinfo(0xAA, 2, "ORA",m6809_addressing_modes.IND),
            new opcodeinfo(0xAB, 2, "ADDA",m6809_addressing_modes.IND),
            new opcodeinfo(0xAC, 2, "CMPX",m6809_addressing_modes.IND),
            new opcodeinfo(0xAD, 2, "JSR",m6809_addressing_modes.IND,DASMFLAG_STEP_OVER),
            new opcodeinfo(0xAE, 2, "LDX",m6809_addressing_modes.IND),
            new opcodeinfo(0xAF, 2, "STX",m6809_addressing_modes.IND),

            new opcodeinfo(0xB0, 3, "SUBA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xB1, 3, "CMPA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xB2, 3, "SBCA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xB3, 3, "SUBD",m6809_addressing_modes.EXT),
            new opcodeinfo(0xB4, 3, "ANDA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xB5, 3, "BITA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xB6, 3, "LDA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xB7, 3, "STA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xB8, 3, "EORA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xB9, 3, "ADCA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xBA, 3, "ORA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xBB, 3, "ADDA",m6809_addressing_modes.EXT),
            new opcodeinfo(0xBC, 3, "CMPX",m6809_addressing_modes.EXT),
            new opcodeinfo(0xBD, 3, "JSR",m6809_addressing_modes.EXT,DASMFLAG_STEP_OVER),
            new opcodeinfo(0xBE, 3, "LDX",m6809_addressing_modes.EXT),
            new opcodeinfo(0xBF, 3, "STX",m6809_addressing_modes.EXT),

            new opcodeinfo(0xC0, 2, "SUBB",m6809_addressing_modes.IMM),
            new opcodeinfo(0xC1, 2, "CMPB",m6809_addressing_modes.IMM),
            new opcodeinfo(0xC2, 2, "SBCB",m6809_addressing_modes.IMM),
            new opcodeinfo(0xC3, 3, "ADDD",m6809_addressing_modes.IMM),
            new opcodeinfo(0xC4, 2, "ANDB",m6809_addressing_modes.IMM),
            new opcodeinfo(0xC5, 2, "BITB",m6809_addressing_modes.IMM),
            new opcodeinfo(0xC6, 2, "LDB",m6809_addressing_modes.IMM),
            new opcodeinfo(0xC8, 2, "EORB",m6809_addressing_modes.IMM),
            new opcodeinfo(0xC9, 2, "ADCB",m6809_addressing_modes.IMM),
            new opcodeinfo(0xCA, 2, "ORB",m6809_addressing_modes.IMM),
            new opcodeinfo(0xCB, 2, "ADDB",m6809_addressing_modes.IMM),
            new opcodeinfo(0xCC, 3, "LDD",m6809_addressing_modes.IMM),
            new opcodeinfo(0xCE, 3, "LDU",m6809_addressing_modes.IMM),

            new opcodeinfo(0xD0, 2, "SUBB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xD1, 2, "CMPB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xD2, 2, "SBCB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xD3, 2, "ADDD",m6809_addressing_modes.DIR),
            new opcodeinfo(0xD4, 2, "ANDB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xD5, 2, "BITB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xD6, 2, "LDB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xD7, 2, "STB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xD8, 2, "EORB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xD9, 2, "ADCB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xDA, 2, "ORB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xDB, 2, "ADDB",m6809_addressing_modes.DIR),
            new opcodeinfo(0xDC, 2, "LDD",m6809_addressing_modes.DIR),
            new opcodeinfo(0xDD, 2, "STD",m6809_addressing_modes.DIR),
            new opcodeinfo(0xDE, 2, "LDU",m6809_addressing_modes.DIR),
            new opcodeinfo(0xDF, 2, "STU",m6809_addressing_modes.DIR),

            new opcodeinfo(0xE0, 2, "SUBB",m6809_addressing_modes.IND),
            new opcodeinfo(0xE1, 2, "CMPB",m6809_addressing_modes.IND),
            new opcodeinfo(0xE2, 2, "SBCB",m6809_addressing_modes.IND),
            new opcodeinfo(0xE3, 2, "ADDD",m6809_addressing_modes.IND),
            new opcodeinfo(0xE4, 2, "ANDB",m6809_addressing_modes.IND),
            new opcodeinfo(0xE5, 2, "BITB",m6809_addressing_modes.IND),
            new opcodeinfo(0xE6, 2, "LDB",m6809_addressing_modes.IND),
            new opcodeinfo(0xE7, 2, "STB",m6809_addressing_modes.IND),
            new opcodeinfo(0xE8, 2, "EORB",m6809_addressing_modes.IND),
            new opcodeinfo(0xE9, 2, "ADCB",m6809_addressing_modes.IND),
            new opcodeinfo(0xEA, 2, "ORB",m6809_addressing_modes.IND),
            new opcodeinfo(0xEB, 2, "ADDB",m6809_addressing_modes.IND),
            new opcodeinfo(0xEC, 2, "LDD",m6809_addressing_modes.IND),
            new opcodeinfo(0xED, 2, "STD",m6809_addressing_modes.IND),
            new opcodeinfo(0xEE, 2, "LDU",m6809_addressing_modes.IND),
            new opcodeinfo(0xEF, 2, "STU",m6809_addressing_modes.IND),

            new opcodeinfo(0xF0, 3, "SUBB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xF1, 3, "CMPB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xF2, 3, "SBCB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xF3, 3, "ADDD",m6809_addressing_modes.EXT),
            new opcodeinfo(0xF4, 3, "ANDB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xF5, 3, "BITB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xF6, 3, "LDB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xF7, 3, "STB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xF8, 3, "EORB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xF9, 3, "ADCB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xFA, 3, "ORB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xFB, 3, "ADDB",m6809_addressing_modes.EXT),
            new opcodeinfo(0xFC, 3, "LDD",m6809_addressing_modes.EXT),
            new opcodeinfo(0xFD, 3, "STD",m6809_addressing_modes.EXT),
            new opcodeinfo(0xFE, 3, "LDU",m6809_addressing_modes.EXT),
            new opcodeinfo(0xFF, 3, "STU",m6809_addressing_modes.EXT),
        };
        public opcodeinfo[] m6809_pg1opcodes = new opcodeinfo[]{
            new opcodeinfo(0x21, 4, "LBRN",m6809_addressing_modes.LREL),
            new opcodeinfo(0x22, 4, "LBHI",m6809_addressing_modes.LREL),
            new opcodeinfo(0x23, 4, "LBLS",m6809_addressing_modes.LREL),
            new opcodeinfo(0x24, 4, "LBCC",m6809_addressing_modes.LREL),
            new opcodeinfo(0x25, 4, "LBCS",m6809_addressing_modes.LREL),
            new opcodeinfo(0x26, 4, "LBNE",m6809_addressing_modes.LREL),
            new opcodeinfo(0x27, 4, "LBEQ",m6809_addressing_modes.LREL),
            new opcodeinfo(0x28, 4, "LBVC",m6809_addressing_modes.LREL),
            new opcodeinfo(0x29, 4, "LBVS",m6809_addressing_modes.LREL),
            new opcodeinfo(0x2A, 4, "LBPL",m6809_addressing_modes.LREL),
            new opcodeinfo(0x2B, 4, "LBMI",m6809_addressing_modes.LREL),
            new opcodeinfo(0x2C, 4, "LBGE",m6809_addressing_modes.LREL),
            new opcodeinfo(0x2D, 4, "LBLT",m6809_addressing_modes.LREL),
            new opcodeinfo(0x2E, 4, "LBGT",m6809_addressing_modes.LREL),
            new opcodeinfo(0x2F, 4, "LBLE",m6809_addressing_modes.LREL),
            new opcodeinfo(0x3F, 2, "SWI2",m6809_addressing_modes.INH),
            new opcodeinfo(0x83, 4, "CMPD",m6809_addressing_modes.IMM),
            new opcodeinfo(0x8C, 4, "CMPY",m6809_addressing_modes.IMM),
            new opcodeinfo(0x8E, 4, "LDY",m6809_addressing_modes.IMM),
            new opcodeinfo(0x93, 3, "CMPD",m6809_addressing_modes.DIR),
            new opcodeinfo(0x9C, 3, "CMPY",m6809_addressing_modes.DIR),
            new opcodeinfo(0x9E, 3, "LDY",m6809_addressing_modes.DIR),
            new opcodeinfo(0x9F, 3, "STY",m6809_addressing_modes.DIR),
            new opcodeinfo(0xA3, 3, "CMPD",m6809_addressing_modes.IND),
            new opcodeinfo(0xAC, 3, "CMPY",m6809_addressing_modes.IND),
            new opcodeinfo(0xAE, 3, "LDY",m6809_addressing_modes.IND),
            new opcodeinfo(0xAF, 3, "STY",m6809_addressing_modes.IND),
            new opcodeinfo(0xB3, 4, "CMPD",m6809_addressing_modes.EXT),
            new opcodeinfo(0xBC, 4, "CMPY",m6809_addressing_modes.EXT),
            new opcodeinfo(0xBE, 4, "LDY",m6809_addressing_modes.EXT),
            new opcodeinfo(0xBF, 4, "STY",m6809_addressing_modes.EXT),
            new opcodeinfo(0xCE, 4, "LDS",m6809_addressing_modes.IMM),
            new opcodeinfo(0xDE, 3, "LDS",m6809_addressing_modes.DIR),
            new opcodeinfo(0xDF, 3, "STS",m6809_addressing_modes.DIR),
            new opcodeinfo(0xEE, 3, "LDS",m6809_addressing_modes.IND),
            new opcodeinfo(0xEF, 3, "STS",m6809_addressing_modes.IND),
            new opcodeinfo(0xFE, 4, "LDS",m6809_addressing_modes.EXT),
            new opcodeinfo(0xFF, 4, "STS",m6809_addressing_modes.EXT)
        };
        public opcodeinfo[] m6809_pg2opcodes = new opcodeinfo[]{
            new opcodeinfo(0x3F, 2, "SWI3",m6809_addressing_modes.INH),
            new opcodeinfo(0x83, 4, "CMPU",m6809_addressing_modes.IMM),
            new opcodeinfo(0x8C, 4, "CMPS",m6809_addressing_modes.IMM),
            new opcodeinfo(0x93, 3, "CMPU",m6809_addressing_modes.DIR),
            new opcodeinfo(0x9C, 3, "CMPS",m6809_addressing_modes.DIR),
            new opcodeinfo(0xA3, 3, "CMPU",m6809_addressing_modes.IND),
            new opcodeinfo(0xAC, 3, "CMPS",m6809_addressing_modes.IND),
            new opcodeinfo(0xB3, 4, "CMPU",m6809_addressing_modes.EXT),
            new opcodeinfo(0xBC, 4, "CMPS",m6809_addressing_modes.EXT)
        };
        public opcodeinfo[][] m6809_pgpointers;
        public int[] m6809_numops;
        public static string[] m6809_regs = new string[] { "X", "Y", "U", "S", "PC" };
        public static string[] m6809_regs_te = new string[]
        {
	        "D", "X",  "Y",  "U",   "S",  "PC", "inv", "inv",
	        "A", "B", "CC", "DP", "inv", "inv", "inv", "inv"
        };
        public byte op;
        public void DisassemblerInit()
        {
            m6809_pgpointers = new opcodeinfo[3][] { m6809_pg0opcodes, m6809_pg1opcodes, m6809_pg2opcodes };
            m6809_numops = new int[3] { m6809_pg0opcodes.Length, m6809_pg1opcodes.Length, m6809_pg2opcodes.Length };
        }
        public string m6809_dasm(int ppc)
        {
            string buffer = "";

            byte opcode, pb, pbm, reg;
            m6809_addressing_modes mode;
            byte[] operandarray;
            int ea;
            uint flags;
            int numoperands, offset;
            int i, j, i1, page = 0;
            ushort p = (ushort)ppc;
            if (ppc == 0xc010)
            {
                i1 = 1;
            }
            buffer = ReadOp(p).ToString("X2");
            bool indirect,opcode_found = false;
            do
            {
                opcode = ReadOp(p);
                p++;
                for (i = 0; i < m6809_numops[page]; i++)
                    if (m6809_pgpointers[page][i].opcode == opcode)
                        break;
                if (i < m6809_numops[page])
                    opcode_found = true;
                else
                {
                    buffer += " Illegal Opcode";
                    return buffer;
                }
                if (m6809_pgpointers[page][i].mode >= m6809_addressing_modes.PG1)
                {
                    page = m6809_pgpointers[page][i].mode - m6809_addressing_modes.PG1 + 1;
                    opcode_found = false;
                }
            } while (!opcode_found);
            if (page == 0)
                numoperands = m6809_pgpointers[page][i].length - 1;
            else
                numoperands = m6809_pgpointers[page][i].length - 2;
            operandarray = new byte[numoperands];
            for (j = 0; j < numoperands; j++)
            {
                operandarray[j] = ReadOpArg((ushort)(p + j));
            }
            p += (ushort)numoperands;
            ppc += numoperands;
            mode = m6809_pgpointers[page][i].mode;
            flags = m6809_pgpointers[page][i].flags;

            buffer += m6809_pgpointers[page][i].name.PadLeft(6);
            switch (mode)
            {
                case m6809_addressing_modes.INH:
                    switch (opcode)
                    {
                        case 0x34:	// PSHS
                        case 0x36:	// PSHU
                            pb = operandarray[0];
                            if ((pb & 0x80) != 0)
                                buffer += "PC";
                            if ((pb & 0x40) != 0)
                                buffer += (((pb & 0x80) != 0) ? "," : "") + ((opcode == 0x34) ? "U" : "S");
                            if ((pb & 0x20) != 0)
                                buffer += (((pb & 0xc0) != 0) ? "," : "") + "Y";
                            if ((pb & 0x10) != 0)
                                buffer += (((pb & 0xe0) != 0) ? "," : "") + "X";
                            if ((pb & 0x08) != 0)
                                buffer += (((pb & 0xf0) != 0) ? "," : "") + "DP";
                            if ((pb & 0x04) != 0)
                                buffer += (((pb & 0xf8) != 0) ? "," : "") + "B";
                            if ((pb & 0x02) != 0)
                                buffer += (((pb & 0xfc) != 0) ? "," : "") + "A";
                            if ((pb & 0x01) != 0)
                                buffer += (((pb & 0xfe) != 0) ? "," : "") + "CC";
                            break;
                        case 0x35:	// PULS
                        case 0x37:	// PULU
                            pb = operandarray[0];
                            if ((pb & 0x01) != 0)
                                buffer += "CC";
                            if ((pb & 0x02) != 0)
                                buffer += (((pb & 0x01) != 0) ? "," : "") + "A";
                            if ((pb & 0x04) != 0)
                                buffer += (((pb & 0x03) != 0) ? "," : "") + "B";
                            if ((pb & 0x08) != 0)
                                buffer += (((pb & 0x07) != 0) ? "," : "") + "DP";
                            if ((pb & 0x10) != 0)
                                buffer += (((pb & 0x0f) != 0) ? "," : "") + "X";
                            if ((pb & 0x20) != 0)
                                buffer += (((pb & 0x1f) != 0) ? "," : "") + "Y";
                            if ((pb & 0x40) != 0)
                                buffer += (((pb & 0x3f) != 0) ? "," : "") + ((opcode == 0x35) ? "U" : "S");
                            if ((pb & 0x80) != 0)
                                buffer += (((pb & 0x7f) != 0) ? "," : "") + "PC ; (PUL? PC=RTS)";
                            break;
                        default:
                            // No operands
                            break;
                    }
                    break;

                case m6809_addressing_modes.DIR:
                    ea = operandarray[0];
                    buffer += ea.ToString("X2");
                    break;

                case m6809_addressing_modes.REL:
                    offset = (sbyte)operandarray[0];
                    buffer += ((ppc + offset) & 0xffff).ToString("X4");
                    break;

                case m6809_addressing_modes.LREL:
                    offset = (short)((operandarray[0] << 8) + operandarray[1]);
                    buffer += ((ppc + offset) & 0xffff).ToString("X4");
                    break;

                case m6809_addressing_modes.EXT:
                    ea = ((operandarray[0] << 8) + operandarray[1]);
                    buffer += ea.ToString("X4");
                    break;

                case m6809_addressing_modes.IND:
                    pb = operandarray[0];
                    reg = (byte)((pb >> 5) & 3);
                    pbm = (byte)(pb & 0x8f);
                    indirect = ((pb & 0x90) == 0x90) ? true : false;
                    // open brackets if indirect
                    if (indirect && pbm != 0x80 && pbm != 0x82)
                        buffer += "[";
                    switch (pbm)
                    {
                        case 0x80:	// ,R+
                            if (indirect)
                                buffer = "Illegal Postbyte";
                            else
                                buffer += "," + m6809_regs[reg] + "+";
                            break;

                        case 0x81:	// ,R++
                            buffer += "," + m6809_regs[reg] + "++";
                            break;

                        case 0x82:	// ,-R
                            if (indirect)
                                buffer = "Illegal Postbyte";
                            else
                                buffer += ",-" + m6809_regs[reg];
                            break;

                        case 0x83:	// ,--R
                            buffer += ",--" + m6809_regs[reg];
                            break;

                        case 0x84:	// ,R
                            buffer += "," + m6809_regs[reg];
                            break;

                        case 0x85:	// (+/- B),R
                            buffer += "B," + m6809_regs[reg];
                            break;

                        case 0x86:	// (+/- A),R
                            buffer += "A," + m6809_regs[reg];
                            break;

                        case 0x87:
                            buffer = "Illegal Postbyte";
                            break;

                        case 0x88:	// (+/- 7 bit offset),R
                            offset = (sbyte)ReadOpArg(p);
                            p++;
                            buffer += ((offset < 0) ? "-" : "");
                            buffer += ((offset < 0) ? -offset : offset).ToString("X2");
                            buffer += m6809_regs[reg];
                            break;

                        case 0x89:	// (+/- 15 bit offset),R
                            offset = (short)((ReadOpArg(p) << 8) + ReadOpArg((ushort)(p + 1)));
                            p += 2;
                            buffer += ((offset < 0) ? "-" : "");
                            buffer += ((offset < 0) ? -offset : offset).ToString("X4");
                            buffer += m6809_regs[reg];
                            break;

                        case 0x8a:
                            buffer = "Illegal Postbyte";
                            break;

                        case 0x8b:	// (+/- D),R
                            buffer += "D," + m6809_regs[reg];
                            break;

                        case 0x8c:	// (+/- 7 bit offset),PC
                            offset = (sbyte)ReadOpArg(p);
                            p++;
                            buffer += ((offset < 0) ? "-" : "");
                            buffer += "$" + ((offset < 0) ? -offset : offset).ToString("X2") + ",PC";
                            break;

                        case 0x8d:	// (+/- 15 bit offset),PC
                            offset = (short)((ReadOpArg(p) << 8) + ReadOpArg((ushort)(p + 1)));
                            p += 2;
                            buffer += ((offset < 0) ? "-" : "");
                            buffer += "$" + ((offset < 0) ? -offset : offset).ToString("X4") + ",PC";
                            break;

                        case 0x8e:
                            buffer = "Illegal Postbyte";
                            break;

                        case 0x8f:	// address
                            ea = (short)((ReadOpArg(p) << 8) + ReadOpArg((ushort)(p + 1)));
                            p += 2;
                            buffer += "$" + ea.ToString("X4");
                            break;

                        default:	// (+/- 4 bit offset),R
                            offset = pb & 0x1f;
                            if (offset > 15)
                                offset = offset - 32;
                            buffer += ((offset < 0) ? "-" : "");
                            buffer += "$" + ((offset < 0) ? -offset : offset).ToString("X") + ",";
                            buffer += m6809_regs[reg];
                            break;
                    }

                    // close brackets if indirect
                    if (indirect && pbm != 0x80 && pbm != 0x82)
                        buffer += "]";
                    break;

                case m6809_addressing_modes.IMM:
                    if (numoperands == 2)
                    {
                        ea = (operandarray[0] << 8) + operandarray[1];
                        buffer += "#$" + ea.ToString("X4");
                    }
                    else
                        if (numoperands == 1)
                        {
                            ea = operandarray[0];
                            buffer += "#$" + ea.ToString("X2");
                        }
                    break;

                case m6809_addressing_modes.IMM_RR:
                    pb = operandarray[0];
                    buffer += m6809_regs_te[(pb >> 4) & 0xf] + "," + m6809_regs_te[pb & 0xf];
                    break;
            }
            return buffer;
        }
    }
}