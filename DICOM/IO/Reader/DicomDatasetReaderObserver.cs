using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO;
using Dicom.IO.Buffer;

namespace Dicom.IO.Reader {
	public class DicomDatasetReaderObserver : IDicomReaderObserver {
		private Stack<DicomDataset> _datasets;
		private Stack<Encoding> _encodings;
		private Stack<DicomSequence> _sequences;
		private DicomFragmentSequence _fragment;

		public DicomDatasetReaderObserver(DicomDataset dataset) {
			_datasets = new Stack<DicomDataset>();
			_datasets.Push(dataset);

			_encodings = new Stack<Encoding>();
			_encodings.Push(DicomEncoding.Default);

			_sequences = new Stack<DicomSequence>();
		}

		public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data) {
			DicomElement element;
			switch (vr.Code) {
				case "AE": element = new DicomApplicationEntity(tag, data); break;
				case "AS": element = new DicomAgeString(tag, data); break;
				case "AT": element = new DicomAttributeTag(tag, data); break;
				case "CS": element = new DicomCodeString(tag, data); break;
				case "DA": element = new DicomDate(tag, data); break;
				case "DS": element = new DicomDecimalString(tag, data); break;
				case "DT": element = new DicomDateTime(tag, data); break;
				case "FD": element = new DicomFloatingPointDouble(tag, data); break;
				case "FL": element = new DicomFloatingPointSingle(tag, data); break;
				case "IS": element = new DicomIntegerString(tag, data); break;
				case "LO": element = new DicomLongString(tag, _encodings.Peek(), data); break;
				case "LT": element = new DicomLongText(tag, _encodings.Peek(), data); break;
				case "OB": element = new DicomOtherByte(tag, data); break;
				case "OF": element = new DicomOtherFloat(tag, data); break;
				case "OW": element = new DicomOtherWord(tag, data); break;
				case "PN": element = new DicomPersonName(tag, _encodings.Peek(), data); break;
				case "SH": element = new DicomShortString(tag, _encodings.Peek(), data); break;
				case "SL": element = new DicomSignedLong(tag, data); break;
				case "SS": element = new DicomSignedShort(tag, data); break;
				case "ST": element = new DicomShortText(tag, _encodings.Peek(), data); break;
				case "TM": element = new DicomTime(tag, data); break;
				case "UI": element = new DicomUniqueIdentifier(tag, data); break;
				case "UL": element = new DicomUnsignedLong(tag, data); break;
				case "UN": element = new DicomUnknown(tag, data); break;
				case "US": element = new DicomUnsignedShort(tag, data); break;
				case "UT": element = new DicomUnlimitedText(tag, _encodings.Peek(), data); break;
				default:
					throw new DicomDataException("Unhandled VR in DICOM parser observer: {0}", vr.Code);
			}

			if (element.Tag == DicomTag.SpecificCharacterSet) {
				Encoding encoding = DicomEncoding.Default;
				if (element.Count > 0)
					encoding = DicomEncoding.GetEncoding(element.Get<string>(0));
				_encodings.Pop();
				_encodings.Push(encoding);
			}

			DicomDataset ds = _datasets.Peek();
			ds.Add(element);
		}

		public void OnBeginSequence(IByteSource source, DicomTag tag, uint length) {
			DicomSequence sq = new DicomSequence(tag);
			_sequences.Push(sq);

			DicomDataset ds = _datasets.Peek();
			ds.Add(sq);
		}

		public void OnBeginSequenceItem(IByteSource source, uint length) {
			DicomSequence sq = _sequences.Peek();

			DicomDataset item = new DicomDataset();
			sq.Items.Add(item);

			_datasets.Push(item);

			_encodings.Push(_encodings.Peek());
		}

		public void OnEndSequenceItem() {
			if (_encodings.Count > 1)
				_encodings.Pop();

			_datasets.Pop();
		}

		public void OnEndSequence() {
			_sequences.Pop();
		}

		public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr) {
			if (vr == DicomVR.OB)
				_fragment = new DicomOtherByteFragment(tag);
			else if (vr == DicomVR.OW)
				_fragment = new DicomOtherWordFragment(tag);
			else
				throw new DicomDataException("Unexpected VR found for DICOM fragment sequence: {0}", vr.Code);

			DicomDataset ds = _datasets.Peek();
			ds.Add(_fragment);
		}

		public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data) {
			_fragment.Add(data);
		}

		public void OnEndFragmentSequence() {
			_fragment = null;
		}
	}
}
