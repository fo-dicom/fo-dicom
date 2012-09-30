using System;
using System.Collections.Generic;

using Dicom.IO.Buffer;

namespace Dicom.IO.Writer {
	public class DicomWriteLengthCalculator {
		private DicomTransferSyntax _syntax;
		private DicomWriteOptions _options;

		public DicomWriteLengthCalculator(DicomTransferSyntax syntax, DicomWriteOptions options) {
			_syntax = syntax;
			_options = options;
		}

		public uint Calculate(IEnumerable<DicomItem> items) {
			uint length = 0;
			foreach (DicomItem item in items)
				length += Calculate(item);
			return length;
		}

		public uint Calculate(DicomItem item) {
			// skip group lengths if not writing to file
			if (!_options.KeepGroupLengths && item.Tag.Element == 0x0000)
				return 0;

			uint length = 0;
			length += 4; // tag

			if (_syntax.IsExplicitVR) {
				length += 2; // vr
				if (item.ValueRepresentation.Is16bitLength) {
					length += 2; // length
				} else {
					length += 2; // reserved
					length += 4; // length
				}
			} else {
				length += 4; // length
			}

			if (item is DicomElement) {
				length += (uint)(item as DicomElement).Buffer.Size;
			} else if (item is DicomFragmentSequence) {
				DicomFragmentSequence sq = item as DicomFragmentSequence;
				// fragment item (offset table)
				length += 4; // tag
				length += 4; // length
				length += (uint)(sq.OffsetTable.Count / 4);

				foreach (IByteBuffer fragment in sq) {
					// fragment item
					length += 4; // tag
					length += 4; // length
					length += fragment.Size;
				}

				// sequence delimitation item
				length += 4; // tag
				length += 4; // length
			} else if (item is DicomSequence) {
				DicomSequence sq = item as DicomSequence;
				length += Calculate(sq);
			}

			return length;
		}

		public uint Calculate(DicomSequence sq) {
			uint length = 0;

			foreach (DicomDataset sqi in sq) {
				// sequence item
				length += 4; // tag
				length += 4; // length

				length += Calculate(sqi);

				if (!_options.ExplicitLengthSequenceItems) {
					// sequence item delimitation item
					length += 4; // tag
					length += 4; // length
				}
			}

			if (!_options.ExplicitLengthSequences && !sq.Tag.IsPrivate) {
				// sequence delimitation item
				length += 4; // tag
				length += 4; // length
			}

			return length;
		}
	}
}
