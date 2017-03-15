// Copyright (c) Hitcents
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace Unity.IO.Compression {
    /// <summary>
    /// NOTE: this is a hacked in replacement for the SR class
    ///     Unity games don't care about localized exception messages, so we just hacked these in the best we could
    /// </summary>
    internal class SR
    {
        public const string ArgumentOutOfRange_Enum = "Argument out of range";
        public const string CorruptedGZipHeader = "Corrupted gzip header";
        public const string CannotReadFromDeflateStream = "Cannot read from deflate stream";
        public const string CannotWriteToDeflateStream = "Cannot write to deflate stream";
        public const string GenericInvalidData = "Invalid data";
        public const string InvalidCRC = "Invalid CRC";
        public const string InvalidStreamSize = "Invalid stream size";
        public const string InvalidHuffmanData = "Invalid Huffman data";
        public const string InvalidBeginCall = "Invalid begin call";
        public const string InvalidEndCall = "Invalid end call";
        public const string InvalidBlockLength = "Invalid block length";
        public const string InvalidArgumentOffsetCount = "Invalid argument offset count";
        public const string NotSupported = "Not supported";
        public const string NotWriteableStream = "Not a writeable stream";
        public const string NotReadableStream = "Not a readable stream";
        public const string ObjectDisposed_StreamClosed = "Object disposed";
        public const string UnknownState = "Unknown state";
        public const string UnknownCompressionMode = "Unknown compression mode";
        public const string UnknownBlockType = "Unknown block type";

        private SR()
        {
        }

        internal static string GetString(string p)
        {
            //HACK: just return the string passed in, not doing localization
            return p;
        }
    }
}
