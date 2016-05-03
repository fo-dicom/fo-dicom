// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;

    using CSJ2K;
    using CSJ2K.j2k.util;

    using Dicom.Imaging;
    using Dicom.Imaging.Codec;
    using Dicom.IO.Buffer;

    internal static class DicomJpeg2000CodecImpl
    {
        /*        private OPJ_COLOR_SPACE getOpenJpegColorSpace(PhotometricInterpretation photometricInterpretation)
        {
            if (photometricInterpretation == PhotometricInterpretation.Rgb) return CLRSPC_SRGB;
            else if (photometricInterpretation == PhotometricInterpretation.Monochrome1
                     || photometricInterpretation == PhotometricInterpretation.Monochrome2) return CLRSPC_GRAY;
            else if (photometricInterpretation == PhotometricInterpretation.PaletteColor) return CLRSPC_GRAY;
            else if (photometricInterpretation == PhotometricInterpretation.YbrFull
                     || photometricInterpretation == PhotometricInterpretation.YbrFull422
                     || photometricInterpretation == PhotometricInterpretation.YbrPartial422) return CLRSPC_SYCC;
            else return CLRSPC_UNKNOWN;
        }*/

        internal static void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomJpeg2000Params parameters)
        {
            if ((oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull422)
                || (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial422)
                || (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial420))
                throw new InvalidOperationException(
                    "Photometric Interpretation '" + oldPixelData.PhotometricInterpretation
                    + "' not supported by JPEG 2000 encoder");

            var jparams = parameters ?? new DicomJpeg2000Params();

            var pixelCount = oldPixelData.Height * oldPixelData.Width;

            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var frameData = oldPixelData.GetFrame(frame);

                if (newPixelData.IsLossy && jparams.Irreversible) eparams.irreversible = 1;

                for (uint r = 0; r < jparams.RateLevels.Length; r++)
                {
                    if (jparams.RateLevels[r] > jparams.Rate)
                    {
                        eparams.tcp_numlayers++;
                        eparams.tcp_rates[r] = (float)jparams.RateLevels[r];
                    }
                    else break;
                }
                eparams.tcp_numlayers++;
                eparams.tcp_rates[r] = (float)jparams.Rate;

                if (!newPixelData.IsLossy && jparams.Rate > 0) eparams.tcp_rates[eparams.tcp_numlayers++] = 0;

                if (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.Rgb && jparams.AllowMCT) eparams.tcp_mct = 1;

                memset(&cmptparm[0], 0, sizeof(opj_image_cmptparm_t) * 3);
                for (int i = 0; i < oldPixelData.SamplesPerPixel; i++)
                {
                    cmptparm[i].bpp = oldPixelData.BitsAllocated;
                    cmptparm[i].prec = oldPixelData.BitsStored;
                    if (!jparams.EncodeSignedPixelValuesAsUnsigned) cmptparm[i].sgnd = oldPixelData.PixelRepresentation == PixelRepresentation.Signed;
                    cmptparm[i].dx = eparams.subsampling_dx;
                    cmptparm[i].dy = eparams.subsampling_dy;
                    cmptparm[i].h = oldPixelData.Height;
                    cmptparm[i].w = oldPixelData.Width;
                }

                OPJ_COLOR_SPACE color_space = getOpenJpegColorSpace(oldPixelData.PhotometricInterpretation);
                image = opj_image_create(oldPixelData.SamplesPerPixel, &cmptparm[0], color_space);

                image.x0 = eparams.image_offset_x0;
                image.y0 = eparams.image_offset_y0;
                image.x1 = image.x0 + ((oldPixelData.Width - 1) * eparams.subsampling_dx) + 1;
                image.y1 = image.y0 + ((oldPixelData.Height - 1) * eparams.subsampling_dy) + 1;

                for (int c = 0; c < image.numcomps; c++)
                {
                    opj_image_comp_t* comp = &image.comps[c];

                    int pos = oldPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? (c * pixelCount) : c;
                    var offset =
                        oldPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? 1 : image.numcomps;

                    if (oldPixelData.BytesAllocated == 1)
                    {
                        if (comp.sgnd)
                        {
                            if (oldPixelData.BitsStored < 8)
                            {
                                var sign = (byte)(1 << oldPixelData.HighBit);
                                var mask = (byte)(0xff >> (oldPixelData.BitsAllocated - oldPixelData.BitsStored));
                                for (var p = 0; p < pixelCount; p++)
                                {
                                    var pixel = frameData[pos];
                                    comp.data[p] = pixel & sign ? - (((-pixel) & mask) + 1) : pixel;
                                    pos += offset;
                                }
                            }
                            else
                            {
                                sbyte[] frameData8 = frameData.begin();
                                for (var p = 0; p < pixelCount; p++)
                                {
                                    comp.data[p] = frameData8[pos];
                                    pos += offset;
                                }
                            }
                        }
                        else
                        {
                            for (var p = 0; p < pixelCount; p++)
                            {
                                comp.data[p] = frameData[pos];
                                pos += offset;
                            }
                        }
                    }
                    else if (oldPixelData.BytesAllocated == 2)
                    {
                        if (comp.sgnd)
                        {
                            if (oldPixelData.BitsStored < 16)
                            {
                                ushort[] frameData16 = frameData.begin();
                                var sign = (ushort)(1 << oldPixelData.HighBit);
                                var mask = (ushort)(0xffff >> (oldPixelData.BitsAllocated - oldPixelData.BitsStored));
                                for (var p = 0; p < pixelCount; p++)
                                {
                                    var pixel = frameData16[pos];
                                    comp.data[p] = pixel & sign ? - (((-pixel) & mask) + 1) : pixel;
                                    pos += offset;
                                }
                            }
                            else
                            {
                                short[] frameData16 = frameData.begin();
                                for (var p = 0; p < pixelCount; p++)
                                {
                                    comp.data[p] = frameData16[pos];
                                    pos += offset;
                                }
                            }
                        }
                        else
                        {
                            ushort[] frameData16 = frameData.begin();
                            for (var p = 0; p < pixelCount; p++)
                            {
                                comp.data[p] = frameData16[pos];
                                pos += offset;
                            }
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("JPEG 2000 codec only supports Bits Allocated == 8 or 16");
                    }
                }

                if (opj_encode(cinfo, cio, image, eparams.index))
                {
                    int clen = cio_tell(cio);
                    var cbuf = new byte[clen + ((clen & 1) == 1 ? 1 : 0)];
                    Array.Copy(cio.buffer, cbuf, clen);

                    newPixelData.AddFrame(cbuf);
                }
                else
                {
                    throw new InvalidOperationException("Unable to JPEG 2000 encode image");
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

                var image = J2kImage.FromBytes(jpegData.Data, ToParameterList(parameters));

                if (image == null) throw new InvalidOperationException("Error in JPEG 2000 code stream!");

                for (var c = 0; c < image.NumberOfComponents; c++)
                {
                    var comp = image.GetComponent(c);

                    var pos = newPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? (c * pixelCount) : c;
                    var offset = newPixelData.PlanarConfiguration == PlanarConfiguration.Planar
                                     ? 1
                                     : image.NumberOfComponents;

                    if (newPixelData.BytesAllocated == 1)
                    {
                        for (var p = 0; p < pixelCount; p++)
                        {
                            destArray[pos] = comp[p];
                            pos += offset;
                        }
                    }
                    else if (newPixelData.BytesAllocated == 2)
                    {
                        for (var p = 0; p < pixelCount; p++)
                        {
                            destArray[2 * pos] = comp[p];
                            pos += offset;
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            "JPEG 2000 module only supports Bytes Allocated == 8 or 16!");
                    }
                }

                newPixelData.AddFrame(new MemoryByteBuffer(destArray));
            }
        }

        private static ParameterList ToParameterList(DicomJpeg2000Params parameters, bool decoder = true)
        {
            // These JPEG2000 codec parameters are not translated: EncodeSignedPixelValuesAsUnsigned, RateLevels, 
            // UpdatePhotometricInterpretation
            var list =
                new ParameterList(
                    decoder
                        ? J2kImage.GetDefaultDecoderParameterList(null)
                        : J2kImage.GetDefaultEncoderParameterList(null))
                    {
                        ["Mct"] = parameters.AllowMCT ? "on" : "off",
                        ["lossless"] = parameters.Irreversible ? "off" : "on",
                        ["verbose"] = parameters.IsVerbose ? "on" : "off",
                        ["rate"] = parameters.Rate.ToString()
                    };

            return list;
        }
    }
}
