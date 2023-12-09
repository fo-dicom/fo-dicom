// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network.Client
{
    public class DicomClientOptions
    {
        /// <summary>
        /// Gets or sets the timeout (in ms) to wait for an association response after sending an association request
        /// </summary>
        public int AssociationRequestTimeoutInMs { get; set; } = 5000;

        /// <summary>
        /// Gets or sets the timeout (in ms) to wait for an association to be released after sending an association release request
        /// </summary>
        public int AssociationReleaseTimeoutInMs { get; set; } = 10000;

        /// <summary>
        /// Gets or sets the timeout (in ms) that associations need to be held open after all requests have been processed
        /// </summary>
        public int AssociationLingerTimeoutInMs { get; set; } = 50;

        /// <summary>
        /// Gets the maximum number of DICOM requests that are allowed to be sent over one single association.
        /// When this limit is reached, the DICOM client will wait for pending requests to complete, and then open a new association
        /// to send the remaining requests, if any.
        /// </summary>
        public int? MaximumNumberOfRequestsPerAssociation { get; set; }

        /// <summary>
        /// The maximum number of consecutive times an association request is allowed to time out before giving up
        /// <br/>
        /// Use <see cref="AssociationRequestTimeoutInMs"/> to configure the timeout 
        /// </summary>
        public int MaximumNumberOfConsecutiveTimedOutAssociationRequests { get; set; } = 3;
        
        public DicomClientOptions Clone() =>
            new DicomClientOptions
            {
                AssociationRequestTimeoutInMs = AssociationRequestTimeoutInMs,
                AssociationReleaseTimeoutInMs = AssociationReleaseTimeoutInMs,
                AssociationLingerTimeoutInMs = AssociationLingerTimeoutInMs,
                MaximumNumberOfRequestsPerAssociation = MaximumNumberOfRequestsPerAssociation,
                MaximumNumberOfConsecutiveTimedOutAssociationRequests = MaximumNumberOfConsecutiveTimedOutAssociationRequests
            };
    }
}
