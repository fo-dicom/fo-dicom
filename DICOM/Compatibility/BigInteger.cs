// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*=============================================================================
**
** Struct: BigInteger
**
** Purpose: Represents an arbitrary precision integer.
**
=============================================================================*/

using System.Globalization;
using System.Text;

using Unity.IO.Compression;

namespace System.Numerics
{
    internal struct BigInteger
    {
        #region members supporting exposed properties

        private const uint kuMaskHighBit = unchecked((uint)int.MinValue);

        // For values int.MinValue < n <= int.MaxValue, the value is stored in sign
        // and _bits is null. For all other values, sign is +1 or -1 and the bits are in _bits
        internal int _sign;
        internal uint[] _bits;

        // We have to make a choice of how to represent int.MinValue. This is the one
        // value that fits in an int, but whose negation does not fit in an int.
        // We choose to use a large representation, so we're symmetric with respect to negation.
        private static readonly BigInteger s_bnMinInt = new BigInteger(-1, new[] { kuMaskHighBit });
        private static readonly BigInteger s_bnZeroInt = new BigInteger(0);
        private static readonly BigInteger s_bnMinusOneInt = new BigInteger(-1);

        #endregion

        #region constructors

        internal BigInteger(int value)
        {
            if (value == int.MinValue)
                this = s_bnMinInt;
            else {
                _sign = value;
                _bits = null;
            }
        }

        //
        // Create a BigInteger from a little-endian twos-complement byte array
        //
        internal BigInteger(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            int byteCount = value.Length;
            bool isNegative = byteCount > 0 && ((value[byteCount - 1] & 0x80) == 0x80);

            // Try to conserve space as much as possible by checking for wasted leading byte[] entries 
            while (byteCount > 0 && value[byteCount-1] == 0) byteCount--;

            if (byteCount == 0) 
            {
                // BigInteger.Zero
                _sign = 0;
                _bits = null;
                return;
            }


            if (byteCount <= 4)
            {
                if (isNegative)
                    _sign = unchecked((int)0xffffffff);
                else
                    _sign = 0;
                for (int i = byteCount - 1; i >= 0; i--)
                {
                    _sign <<= 8;
                    _sign |= value[i];
                }   
                _bits = null;

                if (_sign < 0 && !isNegative)
                {
                    // int32 overflow
                    // example: Int64 value 2362232011 (0xCB, 0xCC, 0xCC, 0x8C, 0x0)
                    // can be naively packed into 4 bytes (due to the leading 0x0)
                    // it overflows into the int32 sign bit
                    _bits = new uint[1];
                    _bits[0] = (uint)_sign;
                    _sign = +1; 
                }
                if (_sign == int.MinValue)
                    this = s_bnMinInt;
            }
            else
            {
                int unalignedBytes = byteCount % 4;
                int dwordCount = byteCount / 4 + (unalignedBytes == 0 ? 0 : 1);         
                bool isZero = true;
                uint[] val = new uint[dwordCount];

                // Copy all dwords, except but don't do the last one if it's not a full four bytes
                int curDword, curByte, byteInDword;
                curByte = 3;
                for (curDword = 0; curDword < dwordCount - (unalignedBytes == 0 ? 0 : 1); curDword++) {
                    byteInDword = 0;
                    while (byteInDword < 4) {
                        if (value[curByte] != 0x00) isZero = false;
                        val[curDword] <<= 8;
                        val[curDword] |= value[curByte];
                        curByte--;
                        byteInDword++;
                    }
                    curByte += 8;
                }

                // Copy the last dword specially if it's not aligned
                if (unalignedBytes != 0) {
                    if (isNegative) val[dwordCount - 1] = 0xffffffff;
                    for (curByte = byteCount - 1; curByte >= byteCount - unalignedBytes; curByte--) {
                        if (value[curByte] != 0x00) isZero = false;
                        val[curDword] <<= 8;
                        val[curDword] |= value[curByte];
                    }
                }

                if (isZero) {
                    this = s_bnZeroInt;
                }
                else if (isNegative) {
                    DangerousMakeTwosComplement(val); // mutates val

                    // pack _bits to remove any wasted space after the twos complement
                    int len = val.Length; 
                    while (len > 0 && val[len - 1] == 0)
                        len--;
                    if (len == 1 && ((int)(val[0])) > 0) {
                        if (val[0] == 1 /* abs(-1) */) {
                            this = s_bnMinusOneInt;
                        }
                        else if (val[0] == kuMaskHighBit /* abs(Int32.MinValue) */) {
                            this = s_bnMinInt;
                        }
                        else {
                            _sign = (-1) * ((int)val[0]);
                            _bits = null;
                        }
                    }
                    else if (len != val.Length) {
                        _sign = -1;
                        _bits = new uint[len];
                        Array.Copy(val, _bits, len);
                    }
                    else {
                        _sign = -1;
                        _bits = val;
                    }
                }
                else 
                {
                    _sign = +1;
                    _bits = val;
                }
            }        
        }

        internal BigInteger(int n, uint[] rgu)
        {
            _sign = n;
            _bits = rgu;
        }

        #endregion

        #region operators

        public static BigInteger operator -(BigInteger value)
        {
            value._sign = -value._sign;
            return value;
        }
        public static bool operator <(BigInteger left, Int64 right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(BigInteger left, Int64 right)
        {
            return left.CompareTo(right) > 0;
        }

        #endregion

        #region public methods
        public override string ToString()
        {
            return FormatBigInteger(this, null, NumberFormatInfo.CurrentInfo);
        }

        #endregion

        #region internal support methods

        // Return the value of this BigInteger as a little-endian twos-complement
        // byte array, using the fewest number of bytes possible. If the value is zero,
        // return an array of one byte whose element is 0x00.
        private byte[] ToByteArray()
        {
            if (_bits == null && _sign == 0)
                return new byte[] { 0 };

            // We could probably make this more efficient by eliminating one of the passes.
            // The current code does one pass for uint array -> byte array conversion,
            // and then another pass to remove unneeded bytes at the top of the array.
            uint[] dwords;
            byte highByte;

            if (_bits == null)
            {
                dwords = new uint[] { (uint)_sign };
                highByte = (byte)((_sign < 0) ? 0xff : 0x00);
            }
            else if (_sign == -1)
            {
                dwords = (uint[])_bits.Clone();
                DangerousMakeTwosComplement(dwords);  // mutates dwords
                highByte = 0xff;
            }
            else
            {
                dwords = _bits;
                highByte = 0x00;
            }

            byte[] bytes = new byte[checked(4 * dwords.Length)];
            int curByte = 0;
            uint dword;
            for (int i = 0; i < dwords.Length; i++)
            {
                dword = dwords[i];
                for (int j = 0; j < 4; j++)
                {
                    bytes[curByte++] = (byte)(dword & 0xff);
                    dword >>= 8;
                }
            }

            // find highest significant byte
            int msb;
            for (msb = bytes.Length - 1; msb > 0; msb--)
            {
                if (bytes[msb] != highByte) break;
            }
            // ensure high bit is 0 if positive, 1 if negative
            bool needExtraByte = (bytes[msb] & 0x80) != (highByte & 0x80);

            byte[] trimmedBytes = new byte[msb + 1 + (needExtraByte ? 1 : 0)];
            Array.Copy(bytes, trimmedBytes, msb + 1);

            if (needExtraByte) trimmedBytes[trimmedBytes.Length - 1] = highByte;
            return trimmedBytes;
        }

        private int CompareTo(long other)
        {
            if (_bits == null)
                return ((long)_sign).CompareTo(other);
            int cu;
            if ((_sign ^ other) < 0 || (cu = Length(_bits)) > 2)
                return _sign;
            ulong uu = other < 0 ? (ulong)-other : (ulong)other;
            ulong uuTmp = cu == 2 ? MakeUlong(_bits[1], _bits[0]) : _bits[0];
            return _sign * uuTmp.CompareTo(uu);
        }

        private static int Length(uint[] rgu)
        {
            var cu = rgu.Length;
            if (rgu[cu - 1] != 0)
                return cu;
            return cu - 1;
        }

        #endregion

        #region Support methods from NumericsHelpers class

        // Do an in-place twos complement of d and also return the result.
        // "Dangerous" because it causes a mutation and needs to be used
        // with care for immutable types
        private static uint[] DangerousMakeTwosComplement(uint[] d)
        {
            // first do complement and +1 as long as carry is needed
            int i = 0;
            uint v = 0;
            for (; i < d.Length; i++)
            {
                v = ~d[i] + 1;
                d[i] = v;
                if (v != 0) { i++; break; }
            }
            if (v != 0)
            {
                // now ones complement is sufficient
                for (; i < d.Length; i++)
                {
                    d[i] = ~d[i];
                }
            }
            else
            {
                //??? this is weird
                d = resize(d, d.Length + 1);
                d[d.Length - 1] = 1;
            }
            return d;
        }

        private static uint[] resize(uint[] v, int len)
        {
            if (v.Length == len) return v;
            uint[] ret = new uint[len];
            int n = System.Math.Min(v.Length, len);
            for (int i = 0; i < n; i++)
            {
                ret[i] = v[i];
            }
            return ret;
        }

        private const int kcbitUint = 32;

        private static ulong MakeUlong(uint uHi, uint uLo)
        {
            return ((ulong)uHi << kcbitUint) | uLo;
        }

        #endregion

        #region Support methods from BigNumber class

        //
        // internal static String FormatBigInteger(BigInteger value, String format, NumberFormatInfo info) {
        //
        private static string FormatBigInteger(BigInteger value, string format, NumberFormatInfo info)
        {
            int digits = 0;
            char fmt = ParseFormatSpecifier(format, out digits);
            if (fmt == 'x' || fmt == 'X')
                return FormatBigIntegerToHexString(value, fmt, digits, info);

            bool decimalFmt = (fmt == 'g' || fmt == 'G' || fmt == 'd' || fmt == 'D' || fmt == 'r' || fmt == 'R');

            if (!decimalFmt)
            {
                // Supports invariant formats only
                throw new FormatException(SR.GetString(SR.Format_InvalidFormatSpecifier));
            }

            if (value._bits == null)
            {
                if (fmt == 'g' || fmt == 'G' || fmt == 'r' || fmt == 'R')
                {
                    if (digits > 0)
                        format = string.Format(CultureInfo.InvariantCulture, "D{0}",
                            digits.ToString(CultureInfo.InvariantCulture));
                    else
                        format = "D";
                }
                return value._sign.ToString(format, info);
            }


            // First convert to base 10^9.
            const uint kuBase = 1000000000; // 10^9
            const int kcchBase = 9;

            int cuSrc = Length(value._bits);
            int cuMax;
            try
            {
                cuMax = checked(cuSrc * 10 / 9 + 2);
            }
            catch (OverflowException e)
            {
                throw new FormatException(SR.GetString(SR.Format_TooLarge), e);
            }
            uint[] rguDst = new uint[cuMax];
            int cuDst = 0;

            for (int iuSrc = cuSrc; --iuSrc >= 0;)
            {
                uint uCarry = value._bits[iuSrc];
                for (int iuDst = 0; iuDst < cuDst; iuDst++)
                {
                    ulong uuRes = MakeUlong(rguDst[iuDst], uCarry);
                    rguDst[iuDst] = (uint) (uuRes % kuBase);
                    uCarry = (uint) (uuRes / kuBase);
                }
                if (uCarry != 0)
                {
                    rguDst[cuDst++] = uCarry % kuBase;
                    uCarry /= kuBase;
                    if (uCarry != 0)
                        rguDst[cuDst++] = uCarry;
                }
            }

            int cchMax;
            try
            {
                // Each uint contributes at most 9 digits to the decimal representation.
                cchMax = checked(cuDst * kcchBase);
            }
            catch (OverflowException e)
            {
                throw new FormatException(SR.GetString(SR.Format_TooLarge), e);
            }

            if (decimalFmt)
            {
                if (digits > 0 && digits > cchMax)
                    cchMax = digits;
                if (value._sign < 0)
                {
                    try
                    {
                        // Leave an extra slot for a minus sign.
                        cchMax = checked(cchMax + info.NegativeSign.Length);
                    }
                    catch (OverflowException e)
                    {
                        throw new FormatException(SR.GetString(SR.Format_TooLarge), e);
                    }
                }
            }

            int rgchBufSize;

            try
            {
                // We'll pass the rgch buffer to native code, which is going to treat it like a string of digits, so it needs
                // to be null terminated.  Let's ensure that we can allocate a buffer of that size.
                rgchBufSize = checked(cchMax + 1);
            }
            catch (OverflowException e)
            {
                throw new FormatException(SR.GetString(SR.Format_TooLarge), e);
            }

            char[] rgch = new char[rgchBufSize];

            int ichDst = cchMax;

            for (int iuDst = 0; iuDst < cuDst - 1; iuDst++)
            {
                uint uDig = rguDst[iuDst];
                for (int cch = kcchBase; --cch >= 0;)
                {
                    rgch[--ichDst] = (char) ('0' + uDig % 10);
                    uDig /= 10;
                }
            }
            for (uint uDig = rguDst[cuDst - 1]; uDig != 0;)
            {
                rgch[--ichDst] = (char) ('0' + uDig % 10);
                uDig /= 10;
            }

            // Format Round-trip decimal
            // This format is supported for integral types only. The number is converted to a string of
            // decimal digits (0-9), prefixed by a minus sign if the number is negative. The precision
            // specifier indicates the minimum number of digits desired in the resulting string. If required,
            // the number is padded with zeros to its left to produce the number of digits given by the
            // precision specifier.
            int numDigitsPrinted = cchMax - ichDst;
            while (digits > 0 && digits > numDigitsPrinted)
            {
                // pad leading zeros
                rgch[--ichDst] = '0';
                digits--;
            }
            if (value._sign < 0)
            {
                string negativeSign = info.NegativeSign;
                for (int i = info.NegativeSign.Length - 1; i > -1; i--)
                    rgch[--ichDst] = info.NegativeSign[i];
            }
            return new string(rgch, ichDst, cchMax - ichDst);
        }

        private static String FormatBigIntegerToHexString(BigInteger value, char format, int digits, NumberFormatInfo info)
        {
            StringBuilder sb = new StringBuilder();
            byte[] bits = value.ToByteArray();
            String fmt = null;
            int cur = bits.Length - 1;

            if (cur > -1)
            {
                // [FF..F8] drop the high F as the two's complement negative number remains clear
                // [F7..08] retain the high bits as the two's complement number is wrong without it
                // [07..00] drop the high 0 as the two's complement positive number remains clear
                bool clearHighF = false;
                byte head = bits[cur];
                if (head > 0xF7)
                {
                    head -= 0xF0;
                    clearHighF = true;
                }
                if (head < 0x08 || clearHighF)
                {
                    // {0xF8-0xFF} print as {8-F}
                    // {0x00-0x07} print as {0-7}
                    fmt = String.Format(CultureInfo.InvariantCulture, "{0}1", format);
                    sb.Append(head.ToString(fmt, info));
                    cur--;
                }
            }
            if (cur > -1)
            {
                fmt = String.Format(CultureInfo.InvariantCulture, "{0}2", format);
                while (cur > -1)
                {
                    sb.Append(bits[cur--].ToString(fmt, info));
                }
            }
            if (digits > 0 && digits > sb.Length)
            {
                // insert leading zeros.  User specified "X5" so we create "0ABCD" instead of "ABCD"
                sb.Insert(0, (value._sign >= 0 ? ("0") : (format == 'x' ? "f" : "F")), digits - sb.Length);
            }
            return sb.ToString();
        }

        // this function is consistent with VM\COMNumber.cpp!COMNumber::ParseFormatSpecifier
        private static char ParseFormatSpecifier(String format, out Int32 digits)
        {
            digits = -1;
            if (String.IsNullOrEmpty(format))
            {
                return 'R';
            }

            int i = 0;
            char ch = format[i];
            if (ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z')
            {
                i++;
                int n = -1;

                if (i < format.Length && format[i] >= '0' && format[i] <= '9')
                {
                    n = format[i++] - '0';
                    while (i < format.Length && format[i] >= '0' && format[i] <= '9')
                    {
                        n = n * 10 + (format[i++] - '0');
                        if (n >= 10)
                            break;
                    }
                }
                if (i >= format.Length || format[i] == '\0')
                {
                    digits = n;
                    return ch;
                }
            }
            return (char)0; // custom format
        }

        #endregion
    }
}
