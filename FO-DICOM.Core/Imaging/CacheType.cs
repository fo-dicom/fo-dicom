// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Imaging
{
    [Flags]
    public enum CacheType
    {

        /// <summary>
        /// No caching at all
        /// </summary>
        None = 0,

        /// <summary>
        /// Specifies the raw uncompressed pixel data (where it needs to be decompressed or read from disk)
        /// </summary>
        PixelData = 1,

        /// <summary>
        /// Specifies the lookup tables and pipelines generated as a result of windowing etc.
        /// </summary>
        LookupTables = 2,

        /// <summary>
        /// Specifies the cached copy of the rendered display data
        /// </summary>
        Display = 4,

        /// <summary>
        /// All caches
        /// </summary>
        All = PixelData | LookupTables | Display

    }
}
