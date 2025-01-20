// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Linq;

namespace FellowOakDicom
{
    public static class DicomDatasetGetterExtensions
    {

        //public static DicomItem GetItem(this DicomDataset dataset, DicomTag tag)
        //{
        //    return dataset.GetDicomItem<DicomItem>(tag);
        //}

        public static DicomLOItem GetItem(this DicomDataset dataset, DicomTagLO tag)
        {
            return new DicomLOItem(dataset.GetDicomItem<DicomLongString>(tag));
        }

        public static DicomPersonName GetItem(this DicomDataset dataset, DicomTagPN tag)
        {
            return dataset.GetDicomItem<DicomPersonName>(tag);
        }

        public static DicomApplicationEntity GetItem(this DicomDataset dataset, DicomTagAE tag)
        {
            return dataset.GetDicomItem<DicomApplicationEntity>(tag);
        }

        public static DicomAgeString GetItem(this DicomDataset dataset, DicomTagAS tag)
        {
            return dataset.GetDicomItem<DicomAgeString>(tag);
        }

        public static DicomAttributeTag GetItem(this DicomDataset dataset, DicomTagAT tag)
        {
            return dataset.GetDicomItem<DicomAttributeTag>(tag);
        }

        public static DicomCodeString GetItem(this DicomDataset dataset, DicomTagCS tag)
        {
            return dataset.GetDicomItem<DicomCodeString>(tag);
        }

        public static DicomDAItem GetItem(this DicomDataset dataset, DicomTagDA tag)
        {
            return new DicomDAItem(dataset.GetDicomItem<DicomDate>(tag));
        }

        public static DicomDecimalString GetItem(this DicomDataset dataset, DicomTagDS tag)
        {
            return dataset.GetDicomItem<DicomDecimalString>(tag);
        }


        public static DicomUnsignedShort GetItem(this DicomDataset dataset, DicomTagUS tag)
        {
            return dataset.GetDicomItem<DicomUnsignedShort>(tag);
        }


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

    public sealed class DicomLOItem
    {
        private readonly DicomLongString _dicomItem;

        internal DicomLOItem(DicomLongString dicomItem)
        {
            _dicomItem = dicomItem;
        }

        public bool Exists => _dicomItem != null;

        public string Value => _dicomItem?.StringValue ?? string.Empty;

        public string[] Values
        {
            get
            {
                if (_dicomItem == null)
                {
                    return Array.Empty<string>();
                }
                else
                {
                    _dicomItem.EnsureSplitValues();
                    return _dicomItem._values;
                }
            }
        }
    }

    public sealed class DicomDAItem
    {
        private readonly DicomDate _dicomItem;

        internal DicomDAItem(DicomDate dicomItem)
        {
            _dicomItem = dicomItem;
        }

        public bool Exists => _dicomItem != null;

        public DateTime? Value
        {
            get
            {
                if (_dicomItem == null)
                {
                    return null;
                }
                _dicomItem.EnsureParseDates();
                return _dicomItem._dateValues?.FirstOrDefault();
            }
        }

        public DateTime[] Values
        {
            get
            {
                if (_dicomItem == null)
                {
                    return Array.Empty<DateTime>();
                }
                _dicomItem.EnsureParseDates();
                return _dicomItem._dateValues;
            }
        }

        public string[] StringValues
        {
            get
            {
                if (_dicomItem == null)
                {
                    return Array.Empty<string>();
                }
                else
                {
                    _dicomItem.EnsureSplitValues();
                    return _dicomItem._values;
                }
            }
        }
    }
}
