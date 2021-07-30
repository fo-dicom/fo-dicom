// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;
using System;
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
            }
            catch
            {
                /* do nothing */
            }
        }

        static void RegisterEncodingProvider()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

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

            return charset.Trim().Replace("_", " ") switch
            {
                "ISO IR 13" => Encoding.GetEncoding("shift_jis"), // JIS X 0201 (Shift JIS)
                "ISO IR 100" => Encoding.GetEncoding("iso-8859-1"), // Latin Alphabet No. 1
                "ISO IR 101" => Encoding.GetEncoding("iso-8859-2"), // Latin Alphabet No. 2
                "ISO IR 109" => Encoding.GetEncoding("iso-8859-3"), // Latin Alphabet No. 3
                "ISO IR 110" => Encoding.GetEncoding("iso-8859-4"), // Latin Alphabet No. 4
                "ISO IR 126" => Encoding.GetEncoding("iso-8859-7"), // Greek
                "ISO IR 127" => Encoding.GetEncoding("iso-8859-6"), // Arabic
                "ISO IR 138" => Encoding.GetEncoding("iso-8859-8"), // Hebrew
                "ISO IR 144" => Encoding.GetEncoding("iso-8859-5"), // Cyrillic
                "ISO IR 148" => Encoding.GetEncoding("iso-8859-9"), // Latin Alphabet No. 5 (Turkish)
                "ISO IR 149" => Encoding.GetEncoding("x-cp20949"), // KS X 1001 (Hangul and Hanja)
                "ISO IR 166" => Encoding.GetEncoding("windows-874"), // TIS 620-2533 (Thai)
                "ISO IR 192" => Encoding.GetEncoding("utf-8"), // Unicode in UTF-8
                "GBK" => Encoding.GetEncoding("GBK"), // Chinese (Simplified)
                "GB18030" => Encoding.GetEncoding("GB18030"), // Chinese (supersedes GBK)
                "ISO 2022 IR 6" => Encoding.GetEncoding("us-ascii"), // ASCII
                "ISO 2022 IR 13" => Encoding.GetEncoding("shift_jis"), // JIS X 0201 (Shift JIS) Extended
                "ISO 2022 IR 87" => Encoding.GetEncoding("iso-2022-jp"), // JIS X 0208 (Kanji) Extended
                "ISO 2022 IR 100" => Encoding.GetEncoding("iso-8859-1"), // Latin Alphabet No. 1 Extended
                "ISO 2022 IR 101" => Encoding.GetEncoding("iso-8859-2"), // Latin Alphabet No. 2 Extended
                "ISO 2022 IR 109" => Encoding.GetEncoding("iso-8859-3"), // Latin Alphabet No. 3 Extended
                "ISO 2022 IR 110" => Encoding.GetEncoding("iso-8859-4"), // Latin Alphabet No. 4 Extended
                "ISO 2022 IR 127" => Encoding.GetEncoding("iso-8859-6"), // Arabic Extended
                "ISO 2022 IR 126" => Encoding.GetEncoding("iso-8859-7"), // Greek Extended
                "ISO 2022 IR 138" => Encoding.GetEncoding("iso-8859-8"), // Hebrew Extended
                "ISO 2022 IR 144" => Encoding.GetEncoding("iso-8859-5"), // Cyrillic Extended
                "ISO 2022 IR 148" => Encoding.GetEncoding("iso-8859-9"), // Latin Alphabet No. 5 (Turkish) Extended
                "ISO 2022 IR 149" => Encoding.GetEncoding("x-cp20949"), // KS X 1001 (Hangul and Hanja) Extended
                "ISO 2022 IR 159" => Encoding.GetEncoding("iso-2022-jp"), // JIS X 0212 (Kanji) Extended
                "ISO 2022 IR 166" => Encoding.GetEncoding("windows-874"), // TIS 620-2533 (Thai) Extended
                "ISO 2022 IR 58" => Encoding.GetEncoding("gb2312"), // Chinese (Simplified) Extended
                "ISO 2022 GBK" => Encoding.GetEncoding("GBK"), // Chinese (Simplified) Extended (supersedes GB2312)
                _ => Default // unknown encoding... return ASCII instead of throwing exception
            };
        }

        /// <summary>
        /// Get charset from encoding.
        /// </summary>
        /// <param name="encoding">Encoding.</param>
        /// <returns>Charset.</returns>
        public static string GetCharset(Encoding encoding)
        {
            // Do we always want the extended charsets?
            return encoding?.WebName switch
            {
                null => "ISO 2022 IR 6",
                "windows-874" => "ISO 2022 IR 166", // TIS 620-2533 (Thai) Extended
                "shift_jis" => "ISO_IR 13", // JIS X 0201 (Shift JIS) Unextended
                "us-ascii" => "ISO 2022 IR 6", // ASCII
                "x-cp20949" => "ISO 2022 IR 149", // KS X 1001 (Hangul and Hanja) Extended
                "iso-8859-1" => "ISO 2022 IR 100", // Latin Alphabet No. 1 Extended
                "iso-8859-2" => "ISO 2022 IR 101", // Latin Alphabet No. 2 Extended
                "iso-8859-3" => "ISO 2022 IR 109", // Latin Alphabet No. 3 Extended
                "iso-8859-4" => "ISO 2022 IR 110", // Latin Alphabet No. 4 Extended
                "iso-8859-5" => "ISO 2022 IR 144", // Cyrillic Extended
                "iso-8859-6" => "ISO 2022 IR 127", // Arabic Extended
                "iso-8859-7" => "ISO 2022 IR 126", // Greek Extended
                "iso-8859-8" => "ISO 2022 IR 138", // Hebrew Extended
                "iso-8859-9" => "ISO 2022 IR 148", // Latin Alphabet No. 5 (Turkish) Extended
                "iso-2022-jp" => "ISO 2022 IR 159", // JIS X 0212 (Kanji) Extended
                "gb18030" => "GB18030",
                "GB18030" => "GB18030", // Chinese (Simplified) Extended
                "utf-8" => "ISO_IR 192", // Unicode in UTF-8
                _ => throw new ArgumentException("No DICOM charset found for requested encoding.", nameof(encoding))
            };
        }

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
                    _ => Default
                },
                0x28 => code2 switch
                {
                    0x42 => Default,
                    0x4a => Encoding.GetEncoding("shift_jis"),
                    _ => Default
                },
                0x29 => code2 switch
                {
                    0x49 => Encoding.GetEncoding("shift_jis"),
                    _ => Default
                },
                0x24 => code2 switch
                {
                    0x28 => code3 switch
                    {
                        0x44 => Encoding.GetEncoding("iso-2022-jp"),
                        _ => Default
                    },
                    0x29 => code3 switch
                        {
                            0x43 => Encoding.GetEncoding("x-cp20949"),
                            0x44 => Encoding.GetEncoding("iso-2022-jp"),
                            0x41 => Encoding.GetEncoding("gb2312"),
                            _ => Default
                        },
                    0x42 => Encoding.GetEncoding("iso-2022-jp"),
                    _ => Default
                },
                _ => Default
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

        public static string DecodeBytes(IByteBuffer buffer, Encoding[] encodings, bool isPN)
        {
            var firstEncoding = encodings?.FirstOrDefault() ?? Default;
            var value = buffer.Data;
            if (encodings == null || encodings.Length < 2)
            {
                return firstEncoding.GetString(value, 0, (int)buffer.Size);
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
                return firstEncoding.GetString(buffer.Data, 0, (int)buffer.Size);
            }

            var decodedString = new StringBuilder();
            var delimiters = isPN ? _pnDelimiters : _textDelimiters;
            
            for (int i = 0; i < escapeIndexes.Count; i++)
            {
                var start = i == 0 ? 0 : escapeIndexes[i - 1];
                var end = escapeIndexes[i];
                if (end > start)
                {
                    var fragment = buffer.GetByteRange(start, end - start);
                    var decodedFragment = DecodeFragment(fragment, encodings, delimiters);
                    decodedString.Append(decodedFragment);
                }
            }

            var lastIndex = escapeIndexes.Last();
            var lastFragment = buffer.GetByteRange(lastIndex, (int)buffer.Size - lastIndex);
            var lastDecodedFragment = DecodeFragment(lastFragment, encodings, delimiters);
            decodedString.Append(lastDecodedFragment);

            return decodedString.ToString();
        }

        private static readonly int[] _codePagesForHandledEncodings =
        {
            50220, // iso-2022-jp
            936 // gb2312
        };

        private static string DecodeFragment(byte[] fragment, Encoding[] encodings, byte[] delimiters)
        {
            var seqLength = 0;
            Encoding encoding;
            if (fragment[0] == 0x1b) // ESC
            {
                seqLength = fragment.Length >= 3 && fragment[1] == '$' && fragment[2] == '(' || fragment[2] == ')'
                    ? 4
                    : 3;
                encoding = GetCodeForEncoding(fragment[1], fragment[2], seqLength == 4 ? fragment[3] : (byte)0);
                if (!encodings.Contains(encoding) && !encoding.Equals(Default))
                {
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
                for (int i = seqLength; i < fragment.Length; i++)
                {
                    if (delimiters.Contains(fragment[i]))
                    {
                        // the encoding is reset after a delimiter
                        return encoding.GetString(fragment, seqLength, i - seqLength) +
                               encodings[0].GetString(fragment, i, fragment.Length - i);
                    }
                }
            }

            return encoding.GetString(fragment, seqLength, fragment.Length - seqLength);
        }
    }
}
