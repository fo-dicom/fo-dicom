using System;
using System.Linq;
using System.Text;

namespace DICOM__Unit_Tests_.IO.Reader
{
  using System.Collections.Generic;
  using System.ComponentModel;

  using Dicom;
  using Dicom.IO;
  using Dicom.IO.Buffer;
  using Dicom.IO.Reader;

  using DICOM__Unit_Tests_.Helpers;

  using Xunit;

  public class DicomReaderTest
  {
    private static DicomDataset ReadFragment(byte[] bytes, Endian endian, bool explicitVr)
    {
      var dataset = new DicomDataset();
      var reader = new DicomReader { IsExplicitVR = explicitVr };
      var byteSource = new ByteBufferByteSource(new MemoryByteBuffer(bytes)) { Endian = endian };
      reader.Read(byteSource, new DicomDatasetReaderObserver(dataset));
      return dataset;
    }

    [Fact]
    public void Read_single_tag_little_endian_implicit_vr_dataset()
    {
      var bytes = SuperSimpleDicomWriter.GetItemAsBytes(0x0008, 0x0018, "UI", "1.2.3", littleEndian: true, explicitVr: false);
      var dataset = ReadFragment(bytes, Endian.Little, explicitVr: false);

      Assert.Equal(1, dataset.Count());
      Assert.Equal(dataset.Get<string>(DicomTag.SOPInstanceUID), "1.2.3");
    }

    [Fact]
    public void Read_single_tag_big_endian_implicit_vr_dataset()
    {
      var bytes = SuperSimpleDicomWriter.GetItemAsBytes(0x0008, 0x0018, "UI", "1.2.3", littleEndian: false, explicitVr: false);
      var dataset = ReadFragment(bytes, Endian.Big, explicitVr: false);

      Assert.Equal(1, dataset.Count());
      Assert.Equal(dataset.Get<string>(DicomTag.SOPInstanceUID), "1.2.3");
    }

    [Fact]
    public void Read_single_tag_little_endian_explicit_vr_dataset()
    {
      var bytes = SuperSimpleDicomWriter.GetItemAsBytes(0x0008, 0x0018, "UI", "1.2.3", littleEndian: true, explicitVr: true);
      var dataset = ReadFragment(bytes, Endian.Little, explicitVr: true);

      Assert.Equal(1, dataset.Count());
      Assert.Equal(dataset.Get<string>(DicomTag.SOPInstanceUID), "1.2.3");
    }

    [Fact]
    public void Read_single_tag_big_endian_explicit_vr_dataset()
    {
      var bytes = SuperSimpleDicomWriter.GetItemAsBytes(0x0008, 0x0018, "UI", "1.2.3", littleEndian: false, explicitVr: true);
      var dataset = ReadFragment(bytes, Endian.Big, explicitVr: true);

      Assert.Equal(1, dataset.Count());
      Assert.Equal(dataset.Get<string>(DicomTag.SOPInstanceUID), "1.2.3");
    }

    [Fact]
    public void Read_two_tags_little_endian_implicit_vr_dataset()
    {
      var bytes = new List<byte>();
      SuperSimpleDicomWriter.WriteTag(bytes, 0x0008, 0x0016, "UI", "1.2.3", littleEndian: true, explicitVr: false);
      SuperSimpleDicomWriter.WriteTag(bytes, 0x0008, 0x0018, "UI", "1.2.4", littleEndian: true, explicitVr: false);
      var dataset = ReadFragment(bytes.ToArray(), Endian.Little, explicitVr: false);
      Assert.Equal(2, dataset.Count());
      Assert.Equal(dataset.Get<string>(DicomTag.SOPClassUID), "1.2.3");
      Assert.Equal(dataset.Get<string>(DicomTag.SOPInstanceUID), "1.2.4");
    }
  }
}
