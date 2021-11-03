// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Text.Json;

namespace FellowOakDicom.Serialization
{
    internal static class Utf8JsonReaderExtensions
    {
        public static void Assume(this ref Utf8JsonReader reader, JsonTokenType tokenType)
        {
            if (reader.TokenType != tokenType)
            {
                throw new JsonException($"invalid: {tokenType} expected at position {reader.TokenStartIndex}, instead found {reader.TokenType}");
            }
        }

        public static void AssumeAndSkip(this ref Utf8JsonReader reader, JsonTokenType tokenType)
        {
            Assume(ref reader, tokenType);
            reader.Read();
        }
    }
}
