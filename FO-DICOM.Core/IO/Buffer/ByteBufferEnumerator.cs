// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FellowOakDicom.IO.Buffer
{

    public abstract class ByteBufferEnumerator<T> : IEnumerable<T>, IEnumerator<T>
    {
        protected ByteBufferEnumerator(IByteBuffer buffer)
        {
            Buffer = buffer;
            UnitSize = Marshal.SizeOf<T>();
            Position = -UnitSize;
        }

        public IByteBuffer Buffer { get; protected set; }

        private byte[] _data;

        protected byte[] Data
        {
            get
            {
                if (_data == null) _data = Buffer.Data;
                return _data;
            }
        }

        protected int Position { get; set; }

        protected int UnitSize { get; set; }

        public void Dispose()
        {
            _data = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public T Current => CurrentItem();

        object System.Collections.IEnumerator.Current => CurrentItem();

        public bool MoveNext()
        {
            Position += UnitSize;
            if (Position <= (Buffer.Size - UnitSize)) return true;
            return false;
        }

        public void Reset()
        {
            Position = -UnitSize;
        }

        protected abstract T CurrentItem();

        public static IEnumerable<T> Create(IByteBuffer buffer)
        {
            if (typeof(T) == typeof(byte)) return (IEnumerable<T>)new ByteBufferByteEnumerator(buffer);
            if (typeof(T) == typeof(sbyte)) return (IEnumerable<T>)new ByteBufferSByteEnumerator(buffer);

            if (typeof(T) == typeof(short)) return (IEnumerable<T>)new ByteBufferInt16Enumerator(buffer);
            if (typeof(T) == typeof(ushort)) return (IEnumerable<T>)new ByteBufferUInt16Enumerator(buffer);

            if (typeof(T) == typeof(int)) return (IEnumerable<T>)new ByteBufferInt32Enumerator(buffer);
            if (typeof(T) == typeof(uint)) return (IEnumerable<T>)new ByteBufferUInt32Enumerator(buffer);

            if (typeof(T) == typeof(long)) return (IEnumerable<T>)new ByteBufferInt64Enumerator(buffer);
            if (typeof(T) == typeof(ulong)) return (IEnumerable<T>)new ByteBufferUInt64Enumerator(buffer);

            if (typeof(T) == typeof(float)) return (IEnumerable<T>)new ByteBufferSingleEnumerator(buffer);
            if (typeof(T) == typeof(double)) return (IEnumerable<T>)new ByteBufferDoubleEnumerator(buffer);

            throw new NotSupportedException(
                "ByteBufferEnumerator<T> only provides support for the base .NET numeric types.");
        }

        protected class ByteBufferByteEnumerator : ByteBufferEnumerator<byte>
        {
            public ByteBufferByteEnumerator(IByteBuffer buffer)
                : base(buffer)
            {
            }

            protected override byte CurrentItem()
            {
                return Data[Position];
            }
        }

        protected class ByteBufferSByteEnumerator : ByteBufferEnumerator<sbyte>
        {
            public ByteBufferSByteEnumerator(IByteBuffer buffer)
                : base(buffer)
            {
            }

            protected override sbyte CurrentItem()
            {
                return (sbyte)Data[Position];
            }
        }

        protected class ByteBufferInt16Enumerator : ByteBufferEnumerator<short>
        {
            public ByteBufferInt16Enumerator(IByteBuffer buffer)
                : base(buffer)
            {
            }

            protected override short CurrentItem()
            {
                return BitConverter.ToInt16(Data, Position);
            }
        }

        protected class ByteBufferUInt16Enumerator : ByteBufferEnumerator<ushort>
        {
            public ByteBufferUInt16Enumerator(IByteBuffer buffer)
                : base(buffer)
            {
            }

            protected override ushort CurrentItem()
            {
                return BitConverter.ToUInt16(Data, Position);
            }
        }

        protected class ByteBufferInt32Enumerator : ByteBufferEnumerator<int>
        {
            public ByteBufferInt32Enumerator(IByteBuffer buffer)
                : base(buffer)
            {
            }

            protected override int CurrentItem()
            {
                return BitConverter.ToInt32(Data, Position);
            }
        }

        protected class ByteBufferUInt32Enumerator : ByteBufferEnumerator<uint>
        {
            public ByteBufferUInt32Enumerator(IByteBuffer buffer)
                : base(buffer)
            {
            }

            protected override uint CurrentItem()
            {
                return BitConverter.ToUInt32(Data, Position);
            }
        }

        protected class ByteBufferInt64Enumerator : ByteBufferEnumerator<long>
        {
            public ByteBufferInt64Enumerator(IByteBuffer buffer)
                : base(buffer)
            {
            }

            protected override long CurrentItem()
            {
                return BitConverter.ToInt64(Data, Position);
            }
        }

        protected class ByteBufferUInt64Enumerator : ByteBufferEnumerator<ulong>
        {
            public ByteBufferUInt64Enumerator(IByteBuffer buffer)
                : base(buffer)
            {
            }

            protected override ulong CurrentItem()
            {
                return BitConverter.ToUInt64(Data, Position);
            }
        }

        protected class ByteBufferSingleEnumerator : ByteBufferEnumerator<float>
        {
            public ByteBufferSingleEnumerator(IByteBuffer buffer)
                : base(buffer)
            {
            }

            protected override float CurrentItem()
            {
                return BitConverter.ToSingle(Data, Position);
            }
        }

        protected class ByteBufferDoubleEnumerator : ByteBufferEnumerator<double>
        {
            public ByteBufferDoubleEnumerator(IByteBuffer buffer)
                : base(buffer)
            {
            }

            protected override double CurrentItem()
            {
                return BitConverter.ToDouble(Data, Position);
            }
        }
    }
}
