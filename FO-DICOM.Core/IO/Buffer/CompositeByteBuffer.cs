// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Buffer
{

    /// <summary>
    /// Implementation of an <see cref="IByteBuffer"/> consisting of a collection of <see cref="IByteBuffer"/> instances.
    /// </summary>
    public class CompositeByteBuffer : IByteBuffer
    {
        private readonly IMemoryProvider _memoryProvider;

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="CompositeByteBuffer"/> class.
        /// </summary>
        /// <param name="buffers">Collection of buffers to initially constitute the <see cref="CompositeByteBuffer"/> instance.</param>
        public CompositeByteBuffer(IEnumerable<IByteBuffer> buffers): this(Setup.ServiceProvider.GetRequiredService<IMemoryProvider>(), buffers)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CompositeByteBuffer"/> class.
        /// </summary>
        /// <param name="buffers">Array of buffers to initially constitute the <see cref="CompositeByteBuffer"/> instance.</param>
        public CompositeByteBuffer(params IByteBuffer[] buffers): this(Setup.ServiceProvider.GetRequiredService<IMemoryProvider>(), buffers)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CompositeByteBuffer"/> class.
        /// </summary>
        /// <param name="memoryProvider">The memory provider that will be used to allocate buffers</param>
        /// <param name="buffers">Collection of buffers to initially constitute the <see cref="CompositeByteBuffer"/> instance.</param>
        public CompositeByteBuffer(IMemoryProvider memoryProvider, IEnumerable<IByteBuffer> buffers)
        {
            _memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
            Buffers = new List<IByteBuffer>(buffers);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CompositeByteBuffer"/> class.
        /// </summary>
        /// <param name="memoryProvider">The memory provider that will be used to allocate buffers</param>
        /// <param name="buffers">Array of buffers to initially constitute the <see cref="CompositeByteBuffer"/> instance.</param>
        public CompositeByteBuffer(IMemoryProvider memoryProvider, params IByteBuffer[] buffers)
        {
            _memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
            Buffers = new List<IByteBuffer>(buffers);
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the collection of <see cref="IByteBuffer"/> constituting the <see cref="CompositeByteBuffer"/>.
        /// </summary>
        public IList<IByteBuffer> Buffers { get; }

        /// <inheritdoc />
        public bool IsMemory
        {
            get
            {
                foreach (var b in Buffers)
                {
                    if (!b.IsMemory)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <inheritdoc />
        public long Size => Buffers.Sum(x => x.Size);

        /// <inheritdoc />
        public byte[] Data
        {
            get
            {
                var data = new byte[Size];
                var offset = 0;
                foreach (var buffer in Buffers)
                {
                    System.Buffer.BlockCopy(buffer.Data, 0, data, offset, (int)buffer.Size);
                    offset += (int)buffer.Size;
                }
                return data;
            }
        }

        #endregion

        #region METHODS
        
        /// <inheritdoc />
        public void GetByteRange(long offset, int count, byte[] output)
        {
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            if (output.Length < count)
            {
                throw new ArgumentException($"Output array with {output.Length} bytes cannot fit {count} bytes of data");
            }
            
            var pos = 0;
            for (; pos < Buffers.Count && offset > Buffers[pos].Size; pos++)
            {
                offset -= Buffers[pos].Size;
            }

            var offset2 = 0;
            for (; pos < Buffers.Count && count > 0; pos++)
            {
                var remain = (int)Math.Min(Buffers[pos].Size - offset, count);

                if (Buffers[pos].IsMemory)
                {
                   System.Buffer.BlockCopy(Buffers[pos].Data, (int)offset, output, offset2, remain);
                }
                else
                {
                    using IMemory temp = _memoryProvider.Provide(remain);
                    Buffers[pos].GetByteRange(offset, remain, temp.Bytes);
                    System.Buffer.BlockCopy(temp.Bytes, 0, output, offset2, remain);
                }

                count -= remain;
                offset2 += remain;
                if (offset > 0)
                {
                    offset = 0;
                }
            }
        }

        public void CopyToStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new InvalidOperationException("Cannot copy to non-writable stream");
            }
            
            foreach (var buffer in Buffers)
            {
                buffer.CopyToStream(stream);
            }
        }

        public async Task CopyToStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new InvalidOperationException("Cannot copy to non-writable stream");
            }
            
            foreach (var buffer in Buffers)
            {
                await buffer.CopyToStreamAsync(stream, cancellationToken).ConfigureAwait(false);
            }
        }

        #endregion
    }
}
