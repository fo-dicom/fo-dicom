// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Log;

namespace Dicom.Imaging.Codec
{
    public class DicomCodecParams
    {
        protected DicomCodecParams()
        {
            Logger = LogManager.Default.GetLogger("Dicom.Imaging.Codec");
        }

        public Logger Logger { get; protected set; }
    }
}
