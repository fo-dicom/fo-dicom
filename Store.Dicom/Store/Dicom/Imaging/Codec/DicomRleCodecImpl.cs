// fo-dicom: A C# DICOM library
//
// Copyright (c) 2006-2008  Colby Dillion
//
// Author:
//    Colby Dillion (colby.dillion@gmail.com)
//
// Credits:
//    Includes patches from ClearCanvas project, licensed under LGPL.
//
// Note:  
//    This file may contain code using a license that has not been 
//    verified to be compatible with the licensing of this software.  
//
// References:
//     * originally based on the RLE codec on DCMTK
//       http://www.dcmtk.org
//     * Implementation adapted from mdcm equivalent,
//       https://github.com/rcd/mdcm

using System;
using System.Collections.Generic;
using System.IO;
using Dicom.IO;
using Dicom.IO.Buffer;

namespace Dicom.Imaging.Codec
{
	public class DicomRleCodecImpl : DicomRleCodec {

		public override void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters) {
/*			var rleParams = parameters as DicomRleParams ?? GetDefaultParameters();

			var pixelCount = oldPixelData.Width * oldPixelData.Height;
			var numberOfSegments = oldPixelData.BytesAllocated * oldPixelData.SamplesPerPixel;

			for (int i = 0; i < oldPixelData.NumberOfFrames; i++) {
				var encoder = new RLEEncoder();
				var frameData = oldPixelData.GetFrame(i);

				for (var s = 0; s < numberOfSegments; s++) {
					encoder.NextSegment();

					var sample = s / oldPixelData.BytesAllocated;
					var sabyte = s % oldPixelData.BytesAllocated;

					int pos;
					int offset;

					if (newPixelData.PlanarConfiguration == 0) {
						pos = sample * oldPixelData.BytesAllocated;
						offset = numberOfSegments;
					}
					else {
						pos = sample * oldPixelData.BytesAllocated * pixelCount;
						offset = oldPixelData.BytesAllocated;
					}

					if (rleParams.ReverseByteOrder)
						pos += sabyte;
					else
						pos += oldPixelData.BytesAllocated - sabyte - 1;

					for (var p = 0; p < pixelCount; p++) {
						if (pos >= frameData.Size)
							throw new DicomCodecException("");
						encoder.Encode(frameData.Data[pos]);
						pos += offset;
					}
					encoder.Flush();
				}

				encoder.MakeEvenLength();

				newPixelData.AddFrame(encoder.GetBuffer());
			}*/
		}

		public override void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			DicomRleNativeCodec.Decode(oldPixelData.ToCodecPixelData(), newPixelData.ToCodecPixelData());
		}
	}
}
