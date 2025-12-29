// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FellowOakDicom.Serialization
{
    [JsonSerializable(typeof(DicomDataset))]
    [JsonSerializable(typeof(DicomDataset[]))]
    public partial class SourceGenerationContext : JsonSerializerContext
    {
        public static SourceGenerationContext Create(bool formatIntended, params JsonConverter[] converters) =>
            Create(formatIntended, (IEnumerable<JsonConverter>)converters);

        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL2026:RequiresUnreferencedCode", Justification = "The returned options will be populated with a JsonSerializerContext"), UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "The returned options will be populated with a JsonSerializerContext")]
        public static SourceGenerationContext Create(bool formatIndented, IEnumerable<JsonConverter> converters)
        {
            // Note - GeneratedSerializerOptions will not be initialized until AFTER the Default static singleton has been initialized,
            // so don't call this method from a static member of the context itself unless wrapped in Lazy<>.
            var options = new JsonSerializerOptions(Default.GeneratedSerializerOptions ?? JsonSerializerOptions.Default)
                .InsertConverters(0, converters);
            options.WriteIndented = formatIndented;
            options.ReadCommentHandling = JsonCommentHandling.Skip;
            return new(options);
        }

    }
}
