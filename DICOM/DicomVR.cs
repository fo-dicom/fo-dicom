using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Dicom {
	public sealed class DicomVR {
		private const byte PadZero = 0x00;
		private const byte PadSpace = 0x20;

		private DicomVR() {
		}

		public string Code {
			get;
			private set;
		}

		public string Name {
			get;
			private set;
		}

		public bool IsString {
			get;
			private set;
		}

		public bool IsStringEncoded {
			get;
			private set;
		}

		public bool Is16bitLength {
			get;
			private set;
		}

		public bool IsMultiValue {
			get;
			private set;
		}

		public byte PaddingValue {
			get;
			private set;
		}

		public uint MaximumLength {
			get;
			private set;
		}

		public int UnitSize {
			get;
			private set;
		}

		public int ByteSwap {
			get;
			private set;
		}

		public Type ValueType {
			get;
			private set;
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("vr", Code);
		}

		public override string ToString() {
			return Code;
		}

		public static DicomVR Parse(string vr) {
			switch (vr) {
			case "NONE": return DicomVR.NONE;
			case "AE": return DicomVR.AE;
			case "AS": return DicomVR.AS;
			case "AT": return DicomVR.AT;
			case "CS": return DicomVR.CS;
			case "DA": return DicomVR.DA;
			case "DS": return DicomVR.DS;
			case "DT": return DicomVR.DT;
			case "FD": return DicomVR.FD;
			case "FL": return DicomVR.FL;
			case "IS": return DicomVR.IS;
			case "LO": return DicomVR.LO;
			case "LT": return DicomVR.LT;
			case "OB": return DicomVR.OB;
			case "OF": return DicomVR.OF;
			case "OW": return DicomVR.OW;
			case "PN": return DicomVR.PN;
			case "SH": return DicomVR.SH;
			case "SL": return DicomVR.SL;
			case "SQ": return DicomVR.SQ;
			case "SS": return DicomVR.SS;
			case "ST": return DicomVR.ST;
			case "TM": return DicomVR.TM;
			case "UI": return DicomVR.UI;
			case "UL": return DicomVR.UL;
			case "UN": return DicomVR.UN;
			case "US": return DicomVR.US;
			case "UT": return DicomVR.UT;
			default:
				throw new DicomDataException("Unknown VR: '" + vr + "'");
			}
		}

		//public static DicomVR NONE = new DicomVR("NONE", "No VR", false, false, false, PadZero, 0, 0, DicomVrRestriction.NotApplicable);
		public readonly static DicomVR NONE = new DicomVR {
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

		//public static DicomVR AE = new DicomVR("AE", "Application Entity", true, false, true, PadSpace, 16, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR AE = new DicomVR {
			Code = "AE",
			Name = "Application Entity",
			IsString = true,
			IsStringEncoded = false,
			Is16bitLength = true,
			IsMultiValue = true,
			PaddingValue = PadSpace,
			MaximumLength = 16,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(string)
		};

		//public static DicomVR AS = new DicomVR("AS", "Age String", true, false, true, PadSpace, 4, 1, DicomVrRestriction.Fixed);
		public readonly static DicomVR AS = new DicomVR {
			Code = "AS",
			Name = "Age String",
			IsString = true,
			IsStringEncoded = false,
			Is16bitLength = true,
			IsMultiValue = true,
			PaddingValue = PadSpace,
			MaximumLength = 4,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(string)
		};

		//public static DicomVR AT = new DicomVR("AT", "Attribute Tag", false, false, true, PadZero, 4, 4, DicomVrRestriction.Fixed);
		public readonly static DicomVR AT = new DicomVR {
			Code = "AT",
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

		//public static DicomVR CS = new DicomVR("CS", "Code String", true, false, true, PadSpace, 16, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR CS = new DicomVR {
			Code = "CS",
			Name = "Code String",
			IsString = true,
			IsStringEncoded = false,
			Is16bitLength = true,
			IsMultiValue = true,
			PaddingValue = PadSpace,
			MaximumLength = 16,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(string)
		};

		//public static DicomVR DA = new DicomVR("DA", "Date", true, false, true, PadSpace, 8, 1, DicomVrRestriction.Fixed);
		public readonly static DicomVR DA = new DicomVR {
			Code = "DA",
			Name = "Date",
			IsString = true,
			IsStringEncoded = false,
			Is16bitLength = true,
			IsMultiValue = true,
			PaddingValue = PadSpace,
			MaximumLength = 8,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(DateTime)
		};

		//public static DicomVR DS = new DicomVR("DS", "Decimal String", true, false, true, PadSpace, 16, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR DS = new DicomVR {
			Code = "DS",
			Name = "Decimal String",
			IsString = true,
			IsStringEncoded = false,
			Is16bitLength = true,
			IsMultiValue = true,
			PaddingValue = PadSpace,
			MaximumLength = 16,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(decimal)
		};

		//public static DicomVR DT = new DicomVR("DT", "Date Time", true, false, true, PadSpace, 26, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR DT = new DicomVR {
			Code = "DT",
			Name = "Date Time",
			IsString = true,
			IsStringEncoded = false,
			Is16bitLength = true,
			IsMultiValue = true,
			PaddingValue = PadSpace,
			MaximumLength = 26,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(DateTime)
		};

		//public static DicomVR FD = new DicomVR("FD", "Floating Point Double", false, false, true, PadZero, 8, 8, DicomVrRestriction.Fixed);
		public readonly static DicomVR FD = new DicomVR {
			Code = "FD",
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

		//public static DicomVR FL = new DicomVR("FL", "Floating Point Single", false, false, true, PadZero, 4, 4, DicomVrRestriction.Fixed);
		public readonly static DicomVR FL = new DicomVR {
			Code = "FL",
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

		//public static DicomVR IS = new DicomVR("IS", "Integer String", true, false, true, PadSpace, 12, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR IS = new DicomVR {
			Code = "IS",
			Name = "Integer String",
			IsString = true,
			IsStringEncoded = false,
			Is16bitLength = true,
			IsMultiValue = true,
			PaddingValue = PadSpace,
			MaximumLength = 12,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(int)
		};

		//public static DicomVR LO = new DicomVR("LO", "Long String", true, true, true, PadSpace, 64, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR LO = new DicomVR {
			Code = "LO",
			Name = "Long String",
			IsString = true,
			IsStringEncoded = true,
			Is16bitLength = true,
			IsMultiValue = false,
			PaddingValue = PadSpace,
			MaximumLength = 64,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(string)
		};

		//public static DicomVR LT = new DicomVR("LT", "Long Text", true, true, true, PadSpace, 10240, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR LT = new DicomVR {
			Code = "LT",
			Name = "Long Text",
			IsString = true,
			IsStringEncoded = true,
			Is16bitLength = true,
			IsMultiValue = false,
			PaddingValue = PadSpace,
			MaximumLength = 10240,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(string)
		};

		//public static DicomVR OB = new DicomVR("OB", "Other Byte", false, false, false, PadZero, 0, 1, DicomVrRestriction.Any);
		public readonly static DicomVR OB = new DicomVR {
			Code = "OB",
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

		//public static DicomVR OF = new DicomVR("OF", "Other Float", false, false, false, PadZero, 0, 4, DicomVrRestriction.Any);
		public readonly static DicomVR OF = new DicomVR {
			Code = "OF",
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

		//public static DicomVR OW = new DicomVR("OW", "Other Word", false, false, false, PadZero, 0, 2, DicomVrRestriction.Any);
		public readonly static DicomVR OW = new DicomVR {
			Code = "OW",
			Name = "Other Word",
			IsString = false,
			IsStringEncoded = false,
			Is16bitLength = false,
			IsMultiValue = true,
			PaddingValue = PadZero,
			MaximumLength = 0,
			UnitSize = 2,
			ByteSwap = 2,
			ValueType = typeof(ushort)
		};

		//public static DicomVR PN = new DicomVR("PN", "Person Name", true, true, true, PadSpace, 64, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR PN = new DicomVR {
			Code = "PN",
			Name = "Person Name",
			IsString = true,
			IsStringEncoded = true,
			Is16bitLength = true,
			IsMultiValue = false,
			PaddingValue = PadSpace,
			MaximumLength = 64,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(String)
		};

		//public static DicomVR SH = new DicomVR("SH", "Short String", true, true, true, PadSpace, 16, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR SH = new DicomVR {
			Code = "SH",
			Name = "Short String",
			IsString = true,
			IsStringEncoded = true,
			Is16bitLength = true,
			IsMultiValue = true,
			PaddingValue = PadSpace,
			MaximumLength = 16,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(string)
		};

		//public static DicomVR SL = new DicomVR("SL", "Signed Long", false, false, true, PadZero, 4, 4, DicomVrRestriction.Fixed);
		public readonly static DicomVR SL = new DicomVR {
			Code = "SL",
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

		//public static DicomVR SQ = new DicomVR("SQ", "Sequence of Items", false, false, false, PadZero, 0, 0, DicomVrRestriction.NotApplicable);
		public readonly static DicomVR SQ = new DicomVR {
			Code = "SQ",
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

		//public static DicomVR SS = new DicomVR("SS", "Signed Short", false, false, true, PadZero, 2, 2, DicomVrRestriction.Fixed);
		public readonly static DicomVR SS = new DicomVR {
			Code = "SS",
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

		//public static DicomVR ST = new DicomVR("ST", "Short Text", true, true, true, PadSpace, 1024, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR ST = new DicomVR {
			Code = "ST",
			Name = "Short Text",
			IsString = true,
			IsStringEncoded = true,
			Is16bitLength = true,
			IsMultiValue = false,
			PaddingValue = PadSpace,
			MaximumLength = 1024,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(string)
		};

		//public static DicomVR TM = new DicomVR("TM", "Time", true, false, true, PadSpace, 16, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR TM = new DicomVR {
			Code = "TM",
			Name = "Time",
			IsString = true,
			IsStringEncoded = false,
			Is16bitLength = true,
			IsMultiValue = true,
			PaddingValue = PadSpace,
			MaximumLength = 16,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(DateTime)
		};

		//public static DicomVR UI = new DicomVR("UI", "Unique Identifier", true, false, true, PadZero, 64, 1, DicomVrRestriction.Maximum);
		public readonly static DicomVR UI = new DicomVR {
			Code = "UI",
			Name = "Unique Identifier",
			IsString = true,
			IsStringEncoded = false,
			Is16bitLength = true,
			IsMultiValue = true,
			PaddingValue = PadZero,
			MaximumLength = 64,
			UnitSize = 1,
			ByteSwap = 1,
			ValueType = typeof(string)
		};

		//public static DicomVR UL = new DicomVR("UL", "Unsigned Long", false, false, true, PadZero, 4, 4, DicomVrRestriction.Fixed);
		public readonly static DicomVR UL = new DicomVR {
			Code = "UL",
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

		//public static DicomVR UN = new DicomVR("UN", "Unknown", false, false, false, PadZero, 0, 1, DicomVrRestriction.Any);
		public readonly static DicomVR UN = new DicomVR {
			Code = "UN",
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

		//public static DicomVR US = new DicomVR("US", "Unsigned Short", false, false, true, PadZero, 2, 2, DicomVrRestriction.Fixed);
		public readonly static DicomVR US = new DicomVR {
			Code = "US",
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

		//public static DicomVR UT = new DicomVR("UT", "Unlimited Text", true, true, false, PadSpace, 0, 1, DicomVrRestriction.Any);
		public readonly static DicomVR UT = new DicomVR {
			Code = "UT",
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
	}
}
