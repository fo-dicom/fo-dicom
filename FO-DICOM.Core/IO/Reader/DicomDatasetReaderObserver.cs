// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Text;
using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.IO.Reader
{

    internal class DicomDatasetReaderObserver : IDicomReaderObserver
    {

        private readonly Stack<DicomDataset> _datasets;

        private readonly Stack<Encoding[]> _encodings;

        private readonly Stack<DicomSequence> _sequences;

        private DicomFragmentSequence _fragment;


        public DicomDatasetReaderObserver(DicomDataset dataset)
            : this(dataset, DicomEncoding.Default)
        {
        }

        public DicomDatasetReaderObserver(DicomDataset dataset, Encoding fallbackEncoding)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException(nameof(fallbackEncoding));
            }
            _datasets = new Stack<DicomDataset>();
            _datasets.Push(dataset);

            _encodings = new Stack<Encoding[]>();
            _encodings.Push(new [] { fallbackEncoding });

            _sequences = new Stack<DicomSequence>();
        }

        public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data)
        {
            DicomElement element = vr.Code switch
            {
                "AE" => new DicomApplicationEntity(tag, data),
                "AS" => new DicomAgeString(tag, data),
                "AT" => new DicomAttributeTag(tag, data),
                "CS" => new DicomCodeString(tag, data),
                "DA" => new DicomDate(tag, data),
                "DS" => new DicomDecimalString(tag, data),
                "DT" => new DicomDateTime(tag, data),
                "FD" => new DicomFloatingPointDouble(tag, data),
                "FL" => new DicomFloatingPointSingle(tag, data),
                "IS" => new DicomIntegerString(tag, data),
                "LO" => new DicomLongString(tag, _encodings.Peek(), data),
                "LT" => new DicomLongText(tag, _encodings.Peek(), data),
                "OB" => new DicomOtherByte(tag, data),
                "OD" => new DicomOtherDouble(tag, data),
                "OF" => new DicomOtherFloat(tag, data),
                "OL" => new DicomOtherLong(tag, data),
                "OV" => new DicomOtherVeryLong(tag, data),
                "OW" => new DicomOtherWord(tag, data),
                "PN" => new DicomPersonName(tag, _encodings.Peek(), data),
                "SH" => new DicomShortString(tag, _encodings.Peek(), data),
                "SL" => new DicomSignedLong(tag, data),
                "SS" => new DicomSignedShort(tag, data),
                "ST" => new DicomShortText(tag, _encodings.Peek(), data),
                "SV" => new DicomSignedVeryLong(tag, data),
                "TM" => new DicomTime(tag, data),
                "UC" => new DicomUnlimitedCharacters(tag, _encodings.Peek(), data),
                "UI" => new DicomUniqueIdentifier(tag, data),
                "UL" => new DicomUnsignedLong(tag, data),
                "UN" => new DicomUnknown(tag, data),
                "UR" => new DicomUniversalResource(tag, _encodings.Peek(), data),
                "US" => new DicomUnsignedShort(tag, data),
                "UT" => new DicomUnlimitedText(tag, _encodings.Peek(), data),
                "UV" => new DicomUnsignedVeryLong(tag, data),
                _ => throw new DicomDataException($"Unhandled VR in DICOM parser observer: {vr.Code}"),
            };
            if (element.Tag == DicomTag.SpecificCharacterSet)
            {
                Encoding[] encodings = _encodings.Peek();
                if (element.Count > 0)
                {
                    encodings = DicomEncoding.GetEncodings(element.Get<string[]>(0));
                }
                _encodings.Pop();
                _encodings.Push(encodings);
            }

            DicomDataset ds = _datasets.Peek();
            ds.AddOrUpdate(element);
        }

        public void OnBeginSequence(IByteSource source, DicomTag tag, uint length)
        {
            var sq = new DicomSequence(tag);
            _sequences.Push(sq);

            DicomDataset ds = _datasets.Peek();
            ds.AddOrUpdate(sq);
        }

        public void OnBeginSequenceItem(IByteSource source, uint length)
        {
            DicomSequence sq = _sequences.Peek();

            DicomDataset item = new DicomDataset().NotValidated();
            sq.Items.Add(item);

            _datasets.Push(item);

            var encoding = _encodings.Peek();
            item.FallbackEncodings = encoding;
            _encodings.Push(encoding);
        }

        public void OnEndSequenceItem()
        {
            if (_encodings.Count > 1)
            {
                _encodings.Pop();
            }

            _datasets.Pop();
        }

        public void OnEndSequence() => _sequences.Pop();

        public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr)
        {
            if (vr == DicomVR.OB)
            {
                _fragment = new DicomOtherByteFragment(tag);
            }
            else if (vr == DicomVR.OW)
            {
                _fragment = new DicomOtherWordFragment(tag);
            }
            else
            {
                throw new DicomDataException($"Unexpected VR found for DICOM fragment sequence: {vr.Code}");
            }
        }

        public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data)
        {
            if (data == null)
            {
                // if a null-buffer is added, then the FragmentSequence is not complete and shall not be added to the DicomDataset
                _fragment = null;
            }
            else
            {
                _fragment?.Add(data);
            }
        }

        public void OnEndFragmentSequence()
        {
            if (_fragment != null)
            {
                // only add the dicomtag if no fragment has been skipped due to SkipLargeTags
                DicomDataset ds = _datasets.Peek();
                ds.AddOrUpdate(_fragment);
            }
            _fragment = null;
        }

    }
}
