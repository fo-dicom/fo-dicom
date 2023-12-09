// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using System;
using System.IO;

namespace FellowOakDicom.Imaging.Codec
{
    /// <summary>
    /// Implementation of the RLE codec for Mono based platforms.
    /// </summary>
    public class DicomRleCodecImpl : DicomRleCodec
    {
        #region METHODS

        public override void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            var pixelCount = oldPixelData.Width * oldPixelData.Height;
            var numberOfSegments = oldPixelData.BytesAllocated * oldPixelData.SamplesPerPixel;

            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var frameData = oldPixelData.GetFrame(frame);
                var frameArray = frameData.Data;

                using var encoder = new RLEEncoder();
                for (var s = 0; s < numberOfSegments; s++)
                {
                    encoder.NextSegment();

                    var sample = s / oldPixelData.BytesAllocated;
                    var sabyte = s % oldPixelData.BytesAllocated;

                    int pos;
                    int offset;

                    if (newPixelData.PlanarConfiguration == PlanarConfiguration.Interleaved)
                    {
                        pos = sample * oldPixelData.BytesAllocated;
                        offset = numberOfSegments;
                    }
                    else
                    {
                        pos = sample * oldPixelData.BytesAllocated * pixelCount;
                        offset = oldPixelData.BytesAllocated;
                    }

                    pos += oldPixelData.BytesAllocated - sabyte - 1;

                    for (var p = 0; p < pixelCount; p++)
                    {
                        if (pos >= frameArray.Length)
                        {
                            throw new InvalidOperationException("Read position is past end of frame buffer");
                        }
                        encoder.Encode(frameArray[pos]);
                        pos += offset;
                    }
                    encoder.Flush();
                }

                encoder.MakeEvenLength();

                var data = encoder.GetBuffer();
                newPixelData.AddFrame(data);
            }
        }

        public override void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var rleData = oldPixelData.GetFrame(frame);

                // Create new frame data of even length
                var frameSize = newPixelData.UncompressedFrameSize;
                if ((frameSize & 1) == 1)
                {
                    ++frameSize;
                }

                var frameData = new MemoryByteBuffer(new byte[frameSize]);

                var pixelCount = oldPixelData.Width * oldPixelData.Height;
                var numberOfSegments = oldPixelData.BytesAllocated * oldPixelData.SamplesPerPixel;

                var decoder = new RLEDecoder(rleData);

                if (decoder.NumberOfSegments != numberOfSegments)
                {
                    throw new InvalidOperationException("Unexpected number of RLE segments!");
                }

                for (var s = 0; s < numberOfSegments; s++)
                {
                    var sample = s / newPixelData.BytesAllocated;
                    var sabyte = s % newPixelData.BytesAllocated;

                    int pos, offset;

                    if (newPixelData.PlanarConfiguration == PlanarConfiguration.Interleaved)
                    {
                        pos = sample * newPixelData.BytesAllocated;
                        offset = newPixelData.SamplesPerPixel * newPixelData.BytesAllocated;
                    }
                    else
                    {
                        pos = sample * newPixelData.BytesAllocated * pixelCount;
                        offset = newPixelData.BytesAllocated;
                    }

                    pos += newPixelData.BytesAllocated - sabyte - 1;
                    decoder.DecodeSegment(s, frameData, pos, offset);
                }

                newPixelData.AddFrame(frameData);
            }
        }

        #endregion

        #region INNER TYPES

        private sealed class RLEEncoder : IDisposable
        {
            #region FIELDS

            private bool _disposed = false;

            private int _count;

            private readonly uint[] _offsets;

            private readonly MemoryStream _stream;

            private readonly BinaryWriter _writer;

            private readonly byte[] _buffer;

            private int _prevByte;

            private int _repeatCount;

            private int _bufferPos;

            #endregion

            #region CONSTRUCTORS

            internal RLEEncoder()
            {
                Length = 0;
                _count = 0;
                _offsets = new uint[15];
                _stream = new MemoryStream();
                _writer = EndianBinaryWriter.Create(_stream, Endian.Little, false);
                _buffer = new byte[132];

                // Write header
                AppendUInt32((uint)_count);
                for (var i = 0; i < 15; i++)
                {
                    AppendUInt32(_offsets[i]);
                }

                _prevByte = -1;
                _repeatCount = 0;
                _bufferPos = 0;
            }

            #endregion

            #region PROPERTIES

            internal long Length { get; private set; }

            #endregion

            #region METHODS

            public void Dispose()
            {
                if (_disposed)
                {
                    return;
                }

                _writer.Dispose();
                _stream.Dispose();

                _disposed = true;
            }

            internal IByteBuffer GetBuffer()
            {
                Flush();

                // Re-write header
                _stream.Seek(0, SeekOrigin.Begin);
                _writer.Write((uint)_count);
                for (var i = 0; i < 15; i++)
                {
                    _writer.Write(_offsets[i]);
                }

                return new MemoryByteBuffer(_stream.ToArray());
            }

            internal void NextSegment()
            {
                Flush();
                if ((Length & 1) == 1)
                {
                    AppendByte(0x00);
                }
                _offsets[_count++] = (uint)Length;
            }

            internal void Encode(byte b)
            {
                if (b == _prevByte)
                {
                    _repeatCount++;

                    if (_repeatCount > 2 && _bufferPos > 0)
                    {
                        // We're starting a run, flush out the buffer
                        while (_bufferPos > 0)
                        {
                            var count = Math.Min(128, _bufferPos);
                            AppendByte((byte)(count - 1));
                            MoveBuffer(count);
                        }
                    }
                    else if (_repeatCount > 128)
                    {
                        var count = Math.Min(_repeatCount, 128);
                        AppendByte((byte)(257 - count));
                        AppendByte((byte)_prevByte);
                        _repeatCount -= count;
                    }
                }
                else
                {
                    switch (_repeatCount)
                    {
                        case 0:
                            break;
                        case 1:
                            {
                                _buffer[_bufferPos++] = (byte)_prevByte;
                                break;
                            }
                        case 2:
                            {
                                _buffer[_bufferPos++] = (byte)_prevByte;
                                _buffer[_bufferPos++] = (byte)_prevByte;
                                break;
                            }
                        default:
                            {
                                while (_repeatCount > 0)
                                {
                                    var count = Math.Min(_repeatCount, 128);
                                    AppendByte((byte)(257 - count));
                                    AppendByte((byte)_prevByte);
                                    _repeatCount -= count;
                                }
                                break;
                            }
                    }

                    while (_bufferPos > 128)
                    {
                        var count = Math.Min(128, _bufferPos);
                        AppendByte((byte)(count - 1));
                        MoveBuffer(count);
                    }

                    _prevByte = b;
                    _repeatCount = 1;
                }
            }

            internal void MakeEvenLength()
            {
                if ((Length & 1) == 1) { AppendByte(0x00); }
            }

            internal void Flush()
            {
                if (_repeatCount < 2)
                {
                    while (_repeatCount > 0)
                    {
                        _buffer[_bufferPos++] = (byte)_prevByte;
                        _repeatCount--;
                    }
                }

                while (_bufferPos > 0)
                {
                    var count = Math.Min(128, _bufferPos);
                    AppendByte((byte)(count - 1));
                    MoveBuffer(count);
                }

                if (_repeatCount >= 2)
                {
                    while (_repeatCount > 0)
                    {
                        var count = Math.Min(_repeatCount, 128);
                        AppendByte((byte)(257 - count));
                        AppendByte((byte)_prevByte);
                        _repeatCount -= count;
                    }
                }

                _prevByte = -1;
                _repeatCount = 0;
                _bufferPos = 0;
            }

            private void MoveBuffer(int count)
            {
                AppendBytes(_buffer, 0, count);
                for (int i = count, n = 0; i < _bufferPos; i++, n++)
                {
                    _buffer[n] = _buffer[i];
                }
                _bufferPos -= count;
            }

            private void AppendBytes(byte[] bytes, int offset, int count)
            {
                _writer.Write(bytes, offset, count);
                Length += count;
            }

            private void AppendByte(byte value)
            {
                _writer.Write(value);
                ++Length;
            }

            private void AppendUInt32(uint value)
            {
                _writer.Write(value);
                Length += 4;
            }

            #endregion
        }

        private class RLEDecoder
        {
            #region FIELDS

            private readonly int[] _offsets;

            private readonly byte[] _data;

            #endregion

            #region CONSTRUCTORS

            internal RLEDecoder(IByteBuffer data)
            {
                var source = new ByteBufferByteSource(data) { Endian = Endian.Little };
                NumberOfSegments = source.GetInt32();

                _offsets = new int[15];
                for (var i = 0; i < 15; ++i)
                {
                    _offsets[i] = source.GetInt32();
                }

                _data = data.Data;
            }

            #endregion

            #region PROPERTIES

            internal int NumberOfSegments { get; }

            #endregion

            #region METHODS

            internal void DecodeSegment(int segment, IByteBuffer buffer, int start, int sampleOffset)
            {
                if (segment < 0 || segment >= NumberOfSegments)
                {
                    throw new ArgumentOutOfRangeException("Segment number out of range");
                }

                var offset = GetSegmentOffset(segment);
                var length = GetSegmentLength(segment);

                Decode(buffer, start, sampleOffset, _data, offset, length);
            }

            private static void Decode(IByteBuffer buffer, int start, int sampleOffset, byte[] rleData, int offset, int count)
            {
                var pos = start;
                var end = offset + count;
                var bufferLength = buffer.Size;

                for (var i = offset; i < end && pos < bufferLength;)
                {
                    var control = (sbyte)rleData[i++];

                    if (control >= 0)
                    {
                        var length = control + 1;

                        if ((end - i) < length)
                        {
                            throw new InvalidOperationException("RLE literal run exceeds input buffer length.");
                        }
                        if ((pos + ((length - 1) * sampleOffset)) >= bufferLength)
                        {
                            throw new InvalidOperationException("RLE literal run exceeds output buffer length.");
                        }

                        if (sampleOffset == 1)
                        {
                            for (var j = 0; j < length; ++j, ++i, ++pos)
                            {
                                buffer.Data[pos] = rleData[i];
                            }
                        }
                        else
                        {
                            while (length-- > 0)
                            {
                                buffer.Data[pos] = rleData[i++];
                                pos += sampleOffset;
                            }
                        }
                    }
                    else if (control >= -127)
                    {
                        int length = -control;

                        if ((pos + ((length - 1) * sampleOffset)) >= bufferLength)
                        {
                            throw new InvalidOperationException("RLE repeat run exceeds output buffer length.");
                        }

                        var b = rleData[i++];

                        if (sampleOffset == 1)
                        {
                            while (length-- >= 0)
                            {
                                buffer.Data[pos++] = b;
                            }
                        }
                        else
                        {
                            while (length-- >= 0)
                            {
                                buffer.Data[pos] = b;
                                pos += sampleOffset;
                            }
                        }
                    }

                    if ((i + 1) >= end)
                    {
                        break;
                    }
                }
            }

            private int GetSegmentOffset(int segment)
            {
                return _offsets[segment];
            }

            private int GetSegmentLength(int segment)
            {
                var offset = GetSegmentOffset(segment);
                if (segment < (NumberOfSegments - 1))
                {
                    var next = GetSegmentOffset(segment + 1);
                    return next - offset;
                }

                return _data.Length - offset;
            }

            #endregion
        }

        #endregion
    }
}
