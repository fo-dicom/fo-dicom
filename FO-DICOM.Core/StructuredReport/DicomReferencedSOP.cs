// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.StructuredReport
{
    public class DicomReferencedSOP : DicomDataset
    {
        public DicomReferencedSOP(DicomDataset dataset)
            : base(dataset)
        {
        }

        public DicomReferencedSOP(DicomSequence sequence)
        {
            if (sequence.Items.Count == 0) throw new DicomDataException("No referenced SOP pair item found in sequence.");
            Add(sequence.Items[0]);
        }

        public DicomReferencedSOP(DicomUID inst, DicomUID clazz)
        {
            Add(DicomTag.ReferencedSOPInstanceUID, inst);
            Add(DicomTag.ReferencedSOPClassUID, clazz);
        }

        public DicomUID Instance
        {
            get => GetSingleValue<DicomUID>(DicomTag.ReferencedSOPInstanceUID);
        }

        public DicomUID Class
        {
            get => GetSingleValue<DicomUID>(DicomTag.ReferencedSOPClassUID);
        }

    }
}
