using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging {
	public class BitDepth {
		public BitDepth(int allocated, int stored, int highBit, bool signed) {
			BitsAllocated = allocated;
			BitsStored = stored;
			HighBit = highBit;
			IsSigned = signed;
		}

		/// <summary>Number of bits allocated per sample. Generally 1, 4, 8, or 16.</summary>
		public int BitsAllocated {
			get; private set;
		}

		/// <summary>Number of bits stored per sample.</summary>
		public int BitsStored {
			get; private set;
		}

		/// <summary>Highest value bit in sample.</summary>
		public int HighBit {
			get; private set;
		}

		/// <summary>Samples are signed values if true.</summary>
		public bool IsSigned {
			get; private set;
		}

		public int MaximumValue {
			get { return GetMaximumValue(BitsStored, IsSigned); }
		}

		public int MinimumValue {
			get { return GetMinimumValue(BitsStored, IsSigned); }
		}

		/// <summary>Rounds up to the next power of 2.</summary>
		/// <param name="value"></param>
		/// <returns>Next power of 2 or current value if already power of 2.</returns>
		public static int GetNextPowerOf2(int value) {
			// http://bits.stephan-brumme.com/roundUpToNextPowerOfTwo.html
			value--;
			value |= value >> 1;  // handle  2 bit numbers
			value |= value >> 2;  // handle  4 bit numbers
			value |= value >> 4;  // handle  8 bit numbers
			value |= value >> 8;  // handle 16 bit numbers
			value |= value >> 16; // handle 32 bit numbers
			return value + 1;
		}

		public static int GetMinimumValue(int bitsStored, bool isSigned) {
			if (isSigned) return -(1 << (bitsStored - 1));
			return 0;
		}

		public static int GetMaximumValue(int bitsStored, bool isSigned) {
			if (isSigned) return (1 << (bitsStored - 1)) - 1;
			return (1 << bitsStored) - 1;
		}

		public static int GetHighBit(int bitsStored, bool isSigned) {
			if (isSigned) return bitsStored - 1;
			return bitsStored;
		}

		public override string ToString() {
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
