namespace DICOM__Unit_Tests_
{
  using System.IO;

  using Dicom;
  using Dicom.IO;
  using Dicom.IO.Buffer;
  using Dicom.IO.Reader;
  using Dicom.IO.Writer;

  using Xunit;

  public class DicomUIDGeneratorTest
  {
    [Fact]
    public void Test_that_a_generator_remembers_generated()
    {
      DicomUIDGenerator generator = new DicomUIDGenerator();
      var a = generator.Generate();
      var b = generator.Generate(a);
      var c = generator.Generate(b);
      var d = generator.Generate(a);
      var e = generator.Generate(b);

      Assert.NotEqual(a, b);
      Assert.NotEqual(a, c);
      Assert.NotEqual(a, d);
      Assert.NotEqual(a, e);
      Assert.NotEqual(b, c);
      Assert.Equal(b, d);
      Assert.NotEqual(b, e);
      Assert.NotEqual(c, d);
      Assert.Equal(c, e);
      Assert.NotEqual(d, e);
    }

    [Fact]
    public void Test_RegenerateAll()
    {
      var uidGenerator = new DicomUIDGenerator();
      var dataset = new DicomDataset
                     {
                       new DicomPersonName(
                         DicomTag.PatientName,
                         new[] { "Anna^Pelle", null, "Olle^Jöns^Pyjamas" }),
                       { DicomTag.SOPClassUID, DicomUID.RTPlanStorage },
                       { DicomTag.SOPInstanceUID, uidGenerator.Generate() },
                       { DicomTag.SeriesInstanceUID,
                         new[] { uidGenerator.Generate(), uidGenerator.Generate() } },
                       { DicomTag.FrameOfReferenceUID, uidGenerator.Generate() }
                     };
      dataset.Add(
        DicomTag.ReferencedFrameOfReferenceSequence,
        new[]
          {
            new DicomDataset { { DicomTag.ReferencedFrameOfReferenceUID, uidGenerator.Generate() } },
            new DicomDataset { { DicomTag.ReferencedFrameOfReferenceUID, uidGenerator.Generate() } }
          });

      var clone = this.DeepClone_(dataset);

      uidGenerator.RegenerateAll(clone);

      Assert.Equal(dataset.Get<string>(DicomTag.PatientName), clone.Get<string>(DicomTag.PatientName));
      Assert.Equal(dataset.Get<string>(DicomTag.SOPClassUID), clone.Get<string>(DicomTag.SOPClassUID));
      Assert.NotEqual(dataset.Get<string>(DicomTag.SOPInstanceUID), clone.Get<string>(DicomTag.SOPInstanceUID));
      Assert.NotEqual(dataset.Get<string>(DicomTag.SeriesInstanceUID), clone.Get<string>(DicomTag.SeriesInstanceUID));
      Assert.NotEqual(dataset.Get<string>(DicomTag.FrameOfReferenceUID), clone.Get<string>(DicomTag.FrameOfReferenceUID));
      Assert.NotEqual(
        dataset.Get<DicomSequence>(DicomTag.ReferencedFrameOfReferenceSequence).Items[0].Get<string>(DicomTag.ReferencedFrameOfReferenceUID),
        clone.Get<DicomSequence>(DicomTag.ReferencedFrameOfReferenceSequence).Items[0].Get<string>(DicomTag.ReferencedFrameOfReferenceUID));
      Assert.NotEqual(
        dataset.Get<DicomSequence>(DicomTag.ReferencedFrameOfReferenceSequence).Items[1].Get<string>(DicomTag.ReferencedFrameOfReferenceUID),
        clone.Get<DicomSequence>(DicomTag.ReferencedFrameOfReferenceSequence).Items[1].Get<string>(DicomTag.ReferencedFrameOfReferenceUID));
    }

    private DicomDataset DeepClone_(DicomDataset dataset)
    {
      var ms = new MemoryStream();
      var target = new StreamByteTarget(ms);
      var writer = new DicomWriter(DicomTransferSyntax.ImplicitVRLittleEndian, DicomWriteOptions.Default, target);
      var walker = new DicomDatasetWalker(dataset);
      walker.Walk(writer);

      var clone = new DicomDataset();
      var reader = new DicomReader { IsExplicitVR = false };
      var byteSource = new ByteBufferByteSource(
        new MemoryByteBuffer(ms.ToArray()));
      reader.Read(byteSource, new DicomDatasetReaderObserver(clone));
      return clone;
    }
  }
}
