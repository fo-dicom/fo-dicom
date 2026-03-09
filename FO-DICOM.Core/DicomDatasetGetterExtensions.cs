// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom
{
    public static class DicomDatasetGetterExtensions
    {

        //public static DicomItem GetItem(this DicomDataset dataset, DicomTag tag)
        //{
        //    return dataset.GetDicomItem<DicomItem>(tag);
        //}

        public static IDicomStrings GetItem(this DicomDataset dataset, DicomTagLO tag)
            => dataset.GetDicomItem<DicomLongString>(tag) ?? (IDicomStrings)EmptyDicomStrings.Instance;

        public static IDicomString GetItem(this DicomDataset dataset, DicomTagLT tag)
            => dataset.GetDicomItem<DicomLongText>(tag) ?? (IDicomString)EmptyDicomString.Instance;

        public static IDicomPersonName GetItem(this DicomDataset dataset, DicomTagPN tag)
            => dataset.GetDicomItem<DicomPersonName>(tag) ?? (IDicomPersonName)EmptyDicomPersonName.Instance;

        public static IDicomStrings GetItem(this DicomDataset dataset, DicomTagAE tag)
            => dataset.GetDicomItem<DicomApplicationEntity>(tag) ?? (IDicomStrings)EmptyDicomStrings.Instance;

        public static IDicomStrings GetItem(this DicomDataset dataset, DicomTagAS tag)
            => dataset.GetDicomItem<DicomAgeString>(tag) ?? (IDicomStrings)EmptyDicomStrings.Instance;

        public static IDicomAttributeTag GetItem(this DicomDataset dataset, DicomTagAT tag)
            => dataset.GetDicomItem<DicomAttributeTag>(tag) ?? EmptyDicomAttributeTag.Instance;

        public static IDicomStrings GetItem(this DicomDataset dataset, DicomTagCS tag)
            => dataset.GetDicomItem<DicomCodeString>(tag) ?? (IDicomStrings)EmptyDicomStrings.Instance;

        public static IDicomDate GetItem(this DicomDataset dataset, DicomTagDA tag)
            => dataset.GetDicomItem<DicomDate>(tag) ?? EmptyDicomDate.Instance;

        public static IDicomDate GetItem(this DicomDataset dataset, DicomTagDT tag)
            => dataset.GetDicomItem<DicomDateTime>(tag) ?? EmptyDicomDate.Instance;

        public static IDicomDate GetItem(this DicomDataset dataset, DicomTagTM tag)
            => dataset.GetDicomItem<DicomTime>(tag) ?? EmptyDicomDate.Instance;

        public static IDicomValue<decimal> GetItem(this DicomDataset dataset, DicomTagDS tag)
            => dataset.GetDicomItem<DicomDecimalString>(tag) ?? EmptyDicomValue<decimal>.Instance;

        public static IDicomValue<double> GetItem(this DicomDataset dataset, DicomTagFD tag)
            => dataset.GetDicomItem<DicomFloatingPointDouble>(tag) ?? EmptyDicomValue<double>.Instance;

        public static IDicomValue<float> GetItem(this DicomDataset dataset, DicomTagFL tag)
            => dataset.GetDicomItem<DicomFloatingPointSingle>(tag) ?? EmptyDicomValue<float>.Instance;

        public static IDicomValue<int> GetItem(this DicomDataset dataset, DicomTagIS tag)
            => dataset.GetDicomItem<DicomIntegerString>(tag) ?? EmptyDicomValue<int>.Instance;

        public static IDicomValue<ushort> GetItem(this DicomDataset dataset, DicomTagUS tag)
            => dataset.GetDicomItem<DicomUnsignedShort>(tag) ?? EmptyDicomValue<ushort>.Instance;

        public static IDicomValue<byte> GetItem(this DicomDataset dataset, DicomTagOB tag)
            => dataset.GetDicomItem<DicomOtherByte>(tag) ?? EmptyDicomValue<byte>.Instance;

        public static IDicomValue<ushort> GetItem(this DicomDataset dataset, DicomTagOW tag)
            => dataset.GetDicomItem<DicomOtherWord>(tag) ?? EmptyDicomValue<ushort>.Instance;

        public static IDicomValue<uint> GetItem(this DicomDataset dataset, DicomTagOL tag)
            => dataset.GetDicomItem<DicomOtherLong>(tag) ?? EmptyDicomValue<uint>.Instance;

        public static IDicomValue<double> GetItem(this DicomDataset dataset, DicomTagOD tag)
            => dataset.GetDicomItem<DicomOtherDouble>(tag) ?? EmptyDicomValue<double>.Instance;

        public static IDicomValue<float> GetItem(this DicomDataset dataset, DicomTagOF tag)
            => dataset.GetDicomItem<DicomOtherFloat>(tag) ?? EmptyDicomValue<float>.Instance;

        public static IDicomValue<ulong> GetItem(this DicomDataset dataset, DicomTagOV tag)
            => dataset.GetDicomItem<DicomOtherVeryLong>(tag) ?? EmptyDicomValue<ulong>.Instance;

        public static IDicomStrings GetItem(this DicomDataset dataset, DicomTagSH tag)
            => dataset.GetDicomItem<DicomShortString>(tag) ?? (IDicomStrings)EmptyDicomStrings.Instance;

        public static IDicomValue<int> GetItem(this DicomDataset dataset, DicomTagSL tag)
            => dataset.GetDicomItem<DicomSignedLong>(tag) ?? EmptyDicomValue<int>.Instance;

        public static IDicomValue<short> GetItem(this DicomDataset dataset, DicomTagSS tag)
            => dataset.GetDicomItem<DicomSignedShort>(tag) ?? EmptyDicomValue<short>.Instance;

        public static IDicomString GetItem(this DicomDataset dataset, DicomTagST tag)
            => dataset.GetDicomItem<DicomShortText>(tag) ?? (IDicomString)EmptyDicomString.Instance;

        public static IDicomValue<long> GetItem(this DicomDataset dataset, DicomTagSV tag)
            => dataset.GetDicomItem<DicomSignedVeryLong>(tag) ?? EmptyDicomValue<long>.Instance;

        public static IDicomStrings GetItem(this DicomDataset dataset, DicomTagUC tag)
            => dataset.GetDicomItem<DicomUnlimitedCharacters>(tag) ?? (IDicomStrings)EmptyDicomStrings.Instance;

        public static IDicomUniqueIdentifier GetItem(this DicomDataset dataset, DicomTagUI tag)
            => dataset.GetDicomItem<DicomUniqueIdentifier>(tag) ?? EmptyDicomUniqueIdentifier.Instance;

        public static IDicomValue<uint> GetItem(this DicomDataset dataset, DicomTagUL tag)
            => dataset.GetDicomItem<DicomUnsignedLong>(tag) ?? EmptyDicomValue<uint>.Instance;

        public static IDicomValue<byte> GetItem(this DicomDataset dataset, DicomTagUN tag)
            => dataset.GetDicomItem<DicomUnknown>(tag) ?? EmptyDicomValue<byte>.Instance;

        public static IDicomString GetItem(this DicomDataset dataset, DicomTagUR tag)
            => dataset.GetDicomItem<DicomUniversalResource>(tag) ?? (IDicomString)EmptyDicomString.Instance;

        public static IDicomString GetItem(this DicomDataset dataset, DicomTagUT tag)
            => dataset.GetDicomItem<DicomUnlimitedText>(tag) ?? (IDicomString)EmptyDicomString.Instance;

        public static IDicomValue<ulong> GetItem(this DicomDataset dataset, DicomTagUV tag)
            => dataset.GetDicomItem<DicomUnsignedVeryLong>(tag) ?? EmptyDicomValue<ulong>.Instance;


        public static DicomSequence GetItem(this DicomDataset dataset, DicomTagSQ tag)
            => dataset.TryGetSequence(tag, out var seq) ? seq : new DicomSequence(tag);


        public static DicomDataset SetItem(this DicomDataset dataset, DicomTagLO tag, params string[] values)
        {
            var newItem = new DicomLongString(tag, values) { TargetEncodings = DicomEncoding.DefaultArray };
            return dataset.DoAdd(newItem, true);
        }

        public static DicomDataset SetItem(this DicomDataset dataset, DicomTagDA tag, params DateTime[] values)
        {
            var newItem = new DicomDate(tag, values);
            return dataset.DoAdd(newItem, true);
        }

        public static DicomDataset SetItem(this DicomDataset dataset, DicomTagDA tag, DicomDateRange range)
        {
            var newItem = new DicomDate(tag, range);
            return dataset.DoAdd(newItem, true);
        }


    }

    // TODO: DateRange, pixeldata-buffer, Enums (basierend on US)
    //       zb RedPaletteColorLookupTabelData (obwohl OW braucht man auch byte[]
    //   wie lösen: get value or default?
    // soll value eine Exception schmeißen, wenn es nicht gibt??
    // Es gibt Tags wie SmallestImagePixelValue (SS/US) oder PixelData (OW/OB), die mehree VRs haben können

    public interface IDicomString
    {
        bool Exists { get; }
        string Value { get; }
    }

    internal struct EmptyDicomString : IDicomString
    {
        public static EmptyDicomString Instance { get; } = new EmptyDicomString();

        public bool Exists => true;
        public string Value => string.Empty;
    }

    public interface IDicomStrings
    {
        bool Exists { get; }
        string Value { get; }
        string[] Values { get; }
    }

    internal struct EmptyDicomStrings : IDicomStrings
    {
        public static EmptyDicomStrings Instance { get; } = new EmptyDicomStrings();
        public bool Exists => false;
        public string Value => string.Empty;
        public string[] Values => Array.Empty<string>();
    }


    public interface IDicomPersonName : IDicomStrings
    {
        string Last { get; }
        string First { get; }
        string Middle { get; }
        string Prefix { get; }
        string Suffix { get; }
    }

    internal struct EmptyDicomPersonName : IDicomPersonName
    {
        public static EmptyDicomPersonName Instance { get; } = new EmptyDicomPersonName();

        public bool Exists => false;
        public string Value => string.Empty;
        public string[] Values => Array.Empty<string>();
        public string Last => string.Empty;
        public string First => string.Empty;
        public string Middle => string.Empty;
        public string Prefix => string.Empty;
        public string Suffix => string.Empty;
    }


    public interface IDicomDate
    {
        bool Exists { get; }
        DateTime? Value { get; }
        DateTime[] Values { get; }
        string[] StringValues { get; }
    }

    internal struct EmptyDicomDate: IDicomDate
    {
        public static IDicomDate Instance { get; } = new EmptyDicomDate();

        public bool Exists => false;

        public DateTime? Value => null;

        public DateTime[] Values => Array.Empty<DateTime>();

        public string[] StringValues => Array.Empty<string>();
    }

    public interface IDicomUniqueIdentifier
    {
        bool Exists { get; }
        DicomUID Value { get; }
        DicomUID[] Values { get; }
        string[] StringValues { get; }
        string StringValue { get; }
    }

    internal struct EmptyDicomUniqueIdentifier : IDicomUniqueIdentifier
    {
        public static IDicomUniqueIdentifier Instance { get; } = new EmptyDicomUniqueIdentifier();

        public bool Exists => false;
        public DicomUID Value => null;
        public DicomUID[] Values => Array.Empty<DicomUID>();
        public string[] StringValues => Array.Empty<string>();
        public string StringValue => string.Empty;
    }

    public interface IDicomValue<T> where T:struct
    {
        bool Exists { get; }
        T Value { get; }
        T[] Values { get; }
        string[] StringValues { get; }
    }

    internal struct EmptyDicomValue<T> : IDicomValue<T> where T: struct
    {
        public static IDicomValue<T> Instance { get; } = new EmptyDicomValue<T>();

        public bool Exists => false;
        public T Value => throw new NullReferenceException();
        public T[] Values => Array.Empty<T>();
        public string[] StringValues => Array.Empty<string>();
    }

    public interface IDicomAttributeTag
    {
        bool Exists { get; }
        DicomTag? Value { get; }
        DicomTag[] Values { get; }
        string[] StringValues { get; }
    }

    internal struct EmptyDicomAttributeTag : IDicomAttributeTag
    {
        public static IDicomAttributeTag Instance { get; } = new EmptyDicomAttributeTag();

        public bool Exists => false;
        public DicomTag? Value => null;
        public DicomTag[] Values => Array.Empty<DicomTag>();
        public string[] StringValues => Array.Empty<string>();
    }
}