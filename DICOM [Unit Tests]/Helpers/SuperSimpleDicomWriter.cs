using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DICOM__Unit_Tests_.Helpers
{
  public static class SuperSimpleDicomWriter
  {
    public static byte[] GetItemAsBytes(UInt16 group, UInt16 tag, string vr, string val, bool littleEndian, bool explicitVr)
    {
      var bytes = new List<byte>();
      WriteTag(bytes, group, tag, vr, val, littleEndian, explicitVr);
      return bytes.ToArray();
    }

    public static void WriteTag(List<byte> bytes, UInt16 group, UInt16 tag, string vr, string val, bool littleEndian, bool explicitVr)
    {
      WriteTag(bytes, group, tag, vr, Encoding.ASCII.GetBytes(val), littleEndian, explicitVr);
    }

    public static IEnumerable<byte> GetBytes(UInt16 val, bool littleEndian)
    {
      if (littleEndian) return BitConverter.GetBytes(val);
      else return BitConverter.GetBytes(val).Reverse();
    }

    public static IEnumerable<byte> GetBytes(UInt32 val, bool littleEndian)
    {
      if (littleEndian) return BitConverter.GetBytes(val);
      else return BitConverter.GetBytes(val).Reverse();
    }

    public static void WriteTag(List<byte> bytes, UInt16 group, UInt16 tag, string vr, byte[] val, bool littleEndian, bool explicitVr)
    {
      bytes.AddRange(GetBytes(group, littleEndian));
      bytes.AddRange(GetBytes(tag, littleEndian));

      if (explicitVr)
      {
        bytes.AddRange(Encoding.ASCII.GetBytes(vr));
        bytes.AddRange(GetBytes((UInt16)val.Length, littleEndian));
      }
      else bytes.AddRange(GetBytes((UInt32)val.Length, littleEndian));
      bytes.AddRange(val);
    }

    public static void WriteSequence(List<byte> bytes, UInt32 tag, bool littleEndian, bool explicitVr)
    {
      // TODO
    }
  }
}
