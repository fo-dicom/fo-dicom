// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace FellowOakDicom.IO
{
    /// <summary>
    /// Factory for creating a stream byte source for reading.
    /// </summary>
    public static class StreamByteSourceFactory
    {
        /// <summary>
        /// Returns a newly created  instance of a stream byte source class.
        /// The actual class depends on the stream capabilities.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        /// <param name="readOption">Defines the handling of large tags.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not.
        /// If 0 is passed, then the default of 64k is used.</param>
        /// <param name="useRentedMemoryByteBuffers"></param>
        public static IByteSource Create(Stream stream, FileReadOption readOption, int largeObjectSize, bool useRentedMemoryByteBuffers)
        {
            var memoryProvider = Setup.ServiceProvider.GetRequiredService<IMemoryProvider>();
            
            if (stream.CanSeek)
            {
                return new StreamByteSource(stream, readOption, largeObjectSize, memoryProvider, useRentedMemoryByteBuffers);
            }

            
            return new UnseekableStreamByteSource(stream, readOption, largeObjectSize, memoryProvider);
        }
    }
}