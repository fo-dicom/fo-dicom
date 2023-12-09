// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of an N-DELETE response.
    /// </summary>
    public sealed class DicomNDeleteResponse : DicomResponse
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomNDeleteResponse"/> class.
        /// </summary>
        /// <param name="command">N-DELETE response command.</param>
        public DicomNDeleteResponse(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomNDeleteResponse"/> class.
        /// </summary>
        /// <param name="request">Associated N-DELETE request.</param>
        /// <param name="status">Response status.</param>
        public DicomNDeleteResponse(DicomNDeleteRequest request, DicomStatus status)
            : base(request, status)
        {
            SOPInstanceUID = request.SOPInstanceUID;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the affected SOP instance UID.
        /// </summary>
        public DicomUID SOPInstanceUID
        {
            get => Command.GetSingleValueOrDefault<DicomUID>(DicomTag.AffectedSOPInstanceUID, null);
            private set => Command.AddOrUpdate(DicomTag.AffectedSOPInstanceUID, value);
        }

        #endregion
    }
}
