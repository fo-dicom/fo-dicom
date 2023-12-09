// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom
{

    /// <summary>DICOM Value Representation</summary>
    public sealed class DicomVR
    {
        private const byte PadZero = 0x00;

        private const byte PadSpace = 0x20;

        private DicomVR()
        {
        }

        /// <summary>Code used to represent VR.</summary>
        public string Code { get; private set; }

        /// <summary>Descriptive name of VR.</summary>
        public string Name { get; private set; }

        /// <summary>Value is a string.</summary>
        public bool IsString { get; private set; }

        /// <summary>String is encoded using the specified character set.</summary>
        public bool IsStringEncoded { get; private set; }

        /// <summary>Length field of value is a 16-bit short integer.</summary>
        public bool Is16bitLength { get; private set; }

        /// <summary>Value can contain multiple items.</summary>
        public bool IsMultiValue { get; private set; }

        /// <summary>Byte value used to pad value to even length.</summary>
        public byte PaddingValue { get; private set; }

        /// <summary>Maximum length of a single value.</summary>
        public uint MaximumLength { get; private set; }

        /// <summary>Size of each individual value unit for fixed length value types.</summary>
        public int UnitSize { get; private set; }

        /// <summary>Number of bytes to swap when changing endian representation of value. Usually equal to the <see cref="UnitSize"/>.</summary>
        public int ByteSwap { get; private set; }

        /// <summary>Type used to represent VR value.</summary>
        public Type ValueType { get; private set; }

        private Action<string> StringValidator { get; set; }

        /// <summary> validates a string content. Throws Dicom
        /// 
        /// </summary>
        /// <param name="content"></param>
        public void ValidateString(string content)
        {
            StringValidator?.Invoke(content);
        }

        /// <summary>
        /// Gets a string representation of this VR.
        /// </summary>
        /// <returns>VR code</returns>
        public override string ToString()
        {
            return Code;
        }

        /// <summary>
        /// Get VR for given string value.
        /// </summary>
        /// <param name="vr">String representation of VR</param>
        /// <returns>VR</returns>
        public static DicomVR Parse(string vr)
        {
            DicomVR result = TryParse(vr, out bool valid);
            if (!valid) throw new DicomDataException($"Unknown VR: '{vr}'");
            return result;
        }

        /// <summary>
        /// Try to get VR for given string value.
        /// </summary>
        /// <param name="vr">String representation of VR</param>
        /// <param name="result">VR code</param>
        /// <returns>true if VR was successfully parsed, false otherwise</returns>
        public static bool TryParse(string vr, out DicomVR result)
        {
            result = TryParse(vr, out bool valid);
            return valid;
        }

        private static DicomVR TryParse(string vr, out bool valid)
        {
            valid = true;
            switch (vr)
            {
                case "NONE":
                    return DicomVR.NONE;
                case DicomVRCode.AE:
                    return DicomVR.AE;
                case DicomVRCode.AS:
                    return DicomVR.AS;
                case DicomVRCode.AT:
                    return DicomVR.AT;
                case DicomVRCode.CS:
                    return DicomVR.CS;
                case DicomVRCode.DA:
                    return DicomVR.DA;
                case DicomVRCode.DS:
                    return DicomVR.DS;
                case DicomVRCode.DT:
                    return DicomVR.DT;
                case DicomVRCode.FD:
                    return DicomVR.FD;
                case DicomVRCode.FL:
                    return DicomVR.FL;
                case DicomVRCode.IS:
                    return DicomVR.IS;
                case DicomVRCode.LO:
                    return DicomVR.LO;
                case DicomVRCode.LT:
                    return DicomVR.LT;
                case DicomVRCode.OB:
                    return DicomVR.OB;
                case DicomVRCode.OD:
                    return DicomVR.OD;
                case DicomVRCode.OF:
                    return DicomVR.OF;
                case DicomVRCode.OL:
                    return DicomVR.OL;
                case DicomVRCode.OW:
                    return DicomVR.OW;
                case DicomVRCode.OV:
                    return DicomVR.OV;
                case DicomVRCode.PN:
                    return DicomVR.PN;
                case DicomVRCode.SH:
                    return DicomVR.SH;
                case DicomVRCode.SL:
                    return DicomVR.SL;
                case DicomVRCode.SQ:
                    return DicomVR.SQ;
                case DicomVRCode.SS:
                    return DicomVR.SS;
                case DicomVRCode.ST:
                    return DicomVR.ST;
                case DicomVRCode.SV:
                    return DicomVR.SV;
                case DicomVRCode.TM:
                    return DicomVR.TM;
                case DicomVRCode.UC:
                    return DicomVR.UC;
                case DicomVRCode.UI:
                    return DicomVR.UI;
                case DicomVRCode.UL:
                    return DicomVR.UL;
                case DicomVRCode.UN:
                    return DicomVR.UN;
                case DicomVRCode.UR:
                    return DicomVR.UR;
                case DicomVRCode.US:
                    return DicomVR.US;
                case DicomVRCode.UT:
                    return DicomVR.UT;
                case DicomVRCode.UV:
                    return DicomVR.UV;
                default:
                    valid = false;
                    return DicomVR.NONE;
            }
        }

        /// <summary>
        /// Try to get VR for given UTF8 encoded string value.
        /// </summary>
        /// <param name="vr">UTF8 encoded byte array representation of VR</param>
        /// <param name="result">VR code</param>
        /// <returns>true if VR was successfully parsed, false otherwise</returns>
        public static bool TryParse(byte[] vr, out DicomVR result)
        {
            result = TryParse(vr, out bool valid);
            return valid;
        }

        private static DicomVR TryParse(byte[] vr, out bool valid)
        {
            if (vr == null || vr.Length < 2)
            {
                valid = false;
                return null;
            }
            
            valid = true;
            // This is a precompiled switch table
            switch (vr[0])
            {
                case 65:
                    switch (vr[1])
                    {
                        case 69: return AE;
                        case 83: return AS;
                        case 84: return AT;
                    }
                    break;
                case 67:
                    switch (vr[1])
                    {
                        case 83: return CS;
                    }
                    break;
                case 68:
                    switch (vr[1])
                    {
                        case 65: return DA;
                        case 83: return DS;
                        case 84: return DT;
                    }
                    break;
                case 70:
                    switch (vr[1])
                    {
                        case 68: return FD;
                        case 76: return FL;
                    }
                    break;
                case 73:
                    switch (vr[1])
                    {
                        case 83: return IS;
                    }
                    break;
                case 76:
                    switch (vr[1])
                    {
                        case 79: return LO;
                        case 84: return LT;
                    }
                    break;
                case 79:
                    switch (vr[1])
                    {
                        case 66: return OB;
                        case 68: return OD;
                        case 70: return OF;
                        case 76: return OL;
                        case 86: return OV;
                        case 87: return OW;
                    }
                    break;
                case 80:
                    switch (vr[1])
                    {
                        case 78: return PN;
                    }
                    break;
                case 83:
                    switch (vr[1])
                    {
                        case 72: return SH;
                        case 76: return SL;
                        case 81: return SQ;
                        case 83: return SS;
                        case 84: return ST;
                        case 86: return SV;
                    }
                    break;
                case 84:
                    switch (vr[1])
                    {
                        case 77: return TM;
                    }
                    break;
                case 85:
                    switch (vr[1])
                    {
                        case 67: return UC;
                        case 73: return UI;
                        case 76: return UL;
                        case 78: return UN;
                        case 82: return UR;
                        case 83: return US;
                        case 84: return UT;
                        case 86: return UV;
                    }
                    break;
            }
            
            valid = false;
            return null;
        }

        /// <summary>Implicit VR in Explicit VR context</summary>
        internal static readonly DicomVR Implicit = new DicomVR();

        /// <summary>No VR</summary>
        public static readonly DicomVR NONE = new DicomVR
                                                  {
                                                      Code = "NONE",
                                                      Name = "No Value Representation",
                                                      IsString = false,
                                                      IsStringEncoded = false,
                                                      Is16bitLength = false,
                                                      IsMultiValue = false,
                                                      PaddingValue = PadZero,
                                                      MaximumLength = 0,
                                                      UnitSize = 0,
                                                      ByteSwap = 0,
                                                      ValueType = typeof(object)
                                                  };

        /// <summary>Application Entity</summary>
        public static readonly DicomVR AE = new DicomVR
                                                {
                                                    Code = DicomVRCode.AE,
                                                    Name = "Application Entity",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 16,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string),
                                                    StringValidator = DicomValidation.ValidateAE
                                                };

        /// <summary>Age String</summary>
        public static readonly DicomVR AS = new DicomVR
                                                {
                                                    Code = DicomVRCode.AS,
                                                    Name = "Age String",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 4,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string),
                                                    StringValidator = DicomValidation.ValidateAS
                                                };

        /// <summary>Attribute Tag</summary>
        public static readonly DicomVR AT = new DicomVR
                                                {
                                                    Code = DicomVRCode.AT,
                                                    Name = "Attribute Tag",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 4,
                                                    UnitSize = 4,
                                                    ByteSwap = 2,
                                                    ValueType = typeof(DicomTag)
                                                };

        /// <summary>Code String</summary>
        public static readonly DicomVR CS = new DicomVR
                                                {
                                                    Code = DicomVRCode.CS,
                                                    Name = "Code String",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 16,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string),
                                                    StringValidator = DicomValidation.ValidateCS
                                                };

        /// <summary>Date</summary>
        public static readonly DicomVR DA = new DicomVR
                                                {
                                                    Code = DicomVRCode.DA,
                                                    Name = "Date",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 18,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(DateTime),
                                                    StringValidator = DicomValidation.ValidateDA
                                                };

        /// <summary>Decimal String</summary>
        public static readonly DicomVR DS = new DicomVR
                                                {
                                                    Code = DicomVRCode.DS,
                                                    Name = "Decimal String",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 16,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(decimal),
                                                    StringValidator = DicomValidation.ValidateDS
                                                };

        /// <summary>Date Time</summary>
        public static readonly DicomVR DT = new DicomVR
                                                {
                                                    Code = DicomVRCode.DT,
                                                    Name = "Date Time",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 54,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(DateTime),
                                                    StringValidator = DicomValidation.ValidateDT
                                                };

        /// <summary>Floating Point Double</summary>
        public static readonly DicomVR FD = new DicomVR
                                                {
                                                    Code = DicomVRCode.FD,
                                                    Name = "Floating Point Double",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 8,
                                                    UnitSize = 8,
                                                    ByteSwap = 8,
                                                    ValueType = typeof(double)
                                                };

        /// <summary>Floating Point Single</summary>
        public static readonly DicomVR FL = new DicomVR
                                                {
                                                    Code = DicomVRCode.FL,
                                                    Name = "Floating Point Single",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 4,
                                                    UnitSize = 4,
                                                    ByteSwap = 4,
                                                    ValueType = typeof(float)
                                                };

        /// <summary>Integer String</summary>
        public static readonly DicomVR IS = new DicomVR
                                                {
                                                    Code = DicomVRCode.IS,
                                                    Name = "Integer String",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 12,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(int),
                                                    StringValidator = DicomValidation.ValidateIS
                                                };

        /// <summary>Long String</summary>
        public static readonly DicomVR LO = new DicomVR
                                                {
                                                    Code = DicomVRCode.LO,
                                                    Name = "Long String",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = true,
                                                    //IsMultiValue = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 64,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string),
                                                    StringValidator = DicomValidation.ValidateLO
                                                };

        /// <summary>Long Text</summary>
        public static readonly DicomVR LT = new DicomVR
                                                {
                                                    Code = DicomVRCode.LT,
                                                    Name = "Long Text",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = true,
                                                    IsMultiValue = false,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 10240,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string),
                                                    StringValidator = DicomValidation.ValidateLT
                                                };

        /// <summary>Other Byte</summary>
        public static readonly DicomVR OB = new DicomVR
                                                {
                                                    Code = DicomVRCode.OB,
                                                    Name = "Other Byte",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(byte[])
                                                };

        /// <summary>Other Double</summary>
        public static readonly DicomVR OD = new DicomVR
                                                {
                                                    Code = DicomVRCode.OD,
                                                    Name = "Other Double",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 8,
                                                    ByteSwap = 8,
                                                    ValueType = typeof(double[])
                                                };

        /// <summary>Other Float</summary>
        public static readonly DicomVR OF = new DicomVR
                                                {
                                                    Code = DicomVRCode.OF,
                                                    Name = "Other Float",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 4,
                                                    ByteSwap = 4,
                                                    ValueType = typeof(float[])
                                                };

        /// <summary>Other Long</summary>
        public static readonly DicomVR OL = new DicomVR
                                                {
                                                    Code = DicomVRCode.OL,
                                                    Name = "Other Long",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 4,
                                                    ByteSwap = 4,
                                                    ValueType = typeof(uint[])
                                                };

        /// <summary>Other Word</summary>
        public static readonly DicomVR OW = new DicomVR
                                                {
                                                    Code = DicomVRCode.OW,
                                                    Name = "Other Word",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 2,
                                                    ByteSwap = 2,
                                                    ValueType = typeof(ushort[])
                                                };

        /// <summary>Other Very Long</summary>
        public static readonly DicomVR OV = new DicomVR
        {
            Code = DicomVRCode.OV,
            Name = "Other Very Long",
            IsString = false,
            IsStringEncoded = false,
            Is16bitLength = false,
            IsMultiValue = true,
            PaddingValue = PadZero,
            MaximumLength = 0,
            UnitSize = 8,
            ByteSwap = 8,
            ValueType = typeof(ulong[])
        };

        /// <summary>Person Name</summary>
        public static readonly DicomVR PN = new DicomVR
                                                {
                                                    Code = DicomVRCode.PN,
                                                    Name = "Person Name",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = true,
                                                    //IsMultiValue = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 64,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string),
                                                    StringValidator = DicomValidation.ValidatePN
                                                };

        /// <summary>Short String</summary>
        public static readonly DicomVR SH = new DicomVR
                                                {
                                                    Code = DicomVRCode.SH,
                                                    Name = "Short String",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = true,
                                                    //IsMultiValue = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 16,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string),
                                                    StringValidator = DicomValidation.ValidateSH
                                                };

        /// <summary>Signed Long</summary>
        public static readonly DicomVR SL = new DicomVR
                                                {
                                                    Code = DicomVRCode.SL,
                                                    Name = "Signed Long",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 4,
                                                    UnitSize = 4,
                                                    ByteSwap = 4,
                                                    ValueType = typeof(int)
                                                };

        /// <summary>Sequence of Items</summary>
        public static readonly DicomVR SQ = new DicomVR
                                                {
                                                    Code = DicomVRCode.SQ,
                                                    Name = "Sequence of Items",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = false,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 0,
                                                    UnitSize = 0,
                                                    ByteSwap = 0,
                                                    //ValueType = typeof(IList<DicomDataset>)
                                                };

        /// <summary>Signed Short</summary>
        public static readonly DicomVR SS = new DicomVR
                                                {
                                                    Code = DicomVRCode.SS,
                                                    Name = "Signed Short",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 2,
                                                    UnitSize = 2,
                                                    ByteSwap = 2,
                                                    ValueType = typeof(short)
                                                };

        /// <summary>Short Text</summary>
        public static readonly DicomVR ST = new DicomVR
                                                {
                                                    Code = DicomVRCode.ST,
                                                    Name = "Short Text",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = true,
                                                    IsMultiValue = false,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 1024,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string),
                                                    StringValidator = DicomValidation.ValidateST
                                                };

        /// <summary>Signed Very Long</summary>
        public static readonly DicomVR SV = new DicomVR
        {
            Code = DicomVRCode.SV,
            Name = "Signed Very Long",
            IsString = false,
            IsStringEncoded = false,
            Is16bitLength = false,
            IsMultiValue = true,
            PaddingValue = PadZero,
            MaximumLength = 8,
            UnitSize = 8,
            ByteSwap = 8,
            ValueType = typeof(long)
        };

        /// <summary>Time</summary>
        public static readonly DicomVR TM = new DicomVR
                                                {
                                                    Code = DicomVRCode.TM,
                                                    Name = "Time",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 16,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(DateTime),
                                                    StringValidator = DicomValidation.ValidateTM
                                                };

        /// <summary>Unlimited Characters</summary>
        public static readonly DicomVR UC = new DicomVR
                                                {
                                                    Code = DicomVRCode.UC,
                                                    Name = "Unlimited Characters",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 0,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Unique Identifier</summary>
        public static readonly DicomVR UI = new DicomVR
                                                {
                                                    Code = DicomVRCode.UI,
                                                    Name = "Unique Identifier",
                                                    IsString = true,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 64,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string),
                                                    StringValidator = DicomValidation.ValidateUI
                                                };

        /// <summary>Unsigned Long</summary>
        public static readonly DicomVR UL = new DicomVR
                                                {
                                                    Code = DicomVRCode.UL,
                                                    Name = "Unsigned Long",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 4,
                                                    UnitSize = 4,
                                                    ByteSwap = 4,
                                                    ValueType = typeof(uint)
                                                };

        /// <summary>Unknown</summary>
        public static readonly DicomVR UN = new DicomVR
                                                {
                                                    Code = DicomVRCode.UN,
                                                    Name = "Unknown",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = false,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 0,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(byte[])
                                                };

        /// <summary>Universal Resource Identifier or Universal Resource Locator (URI/URL)</summary>
        public static readonly DicomVR UR = new DicomVR
                                                {
                                                    Code = DicomVRCode.UR,
                                                    Name = "Universal Resource Identifier or Locator",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = false,
                                                    IsMultiValue = false,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 0,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Unsigned Short</summary>
        public static readonly DicomVR US = new DicomVR
                                                {
                                                    Code = DicomVRCode.US,
                                                    Name = "Unsigned Short",
                                                    IsString = false,
                                                    IsStringEncoded = false,
                                                    Is16bitLength = true,
                                                    IsMultiValue = true,
                                                    PaddingValue = PadZero,
                                                    MaximumLength = 2,
                                                    UnitSize = 2,
                                                    ByteSwap = 2,
                                                    ValueType = typeof(ushort)
                                                };

        /// <summary>Unlimited Text</summary>
        public static readonly DicomVR UT = new DicomVR
                                                {
                                                    Code = DicomVRCode.UT,
                                                    Name = "Unlimited Text",
                                                    IsString = true,
                                                    IsStringEncoded = true,
                                                    Is16bitLength = false,
                                                    IsMultiValue = false,
                                                    PaddingValue = PadSpace,
                                                    MaximumLength = 0,
                                                    UnitSize = 1,
                                                    ByteSwap = 1,
                                                    ValueType = typeof(string)
                                                };

        /// <summary>Unsigned Very Long</summary>
        public static readonly DicomVR UV = new DicomVR
        {
            Code = DicomVRCode.UV,
            Name = "Unsigned Very Long",
            IsString = false,
            IsStringEncoded = false,
            Is16bitLength = false,
            IsMultiValue = true,
            PaddingValue = PadZero,
            MaximumLength = 8,
            UnitSize = 8,
            ByteSwap = 8,
            ValueType = typeof(ulong)
        };
    }
}
