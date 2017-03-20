// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace System.IO
{
    /// <summary>
    /// Extension method mimicking <see cref="Stream"/> API methods missing in .NET 3.5/Unity
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Copy contents of <paramref name="source"/> stream to <paramref name="destination"/> stream.
        /// </summary>
        /// <param name="source">Stream from which data should be copied.</param>
        /// <param name="destination">Stream to which data should be copied.</param>
        public static void CopyTo(this Stream source, Stream destination)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));

            if (source.CanSeek)
            {
                var count = (int) (source.Length - source.Position);
                var bytes = new byte[count];

                count = source.Read(bytes, 0, count);
                destination.Write(bytes, 0, count);
            }
            else
            {
                int b;
                while ((b = source.ReadByte()) >= 0)
                {
                    destination.WriteByte((byte) b);
                }
            }
        }
    }
}