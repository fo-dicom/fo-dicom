// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Imaging.Codec
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
            Logger = Setup.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(FellowOakDicom.Log.LogCategories.Codec);
        }

        /// <summary>
        /// Gets or sets the DICOM codec parameters <see cref="Logger"/>.
        /// </summary>
        public ILogger Logger { get; protected set; }
    }
}
