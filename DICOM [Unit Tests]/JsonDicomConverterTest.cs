namespace DICOM__Unit_Tests_
{
  using System;
  using System.IO;
  using System.Linq;

  using Dicom;
  using Dicom.IO;
  using Dicom.IO.Buffer;

  using Newtonsoft.Json;

  using Xunit;

  /// <summary>
  /// The json dicom converter test.
  /// </summary>
  public class JsonDicomConverterTest
  {
    /// <summary>
    /// Tests a "triple trip" test of serializing, de-serializing and re-serializing for a DICOM dataset containing a zoo of different types.
    /// </summary>
    [Fact]
    public void SerializeAndDeserializeZoo()
    {
      var target = BuildZooDataset();
      VerifyJsonTripleTrip(target);
    }

    /// <summary>
    ///     A test for ToString
    /// </summary>
    [Fact]
    public void TestKeywordDeserialization()
    {
      const string json = "{\"PatientName\": { \"vr\": \"PN\", \"Value\": [{ \"Alphabetic\": \"Kalle\" }] } }";
      var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
      Assert.Equal("Kalle", reconstituated.Get<string>(DicomTag.PatientName));
    }

    /// <summary>
    /// Test deserializing a dicom dataset containing a bulk data URI.
    /// </summary>
    [Fact]
    public void TestBulkDataUri()
    {
      const string json = @"
{
  ""7FE00010"": {
    ""vr"": ""OW"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
      var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
      var buffer = reconstituated.Get<IByteBuffer>(DicomTag.PixelData);
      Assert.True(buffer is BulkUriByteBuffer);
      Assert.Equal("http://www.example.com/testdicom.dcm", ((BulkUriByteBuffer)buffer).BulkDataUri);
    }

    /// <summary>
    /// Run the examples from DICOM Standard PS 3.18, section F.2.1.1.2.
    /// </summary>
    [Fact]
    public void RunExamplesFromPS3_18_F_2_1_1_2()
    {
      var json = @"
[
  {
     ""0020000D"": {
      ""vr"": ""UI"",
      ""Value"": [ ""1.2.392.200036.9116.2.2.2.1762893313.1029997326.945873"" ]
    }
  },
  {
    ""0020000D"" : {
      ""vr"": ""UI"",
      ""Value"": [ ""1.2.392.200036.9116.2.2.2.2162893313.1029997326.945876"" ]
    }
  }
]";

      var reconstituated = JsonConvert.DeserializeObject<DicomDataset[]>(json, new JsonDicomConverter());
      Assert.Equal("1.2.392.200036.9116.2.2.2.1762893313.1029997326.945873", reconstituated[0].Get<DicomUID>(0x0020000d).UID);
      Assert.Equal("1.2.392.200036.9116.2.2.2.2162893313.1029997326.945876", reconstituated[1].Get<DicomUID>(0x0020000d).UID);
    }

    /// <summary>
    /// Test round-tripping a dicom dataset containing a bulk uri byte buffer.
    /// </summary>
    [Fact]
    public void BulkDataRoundTrip()
    {
      var target = new DicomDataset();
      target.Add(new DicomOtherWord(DicomTag.PixelData, new BulkUriByteBuffer("http://www.example.com/bulk.dcm")));
      VerifyJsonTripleTrip(target);
    }

    /// <summary>
    /// The bulk data read.
    /// </summary>
    [Fact]
    public void BulkDataRead()
    {
      var path = Path.GetFullPath("test.txt");
      var bulkData = new BulkUriByteBuffer("file:" + path);

      var target = new DicomDataset();
      target.Add(new DicomOtherWord(DicomTag.PixelData, bulkData));
      var json = JsonConvert.SerializeObject(target, Formatting.Indented, new JsonDicomConverter());
      var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
      Console.WriteLine(json);
      var json2 = JsonConvert.SerializeObject(reconstituated, Formatting.Indented, new JsonDicomConverter());
      Assert.Equal(json, json2);
      Assert.True(ValueEquals(target, reconstituated));

      byte[] expectedPixelData = File.ReadAllBytes("test.txt");

      Assert.Equal(target.Get<IByteBuffer>(DicomTag.PixelData).Size, (uint)expectedPixelData.Length);
      Assert.True(target.Get<byte[]>(DicomTag.PixelData).SequenceEqual(expectedPixelData));
      Assert.Equal(reconstituated.Get<IByteBuffer>(DicomTag.PixelData).Size, (uint)expectedPixelData.Length);
      Assert.True(reconstituated.Get<byte[]>(DicomTag.PixelData).SequenceEqual(expectedPixelData));
    }

    private static bool ValueEquals(DicomDataset a, DicomDataset b)
    {
      if (a == null || b == null)
        return a == b;
      else if (a == b)
        return true;
      else
        return a.Zip(b, (x, y) => ValueEquals(x, y)).All(x => x);
    }

    private static bool ValueEquals(DicomItem a, DicomItem b)
    {
      if (a == null || b == null)
        return a == b;
      else if (a == b)
        return true;
      else if (a.ValueRepresentation != b.ValueRepresentation || a.Tag != b.Tag)
        return false;
      else if (a is DicomElement)
      {
        if (b is DicomElement == false)
          return false;
        else
          return ValueEquals(((DicomElement)a).Buffer, ((DicomElement)b).Buffer);
      }
      else if (a is DicomSequence)
      {
        if (b is DicomSequence == false)
          return false;
        else
          return ((DicomSequence)a).Items.Zip(((DicomSequence)b).Items, (x, y) => ValueEquals(x, y)).All(x => x);
      }
      else if (a is DicomFragmentSequence)
      {
        if (b is DicomFragmentSequence == false)
          return false;
        else
          return ((DicomFragmentSequence)a).Fragments.Zip(((DicomFragmentSequence)b).Fragments, (x, y) => ValueEquals(x, y)).All(x => x);
      }
      else
        return a.Equals(b);
    }

    private static bool ValueEquals(IByteBuffer a, IByteBuffer b)
    {
      if (a == null || b == null)
        return a == b;
      else if (a == b)
        return true;
      else if (a.IsMemory)
      {
        return b.IsMemory && a.Data.SequenceEqual(b.Data);
      }
      else if (a is BulkUriByteBuffer)
      {
        if (b is BulkUriByteBuffer)
          return ((BulkUriByteBuffer)a).BulkDataUri == ((BulkUriByteBuffer)b).BulkDataUri;
        else
          return false;
      }
      else if (a is EmptyBuffer && b is EmptyBuffer)
        return true;
      else if (a is StreamByteBuffer && b is StreamByteBuffer)
      {
        var asbb = (StreamByteBuffer)a;
        var bsbb = (StreamByteBuffer)b;
        if (asbb.Stream == null || bsbb.Stream == null)
        {
          return asbb.Stream == bsbb.Stream;
        }
        else
        {
          return asbb.Position == bsbb.Position && asbb.Size == bsbb.Size && asbb.Stream.Equals(bsbb.Stream);
        }
      }
      else if (a is CompositeByteBuffer && b is CompositeByteBuffer)
      {
        return ((CompositeByteBuffer)a).Buffers.Zip(((CompositeByteBuffer)b).Buffers, ValueEquals).All(x => x);
      }
      else
      {
        return a.Equals(b);
      }
    }

    private static DicomDataset BuildZooDataset()
    {
      var target = new DicomDataset
                           {
                             new DicomPersonName(DicomTag.PatientName, new[] { "Anna^Pelle", null, "Olle^Jöns^Pyjamas" }),
                             { DicomTag.SOPClassUID, DicomUID.RTPlanStorage },
                             { DicomTag.SOPInstanceUID, new DicomUIDGenerator().Generate() },
                             { DicomTag.SeriesInstanceUID, new DicomUID[] { } },
                             { DicomTag.DoseType, new[] { "HEJ", null, "BLA" } }
                           };

      target.Add<DicomSequence>(DicomTag.ControlPointSequence, null);
      var beams = new[] { 1, 2, 3 }.Select(beamNumber =>
      {
        var beam = new DicomDataset();
        beam.Add(DicomTag.BeamNumber, beamNumber);
        beam.Add(DicomTag.BeamName, string.Format("Beam #{0}", beamNumber));
        return beam;
      }
          ).ToList();
      beams.Insert(1, null);
      target.Add(DicomTag.BeamSequence, beams.ToArray());
      return target;
    }

    /// <summary>
    /// Serializes originalDataset to JSON, deserializes, and re-serializes. 
    /// Verifies that both datasets are equal, and both json serializations are equal.
    /// </summary>
    private static void VerifyJsonTripleTrip(DicomDataset originalDataset)
    {
      var json = JsonConvert.SerializeObject(originalDataset, new JsonDicomConverter());
      var reconstituatedDataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
      var json2 = JsonConvert.SerializeObject(reconstituatedDataset, new JsonDicomConverter());
      Assert.Equal(json, json2);
      Assert.True(ValueEquals(originalDataset, reconstituatedDataset));
    }
  }
}