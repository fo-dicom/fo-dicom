// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of the N-GET response.
    /// </summary>
    public sealed class DicomNGetResponse : DicomResponse
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="DicomNGetResponse"/> class.
        /// </summary>
        /// <param name="command">N-GET response command.</param>
        public DicomNGetResponse(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomNGetResponse"/> class.
        /// </summary>
        /// <param name="request">Associated N-GET request.</param>
        /// <param name="status">Response status.</param>
        public DicomNGetResponse(DicomNGetRequest request, DicomStatus status)
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
