// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FellowOakDicom
{

    public interface IDicomTransformRule
    {
        void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null);
    }

    public class DicomTransformRuleSet : IDicomTransformRule
    {
        #region Private Members

        private readonly List<IDicomTransformRule> _transformRules;

        private DicomMatchRuleSet _conditions;

        #endregion

        #region Public Constructor

        public DicomTransformRuleSet()
        {
            _transformRules = new List<IDicomTransformRule>();
        }

        public DicomTransformRuleSet(params IDicomTransformRule[] rules)
        {
            _transformRules = new List<IDicomTransformRule>(rules);
        }

        public DicomTransformRuleSet(DicomMatchRuleSet conditions, params IDicomTransformRule[] rules)
        {
            _conditions = conditions;
            _transformRules = new List<IDicomTransformRule>(rules);
        }

        #endregion

        #region Public Properties

        public DicomMatchRuleSet Conditions
        {
            get
            {
                if (_conditions == null) _conditions = new DicomMatchRuleSet();
                return _conditions;
            }
            set => _conditions = value;
        }

        #endregion

        #region Public Methods

        public void Add(IDicomTransformRule rule)
        {
            _transformRules.Add(rule);
        }

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (_conditions != null && !_conditions.Match(dataset)) return;

            foreach (IDicomTransformRule rule in _transformRules) rule.Transform(dataset);
        }

        #endregion
    }

    /// <summary>
    /// Remove an element from a DICOM dataset.
    /// </summary>
    public class RemoveElementDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomMaskedTag _mask;

        #endregion

        #region Public Constructor

        public RemoveElementDicomTransformRule(DicomMaskedTag mask)
        {
            _mask = mask;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            var remove = dataset.EnumerateMasked(_mask).Select(x => x.Tag).ToList();
            foreach (DicomTag tag in remove)
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, tag);
                dataset.Remove(tag);
            }
        }

        public override string ToString() => $"'[{_mask}]' remove";

        #endregion
    }

    /// <summary>
    /// Sets the value of a DICOM element.
    /// </summary>
    public class SetValueDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        private readonly string _value;

        #endregion

        #region Public Constructor

        public SetValueDicomTransformRule(DicomTag tag, string value)
        {
            _tag = tag;
            _value = value;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
            dataset.AddOrUpdate(_tag, _value);
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' set '{1}'", name, _value);
        }

        #endregion
    }

    /// <summary>
    /// Maps the value of a DICOM element to a value.
    /// </summary>
    public class MapValueDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly EqualsDicomMatchRule _match;

        private readonly string _value;

        #endregion

        #region Public Constructor

        public MapValueDicomTransformRule(DicomTag tag, string match, string value)
        {
            _match = new EqualsDicomMatchRule(tag, match);
            _value = value;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (_match.Match(dataset))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _match.Tag);
                dataset.AddOrUpdate(_match.Tag, _value);
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _match.Tag, _match.Tag.DictionaryEntry.Name);
            return string.Format("'{0}' map '{1}' -> '{2}'", name, _match.Value, _value);
        }

        #endregion
    }

    /// <summary>
    /// Copies the value of a DICOM element to another DICOM element.
    /// </summary>
    public class CopyValueDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _src;

        private readonly DicomTag _dst;

        #endregion

        #region Public Constructor

        public CopyValueDicomTransformRule(DicomTag src, DicomTag dst)
        {
            _src = src;
            _dst = dst;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_src))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _dst);
                dataset.AddOrUpdate(_dst, dataset.GetDicomItem<DicomElement>(_src).Buffer);
            }
        }

        public override string ToString()
        {
            string sname = string.Format("{0} {1}", _src, _src.DictionaryEntry.Name);
            string dname = string.Format("{0} {1}", _dst, _dst.DictionaryEntry.Name);
            return string.Format("'{0}' copy to '{1}'", sname, dname);
        }

        #endregion
    }

    /// <summary>
    /// Performs a regular expression replace operation on a DICOM element value.
    /// </summary>
    public class RegexDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        private readonly string _pattern;

        private readonly string _replacement;

        #endregion

        #region Public Constructor

        public RegexDicomTransformRule(DicomTag tag, string pattern, string replacement)
        {
            _tag = tag;
            _pattern = pattern;
            _replacement = replacement;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_tag))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
                var value = dataset.GetString(_tag);
                value = Regex.Replace(value, _pattern, _replacement);
                dataset.AddOrUpdate(_tag, value);
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' regex '{1}' -> '{2}'", name, _pattern, _replacement);
        }

        #endregion
    }

    /// <summary>
    /// Prefix the value of a DICOM element.
    /// </summary>
    public class PrefixDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        private readonly string _prefix;

        #endregion

        #region Public Constructor

        public PrefixDicomTransformRule(DicomTag tag, string prefix)
        {
            _tag = tag;
            _prefix = prefix;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_tag))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
                var value = dataset.GetString(_tag);
                dataset.AddOrUpdate(_tag, _prefix + value);
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' prefix '{1}'", name, _prefix);
        }

        #endregion
    }

    /// <summary>
    /// Append the value of a DICOM element.
    /// </summary>
    public class AppendDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        private readonly string _append;

        #endregion

        #region Public Constructor

        public AppendDicomTransformRule(DicomTag tag, string append)
        {
            _tag = tag;
            _append = append;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_tag))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
                var value = dataset.GetString(_tag);
                dataset.AddOrUpdate(_tag, value + _append);
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' append '{1}'", name, _append);
        }

        #endregion
    }

    public enum DicomTrimPosition
    {
        Start,

        End,

        Both
    }

    /// <summary>
    /// Trims a string from the beginning and end of a DICOM element value.
    /// </summary>
    public class TrimStringDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        private readonly string _trim;

        private readonly DicomTrimPosition _position;

        #endregion

        #region Public Constructor

        public TrimStringDicomTransformRule(DicomTag tag, DicomTrimPosition position, string trim)
        {
            _tag = tag;
            _trim = trim;
            _position = position;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_tag))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
                var value = dataset.GetString(_tag);
                if (_position == DicomTrimPosition.Start || _position == DicomTrimPosition.Both) while (value.StartsWith(_trim)) value = value.Substring(_trim.Length);
                if (_position == DicomTrimPosition.End || _position == DicomTrimPosition.Both) while (value.EndsWith(_trim)) value = value.Substring(0, value.Length - _trim.Length);
                dataset.AddOrUpdate(_tag, value);
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' trim '{1}' from {2}", name, _trim, _position.ToString().ToLower());
        }

        #endregion
    }

    /// <summary>
    /// Trims whitespace or a set of characters from the beginning and end of a DICOM element value.
    /// </summary>
    public class TrimCharactersDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        private readonly char[] _trim;

        private readonly DicomTrimPosition _position;

        #endregion

        #region Public Constructor

        public TrimCharactersDicomTransformRule(DicomTag tag, DicomTrimPosition position)
        {
            _tag = tag;
            _trim = null;
            _position = position;
        }

        public TrimCharactersDicomTransformRule(DicomTag tag, DicomTrimPosition position, char[] trim)
        {
            _tag = tag;
            _trim = trim;
            _position = position;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_tag))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
                var value = dataset.GetString(_tag);
                if (_position == DicomTrimPosition.Both)
                {
                    if (_trim != null) value = value.Trim(_trim);
                    else value = value.Trim();
                }
                else if (_position == DicomTrimPosition.Start)
                {
                    if (_trim != null) value = value.TrimStart(_trim);
                    else value = value.TrimStart();
                }
                else
                {
                    if (_trim != null) value = value.TrimEnd(_trim);
                    else value = value.TrimEnd();
                }
                dataset.AddOrUpdate(_tag, value);
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            if (_trim != null)
                return string.Format(
                    "'{0}' trim '{1}' from {2}",
                    name,
                    new string(_trim),
                    _position.ToString().ToLower());
            else return string.Format("'{0}' trim whitespace from {1}", name, _position.ToString().ToLower());
        }

        #endregion
    }

    /// <summary>
    /// Pads a DICOM element value.
    /// </summary>
    public class PadStringDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        private readonly char _paddingChar;

        private readonly int _totalLength;

        #endregion

        #region Public Constructor

        public PadStringDicomTransformRule(DicomTag tag, int totalLength, char paddingChar)
        {
            _tag = tag;
            _totalLength = totalLength;
            _paddingChar = paddingChar;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_tag))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
                var value = dataset.GetString(_tag);
                if (_totalLength < 0) value = value.PadLeft(-_totalLength, _paddingChar);
                else value = value.PadRight(_totalLength, _paddingChar);
                dataset.AddOrUpdate(_tag, value);
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' pad to {1} with '{2}'", name, _totalLength, _paddingChar);
        }

        #endregion
    }

    /// <summary>
    /// Truncates a DICOM element value.
    /// </summary>
    public class TruncateDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        private readonly int _length;

        #endregion

        #region Public Constructor

        public TruncateDicomTransformRule(DicomTag tag, int length)
        {
            _tag = tag;
            _length = length;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_tag))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
                var value = dataset.GetString(_tag);
                string[] parts = value.Split('\\');
                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i].Length > _length) parts[i] = parts[i].Substring(0, _length);
                }
                value = string.Join("\\", parts);
                dataset.AddOrUpdate(_tag, value);
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' truncate to {1} characters", name, _length);
        }

        #endregion
    }

    /// <summary>
    /// Splits a DICOM element value and then formats the a string from the resulting array.
    /// </summary>
    public class SplitFormatDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        private readonly char[] _seperators;

        private readonly string _format;

        #endregion

        #region Public Constructor

        public SplitFormatDicomTransformRule(DicomTag tag, char[] seperators, string format)
        {
            _tag = tag;
            _seperators = seperators;
            _format = format;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_tag))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
                var value = dataset.GetString(_tag);
                string[] parts = value.Split(_seperators);
                value = string.Format(_format, parts);
                dataset.AddOrUpdate(_tag, value);
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' split on '{1}' and format as '{2}'", name, new string(_seperators), _format);
        }

        #endregion
    }

    /// <summary>
    /// Changes the case of a DICOM element value to all upper case.
    /// </summary>
    public class ToUpperDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        #endregion

        #region Public Constructor

        public ToUpperDicomTransformRule(DicomTag tag)
        {
            _tag = tag;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_tag))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
                var value = dataset.GetString(_tag);
                dataset.AddOrUpdate(_tag, value.ToUpper());
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' to upper case", name);
        }

        #endregion
    }

    /// <summary>
    /// Changes the case of a DICOM element value to all lower case.
    /// </summary>
    public class ToLowerDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        #endregion

        #region Public Constructor

        public ToLowerDicomTransformRule(DicomTag tag)
        {
            _tag = tag;
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            if (dataset.Contains(_tag))
            {
                dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
                var value = dataset.GetString(_tag);
                dataset.AddOrUpdate(_tag, value.ToLower());
            }
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' to lower case", name);
        }

        #endregion
    }

    /// <summary>
    /// Generates a new UID for a DICOM element.
    /// </summary>
    public class GenerateUidDicomTransformRule : IDicomTransformRule
    {
        #region Private Members

        private readonly DicomTag _tag;

        private readonly DicomUIDGenerator _generator;

        #endregion

        #region Public Constructor

        public GenerateUidDicomTransformRule(DicomTag tag, DicomUIDGenerator generator = null)
        {
            _tag = tag;
            _generator = generator ?? new DicomUIDGenerator();
        }

        #endregion

        #region Public Methods

        public void Transform(DicomDataset dataset, DicomDataset modifiedAttributesSequenceItem = null)
        {
            dataset.CopyTo(modifiedAttributesSequenceItem, _tag);
            var uid = dataset.GetSingleValue<DicomUID>(_tag);
            dataset.AddOrUpdate(_tag, _generator.Generate(uid));
        }

        public override string ToString()
        {
            string name = string.Format("{0} {1}", _tag, _tag.DictionaryEntry.Name);
            return string.Format("'{0}' generate UID", name);
        }

        #endregion
    }
}
