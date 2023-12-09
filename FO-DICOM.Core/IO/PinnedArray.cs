// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Runtime.InteropServices;

namespace FellowOakDicom.IO
{

    public class PinnedArray<T> : IDisposable
    {
        #region Private Members

        private T[] _data;

        private int _size;

        private int _count;

        private GCHandle _handle;

        private IntPtr _pointer;

        #endregion

        #region Public Properties

        public T[] Data => _data;

        public int Count => _count;

        public int ByteSize => _size;

        public IntPtr Pointer => _pointer;

        public T this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }

        #endregion

        #region Public Constructor

        public PinnedArray(int count)
        {
            _count = count;
            _size = Marshal.SizeOf<T>() * _count;
            _data = new T[_count];
            _handle = GCHandle.Alloc(_data, GCHandleType.Pinned);
            _pointer = _handle.AddrOfPinnedObject();
        }

        public PinnedArray(T[] data)
        {
            _count = data.Length;
            _size = Marshal.SizeOf<T>() * _count;
            _data = data;
            _handle = GCHandle.Alloc(_data, GCHandleType.Pinned);
            _pointer = _handle.AddrOfPinnedObject();
        }

        ~PinnedArray()
        {
            Dispose(false);
        }

        #endregion

        #region Public Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static implicit operator IntPtr(PinnedArray<T> array)
        {
            return array._pointer;
        }

        #endregion

        #region Private Members

        private void Dispose(bool disposing)
        {
            if (_data != null)
            {
                if(_handle.IsAllocated)
                {
                    _handle.Free();
                }
                _pointer = IntPtr.Zero;
                _data = null;
            }
        }

        #endregion
    }

    public class PinnedByteArray : PinnedArray<byte>
    {
        public PinnedByteArray(int count)
            : base(count)
        {
        }

        public PinnedByteArray(byte[] data)
            : base(data)
        {
        }
    }

    public class PinnedIntArray : PinnedArray<int>
    {
        public PinnedIntArray(int count)
            : base(count)
        {
        }

        public PinnedIntArray(int[] data)
            : base(data)
        {
        }
    }
}
