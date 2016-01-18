// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Render
{
  using System;
  using System.Linq;
  using Xunit;

  public class PixelDataTest
  {
    [Fact]
    public void DicomPixelData_CreateSignedGrayscale32_12BitMaskWorks()
    {
      ushort bitsAllocated = 32;
      ushort bitsStored = 12;
      ushort highBit = 11;
      ushort pixelRepresentation = 1; // signed

      var mask = (int)((1 << (highBit + 1)) - 1);

      var origData = new int[] { -2048, -1, 0, 1, 0x7ff };

      // Bits outside bitsStored should be ignored. Try setting them to zeros and ones, respectively, and verify that the
      // output is unchanged:
      var equivalentData = origData.Select(x => (int)(x & mask)).ToArray();
      var equivalentData2 = origData.Select(x => (int)(x | ~mask)).ToArray();

      var pixelData = CreatePixelData_(equivalentData, bitsAllocated, bitsStored, highBit, pixelRepresentation, BitConverter.GetBytes);
      var pixelData2 = CreatePixelData_(equivalentData2, bitsAllocated, bitsStored, highBit, pixelRepresentation, BitConverter.GetBytes);

      Assert.IsAssignableFrom(typeof (GrayscalePixelDataS32), pixelData);
      Assert.IsAssignableFrom(typeof (GrayscalePixelDataS32), pixelData2);
      var grayscaleData = (GrayscalePixelDataS32)pixelData;
      var grayscaleData2 = (GrayscalePixelDataS32)pixelData2;
      Assert.Equal(origData.Length, grayscaleData.Data.Length);
      Assert.Equal(origData.Length, grayscaleData2.Data.Length);
      Assert.Equal(origData, grayscaleData.Data);
      Assert.Equal(origData, grayscaleData2.Data);
    }

    [Fact]
    public void DicomPixelData_CreateSignedGrayscale16_12BitMaskWorks()
    {
      // Example 1 from Figure D-3 in PS3.5 Chapter D:
      ushort bitsAllocated = 16;
      ushort bitsStored = 12;
      ushort highBit = 11;
      ushort pixelRepresentation = 1; // signed

      var mask = (short)((1 << (highBit + 1)) - 1);

      var origData = new short[] { -2048, -1, 0, 1, 0x7ff };

      // Bits outside bitsStored should be ignored. Try setting them to zeros and ones, respectively, and verify that the
      // output is unchanged:
      var equivalentData = origData.Select(x => (short)(x & mask)).ToArray();
      var equivalentData2 = origData.Select(x => (short)(x | ~mask)).ToArray();

      var pixelData = CreatePixelData_(equivalentData, bitsAllocated, bitsStored, highBit, pixelRepresentation, BitConverter.GetBytes);
      var pixelData2 = CreatePixelData_(equivalentData2, bitsAllocated, bitsStored, highBit, pixelRepresentation, BitConverter.GetBytes);

      Assert.IsAssignableFrom(typeof (GrayscalePixelDataS16), pixelData);
      Assert.IsAssignableFrom(typeof (GrayscalePixelDataS16), pixelData2);
      var grayscaleData = (GrayscalePixelDataS16)pixelData;
      var grayscaleData2 = (GrayscalePixelDataS16)pixelData2;
      Assert.Equal(origData.Length, grayscaleData.Data.Length);
      Assert.Equal(origData.Length, grayscaleData2.Data.Length);
      Assert.Equal(origData, grayscaleData.Data);
      Assert.Equal(origData, grayscaleData2.Data);
    }

    [Fact]
    public void DicomPixelData_CreateUnsignedGrayscale16_12BitMaskWorks()
    {
      // Example 1 from Figure D-3 in PS3.5 Chapter D:
      const ushort bitsAllocated = 16;
      const ushort bitsStored = 12;
      const ushort highBit = 11;
      const ushort pixelRepresentation = 0; // unsigned

      var mask = (ushort)((1 << (highBit + 1)) - 1);

      var origData = new ushort[] { 0, 1, 2047, 2048, 2049, 4095 };

      // Bits outside bitsStored should be ignored. Try setting them to zeros and ones, respectively, and verify that the
      // output is unchanged:
      var equivalentData = origData.Select(x => (ushort)(x & mask)).ToArray();
      var equivalentData2 = origData.Select(x => (ushort)(x | ~mask)).ToArray();

      var pixelData = CreatePixelData_(equivalentData, bitsAllocated, bitsStored, highBit, pixelRepresentation, BitConverter.GetBytes);
      var pixelData2 = CreatePixelData_(equivalentData2, bitsAllocated, bitsStored, highBit, pixelRepresentation, BitConverter.GetBytes);

      Assert.IsAssignableFrom(typeof(GrayscalePixelDataU16), pixelData);
      Assert.IsAssignableFrom(typeof(GrayscalePixelDataU16), pixelData2);
      var grayscaleData = (GrayscalePixelDataU16)pixelData;
      var grayscaleData2 = (GrayscalePixelDataU16)pixelData2;
      Assert.Equal(origData.Length, grayscaleData.Data.Length);
      Assert.Equal(origData.Length, grayscaleData2.Data.Length);
      Assert.Equal(origData, grayscaleData.Data);
      Assert.Equal(origData, grayscaleData2.Data);
    }

    [Fact]
    public void DicomPixelData_CreateUnsignedGrayscale32_12BitMaskWorks()
    {
      const ushort bitsAllocated = 32;
      const ushort bitsStored = 12;
      const ushort highBit = 11;
      const ushort pixelRepresentation = 0; // unsigned

      var mask = (uint)((1 << (highBit + 1)) - 1);

      var origData = new uint[] { 0, 1, 2047, 2048, 2049, 4095 };

      // Bits outside bitsStored should be ignored. Try setting them to zeros and ones, respectively, and verify that the
      // output is unchanged:
      var equivalentData = origData.Select(x => (uint)(x & mask)).ToArray();
      var equivalentData2 = origData.Select(x => (uint)(x | ~mask)).ToArray();

      var pixelData = CreatePixelData_(equivalentData, bitsAllocated, bitsStored, highBit, pixelRepresentation, BitConverter.GetBytes);
      var pixelData2 = CreatePixelData_(equivalentData2, bitsAllocated, bitsStored, highBit, pixelRepresentation, BitConverter.GetBytes);

      Assert.IsAssignableFrom(typeof(GrayscalePixelDataU32), pixelData);
      Assert.IsAssignableFrom(typeof(GrayscalePixelDataU32), pixelData2);
      var grayscaleData = (GrayscalePixelDataU32)pixelData;
      var grayscaleData2 = (GrayscalePixelDataU32)pixelData2;
      Assert.Equal(origData.Length, grayscaleData.Data.Length);
      Assert.Equal(origData.Length, grayscaleData2.Data.Length);
      Assert.Equal(origData, grayscaleData.Data);
      Assert.Equal(origData, grayscaleData2.Data);
    }

    private static IPixelData CreatePixelData_<T>(T[] data, ushort bitsAllocated, ushort bitsStored, ushort highBit, ushort pixelRepresentation, Func<T, byte[]> getBytes)
    {
      return PixelDataFactory.Create(
        DicomPixelData.Create(new DicomDataset
        {
          { DicomTag.BitsAllocated, bitsAllocated },
          { DicomTag.BitsStored, bitsStored },
          { DicomTag.HighBit, highBit },
          { DicomTag.PixelData, data.SelectMany(getBytes).ToArray() },
          { DicomTag.PixelRepresentation, pixelRepresentation },
          { DicomTag.Columns, (ushort)1 },
          { DicomTag.Rows, (ushort)data.Length },
        }),
        0);
    }
  }
}