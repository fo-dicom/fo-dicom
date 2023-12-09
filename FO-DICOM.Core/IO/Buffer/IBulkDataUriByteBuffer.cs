// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.IO.Buffer
{

    /// <summary>
    /// Interface for byte buffers representing a Bulk Data byte buffer, e.g. as in the DICOM Json model, in PS3.18 Chapter F.2.2.
    /// </summary>
    public interface IBulkDataUriByteBuffer : IByteBuffer
    {
        /// <summary>
        /// Gets the URI of a bulk data element as defined in <a href="http://dicom.nema.org/medical/dicom/current/output/chtml/part19/chapter_A.html#table_A.1.5-2">Table A.1.5-2 in PS3.19</a>.
        /// </summary>
        string BulkDataUri { get; }
    }
}
