// Copyright (c) 2012-2026 fo-dicom contributors.
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
            if (sequence.Items.Count == 0)
            {
                throw new DicomDataException("No code item found in sequence.");
            }

            Add(sequence.Items[0]);
        }

        public DicomCodeItem(string value, string scheme, string meaning, string version = null)
        {
            value ??= string.Empty; // avoid null value
            if (value.StartsWith("http", StringComparison.InvariantCultureIgnoreCase) || value.StartsWith("urn:", StringComparison.InvariantCultureIgnoreCase))
            {
                Add(DicomTag.URNCodeValue, value);
                if (!string.IsNullOrEmpty(scheme))
                {
                    Add(DicomTag.CodingSchemeDesignator, scheme);
                }
            }
            else if (value.Length > 16)
            {
                Add(DicomTag.LongCodeValue, value);
                Add(DicomTag.CodingSchemeDesignator, scheme);
            }
            else
            {
            Add(DicomTag.CodeValue, value);
            Add(DicomTag.CodingSchemeDesignator, scheme);
            }

            Add(DicomTag.CodeMeaning, meaning);
            if (version != null)
            {
                Add(DicomTag.CodingSchemeVersion, version);
            }
        }

        public string Value
        {
            get
            {
                if (TryGetValue<string>(DicomTag.CodeValue, 0, out var codeValue))
                {
                    return codeValue;
                }
                if (TryGetValue<string>(DicomTag.LongCodeValue, 0, out var longCodeValue))
                {
                    return longCodeValue;
                }
                if (TryGetValue<string>(DicomTag.URNCodeValue, 0, out var urnCodeValue))
                {
                    return urnCodeValue;
                }
                return string.Empty;
            }
        }
            
        public string Scheme => GetValueOrDefault(DicomTag.CodingSchemeDesignator, 0, string.Empty);

        public string Meaning => GetValueOrDefault(DicomTag.CodeMeaning, 0, string.Empty);

        public string Version => GetValueOrDefault(DicomTag.CodingSchemeVersion, 0, string.Empty);

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(DicomCodeItem))
            {
                return false;
            }
            return Value == ((DicomCodeItem)obj).Value && Scheme == ((DicomCodeItem)obj).Scheme
                   && Version == ((DicomCodeItem)obj).Version;
        }

        public static bool operator ==(DicomCodeItem a, DicomCodeItem b)
        {
            if (((object)a == null) && ((object)b == null))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(DicomCodeItem a, DicomCodeItem b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Scheme, Version);
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Version))
            {
                return string.Format("({0},{1}:{2},\"{3}\")", Value, Scheme, Version, Meaning);
            }
            return string.Format("({0},{1},\"{2}\")", Value, Scheme, Meaning);
        }
    }
}
