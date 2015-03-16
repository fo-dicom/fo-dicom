namespace DICOM__Unit_Tests_
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Reflection;
  using System.Text;

  using Dicom;
  using Dicom.IO;
  using Dicom.IO.Buffer;

  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

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
    /// Tests a "triple trip" test of serializing, de-serializing and re-serializing for a DICOM dataset containing attributes with all VRs.
    /// </summary>
    [Fact]
    public void SerializeAndDeserializeAllVRs()
    {
      var target = BuildAllTypesDataset_();

      var allVRs =
        typeof(DicomVR).GetFields(BindingFlags.Static | BindingFlags.Public)
          .Where(field => field.FieldType == typeof(DicomVR))
          .Select(field => (DicomVR)field.GetValue(null))
          .Where(vr => vr != DicomVR.NONE)
            .ToArray();

      var includedVRs = target.Select(item => item.ValueRepresentation).ToArray();

      Assert.True(allVRs.All(includedVRs.Contains));
      Assert.True(includedVRs.All(allVRs.Contains));

      VerifyJsonTripleTrip(target);
    }

		/// <summary>
    /// Tests that DS values are not mangled
    /// </summary>
    [Fact(Skip="Json.NET does not support accessing the string value of a json floating point number")]
    public void DSValuesShouldPass()
    {
      var ds = new DicomDataset { { DicomTag.ImagePositionPatient, new[] { "1.0000", "0.00", "0", "1e-3096", "1", "0.0000000" } } };
			VerifyJsonTripleTrip(ds);
    }

    /// <summary>
    /// Tests that empty strings serialize to null, and not "", per PS3.18, F.2.5 "DICOM JSON Model Null Values"
    /// </summary>
    [Fact]
    public void EmptyStringsShouldSerializeAsNull()
    {
      var ds = new DicomDataset { { DicomTag.PatientAge, new[] { "1Y", "", "3Y" } } };
      var json = JsonConvert.SerializeObject(ds, new JsonDicomConverter());
      dynamic obj = JObject.Parse(json);
      Assert.Equal("1Y", (string)obj["00101010"].Value[0]);
      Assert.Equal(null, (string)obj["00101010"].Value[1]);
      Assert.NotEqual("", (string)obj["00101010"].Value[1]);
      Assert.Equal("3Y", (string)obj["00101010"].Value[2]);
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

	    Assert.Throws<InvalidOperationException>(() => bulkData.Data);
	    Assert.Throws<InvalidOperationException>(() => bulkData.Size);

	    bulkData.GetData();

      var target = new DicomDataset();
      target.Add(new DicomOtherWord(DicomTag.PixelData, bulkData));
      var json = JsonConvert.SerializeObject(target, Formatting.Indented, new JsonDicomConverter());
      var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
      Console.WriteLine(json);
      var json2 = JsonConvert.SerializeObject(reconstituated, Formatting.Indented, new JsonDicomConverter());
      Assert.Equal(json, json2);

	    ((BulkUriByteBuffer)reconstituated.Get<IByteBuffer>(DicomTag.PixelData)).GetData();

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
      else if (a.ValueRepresentation != b.ValueRepresentation || (uint)a.Tag != (uint)b.Tag)
        return false;
      else if (a is DicomElement)
      {
        if (b is DicomElement == false)
          return false;
        else if (b is DicomStringElement && a is DicomDecimalString)
          return ((DicomDecimalString)a).Get<decimal[]>().SequenceEqual(((DicomDecimalString)b).Get<decimal[]>());
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

    private static DicomDataset BuildAllTypesDataset_()
    {
      var privateCreator = DicomDictionary.Default.GetPrivateCreator("TEST");
      return new DicomDataset {
                           new DicomLongString(new DicomTag(3, 0x0010, privateCreator), privateCreator.Creator),
                           new DicomApplicationEntity(new DicomTag(3, 0x1002, privateCreator), "AETITLE"),
                           new DicomAgeString(new DicomTag(3, 0x1003, privateCreator), "34y"),
                           new DicomAttributeTag(new DicomTag(3, 0x1004, privateCreator), new[] { DicomTag.SOPInstanceUID }),
                           new DicomCodeString(new DicomTag(3, 0x1005, privateCreator), "FOOBAR"),
                           new DicomDate(new DicomTag(3, 0x1006, privateCreator), "20000229"),
                           new DicomDecimalString(new DicomTag(3, 0x1007, privateCreator), new[] { "9876543210123457" }),
                           new DicomDateTime(new DicomTag(3, 0x1008, privateCreator), "20141231194212"),
                           new DicomFloatingPointSingle(new DicomTag(3, 0x1009, privateCreator), new[] { 0.25f }),
                           new DicomFloatingPointDouble(new DicomTag(3, 0x100a, privateCreator), new[] { Math.PI }),
                           new DicomIntegerString(new DicomTag(3, 0x100b, privateCreator), 2147483647),
                           new DicomLongString(new DicomTag(3, 0x100c, privateCreator), "(╯°□°）╯︵ ┻━┻"),
                           new DicomLongText(new DicomTag(3, 0x100d, privateCreator), "┬──┬ ノ( ゜-゜ノ)"),
                           new DicomOtherByte(new DicomTag(3, 0x100e, privateCreator), new byte[] { 1, 2, 3, 0, 255 }),
                           new DicomOtherDouble(new DicomTag(3, 0x100f, privateCreator), new double[] { 1.0, 2.5 }),
                           new DicomOtherFloat(new DicomTag(3, 0x1010, privateCreator), new float[] { 1.0f, 2.9f }),
                           new DicomOtherWord(new DicomTag(3, 0x1011, privateCreator), new ushort[] { 0xffff, 0x0000, 0x1234 }),
                           new DicomPersonName(new DicomTag(3, 0x1012, privateCreator), "Morrison-Jones^Susan^^^Ph.D."),
                           new DicomShortString(new DicomTag(3, 0x1013, privateCreator), "顔文字"),
                           new DicomSignedLong(new DicomTag(3, 0x1104, privateCreator), -65538),
                           new DicomSequence(new DicomTag(3, 0x1015, privateCreator), new [] {
                             new DicomDataset { new DicomShortText(new DicomTag(3, 0x1016, privateCreator), "ಠ_ಠ") }
                           }),
                           new DicomSignedShort(new DicomTag(3, 0x1017, privateCreator), -32768),
                           new DicomShortText(new DicomTag(3, 0x1018, privateCreator), "ಠ_ಠ"),
                           new DicomTime(new DicomTag(3, 0x1019, privateCreator), "123456"),
                           new DicomUnlimitedCharacters(new DicomTag(3, 0x101a, privateCreator), "Hmph."),
                           new DicomUniqueIdentifier(new DicomTag(3, 0x101b, privateCreator), DicomUID.CTImageStorage),
                           new DicomUnsignedLong(new DicomTag(3, 0x101c, privateCreator), 0xffffffff),
                           new DicomUnknown(new DicomTag(3, 0x101d, privateCreator), new byte[] { 1, 2, 3, 0, 255 }),
                           new DicomUniversalResource(new DicomTag(3, 0x101e, privateCreator), "http://example.com?q=1"),
                           new DicomUnsignedShort(new DicomTag(3, 0x101f, privateCreator), 0xffff),
                           new DicomUnlimitedText(new DicomTag(3, 0x1020, privateCreator), "unlimited!")
                         };
    }

		private static DicomDataset BuildAllTypesNullDataset_()
		{
			var privateCreator = DicomDictionary.Default.GetPrivateCreator("TEST");
			return new DicomDataset {
                           new DicomLongString(new DicomTag(3, 0x0010, privateCreator), privateCreator.Creator),
                           new DicomApplicationEntity(new DicomTag(3, 0x1002, privateCreator)),
                           new DicomAgeString(new DicomTag(3, 0x1003, privateCreator)),
                           new DicomAttributeTag(new DicomTag(3, 0x1004, privateCreator)),
                           new DicomCodeString(new DicomTag(3, 0x1005, privateCreator)),
                           new DicomDate(new DicomTag(3, 0x1006, privateCreator), new string[0]),
                           new DicomDecimalString(new DicomTag(3, 0x1007, privateCreator), new string[0]),
                           new DicomDateTime(new DicomTag(3, 0x1008, privateCreator), new string[0]),
                           new DicomFloatingPointSingle(new DicomTag(3, 0x1009, privateCreator)),
                           new DicomFloatingPointDouble(new DicomTag(3, 0x100a, privateCreator)),
                           new DicomIntegerString(new DicomTag(3, 0x100b, privateCreator), new string[0]),
                           new DicomLongString(new DicomTag(3, 0x100c, privateCreator)),
                           new DicomLongText(new DicomTag(3, 0x100d, privateCreator), null),
                           new DicomOtherByte(new DicomTag(3, 0x100e, privateCreator), new byte[0]),
                           new DicomOtherDouble(new DicomTag(3, 0x100f, privateCreator), new double[0]),
                           new DicomOtherFloat(new DicomTag(3, 0x1010, privateCreator), new float[0]),
                           new DicomOtherWord(new DicomTag(3, 0x1011, privateCreator), new ushort[0]),
                           new DicomPersonName(new DicomTag(3, 0x1012, privateCreator)),
                           new DicomShortString(new DicomTag(3, 0x1013, privateCreator)),
                           new DicomSignedLong(new DicomTag(3, 0x1104, privateCreator)),
                           new DicomSequence(new DicomTag(3, 0x1015, privateCreator)),
                           new DicomSignedShort(new DicomTag(3, 0x1017, privateCreator)),
                           new DicomShortText(new DicomTag(3, 0x1018, privateCreator), null),
                           new DicomTime(new DicomTag(3, 0x1019, privateCreator), new string[0]),
                           new DicomUnlimitedCharacters(new DicomTag(3, 0x101a, privateCreator), null),
                           new DicomUniqueIdentifier(new DicomTag(3, 0x101b, privateCreator), new string[0]),
                           new DicomUnsignedLong(new DicomTag(3, 0x101c, privateCreator)),
                           new DicomUnknown(new DicomTag(3, 0x101d, privateCreator)),
                           new DicomUniversalResource(new DicomTag(3, 0x101e, privateCreator), null),
                           new DicomUnsignedShort(new DicomTag(3, 0x101f, privateCreator)),
                           new DicomUnlimitedText(new DicomTag(3, 0x1020, privateCreator), null)
                         };
		}


    private static DicomDataset BuildZooDataset()
    {
      var target = new DicomDataset
                           {
                             new DicomPersonName(DicomTag.PatientName, new[] { "Anna^Pelle", null, "Olle^Jöns^Pyjamas" }),
                             { DicomTag.SOPClassUID, DicomUID.RTPlanStorage },
                             { DicomTag.SOPInstanceUID, new DicomUIDGenerator().Generate() },
                             { DicomTag.SeriesInstanceUID, new DicomUID[] { } },
                             { DicomTag.DoseType, new[] { "HEJ", null, "BLA" } },
                           };

      target.Add<DicomSequence>(DicomTag.ControlPointSequence, null);
      var beams = new[] { 1, 2, 3 }.Select(beamNumber =>
      {
        var beam = new DicomDataset();
        beam.Add(DicomTag.BeamNumber, beamNumber);
        beam.Add(DicomTag.BeamName, string.Format("Beam #{0}", beamNumber));
        return beam;
      }).ToList();
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

		[Fact]
		private static void TestKeywordSerialization()
	  {
		  var privateCreator = DicomDictionary.Default.GetPrivateCreator("TEST");
		  var ds = new DicomDataset
			         {
				         new DicomLongString(new DicomTag(3, 0x0010, privateCreator), privateCreator.Creator),
				         new DicomApplicationEntity(new DicomTag(3, 0x1002, privateCreator), "AETITLE"),
				         new DicomUnknown(new DicomTag(3, 0x1003, privateCreator), Encoding.ASCII.GetBytes("WHATISTHIS")),
				         new DicomDecimalString(DicomTag.GantryAngle, 36)
			         };
			var json = JsonConvert.SerializeObject(ds, new JsonDicomConverter(writeTagsAsKeywords: true));
			Assert.Equal("{\"00030010\":{\"vr\":\"LO\",\"Value\":[\"TEST\"]},\"00031002\":{\"vr\":\"AE\",\"Value\":[\"AETITLE\"]},\"00031003\":{\"vr\":\"UN\",\"InlineBinary\":\"V0hBVElTVEhJUw==\"},\"00030010\":{\"vr\":\"LO\",\"Value\":[\"TEST\"]},\"GantryAngle\":{\"vr\":\"DS\",\"Value\":[36]}}",
				json);
	  }

	  [Fact]
	  private static void TripleTripNulls()
	  {
		  var x = BuildAllTypesNullDataset_();
			VerifyJsonTripleTrip(x);
	  }
  }
}
