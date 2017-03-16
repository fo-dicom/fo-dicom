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

namespace System.Numerics
{
    public struct BigInteger
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

        public BigInteger(int value)
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
        public BigInteger(byte[] value)
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
                    NumericsHelpers.DangerousMakeTwosComplement(val); // mutates val

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

        #region internal support methods

        public int CompareTo(long other)
        {
            if (_bits == null)
                return ((long)_sign).CompareTo(other);
            int cu;
            if ((_sign ^ other) < 0 || (cu = Length(_bits)) > 2)
                return _sign;
            ulong uu = other < 0 ? (ulong)-other : (ulong)other;
            ulong uuTmp = cu == 2 ? NumericsHelpers.MakeUlong(_bits[1], _bits[0]) : _bits[0];
            return _sign * uuTmp.CompareTo(uu);
        }

        internal static int Length(uint[] rgu)
        {
            var cu = rgu.Length;
            if (rgu[cu - 1] != 0)
                return cu;
            return cu - 1;
        }

        #endregion
    } 
}
