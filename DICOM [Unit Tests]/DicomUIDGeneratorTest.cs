using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DICOM__Unit_Tests_
{
  using System.CodeDom.Compiler;

  using Dicom;

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
        new[] { new DicomDataset {
          { DicomTag.ReferencedFrameOfReferenceUID, uidGenerator.Generate() },
          { DicomTag.ReferencedFrameOfReferenceUID, uidGenerator.Generate() }
      } });

      var clone = dataset.Clone();

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
  }
}
