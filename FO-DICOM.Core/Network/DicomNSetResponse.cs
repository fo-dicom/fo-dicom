// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of an N-SET response.
    /// </summary>
    public sealed class DicomNSetResponse : DicomResponse
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="DicomNSetResponse"/> class.
        /// </summary>
        /// <param name="command">N-SET response command.</param>
        public DicomNSetResponse(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomNSetResponse"/> class.
        /// </summary>
        /// <param name="request">Associated N-SET request.</param>
        /// <param name="status">Response status.</param>
        public DicomNSetResponse(DicomNSetRequest request, DicomStatus status)
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
