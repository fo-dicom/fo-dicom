// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom
{

    /// <summary>Code String for DICOM Value Representation</summary>
    public static class DicomVRCode
    {
        public const string AE = "AE";
                                                    
        /// <summary>Age String</summary>
        public const string AS = "AS";

        /// <summary>Attribute Tag</summary>
        public const string AT = "AT";

        /// <summary>Code String</summary>
        public const string CS = "CS";

        /// <summary>Date</summary>
        public const string DA = "DA";

        /// <summary>Decimal String</summary>
        public const string DS = "DS";

        /// <summary>Date Time</summary>
        public const string DT = "DT";

        /// <summary>Floating Point Double</summary>
        public const string FD = "FD";

        /// <summary>Floating Point Single</summary>
        public const string FL = "FL";

        /// <summary>Integer String</summary>
        public const string IS = "IS";

        /// <summary>Long String</summary>
        public const string LO = "LO";
        /// <summary>Long Text</summary>
        public const string LT = "LT";

        /// <summary>Other Byte</summary>
        public const string OB = "OB";

        /// <summary>Other Double</summary>
        public const string OD = "OD";

        /// <summary>Other Float</summary>
        public const string OF = "OF";

        /// <summary>Other Long</summary>
        public const string OL = "OL";

        /// <summary>Other Very Long</summary>
        public const string OV = nameof(OV);

        /// <summary>Other Word</summary>
        public const string OW = "OW";

        /// <summary>Person Name</summary>
        public const string PN = "PN";

        /// <summary>Short String</summary>
        public const string SH = "SH";

        /// <summary>Signed Long</summary>
        public const string SL = "SL";

        /// <summary>Sequence of Items</summary>
        public const string SQ = "SQ";

        /// <summary>Signed Short</summary>
        public const string SS = "SS";

        /// <summary>Short Text</summary>
        public const string ST = "ST";

        /// <summary>Signed Very Long</summary>
        public const string SV = nameof(SV);

        /// <summary>Time</summary>
        public const string TM = "TM";

        /// <summary>Unlimited Characters</summary>
        public const string UC = "UC";

        /// <summary>Unique Identifier</summary>
        public const string UI = "UI";

        /// <summary>Unsigned Long</summary>
        public const string UL = "UL";

        /// <summary>Unknown</summary>
        public const string UN = "UN";

        /// <summary>Universal Resource Identifier or Universal Resource Locator (URI/URL)</summary>
        public const string UR = "UR";

        /// <summary>Unsigned Short</summary>
        public const string US = "US";

        /// <summary>Unlimited Text</summary>
        public const string UT = "UT";

        /// <summary>Unsigned Very Long</summary>
        public const string UV = nameof(UV);
    }
}
