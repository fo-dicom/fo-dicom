// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;

namespace FellowOakDicom.Imaging.Codec.JpegLossless
{

    internal class HuffmanTable
    {

        private readonly int[,][] _l = JaggedArrayFactory.Create<int>(4, 2, 16);
        private readonly int[] _th = new int[4]; // 1: this table is presented
        private readonly int[,][,] _v = JaggedArrayFactory.Create<int>(4, 2, 16, 200); // tables
        private readonly int[,] _tc = new int[4, 2]; // 1: this table is presented

        public static int MSB = -2147483648; // 0x80000000


        public HuffmanTable()
        {
            _tc[0, 0] = 0;
            _tc[1, 0] = 0;
            _tc[2, 0] = 0;
            _tc[3, 0] = 0;
            _tc[0, 1] = 0;
            _tc[1, 1] = 0;
            _tc[2, 1] = 0;
            _tc[3, 1] = 0;
            _th[0] = 0;
            _th[1] = 0;
            _th[2] = 0;
            _th[3] = 0;
        }


        internal int Read(IDataStream data, int[,][] HuffTab)
        {
            int count = 0;
            int length = data.Get16();
            count += 2;

            while (count < length)
            {
                int temp = data.Get8();
                count++;
                int t = temp & 0x0F;
                if (t > 3)
                {
                    throw new IOException("ERROR: Huffman table ID > 3");
                }

                int c = temp >> 4;
                if (c > 2)
                {
                    throw new IOException("ERROR: Huffman table [Table class > 2 ]");
                }

                _th[t] = 1;
                _tc[t, c] = 1;

                for (int i = 0; i < 16; i++)
                {
                    _l[t, c][i] = data.Get8();
                    count++;
                }

                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < _l[t, c][i]; j++)
                    {
                        if (count > length)
                        {
                            throw new IOException("ERROR: Huffman table format error [count>Lh]");
                        }
                        _v[t, c][i, j] = data.Get8();
                        count++;
                    }
                }
            }

            if (count != length)
            {
                throw new IOException("ERROR: Huffman table format error [count!=Lf]");
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (_tc[i, j] != 0)
                    {
                        BuildHuffTable(HuffTab[i, j], _l[i, j], _v[i, j]);
                    }
                }
            }

            return 1;
        }


        //	Build_HuffTab()
        //	Parameter:  t       table ID
        //	            c       table class ( 0 for DC, 1 for AC )
        //	            L[i]    # of codewords which length is i
        //	            V[i][j] Huffman Value (length=i)
        //	Effect:
        //	    build up HuffTab[t][c] using L and V.
        private void BuildHuffTable(int[] tab, int[] L, int[,] V)
        {
            int currentTable, temp;
            int k;
            temp = 256;
            k = 0;

            for (int i = 0; i < 8; i++)
            { // i+1 is Code length
                for (int j = 0; j < L[i]; j++)
                {
                    for (int n = 0; n < (temp >> (i + 1)); n++)
                    {
                        tab[k] = V[i, j] | ((i + 1) << 8);
                        k++;
                    }
                }
            }

            for (int i = 1; k < 256; i++, k++)
            {
                tab[k] = i | MSB;
            }

            currentTable = 1;
            k = 0;

            for (int i = 8; i < 16; i++)
            { // i+1 is Code length
                for (int j = 0; j < L[i]; j++)
                {
                    for (int n = 0; n < (temp >> (i - 7)); n++)
                    {
                        tab[(currentTable * 256) + k] = V[i, j] | ((i + 1) << 8);
                        k++;
                    }
                    if (k >= 256)
                    {
                        if (k > 256)
                        {
                            throw new IOException("ERROR: Huffman table error(1)!");
                        }
                        k = 0;
                        currentTable++;
                    }
                }
            }
        }
    }
}
