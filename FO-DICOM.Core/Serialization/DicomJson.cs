﻿using System.Text.Json;

namespace FellowOakDicom.Serialization
{
    public static class DicomJson
    {

        /// <summary>
        /// Converts a <see cref="DicomDataset"/> to a Json-String.
        /// </summary>
        /// <param name="writeTagsAsKeywords">Whether to write the json keys as DICOM keywords instead of tags. This makes the json non-compliant to DICOM JSON.</param>
        /// <param name="formatIndented">Gets or sets a value that defines whether JSON should use pretty printing. By default, JSON is serialized without any extra white space.</param>
        public static string ConvertDicomToJson(DicomDataset dataset, bool writeTagsAsKeywords = false, bool formatIndented = false)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DicomJsonConverter(writeTagsAsKeywords: writeTagsAsKeywords));
            options.WriteIndented = formatIndented;
            var conv = JsonSerializer.Serialize<DicomDataset>(dataset, options);
            return conv;
        }

        /// <summary>
        /// Converts a Json-String to a <see cref="DicomDataset"/>.
        /// </summary>
        /// <param name="autoValidate">Whether the content of DicomItems shall be validated as soon as they are added to the DicomDataset.</param>
        public static DicomDataset ConvertJsonToDicom(string json, bool autoValidate = true)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DicomJsonConverter(autoValidate: autoValidate));
            options.ReadCommentHandling = JsonCommentHandling.Skip;
            var ds = JsonSerializer.Deserialize<DicomDataset>(json, options);
            return ds;
        }

        public static DicomDataset[] ConvertJsonToDicomArray(string json)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DicomArrayJsonConverter());
            options.ReadCommentHandling = JsonCommentHandling.Skip;
            var ds = JsonSerializer.Deserialize<DicomDataset[]>(json, options);
            return ds;
        }

    }
}
