// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Memory
{
    /// <summary>
    /// Represents a (possibly rented) chunk of memory that is temporarily available to use
    /// </summary>
    public interface IMemory : IDisposable
    {
        /// <summary>
        /// The originally requested length of the memory
        /// </summary>
        int Length { get; }
        
        /// <summary>
        /// The underlying byte array. Note that this will probably be longer than the requested length.
        /// Always use <see cref="Length"/> instead of the length property of the <see cref="Bytes"/>.
        /// Prefer to use <see cref="Span"/> or <see cref="Memory"/> if possible
        /// </summary>
        byte[] Bytes { get; }
        
        /// <summary>
        /// An instance of span wrapping the memory (for use in synchronous methods)
        /// </summary>
        Span<byte> Span { get; }
        
        /// <summary>
        /// An instance of memory wrapping the memory (for use in asynchronous methods)
        /// </summary>
        Memory<byte> Memory { get; }
    }
}
