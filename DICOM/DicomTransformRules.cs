using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Dicom.IO.Buffer;

namespace Dicom {
	public interface IDicomTransformRule {
		void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null);
	}

	public class DicomTransformRuleSet : IDicomTransformRule {
		#region Private Members
		private List<IDicomTransformRule> _transformRules;
		private DicomMatchRuleSet _conditions;
		#endregion

		#region Public Constructor
		public DicomTransformRuleSet() {
			_transformRules = new List<IDicomTransformRule>();
		}

		public DicomTransformRuleSet(params IDicomTransformRule[] rules) {
			_transformRules = new List<IDicomTransformRule>(rules);
		}

		public DicomTransformRuleSet(DicomMatchRuleSet conditions, params IDicomTransformRule[] rules) {
			_conditions = conditions;
			_transformRules = new List<IDicomTransformRule>(rules);
		}
		#endregion

		#region Public Properties
		public DicomMatchRuleSet Conditions {
			get {
				if (_conditions == null)
					_conditions = new DicomMatchRuleSet();
				return _conditions;
			}
			set { _conditions = value; }
		}
		#endregion

		#region Public Methods
		public void Add(IDicomTransformRule rule) {
			_transformRules.Add(rule);
		}

		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (_conditions != null)
				if (!_conditions.Match(dataset))
					return;

			foreach (IDicomTransformRule rule in _transformRules)
				rule.Transform(dataset);
		}
		#endregion
	}

	/// <summary>
	/// Remove an element from a DICOM dataset.
	/// </summary>
	public class RemoveElementDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomMaskedTag _mask;
		#endregion

		#region Public Constructor
		public RemoveElementDicomTransformRule(DicomMaskedTag mask) {
			_mask = mask;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			var remove = dataset.EnumerateMasked(_mask).Select(x => x.Tag).ToList();
			foreach (DicomTag tag in remove) {
				dataset.CopyTo(modifiedAttributesSequenceItem, tag);
				dataset.Remove(tag);
			}
		}

		public override string ToString() {
			string name = String.Format("[{0}]", _mask);
			return String.Format("'{0}' remove", name);
		}
		#endregion
	}

	/// <summary>
	/// Sets the value of a DICOM element.
	/// </summary>
	public class SetValueDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private string _value;
		#endregion

		#region Public Constructor
		public SetValueDicomTransformRule(DicomTag tag, string value) {
			_tag = tag;
			_value = value;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
			dataset.Add(_tag, _value);
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' set '{1}'", name, _value);
		}
		#endregion
	}

	/// <summary>
	/// Maps the value of a DICOM element to a value.
	/// </summary>
	public class MapValueDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private string _match;
		private string _value;
		#endregion

		#region Public Constructor
		public MapValueDicomTransformRule(DicomTag tag, string match, string value) {
			_tag = tag;
			_match = match;
			_value = value;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag) && dataset.Get<string>(_tag, -1, String.Empty) == _match) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				dataset.Add(_tag, _value);
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' map '{1}' -> '{2}'", name, _match, _value);
		}
		#endregion
	}

	/// <summary>
	/// Copies the value of a DICOM element to another DICOM element.
	/// </summary>
	public class CopyValueDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _src;
		private DicomTag _dst;
		#endregion

		#region Public Constructor
		public CopyValueDicomTransformRule(DicomTag src, DicomTag dst) {
			_src = src;
			_dst = dst;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_src)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _dst);
				dataset.Add(_dst, dataset.Get<IByteBuffer>(_src));
			}
		}

		public override string ToString() {
			string sname = String.Format("{0} {1}", _src, _src.DictionaryEntry.Name);
			string dname = String.Format("{0} {1}", _dst, _dst.DictionaryEntry.Name);
			return String.Format("'{0}' copy to '{1}'", sname, dname);
		}
		#endregion
	}

	/// <summary>
	/// Performs a regular expression replace operation on a DICOM element value.
	/// </summary>
	public class RegexDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private string _pattern;
		private string _replacement;
		#endregion

		#region Public Constructor
		public RegexDicomTransformRule(DicomTag tag, string pattern, string replacement) {
			_tag = tag;
			_pattern = pattern;
			_replacement = replacement;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				value = Regex.Replace(value, _pattern, _replacement);
				dataset.Add(_tag, value);
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' regex '{1}' -> '{2}'", name, _pattern, _replacement);
		}
		#endregion
	}

	/// <summary>
	/// Prefix the value of a DICOM element.
	/// </summary>
	public class PrefixDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private string _prefix;
		#endregion

		#region Public Constructor
		public PrefixDicomTransformRule(DicomTag tag, string prefix) {
			_tag = tag;
			_prefix = prefix;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				dataset.Add(_tag, _prefix + value);
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' prefix '{1}'", name, _prefix);
		}
		#endregion
	}

	/// <summary>
	/// Append the value of a DICOM element.
	/// </summary>
	public class AppendDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private string _append;
		#endregion

		#region Public Constructor
		public AppendDicomTransformRule(DicomTag tag, string append) {
			_tag = tag;
			_append = append;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				dataset.Add(_tag, value + _append);
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' append '{1}'", name, _append);
		}
		#endregion
	}

	public enum DicomTrimPosition {
		Start,
		End,
		Both
	}

	/// <summary>
	/// Trims a string from the beginning and end of a DICOM element value.
	/// </summary>
	public class TrimStringDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private string _trim;
		private DicomTrimPosition _position;
		#endregion

		#region Public Constructor
		public TrimStringDicomTransformRule(DicomTag tag, DicomTrimPosition position, string trim) {
			_tag = tag;
			_trim = trim;
			_position = position;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				if (_position == DicomTrimPosition.Start || _position == DicomTrimPosition.Both)
					while (value.StartsWith(_trim))
						value = value.Substring(_trim.Length);
				if (_position == DicomTrimPosition.End || _position == DicomTrimPosition.Both)
					while (value.EndsWith(_trim))
						value = value.Substring(0, value.Length - _trim.Length);
				dataset.Add(_tag, value);
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' trim '{1}' from {2}", name, _trim, _position.ToString().ToLower());
		}
		#endregion
	}

	/// <summary>
	/// Trims whitespace or a set of characters from the beginning and end of a DICOM element value.
	/// </summary>
	public class TrimCharactersDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private char[] _trim;
		private DicomTrimPosition _position;
		#endregion

		#region Public Constructor
		public TrimCharactersDicomTransformRule(DicomTag tag, DicomTrimPosition position) {
			_tag = tag;
			_trim = null;
			_position = position;
		}

		public TrimCharactersDicomTransformRule(DicomTag tag, DicomTrimPosition position, char[] trim) {
			_tag = tag;
			_trim = trim;
			_position = position;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				if (_position == DicomTrimPosition.Both) {
					if (_trim != null)
						value = value.Trim(_trim);
					else
						value = value.Trim();
				} else if (_position == DicomTrimPosition.Start) {
					if (_trim != null)
						value = value.TrimStart(_trim);
					else
						value = value.TrimStart();
				} else {
					if (_trim != null)
						value = value.TrimEnd(_trim);
					else
						value = value.TrimEnd();
				}
				dataset.Add(_tag, value);
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			if (_trim != null)
				return String.Format("'{0}' trim '{1}' from {2}", name, new string(_trim), _position.ToString().ToLower());
			else
				return String.Format("'{0}' trim whitespace from {2}", name, _position.ToString().ToLower());
		}
		#endregion
	}

	/// <summary>
	/// Pads a DICOM element value.
	/// </summary>
	public class PadStringDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private char _paddingChar;
		private int _totalLength;
		#endregion

		#region Public Constructor
		public PadStringDicomTransformRule(DicomTag tag, int totalLength, char paddingChar) {
			_tag = tag;
			_totalLength = totalLength;
			_paddingChar = paddingChar;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				if (_totalLength < 0)
					value = value.PadLeft(-_totalLength, _paddingChar);
				else
					value = value.PadRight(_totalLength, _paddingChar);
				dataset.Add(_tag, value);
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' pad to {1} with '{2}'", name, _totalLength, _paddingChar);
		}
		#endregion
	}

	/// <summary>
	/// Truncates a DICOM element value.
	/// </summary>
	public class TruncateDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private int _length;
		#endregion

		#region Public Constructor
		public TruncateDicomTransformRule(DicomTag tag, int length) {
			_tag = tag;
			_length = length;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				string[] parts = value.Split('\\');
				for (int i = 0; i < parts.Length; i++) {
					if (parts[i].Length > _length)
						parts[i] = parts[i].Substring(0, _length);
				}
				value = String.Join("\\", parts);
				dataset.Add(_tag, value);
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' truncate to {1} characters", name, _length);
		}
		#endregion
	}

	/// <summary>
	/// Splits a DICOM element value and then formats the a string from the resulting array.
	/// </summary>
	public class SplitFormatDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private char[] _seperators;
		private string _format;
		#endregion

		#region Public Constructor
		public SplitFormatDicomTransformRule(DicomTag tag, char[] seperators, string format) {
			_tag = tag;
			_seperators = seperators;
			_format = format;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				string[] parts = value.Split(_seperators);
				value = String.Format(_format, parts);
				dataset.Add(_tag, value);
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' split on '{1}' and format as '{2}'", name, new string(_seperators), _format);
		}
		#endregion
	}

	/// <summary>
	/// Changes the case of a DICOM element value to all upper case.
	/// </summary>
	public class ToUpperDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		#endregion

		#region Public Constructor
		public ToUpperDicomTransformRule(DicomTag tag) {
			_tag = tag;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				dataset.Add(_tag, value.ToUpper());
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' to upper case", name);
		}
		#endregion
	}

	/// <summary>
	/// Changes the case of a DICOM element value to all lower case.
	/// </summary>
	public class ToLowerDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		#endregion

		#region Public Constructor
		public ToLowerDicomTransformRule(DicomTag tag) {
			_tag = tag;
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			if (dataset.Contains(_tag)) {
				dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				dataset.Add(_tag, value.ToLower());
			}
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' to lower case", name);
		}
		#endregion
	}

	/// <summary>
	/// Generates a new UID for a DICOM element.
	/// </summary>
	public class GenerateUidDicomTransformRule : IDicomTransformRule {
		#region Private Members
		private DicomTag _tag;
		private DicomUIDGenerator _generator;
		#endregion

		#region Public Constructor
		public GenerateUidDicomTransformRule(DicomTag tag, DicomUIDGenerator generator = null) {
			_tag = tag;
			_generator = generator ?? new DicomUIDGenerator();
		}
		#endregion

		#region Public Methods
		public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null) {
			dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
			var uid = dataset.Get<DicomUID>(_tag);
			dataset.Add(_tag, _generator.Generate(uid));
		}

		public override string ToString() {
			string name = String.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
			return String.Format("'{0}' generate UID", name);
		}
		#endregion
	}
}
