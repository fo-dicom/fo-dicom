// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.IO.Buffer
{

    public class SwapByteBuffer : IByteBuffer
    {

        public SwapByteBuffer(IByteBuffer buffer, int unitSize)
        {
            Internal = buffer;
            UnitSize = unitSize;
        }

        public IByteBuffer Internal { get; private set; }

        public int UnitSize { get; private set; }

        public bool IsMemory => Internal.IsMemory;

        public long Size=> Internal.Size;

        public byte[] Data
        {
            get
            {
                byte[] data = null;
                if (IsMemory)
                {
                    data = new byte[Size];
                    System.Buffer.BlockCopy(Internal.Data, 0, data, 0, data.Length);
                }
                else
                {
                    data = Internal.Data;
                }

                Endian.SwapBytes(UnitSize, data);

                return data;
            }
        }

        public byte[] GetByteRange(long offset, int count)
        {
            byte[] data = Internal.GetByteRange(offset, count);
            Endian.SwapBytes(UnitSize, data);
            return data;
        }
    }
}
