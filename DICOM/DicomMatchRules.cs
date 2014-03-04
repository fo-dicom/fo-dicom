using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Dicom {
	public interface IDicomMatchRule {
		bool Match(DicomDataset dataset);
	}

	public enum DicomMatchOperator : byte {
		/// <summary>All rules match</summary>
		And,

		/// <summary>Any rule matches</summary>
		Or
	}

	public class DicomMatchRuleSet : IDicomMatchRule {
		#region Private Members
		private DicomMatchOperator _operator;
		private IList<IDicomMatchRule> _rules;
		#endregion

		#region Public Constructor
		public DicomMatchRuleSet() {
			_rules = new List<IDicomMatchRule>();
			_operator = DicomMatchOperator.And;
		}

		public DicomMatchRuleSet(DicomMatchOperator op) {
			_rules = new List<IDicomMatchRule>();
			_operator = op;
		}

		public DicomMatchRuleSet(params IDicomMatchRule[] rules) {
			_rules = new List<IDicomMatchRule>(rules);
			_operator = DicomMatchOperator.And;
		}

		public DicomMatchRuleSet(DicomMatchOperator op, params IDicomMatchRule[] rules) {
			_rules = new List<IDicomMatchRule>(rules);
			_operator = op;
		}
		#endregion

		#region Public Properties
		public DicomMatchOperator Operator {
			get { return _operator; }
			set { _operator = value; }
		}
		#endregion

		#region Public Methods
		public void Add(IDicomMatchRule rule) {
			_rules.Add(rule);
		}

		public bool Match(DicomDataset dataset) {
			if (_rules.Count == 0)
				return true;

			if (_operator == DicomMatchOperator.Or) {
				foreach (var rule in _rules)
					if (rule.Match(dataset))
						return true;

				return false;
			} else {
				foreach (var rule in _rules)
					if (!rule.Match(dataset))
						return false;

				return true;
			}
		}

		public override string ToString() {
			var sb = new StringBuilder();
			foreach (IDicomMatchRule rule in _rules) {
				if (sb.Length > 0)
					sb.Append("  ").Append(Operator.ToString().ToUpper()).Append(" ");
				if (rule is DicomMatchRuleSet)
					sb.Append("(").AppendLine(rule.ToString()).AppendLine(")");
				else
					sb.Append(rule.ToString());
			}
			return sb.ToString();
		}
		#endregion
	}

	/// <summary>
	/// Negates the return value of a match rule.
	/// </summary>
	public class NegateDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private IDicomMatchRule _rule;
		#endregion

		#region Public Constructor
		public NegateDicomMatchRule(IDicomMatchRule rule) {
			_rule = rule;
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			return !_rule.Match(dataset);
		}

		public override string ToString() {
			return String.Format("not {0}", _rule);
		}
		#endregion
	}

	/// <summary>
	/// Checks that a DICOM element exists.
	/// </summary>
	public class ExistsDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private DicomTag _tag;
		#endregion

		#region Public Constructor
		public ExistsDicomMatchRule(DicomTag tag) {
			_tag = tag;
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			return dataset.Contains(_tag);
		}

		public override string ToString() {
			return String.Format("{0} exists", _tag);
		}
		#endregion
	}

	/// <summary>
	/// Checks if a DICOM element exists and has a value.
	/// </summary>
	public class IsEmptyDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private DicomTag _tag;
		#endregion

		#region Public Constructor
		public IsEmptyDicomMatchRule(DicomTag tag) {
			_tag = tag;
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			if (dataset.Contains(_tag)) {
				var value = dataset.Get<string>(_tag, -1, String.Empty);
				return String.IsNullOrEmpty(value);
			}
			return true;
		}

		public override string ToString() {
			return String.Format("{0} is empty", _tag);
		}
		#endregion
	}

	/// <summary>
	/// Compares a DICOM element value against a string.
	/// </summary>
	public class EqualsDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private DicomTag _tag;
		private string _value;
		#endregion

		#region Public Constructor
		public EqualsDicomMatchRule(DicomTag tag, string value) {
			_tag = tag;
			_value = value;
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			var value = dataset.Get<string>(_tag, -1, String.Empty);
			return _value == value;
		}

		public override string ToString() {
			return String.Format("{0} equals '{1}'", _tag, _value);
		}
		#endregion
	}

	/// <summary>
	/// Checks if a DICOM element value starts with a string.
	/// </summary>
	public class StartsWithDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private DicomTag _tag;
		private string _value;
		#endregion

		#region Public Constructor
		public StartsWithDicomMatchRule(DicomTag tag, string value) {
			_tag = tag;
			_value = value;
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			var value = dataset.Get<string>(_tag, -1, String.Empty);
			return value.StartsWith(_value);
		}

		public override string ToString() {
			return String.Format("{0} starts with '{1}'", _tag, _value);
		}
		#endregion
	}

	/// <summary>
	/// Checks if a DICOM element value ends with a string.
	/// </summary>
	public class EndsWithDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private DicomTag _tag;
		private string _value;
		#endregion

		#region Public Constructor
		public EndsWithDicomMatchRule(DicomTag tag, string value) {
			_tag = tag;
			_value = value;
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			var value = dataset.Get<string>(_tag, -1, String.Empty);
			return value.EndsWith(_value);
		}

		public override string ToString() {
			return String.Format("{0} ends with '{1}'", _tag, _value);
		}
		#endregion
	}

	/// <summary>
	/// Checks if a DICOM element value contains a string.
	/// </summary>
	public class ContainsDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private DicomTag _tag;
		private string _value;
		#endregion

		#region Public Constructor
		public ContainsDicomMatchRule(DicomTag tag, string value) {
			_tag = tag;
			_value = value;
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			var value = dataset.Get<string>(_tag, -1, String.Empty);
			return value.Contains(_value);
		}

		public override string ToString() {
			return String.Format("{0} contains '{1}'", _tag, _value);
		}
		#endregion
	}

	/// <summary>
	/// Matches a wildcard pattern against a DICOM element value.
	/// </summary>
	public class WildcardDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private DicomTag _tag;
		private string _pattern;
		#endregion

		#region Public Constructor
		public WildcardDicomMatchRule(DicomTag tag, string pattern) {
			_tag = tag;
			_pattern = pattern;
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			var value = dataset.Get<string>(_tag, -1, String.Empty);
			return value.Wildcard(_pattern);
		}

		public override string ToString() {
			return String.Format("{0} wildcard match '{1}'", _tag, _pattern);
		}
		#endregion
	}

	/// <summary>
	/// Matches regular expression pattern against a DICOM element value.
	/// </summary>
	public class RegexDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private DicomTag _tag;
		private string _pattern;
		private Regex _regex;
		#endregion

		#region Public Constructor
		public RegexDicomMatchRule(DicomTag tag, string pattern) {
			_tag = tag;
			_pattern = pattern;
			_regex = new Regex(_pattern, RegexOptions.Compiled);
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			var value = dataset.Get<string>(_tag, -1, String.Empty);
			return _regex.IsMatch(value);
		}

		public override string ToString() {
			return String.Format("{0} regex match '{1}'", _tag, _pattern);
		}
		#endregion
	}

	/// <summary>
	/// Matches a DICOM element value against a set of strings.
	/// </summary>
	public class OneOfDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private DicomTag _tag;
		private string[] _values;
		#endregion

		#region Public Constructor
		public OneOfDicomMatchRule(DicomTag tag, params string[] values) {
			_tag = tag;
			_values = values;
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			var value = dataset.Get<string>(_tag, -1, String.Empty);
			foreach (string v in _values)
				if (v == value)
					return true;
			return false;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} is one of ['", _tag);
			for (int i = 0; i < _values.Length; i++) {
				if (i > 0)
					sb.Append("', '");
				sb.Append(_values[i]);
			}
			sb.Append("']");
			return sb.ToString();
		}
		#endregion
	}

	/// <summary>
	/// Rule that always returns true or false.
	/// </summary>
	public class BoolDicomMatchRule : IDicomMatchRule {
		#region Private Members
		private bool _value;
		#endregion

		#region Public Constructor
		public BoolDicomMatchRule(bool value) {
			_value = value;
		}
		#endregion

		#region Public Methods
		public bool Match(DicomDataset dataset) {
			return _value;
		}

		public override string ToString() {
			return _value.ToString();
		}
		#endregion
	}
}
