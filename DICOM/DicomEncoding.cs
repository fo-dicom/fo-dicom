using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
	public static class DicomEncoding {
		public readonly static Encoding Default = Encoding.UTF8;

		public static Encoding GetEncoding(string charset) {
			if (String.IsNullOrWhiteSpace(charset))
				return Default;

			switch (charset.Trim()) {
			case "ISO IR 13":  case "ISO_IR 13":  return Encoding.GetEncoding("shift_jis");   // JIS X 0201 (Shift JIS) Unextended
			case "ISO IR 100": case "ISO_IR 100": return Encoding.GetEncoding("iso-8859-1"); // Latin Alphabet No. 1 Unextended
			case "ISO IR 101": case "ISO_IR 101": return Encoding.GetEncoding("iso-8859-2"); // Latin Alphabet No. 2 Unextended
			case "ISO IR 109": case "ISO_IR 109": return Encoding.GetEncoding("iso-8859-3"); // Latin Alphabet No. 3 Unextended
			case "ISO IR 110": case "ISO_IR 110": return Encoding.GetEncoding("iso-8859-4"); // Latin Alphabet No. 4 Unextended
			case "ISO IR 126": case "ISO_IR 126": return Encoding.GetEncoding("iso-8859-7"); // Greek Unextended
			case "ISO IR 127": case "ISO_IR 127": return Encoding.GetEncoding("iso-8859-6"); // Arabic Unextended
			case "ISO IR 138": case "ISO_IR 138": return Encoding.GetEncoding("iso-8859-8"); // Hebrew Unextended
			case "ISO IR 144": case "ISO_IR 144": return Encoding.GetEncoding("iso-8859-5"); // Cyrillic Unextended
			case "ISO IR 148": case "ISO_IR 148": return Encoding.GetEncoding("iso-8859-9"); // Latin Alphabet No. 5 (Turkish) Unextended
			case "ISO IR 166": case "ISO_IR 166": return Encoding.GetEncoding("windows-874");   // TIS 620-2533 (Thai) Unextended
			case "ISO IR 192": case "ISO_IR 192": return Encoding.GetEncoding("utf-8"); // Unicode in UTF-8
            case "ISO 2022 IR 6": return Encoding.GetEncoding("us-ascii"); // ASCII
            case "ISO 2022 IR 13": return Encoding.GetEncoding("iso-2022-jp"); // JIS X 0201 (Shift JIS) Extended
            case "ISO 2022 IR 87": return Encoding.GetEncoding("iso-2022-jp"); // JIS X 0208 (Kanji) Extended
            case "ISO 2022 IR 100": return Encoding.GetEncoding("iso-8859-1"); // Latin Alphabet No. 1 Extended
            case "ISO 2022 IR 101": return Encoding.GetEncoding("iso-8859-2"); // Latin Alphabet No. 2 Extended
            case "ISO 2022 IR 109": return Encoding.GetEncoding("iso-8859-3"); // Latin Alphabet No. 3 Extended
            case "ISO 2022 IR 110": return Encoding.GetEncoding("iso-8859-4"); // Latin Alphabet No. 4 Extended
            case "ISO 2022 IR 127": return Encoding.GetEncoding("iso-8859-6"); // Arabic Extended
            case "ISO 2022 IR 126": return Encoding.GetEncoding("iso-8859-7"); // Greek Extended
            case "ISO 2022 IR 138": return Encoding.GetEncoding("iso-8859-8"); // Hebrew Extended
            case "ISO 2022 IR 144": return Encoding.GetEncoding("iso-8859-5"); // Cyrillic Extended
            case "ISO 2022 IR 148": return Encoding.GetEncoding("iso-8859-9"); // Latin Alphabet No. 5 (Turkish) Extended
            case "ISO 2022 IR 149": return Encoding.GetEncoding("x-cp20949"); // KS X 1001 (Hangul and Hanja) Extended
            case "ISO 2022 IR 159": return Encoding.GetEncoding("iso-2022-jp"); // JIS X 0212 (Kanji) Extended
            case "ISO 2022 IR 166": return Encoding.GetEncoding("windows-874");   // TIS 620-2533 (Thai) Extended
            case "GB18030": return Encoding.GetEncoding("GB18030"); // Chinese (Simplified) Extended
			default: // unknown encoding... return ASCII instead of throwing exception
				//throw new ArgumentException("No codepage found for requested DICOM charset.", "charset");
				return Encoding.UTF8;
			}
		}

		public static string GetCharset(Encoding encoding) {
			if (encoding == null)
				return "ISO 2022 IR 6";

			// Do we always want the extended charsets?
			switch (encoding.WebName) {
				//case 874:   return "ISO_IR 166";		// TIS 620-2533 (Thai) Unextended
                case "windows-874": return "ISO 2022 IR 166";	// TIS 620-2533 (Thai) Extended
				case "shift_jis":   return "ISO_IR 13";			// JIS X 0201 (Shift JIS) Unextended
				case "us-ascii": return "ISO 2022 IR 6";		// ASCII
				case "x-cp20949": return "ISO 2022 IR 149";	// KS X 1001 (Hangul and Hanja) Extended
				//case 28591: return "ISO_IR 100";		// Latin Alphabet No. 1 Unextended
				case "iso-8859-1": return "ISO 2022 IR 100";	// Latin Alphabet No. 1 Extended
				//case 28592: return "ISO_IR 101";		// Latin Alphabet No. 2 Unextended
                case "iso-8859-2": return "ISO 2022 IR 101";	// Latin Alphabet No. 2 Extended
				//case 28593: return "ISO_IR 109";		// Latin Alphabet No. 3 Unextended
                case "iso-8859-3": return "ISO 2022 IR 109";	// Latin Alphabet No. 3 Extended
				//case 28594: return "ISO_IR 110";		// Latin Alphabet No. 4 Unextended
                case "iso-8859-4": return "ISO 2022 IR 110";	// Latin Alphabet No. 4 Extended
				//case 28595: return "ISO_IR 144";		// Cyrillic Unextended
                case "iso-8859-5": return "ISO 2022 IR 144";	// Cyrillic Extended
				//case 28596: return "ISO_IR 127";		// Arabic Unextended
                case "iso-8859-6": return "ISO 2022 IR 127";	// Arabic Extended
				//case 28597: return "ISO_IR 126";		// Greek Unextended
                case "iso-8859-7": return "ISO 2022 IR 126";	// Greek Extended
				//case 28598: return "ISO_IR 138";		// Hebrew Unextended
                case "iso-8859-8": return "ISO 2022 IR 138";	// Hebrew Extended
				//case 28599: return "ISO_IR 148";		// Latin Alphabet No. 5 (Turkish) Unextended
                case "iso-8859-9": return "ISO 2022 IR 148";	// Latin Alphabet No. 5 (Turkish) Extended
				//case 50222: return "ISO 2022 IR 13";	// JIS X 0201 (Shift JIS) Extended
				//case 50222: return "ISO 2022 IR 87";	// JIS X 0208 (Kanji) Extended
				case "iso-2022-jp": return "ISO 2022 IR 159";	// JIS X 0212 (Kanji) Extended
				case "GB18030": return "GB18030";			// Chinese (Simplified) Extended
				case "utf-8": return "ISO_IR 192";		// Unicode in UTF-8
				default:
					throw new ArgumentException("No DICOM charset found for requested encoding.", "encoding");
			}
		}
	}
}
