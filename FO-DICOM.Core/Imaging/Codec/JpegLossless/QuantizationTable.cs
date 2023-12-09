// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;

namespace FellowOakDicom.Imaging.Codec.JpegLossless
{

    internal class QuantizationTable
    {

        private readonly int[] _precision = new int[4]; // Quantization precision 8 or 16
        private readonly int[] _tq = new int[4]; // 1: this table is presented

        internal int[][] quantTables = JaggedArrayFactory.Create<int>(4, 64); // Tables


        public QuantizationTable()
        {
            _tq[0] = 0;
            _tq[1] = 0;
            _tq[2] = 0;
            _tq[3] = 0;
        }


        internal int Read(IDataStream data, int[] table)
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
                    throw new IOException("ERROR: Quantization table ID > 3");
                }

                _precision[t] = temp >> 4;

                if (_precision[t] == 0)
                {
                    _precision[t] = 8;
                }
                else if (_precision[t] == 1)
                {
                    _precision[t] = 16;
                }
                else
                {
                    throw new IOException("ERROR: Quantization table precision error");
                }

                _tq[t] = 1;

                if (_precision[t] == 8)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (count > length)
                        {
                            throw new IOException("ERROR: Quantization table format error");
                        }

                        quantTables[t][i] = data.Get8();
                        count++;
                    }

                    EnhanceQuantizationTable(quantTables[t], table);
                }
                else
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (count > length)
                        {
                            throw new IOException("ERROR: Quantization table format error");
                        }

                        quantTables[t][i] = data.Get16();
                        count += 2;
                    }

                    EnhanceQuantizationTable(quantTables[t], table);
                }
            }

            if (count != length)
            {
                throw new IOException("ERROR: Quantization table error [count!=Lq]");
            }

            return 1;
        }


        private void EnhanceQuantizationTable(int[] qtab, int[] table)
        {
            for (int i = 0; i < 8; i++)
            {
                qtab[table[(0 * 8) + i]] *= 90;
                qtab[table[(4 * 8) + i]] *= 90;
                qtab[table[(2 * 8) + i]] *= 118;
                qtab[table[(6 * 8) + i]] *= 49;
                qtab[table[(5 * 8) + i]] *= 71;
                qtab[table[(1 * 8) + i]] *= 126;
                qtab[table[(7 * 8) + i]] *= 25;
                qtab[table[(3 * 8) + i]] *= 106;
            }

            for (int i = 0; i < 8; i++)
            {
                qtab[table[0 + (8 * i)]] *= 90;
                qtab[table[4 + (8 * i)]] *= 90;
                qtab[table[2 + (8 * i)]] *= 118;
                qtab[table[6 + (8 * i)]] *= 49;
                qtab[table[5 + (8 * i)]] *= 71;
                qtab[table[1 + (8 * i)]] *= 126;
                qtab[table[7 + (8 * i)]] *= 25;
                qtab[table[3 + (8 * i)]] *= 106;
            }

            for (int i = 0; i < 64; i++)
            {
                qtab[i] >>= 6;
            }
        }


    }
}
