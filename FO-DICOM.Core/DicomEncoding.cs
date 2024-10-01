// Copyright (c) 2012-2024 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using FellowOakDicom.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FellowOakDicom
{
    /// <summary>
    /// Support methods for DICOM encoding.
    /// </summary>
    public static class DicomEncoding
    {
        static DicomEncoding()
        {
            try
            {
                RegisterEncodingProvider();
                foreach (var encodingName in _knownEncodingNames)
                {
                    RegisterEncoding(encodingName.Key, encodingName.Value);
                }
            }
            catch
            {
                /* do nothing */
            }
        }

        static void RegisterEncodingProvider() => Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        /// <summary>
        /// Default encoding used in DICOM.
        /// </summary>
        public static readonly Encoding Default = Encoding.ASCII;

        public static readonly Encoding[] DefaultArray = { Default };

        /// <summary>
        /// Get multiple encodings from Specific Character Set attribute values.
        /// </summary>
        /// <param name="charsets">List of character sets.</param>
        /// <returns>String encodings.</returns>
        public static Encoding[] GetEncodings(string[] charsets) =>
            charsets.Select(GetEncoding).ToArray();

        /// <summary>
        /// Get encoding from Specific Character Set attribute value.
        /// Tolerates some common misspellings.
        /// </summary>
        /// <param name="charset">DICOM character set.</param>
        /// <returns>String encoding.</returns>
        public static Encoding GetEncoding(string charset)
        {
            charset = charset?.Trim();
            if (string.IsNullOrEmpty(charset))
            {
                return Default;
            }

            if (_knownEncodings.TryGetValue(charset, out Encoding encoding))
            {
                return encoding;
            }

            // Also allow some common misspellings (ISO-IR ### or ISO IR ### instead of ISO_IR ###)
            if (_knownEncodings.TryGetValue(charset.Replace("ISO IR", "ISO_IR")
                    .Replace("ISO-IR", "ISO_IR"), out encoding))
            {
                return encoding;
            }

            Logger.LogWarning("\'{Charset}\' is not a valid DICOM encoding - using ASCII encoding instead", charset);

            return Default;
        }

        /// <summary>
        /// Register an encoding for a specific character set value.
        /// Can be used to add an encoding that is not handled by the library, or a private encoding.
        /// Can also be used to map an existing character set to another encoding.
        /// </summary>
        /// <param name="charset">The name of the character set as given
        /// in the Specific Character Set DICOM attribute.</param>
        /// <param name="encoding">The name of the character encoding used to decode the DICOM tag values
        /// as defined in the .NET framework.</param>
        public static void RegisterEncoding(string charset, string encoding)
        {
            var knownEncoding = Encoding.GetEncoding(encoding);
            _knownEncodings[charset] = knownEncoding;
            // for decoding, we use encoders that raise exceptions on encoding/decoding errors do be able
            // to detect such errors - these exceptions have to be handled during encoding/decoding
            _strictEncodings[knownEncoding.CodePage] = Encoding.GetEncoding(encoding,
                EncoderFallback.ExceptionFallback,
                DecoderFallback.ExceptionFallback);
        }

        /// <summary>
        /// Get strict encodings for given encodings.
        /// The encodings will throw EncoderFallbackException if applied to a string they cannot encode.
        /// </summary>
        /// <param name="encodings">The list of non-strict encodings.</param>
        /// <returns>Array of string encoding.</returns>
        private static Encoding[] GetStrictEncodings(Encoding[] encodings) =>
            encodings.Select(StrictEncoding).ToArray();

        private static Encoding StrictEncoding(Encoding encoding)
        {
            if (!_strictEncodings.TryGetValue(encoding.CodePage, out Encoding strictEncoding))
            {
                strictEncoding = Encoding.GetEncoding(encoding.CodePage,
                    EncoderFallback.ExceptionFallback,
                    DecoderFallback.ExceptionFallback);
            }

            return strictEncoding;
        }

        /// <summary>
        /// Get DICOM character set from encoding.
        /// </summary>
        /// <param name="encoding">Encoding.</param>
        /// <param name="extended">If true, the extended version of the character set is returned.</param>
        /// <returns>The Specific Character Set as defined in DICOM.</returns>
        /// <exception cref="System.ArgumentException">No character set found for the encoding.</exception>
        public static string GetCharset(Encoding encoding, bool extended = false)
        {
            var name = _knownEncodingNames.FirstOrDefault(x => x.Value == encoding.WebName &&
                                                               x.Key.StartsWith("ISO 2022") == extended).Key;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("No DICOM charset found for requested encoding.", nameof(encoding));
            }

            return name;
        }

        private static readonly IDictionary<string, string> _knownEncodingNames = new Dictionary<string, string>
        {
            { "ISO_IR 13", "shift_jis" }, // JIS X 0201 (Shift JIS)
            { "ISO_IR 100", "iso-8859-1" }, // Latin Alphabet No. 1
            { "ISO_IR 101", "iso-8859-2" }, // Latin Alphabet No. 2
            { "ISO_IR 109", "iso-8859-3" }, // Latin Alphabet No. 3
            { "ISO_IR 110", "iso-8859-4" }, // Latin Alphabet No. 4
            { "ISO_IR 126", "iso-8859-7" }, // Greek
            { "ISO_IR 127", "iso-8859-6" }, // Arabic
            { "ISO_IR 138", "iso-8859-8" }, // Hebrew
            { "ISO_IR 144", "iso-8859-5" }, // Cyrillic
            { "ISO_IR 148", "iso-8859-9" }, // Latin Alphabet No. 5 (Turkish)
            { "ISO_IR 149", "x-cp20949" }, // KS X 1001 (Hangul and Hanja)
            { "ISO_IR 166", "windows-874" }, // TIS 620-2533 (Thai)
            { "ISO_IR 192", "utf-8" }, // Unicode in UTF-8
            { "GBK", "GBK" }, // Chinese (Simplified)
            { "GB18030", "gb18030" }, // Chinese (supersedes GBK)
            { "ISO 2022 IR 6", "us-ascii" }, // ASCII
            { "ISO 2022 IR 13", "shift_jis" }, // JIS X 0201 (Shift JIS) Extended
            { "ISO 2022 IR 87", "iso-2022-jp" }, // JIS X 0208 (Kanji) Extended
            { "ISO 2022 IR 100", "iso-8859-1" }, // Latin Alphabet No. 1 Extended
            { "ISO 2022 IR 101", "iso-8859-2" }, // Latin Alphabet No. 2 Extended
            { "ISO 2022 IR 109", "iso-8859-3" }, // Latin Alphabet No. 3 Extended
            { "ISO 2022 IR 110", "iso-8859-4" }, // Latin Alphabet No. 4 Extended
            { "ISO 2022 IR 127", "iso-8859-6" }, // Arabic Extended
            { "ISO 2022 IR 126", "iso-8859-7" }, // Greek Extended
            { "ISO 2022 IR 138", "iso-8859-8" }, // Hebrew Extended
            { "ISO 2022 IR 144", "iso-8859-5" }, // Cyrillic Extended
            { "ISO 2022 IR 148", "iso-8859-9" }, // Latin Alphabet No. 5 (Turkish) Extended
            { "ISO 2022 IR 149", "x-cp20949" }, // KS X 1001 (Hangul and Hanja) Extended
            { "ISO 2022 IR 159", "iso-2022-jp" }, // JIS X 0212 (Kanji) Extended
            { "ISO 2022 IR 166", "windows-874" }, // TIS 620-2533 (Thai) Extended
            { "ISO 2022 IR 58", "gb2312" }, // Chinese (Simplified) Extended
            { "ISO 2022 GBK", "GBK" }, // Chinese (Simplified) Extended (supersedes GB2312)
        };

        /// <summary>
        /// The escape sequence that we need to insert ourselves for extended encodings
        /// mapped against the code pages of the encodings.
        /// </summary>
        private static readonly IDictionary<int, byte[]> _escapeSequences = new Dictionary<int, byte[]>
        {
            { 28591, new byte[] { 0x1b, 0x2d, 0x41 } },   // Latin 1 (Western European)
            { 28592, new byte[] { 0x1b, 0x2d, 0x42 } },   // Latin 2 (Central European)
            { 28593, new byte[] { 0x1b, 0x2d, 0x43 } },   // Latin 3 (South European)
            { 28594, new byte[] { 0x1b, 0x2d, 0x44 } },   // Latin 4 (North European)
            { 28595, new byte[] { 0x1b, 0x2d, 0x4c } },   // Latin/Cyrillic
            { 28596, new byte[] { 0x1b, 0x2d, 0x47 } },   // Latin/Arabic
            { 28597, new byte[] { 0x1b, 0x2d, 0x46 } },   // Latin/Greek
            { 28598, new byte[] { 0x1b, 0x2d, 0x48 } },   // Latin/Hebrew
            { 28599, new byte[] { 0x1b, 0x2d, 0x4d } },   // Latin 5 (Turkish)
            { 874, new byte[] { 0x1b, 0x2d, 0x54 } },     // Thai (Windows)
            { 932, new byte[] { 0x1b, 0x28, 0x4a } },     // Japanese (Shift-JIS)
            { 20949, new byte[] { 0x1b, 0x24, 0x29, 0x43 } },  // Korean Wansung
        };
    
        /// <summary>
        /// The known encodings with character replacement fallback handlers.
        /// </summary>
        private static readonly IDictionary<string, Encoding> _knownEncodings =
            new Dictionary<string, Encoding>();

        /// <summary>
        /// The known encodings with exception fallback handlers.
        /// Used to detect encoding/decoding errors.
        /// </summary>
        private static readonly IDictionary<int, Encoding> _strictEncodings =
            new Dictionary<int, Encoding>();

        private static Encoding GetEncodingForEscapeSequence(byte code1, byte code2, byte code3) =>
            code1 switch
            {
                0x2d => code2 switch
                {
                    0x41 => Encoding.GetEncoding("iso-8859-1"),
                    0x42 => Encoding.GetEncoding("iso-8859-2"),
                    0x43 => Encoding.GetEncoding("iso-8859-3"),
                    0x44 => Encoding.GetEncoding("iso-8859-4"),
                    0x46 => Encoding.GetEncoding("iso-8859-7"),
                    0x47 => Encoding.GetEncoding("iso-8859-6"),
                    0x48 => Encoding.GetEncoding("iso-8859-8"),
                    0x4c => Encoding.GetEncoding("iso-8859-5"),
                    0x4d => Encoding.GetEncoding("iso-8859-9"),
                    0x54 => Encoding.GetEncoding("windows-874"),
                    _ => null
                },
                0x28 => code2 switch
                {
                    0x42 => Default,
                    0x4a => Encoding.GetEncoding("shift_jis"),
                    _ => null
                },
                0x29 => code2 switch
                {
                    0x49 => Encoding.GetEncoding("shift_jis"),
                    _ => null
                },
                0x24 => code2 switch
                {
                    0x28 => code3 switch
                    {
                        0x44 => Encoding.GetEncoding("iso-2022-jp"),
                        _ => null
                    },
                    0x29 => code3 switch
                    {
                        0x43 => Encoding.GetEncoding("x-cp20949"),
                        0x44 => Encoding.GetEncoding("iso-2022-jp"),
                        0x41 => Encoding.GetEncoding("gb2312"),
                        _ => null
                    },
                    0x42 => Encoding.GetEncoding("iso-2022-jp"),
                    _ => null
                },
                _ => null
            };

        // Delimiters in text values that reset the encoding
        private static readonly byte[] _textDelimiterBytes =
        {
            0x0d, // CR
            0x0a, // LF
            0x09, // TAB
            0x0c // FF
        };
        
        private static readonly char[] _textDelimiterChars =
            Encoding.ASCII.GetString(_textDelimiterBytes).ToCharArray();

        // Delimiters in PN values that reset the encoding
        private static readonly byte[] _pnDelimiterBytes =
        {
            0x5e, // ^
            0x3d, // =
        };

        private static readonly char[] _pnDelimiterChars = 
            Encoding.ASCII.GetString(_pnDelimiterBytes).ToCharArray();

        internal static string DecodeBytes(IByteBuffer buffer, Encoding[] encodings, bool isPersonName)
        {
            var firstEncoding = encodings?.FirstOrDefault() ?? Default;
            var value = buffer.Data;
            if (encodings == null || encodings.Length < 2)
            {
                return GetStringFromEncoding(value, firstEncoding, 0, (int)buffer.Size);
            }

            var escapeIndexes = new List<int>();
            for (int i = 0; i < buffer.Size; i++)
            {
                if (value[i] == 0x1b) // ESC
                {
                    escapeIndexes.Add(i);
                }
            }

            if (escapeIndexes.Count == 0)
            {
                return GetStringFromEncoding(buffer.Data, firstEncoding, 0, (int)buffer.Size);
            }

            var decodedString = new StringBuilder();
            var delimiters = isPersonName ? _pnDelimiterBytes : _textDelimiterBytes;
            var memoryProvider = Setup.ServiceProvider.GetRequiredService<IMemoryProvider>();
            for (int i = 0; i < escapeIndexes.Count; i++)
            {
                var start = i == 0 ? 0 : escapeIndexes[i - 1];
                var end = escapeIndexes[i];
                if (end > start)
                {
                    var length = end - start;
                    using var fragment = memoryProvider.Provide(length);
                    buffer.GetByteRange(start, length, fragment.Bytes);
                    var decodedFragment = DecodeFragment(fragment.Bytes, length, encodings, delimiters);
                    decodedString.Append(decodedFragment);
                }
            }

            var lastIndex = escapeIndexes.Last();
            var lastFragmentLength = (int)buffer.Size - lastIndex;
            using var lastFragment = memoryProvider.Provide(lastFragmentLength);
            buffer.GetByteRange(lastIndex, lastFragmentLength, lastFragment.Bytes);
            var lastDecodedFragment = DecodeFragment(lastFragment.Bytes, lastFragmentLength, encodings, delimiters);
            decodedString.Append(lastDecodedFragment);

            return decodedString.ToString();
        }

        internal static byte[] EncodeString(string value, Encoding[] encodings, bool isPersonName)
        {
            var strictEncodings = GetStrictEncodings(encodings);
            try
            {
                // default case - can be encoded using the first encoding
                return strictEncodings[0].GetBytes(value);
            }
            catch (EncoderFallbackException)
            {
                // could not encode the value with the first encoding, try all encodings
                
                // if there are delimiters in the string, the string has to be split into fragments
                // for encoding, as a delimiter resets the encoding
                var delimiters = isPersonName ? _pnDelimiterChars : _textDelimiterChars;
                
                MemoryStream stream = new MemoryStream();
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    var currentIndex = 0;
                    while(true)
                    {
                        var delimiterIndex = -1;
                        char currentDelimiter = '\0';
                        foreach (var delimiter in delimiters)
                        {
                            var index = value.IndexOf(delimiter, currentIndex);
                            if (index >= 0 && (delimiterIndex == -1 || index < delimiterIndex))
                            {
                                delimiterIndex = index;
                                currentDelimiter = delimiter;
                            }
                        }

                        if (delimiterIndex == -1)
                        {
                            // found last fragment
                            EncodeFragment(value.Substring(currentIndex, value.Length - currentIndex), encodings, strictEncodings, writer);
                            break;
                        }
                        EncodeFragment(value.Substring(currentIndex, delimiterIndex - currentIndex), encodings, strictEncodings, writer);
                        writer.Write(Convert.ToByte(currentDelimiter));
                        currentIndex = delimiterIndex + 1;
                    }                    
                }

                return stream.ToArray();
            }
        }

        private static readonly int[] _codePagesForHandledEncodings =
        {
            50220, // iso-2022-jp
        };

        private static string DecodeFragment(byte[] fragment, int fragmentLength, Encoding[] encodings, byte[] delimiters)
        {
            var seqLength = 0;
            Encoding encoding;
            if (fragment[0] == 0x1b) // ESC
            {
                seqLength = fragmentLength >= 3 && fragment[1] == '$' && fragment[2] == '(' || fragment[2] == ')'
                    ? 4
                    : 3;
                encoding = GetEncodingForEscapeSequence(fragment[1], fragment[2], seqLength == 4 ? fragment[3] : (byte)0);
                if (encoding == null)
                {
                    Logger.LogWarning("Unknown escape sequence found in string, using ASCII encoding");
                    encoding = Default;
                }
                else if (encoding.CodePage != Default.CodePage && !encodings.Contains(encoding))
                {
                    // maybe be shall try to use the encoding anyway? 
                    Logger.LogWarning("Found escape sequence for '{EncodingName}', which is " +
                                "not defined in Specific Character Set, using ASCII encoding instead",
                        encoding.WebName);
                    encoding = Default;
                }
            }
            else
            {
                encoding = encodings[0];
            }

            if (_codePagesForHandledEncodings.Contains(encoding.CodePage))
            {
                // for these encodings the escape codes are handled by the encoding
                // and have to remain in the byte array
                seqLength = 0;
            }
            else
            {
                for (int i = seqLength; i < fragmentLength - 1; i++)
                {
                    if (delimiters.Contains(fragment[i]))
                    {
                        // the encoding is reset after a delimiter
                        return GetStringFromEncoding(fragment, encoding, seqLength, i - seqLength) +
                               GetStringFromEncoding(fragment, encodings[0], i, fragmentLength - i);
                    }
                }
            }

            return GetStringFromEncoding(fragment, encoding, seqLength, fragmentLength - seqLength);
        }

        private static void EncodeFragment(string fragment, Encoding[] encodings, Encoding[] strictEncodings, BinaryWriter writer)
        {
            try
            {
                writer.Write(strictEncodings[0].GetBytes(fragment));
                return;
            }
            catch (EncoderFallbackException)
            {
                foreach (var encoding in strictEncodings.Skip(1))
                {
                    try
                    {
                        var bytes = encoding.GetBytes(fragment);
                        // some escape sequences are already added by the encoder
                        if (!_codePagesForHandledEncodings.Contains(encoding.CodePage))
                        {
                            var controlBytes = _escapeSequences[encoding.CodePage];
                            writer.Write(controlBytes);
                        }
                        writer.Write(bytes);
                        return;
                    }
                    catch (EncoderFallbackException)
                    {
                        // try next encoding if any
                    }
                }
            }

            // the fallback uses replacement characters
            Logger.LogWarning("Could not encode string '{Fragment}' with given encodings, " +
                              "using replacement characters for encoding", fragment);
            var encoded = encodings[0].GetBytes(fragment);
            writer.Write(encoded);
        }

        private static string GetStringFromEncoding(byte[] fragment, Encoding encoding, int index, int count)
        {
            try
            {
                return StrictEncoding(encoding).GetString(fragment, index, count);
            }
            catch (DecoderFallbackException)
            {
                var decoded = encoding.GetString(fragment, index, count);
                Logger.LogWarning("Could not decode string '{Decoded}' with given encoding, using replacement characters",
                    decoded);
                return decoded;
            }
        }

        private static ILogger Logger =>
            Setup.ServiceProvider.GetRequiredService<ILoggerFactory>()
                .CreateLogger(Log.LogCategories.Encoding);
    }
}
