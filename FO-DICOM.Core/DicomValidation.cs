// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text.RegularExpressions;

namespace FellowOakDicom
{

    public static partial class DicomValidation
    {
        internal static bool PerformValidation { get; set; } = true;

        [GeneratedRegex(@"^\s*$")]
        private static partial Regex AllWhitespaceRegex();

        [GeneratedRegex(@"^\d\d\d[DWMY]$")]
        private static partial Regex AgeStringRegex();

        [GeneratedRegex("^[A-Z0-9_ ]*$")]
        private static partial Regex CodeStringRegex();

        [GeneratedRegex(@"^\d{8}$")]
        private static partial Regex EightDigitsRegex();

        [GeneratedRegex(@"^\d{4}$")]
        private static partial Regex FourDigitsRegex();

        [GeneratedRegex(@"^[+-]?((\d+(\.\d*)?)|(\.\d+))([eE][-+]?\d+)?$")]
        private static partial Regex DecimalStringRegex();

        [GeneratedRegex(@"^\d{4}$|^\d{6}$|^\d{8}$|^\d{10}$|^\d{12}$|^\d{14}$|^\d{14}\.\d{1,6}$")]
        private static partial Regex DateTimeStringRegex();

        [GeneratedRegex(@"^[+-]?\d+$")]
        private static partial Regex IntegerStringRegex();

        [GeneratedRegex(@"^\d{2}$|^\d{4}$|^\d{6}$|^\d{6}\.\d{1,6}$")]
        private static partial Regex TimeStringRegex();

        [GeneratedRegex(@"^[0-9.]*$")]
        private static partial Regex UidCharsRegex();

        [GeneratedRegex(@"\.0\d")]
        private static partial Regex UidLeadingZeroComponentRegex();

        [GeneratedRegex(@"^\.|\.\.|\.$")]
        private static partial Regex UidEmptyComponentRegex();

        [GeneratedRegex(@"^[+-]\d{4}$")]
        private static partial Regex TimezoneOffsetRegex();

        public static void ValidateAE(string content)
        {
            HandleNullValue(content, DicomVR.AE);

            // may not be longer than 16 characters
            if (content.Length > 16)
            {
                throw new DicomValidationException(content, DicomVR.AE, "value exceeds maximum length of 16 characters");
            }
            // may not contain only of spaces
            if (AllWhitespaceRegex().IsMatch(content))
            {
                throw new DicomValidationException(content, DicomVR.AE, "value may not consist only of spaces");
            }
            // Default Character Repertoire excluding character code 5CH (the BACKSLASH "\" in ISO-IR 6), and control characters LF, FF, CR and ESC.
            if (content.Contains('\\') || ContainsControlChar(content))
            {
                throw new DicomValidationException(content, DicomVR.AE, "value contains invalid control character");
            }
        }


        public static void ValidateAS(string content)
        {
            HandleNullValue(content, DicomVR.AS);
            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            // 4 charachters fixed
            // one of the following formats -- nnnD, nnnW, nnnM, nnnY; where nnn shall contain the number of days for D, weeks for W, months for M, or years for Y.
            if (!AgeStringRegex().IsMatch(content))
            {
                throw new DicomValidationException(content, DicomVR.AS, "value does not have pattern 000[DWMY]");
            }
        }


        public static void ValidateCS(string content)
        {
            HandleNullValue(content, DicomVR.CS);

            // 16 bytes maximum
            if (content.Length > 16)
            {
                throw new DicomValidationException(content, DicomVR.CS, "value exceeds maximum length of 16 characters");
            }
            // Uppercase characters, "0" - "9", the SPACE character, and underscore "_", of the Default Character Repertoire
            if (!CodeStringRegex().IsMatch(content))
            {
                throw new DicomValidationException(content, DicomVR.CS, "value contains invalid character. Only uppercase character, digits, space and underscore alre allowed");
            }
        }


        public static void ValidateDA(string content)
        {
            HandleNullValue(content, DicomVR.DA);

            /*
            A string of characters of the format YYYYMMDD; where YYYY shall contain year, MM shall contain the
            month, and DD shall contain the day, interpreted as a date of the Gregorian calendar system.
            */

            string[] dateComponents = content.Split('-');

            if (dateComponents.Length > 2)
            {
                throw new DicomValidationException(content, DicomVR.DA, "value contains too many range separators '-'");
            }

            foreach (var component in dateComponents)
            {
                // Trailling spaces are allowed
                var trimmedComponent = component.TrimEnd(' ');

                if (string.IsNullOrEmpty(trimmedComponent))
                {
                    continue;
                }

                // Check Character Repertoire
                if (!EightDigitsRegex().IsMatch(trimmedComponent))
                {
                    throw new DicomValidationException(content, DicomVR.DA, "one of the date values does not match the pattern YYYYMMDD");
                }

                // The date is in the numeric format, validate the month and day components
                var monthSpan = trimmedComponent.AsSpan(4, 2);
                var daySpan = trimmedComponent.AsSpan(6, 2);

                if (int.Parse(monthSpan) > 12)
                {
                    throw new DicomValidationException(content, DicomVR.DA, "month component exceeds the value 12");
                }

                if (int.Parse(daySpan) > 31)
                {
                    throw new DicomValidationException(content, DicomVR.DA, "day component exceeds the value 31");
                }
            }
        }


        public static void ValidateDS(string content)
        {
            HandleNullValue(content, DicomVR.DS);

            // 16 bytes maximum
            if (content.Length > 16)
            {
                throw new DicomValidationException(content, DicomVR.DS, "value exceeds maximum length of 16 characters");
            }

            content = content.Trim();
            if (!DecimalStringRegex().IsMatch(content))
            {
                throw new DicomValidationException(content, DicomVR.DS, "value is no decimal string");
            }
        }


        public static void ValidateDT(string content)
        {
            HandleNullValue(content, DicomVR.DT);

            if (content.Contains("-0000"))
            {
                throw new DicomValidationException(content, DicomVR.DT, "negative UTC hours component with value -0000 is not allowed");
            }

            if (content.Trim().Equals("-"))
            {
                throw new DicomValidationException(content, DicomVR.DT, "both dateTime components in range cannot be empty");
            }

            string[] dateTimeComponents = content.Split('-');

            // DateTime may contain more than two '-' characters because of the negative UTC suffixes
            if (dateTimeComponents.Length > 4)
            {
                throw new DicomValidationException(content, DicomVR.DT, "value contains too many range separators '-'");
            }

            if (dateTimeComponents.Length == 4)
            {
                // Join 4 range separated components (X,Y,X,Y) into 2 range components with negative UTC (X-Y,X-Y)
                string firstComponent = dateTimeComponents[0] + "-" + dateTimeComponents[1];
                string secondComponent = dateTimeComponents[2] + "-" + dateTimeComponents[3];

                dateTimeComponents = new string[2] { firstComponent, secondComponent };
            }
            else if (dateTimeComponents.Length == 3)
            {
                // Join 3 range separated components (X, Y, Z) into 2 range components with negative UTC (X-Y,Z) or (X,Y-Z)
                string firstComponent;
                string secondComponent;
                if (FourDigitsRegex().IsMatch(dateTimeComponents[1]) && int.Parse(dateTimeComponents[1]) <= 1200)
                {
                    // Second component is UTC -> (X-Y,Z)
                    firstComponent = dateTimeComponents[0] + "-" + dateTimeComponents[1];
                    secondComponent = dateTimeComponents[2];
                }
                else if (FourDigitsRegex().IsMatch(dateTimeComponents[2]) && int.Parse(dateTimeComponents[2]) <= 1200)
                {
                    // Third component is UTC -> (X,Y-Z)
                    firstComponent = dateTimeComponents[0];
                    secondComponent = dateTimeComponents[1] + "-" + dateTimeComponents[2];
                }
                else
                {
                    throw new DicomValidationException(content, DicomVR.DT, "value is in invalid range format");
                }

                dateTimeComponents = new string[2] { firstComponent, secondComponent };
            }
            else if (dateTimeComponents.Length == 2)
            {
                // Join 2 range separated components (X,Y) into one (X-Y) if Y is negative UTC (0000-1200)
                if (FourDigitsRegex().IsMatch(dateTimeComponents[1]) && int.Parse(dateTimeComponents[1]) <= 1200)
                {
                    string newComponent = dateTimeComponents[0] + "-" + dateTimeComponents[1];
                    dateTimeComponents = new string[1] { newComponent };
                }
            }

            foreach (var component in dateTimeComponents)
            {
                // Trailling spaces are allowed
                var trimmedComponent = component.TrimEnd(' ');

                if (string.IsNullOrEmpty(trimmedComponent))
                {
                    continue;
                }

                // Split by optional suffix for UTC +/-ZZXX
                string[] splittedDateTime = trimmedComponent.Split(new char[] { '+', '-' }, StringSplitOptions.None);
                if (splittedDateTime.Length > 2)
                {
                    throw new DicomValidationException(content, DicomVR.DT, "value contains too many UTC separators '+' or '-'");
                }
                else if (splittedDateTime.Length == 2)
                {
                    string utcSuffixString = splittedDateTime[1];

                    // If optional UTC suffix is present
                    if (!FourDigitsRegex().IsMatch(utcSuffixString))
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "value does not match the UTC pattern &ZZXX");
                    }

                    bool isPositiveOffset = trimmedComponent.Contains('+');
                    var hoursValue = int.Parse(utcSuffixString.AsSpan(0, 2));
                    var minutesValue = int.Parse(utcSuffixString.AsSpan(2, 2));

                    if (isPositiveOffset && hoursValue > 14)
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "positive UTC hours component exceeds 14 (allowed range is -1200 to +1400)");
                    }
                    else if (!isPositiveOffset && hoursValue > 12)
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "negative UTC hours component exceeds 12 (allowed range is -1200 to +1400)");
                    }

                    if (minutesValue > 59)
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "UTC minutes component exceeds 59");
                    }
                }

                string dateTimeString = splittedDateTime[0];

                // Check Character Repertoire
                if (!DateTimeStringRegex().IsMatch(dateTimeString))
                {
                    throw new DicomValidationException(content, DicomVR.DT, "value does not mach pattern YYYY[MM[DD[HH[MM[SS[.F{1-6}]]]]]]");
                }

                // The date is in the right numeric format, validate the components
                if (dateTimeString.Length >= 14)
                {
                    if (int.Parse(dateTimeString.AsSpan(12, 2)) > 60)
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "seconds component exceeds 60");
                    }
                }

                if (dateTimeString.Length >= 12)
                {
                    if (int.Parse(dateTimeString.AsSpan(10, 2)) > 59)
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "minutes component exceeds 59");
                    }
                }

                if (dateTimeString.Length >= 10)
                {
                    if (int.Parse(dateTimeString.AsSpan(8, 2)) > 23)
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "hours component exceeds 23");
                    }
                }

                if (dateTimeString.Length >= 8)
                {
                    var dayValue = int.Parse(dateTimeString.AsSpan(6, 2));
                    if (dayValue > 31)
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "day component exceeds 31");
                    }
                    else if (dayValue == 0)
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "day component cannot be 0");
                    }
                }

                if (dateTimeString.Length >= 6)
                {
                    var monthValue = int.Parse(dateTimeString.AsSpan(4, 2));
                    if (monthValue > 12)
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "month component exceeds 12");
                    }
                    else if (monthValue == 0)
                    {
                        throw new DicomValidationException(content, DicomVR.DT, "month component cannot be 0");
                    }
                }

                if (dateTimeString.Length > 0 && dateTimeString.Length < 4)
                {
                    throw new DicomValidationException(content, DicomVR.DT, "year component is too short and not in the correct YYYY format");
                }
            }
        }


        public static void ValidateIS(string content)
        {
            HandleNullValue(content, DicomVR.IS);

            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            // leading or trailing spaces allowed
            content = content.Trim(' ');

            if (!IntegerStringRegex().IsMatch(content))
            {
                throw new DicomValidationException(content, DicomVR.IS, "value is not an integer string");
            }

            if (!int.TryParse(content, out _))
            {
                throw new DicomValidationException(content, DicomVR.IS, "value too large to fit 32 bit integer");
            }
        }


        public static void ValidateLO(string content)
        {
            HandleNullValue(content, DicomVR.LO);

            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            if (content.Length > 64)
            {
                throw new DicomValidationException(content, DicomVR.LO, "value exceeds maximum length of 64 characters");
            }

            if (content.Contains('\\') || ContainsControlCharExceptEsc(content))
            {
                throw new DicomValidationException(content, DicomVR.LO, "value contains invalid character");
            }
        }


        public static void ValidateLT(string content)
        {
            HandleNullValue(content, DicomVR.LT);
            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            if (content.Length > 10240)
            {
                throw new DicomValidationException(content, DicomVR.LT, "value exceeds maximum length of 10240 characters");
            }
        }


        public static void ValidatePN(string content)
        {
            HandleNullValue(content, DicomVR.PN);
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            var groups = content.Split('=');
            if (groups.Length > 3)
            {
                throw new DicomValidationException(content, DicomVR.PN, "value contains too many groups");
            }
            foreach (var group in groups)
            {
                if (group.Length > 64)
                {
                    throw new DicomValidationException(content, DicomVR.PN, "value exceeds maximum length of 64 characters");
                }
                if (ContainsControlCharExceptEsc(group))
                {
                    throw new DicomValidationException(content, DicomVR.PN, "value contains invalid control character");
                }
                // count('^') + 1 > 5  ==>  count('^') >= 5
                if (group.AsSpan().Count('^') >= 5)
                {
                    throw new DicomValidationException(content, DicomVR.PN, "value contains too many components");
                }
            }
        }


        public static void ValidateSH(string content)
        {
            HandleNullValue(content, DicomVR.SH);

            if (content.Contains('\\') || ContainsControlCharExceptEsc(content))
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
            HandleNullValue(content, DicomVR.ST);

            if (content?.Length > 1024)
            {
                throw new DicomValidationException(content, DicomVR.ST, "value exceeds maximum length of 1024 characters");
            }
        }


        public static void ValidateTM(string content)
        {
            HandleNullValue(content, DicomVR.TM);

            string[] queryComponents = content.Split('-');
            if (queryComponents.Length > 2)
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


                if (!TimeStringRegex().IsMatch(cont))
                {
                    throw new DicomValidationException(content, DicomVR.TM, "value does not mach pattern HH or HHMM or HHMMSS or HHMMSS.F{1-6}");
                }
                // validate the components, now that we know that there are only digits
                if (cont.Length >= 2)
                {
                    if (int.Parse(cont.AsSpan(0, 2)) > 23)
                    {
                        throw new DicomValidationException(content, DicomVR.TM, "hour component exceeds 23");
                    }
                }
                if (cont.Length >= 4)
                {
                    if (int.Parse(cont.AsSpan(2, 2)) > 59)
                    {
                        throw new DicomValidationException(content, DicomVR.TM, "minutes component exceeds 59");
                    }
                }
                if (cont.Length >= 6)
                {
                    if (int.Parse(cont.AsSpan(4, 2)) > 60)
                    {
                        throw new DicomValidationException(content, DicomVR.TM, "seconds component exceeds 60");
                    }
                }
            }
        }


        public static void ValidateUI(string content)
        {
            HandleNullValue(content, DicomVR.UI);

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
            if (!UidCharsRegex().IsMatch(content))
            {
                throw new DicomValidationException(content, DicomVR.UI, "value contains invalid characters other than '0'-'9' and '.'");
            }
            if (content[0] == '0' || UidLeadingZeroComponentRegex().IsMatch(content))
            {
                throw new DicomValidationException(content, DicomVR.UI, "components must not have leading zeros");
            }
            if (UidEmptyComponentRegex().IsMatch(content))
            {
                throw new DicomValidationException(content, DicomVR.UI, "a component can not be empty");
            }
        }

        public static void ValidateTimezoneOffset(string content)
        {
            // http://dicom.nema.org/medical/dicom/current/output/chtml/part03/sect_C.12.html#sect_C.12.1.1.8
            if (!IsValidTimezoneOffset(content))
            {
                throw new DicomValidationException(content, DicomVR.SH, "Invalid format for TimezoneOffsetFromUTC");
            }
        }

        public static bool IsValidTimezoneOffset(string content) =>
            // http://dicom.nema.org/medical/dicom/current/output/chtml/part03/sect_C.12.html#sect_C.12.1.1.8
            TimezoneOffsetRegex().IsMatch(content);


        private static bool ContainsControlChar(string s)
        {
            foreach (var c in s)
            {
                if (char.IsControl(c)) return true;
            }
            return false;
        }

        private static bool ContainsControlCharExceptEsc(string s)
        {
            foreach (var c in s)
            {
                if (char.IsControl(c) && c != '') return true;
            }
            return false;
        }

        private static void HandleNullValue(string content, DicomVR vr)
        {
            if (content == null)
            {
                throw new DicomValidationException(null, vr, "value is null");
            }
        }

    }


    public static class DicomValidationBuilderExtension
    {

        /// <summary>
        /// Enables that the content of DicomItems shall be validated as soon as they are added to the DicomDataset.
        /// This enables fo-dicom to do validation globally instead of the datasets, where validation is disabled explicit.
        /// </summary>
        public static DicomSetupBuilder DoValidation(this DicomSetupBuilder builder)
        {
            DicomValidation.PerformValidation = true;
            return builder;
        }

        /// <summary>
        /// Disables that the content of DicomItems shall be validated as soon as they are added to the DicomDataset.
        /// This disables validation gobally instead of turning validation off for every single DicomDataset.
        /// </summary>
        public static DicomSetupBuilder SkipValidation(this DicomSetupBuilder builder)
        {
            DicomValidation.PerformValidation = false;
            return builder;
        }

    }

}
