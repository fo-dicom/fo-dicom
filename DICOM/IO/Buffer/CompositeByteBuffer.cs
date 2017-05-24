﻿// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicom.IO.Buffer
{
    public class CompositeByteBuffer : IByteBuffer
    {
        public CompositeByteBuffer(params IByteBuffer[] buffers)
        {
            Buffers = new List<IByteBuffer>(buffers);
        }

        public IList<IByteBuffer> Buffers { get; private set; }

        public bool IsMemory
        {
            get
            {
                return true;
            }
        }

        public uint Size
        {
            get
            {
                return (uint)Buffers.Sum(x => x.Size);
            }
        }

        public byte[] Data
        {
            get
            {
                byte[] data = new byte[Size];
                int offset = 0;
                foreach (IByteBuffer buffer in Buffers)
                {
                    System.Buffer.BlockCopy(buffer.Data, 0, data, offset, (int)buffer.Size);
                    offset += (int)buffer.Size;
                }
                return data;
            }
        }

        public byte[] GetByteRange(int offset, int count)
        {
            int pos = 0;
            for (; pos < Buffers.Count && offset > Buffers[pos].Size; pos++) offset -= (int)Buffers[pos].Size;

            int offset2 = 0;
            byte[] data = new byte[count];
            for (; pos < Buffers.Count && count > 0; pos++)
            {
                int remain = Math.Min((int)Buffers[pos].Size - offset, count);

                if (Buffers[pos].IsMemory)
                {
                    try
                    {
                        System.Buffer.BlockCopy(Buffers[pos].Data, offset, data, offset2, remain);
                    }
                    catch (Exception)
                    {
                        data = Buffers[pos].Data.ToArray();
                    }

                }

                else
                {
                    byte[] temp = Buffers[pos].GetByteRange(offset, remain);
                    System.Buffer.BlockCopy(temp, 0, data, offset2, remain);
                }

                count -= remain;
                offset2 += remain;
                if (offset > 0) offset = 0;
            }

            return data;
        }
    }
}
