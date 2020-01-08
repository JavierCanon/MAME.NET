using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace mame
{
    public partial class Inptport
    {
        private static List<KeyStruct> lks;
        public static List<Key> lk;
        public class KeyStruct
        {
            public Key key;
            public char c;
            public KeyStruct(Key _key, char _c)
            {
                key = _key;
                c = _c;
            }
        }
        public static char getcharbykey(Key key1)
        {
            char c1=' ';
            foreach (KeyStruct ks in lks)
            {
                if (ks.key == key1)
                {
                    c1 = ks.c;
                    break;
                }
            }
            return c1;
        }
        public static void input_init()
        {
            lk = new List<Key>();
            lk.Add(Key.D1);
            lk.Add(Key.D2);
            lk.Add(Key.D3);
            lk.Add(Key.D4);
            lk.Add(Key.D5);
            lk.Add(Key.D6);
            lk.Add(Key.D7);
            lk.Add(Key.D8);
            lk.Add(Key.D9);
            lk.Add(Key.D0);
            lk.Add(Key.A);
            lk.Add(Key.B);
            lk.Add(Key.C);
            lk.Add(Key.D);
            lk.Add(Key.E);
            lk.Add(Key.F);
            lk.Add(Key.G);
            lk.Add(Key.H);
            lk.Add(Key.I);
            lk.Add(Key.J);
            lk.Add(Key.K);
            lk.Add(Key.L);
            lk.Add(Key.M);
            lk.Add(Key.N);
            lk.Add(Key.O);
            lk.Add(Key.P);
            lk.Add(Key.Q);
            lk.Add(Key.R);
            lk.Add(Key.S);
            lk.Add(Key.T);
            lk.Add(Key.U);
            lk.Add(Key.V);
            lk.Add(Key.W);
            lk.Add(Key.X);
            lk.Add(Key.Y);
            lk.Add(Key.Z);
            lks = new List<KeyStruct>();
            lks.Add(new KeyStruct(Key.D1, '1'));
            lks.Add(new KeyStruct(Key.D2, '2'));
            lks.Add(new KeyStruct(Key.D3, '3'));
            lks.Add(new KeyStruct(Key.D4, '4'));
            lks.Add(new KeyStruct(Key.D5, '5'));
            lks.Add(new KeyStruct(Key.D6, '6'));
            lks.Add(new KeyStruct(Key.D7, '7'));
            lks.Add(new KeyStruct(Key.D8, '8'));
            lks.Add(new KeyStruct(Key.D9, '9'));
            lks.Add(new KeyStruct(Key.D0, '0'));
            lks.Add(new KeyStruct(Key.A, 'a'));
            lks.Add(new KeyStruct(Key.B, 'b'));
            lks.Add(new KeyStruct(Key.C, 'c'));
            lks.Add(new KeyStruct(Key.D, 'd'));
            lks.Add(new KeyStruct(Key.E, 'e'));
            lks.Add(new KeyStruct(Key.F, 'f'));
            lks.Add(new KeyStruct(Key.G, 'g'));
            lks.Add(new KeyStruct(Key.H, 'h'));
            lks.Add(new KeyStruct(Key.I, 'i'));
            lks.Add(new KeyStruct(Key.J, 'j'));
            lks.Add(new KeyStruct(Key.K, 'k'));
            lks.Add(new KeyStruct(Key.L, 'l'));
            lks.Add(new KeyStruct(Key.M, 'm'));
            lks.Add(new KeyStruct(Key.N, 'n'));
            lks.Add(new KeyStruct(Key.O, 'o'));
            lks.Add(new KeyStruct(Key.P, 'p'));
            lks.Add(new KeyStruct(Key.Q, 'q'));
            lks.Add(new KeyStruct(Key.R, 'r'));
            lks.Add(new KeyStruct(Key.S, 's'));
            lks.Add(new KeyStruct(Key.T, 't'));
            lks.Add(new KeyStruct(Key.U, 'u'));
            lks.Add(new KeyStruct(Key.V, 'v'));
            lks.Add(new KeyStruct(Key.W, 'w'));
            lks.Add(new KeyStruct(Key.X, 'x'));
            lks.Add(new KeyStruct(Key.Y, 'y'));
            lks.Add(new KeyStruct(Key.Z, 'z'));
        }
    }
}
