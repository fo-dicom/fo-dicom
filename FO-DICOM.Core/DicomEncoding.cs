// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
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
            catch { /* do nothing */ }
        }

        static void RegisterEncodingProvider()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Default DICOM encoding.
        /// </summary>
        public static readonly Encoding Default = Encoding.ASCII;

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

            return charset.Trim() switch
            {
                "ISO IR 13" => Encoding.GetEncoding("shift_jis"), // JIS X 0201 (Shift JIS) Unextended
                "ISO_IR 13" => Encoding.GetEncoding("shift_jis"), // JIS X 0201 (Shift JIS) Unextended
                "ISO IR 100" => Encoding.GetEncoding("iso-8859-1"), // Latin Alphabet No. 1 Unextended
                "ISO_IR 100" => Encoding.GetEncoding("iso-8859-1"), // Latin Alphabet No. 1 Unextended
                "ISO IR 101" => Encoding.GetEncoding("iso-8859-2"), // Latin Alphabet No. 2 Unextended
                "ISO_IR 101" => Encoding.GetEncoding("iso-8859-2"), // Latin Alphabet No. 2 Unextended
                "ISO IR 109" => Encoding.GetEncoding("iso-8859-3"), // Latin Alphabet No. 3 Unextended
                "ISO_IR 109" => Encoding.GetEncoding("iso-8859-3"), // Latin Alphabet No. 3 Unextended
                "ISO IR 110" => Encoding.GetEncoding("iso-8859-4"), // Latin Alphabet No. 4 Unextended
                "ISO_IR 110" => Encoding.GetEncoding("iso-8859-4"), // Latin Alphabet No. 4 Unextended
                "ISO IR 126" => Encoding.GetEncoding("iso-8859-7"), // Greek Unextended
                "ISO_IR 126" => Encoding.GetEncoding("iso-8859-7"), // Greek Unextended
                "ISO IR 127" => Encoding.GetEncoding("iso-8859-6"), // Arabic Unextended
                "ISO_IR 127" => Encoding.GetEncoding("iso-8859-6"), // Arabic Unextended
                "ISO IR 138" => Encoding.GetEncoding("iso-8859-8"), // Hebrew Unextended
                "ISO_IR 138" => Encoding.GetEncoding("iso-8859-8"), // Hebrew Unextended
                "ISO IR 144" => Encoding.GetEncoding("iso-8859-5"), // Cyrillic Unextended
                "ISO_IR 144" => Encoding.GetEncoding("iso-8859-5"), // Cyrillic Unextended
                "ISO IR 148" => Encoding.GetEncoding("iso-8859-9"), // Latin Alphabet No. 5 (Turkish) Unextended
                "ISO_IR 148" => Encoding.GetEncoding("iso-8859-9"), // Latin Alphabet No. 5 (Turkish) Unextended
                "ISO IR 166" => Encoding.GetEncoding("windows-874"), // TIS 620-2533 (Thai) Unextended
                "ISO_IR 166" => Encoding.GetEncoding("windows-874"), // TIS 620-2533 (Thai) Unextended
                "ISO IR 192" => Encoding.GetEncoding("utf-8"), // Unicode in UTF-8
                "ISO_IR 192" => Encoding.GetEncoding("utf-8"), // Unicode in UTF-8
                "ISO 2022 IR 6" => Encoding.GetEncoding("us-ascii"), // ASCII
                "ISO 2022 IR 13" => Encoding.GetEncoding("iso-2022-jp"), // JIS X 0201 (Shift JIS) Extended
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
                "GB18030" => Encoding.GetEncoding("GB18030"), // Chinese (Simplified) Extended
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
                _ => throw new ArgumentException("No DICOM charset found for requested encoding.", "encoding")
            };
        }

    }
}
