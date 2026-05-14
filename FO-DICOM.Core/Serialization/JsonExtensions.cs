// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace FellowOakDicom.Serialization
{
    public static class JsonExtensions
    {
        public static JsonTypeInfo<T> GetTypeInfo<T>(this JsonSerializerOptions options, T value) =>
            (JsonTypeInfo<T>)options.GetTypeInfo(typeof(T));

        public static JsonTypeInfo<T> GetTypeInfo<T>(this JsonSerializerOptions options, JsonTypeInfo<T> otherTypeInfo) =>
            otherTypeInfo?.Options == options ? otherTypeInfo : options.GetTypeInfo<T>();

        public static JsonTypeInfo<T> GetTypeInfo<T>(this JsonSerializerOptions options) =>
            (JsonTypeInfo<T>)options.GetTypeInfo(typeof(T));

        public static JsonSerializerOptions InsertConverters(this JsonSerializerOptions options, int index, IEnumerable<JsonConverter> converters)
        {
            foreach (var converter in converters) // TODO: make more efficient by using AddRange() when converters is actually a list
                options.Converters.Insert(index++, converter);
            return options;
        }

    }
}
