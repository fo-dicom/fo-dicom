// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#nullable disable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json;

namespace FellowOakDicom.Serialization
{
    public static class DicomJson
    {
        private static ConcurrentDictionary<JsonSerializerOptionsKey, JsonSerializerOptions> _cachedOptions =
            new ConcurrentDictionary<JsonSerializerOptionsKey, JsonSerializerOptions>();

        private static JsonSerializerOptions GetOrCreateJsonSerializerOptions(
            bool autoValidate = false,
            bool writeTagsAsKeywords = false,
            bool formatIndented = false,
            NumberSerializationMode numberSerializationMode = NumberSerializationMode.AsNumber)
        {
            var key = new JsonSerializerOptionsKey(writeTagsAsKeywords, formatIndented, autoValidate,
                numberSerializationMode);

            if (_cachedOptions.TryGetValue(key, out var jsonSerializerOptions))
            {
                return jsonSerializerOptions;
            }

            var dicomJsonConverter = new DicomJsonConverter(
                autoValidate: autoValidate,
                writeTagsAsKeywords: key.WriteTagsAsKeywords,
                numberSerializationMode: key.NumberSerializationMode
            );
            var options = new JsonSerializerOptions
            {
                WriteIndented = key.FormatIndented, ReadCommentHandling = JsonCommentHandling.Skip
            };
            options.Converters.Add(dicomJsonConverter);

            _cachedOptions.TryAdd(key, options);

            return options;
        }

        /// <summary>
        /// Converts a <see cref="DicomDataset"/> to a Json-String.
        /// </summary>
        /// <param name="writeTagsAsKeywords">Whether to write the json keys as DICOM keywords instead of tags. This makes the json non-compliant to DICOM JSON.</param>
        /// <param name="formatIndented">Gets or sets a value that defines whether JSON should use pretty printing. By default, JSON is serialized without any extra white space.</param>
        /// <param name="numberSerializationMode">Defines how numbers should be serialized. Defaults to 'AsNumber', which will throw FormatException when a number is not parsable.</param>
        public static string ConvertDicomToJson(DicomDataset dataset, bool writeTagsAsKeywords = false,
            bool formatIndented = false,
            NumberSerializationMode numberSerializationMode = NumberSerializationMode.AsNumber)
        {
            var options = GetOrCreateJsonSerializerOptions(
                writeTagsAsKeywords: writeTagsAsKeywords,
                formatIndented: formatIndented,
                numberSerializationMode: numberSerializationMode);
            var conv = JsonSerializer.Serialize(dataset, options);
            return conv;
        }


        /// <summary>
        /// Converts an array or list of <see cref="DicomDataset"/> to a Json-String.
        /// </summary>
        /// <param name="writeTagsAsKeywords">Whether to write the json keys as DICOM keywords instead of tags. This makes the json non-compliant to DICOM JSON.</param>
        /// <param name="formatIndented">Gets or sets a value that defines whether JSON should use pretty printing. By default, JSON is serialized without any extra white space.</param>
        public static string ConvertDicomToJson(IEnumerable<DicomDataset> dataset, bool writeTagsAsKeywords = false,
            bool formatIndented = false)
        {
            var options = GetOrCreateJsonSerializerOptions(
                writeTagsAsKeywords: writeTagsAsKeywords,
                formatIndented: formatIndented);
            var conv = JsonSerializer.Serialize(dataset, options);
            return conv;
        }


        /// <summary>
        /// Converts a Json-String to a <see cref="DicomDataset"/>.
        /// </summary>
        /// <param name="autoValidate">Whether the content of DicomItems shall be validated as soon as they are added to the DicomDataset.</param>
        public static DicomDataset ConvertJsonToDicom(string json, bool autoValidate = true)
        {
            var options = GetOrCreateJsonSerializerOptions(autoValidate: autoValidate);
            var ds = JsonSerializer.Deserialize<DicomDataset>(json, options);
            return ds;
        }

        public static DicomDataset[] ConvertJsonToDicomArray(string json)
        {
            var options = GetOrCreateJsonSerializerOptions();
            var ds = JsonSerializer.Deserialize<DicomDataset[]>(json, options);
            return ds;
        }

        private class JsonSerializerOptionsKey
        {
            /// <summary>
            /// Whether to write the json keys as DICOM keywords instead of tags. This makes the json non-compliant to DICOM JSON.
            /// </summary>
            public bool WriteTagsAsKeywords { get; }

            /// <summary>
            /// Gets or sets a value that defines whether JSON should use pretty printing. By default, JSON is serialized without any extra white space.
            /// </summary>
            public bool FormatIndented { get; }

            /// <summary>
            /// Whether the content of DicomItems shall be validated as soon as they are added to the DicomDataset.
            /// </summary>
            public bool AutoValidate { get; }

            /// <summary>
            /// Defines how numbers should be serialized. Defaults to 'AsNumber', which will throw FormatException when a number is not parsable.
            /// </summary>
            public NumberSerializationMode NumberSerializationMode { get; }

            public JsonSerializerOptionsKey(bool writeTagsAsKeywords, bool formatIndented, bool autoValidate,
                NumberSerializationMode numberSerializationMode)
            {
                WriteTagsAsKeywords = writeTagsAsKeywords;
                FormatIndented = formatIndented;
                AutoValidate = autoValidate;
                NumberSerializationMode = numberSerializationMode;
            }

            protected bool Equals(JsonSerializerOptionsKey other) => WriteTagsAsKeywords == other.WriteTagsAsKeywords
                                                                     && FormatIndented == other.FormatIndented
                                                                     && AutoValidate == other.AutoValidate
                                                                     && NumberSerializationMode ==
                                                                     other.NumberSerializationMode;

            public override bool Equals(object obj)
            {
                if (obj is null)
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (obj.GetType() != GetType())
                {
                    return false;
                }

                return Equals((JsonSerializerOptionsKey)obj);
            }

            public override int GetHashCode() => HashCode.Combine(WriteTagsAsKeywords, FormatIndented, AutoValidate,
                (int)NumberSerializationMode);

            public static bool operator ==(JsonSerializerOptionsKey left, JsonSerializerOptionsKey right) =>
                Equals(left, right);

            public static bool operator !=(JsonSerializerOptionsKey left, JsonSerializerOptionsKey right) =>
                !Equals(left, right);
        }
    }
}