// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using CSJ2K;
using CSJ2K.j2k.util;
using CSJ2K.Util;
using FellowOakDicom.IO.Buffer;
using System;
using System.Linq;

namespace FellowOakDicom.Imaging.Codec
{

    internal static class DicomJpeg2000CodecImpl
    {
        internal static void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomJpeg2000Params parameters)
        {
            if ((oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull422)
                || (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial422)
                || (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial420))
            {
                throw new InvalidOperationException(
                    "Photometric Interpretation '" + oldPixelData.PhotometricInterpretation
                    + "' not supported by JPEG 2000 encoder");
            }

            var jparams = parameters ?? new DicomJpeg2000Params();

            jparams.Irreversible = newPixelData.IsLossy;
            jparams.AllowMCT = oldPixelData.PhotometricInterpretation == PhotometricInterpretation.Rgb;

            var pixelCount = oldPixelData.Height * oldPixelData.Width;

            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var frameData = oldPixelData.GetFrame(frame);

                var nc = oldPixelData.SamplesPerPixel;
                var sgnd = oldPixelData.PixelRepresentation == PixelRepresentation.Signed
                           && !jparams.EncodeSignedPixelValuesAsUnsigned;
                var comps = new int[nc][];

                for (var c = 0; c < nc; c++)
                {
                    var comp = new int[pixelCount];

                    var pos = oldPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? (c * pixelCount) : c;
                    var offset = oldPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? 1 : nc;

                    if (oldPixelData.BytesAllocated == 1)
                    {
                        var data = frameData.Data;
                        if (sgnd)
                        {
                            if (oldPixelData.BitsStored < 8)
                            {
                                var sign = (byte)(1 << oldPixelData.HighBit);
                                var mask = (byte)(0xff >> (oldPixelData.BitsAllocated - oldPixelData.BitsStored));
                                for (var p = 0; p < pixelCount; p++)
                                {
                                    var pixel = (sbyte)data[pos];
                                    comp[p] = (pixel & sign) > 0 ? -(((-pixel) & mask) + 1) : pixel;
                                    pos += offset;
                                }
                            }
                            else
                            {
                                for (var p = 0; p < pixelCount; p++)
                                {
                                    comp[p] = (sbyte)data[pos];
                                    pos += offset;
                                }
                            }
                        }
                        else
                        {
                            for (var p = 0; p < pixelCount; p++)
                            {
                                comp[p] = data[pos];
                                pos += offset;
                            }
                        }
                    }
                    else if (oldPixelData.BytesAllocated == 2)
                    {
                        if (sgnd)
                        {
                            if (oldPixelData.BitsStored < 16)
                            {
                                var frameData16 = new ushort[pixelCount];
                                Buffer.BlockCopy(frameData.Data, 0, frameData16, 0, (int)frameData.Size);

                                var sign = (ushort)(1 << oldPixelData.HighBit);
                                var mask = (ushort)(0xffff >> (oldPixelData.BitsAllocated - oldPixelData.BitsStored));
                                for (var p = 0; p < pixelCount; p++)
                                {
                                    var pixel = frameData16[pos];
                                    comp[p] = (pixel & sign) > 0 ? -(((-pixel) & mask) + 1) : pixel;
                                    pos += offset;
                                }
                            }
                            else
                            {
                                var frameData16 = new short[pixelCount];
                                Buffer.BlockCopy(frameData.Data, 0, frameData16, 0, (int)frameData.Size);

                                for (var p = 0; p < pixelCount; p++)
                                {
                                    comp[p] = frameData16[pos];
                                    pos += offset;
                                }
                            }
                        }
                        else
                        {
                            var frameData16 = new ushort[pixelCount];
                            Buffer.BlockCopy(frameData.Data, 0, frameData16, 0, (int)frameData.Size);

                            for (var p = 0; p < pixelCount; p++)
                            {
                                comp[p] = frameData16[pos];
                                pos += offset;
                            }
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            $"JPEG 2000 codec does not support Bits Allocated == {oldPixelData.BitsAllocated}");
                    }

                    comps[c] = comp;
                }

                var image = new PortableImageSource(
                    oldPixelData.Width,
                    oldPixelData.Height,
                    nc,
                    oldPixelData.BitsAllocated,
                    Enumerable.Repeat(sgnd, nc).ToArray(),
                    comps);

                try
                {
                    var cbuf = J2kImage.ToBytes(image, ToParameterList(jparams, false));
                    newPixelData.AddFrame(new MemoryByteBuffer(cbuf));
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Unable to JPEG 2000 encode image", e);
                }
            }

            if (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.Rgb)
            {
                newPixelData.PlanarConfiguration = PlanarConfiguration.Interleaved;

                if (jparams.AllowMCT && jparams.UpdatePhotometricInterpretation)
                {
                    if (newPixelData.IsLossy && jparams.Irreversible) newPixelData.PhotometricInterpretation = PhotometricInterpretation.YbrIct;
                    else newPixelData.PhotometricInterpretation = PhotometricInterpretation.YbrRct;
                }
            }
        }

        internal static void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomJpeg2000Params parameters)
        {
            var pixelCount = oldPixelData.Height * oldPixelData.Width;
            var bytesAllocated = newPixelData.BytesAllocated;

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

                var image = J2kImage.FromBytes(jpegData.Data, ToParameterList(parameters, true));
                if (image == null) throw new InvalidOperationException("Error in JPEG 2000 code stream!");

                for (var c = 0; c < image.NumberOfComponents; c++)
                {
                    // convert RGB BGR colorspace
                    var comp = image.GetComponent(image.NumberOfComponents - c - 1);

                    var pos = bytesAllocated * (newPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? c * pixelCount : c);
                    var offset = bytesAllocated * (newPixelData.PlanarConfiguration == PlanarConfiguration.Planar
                                     ? 1
                                     : image.NumberOfComponents);

                    if (bytesAllocated == 1)
                    {
                        if (newPixelData.PixelRepresentation == PixelRepresentation.Signed)
                        {
                            var sign = 1 << newPixelData.HighBit;
                            var mask = 0xff ^ sign;

                            for (var p = 0; p < pixelCount; p++)
                            {
                                int i = (i = comp[p] - sign) < 0 ? (i & mask) | sign : i & mask;
                                destArray[pos] = (byte) i;
                                pos += offset;
                            }
                        }
                        else
                        {
                            for (var p = 0; p < pixelCount; p++)
                            {
                                destArray[pos] = (byte)comp[p];
                                pos += offset;
                            }
                        }
                    }
                    else if (bytesAllocated == 2)
                    {
                        if (newPixelData.PixelRepresentation == PixelRepresentation.Signed)
                        {
                            var sign = 1 << newPixelData.HighBit;
                            var mask = 0xffff ^ sign;

                            for (var p = 0; p < pixelCount; p++)
                            {
                                int i = (i = comp[p] - sign) < 0 ? (i & mask) | sign : i & mask;
                                destArray[pos] = (byte) (i & 0xff);
                                destArray[pos + 1] = (byte) ((i >> 8) & 0xff);
                                pos += offset;
                            }
                        }
                        else
                        {
                            for (var p = 0; p < pixelCount; p++)
                            {
                                var i = comp[p];
                                destArray[pos] = (byte)(i & 0xff);
                                destArray[pos + 1] = (byte)((i >> 8) & 0xff);
                                pos += offset;
                            }
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            $"JPEG 2000 codec does not support Bits Allocated == {newPixelData.BitsAllocated}!");
                    }
                }

                newPixelData.AddFrame(new MemoryByteBuffer(destArray));
            }
        }

        private static ParameterList ToParameterList(DicomJpeg2000Params parameters, bool decoder)
        {
            // These JPEG2000 codec parameters are not translated: EncodeSignedPixelValuesAsUnsigned, RateLevels, 
            // UpdatePhotometricInterpretation
            var list =
                new ParameterList(
                    decoder
                        ? J2kImage.GetDefaultDecoderParameterList(null)
                        : J2kImage.GetDefaultEncoderParameterList(null));

            var param = parameters ?? new DicomJpeg2000Params();

            list["Mct"] = param.AllowMCT ? "on" : "off";
            list["lossless"] = param.Irreversible ? "off" : "on";
            list["verbose"] = param.IsVerbose ? "on" : "off";

            if (param.Irreversible)
            {
                list["rate"] = param.Rate.ToString();
            }

            return list;
        }
    }
}
