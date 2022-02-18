// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;
using FellowOakDicom.Log;
using FellowOakDicom.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Buffers;
using System.Collections.Generic;
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
        /// Default DICOM encoding.
        /// </summary>
        public static readonly Encoding Default = Encoding.ASCII;

        public static readonly Encoding[] DefaultArray = { Default };

        /// <summary>
        /// Get multiple encodings from charsets.
        /// </summary>
        /// <param name="charsets">List of character sets.</param>
        /// <returns>DICOM encodings.</returns>
        public static Encoding[] GetEncodings(string[] charsets) =>
            (from charset in charsets select GetEncoding(charset)).ToArray();

        /// <summary>
        /// Get encoding from charset.
        /// </summary>
        /// <param name="charset">Charset.</param>
        /// <returns>DICOM encoding.</returns>
        public static Encoding GetEncoding(string charset)
        {
            if (string.IsNullOrEmpty(charset?.Trim()))
            {
                return Default;
            }

            if (_knownEncodings.TryGetValue(charset.Trim().Replace("_", " "), out Encoding encoding))
            {
                return encoding;
            }

            Logger.Warn($"'{charset}' is not a valid DICOM encoding - using ASCII encoding instead.");

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
        /// <exception cref="ArgumentException">No character set found for the encoding.</exception>
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
            { "ISO IR 13", "shift_jis" }, // JIS X 0201 (Shift JIS)
            { "ISO IR 100", "iso-8859-1" }, // Latin Alphabet No. 1
            { "ISO IR 101", "iso-8859-2" }, // Latin Alphabet No. 2
            { "ISO IR 109", "iso-8859-3" }, // Latin Alphabet No. 3
            { "ISO IR 110", "iso-8859-4" }, // Latin Alphabet No. 4
            { "ISO IR 126", "iso-8859-7" }, // Greek
            { "ISO IR 127", "iso-8859-6" }, // Arabic
            { "ISO IR 138", "iso-8859-8" }, // Hebrew
            { "ISO IR 144", "iso-8859-5" }, // Cyrillic
            { "ISO IR 148", "iso-8859-9" }, // Latin Alphabet No. 5 (Turkish)
            { "ISO IR 149", "x-cp20949" }, // KS X 1001 (Hangul and Hanja)
            { "ISO IR 166", "windows-874" }, // TIS 620-2533 (Thai)
            { "ISO IR 192", "utf-8" }, // Unicode in UTF-8
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

        private static Encoding GetCodeForEncoding(byte code1, byte code2, byte code3) =>
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
        private static readonly byte[] _textDelimiters =
        {
            0x0d, // CR
            0x0a, // LF
            0x09, // TAB
            0x0c // FF
        };

        // Delimiters in PN values that reset the encoding
        private static readonly byte[] _pnDelimiters =
        {
            0x5e, // ^
            0x3d, // =
        };

        internal static string DecodeBytes(IByteBuffer buffer, Encoding[] encodings, bool isPN)
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
            var delimiters = isPN ? _pnDelimiters : _textDelimiters;
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

        private static readonly int[] _codePagesForHandledEncodings =
        {
            50220, // iso-2022-jp
            936 // gb2312
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
                encoding = GetCodeForEncoding(fragment[1], fragment[2], seqLength == 4 ? fragment[3] : (byte)0);
                if (encoding == null)
                {
                    Logger.Warn("Unknown escape sequence found in string, using ASCII encoding.");
                    encoding = Default;
                }
                else if (encoding.CodePage != Default.CodePage && !encodings.Contains(encoding))
                {
                    // maybe be shall try to use the encoding anyway? 
                    Logger.Warn("Found escape sequence for '{encodingName}', which is " +
                                "not defined in Specific Character Set, using ASCII encoding instead.",
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

        private static string GetStringFromEncoding(byte[] fragment, Encoding encoding, int index, int count)
        {
            try
            {
                return StrictEncoding(encoding).GetString(fragment, index, count);
            }
            catch (DecoderFallbackException)
            {
                var decoded = encoding.GetString(fragment, index, count);
                Logger.Warn("Could not decode string '{decoded}' with given encoding, using replacement characters.",
                    decoded);
                return decoded;
            }
        }

        private static ILogger Logger =>
            Setup.ServiceProvider.GetRequiredService<ILogManager>()
                .GetLogger("FellowOakDicom.DicomEncoding");
    }
}