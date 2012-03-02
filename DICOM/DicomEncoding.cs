using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
	public static class DicomEncoding {
		public readonly static Encoding Default = Encoding.ASCII;

		public static Encoding GetEncoding(string charset) {
			if (String.IsNullOrWhiteSpace(charset))
				return Default;

			switch (charset.Trim()) {
				case "ISO_IR 13":		return Encoding.GetEncoding(932);   // JIS X 0201 (Shift JIS) Unextended
				case "ISO_IR 100":		return Encoding.GetEncoding(28591); // Latin Alphabet No. 1 Unextended
				case "ISO_IR 101":		return Encoding.GetEncoding(28592); // Latin Alphabet No. 2 Unextended
				case "ISO_IR 109":		return Encoding.GetEncoding(28593); // Latin Alphabet No. 3 Unextended
				case "ISO_IR 110":		return Encoding.GetEncoding(28594); // Latin Alphabet No. 4 Unextended
				case "ISO_IR 126":		return Encoding.GetEncoding(28597); // Greek Unextended
				case "ISO_IR 127":		return Encoding.GetEncoding(28596); // Arabic Unextended
				case "ISO_IR 138":		return Encoding.GetEncoding(28598); // Hebrew Unextended
				case "ISO_IR 144":		return Encoding.GetEncoding(28595); // Cyrillic Unextended
				case "ISO_IR 148":		return Encoding.GetEncoding(28599); // Latin Alphabet No. 5 (Turkish) Unextended
				case "ISO_IR 166":		return Encoding.GetEncoding(874);   // TIS 620-2533 (Thai) Unextended
				case "ISO_IR 192":		return Encoding.GetEncoding(65001); // Unicode in UTF-8
				case "ISO 2022 IR 6":	return Encoding.GetEncoding(20127); // ASCII
				case "ISO 2022 IR 13":	return Encoding.GetEncoding(50222); // JIS X 0201 (Shift JIS) Extended
				case "ISO 2022 IR 87":	return Encoding.GetEncoding(50222); // JIS X 0208 (Kanji) Extended
				case "ISO 2022 IR 100":	return Encoding.GetEncoding(28591); // Latin Alphabet No. 1 Extended
				case "ISO 2022 IR 101":	return Encoding.GetEncoding(28592); // Latin Alphabet No. 2 Extended
				case "ISO 2022 IR 109":	return Encoding.GetEncoding(28593); // Latin Alphabet No. 3 Extended
				case "ISO 2022 IR 110":	return Encoding.GetEncoding(28594); // Latin Alphabet No. 4 Extended
				case "ISO 2022 IR 127": return Encoding.GetEncoding(28596); // Arabic Extended
				case "ISO 2022 IR 126": return Encoding.GetEncoding(28597); // Greek Extended
				case "ISO 2022 IR 138": return Encoding.GetEncoding(28598); // Hebrew Extended
				case "ISO 2022 IR 144":	return Encoding.GetEncoding(28595); // Cyrillic Extended
				case "ISO 2022 IR 148":	return Encoding.GetEncoding(28599); // Latin Alphabet No. 5 (Turkish) Extended
				case "ISO 2022 IR 149": return Encoding.GetEncoding(20949); // KS X 1001 (Hangul and Hanja) Extended
				case "ISO 2022 IR 159": return Encoding.GetEncoding(50222); // JIS X 0212 (Kanji) Extended
				case "ISO 2022 IR 166":	return Encoding.GetEncoding(874);   // TIS 620-2533 (Thai) Extended
				case "GB18030":			return Encoding.GetEncoding(54936); // Chinese (Simplified) Extended
				default:
					throw new ArgumentException("No codepage found for requested DICOM charset.", "charset");
			}
		}

		public static string GetCharset(Encoding encoding) {
			if (encoding == null)
				return "ISO 2022 IR 6";

			// Do we always want the extended charsets?
			switch (encoding.CodePage) {
				//case 874:   return "ISO_IR 166";		// TIS 620-2533 (Thai) Unextended
				case 874:   return "ISO 2022 IR 166";	// TIS 620-2533 (Thai) Extended
				case 932:   return "ISO_IR 13";			// JIS X 0201 (Shift JIS) Unextended
				case 20127: return "ISO 2022 IR 6";		// ASCII
				case 20949: return "ISO 2022 IR 149";	// KS X 1001 (Hangul and Hanja) Extended
				//case 28591: return "ISO_IR 100";		// Latin Alphabet No. 1 Unextended
				case 28591: return "ISO 2022 IR 100";	// Latin Alphabet No. 1 Extended
				//case 28592: return "ISO_IR 101";		// Latin Alphabet No. 2 Unextended
				case 28592: return "ISO 2022 IR 101";	// Latin Alphabet No. 2 Extended
				//case 28593: return "ISO_IR 109";		// Latin Alphabet No. 3 Unextended
				case 28593: return "ISO 2022 IR 109";	// Latin Alphabet No. 3 Extended
				//case 28594: return "ISO_IR 110";		// Latin Alphabet No. 4 Unextended
				case 28594: return "ISO 2022 IR 110";	// Latin Alphabet No. 4 Extended
				//case 28595: return "ISO_IR 144";		// Cyrillic Unextended
				case 28595: return "ISO 2022 IR 144";	// Cyrillic Extended
				//case 28596: return "ISO_IR 127";		// Arabic Unextended
				case 28596: return "ISO 2022 IR 127";	// Arabic Extended
				//case 28597: return "ISO_IR 126";		// Greek Unextended
				case 28597: return "ISO 2022 IR 126";	// Greek Extended
				//case 28598: return "ISO_IR 138";		// Hebrew Unextended
				case 28598: return "ISO 2022 IR 138";	// Hebrew Extended
				//case 28599: return "ISO_IR 148";		// Latin Alphabet No. 5 (Turkish) Unextended
				case 28599: return "ISO 2022 IR 148";	// Latin Alphabet No. 5 (Turkish) Extended
				//case 50222: return "ISO 2022 IR 13";	// JIS X 0201 (Shift JIS) Extended
				//case 50222: return "ISO 2022 IR 87";	// JIS X 0208 (Kanji) Extended
				case 50222: return "ISO 2022 IR 159";	// JIS X 0212 (Kanji) Extended
				case 54936: return "GB18030";			// Chinese (Simplified) Extended
				case 65001: return "ISO_IR 192";		// Unicode in UTF-8
				default:
					throw new ArgumentException("No DICOM charset found for requested encoding.", "encoding");
			}
		}
	}
}
