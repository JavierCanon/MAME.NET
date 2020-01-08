using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using mame;

namespace cpu.m6800
{
    public partial class M6800
    {        
        void illegal()
        {
            
        }
        void trap()
        {
            //logerror("M6808: illegal opcode: address %04X, op %02X\n",PC,(int) M_RDOP_ARG(PC)&0xFF);
            TAKE_TRAP();
        }
        void nop()
        {
        }
        void lsrd()
        {
            ushort t;
            CLR_NZC();
            t = D.LowWord;
            cc |= (byte)(t & 0x0001);
            t >>= 1;
            SET_Z16(t);
            D.LowWord = t;
        }
        void asld()
        {
            int r;
            ushort t;
            t = D.LowWord;
            r = t << 1;
            CLR_NZVC();
            SET_FLAGS16((uint)t, (uint)t, (uint)r);
            D.LowWord = (ushort)r;
        }
        void tap()
        {
            cc = D.HighByte;
            ONE_MORE_INSN();
            CHECK_IRQ_LINES();
        }
        void tpa()
        {
            D.HighByte = cc;
        }
        void inx()
        {
            ++X.LowWord;
            CLR_Z();
            SET_Z16(X.LowWord);
        }
        void dex()
        {
            --X.LowWord;
            CLR_Z();
            SET_Z16(X.LowWord);
        }
        void clv()
        {
            CLV();
        }
        void sev()
        {
            SEV();
        }
        void clc()
        {
            CLC();
        }
        void sec()
        {
            SEC();
        }
        void cli()
        {
            CLI();
            ONE_MORE_INSN();
            CHECK_IRQ_LINES();
        }
        void sei()
        {
            SEI();
            ONE_MORE_INSN();
            CHECK_IRQ_LINES();
        }
        void sba()
        {
            ushort t;
            t = (ushort)(D.HighByte - D.LowByte);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, D.LowByte, t);
            D.HighByte = (byte)t;
        }
        void cba()
        {
            ushort t;
            t = (ushort)(D.HighByte - D.LowByte);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, D.LowByte, t);
        }
        void undoc1()
        {
            X.LowWord += ReadMemory((ushort)(S.LowWord + 1));
        }
        void undoc2()
        {
            X.LowWord += ReadMemory((ushort)(S.LowWord + 1));
        }
        void tab()
        {
            D.LowByte = D.HighByte;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void tba()
        {
            D.HighByte = D.LowByte;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void xgdx()
        {
            ushort t = X.LowWord;
            X.LowWord = D.LowWord;
            D.LowWord = t;
        }
        void daa()
        {
            byte msn, lsn;
            ushort t, cf = 0;
            msn = (byte)(D.HighByte & 0xf0);
            lsn = (byte)(D.HighByte & 0x0f);
            if (lsn > 0x09 || (cc & 0x20) != 0)
                cf |= 0x06;
            if (msn > 0x80 && lsn > 0x09)
                cf |= 0x60;
            if (msn > 0x90 || (cc & 0x01) != 0)
                cf |= 0x60;
            t = (ushort)(cf + D.HighByte);
            CLR_NZV();
            SET_NZ8((byte)t);
            SET_C8(t);
            D.HighByte = (byte)t;
        }
        void slp()
        {
            wai_state |= M6800_SLP;
            EAT_CYCLES();
        }
        void aba()
        {
            ushort t;
            t = (ushort)(D.HighByte + D.LowByte);
            CLR_HNZVC();
            SET_FLAGS8(D.HighByte, D.LowByte, t);
            SET_H(D.HighByte, D.LowByte, t);
            D.HighByte = (byte)t;
        }
        void bra()
        {
            byte t;
            t = IMMBYTE();
            PC.LowWord += (ushort)SIGNED(t);
            if (t == 0xfe)
                EAT_CYCLES();
        }
        void brn()
        {
            byte t;
            t = IMMBYTE();
        }
        void bhi()
        {
            BRANCH((cc & 0x05) == 0);
        }
        void bls()
        {
            BRANCH((cc & 0x05) != 0);
        }
        void bcc()
        {
            BRANCH((cc & 0x01) == 0);
        }
        void bcs()
        {
            BRANCH((cc & 0x01) != 0);
        }
        void bne()
        {
            BRANCH((cc & 0x04) == 0);
        }
        void beq()
        {
            BRANCH((cc & 0x04) != 0);
        }
        void bvc()
        {
            BRANCH((cc & 0x02) == 0);
        }
        void bvs()
        {
            BRANCH((cc & 0x02) != 0);
        }
        void bpl()
        {
            BRANCH((cc & 0x08) == 0);
        }
        void bmi()
        {
            BRANCH((cc & 0x08) != 0);
        }
        void bge()
        {
            BRANCH(NXORV() == 0);
        }
        void blt()
        {
            BRANCH(NXORV() != 0);
        }
        void bgt()
        {
            BRANCH(!(NXORV() != 0 || (cc & 0x04) != 0));
        }
        void ble()
        {
            BRANCH(NXORV() != 0 || (cc & 0x04) != 0);
        }
        void tsx()
        {
            X.LowWord = (ushort)(S.LowWord + 1);
        }
        void ins()
        {
            ++S.LowWord;
        }
        void pula()
        {
            D.HighByte=PULLBYTE();
        }
        void pulb()
        {
            D.LowByte=PULLBYTE();
        }
        void des()
        {
            --S.LowWord;
        }
        void txs()
        {
            S.LowWord = (ushort)(X.LowWord - 1);
        }
        void psha()
        {
            PUSHBYTE(D.HighByte);
        }
        void pshb()
        {
            PUSHBYTE(D.LowByte);
        }
        void pulx()
        {
            X=PULLWORD();
        }
        void rts()
        {
            PC=PULLWORD();
            //CHANGE_PC();
        }
        void abx()
        {
            X.LowWord += D.LowByte;
        }
        void rti()
        {
            cc=PULLBYTE();
            D.LowByte=PULLBYTE();
            D.HighByte=PULLBYTE();
            X=PULLWORD();
            PC=PULLWORD();
            //CHANGE_PC();
            CHECK_IRQ_LINES();
        }
        void pshx()
        {
            PUSHWORD(X);
        }
        void mul()
        {
            ushort t;
            t = (ushort)(D.HighByte * D.LowByte);
            CLR_C();
            if ((t & 0x80) != 0)
                SEC();
            D.LowWord = t;
        }
        void wai()
        {
            wai_state |= M6800_WAI;
            PUSHWORD(PC);
            PUSHWORD(X);
            PUSHBYTE(D.HighByte);
            PUSHBYTE(D.LowByte);
            PUSHBYTE(cc);
            CHECK_IRQ_LINES();
            if ((wai_state & M6800_WAI) != 0)
                EAT_CYCLES();
        }
        void swi()
        {
            PUSHWORD(PC);
            PUSHWORD(X);
            PUSHBYTE(D.HighByte);
            PUSHBYTE(D.LowByte);
            PUSHBYTE(cc);
            SEI();
            PC.d = RM16(0xfffa);
            //CHANGE_PC();
        }
        void nega()
        {
            ushort r;
            r = (ushort)(-D.HighByte);
            CLR_NZVC();
            SET_FLAGS8(0, D.HighByte, r);
            D.HighByte = (byte)r;
        }
        void coma()
        {
            D.HighByte = (byte)(~D.HighByte);
            CLR_NZV();
            SET_NZ8(D.HighByte);
            SEC();
        }
        void lsra()
        {
            CLR_NZC();
            cc |= (byte)(D.HighByte & 0x01);
            D.HighByte >>= 1;
            SET_Z8(D.HighByte);
        }
        void rora()
        {
            byte r;
            r = (byte)((cc & 0x01) << 7);
            CLR_NZC();
            cc |= (byte)(D.HighByte & 0x01);
            r |= (byte)(D.HighByte >> 1);
            SET_NZ8(r);
            D.HighByte = r;
        }
        void asra()
        {
            CLR_NZC();
            cc |= (byte)(D.HighByte & 0x01);
            D.HighByte >>= 1;
            D.HighByte |= (byte)((D.HighByte & 0x40) << 1);
            SET_NZ8(D.HighByte);
        }
        void asla()
        {
            ushort r;
            r = (ushort)(D.HighByte << 1);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, D.HighByte, r);
            D.HighByte = (byte)r;
        }
        void rola()
        {
            ushort t, r;
            t = D.HighByte;
            r = (ushort)(cc & 0x01);
            r |= (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8(t, t, r);
            D.HighByte = (byte)r;
        }
        void deca()
        {
            --D.HighByte;
            CLR_NZV();
            SET_FLAGS8D(D.HighByte);
        }
        void inca()
        {
            ++D.HighByte;
            CLR_NZV();
            SET_FLAGS8I(D.HighByte);
        }
        void tsta()
        {
            CLR_NZVC();
            SET_NZ8(D.HighByte);
        }
        void clra()
        {
            D.HighByte = 0;
            CLR_NZVC();
            SEZ();
        }
        void negb()
        {
            ushort r;
            r = (ushort)(-D.LowByte);
            CLR_NZVC();
            SET_FLAGS8(0, D.LowByte, r);
            D.LowByte = (byte)r;
        }
        void comb()
        {
            D.LowByte = (byte)(~D.LowByte);
            CLR_NZV();
            SET_NZ8(D.LowByte);
            SEC();
        }
        void lsrb()
        {
            CLR_NZC();
            cc |= (byte)(D.LowByte & 0x01);
            D.LowByte >>= 1;
            SET_Z8(D.LowByte);
        }
        void rorb()
        {
            byte r;
            r = (byte)((cc & 0x01) << 7);
            CLR_NZC();
            cc |= (byte)(D.LowByte & 0x01);
            r |= (byte)(D.LowByte >> 1);
            SET_NZ8(r);
            D.LowByte = r;
        }
        void asrb()
        {
            CLR_NZC();
            cc |= (byte)(D.LowByte & 0x01);
            D.LowByte >>= 1;
            D.LowByte |= (byte)((D.LowByte & 0x40) << 1);
            SET_NZ8(D.LowByte);
        }
        void aslb()
        {
            ushort r;
            r = (ushort)(D.LowByte << 1);
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, D.LowByte, r);
            D.LowByte = (byte)r;
        }
        void rolb()
        {
            ushort t, r;
            t = D.LowByte;
            r = (ushort)(cc & 0x01);
            r |= (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8(t, t, r);
            D.LowByte = (byte)r;
        }
        void decb()
        {
            --D.LowByte;
            CLR_NZV();
            SET_FLAGS8D(D.LowByte);
        }
        void incb()
        {
            ++D.LowByte;
            CLR_NZV();
            SET_FLAGS8I(D.LowByte);
        }
        void tstb()
        {
            CLR_NZVC();
            SET_NZ8(D.LowByte);
        }
        void clrb()
        {
            D.LowByte = 0;
            CLR_NZVC();
            SEZ();
        }
        void neg_ix()
        {
            ushort r, t;
            t = (ushort)IDXBYTE();
            r = (ushort)(-t);
            CLR_NZVC();
            SET_FLAGS8(0, t, r);
            WriteMemory(EA.LowWord, (byte)r);
        }
        void aim_ix()
        {
            byte t, r;
            t = IMMBYTE();
            r = IDXBYTE();
            r &= t;
            CLR_NZV();
            SET_NZ8(r);
            WriteMemory(EA.LowWord, r);
        }
        void oim_ix()
        {
            byte t, r;
            t = IMMBYTE();
            r = IDXBYTE();
            r |= t;
            CLR_NZV();
            SET_NZ8(r);
            WriteMemory(EA.LowWord, r);
        }
        void com_ix()
        {
            byte t;
            t = IDXBYTE();
            t = (byte)(~t);
            CLR_NZV();
            SET_NZ8(t);
            SEC();
            WriteMemory(EA.LowWord, t);
        }
        void lsr_ix()
        {
            byte t;
            t = IDXBYTE();
            CLR_NZC();
            cc |= (byte)(t & 0x01);
            t >>= 1;
            SET_Z8(t);
            WriteMemory(EA.LowWord, t);
        }
        void eim_ix()
        {
            byte t, r;
            t = IMMBYTE();
            r = IDXBYTE();
            r ^= t;
            CLR_NZV();
            SET_NZ8(r);
            WriteMemory(EA.LowWord, r);
        }
        void ror_ix()
        {
            byte t, r;
            t = IDXBYTE();
            r = (byte)((cc & 0x01) << 7);
            CLR_NZC();
            cc |= (byte)(t & 0x01);
            r |= (byte)(t >> 1);
            SET_NZ8(r);
            WriteMemory(EA.LowWord, r);
        }
        void asr_ix()
        {
            byte t;
            t = IDXBYTE();
            CLR_NZC();
            cc |= (byte)(t & 0x01);
            t >>= 1;
            t |= (byte)((t & 0x40) << 1);
            SET_NZ8(t);
            WriteMemory(EA.LowWord, t);
        }
        void asl_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8(t, t, r);
            WriteMemory(EA.LowWord, (byte)r);
        }
        void rol_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(cc & 0x01);
            r |= (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8(t, t, r);
            WriteMemory(EA.LowWord, (byte)r);
        }
        void dec_ix()
        {
            byte t;
            t = IDXBYTE();
            --t;
            CLR_NZV();
            SET_FLAGS8D(t);
            WriteMemory(EA.LowWord, t);
        }
        void tim_ix()
        {
            byte t, r;
            t = IMMBYTE();
            r = IDXBYTE();
            r &= t;
            CLR_NZV();
            SET_NZ8(r);
        }
        void inc_ix()
        {
            byte t;
            t = IDXBYTE();
            ++t;
            CLR_NZV();
            SET_FLAGS8I(t);
            WriteMemory(EA.LowWord, t);
        }
        void tst_ix()
        {
            byte t;
            t = IDXBYTE();
            CLR_NZVC();
            SET_NZ8(t);
        }
        void jmp_ix()
        {
            INDEXED();
            PC.LowWord = EA.LowWord;
            //CHANGE_PC();
        }
        void clr_ix()
        {
            INDEXED();
            WriteMemory(EA.LowWord, 0);
            CLR_NZVC();
            SEZ();
        }
        void neg_ex()
        {
            ushort r, t;
            t = EXTBYTE();
            r = (ushort)(-t);
            CLR_NZVC();
            SET_FLAGS8(0, t, r);
            WriteMemory(EA.LowWord, (byte)r);
        }
        void aim_di()
        {
            byte t, r;
            t = IMMBYTE();
            r = DIRBYTE();
            r &= t;
            CLR_NZV();
            SET_NZ8(r);
            WriteMemory(EA.LowWord, r);
        }
        void oim_di()
        {
            byte t, r;
            t = IMMBYTE();
            r = DIRBYTE();
            r |= t;
            CLR_NZV();
            SET_NZ8(r);
            WriteMemory(EA.LowWord, r);
        }
        void com_ex()
        {
            byte t;
            t = EXTBYTE();
            t = (byte)(~t);
            CLR_NZV();
            SET_NZ8(t);
            SEC();
            WriteMemory(EA.LowWord, t);
        }
        void lsr_ex()
        {
            byte t;
            t = EXTBYTE();
            CLR_NZC();
            cc |= (byte)(t & 0x01);
            t >>= 1;
            SET_Z8(t);
            WriteMemory(EA.LowWord, t);
        }
        void eim_di()
        {
            byte t, r;
            t = IMMBYTE();
            r = DIRBYTE();
            r ^= t;
            CLR_NZV();
            SET_NZ8(r);
            WriteMemory(EA.LowWord, r);
        }
        void ror_ex()
        {
            byte t, r;
            t = EXTBYTE();
            r = (byte)((cc & 0x01) << 7);
            CLR_NZC();
            cc |= (byte)(t & 0x01);
            r |= (byte)(t >> 1);
            SET_NZ8(r);
            WriteMemory(EA.LowWord, r);
        }
        void asr_ex()
        {
            byte t;
            t = EXTBYTE();
            CLR_NZC();
            cc |= (byte)(t & 0x01);
            t >>= 1;
            t |= (byte)((t & 0x40) << 1);
            SET_NZ8(t);
            WriteMemory(EA.LowWord, t);
        }
        void asl_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8(t, t, r);
            WriteMemory(EA.LowWord, (byte)r);
        }
        void rol_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(cc & 0x01);
            r |= (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8(t, t, r);
            WriteMemory(EA.LowWord, (byte)r);
        }
        void dec_ex()
        {
            byte t;
            t = EXTBYTE();
            --t;
            CLR_NZV();
            SET_FLAGS8D(t);
            WriteMemory(EA.LowWord, (byte)t);
        }
        void tim_di()
        {
            byte t, r;
            t = IMMBYTE();
            r = DIRBYTE();
            r &= t;
            CLR_NZV();
            SET_NZ8(r);
        }
        void inc_ex()
        {
            byte t;
            t = EXTBYTE();
            ++t;
            CLR_NZV();
            SET_FLAGS8I(t);
            WriteMemory(EA.LowWord, t);
        }
        void tst_ex()
        {
            byte t;
            t = EXTBYTE();
            CLR_NZVC();
            SET_NZ8(t);
        }
        void jmp_ex()
        {
            EXTENDED();
            PC.LowWord = EA.LowWord;
            //CHANGE_PC();
        }
        void clr_ex()
        {
            EXTENDED();
            WriteMemory(EA.LowWord, 0);
            CLR_NZVC();
            SEZ();
        }
        void suba_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(D.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void cmpa_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(D.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
        }
        void sbca_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(D.HighByte - t - (cc & 0x01));
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void subd_im()
        {
            uint r, d;
            Register b;
            b = IMMWORD();
            d = D.LowWord;
            r = d - b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
            D.LowWord = (ushort)r;
        }
        void anda_im()
        {
            byte t;
            t = IMMBYTE();
            D.HighByte &= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void bita_im()
        {
            byte t, r;
            t = IMMBYTE();
            r = (byte)(D.HighByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void lda_im()
        {
            D.HighByte = IMMBYTE();
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void sta_im()
        {
            CLR_NZV();
            SET_NZ8(D.HighByte);
            IMM8();
            WriteMemory(EA.LowWord, D.HighByte);
        }
        void eora_im()
        {
            byte t;
            t = IMMBYTE();
            D.HighByte ^= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void adca_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(D.HighByte + t + (cc & 0x01));
            CLR_HNZVC();
            SET_FLAGS8(D.HighByte, t, r);
            SET_H(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void ora_im()
        {
            byte t;
            t = IMMBYTE();
            D.HighByte |= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void adda_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(D.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(D.HighByte, t, r);
            SET_H(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void cmpx_im()
        {
            uint r, d;
            Register b;
            b = IMMWORD();
            d = X.LowWord;
            r = d - b.d;
            CLR_NZV();
            SET_NZ16((ushort)r);
            SET_V16(d, b.d, r);
        }
        void cpx_im()
        {
            uint r, d;
            Register b;
            b = IMMWORD();
            d = X.LowWord;
            r = d - b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
        }
        void bsr()
        {
            byte t;
            t = IMMBYTE();
            PUSHWORD(PC);
            PC.LowWord += (ushort)SIGNED(t);
            //CHANGE_PC();
        }
        void lds_im()
        {
            S = IMMWORD();
            CLR_NZV();
            SET_NZ16(S.LowWord);
        }
        void sts_im()
        {
            CLR_NZV();
            SET_NZ16(S.LowWord);
            IMM16();
            WM16(EA.LowWord, S);
        }
        void suba_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(D.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void cmpa_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(D.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
        }
        void sbca_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(D.HighByte - t - (cc & 0x01));
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void subd_di()
        {
            uint r, d;
            Register b;
            b = DIRWORD();
            d = D.LowWord;
            r = d - b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
            D.LowWord = (ushort)r;
        }
        void anda_di()
        {
            byte t;
            t = DIRBYTE();
            D.HighByte &= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void bita_di()
        {
            byte t, r;
            t = DIRBYTE();
            r = (byte)(D.HighByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void lda_di()
        {
            D.HighByte = DIRBYTE();
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void sta_di()
        {
            CLR_NZV();
            SET_NZ8(D.HighByte);
            DIRECT();
            WriteMemory(EA.LowWord, D.HighByte);
        }
        void eora_di()
        {
            byte t;
            t = DIRBYTE();
            D.HighByte ^= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void adca_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(D.HighByte + t + (cc & 0x01));
            CLR_HNZVC();
            SET_FLAGS8(D.HighByte, t, r);
            SET_H(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void ora_di()
        {
            byte t;
            t = DIRBYTE();
            D.HighByte |= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void adda_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(D.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(D.HighByte, t, r);
            SET_H(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void cmpx_di()
        {
            uint r, d;
            Register b;
            b = DIRWORD();
            d = X.LowWord;
            r = d - b.d;
            CLR_NZV();
            SET_NZ16((ushort)r);
            SET_V16(d, b.d, r);
        }
        void cpx_di()
        {
            uint r, d;
            Register b;
            b = DIRWORD();
            d = X.LowWord;
            r = d - b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
        }
        void jsr_di()
        {
            DIRECT();
            PUSHWORD(PC);
            PC.LowWord = EA.LowWord;
            //CHANGE_PC();
        }
        void lds_di()
        {
            S = DIRWORD();
            CLR_NZV();
            SET_NZ16(S.LowWord);
        }
        void sts_di()
        {
            CLR_NZV();
            SET_NZ16(S.LowWord);
            DIRECT();
            WM16(EA.LowWord, S);
        }
        void suba_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(D.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void cmpa_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(D.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
        }
        void sbca_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(D.HighByte - t - (cc & 0x01));
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void subd_ix()
        {
            uint r, d;
            Register b;
            b = IDXWORD();
            d = D.LowWord;
            r = d - b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
            D.LowWord = (ushort)r;
        }
        void anda_ix()
        {
            byte t;
            t = IDXBYTE();
            D.HighByte &= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void bita_ix()
        {
            byte t, r;
            t = IDXBYTE();
            r = (byte)(D.HighByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void lda_ix()
        {
            D.HighByte = IDXBYTE();
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void sta_ix()
        {
            CLR_NZV();
            SET_NZ8(D.HighByte);
            INDEXED();
            WriteMemory(EA.LowWord, D.HighByte);
        }
        void eora_ix()
        {
            byte t;
            t = IDXBYTE();
            D.HighByte ^= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void adca_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(D.HighByte + t + (cc & 0x01));
            CLR_HNZVC();
            SET_FLAGS8(D.HighByte, t, r);
            SET_H(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void ora_ix()
        {
            byte t;
            t = IDXBYTE();
            D.HighByte |= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void adda_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(D.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(D.HighByte, t, r);
            SET_H(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void cmpx_ix()
        {
            uint r, d;
            Register b;
            b = IDXWORD();
            d = X.LowWord;
            r = d - b.d;
            CLR_NZV();
            SET_NZ16((ushort)r);
            SET_V16(d, b.d, r);
        }
        void cpx_ix()
        {
            uint r, d;
            Register b;
            b = IDXWORD();
            d = X.LowWord;
            r = d - b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
        }
        void jsr_ix()
        {
            INDEXED();
            PUSHWORD(PC);
            PC.LowWord = EA.LowWord;
            //CHANGE_PC();
        }
        void lds_ix()
        {
            S = IDXWORD();
            CLR_NZV();
            SET_NZ16(S.LowWord);
        }
        void sts_ix()
        {
            CLR_NZV();
            SET_NZ16(S.LowWord);
            INDEXED();
            WM16(EA.LowWord, S);
        }
        void suba_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(D.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void cmpa_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(D.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
        }
        void sbca_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(D.HighByte - t - (cc & 0x01));
            CLR_NZVC();
            SET_FLAGS8(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void subd_ex()
        {
            uint r, d;
            Register b;
            b = EXTWORD();
            d = D.LowWord;
            r = d - b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
            D.LowWord = (ushort)r;
        }
        void anda_ex()
        {
            byte t;
            t = EXTBYTE();
            D.HighByte &= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void bita_ex()
        {
            byte t, r;
            t = EXTBYTE();
            r = (byte)(D.HighByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void lda_ex()
        {
            D.HighByte = EXTBYTE();
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void sta_ex()
        {
            CLR_NZV();
            SET_NZ8(D.HighByte);
            EXTENDED();
            WriteMemory(EA.LowWord, D.HighByte);
        }
        void eora_ex()
        {
            byte t;
            t = EXTBYTE();
            D.HighByte ^= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void adca_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(D.HighByte + t + (cc & 0x01));
            CLR_HNZVC();
            SET_FLAGS8(D.HighByte, t, r); SET_H(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void ora_ex()
        {
            byte t;
            t = EXTBYTE();
            D.HighByte |= t;
            CLR_NZV();
            SET_NZ8(D.HighByte);
        }
        void adda_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(D.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(D.HighByte, t, r); SET_H(D.HighByte, t, r);
            D.HighByte = (byte)r;
        }
        void cmpx_ex()
        {
            uint r, d;
            Register b;
            b = EXTWORD();
            d = X.LowWord;
            r = d - b.d;
            CLR_NZV();
            SET_NZ16((ushort)r);
            SET_V16(d, b.d, r);
        }
        void cpx_ex()
        {
            uint r, d;
            Register b;
            b = EXTWORD();
            d = X.LowWord;
            r = d - b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
        }
        void jsr_ex()
        {
            EXTENDED();
            PUSHWORD(PC);
            PC.LowWord = EA.LowWord;
            //CHANGE_PC();
        }
        void lds_ex()
        {
            S = EXTWORD();
            CLR_NZV();
            SET_NZ16(S.LowWord);
        }
        void sts_ex()
        {
            CLR_NZV();
            SET_NZ16(S.LowWord);
            EXTENDED();
            WM16(EA.LowWord, S);
        }
        void subb_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(D.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void cmpb_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(D.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
        }
        void sbcb_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(D.LowByte - t - (cc & 0x01));
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void addd_im()
        {
            uint r, d;
            Register b;
            b = IMMWORD();
            d = D.LowWord;
            r = d + b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
            D.LowWord = (ushort)r;
        }
        void andb_im()
        {
            byte t;
            t = IMMBYTE();
            D.LowByte &= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void bitb_im()
        {
            byte t, r;
            t = IMMBYTE();
            r = (byte)(D.LowByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void ldb_im()
        {
            D.LowByte = IMMBYTE();
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void stb_im()
        {
            CLR_NZV();
            SET_NZ8(D.LowByte);
            IMM8();
            WriteMemory(EA.LowWord, D.LowByte);
        }
        void eorb_im()
        {
            byte t;
            t = IMMBYTE();
            D.LowByte ^= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void adcb_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(D.LowByte + t + (cc & 0x01));
            CLR_HNZVC();
            SET_FLAGS8(D.LowByte, t, r);
            SET_H(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void orb_im()
        {
            byte t;
            t = IMMBYTE();
            D.LowByte |= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void addb_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(D.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(D.LowByte, t, r);
            SET_H(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void ldd_im()
        {
            D = IMMWORD();
            CLR_NZV();
            SET_NZ16(D.LowWord);
        }
        void std_im()
        {
            IMM16();
            CLR_NZV();
            SET_NZ16(D.LowWord);
            WM16(EA.LowWord, D);
        }
        void ldx_im()
        {
            X = IMMWORD();
            CLR_NZV();
            SET_NZ16(X.LowWord);
        }
        void stx_im()
        {
            CLR_NZV();
            SET_NZ16(X.LowWord);
            IMM16();
            WM16(EA.LowWord, X);
        }
        void subb_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(D.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void cmpb_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(D.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
        }
        void sbcb_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(D.LowByte - t - (cc & 0x01));
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void addd_di()
        {
            uint r, d;
            Register b;
            b = DIRWORD();
            d = D.LowWord;
            r = d + b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
            D.LowWord = (ushort)r;
        }
        void andb_di()
        {
            byte t;
            t = DIRBYTE();
            D.LowByte &= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void bitb_di()
        {
            byte t, r;
            t = DIRBYTE();
            r = (byte)(D.LowByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void ldb_di()
        {
            D.LowByte = DIRBYTE();
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void stb_di()
        {
            CLR_NZV();
            SET_NZ8(D.LowByte);
            DIRECT();
            WriteMemory(EA.LowWord, D.LowByte);
        }
        void eorb_di()
        {
            byte t;
            t = DIRBYTE();
            D.LowByte ^= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void adcb_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(D.LowByte + t + (cc & 0x01));
            CLR_HNZVC();
            SET_FLAGS8(D.LowByte, t, r);
            SET_H(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void orb_di()
        {
            byte t;
            t = DIRBYTE();
            D.LowByte |= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void addb_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(D.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(D.LowByte, t, r);
            SET_H(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void ldd_di()
        {
            D = DIRWORD();
            CLR_NZV();
            SET_NZ16(D.LowWord);
        }
        void std_di()
        {
            DIRECT();
            CLR_NZV();
            SET_NZ16(D.LowWord);
            WM16(EA.LowWord, D);
        }
        void ldx_di()
        {
            X = DIRWORD();
            CLR_NZV();
            SET_NZ16(X.LowWord);
        }
        void stx_di()
        {
            CLR_NZV();
            SET_NZ16(X.LowWord);
            DIRECT();
            WM16(EA.LowWord, X);
        }
        void subb_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(D.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void cmpb_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(D.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
        }
        void sbcb_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(D.LowByte - t - (cc & 0x01));
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void addd_ix()
        {
            uint r, d;
            Register b;
            b = IDXWORD();
            d = D.LowWord;
            r = d + b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
            D.LowWord = (ushort)r;
        }
        void andb_ix()
        {
            byte t;
            t = IDXBYTE();
            D.LowByte &= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void bitb_ix()
        {
            byte t, r;
            t = IDXBYTE();
            r = (byte)(D.LowByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void ldb_ix()
        {
            D.LowByte = IDXBYTE();
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void stb_ix()
        {
            CLR_NZV();
            SET_NZ8(D.LowByte);
            INDEXED();
            WriteMemory(EA.LowWord, D.LowByte);
        }
        void eorb_ix()
        {
            byte t;
            t = IDXBYTE();
            D.LowByte ^= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void adcb_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(D.LowByte + t + (cc & 0x01));
            CLR_HNZVC();
            SET_FLAGS8(D.LowByte, t, r);
            SET_H(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void orb_ix()
        {
            byte t;
            t = IDXBYTE();
            D.LowByte |= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void addb_ix()
        {
            ushort t, r;
            t = IDXBYTE();
            r = (ushort)(D.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(D.LowByte, t, r);
            SET_H(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void ldd_ix()
        {
            D = IDXWORD();
            CLR_NZV();
            SET_NZ16(D.LowWord);
        }
        void adcx_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(X.LowWord + t + (cc & 0x01));
            CLR_HNZVC();
            SET_FLAGS8(X.LowWord, t, r); SET_H(X.LowWord, t, r);
            X.LowWord = r;
        }
        void std_ix()
        {
            INDEXED();
            CLR_NZV();
            SET_NZ16(D.LowWord);
            WM16(EA.LowWord, D);
        }
        void ldx_ix()
        {
            X = IDXWORD();
            CLR_NZV();
            SET_NZ16(X.LowWord);
        }
        void stx_ix()
        {
            CLR_NZV();
            SET_NZ16(X.LowWord);
            INDEXED();
            WM16(EA.LowWord, X);
        }
        void subb_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(D.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void cmpb_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(D.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
        }
        void sbcb_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(D.LowByte - t - (cc & 0x01));
            CLR_NZVC();
            SET_FLAGS8(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void addd_ex()
        {
            uint r, d;
            Register b;
            b = EXTWORD();
            d = D.LowWord;
            r = d + b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
            D.LowWord = (ushort)r;
        }
        void andb_ex()
        {
            byte t;
            t = EXTBYTE();
            D.LowByte &= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void bitb_ex()
        {
            byte t, r;
            t = EXTBYTE();
            r = (byte)(D.LowByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void ldb_ex()
        {
            D.LowByte = EXTBYTE();
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void stb_ex()
        {
            CLR_NZV();
            SET_NZ8(D.LowByte);
            EXTENDED();
            WriteMemory(EA.LowWord, D.LowByte);
        }
        void eorb_ex()
        {
            byte t;
            t = EXTBYTE();
            D.LowByte ^= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void adcb_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(D.LowByte + t + (cc & 0x01));
            CLR_HNZVC();
            SET_FLAGS8(D.LowByte, t, r); SET_H(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void orb_ex()
        {
            byte t;
            t = EXTBYTE();
            D.LowByte |= t;
            CLR_NZV();
            SET_NZ8(D.LowByte);
        }
        void addb_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(D.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(D.LowByte, t, r);
            SET_H(D.LowByte, t, r);
            D.LowByte = (byte)r;
        }
        void ldd_ex()
        {
            D = EXTWORD();
            CLR_NZV();
            SET_NZ16(D.LowWord);
        }
        void addx_ex()
        {
            uint r, d;
            Register b;
            b = EXTWORD();
            d = X.LowWord;
            r = d + b.d;
            CLR_NZVC();
            SET_FLAGS16(d, b.d, r);
            X.LowWord = (ushort)r;
        }
        void std_ex()
        {
            EXTENDED();
            CLR_NZV();
            SET_NZ16(D.LowWord);
            WM16(EA.LowWord, D);
        }
        void ldx_ex()
        {
            X = EXTWORD();
            CLR_NZV();
            SET_NZ16(X.LowWord);
        }
        void stx_ex()
        {
            CLR_NZV();
            SET_NZ16(X.LowWord);
            EXTENDED();
            WM16(EA.LowWord, X);
        }
    }
}
