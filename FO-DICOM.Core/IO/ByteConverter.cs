// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace FellowOakDicom.IO
{

    public static class ByteConverter
    {

        public static IByteBuffer ToByteBuffer(string value, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;

            byte[] bytes = encoding.GetBytes(value);

            return new MemoryByteBuffer(bytes);
        }

        public static IByteBuffer ToByteBuffer(string value, Encoding encoding, byte padding)
        {
            encoding ??= Encoding.UTF8;

            byte[] bytes = encoding.GetBytes(value);

            if (bytes.Length.IsOdd())
            {
                Array.Resize(ref bytes, bytes.Length + 1);
                bytes[bytes.Length - 1] = padding;
            }

            return new MemoryByteBuffer(bytes);
        }

        public static IByteBuffer ToByteBuffer<T>(T[] values) where T : struct
        {
            int size = System.Buffer.ByteLength(values);
            byte[] data = new byte[size];
            System.Buffer.BlockCopy(values, 0, data, 0, size);
            return new MemoryByteBuffer(data);
        }

        public static T[] ToArray<T>(IByteBuffer buffer)
        {
            uint size = (uint)Marshal.SizeOf<T>();
            uint padding = (uint)(buffer.Size % size);
            uint count = (uint)(buffer.Size / size);
            var values = new T[count];
            System.Buffer.BlockCopy(buffer.Data, 0, values, 0, (int)(buffer.Size - padding));
            return values;
        }

        public static T[] ToArray<T>(IByteBuffer buffer, int bitsAllocated)
        {
            var bytesRequested = Marshal.SizeOf<T>();
            var bitsRequested = 8 * bytesRequested;
            if (bitsAllocated > bitsRequested)
            {
                throw new ArgumentOutOfRangeException(nameof(bitsAllocated), "Bits allocated too large for array type");
            }
            if (bitsAllocated == bitsRequested)
            {
                return ToArray<T>(buffer);
            }

            var count = (int)(8 * buffer.Size / bitsAllocated);
            var src = buffer.Data;
            var dst = new byte[bytesRequested * count];

            for (int j = 0, sij = 0; j < count; ++j)
            {
                for (int i = 0, dij = j * bitsRequested; i < bitsAllocated; ++i, ++sij, ++dij)
                {
                    if ((src[sij / 8] & (1 << (sij % 8))) != 0)
                    {
                        dst[dij / 8] |= (byte)(1 << (dij % 8));
                    }
                }
            }

            var values = new T[count];
            System.Buffer.BlockCopy(dst, 0, values, 0, dst.Length);

            return values;
        }

        public static T Get<T>(IByteBuffer buffer, int n)
        {
            int size = Marshal.SizeOf<T>();
            var values = new T[1];
            if (buffer.IsMemory)
            {
                System.Buffer.BlockCopy(buffer.Data, size * n, values, 0, size);
            }
            else
            {
                var memoryProvider = Setup.ServiceProvider.GetRequiredService<IMemoryProvider>();
                using var temp = memoryProvider.Provide(size);
                buffer.GetByteRange(size * n, size, temp.Bytes);
                System.Buffer.BlockCopy(temp.Bytes, 0, values, 0, size);
            }
            return values[0];
        }

        public static IByteBuffer UnpackLow16(IByteBuffer data)
        {
            byte[] bytes = new byte[data.Size / 2];
            byte[] datab = data.Data;
            for (int i = 0; i < bytes.Length && (i * 2) < datab.Length; i++)
            {
                bytes[i] = datab[i * 2];
            }
            return new MemoryByteBuffer(bytes);
        }

        public static IByteBuffer UnpackHigh16(IByteBuffer data)
        {
            byte[] bytes = new byte[data.Size / 2];
            byte[] datab = data.Data;
            for (int i = 0; i < bytes.Length && ((i * 2) + 1) < datab.Length; i++)
            {
                bytes[i] = datab[(i * 2) + 1];
            }
            return new MemoryByteBuffer(bytes);
        }
    }
}
