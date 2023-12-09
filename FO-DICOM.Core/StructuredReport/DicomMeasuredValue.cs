// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.StructuredReport
{
    public class DicomMeasuredValue : DicomDataset
    {
        public DicomMeasuredValue(DicomDataset dataset)
            : base(dataset)
        {
        }

        public DicomMeasuredValue(DicomSequence sequence)
        {
            if (sequence.Items.Count == 0) throw new DicomDataException("No measurement item found in sequence.");
            Add(sequence.Items[0]);
        }

        public DicomMeasuredValue(decimal value, DicomCodeItem units)
        {
            Add(DicomTag.NumericValue, value);
            Add(new DicomSequence(DicomTag.MeasurementUnitsCodeSequence, units));
        }

        public DicomCodeItem Code => GetCodeItem(DicomTag.MeasurementUnitsCodeSequence);

        public decimal Value => GetSingleValue<decimal>(DicomTag.NumericValue);

        public override string ToString() => $"{Value} {Code.Value}";

    }
}
