// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Imaging
{
    public enum CacheType
    {

        /// <summary>
        /// No caching at all
        /// </summary>
        None,

        /// <summary>
        /// Specifies the raw compressed pixel data (where it needs to be read from disk)
        /// </summary>
        CompressedData,

        /// <summary>
        /// Specifies the raw uncompressed pixel data (where it needs to be decompressed or read from disk)
        /// </summary>
        PixelData,

        /// <summary>
        /// Specifies the lookup tables generated as a result of windowing etc.
        /// </summary>
        LookupTables,

        /// <summary>
        /// Specifies the overlay data contained in a dataset.
        /// </summary>
        OverlayData,

        /// <summary>
        /// Specifies the cached copy of the pixel display data
        /// </summary>
        Display,

        /// <summary>
        /// All caches
        /// </summary>
        All,

    }
}
