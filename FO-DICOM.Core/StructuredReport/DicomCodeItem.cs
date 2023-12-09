// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.StructuredReport
{
    public class DicomCodeItem : DicomDataset
    {
        public DicomCodeItem(DicomDataset dataset)
            : base(dataset)
        {
        }

        public DicomCodeItem(DicomSequence sequence)
        {
            if (sequence.Items.Count == 0) throw new DicomDataException("No code item found in sequence.");
            Add(sequence.Items[0]);
        }

        public DicomCodeItem(string value, string scheme, string meaning, string version = null)
        {
            Add(DicomTag.CodeValue, value);
            Add(DicomTag.CodingSchemeDesignator, scheme);
            Add(DicomTag.CodeMeaning, meaning);
            if (version != null) Add(DicomTag.CodingSchemeVersion, version);
        }

        public string Value
        {
            get => GetValueOrDefault(DicomTag.CodeValue, 0, string.Empty);
        }

        public string Scheme
        {
            get => GetValueOrDefault(DicomTag.CodingSchemeDesignator, 0, string.Empty);
        }

        public string Meaning
        {
            get => GetValueOrDefault(DicomTag.CodeMeaning, 0, string.Empty);
        }

        public string Version
        {
            get => GetValueOrDefault(DicomTag.CodingSchemeVersion, 0, string.Empty);
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(DicomCodeItem)) return false;
            return Value == ((DicomCodeItem)obj).Value && Scheme == ((DicomCodeItem)obj).Scheme
                   && Version == ((DicomCodeItem)obj).Version;
        }

        public static bool operator ==(DicomCodeItem a, DicomCodeItem b)
        {
            if (((object)a == null) && ((object)b == null)) return true;
            if (((object)a == null) || ((object)b == null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(DicomCodeItem a, DicomCodeItem b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Version)) return string.Format("({0},{1}:{2},\"{3}\")", Value, Scheme, Version, Meaning);
            return string.Format("({0},{1},\"{2}\")", Value, Scheme, Meaning);
        }
    }
}
