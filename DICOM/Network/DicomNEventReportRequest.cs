// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System.Text;

    public class DicomNEventReportRequest : DicomRequest
    {
        public DicomNEventReportRequest(DicomDataset command)
            : base(command)
        {
        }

        public DicomNEventReportRequest(
            DicomUID requestedClassUid,
            DicomUID requestedInstanceUid,
            ushort eventTypeId)
            : base(DicomCommandField.NEventReportRequest, requestedClassUid)
        {
            SOPInstanceUID = requestedInstanceUid;
            EventTypeID = eventTypeId;
        }

        public DicomUID SOPInstanceUID
        {
            get
            {
                return Command.Get<DicomUID>(DicomTag.RequestedSOPInstanceUID);
            }
            private set
            {
                Command.Add(DicomTag.RequestedSOPInstanceUID, value);
            }
        }

        public ushort EventTypeID
        {
            get
            {
                return Command.Get<ushort>(DicomTag.EventTypeID);
            }
            private set
            {
                Command.Add(DicomTag.EventTypeID, value);
            }
        }

        internal bool HasSOPInstanceUID => this.Command.Contains(DicomTag.RequestedSOPInstanceUID);

        public delegate void ResponseDelegate(DicomNEventReportRequest request, DicomNEventReportResponse response);

        public ResponseDelegate OnResponseReceived;

        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                if (OnResponseReceived != null) OnResponseReceived(this, (DicomNEventReportResponse)response);
            }
            catch
            {
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} [{1}]", ToString(Type), MessageID);
            sb.AppendFormat("\n\t\tEvent Type:	{0:x4}", EventTypeID);
            return sb.ToString();
        }
    }
}
