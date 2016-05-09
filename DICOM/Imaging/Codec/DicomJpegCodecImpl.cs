// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).


namespace Dicom
{
    using System;
    using System.IO;

    using Dicom.Imaging;
    using Dicom.Imaging.Codec;
    using Dicom.IO.Buffer;

    using FluxJpeg.Core;
    using FluxJpeg.Core.Decoder;
    using FluxJpeg.Core.Encoder;

    using JpegColorSpace = FluxJpeg.Core.ColorSpace;

    internal static class DicomJpegCodecImpl
    {
        internal static void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomJpegParams parameters)
        {
            if ((oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull422)
                || (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial422)
                || (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial420))
            {
                throw new InvalidOperationException(
                    "Photometric Interpretation '" + oldPixelData.PhotometricInterpretation
                    + "' not supported by JPEG 2000 encoder");
            }

            var jparams = parameters ?? new DicomJpegParams();

            var w = oldPixelData.Width;
            var h = oldPixelData.Height;
            var colorModel = GetColorModel(oldPixelData.PhotometricInterpretation);

            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var frameData = oldPixelData.GetFrame(frame);

                var nc = oldPixelData.SamplesPerPixel;
                var sgnd = oldPixelData.PixelRepresentation == PixelRepresentation.Signed;
                var comps = new byte[nc][,];


                for (var c = 0; c < nc; c++)
                {
                    var comp = new byte[w, h];

                    var pos = oldPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? (c * w * h) : c;
                    var offset = oldPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? 1 : nc;

                    if (oldPixelData.BytesAllocated == 1)
                    {
                        if (sgnd)
                        {
                            /*if (oldPixelData.BitsStored < 8)
                            {
                                var sign = (byte)(1 << oldPixelData.HighBit);
                                var mask = (byte)(0xff >> (oldPixelData.BitsAllocated - oldPixelData.BitsStored));
                                for (var y = 0; y < h; ++y)
                                {
                                    for (var x = 0; x < w; ++x)
                                    {
                                        var pixel = (sbyte)frameData.Data[pos];
                                        comp[x, y] = (pixel & sign) > 0 ? -(((-pixel) & mask) + 1) : pixel;
                                        pos += offset;
                                    }
                                }
                            }
                            else
                            {
                                for (var y = 0; y < h; ++y)
                                {
                                    for (var x = 0; x < w; ++x)
                                    {
                                        comp[x, y] = (sbyte)frameData.Data[pos];
                                        pos += offset;
                                    }
                                }
                            }*/
                        }
                        else
                        {
                            var bytes = frameData.Data;
                            for (var y = 0; y < h; ++y)
                            {
                                for (var x = 0; x < w; ++x)
                                {
                                    comp[x, y] = bytes[pos];
                                    pos += offset;
                                }
                            }
                        }
                    }
                    /*else if (oldPixelData.BytesAllocated == 2)
                    {
                        if (sgnd)
                        {
                            if (oldPixelData.BitsStored < 16)
                            {
                                var frameData16 = new ushort[pixelCount];
                                Buffer.BlockCopy(frameData.Data, 0, frameData16, 0, (int)frameData.Size);

                                var sign = (ushort)(1 << oldPixelData.HighBit);
                                var mask = (ushort)(0xffff >> (oldPixelData.BitsAllocated - oldPixelData.BitsStored));
                                for (var y = 0; y < h; ++y)
                                {
                                    for (var x = 0; x < w; ++x)
                                    {
                                        var pixel = frameData16[pos];
                                        comp[x, y] = (pixel & sign) > 0 ? -(((-pixel) & mask) + 1) : pixel;
                                        pos += offset;
                                    }
                                }
                            }
                            else
                            {
                                var frameData16 = new short[pixelCount];
                                Buffer.BlockCopy(frameData.Data, 0, frameData16, 0, (int)frameData.Size);

                                for (var y = 0; y < h; ++y)
                                {
                                    for (var x = 0; x < w; ++x)
                                    {
                                        comp[x, y] = frameData16[pos];
                                        pos += offset;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var frameData16 = new ushort[pixelCount];
                            Buffer.BlockCopy(frameData.Data, 0, frameData16, 0, (int)frameData.Size);

                            for (var y = 0; y < h; ++y)
                            {
                                for (var x = 0; x < w; ++x)
                                {
                                    comp[x, y] = frameData16[pos];
                                    pos += offset;
                                }
                            }
                        }
                    }*/
                    else
                    {
                        throw new InvalidOperationException("JPEG codec only supports Bits Allocated == 8");
                    }

                    comps[c] = comp;
                }


                var image = new Image(colorModel, comps);

                try
                {
                    using (var stream = new MemoryStream())
                    {
                        var encoder = new JpegEncoder(image, jparams.Quality, stream);
                        encoder.Encode();
                        newPixelData.AddFrame(new MemoryByteBuffer(stream.ToArray()));
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Unable to JPEG encode image", e);
                }
            }

            if (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.Rgb)
            {
                newPixelData.PlanarConfiguration = PlanarConfiguration.Interleaved;
            }
        }

        internal static void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomJpegParams parameters)
        {
            var pixelCount = oldPixelData.Height * oldPixelData.Width;

            if (newPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrIct
                || newPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrRct)
            {
                newPixelData.PhotometricInterpretation = PhotometricInterpretation.Rgb;
            }

            if (newPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull422
                || newPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial422)
            {
                newPixelData.PhotometricInterpretation = PhotometricInterpretation.YbrFull;
            }

            if (newPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull)
            {
                newPixelData.PlanarConfiguration = PlanarConfiguration.Planar;
            }

            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var jpegData = oldPixelData.GetFrame(frame);

                // Destination frame should be of even length
                var frameSize = newPixelData.UncompressedFrameSize;
                if ((frameSize & 1) == 1) ++frameSize;
                var destArray = new byte[frameSize];

                using (var stream = new MemoryStream(jpegData.Data))
                {
                    var decoder = new JpegDecoder(stream);
                    var image = decoder.Decode().Image;
                    var components = image.Raster;

                    for (var c = 0; c < components.Length; c++)
                    {
                        var comp = components[c];

                        var pos = newPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? (c * pixelCount) : c;
                        var offset = newPixelData.PlanarConfiguration == PlanarConfiguration.Planar
                                         ? 1
                                         : components.Length;

                        if (newPixelData.BytesAllocated == 1)
                        {
                            for (var y = 0; y < comp.GetLength(1); ++y)
                            {
                                for (var x = 0; x < comp.GetLength(0); ++x)
                                {
                                    destArray[pos] = comp[x, y];
                                    pos += offset;
                                }
                            }
                        }
                        else if (newPixelData.BytesAllocated == 2)
                        {
                            for (var y = 0; y < comp.GetLength(1); ++y)
                            {
                                for (var x = 0; x < comp.GetLength(0); ++x)
                                {
                                    destArray[2 * pos] = comp[x, y];
                                    pos += offset;
                                }
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException(
                                "JPEG module only supports Bytes Allocated == 8 or 16!");
                        }
                    }

                    newPixelData.AddFrame(new MemoryByteBuffer(destArray));
                }
            }
        }

        private static ColorModel GetColorModel(PhotometricInterpretation photometricInterpretation)
        {
            if (photometricInterpretation == PhotometricInterpretation.Rgb)
            {
                return new ColorModel { colorspace = JpegColorSpace.RGB, Opaque = true };
            }
            else if (photometricInterpretation == PhotometricInterpretation.Monochrome1
                     || photometricInterpretation == PhotometricInterpretation.Monochrome2)
            {
                return new ColorModel { colorspace = JpegColorSpace.Gray, Opaque = true };
            }
            else if (photometricInterpretation == PhotometricInterpretation.PaletteColor)
            {
                return new ColorModel { colorspace = JpegColorSpace.Gray, Opaque = true };
            }
            else if (photometricInterpretation == PhotometricInterpretation.YbrFull
                     || photometricInterpretation == PhotometricInterpretation.YbrFull422
                     || photometricInterpretation == PhotometricInterpretation.YbrPartial422)
            {
                return new ColorModel { colorspace = JpegColorSpace.YCbCr, Opaque = true };
            }

            return default(ColorModel);
        }
    }
}
