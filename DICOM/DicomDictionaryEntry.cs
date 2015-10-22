// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom
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

            if (String.IsNullOrWhiteSpace(name)) Name = Tag.ToString();
            else Name = name;

            if (String.IsNullOrWhiteSpace(keyword)) Keyword = Name;
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

            if (String.IsNullOrWhiteSpace(name)) Name = Tag.ToString();
            else Name = name;

            if (String.IsNullOrWhiteSpace(keyword)) Keyword = Name;
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
