using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO;
using Dicom.IO.Buffer;
using Dicom.IO.Reader;
using Dicom.Log;

namespace Dicom.Log {
	public class DicomParserLogger : IDicomReaderObserver {
		private Logger _log;
		private LogLevel _level;
		private int _depth;
		private string _pad;

		public DicomParserLogger(Logger log, LogLevel level) {
			_log = log;
			_level = level;
			_depth = 0;
			_pad = String.Empty;
		}

		public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data) {
			_log.Log(_level, "{0:x8}: {1}{2} {3} {4} [{5}]", source.Marker, _pad, tag, vr.Code, tag.DictionaryEntry.Name, data.Size);
		}

		public void OnBeginSequence(IByteSource source, DicomTag tag, uint length) {
			_log.Log(_level, "{0:x8}: {1}{2} SQ {3}", source.Marker, _pad, tag, tag.DictionaryEntry.Name, length);
			IncreaseDepth();
		}

		public void OnBeginSequenceItem(IByteSource source, uint length) {
			_log.Log(_level, "{0:x8}: {1}Item:", source.Marker, _pad);
			IncreaseDepth();
		}

		public void OnEndSequenceItem() {
			DecreaseDepth();
		}

		public void OnEndSequence() {
			DecreaseDepth();
		}

		public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr) {
			_log.Log(_level, "{0:x8}: {1}{2} {3} {4}", source.Marker, _pad, tag, vr.Code, tag.DictionaryEntry.Name);
			IncreaseDepth();
		}

		public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data) {
			_log.Log(_level, "{0:x8}: {1}Fragment [{2}]", source.Marker, _pad, data.Size);
		}

		public void OnEndFragmentSequence() {
			DecreaseDepth();
		}

		private void IncreaseDepth() {
			_depth++;
		}

		private void DecreaseDepth() {
			_depth--;
		}
	}
}
