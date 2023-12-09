// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Query&#47;Retrieve level of DICOM command messages, applicable to C-FIND, C-GET and C-MOVE.
    /// <see cref="DicomTag.QueryRetrieveLevel"/>
    /// </summary>
    public enum DicomQueryRetrieveLevel
    {
        /// <summary>
        /// Patient level, associated with Patient Q&#47;R queries.
        /// </summary>
        Patient,

        /// <summary>
        /// Study level, associated with Study Q&#47;R queries.
        /// </summary>
        Study,

        /// <summary>
        /// Series level, associated with Study Q&#47;R queries.
        /// </summary>
        Series,

        /// <summary>
        /// Image level, associated with Study Q&#47;R queries.
        /// </summary>
        Image,

        /// <summary>
        /// Worklist level.
        /// </summary>
        [Obsolete("Artificial Q/R level, use DicomQueryRetrieveLevel.NotApplicable instead.")]
        Worklist,

        /// <summary>
        /// Q&#47;R level not applicable in given context.
        /// </summary>
        NotApplicable = -1
    }
}
