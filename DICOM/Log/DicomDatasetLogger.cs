using System;
using System.Text;

using Dicom.IO.Buffer;
using Dicom.Log;

namespace Dicom.Log {
	public class DicomDatasetLogger : IDicomDatasetWalker {
		private Logger _log;
		private LogLevel _level;
		private int _width = 128;
		private int _value = 64;
		private int _depth = 0;
		private string _pad = String.Empty;

		public DicomDatasetLogger(Logger logger, LogLevel level, int width = 128, int valueLength = 64) {
			_log = logger;
			_level = level;
			_width = width;
			_value = valueLength;
		}

		public void OnBeginWalk(DicomDatasetWalker walker, DicomDatasetWalkerCallback callback) {
		}

		public bool OnElement(DicomElement element) {
			StringBuilder sb = new StringBuilder();
			if (_depth > 0)
				sb.Append(_pad).Append("> ");
			sb.Append(element.Tag);
			sb.Append(' ');
			sb.Append(element.ValueRepresentation.Code);
			sb.Append(' ');
			if (element.Length == 0) {
				sb.Append("(no value available)");
			} else if (element.ValueRepresentation.IsString) {
				sb.Append('[');
				string val = element.Get<string>();
				if (val.Length > (_value - 2 - sb.Length)) {
					sb.Append(val.Substring(0, _value - 2 - sb.Length));
					sb.Append(')');
				} else {
					sb.Append(val);
					sb.Append(']');
				}
			} else if (element.Length >= 1024) {
				sb.Append("<skipping large element>");
			} else {
				var val = String.Join("/", element.Get<string[]>());
				if (val.Length > (_value - sb.Length)) {
					sb.Append(val.Substring(0, _value - sb.Length));
				} else {
					sb.Append(val);
				}
			}
			while (sb.Length < _value)
				sb.Append(' ');
			sb.Append('#');
			string name = element.Tag.DictionaryEntry.Keyword;
			sb.AppendFormat("{0,6}, {1}", element.Length, name.Substring(0, System.Math.Min(_width - sb.Length - 9, name.Length)));
			_log.Log(_level, sb.ToString());
			return true;
		}

		public bool OnBeginSequence(DicomSequence sequence) {
			_log.Log(_level, "{0}{1} SQ {2}", (_depth > 0) ? _pad + "> " : "", sequence.Tag, sequence.Tag.DictionaryEntry.Name);
			IncreaseDepth();
			return true;
		}

		public bool OnBeginSequenceItem(DicomDataset dataset) {
			_log.Log(_level, _pad + "Item:");
			IncreaseDepth();
			return true;
		}

		public bool OnEndSequenceItem() {
			DecreaseDepth();
			return true;
		}

		public bool OnEndSequence() {
			DecreaseDepth();
			return true;
		}

		public bool OnBeginFragment(DicomFragmentSequence fragment) {
			_log.Log(_level, "{0}{1} {2} {3} [{4} offsets, {5} fragments]", (_depth > 0) ? _pad + "> " : "", 
				fragment.Tag, fragment.ValueRepresentation.Code, fragment.Tag.DictionaryEntry.Name, 
				fragment.OffsetTable.Count, fragment.Fragments.Count);
			return true;
		}

		public bool OnFragmentItem(IByteBuffer item) {
			return true;
		}

		public bool OnEndFragment() {
			return true;
		}

		public void OnEndWalk() {
		}

		private void IncreaseDepth() {
			_depth++;

			_pad = String.Empty;
			for (int i = 0; i < _depth; i++)
				_pad += "  ";
		}

		private void DecreaseDepth() {
			_depth--;

			_pad = String.Empty;
			for (int i = 0; i < _depth; i++)
				_pad += "  ";
		}
	}
}
