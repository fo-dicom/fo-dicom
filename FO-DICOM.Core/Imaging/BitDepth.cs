// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Text;

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Extract image bit depth infomation from dataset and provide information for image rendering process
    /// </summary>
    public class BitDepth
    {
        /// <summary>
        /// Initialize the bit depth information using passed parameters
        /// </summary>
        /// <param name="allocated">Number of bits allocated per pixel sample (8,16,32) </param>
        /// <param name="stored">Number of bits stored per pixel sample (from LSB to MSB)</param>
        /// <param name="highBit">The high bit zero based index</param>
        /// <param name="signed">True if pixel data signed (sign will be stored in the high bit)</param>
        public BitDepth(int allocated, int stored, int highBit, bool signed)
        {
            BitsAllocated = allocated;
            BitsStored = stored;
            HighBit = highBit;
            IsSigned = signed;
        }

        /// <summary>Number of bits allocated per sample. Generally 1, 4, 8, or 16.</summary>
        public int BitsAllocated { get; private set; }

        /// <summary>Number of bits stored per sample.</summary>
        public int BitsStored { get; private set; }

        /// <summary>Highest value bit in sample.</summary>
        public int HighBit { get; private set; }

        /// <summary>Samples are signed values if true.</summary>
        public bool IsSigned { get; private set; }

        /// <summary>
        /// The maximum possible pixel value from stored bits
        /// </summary>
        public int MaximumValue => GetMaximumValue(BitsStored, IsSigned);

        /// <summary>
        /// The minimum possible pixel value from stored bits
        /// </summary>
        public int MinimumValue => GetMinimumValue(BitsStored, IsSigned);

        /// <summary>Rounds up to the next power of 2.</summary>
        /// <param name="value"></param>
        /// <returns>Next power of 2 or current value if already power of 2.</returns>
        public static int GetNextPowerOf2(int value)
        {
            // http://bits.stephan-brumme.com/roundUpToNextPowerOfTwo.html
            value--;
            value |= value >> 1; // handle  2 bit numbers
            value |= value >> 2; // handle  4 bit numbers
            value |= value >> 4; // handle  8 bit numbers
            value |= value >> 8; // handle 16 bit numbers
            value |= value >> 16; // handle 32 bit numbers
            return value + 1;
        }

        /// <summary>
        /// Calculate the minimum value for specified bits and sign
        /// </summary>
        /// <param name="bitsStored">Number of bits</param>
        /// <param name="isSigned">True if signed</param>
        /// <returns>The minimum value</returns>
        public static int GetMinimumValue(int bitsStored, bool isSigned)
        {
            return isSigned ? -(1 << (bitsStored - 1)) : 0;
        }

        /// <summary>
        /// Calculate the maximum value for specified bits and sign
        /// </summary>
        /// <param name="bitsStored">Number of bits</param>
        /// <param name="isSigned">True if signed</param>
        /// <returns>The maximum value</returns>
        public static int GetMaximumValue(int bitsStored, bool isSigned)
        {
            return isSigned ? (1 << (bitsStored - 1)) - 1 : (1 << bitsStored) - 1;
        }

        /// <summary>
        /// Return the high data bit index (excluding sign bit if data is signed)
        /// </summary>
        /// <param name="bitsStored">Number of bits</param>
        /// <param name="isSigned">True if signed</param>
        /// <returns>Index of high data bit (excluding sign bit if data is signed)</returns>
        public static int GetHighBit(int bitsStored, bool isSigned)
        {
            return isSigned ? bitsStored - 1 : bitsStored;
        }

        /// <summary>
        /// Create new instance of <see cref="BitDepth"/> from input <paramref name="dataset"/>
        /// </summary>
        /// <param name="dataset">Input dataset to extract bit depth information from</param>
        /// <returns>New <see cref="BitDepth"/> instance</returns>
        public static BitDepth FromDataset(DicomDataset dataset)
        {
            var allocated = dataset.GetSingleValue<ushort>(DicomTag.BitsAllocated);
            var stored = dataset.GetSingleValue<ushort>(DicomTag.BitsStored);
            var signed = dataset.GetSingleValue<PixelRepresentation>(DicomTag.PixelRepresentation) == PixelRepresentation.Signed;
            return new BitDepth(allocated, stored, GetHighBit(stored, signed), signed);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Bits: {");
            sb.AppendFormat("    Allocated: {0}\n", BitsAllocated);
            sb.AppendFormat("       Stored: {0}\n", BitsStored);
            sb.AppendFormat("     High Bit: {0}\n", HighBit);
            sb.AppendFormat("       Signed: {0}\n", IsSigned);
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
