// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace Dicom
{

    public static class DicomValidation
    {

        internal static bool PerformValidation { get; set; } = true;


        /// <summary>
        /// Gets or sets if the content of DicomItems shall be validated as soon as they are added to the DicomDataset.
        /// This gobal property enables you to disable validation gobally instead of turning validation off for every single DicomDataset.
        /// </summary>
        [Obsolete("Use this property with care. You can suppress validation, but be aware you might create invalid Datasets if you set this property to true.", false)]
        public static bool AutoValidation
        {
            get => PerformValidation;
            set => PerformValidation = value;
        }


        public static void ValidateAE(string content)
        {
            // may not be longer than 16 characters
            if (content.Length > 16)
            {
                throw new DicomValidationException(content, DicomVR.AE, "value exceeds maximum length of 16 characters");
            }
            // may not contain only of spaces
            if (Regex.IsMatch(content, @"^\s*$"))
            {
                throw new DicomValidationException(content, DicomVR.AE, "value may not consist only of spaces");
            }
            // Default Character Repertoire excluding character code 5CH (the BACKSLASH "\" in ISO-IR 6), and control characters LF, FF, CR and ESC.
            if (content.Contains("\\") || content.ToCharArray().Any(Char.IsControl))
            {
                throw new DicomValidationException(content, DicomVR.AE, "value contains invalid control character");
            }
        }


        public static void ValidateAS(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            // 4 charachters fixed
            // one of the following formats -- nnnD, nnnW, nnnM, nnnY; where nnn shall contain the number of days for D, weeks for W, months for M, or years for Y.
            if (!Regex.IsMatch(content, @"^\d\d\d[DWMY]$"))
            {
                throw new DicomValidationException(content, DicomVR.AS, "value does not have pattern 000[DWMY]");
            }
        }


        public static void ValidateCS(string content)
        {
            // 16 bytes maximum
            if (content.Length > 16)
            {
                throw new DicomValidationException(content, DicomVR.CS, "value exceeds maximum length of 16 characters");
            }
            // Uppercase characters, "0" - "9", the SPACE character, and underscore "_", of the Default Character Repertoire
            if (!Regex.IsMatch(content, "^[A-Z0-9_ ]*$"))
            {
                throw new DicomValidationException(content, DicomVR.CS, "value contains invalid character. Only uppercase character, digits, space and underscore alre allowed");
            }
        }


        public static void ValidateDA(string content)
        {
            // TODO
        }


        public static void ValidateDS(string content)
        {
            // This is not very inefficient - uses .NET regex caching
            if (!Regex.IsMatch(content, "^[+-]?((0|[1-9][0-9]*)([.][0-9]+)?|[.][0-9]+)([eE][-+]?[0-9]+)?$"))
            {
                throw new DicomValidationException(content, DicomVR.DS, "value is no decimal string");
            }
        }


        public static void ValidateDT(string content)
        {
            // TODO:
            /*
             "0"-"9", "+", "-", "." and the SPACE character of Default Character Repertoire

              26 bytes maximum. In the context of a Query with range matching (see PS3.4), the length is 54 bytes maximum.

             A concatenated date-time character string in the format:
             YYYYMMDDHHMMSS.FFFFFF&ZZXX

             The components of this string, from left to right, are YYYY = Year, MM = Month, DD = Day,
             HH = Hour (range "00" - "23"), MM = Minute (range "00" - "59"), SS = Second (range "00" - "60").

             FFFFFF = Fractional Second contains a fractional part of a second as small as 1 millionth of
             a second (range "000000" - "999999").

             &ZZXX is an optional suffix for offset from Coordinated Universal Time (UTC), where & = "+"
             or "-", and ZZ = Hours and XX = Minutes of offset.

             The year, month, and day shall be interpreted as a date of the Gregorian calendar system.

             A 24-hour clock is used. Midnight shall be represented by only "0000" since "2400" would
             violate the hour range.

             The Fractional Second component, if present, shall contain 1 to 6 digits. If Fractional
             Second is unspecified the preceding "." shall not be included. The offset suffix, if present,
             shall contain 4 digits. The string may be padded with trailing SPACE characters. Leading
             and embedded spaces are not allowed.

             A component that is omitted from the string is termed a null component. Trailing null
             components of Date Time indicate that the value is not precise to the precision of those
             components. The YYYY component shall not be null. Non-trailing null components are prohibited.
             The optional suffix is not considered as a component.

             A Date Time value without the optional suffix is interpreted to be in the local time zone
             of the application creating the Data Element, unless explicitly specified by the Timezone
             Offset From UTC (0008,0201).

             UTC offsets are calculated as "local time minus UTC". The offset for a Date Time value in
             UTC shall be +0000.

             */
        }


        public static void ValidateIS(string content)
        {
            /* "0" - "9", "+", "-" of Default Character Repertoire

            12 bytes maximum

            A string of characters representing an Integer in base-10 (decimal), shall contain only the
            characters 0 - 9, with an optional leading "+" or "-". It may be padded with leading and/or
            trailing spaces. Embedded spaces are not allowed.

            The integer, n, represented shall be in the range:

            -2^31 <= n <= (2^31-1).
             */

             if (string.IsNullOrEmpty(content))
            {
                // empty value allowed
                return;
            }
       
            // leading or trailing spaces allowed
            content = content.Trim(' ');

            if (!Regex.IsMatch(content, @"^[+-]?\d+$"))
            {
                throw new DicomValidationException(content, DicomVR.IS, "value is not an integer string");
            }

            if (!Int32.TryParse(content, out _))
            {
                throw new DicomValidationException(content, DicomVR.IS, "value too large to fit 32 bit integer");
            }
        }


        public static void ValidateLO(string content)
        {
            /*
             * Default Character Repertoire and/or as defined by (0008,0005).
             *
             * 64 chars maximum (see Note in Section 6.2)
             *
             * A character string that may be padded with leading and/or trailing spaces. The character
             * code 5CH (the BACKSLASH "\" in ISO-IR 6) shall not be present, as it is used as the
             * delimiter between values in multiple valued data elements. The string shall not have
             * Control Characters except for ESC.
             */

            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            if (content.Length > 64)
            {
                throw new DicomValidationException(content, DicomVR.LO, "value exceeds maximum length of 64 characters");
            }

            if (content.Contains("\\") || content.ToCharArray().Any(Char.IsControl))
            {
                throw new DicomValidationException(content, DicomVR.LO, "value contains invalid character");
            }
        }


        public static void ValidateLT(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            /*
            A character string that may contain one or more paragraphs. It may contain the Graphic
            Character set and the Control Characters, CR, LF, FF, and ESC. It may be padded with
            trailing spaces, which may be ignored, but leading spaces are considered to be significant.
            Data Elements with this VR shall not be multi - valued and therefore character code
            5CH(the BACKSLASH "\" in ISO-IR 6) may be used.
 
            Default Character Repertoire and / or as defined by(0008, 0005).
 
            10240 chars maximum(see Note in Section 6.2) */

            if (content.Length > 10240)
            {
                throw new DicomValidationException(content, DicomVR.LT, "value exceeds maximum length of 10240 characters");
            }
        }


        public static void ValidatePN(string content)
        {
            /*
            A character string encoded using a 5 component convention. The character code 5CH (the
            BACKSLASH "\" in ISO-IR 6) shall not be present, as it is used as the delimiter between
            values in multiple valued data elements. The string may be padded with trailing spaces. For
            human use, the five components in their order of occurrence are: family name complex, given
            name complex, middle name, name prefix, name suffix.

            Note
            HL7 prohibits leading spaces within a component; DICOM allows leading and trailing spaces
            and considers them insignificant.

             Any of the five components may be an empty string. The component delimiter shall be the
             caret "^" character (5EH). Delimiters are required for interior null components. Trailing
             null components and their delimiters may be omitted. Multiple entries are permitted in
             each component and are encoded as natural text strings, in the format preferred by the
             named person.

             For veterinary use, the first two of the five components in their order of occurrence
             are: responsible party family name or responsible organization name, patient name. The
             remaining components are not used and shall not be present.

             This group of five components is referred to as a Person Name component group.

             For the purpose of writing names in ideographic characters and in phonetic characters,
             up to 3 groups of components (see Annexes H, I and J) may be used. The delimiter for
             component groups shall be the equals character "=" (3DH). The three component groups
             of components in their order of occurrence are: an alphabetic representation, an
             ideographic representation, and a phonetic representation.

             Any component group may be absent, including the first component group. In this case,
             the person name may start with one or more "=" delimiters. Delimiters are required for
             interior null component groups. Trailing null component groups and their delimiters may
             be omitted.

             Precise semantics are defined for each component group. See Section 6.2.1.2.

             For examples and notes, see Section 6.2.1.1.

             Default Character Repertoire and/or as defined by (0008,0005) excluding Control Characters
             LF, FF, and CR but allowing Control Character ESC.

             64 chars maximum per component group */
            if (string.IsNullOrEmpty(content))
            {
                // empty values allowed
                return;
            }
            var groups = content.Split('=');
            if (groups.Length > 3)
            {
                throw new DicomValidationException(content, DicomVR.PN, "value contains too many groups");
            }
            foreach(var group in groups)
            {
                if (group.Length > 64)
                {
                    throw new DicomValidationException(content, DicomVR.PN, "value exceeds maximum length of 64 characters");
                }
                if (group.ToCharArray().Any(Char.IsControl))
                {
                    throw new DicomValidationException(content, DicomVR.PN, "value contains invalid control character");
                }
            }
            var groupcomponents = groups.Select(group => group.Split('^').Length);
            if (groupcomponents.Any(l => l > 5))
            {
                throw new DicomValidationException(content, DicomVR.PN, "value contains too many components");
            }
        }


        public static void ValidateSH(string content)
        {
            /*
             A character string that may be padded with leading and/or trailing spaces. The character
             code 05CH (the BACKSLASH "\" in ISO-IR 6) shall not be present, as it is used as the
             delimiter between values for multiple data elements. The string shall not have Control
             Characters except ESC.

             Default Character Repertoire and/or as defined by (0008,0005).

             16 chars maximum (see Note in Section 6.2)*/

            if (content.Contains("\\") || content.ToCharArray().Any(Char.IsControl))
            {
                throw new DicomValidationException(content, DicomVR.SH, "value contains invalid character");
            }

            if (content.Length > 16)
            {
                throw new DicomValidationException(content, DicomVR.SH, "value exceeds maximum length of 16 characters");
            }
        }


        public static void ValidateST(string content)
        {
            /*
             A character string that may contain one or more paragraphs. It may contain the Graphic
             Character set and the Control Characters, CR, LF, FF, and ESC. It may be padded with
             trailing spaces, which may be ignored, but leading spaces are considered to be significant.
             Data Elements with this VR shall not be multi-valued and therefore character code
             5CH (the BACKSLASH "\" in ISO-IR 6) may be used.

             Default Character Repertoire and/or as defined by (0008,0005).

             1024 chars maximum (see Note in Section 6.2) */

            if (content?.Length > 1024)
            {
                throw new DicomValidationException(content, DicomVR.ST, "value exceeds maximum length of 1024 characters");
            }
        }


        public static void ValidateTM (string content)
        {
            /*
             A string of characters of the format HHMMSS.FFFFFF; where HH contains hours (range "00" - "23"),
             MM contains minutes (range "00" - "59"), SS contains seconds (range "00" - "60"), and FFFFFF
             contains a fractional part of a second as small as 1 millionth of a second (range "000000" -
             "999999"). A 24-hour clock is used. Midnight shall be represented by only "0000" since "2400"
             would violate the hour range. The string may be padded with trailing spaces. Leading and
             embedded spaces are not allowed.

             One or more of the components MM, SS, or FFFFFF may be unspecified as long as every component
             to the right of an unspecified component is also unspecified, which indicates that the value
             is not precise to the precision of those unspecified components.

             The FFFFFF component, if present, shall contain 1 to 6 digits. If FFFFFF is unspecified the
             preceding "." shall not be included.

             Examples:

             "070907.0705 " represents a time of 7 hours, 9 minutes and 7.0705 seconds.

             "1010" represents a time of 10 hours, and 10 minutes.

             "021 " is an invalid value.

             The SS component may have a value of 60 only for a leap second.

             "0"-"9", "." and the SPACE character of Default Character Repertoire

             In the context of a Query with range matching (see PS3.4), the character "-" is allowed.

             16 bytes maximum
             In the context of a Query with range matching (see PS3.4), the length is 28 bytes maximum.
             */

            var queryComponents = content.Split('-');
            if (queryComponents.Count() > 2)
            {
                throw new DicomValidationException(content, DicomVR.TM, "value contains too many range separators '-'");
            }

            foreach (var component in queryComponents)
            {
                // trailling spaces are allowed
                var cont = component.TrimEnd(' ');
                if (string.IsNullOrEmpty(cont))
                {
                    continue;
                }


                if (!Regex.IsMatch(cont, @"^\d{2}$|^\d{4}$|^\d{6}$|^\d{6}\.\d{1,6}$"))
                {
                    throw new DicomValidationException(content, DicomVR.TM, "value does not mach pattern HH or HHMM or HHMMSS or HHMMSS.F{1-6}");
                }
                // validate the components, now that we know that there are only digits
                if (cont.Length >= 2)
                {
                    var hh = cont.Substring(0, 2);
                    if (int.Parse(hh) > 23)
                    {
                        throw new DicomValidationException(content, DicomVR.TM, "hour component exceeds 23");
                    }
                }
                if (cont.Length >= 4)
                {
                    var mm = cont.Substring(2, 2);
                    if (int.Parse(mm) > 59)
                    {
                        throw new DicomValidationException(content, DicomVR.TM, "minutes component exceeds 59");
                    }
                }
                if (cont.Length >= 6)
                {
                    var ss = cont.Substring(4, 2);
                    if (int.Parse(ss) > 60)
                    {
                        throw new DicomValidationException(content, DicomVR.TM, "seconds component exceeds 60");
                    }
                }
            }
        }


        public static void ValidateUI(string content)
        {
            /*
             The UID is a series of numeric components separated by the period "." character.
             If a Value Field containing one or more UIDs is an odd number of bytes in length,
             the Value Field shall be padded with a single trailing NULL (00H)
             character to ensure that the Value Field is an even number of bytes in length.
             See Section 9 and Annex B for a complete specification and examples.

             "0"-"9", "." of Default Character Repertoire

             64 bytes maximum */

            // trailling spaces are allowed
            content = content.TrimEnd(' ');
            if (string.IsNullOrEmpty(content))
            {
                // empty values are valid
                return;
            }
            if (content.Length > 64)
            {
                throw new DicomValidationException(content, DicomVR.UI, "value exceeds maximum length of 64 characters");
            }
            if (!Regex.IsMatch(content, "^[0-9\\.]*$"))
            {
                throw new DicomValidationException(content, DicomVR.UI, "value contains invalid characters other than '0'-'9' and '.'");
            }
            if (content.StartsWith("0") || Regex.IsMatch(content, @"[.]0\d"))
            {
                throw new DicomValidationException(content, DicomVR.UI, "components must not have leading zeros");
            }
        }


    }

}
