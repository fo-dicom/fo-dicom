// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Class for managing DICOM extended negotiation.
    /// See http://dicom.nema.org/medical/dicom/current/output/chtml/part07/sect_D.3.3.5.html
    /// and sect_D.3.3.6 for details on the SOP Class (Common) Extended Negotiation Sub-item.
    /// </summary>
    public class DicomExtendedNegotiation
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DicomExtendedNegotiation"/> class.
        /// </summary>
        /// <param name="sopClassUid">SOP class UID.</param>
        /// <param name="applicationInfo">Extended negotiation Application Information.</param>
        public DicomExtendedNegotiation(DicomUID sopClassUid, DicomServiceApplicationInfo applicationInfo)
        {
            SopClassUid = sopClassUid;
            RequestedApplicationInfo = applicationInfo;
            RelatedGeneralSopClasses = new List<DicomUID>();
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomExtendedNegotiation"/> class.
        /// </summary>
        /// <param name="sopClassUid">SOP class UID.</param>
        /// <param name="serviceClassUid">Common Service Class UID.</param>
        /// <param name="relatedGeneralSopClasses">Related General SOP Classes.</param>
        public DicomExtendedNegotiation(DicomUID sopClassUid, DicomUID serviceClassUid, params DicomUID[] relatedGeneralSopClasses)
        {
            SopClassUid = sopClassUid;
            ServiceClassUid = serviceClassUid;
            RelatedGeneralSopClasses = relatedGeneralSopClasses.ToList();
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomExtendedNegotiation"/> class.
        /// </summary>
        /// <param name="sopClassUid">SOP class UID.</param>
        /// <param name="applicationInfo">Extended negotiation Application Information.</param>
        /// <param name="serviceClassUid">Common Service Class UID.</param>
        /// <param name="relatedGeneralSopClasses">Related General SOP Classes.</param>
        public DicomExtendedNegotiation(DicomUID sopClassUid, DicomServiceApplicationInfo applicationInfo,
            DicomUID serviceClassUid, params DicomUID[] relatedGeneralSopClasses)
        {
            SopClassUid = sopClassUid;
            RequestedApplicationInfo = applicationInfo;
            ServiceClassUid = serviceClassUid;
            RelatedGeneralSopClasses = relatedGeneralSopClasses.ToList();
        }

        /// <summary>
        /// Gets SOP Class UID.
        /// </summary>
        public DicomUID SopClassUid { get; }

        /// <summary>
        /// Gets the Requested Service Class Application Information.
        /// </summary>
        public DicomServiceApplicationInfo RequestedApplicationInfo { get; internal set; }

        /// <summary>
        /// Gets the Accepted Service Class Application Information.
        /// </summary>
        public DicomServiceApplicationInfo AcceptedApplicationInfo { get; private set; }

        /// <summary>
        /// Gets the (optional) Service Class UID.
        /// </summary>
        public DicomUID ServiceClassUid { get; internal set; }

        /// <summary>
        /// Gets the (optional) Related General SOP Class Identification
        /// </summary>
        public List<DicomUID> RelatedGeneralSopClasses { get; }

        /// <summary>
        /// Gets the string representation of the Service Class Application information.
        /// </summary>
        /// <returns></returns>
        public string GetApplicationInfo()
        {
            return AcceptedApplicationInfo != null
                ? $"{AcceptedApplicationInfo} [Accept]"
                : $"{RequestedApplicationInfo} [Proposed]";
        }

        /// <summary>
        /// Compares the Service Class Application Information accepted by the SCP with the
        /// Application Information that was requested by the SCU. The accepted Application Information
        /// will be ignored when no existing application info exists. The existing field values will be
        /// overwritten by the accepted field values, if the accepted info has fields that are not
        /// requested, then these fields will be removed.
        /// </summary>
        /// <param name="acceptedInfo">The Service Class Application Info accepted by the SCP.</param>
        public void AcceptApplicationInfo(DicomServiceApplicationInfo acceptedInfo)
        {
            if (RequestedApplicationInfo == null)
            {
                return;
            }

            for (var i = acceptedInfo.Count + 1; i --> 1; )
            {
                if (!RequestedApplicationInfo.Contains((byte)i))
                {
                    acceptedInfo.Remove((byte)i);
                }
            }

            AcceptedApplicationInfo = acceptedInfo;
        }
    }
}
