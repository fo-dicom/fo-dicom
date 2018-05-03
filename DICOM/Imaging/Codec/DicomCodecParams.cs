// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Log;

namespace Dicom.Imaging.Codec
{
    /// <summary>
    /// Base class for DICOM codec parameters.
    /// </summary>
    public class DicomCodecParams
    {
        /// <summary>
        /// Protected base class constructor.
        /// </summary>
        protected DicomCodecParams()
        {
            this.Logger = LogManager.GetLogger("Dicom.Imaging.Codec");
        }

        /// <summary>
        /// Gets or sets the DICOM codec parameters <see cref="Logger"/>.
        /// </summary>
        public Logger Logger { get; protected set; }
    }
}
