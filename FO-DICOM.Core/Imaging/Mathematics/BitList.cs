// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;

namespace FellowOakDicom.Imaging.Mathematics
{

    public class BitList
    {

        public BitList()
        {
            List = new List<byte>();
        }

        public int Capacity
        {
            get => List.Count * 8;
            set
            {
                int count = value / 8;
                if (value % 8 > 0)
                {
                    count++;
                }

                while (List.Count < count)
                {
                    List.Add(0);
                }
            }
        }

        public List<byte> List { get; }

        public byte[] Array => List.ToArray();

        public bool this[int pos]
        {
            get
            {
                int p = pos / 8;
                int m = pos % 8;

                if (p >= List.Count)
                {
                    return false;
                }

                var b = List[p];

                return (b & (1 << m)) != 0;
            }
            set
            {
                int p = pos / 8;
                int m = pos % 8;

                if (p >= List.Count)
                {
                    Capacity = pos + 1;
                }

                if (value)
                {
                    List[p] |= (byte)(1 << m);
                }
                else
                {
                    List[p] -= (byte)(1 << m);
                }
            }
        }
    }
}
