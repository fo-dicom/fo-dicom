// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom
{

    public sealed class DicomDictionaryEntry
    {
        public DicomDictionaryEntry(
            DicomTag tag,
            string name,
            string keyword,
            DicomVM vm,
            bool retired,
            params DicomVR[] vrs)
        {
            Tag = tag;

            if (string.IsNullOrEmpty(name?.Trim())) Name = Tag.ToString();
            else Name = name;

            if (string.IsNullOrEmpty(keyword?.Trim())) Keyword = Name;
            else Keyword = keyword;

            ValueMultiplicity = vm;
            ValueRepresentations = vrs;
            IsRetired = retired;
        }

        public DicomDictionaryEntry(
            DicomMaskedTag tag,
            string name,
            string keyword,
            DicomVM vm,
            bool retired,
            params DicomVR[] vrs)
        {
            Tag = tag.Tag;
            MaskTag = tag;

            if (string.IsNullOrEmpty(name?.Trim())) Name = Tag.ToString();
            else Name = name;

            if (string.IsNullOrEmpty(keyword?.Trim())) Keyword = Name;
            else Keyword = keyword;

            ValueMultiplicity = vm;
            ValueRepresentations = vrs;
            IsRetired = retired;
        }

        public DicomTag Tag { get; set; }

        public DicomMaskedTag MaskTag { get; private set; }

        public string Name { get; private set; }

        public string Keyword { get; private set; }

        public DicomVR[] ValueRepresentations { get; private set; }

        public DicomVM ValueMultiplicity { get; private set; }

        public bool IsRetired { get; private set; }
    }
}
