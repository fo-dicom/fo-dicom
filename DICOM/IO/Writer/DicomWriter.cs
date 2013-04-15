using System;
using System.Collections.Generic;

using Dicom.IO;
using Dicom.IO.Buffer;

namespace Dicom.IO.Writer {
	public class DicomWriter : IDicomDatasetWalker {
		private const uint UndefinedLength = 0xffffffff;

		private DicomTransferSyntax _syntax;
		private DicomWriteOptions _options;

		private IByteTarget _target;

		private Stack<DicomSequence> _sequences;
		private DicomDatasetWalkerCallback _callback;

		public DicomWriter(DicomTransferSyntax syntax, DicomWriteOptions options, IByteTarget target) {
			_syntax = syntax;
			_options = options ?? DicomWriteOptions.Default;
			_target = target;
		}

		public DicomTransferSyntax Syntax {
			get { return _syntax; }
			set { _syntax = value; }
		}

		public void OnBeginWalk(DicomDatasetWalker walker, DicomDatasetWalkerCallback callback) {
			_target.Endian = _syntax.Endian;
			_callback = callback;
			_sequences = new Stack<DicomSequence>();
		}

		public bool OnElement(DicomElement element) {
			WriteTagHeader(element.Tag, element.ValueRepresentation, element.Length);

			IByteBuffer buffer = element.Buffer;
			if (buffer is EndianByteBuffer) {
				EndianByteBuffer ebb = buffer as EndianByteBuffer;
				if (ebb.Endian != Endian.LocalMachine && ebb.Endian == _target.Endian)
				    buffer = ebb.Internal;
			} else if (_target.Endian != Endian.LocalMachine) {
				if (element.ValueRepresentation.UnitSize > 1)
					buffer = new SwapByteBuffer(buffer, element.ValueRepresentation.UnitSize);
			}

			if (element.Length >= _options.LargeObjectSize) {
				_target.Write(buffer.Data, 0, buffer.Size, OnEndWriteBuffer, null);
				return false;
			} else {
				_target.Write(buffer.Data);
				return true;
			}
		}

		public bool OnBeginSequence(DicomSequence sequence) {
			uint length = UndefinedLength;

			if (_options.ExplicitLengthSequences || sequence.Tag.IsPrivate) {
				DicomWriteLengthCalculator calc = new DicomWriteLengthCalculator(_syntax, _options);
				length = calc.Calculate(sequence);
			}

			_sequences.Push(sequence);

			WriteTagHeader(sequence.Tag, DicomVR.SQ, length);
			return true;
		}

		public bool OnBeginSequenceItem(DicomDataset dataset) {
			uint length = UndefinedLength;

			if (_options.ExplicitLengthSequenceItems) {
				DicomWriteLengthCalculator calc = new DicomWriteLengthCalculator(_syntax, _options);
				length = calc.Calculate(dataset);
			}

			WriteTagHeader(DicomTag.Item, DicomVR.NONE, length);
			return true;
		}

		public bool OnEndSequenceItem() {
			DicomSequence sequence = _sequences.Peek();

			if (!_options.ExplicitLengthSequenceItems) {
				WriteTagHeader(DicomTag.ItemDelimitationItem, DicomVR.NONE, 0);
			}

			return true;
		}

		public bool OnEndSequence() {
			DicomSequence sequence = _sequences.Pop();

			if (!_options.ExplicitLengthSequences && !sequence.Tag.IsPrivate) {
				WriteTagHeader(DicomTag.SequenceDelimitationItem, DicomVR.NONE, 0);
			}

			return true;
		}

		public bool OnBeginFragment(DicomFragmentSequence fragment) {
			WriteTagHeader(fragment.Tag, fragment.ValueRepresentation, UndefinedLength);
			WriteTagHeader(DicomTag.Item, DicomVR.NONE, (uint)(fragment.OffsetTable.Count * 4));
			foreach (uint offset in fragment.OffsetTable)
				_target.Write(offset);
			return true;
		}

		public bool OnFragmentItem(IByteBuffer item) {
			WriteTagHeader(DicomTag.Item, DicomVR.NONE, item.Size);

			IByteBuffer buffer = item;
			if (buffer is EndianByteBuffer) {
				EndianByteBuffer ebb = buffer as EndianByteBuffer;
				if (ebb.Endian != Endian.LocalMachine && ebb.Endian == _target.Endian)
					buffer = ebb.Internal;
			}

			if (item.Size >= _options.LargeObjectSize) {
				_target.Write(buffer.Data, 0, buffer.Size, OnEndWriteBuffer, null);
				return false;
			} else {
				_target.Write(buffer.Data);
				return true;
			}
		}

		public bool OnEndFragment() {
			WriteTagHeader(DicomTag.SequenceDelimitationItem, DicomVR.NONE, 0);
			return true;
		}

		public void OnEndWalk() {
			_sequences = null;
			_callback = null;
		}

		private void WriteTagHeader(DicomTag tag, DicomVR vr, uint length) {
			_target.Write(tag.Group);
			_target.Write(tag.Element);

			if (_syntax.IsExplicitVR && vr != DicomVR.NONE) {
				_target.Write((byte)vr.Code[0]);
				_target.Write((byte)vr.Code[1]);

				if (vr.Is16bitLength) {
					_target.Write((ushort)length);
				} else {
					_target.Write((ushort)0);
					_target.Write(length);
				}
			} else {
				_target.Write(length);
			}
		}

		private void OnEndWriteBuffer(IByteTarget target, object state) {
			_callback();
		}
	}
}
