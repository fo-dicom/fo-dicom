// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.StructuredReport
{
    public class DicomStructuredReport : DicomContentItem
    {
        public DicomStructuredReport(DicomDataset dataset)
            : base(dataset)
        {
        }

        public DicomStructuredReport(DicomCodeItem code, params DicomContentItem[] items)
            : base(code, DicomRelationship.Contains, DicomContinuity.Separate, items)
        {
            // relationship type is not needed for root element
            Dataset.Remove(DicomTag.RelationshipType);
        }
    }
}
