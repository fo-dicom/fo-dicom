// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;

using Dicom.IO;
using Dicom.IO.Buffer;

namespace Dicom.Imaging.Codec
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
            throw new NotImplementedException();
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

        private class RLEDecoder
        {
            #region FIELDS

            private readonly int[] offsets;

            private readonly byte[] data;

            #endregion

            #region CONSTRUCTORS

            internal RLEDecoder(IByteBuffer data)
            {
                var source = new ByteBufferByteSource(data);
                //source.Mark();
                this.NumberOfSegments = source.GetInt32();
                this.offsets = new int[15];
                for (var i = 0; i < 15; ++i)
                {
                    this.offsets[i] = source.GetInt32();
                }
                //source.Rewind();

                this.data = data.Data;
            }

            #endregion

            #region PROPERTIES

            internal int NumberOfSegments { get; }

            #endregion

            #region METHODS

            internal void DecodeSegment(int segment, IByteBuffer buffer, int start, int sampleOffset)
            {
                if (segment < 0 || segment >= this.NumberOfSegments)
                {
                    throw new ArgumentOutOfRangeException("Segment number out of range");
                }

                int offset = this.GetSegmentOffset(segment);
                int length = this.GetSegmentLength(segment);

                this.Decode(buffer, start, sampleOffset, this.data, offset, length);
            }

            private void Decode(IByteBuffer buffer, int start, int sampleOffset, byte[] rleData, int offset, int count)
            {
                var pos = start;
                var end = offset + count;
                var bufferLength = buffer.Size;

                for (var i = offset; i < end && pos < bufferLength;)
                {
                    char control = (char)rleData[i++];

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
                            for (int j = 0; j < length; ++j, ++i, ++pos)
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

                    if ((i + 2) >= end)
                    {
                        break;
                    }
                }
            }

            private int GetSegmentOffset(int segment)
            {
                return this.offsets[segment];
            }

            private int GetSegmentLength(int segment)
            {
                var offset = this.GetSegmentOffset(segment);
                if (segment < (this.NumberOfSegments - 1))
                {
                    var next = this.GetSegmentOffset(segment + 1);
                    return next - offset;
                }

                return this.data.Length - offset;
            }

            #endregion
        }

        #endregion
    }
}
