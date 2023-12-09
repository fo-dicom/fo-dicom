// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom.StructuredReport
{
    public enum DicomValueType
    {
        Container,

        Text,

        Code,

        Numeric,

        PersonName,

        Date,

        Time,

        DateTime,

        UIDReference,

        Composite,

        Image,

        Waveform,

        SpatialCoordinate,

        TemporalCoordinate
    }

    public enum DicomContinuity
    {
        None,

        Separate,

        Continuous
    }

    public enum DicomRelationship
    {
        Contains,

        HasProperties,

        InferredFrom,

        SelectedFrom,

        HasObservationContext,

        HasAcquisitionContext,

        HasConceptModifier
    }

    public class DicomContentItem
    {
        private DicomDataset _dataset;

        public DicomContentItem(DicomDataset dataset)
        {
            Dataset = dataset;
        }

        public DicomContentItem(DicomCodeItem code, DicomRelationship relationship, DicomValueType type, string value)
        {
            Dataset = new DicomDataset();
            Code = code;
            Relationship = relationship;
            Type = type;

            if (type == DicomValueType.Text)
            {
                Dataset.Add(DicomTag.TextValue, value);
            }
            else if (type == DicomValueType.PersonName)
            {
                Dataset.Add(DicomTag.PersonName, value);
            }
            else
            {
                throw new DicomStructuredReportException($"Type of string is not the correct value type for {type} content item.");
            }
        }

        public DicomContentItem(DicomCodeItem code, DicomRelationship relationship, DicomValueType type, DateTime value)
        {
            Dataset = new DicomDataset();
            Code = code;
            Relationship = relationship;
            Type = type;

            if (type == DicomValueType.Date)
            {
                Dataset.Add(DicomTag.Date, value);
            }
            else if (type == DicomValueType.Time)
            {
                Dataset.Add(DicomTag.Time, value);
            }
            else if (type == DicomValueType.DateTime)
            {
                Dataset.Add(DicomTag.DateTime, value);
            }
            else
            {
                throw new DicomStructuredReportException($"Type of DateTime is not the correct value type for {type} content item.");
            }
        }

        public DicomContentItem(DicomCodeItem code, DicomRelationship relationship, DicomUID value)
        {
            Dataset = new DicomDataset();
            Code = code;
            Relationship = relationship;
            Type = DicomValueType.UIDReference;

            Dataset.Add(DicomTag.UID, value);
        }

        public DicomContentItem(DicomCodeItem code, DicomRelationship relationship, DicomCodeItem value)
        {
            Dataset = new DicomDataset();
            Code = code;
            Relationship = relationship;
            Type = DicomValueType.Code;

            Dataset.Add(DicomTag.ConceptCodeSequence, value);
        }

        public DicomContentItem(DicomCodeItem code, DicomRelationship relationship, DicomMeasuredValue value)
        {
            Dataset = new DicomDataset();
            Code = code;
            Relationship = relationship;
            Type = DicomValueType.Numeric;

            Dataset.Add(DicomTag.MeasuredValueSequence, value);
        }

        public DicomContentItem(
            DicomCodeItem code,
            DicomRelationship relationship,
            DicomValueType type,
            DicomReferencedSOP value)
        {
            Dataset = new DicomDataset();
            Code = code;
            Relationship = relationship;
            Type = type;

            if (type == DicomValueType.Composite || type == DicomValueType.Image || type == DicomValueType.Waveform)
            {
                Dataset.Add(DicomTag.ReferencedSOPSequence, value);
            }
            else
            {
                throw new DicomStructuredReportException($"Type of DicomReferencedSOP is not the correct value type for {type} content item.");
            }
        }

        public DicomContentItem(
            DicomCodeItem code,
            DicomRelationship relationship,
            DicomContinuity continuity,
            params DicomContentItem[] items)
        {
            Dataset = new DicomDataset();
            Code = code;
            Relationship = relationship;
            Type = DicomValueType.Container;
            Continuity = continuity;

            Dataset.Add(DicomTag.ContentSequence, items);
        }

        public DicomDataset Dataset
        {
            get => _dataset;
            private set => _dataset = value;
        }

        public DicomCodeItem Code
        {
            get => Dataset.GetCodeItem(DicomTag.ConceptNameCodeSequence);
            private set => Dataset.AddOrUpdate(DicomTag.ConceptNameCodeSequence, value);
        }

        public DicomValueType Type
        {
            get
            {
                var type = Dataset.GetValueOrDefault(DicomTag.ValueType, 0, "UNKNOWN");
                return type switch
                {
                    "CONTAINER" => DicomValueType.Container,
                    "TEXT" => DicomValueType.Text,
                    "CODE" => DicomValueType.Code,
                    "NUM" => DicomValueType.Numeric,
                    "PNAME" => DicomValueType.PersonName,
                    "DATE" => DicomValueType.Date,
                    "TIME" => DicomValueType.Time,
                    "DATETIME" => DicomValueType.DateTime,
                    "UIDREF" => DicomValueType.UIDReference,
                    "COMPOSITE" => DicomValueType.Composite,
                    "IMAGE" => DicomValueType.Image,
                    "WAVEFORM" => DicomValueType.Waveform,
                    "SCOORD" => DicomValueType.SpatialCoordinate,
                    "TCOORD" => DicomValueType.TemporalCoordinate,
                    _ => throw new DicomStructuredReportException($"Unknown value type: {type}"),
                };
            }
            private set
            {
                switch (value)
                {
                    case DicomValueType.Container:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "CONTAINER");
                        return;
                    case DicomValueType.Text:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "TEXT");
                        return;
                    case DicomValueType.Code:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "CODE");
                        return;
                    case DicomValueType.Numeric:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "NUM");
                        return;
                    case DicomValueType.PersonName:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "PNAME");
                        return;
                    case DicomValueType.Date:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "DATE");
                        return;
                    case DicomValueType.Time:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "TIME");
                        return;
                    case DicomValueType.DateTime:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "DATETIME");
                        return;
                    case DicomValueType.UIDReference:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "UIDREF");
                        return;
                    case DicomValueType.Composite:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "COMPOSITE");
                        return;
                    case DicomValueType.Image:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "IMAGE");
                        return;
                    case DicomValueType.Waveform:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "WAVEFORM");
                        return;
                    case DicomValueType.SpatialCoordinate:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "SCOORD");
                        return;
                    case DicomValueType.TemporalCoordinate:
                        Dataset.AddOrUpdate(DicomTag.ValueType, "TCOORD");
                        return;
                    default:
                        break;
                }
            }
        }

        public DicomRelationship Relationship
        {
            get
            {
                var type = Dataset.GetValueOrDefault(DicomTag.RelationshipType, 0, "UNKNOWN");
                return type switch
                {
                    "CONTAINS" => DicomRelationship.Contains,
                    "HAS PROPERTIES" => DicomRelationship.HasProperties,
                    "INFERRED FROM" => DicomRelationship.InferredFrom,
                    "SELECTED FROM" => DicomRelationship.SelectedFrom,
                    "HAS OBS CONTEXT" => DicomRelationship.HasObservationContext,
                    "HAS ACQ CONTEXT" => DicomRelationship.HasAcquisitionContext,
                    "HAS CONCEPT MOD" => DicomRelationship.HasConceptModifier,
                    _ => throw new DicomStructuredReportException($"Unknown relationship type: {type}"),
                };
            }
            private set
            {
                switch (value)
                {
                    case DicomRelationship.Contains:
                        Dataset.AddOrUpdate(DicomTag.RelationshipType, "CONTAINS");
                        return;
                    case DicomRelationship.HasProperties:
                        Dataset.AddOrUpdate(DicomTag.RelationshipType, "HAS PROPERTIES");
                        return;
                    case DicomRelationship.InferredFrom:
                        Dataset.AddOrUpdate(DicomTag.RelationshipType, "INFERRED FROM");
                        return;
                    case DicomRelationship.SelectedFrom:
                        Dataset.AddOrUpdate(DicomTag.RelationshipType, "SELECTED FROM");
                        return;
                    case DicomRelationship.HasObservationContext:
                        Dataset.AddOrUpdate(DicomTag.RelationshipType, "HAS OBS CONTEXT");
                        return;
                    case DicomRelationship.HasAcquisitionContext:
                        Dataset.AddOrUpdate(DicomTag.RelationshipType, "HAS ACQ CONTEXT");
                        return;
                    case DicomRelationship.HasConceptModifier:
                        Dataset.AddOrUpdate(DicomTag.RelationshipType, "HAS CONCEPT MOD");
                        return;
                    default:
                        break;
                }
            }
        }

        public DicomContinuity Continuity
        {
            get => Dataset.GetValueOrDefault(DicomTag.ContinuityOfContent, 0, DicomContinuity.None);
            private set => Dataset.AddOrUpdate(DicomTag.ContinuityOfContent, value.ToString().ToUpperInvariant());
        }

        public IEnumerable<DicomContentItem> Children()
        {
            if (Dataset.TryGetSequence(DicomTag.ContentSequence, out var sequence))
            {
                foreach (var item in sequence)
                {
                    yield return new DicomContentItem(item);
                }
            }
        }

        public DicomContentItem Add(DicomContentItem item)
        {
            if (! Dataset.TryGetSequence(DicomTag.ContentSequence, out var sequence))
            {
                sequence = new DicomSequence(DicomTag.ContentSequence);
                Dataset.Add(sequence);
            }

            sequence.Items.Add(item.Dataset);

            return item;
        }

        public DicomContentItem Add(
            DicomCodeItem code,
            DicomRelationship relationship,
            DicomValueType type,
            string value)
        {
            return Add(new DicomContentItem(code, relationship, type, value));
        }

        public DicomContentItem Add(
            DicomCodeItem code,
            DicomRelationship relationship,
            DicomValueType type,
            DateTime value)
        {
            return Add(new DicomContentItem(code, relationship, type, value));
        }

        public DicomContentItem Add(DicomCodeItem code, DicomRelationship relationship, DicomUID value)
        {
            return Add(new DicomContentItem(code, relationship, value));
        }

        public DicomContentItem Add(DicomCodeItem code, DicomRelationship relationship, DicomCodeItem value)
        {
            return Add(new DicomContentItem(code, relationship, value));
        }

        public DicomContentItem Add(DicomCodeItem code, DicomRelationship relationship, DicomMeasuredValue value)
        {
            return Add(new DicomContentItem(code, relationship, value));
        }

        public DicomContentItem Add(
            DicomCodeItem code,
            DicomRelationship relationship,
            DicomValueType type,
            DicomReferencedSOP value)
        {
            return Add(new DicomContentItem(code, relationship, type, value));
        }

        public DicomContentItem Add(
            DicomCodeItem code,
            DicomRelationship relationship,
            DicomContinuity continuity,
            params DicomContentItem[] items)
        {
            return Add(new DicomContentItem(code, relationship, continuity, items));
        }

        public T Get<T>()
        {
            if (typeof(T) == typeof(string))
            {
                if (Type == DicomValueType.Text) return (T)(object)Dataset.GetValueOrDefault(DicomTag.TextValue, 0, string.Empty);
                if (Type == DicomValueType.PersonName) return (T)(object)Dataset.GetValueOrDefault(DicomTag.PersonName, 0, string.Empty);
                if (Type == DicomValueType.Numeric)
                {
                    var mv = Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
                    if (mv == null) return default(T);
                    return (T)(object)mv.ToString();
                }
                if (Type == DicomValueType.Date) return (T)(object)Dataset.GetValueOrDefault(DicomTag.Date, 0, string.Empty);
                if (Type == DicomValueType.Time) return (T)(object)Dataset.GetValueOrDefault(DicomTag.Time, 0, string.Empty);
                if (Type == DicomValueType.DateTime) return (T)(object)Dataset.GetValueOrDefault(DicomTag.DateTime, 0, string.Empty);
                if (Type == DicomValueType.UIDReference) return (T)(object)Dataset.GetSingleValue<string>(DicomTag.UID);
                if (Type == DicomValueType.Code)
                {
                    var c = Dataset.GetCodeItem(DicomTag.ConceptCodeSequence);
                    if (c == null) return default(T);
                    return (T)(object)c.ToString();
                }
            }

            if (typeof(T) == typeof(DateTime))
            {
                if (Type == DicomValueType.Date) return (T)(object)Dataset.GetValueOrDefault(DicomTag.Date, 0, DateTime.Today);
                if (Type == DicomValueType.Time) return (T)(object)Dataset.GetValueOrDefault(DicomTag.Time, 0, DateTime.Today);
                if (Type == DicomValueType.DateTime) return (T)(object)Dataset.GetValueOrDefault(DicomTag.DateTime, 0, DateTime.Today);
            }

            if (typeof(T) == typeof(decimal))
            {
                if (Type == DicomValueType.Numeric)
                {
                    var mv = Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
                    if (mv == null) return default(T);
                    return (T)(object)mv.Value;
                }
            }

            if (typeof(T) == typeof(double))
            {
                if (Type == DicomValueType.Numeric)
                {
                    var mv = Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
                    if (mv == null) return default(T);
                    return (T)(object)(double)mv.Value;
                }
            }

            if (typeof(T) == typeof(int))
            {
                if (Type == DicomValueType.Numeric)
                {
                    var mv = Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
                    if (mv == null) return default(T);
                    return (T)(object)(int)mv.Value;
                }
            }

            if (typeof(T) == typeof(DicomUID))
            {
                if (Type == DicomValueType.UIDReference) return (T)(object)Dataset.GetSingleValue<DicomUID>(DicomTag.UID);
            }

            if (typeof(T) == typeof(DicomCodeItem))
            {
                if (Type == DicomValueType.Code) return (T)(object)Dataset.GetCodeItem(DicomTag.ConceptCodeSequence);
            }

            if (typeof(T) == typeof(DicomMeasuredValue))
            {
                if (Type == DicomValueType.Numeric) return (T)(object)Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
            }

            if (typeof(T) == typeof(DicomReferencedSOP))
            {
                if (Type == DicomValueType.Composite || Type == DicomValueType.Image || Type == DicomValueType.Waveform) return (T)(object)Dataset.GetReferencedSOP(DicomTag.ReferencedSOPSequence);
            }

            throw new DicomStructuredReportException($"Unable to get type of {typeof(T)} from {Type} content item.");
        }

        public T Get<T>(DicomCodeItem code, T defaultValue)
        {
            var item = Children().FirstOrDefault(x => x.Code == code);
            if (item == null) return default;

            if (typeof(T) == typeof(DicomContentItem)) return (T)(object)item;

            return item.Get<T>();
        }

        public override string ToString()
        {
            var s = Dataset.GetValueOrDefault(DicomTag.RelationshipType, 0, string.Empty);
            if (!string.IsNullOrEmpty(s)) s += " ";
            else s = string.Empty;
            if (Code != null) s += string.Format("{0} {1}", Code.ToString(), Dataset.GetSingleValueOrDefault(DicomTag.ValueType, "UNKNOWN"));
            else
                s += string.Format(
                    "{0} {1}",
                    "(no code provided)",
                    Dataset.GetValueOrDefault(DicomTag.ValueType, 0, "UNKNOWN"));
            try
            {
                s += string.Format(" [{0}]", Get<string>());
            }
            catch
            {
                // silently catch the exception
            }
            return s;
        }
    }
}
