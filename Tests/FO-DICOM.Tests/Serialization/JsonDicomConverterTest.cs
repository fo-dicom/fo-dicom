// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using FellowOakDicom.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Serialization
{

    /// <summary>
    /// The json dicom converter test.
    /// </summary>
    [Collection(TestCollections.WithHttpClient)]
    public class JsonDicomConverterTest
    {
        private readonly ITestOutputHelper _output;
        private readonly HttpClientFixture _httpClientFixture;

        public JsonDicomConverterTest(ITestOutputHelper output, HttpClientFixture httpClientFixture)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _httpClientFixture = httpClientFixture ?? throw new ArgumentNullException(nameof(httpClientFixture));
        }

        /// <summary>
        /// Tests a "triple trip" test of serializing, de-serializing and re-serializing for a DICOM dataset containing a zoo of different types.
        /// </summary>
        [Fact]
        public void SerializeAndDeserializeZoo()
        {
            var target = BuildZooDataset();
            VerifyJsonTripleTrip(target);
        }

        private double TimeCall(int numCalls, Action call)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < numCalls; i++) call();

            stopWatch.Stop();

            var totalElapsedMilliseconds = stopWatch.ElapsedMilliseconds;
            var millisecondsPerCall = totalElapsedMilliseconds / (double)numCalls;

            return millisecondsPerCall;
        }

        [Fact]
        public void DeserializeISAsString()
        {
            // in DICOM Standard PS3.18 F.2.3.1 now VRs DS, IS SV and UV may be either number or string
            var json = @"
            {
                ""00201206"": {
                    ""vr"":""IS"",
                    ""Value"":[311]
                },
                ""00201209"": {
                    ""vr"":""IS"",
                    ""Value"":[""311""]
                },
                ""00201204"": {
                    ""vr"":""IS"",
                    ""Value"":[]
                }
            }";
            var dataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            Assert.NotNull(dataset);
            Assert.Equal(311, dataset.GetSingleValue<int>(DicomTag.NumberOfStudyRelatedSeries));
            Assert.Equal(311, dataset.GetSingleValue<int>(DicomTag.NumberOfSeriesRelatedInstances));
            Assert.Equal(0, dataset.GetValueCount(DicomTag.NumberOfPatientRelatedInstances));
        }

        [Fact]
        public void DeserializeDSAsString()
        {
            // in DICOM Standard PS3.18 F.2.3.1 now VRs DS, IS SV and UV may be either number or string
            var json = @"
            {
                ""00101030"": {
                    ""vr"":""DS"",
                    ""Value"":[84.5]
                },
                ""00101020"": {
                    ""vr"":""DS"",
                    ""Value"":[""174.5""]
                }

            }";
            var dataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            Assert.NotNull(dataset);
            Assert.Equal(84.5m, dataset.GetSingleValue<decimal>(DicomTag.PatientWeight));
            Assert.Equal(174.5m, dataset.GetSingleValue<decimal>(DicomTag.PatientSize));
        }


        [Fact]
        public void ParseEmptyValues()
        {
            var json = @"
            {
                ""00082111"": {
                    ""vr"": ""ST""
                 }
            } ";
            var header = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            Assert.NotNull(header.GetDicomItem<DicomShortText>(DicomTag.DerivationDescription));
        }

        [Fact]
        public void ParseFloatingPointNaNValues()
        {
            var json = @"
            {
                ""00720076"": {
                    ""vr"": ""FL"",
                     ""Value"": [""NaN""]
                 }
            } ";
            var tagValue = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            Assert.NotNull(tagValue.GetDicomItem<DicomFloatingPointSingle>(DicomTag.SelectorFLValue));
        }

        [Fact]
        public void TimeParseTag()
        {
            var millisecondsPerCallA = TimeCall(100, () =>
            {
                foreach (var kw in DicomDictionary.Default.Select(dde => dde.Keyword))
                {
                    var tag = DicomDictionary.Default[kw];
                    Assert.NotNull(tag);
                }
            });

            var millisecondsPerCallB = TimeCall(3, () =>
            {
                foreach (var kw in DicomDictionary.Default.Select(dde => dde.Keyword))
                {
                    var tag = DicomDictionary.Default.FirstOrDefault(dde => dde.Keyword == kw);
                    Assert.NotNull(tag);
                }
            });

            var millisecondsPerCallC = TimeCall(100, () =>
            {
                var dict = DicomDictionary.Default.ToDictionary(dde => dde.Keyword, dde => dde.Tag);
                foreach (var kw in DicomDictionary.Default.Select(dde => dde.Keyword))
                {
                    var tag = dict[kw];
                    Assert.NotNull(tag);
                }
            });

            var millisecondsPerCallD = TimeCall(100, () =>
            {
                foreach (var kw in DicomDictionary.Default.Select(dde => dde.Keyword))
                {
                    var tag = JsonDicomConverter.ParseTag(kw);
                    Assert.NotNull(tag);
                }
            });

            _output.WriteLine(
                $"Looking up keyword with pre-built dictionary: {millisecondsPerCallA} ms for {DicomDictionary.Default.Count()} tests");

            _output.WriteLine(
                $"Looking up keyword with LINQ: {millisecondsPerCallB} ms for {DicomDictionary.Default.Count()} tests");

            _output.WriteLine(
                $"Looking up keyword with one dictionary built for all calls: {millisecondsPerCallC} ms for {DicomDictionary.Default.Count()} tests");

            _output.WriteLine(
                $"Parsing tag with JsonDicomConverter.ParseTag: {millisecondsPerCallD} ms for {DicomDictionary.Default.Count()} tests");

            Assert.InRange(millisecondsPerCallD / (1 + millisecondsPerCallC), 0, 20);

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

            Assert.Empty(allVRs.Except(includedVRs));
            Assert.Empty(includedVRs.Except(allVRs));

            VerifyJsonTripleTrip(target);
        }

        /// <summary>
        /// Tests that DS values are not mangled
        /// </summary>
        [Fact]
        public void DecimalStringValuesShouldPass()
        {
            var originalDataset = new DicomDataset {
                { DicomTag.ImagePositionPatient, new[] { "1.0000", "0.00", "0" } },
                { DicomTag.ImagePlanePixelSpacing, new[] { "10.0", "10."} },
                { DicomTag.ImageOrientationPatient, new[] { "1e-3096", "1", "0.0000000", ".03", "-.03", "-0" } }
            };

            var json = JsonConvert.SerializeObject(originalDataset, new JsonDicomConverter());
            var reconstituatedDataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());

            Assert.True(ValueEquals(originalDataset, reconstituatedDataset));
            /* This test only verifies the DicomDatasets and not the serialized json strings, because
             * deserialization parses the DS as floats and then when re-serializing the
             * value after serialization:
             * {"00200032":{"vr":"DS","Value":[1.0000,0.00,0,0.0000000000000000000000000000,1,0.0000000,0.03,-0.03]}}
             * value after deserialization and then again serialization:
             * {"00200032":{"vr":"DS","Value":[1,0,0,0,1,0,0.03,-0.03]}}
             * Is this ok behavior but string comparison fails
             */
        }

        /// <summary>Verify that PrivateCreators are set for the tags in a deserialized dataset.</summary>
        [Fact]
        public void Deserialize_PrivateCreators_AreSet()
        {
            var originalDataset = BuildAllTypesDataset_();

            ValidatePrivateCreatorsExist_(originalDataset);

            var json = JsonConvert.SerializeObject(originalDataset, new JsonDicomConverter());
            var reconstituatedDataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());

            ValidatePrivateCreatorsExist_(reconstituatedDataset);
        }

        private static void ValidatePrivateCreatorsExist_(DicomDataset dataset)
        {
            Assert.All(
                dataset,
                item =>
                    {
                        if ((item.Tag.Element & 0xff00) != 0) Assert.False(string.IsNullOrWhiteSpace(item.Tag.PrivateCreator?.Creator));
                        Assert.NotNull(item.Tag.DictionaryEntry);
                        if (item.ValueRepresentation == DicomVR.SQ) Assert.All(((DicomSequence)item).Items, ds => ValidatePrivateCreatorsExist_(ds));
                    });
        }

        /// <summary>
        /// Tests that DS values that are not proper json numbers get fixed on serialization.
        /// </summary>
        [Fact]
        public void NonJsonDecimalStringValuesGetFixed()
        {
            var ds = new DicomDataset { ValidateItems = false };
            // have to turn off validation, since we want to add invalid DS values
            ds.Add(new DicomDecimalString(DicomTag.ImagePositionPatient, new[] { "   001 ", " +13 ", "+000000.0000E+00", "-000000.0000E+00" }));
            var json = JsonConvert.SerializeObject(ds, new JsonDicomConverter());
            dynamic obj = JObject.Parse(json);

            Assert.Equal("1", (string)obj["00200032"].Value[0]);
            Assert.Equal("13", (string)obj["00200032"].Value[1]);
            Assert.Equal("0", (string)obj["00200032"].Value[2]);

            // Would be nice, but Json.NET mangles the parsed json. Verify string instead:
            Assert.Equal("{\"00200032\":{\"vr\":\"DS\",\"Value\":[1,13,0.0000,0.0000]}}", json);
        }

        /// <summary>
        /// Tests that empty strings serialize to null, and not "", per PS3.18, F.2.5 "DICOM JSON Model Null Values"
        /// </summary>
        [Fact]
        public void EmptyStringsShouldSerializeAsNull()
        {
            var ds = new DicomDataset { ValidateItems = false };
            // have to turn off validation, since DicomTag.PatientAge has Value Multiplicity 1, so
            // this dataset cannot be constructed without validation exception
            ds.Add(new DicomAgeString(DicomTag.PatientAge, new[] { "1Y", "", "3Y" }));
            var json = JsonConvert.SerializeObject(ds, new JsonDicomConverter());
            dynamic obj = JObject.Parse(json);
            Assert.Equal("1Y", (string)obj["00101010"].Value[0]);
            Assert.Null((string)obj["00101010"].Value[1]);
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
            Assert.Equal("Kalle", reconstituated.GetString(DicomTag.PatientName));
        }

        [Theory]
        [InlineData("{\"PatientName\": { \"vr\": \"PN\", \"Value\": [{ \"Alphabetic\": \"Kalle\", \"Ideographic\": \"ideo\", \"Phonetic\": \"pho\" }] } }", "Kalle=ideo=pho")]
        [InlineData("{\"PatientName\": { \"vr\": \"PN\", \"Value\": [{ \"Alphabetic\": \"Kalle\",  \"Phonetic\": \"pho\" }] } }", "Kalle==pho")]
        [InlineData("{\"PatientName\": { \"vr\": \"PN\", \"Value\": [{ \"Alphabetic\": \"Kalle\", \"Ideographic\": \"ideo\" }] } }", "Kalle=ideo")]
        [InlineData("{\"PatientName\": { \"vr\": \"PN\", \"Value\": [{ \"Ideographic\": \"ideo\"}] } }", "=ideo")]
        public void TestJsonToDicomPNConverstion(string json, string expectedResult)
        {
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            Assert.Equal(expectedResult, reconstituated.GetString(DicomTag.PatientName));
        }

        [Theory]
        [InlineData("{\"00100010\":{\"vr\":\"PN\",\"Value\":[{\"Alphabetic\":\"Kalle\",\"Ideographic\":\"ideo\",\"Phonetic\":\"pho\"}]}}", "Kalle=ideo=pho")]
        [InlineData("{\"00100010\":{\"vr\":\"PN\",\"Value\":[{\"Alphabetic\":\"Kalle\",\"Phonetic\":\"pho\"}]}}", "Kalle==pho")]
        [InlineData("{\"00100010\":{\"vr\":\"PN\",\"Value\":[{\"Alphabetic\":\"Kalle\",\"Ideographic\":\"ideo\"}]}}", "Kalle=ideo")]
        [InlineData("{\"00100010\":{\"vr\":\"PN\",\"Value\":[{\"Ideographic\":\"ideo\"}]}}", "=ideo")]
        public void TestDicomToJsonPNConversion(string expectedJson, string value)
        {
            var originalDataset = new DicomDataset {
                { DicomTag.PatientName, value }
            };

            var json = JsonConvert.SerializeObject(originalDataset, new JsonDicomConverter());

            Assert.Equal(expectedJson, json);
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
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.PixelData).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=DS.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForDS()
        {
            const string json = @"
{
  ""00720072"": {
    ""vr"": ""DS"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorDSValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=FD.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForFD()
        {
            const string json = @"
{
  ""00720074"": {
    ""vr"": ""FD"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorFDValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=FL.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForFL()
        {
            const string json = @"
{
  ""00720076"": {
    ""vr"": ""FL"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorFLValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=IS.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForIS()
        {
            const string json = @"
{
  ""00720064"": {
    ""vr"": ""IS"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorISValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=LT.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForLT()
        {
            const string json = @"
{
  ""00720068"": {
    ""vr"": ""LT"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorLTValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=SL.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForSL()
        {
            const string json = @"
{
  ""0072007C"": {
    ""vr"": ""SL"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorSLValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=SS.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForSS()
        {
            const string json = @"
{
  ""0072007E"": {
    ""vr"": ""SS"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorSSValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=ST.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForST()
        {
            const string json = @"
{
  ""0072006E"": {
    ""vr"": ""ST"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorSTValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=UC.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForUC()
        {
            const string json = @"
{
  ""0072006F"": {
    ""vr"": ""UC"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorUCValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=UL.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForUL()
        {
            const string json = @"
{
  ""00720078"": {
    ""vr"": ""UL"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorULValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=US.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForUS()
        {
            const string json = @"
{
  ""0072007A"": {
    ""vr"": ""US"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetDicomItem<DicomElement>(DicomTag.SelectorUSValue).Buffer as IBulkDataUriByteBuffer;
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
        }

        /// <summary>
        /// Test deserializing a dicom dataset containing a bulk data URI with VR=UT.
        /// </summary>
        [Fact]
        public void TestBulkDataUriForUT()
        {
            const string json = @"
{
  ""00720070"": {
    ""vr"": ""UT"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.GetSingleValue<IBulkDataUriByteBuffer>(DicomTag.SelectorUTValue);
            Assert.NotNull(buffer);
            Assert.Equal("http://www.example.com/testdicom.dcm", buffer.BulkDataUri);
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
            Assert.Equal("1.2.392.200036.9116.2.2.2.1762893313.1029997326.945873", reconstituated[0].GetSingleValue<DicomUID>(0x0020000d).UID);
            Assert.Equal("1.2.392.200036.9116.2.2.2.2162893313.1029997326.945876", reconstituated[1].GetSingleValue<DicomUID>(0x0020000d).UID);
        }

        [Fact]
        public void ParseExampleJsonFromDicomNemaOrg()
        {
            var json = _jsonExampleFromDicomNemaOrg;
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset[]>(json, new JsonDicomConverter());
            Assert.Equal(new DateTime(2013, 4, 9), reconstituated[0].GetSingleValue<DateTime>(DicomTag.StudyDate));
            Assert.Equal("^Bob^^Dr.", reconstituated[0].GetSingleValue<string>(DicomTag.ReferringPhysicianName));
        }

        /// <summary>
        /// vr is not first position of json properties.
        /// </summary>
        [Fact]
        public void VrIsNotFirstPosition()
        {
            var json = @"
[
  {
     ""0020000D"": {
      ""Value"": [ ""1.2.392.200036.9116.2.2.2.1762893313.1029997326.945873"" ],
      ""vr"": ""UI""
    }
  },
  {
    ""0020000D"" : {
      ""Value"": [ ""1.2.392.200036.9116.2.2.2.2162893313.1029997326.945876"" ],
      ""vr"": ""UI""
    }
  }
]";

            var reconstituated = JsonConvert.DeserializeObject<DicomDataset[]>(json, new JsonDicomConverter());
            Assert.Equal("1.2.392.200036.9116.2.2.2.1762893313.1029997326.945873", reconstituated[0].GetSingleValue<DicomUID>(0x0020000d).UID);
            Assert.Equal("1.2.392.200036.9116.2.2.2.2162893313.1029997326.945876", reconstituated[1].GetSingleValue<DicomUID>(0x0020000d).UID);
        }

        /// <summary>
        /// Test round-tripping a dicom dataset containing a bulk uri byte buffer.
        /// </summary>
        [Fact]
        public void BulkDataRoundTrip()
        {
            var target = new DicomDataset
            {
                new DicomOtherWord(DicomTag.PixelData, new BulkDataUriByteBuffer("http://www.example.com/bulk.dcm"))
            };
            VerifyJsonTripleTrip(target);
        }

        /// <summary>
        /// Test round-tripping a dicom dataset containing a bulk uri byte buffer.
        /// </summary>
        [Fact]
        public void EncodedTextRoundTrip()
        {
            var target = new DicomDataset
            {
                new DicomCodeString(DicomTag.SpecificCharacterSet, "ISO_IR 192"),
                new DicomLongText(DicomTag.StudyDescription, "Label®")
            };
            VerifyJsonTripleTrip(target);
        }

        [Fact]
        public void TestDecimalStringConversion()
        {
            var ds = new DicomDataset().NotValidated();
            ds.Add(new DicomDecimalString(DicomTag.PatientWeight, "0\0"));

            VerifyJsonTripleTrip(ds);
        }

        private async Task DownloadBulkDataAsync(BulkDataUriByteBuffer bulkData)
        {
            var uri = new UriBuilder(bulkData.BulkDataUri);
            switch (uri.Scheme)
            {
                case "file":
                    bulkData.Data = File.ReadAllBytes(uri.Path);
                    break;
                case "http":
                case "https":
                    var httpClient = _httpClientFixture.HttpClient;
                    bulkData.Data = await httpClient.GetByteArrayAsync(bulkData.BulkDataUri);
                    return;
            }
        }

        /// <summary>
        /// The bulk data read.
        /// </summary>
        [Fact]
        public async Task BulkDataRead()
        {
            File.WriteAllText("test.txt", "xxx!");
            var path = Path.GetFullPath("test.txt");
            var bulkData = new BulkDataUriByteBuffer(new Uri(path).AbsoluteUri);

            Assert.Throws<InvalidOperationException>(() => bulkData.Data);
            Assert.Throws<InvalidOperationException>(() => bulkData.Size);

            var target = new DicomDataset { new DicomOtherWord(DicomTag.PixelData, bulkData) };
            var json = JsonConvert.SerializeObject(target, Formatting.Indented, new JsonDicomConverter());
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var json2 = JsonConvert.SerializeObject(reconstituated, Formatting.Indented, new JsonDicomConverter());
            Assert.Equal(json, json2);

            await DownloadBulkDataAsync(reconstituated.GetDicomItem<DicomElement>(DicomTag.PixelData).Buffer as BulkDataUriByteBuffer);
            await DownloadBulkDataAsync(bulkData);

            Assert.True(ValueEquals(target, reconstituated));

            byte[] expectedPixelData = File.ReadAllBytes("test.txt");

            File.Delete("test.txt");

            Assert.Equal(target.GetDicomItem<DicomElement>(DicomTag.PixelData).Buffer.Size, (uint)expectedPixelData.Length);
            Assert.True(target.GetValues<byte>(DicomTag.PixelData).SequenceEqual(expectedPixelData));
            Assert.Equal(reconstituated.GetDicomItem<DicomElement>(DicomTag.PixelData).Buffer.Size, (uint)expectedPixelData.Length);
            Assert.True(reconstituated.GetValues<byte>(DicomTag.PixelData).SequenceEqual(expectedPixelData));
        }

        private static bool ValueEquals(DicomDataset a, DicomDataset b)
        {
            if (a == null || b == null)
            {
                return a == b;
            }
            else if (a == b)
            {
                return true;
            }
            else
            {
                return a.Zip(b, ValueEquals).All(x => x);
            }
        }

        private static bool ValueEquals(DicomItem a, DicomItem b)
        {
            if (a == null || b == null)
            {
                return a == b;
            }
            else if (a == b)
            {
                return true;
            }
            else if (a.ValueRepresentation != b.ValueRepresentation || (uint)a.Tag != (uint)b.Tag)
            {
                return false;
            }
            else if (a is DicomElement elementA)
            {
                if (!(b is DicomElement elementB))
                {
                    return false;
                }
                else if (b is DicomDecimalString decimalStringB
                    && a is DicomDecimalString decimalStringA)
                {
                    return decimalStringA.Get<decimal[]>().SequenceEqual(decimalStringB.Get<decimal[]>());
                }
                else
                {
                    return ValueEquals(elementA.Buffer, elementB.Buffer);
                }
            }
            else if (a is DicomSequence sequenceA)
            {
                if (!(b is DicomSequence sequenceB))
                {
                    return false;
                }
                else
                {
                    return sequenceA.Items.Zip(sequenceB.Items, ValueEquals).All(x => x);
                }
            }
            else if (a is DicomFragmentSequence fragmentSequenceA)
            {
                if (!(b is DicomFragmentSequence fragmentSequenceB))
                {
                    return false;
                }
                else
                {
                    return fragmentSequenceA.Fragments.Zip(fragmentSequenceB.Fragments, ValueEquals).All(x => x);
                }
            }
            else
            {
                return a.Equals(b);
            }
        }

        private static bool ValueEquals(IByteBuffer a, IByteBuffer b)
        {
            if (a == null || b == null)
            {
                return a == b;
            }
            else if (a == b)
            {
                return true;
            }
            else if (a.IsMemory)
            {
                return b.IsMemory && a.Data.SequenceEqual(b.Data);
            }
            else if (a is IBulkDataUriByteBuffer bufferA)
            {
                if (b is IBulkDataUriByteBuffer bufferB)
                    return bufferA.BulkDataUri == bufferB.BulkDataUri;
                else
                    return false;
            }
            else if (a is EmptyBuffer && b is EmptyBuffer)
            {
                return true;
            }
            else if (a is StreamByteBuffer asbb && b is StreamByteBuffer bsbb)
            {
                if (asbb.Stream == null || bsbb.Stream == null)
                {
                    return asbb.Stream == bsbb.Stream;
                }
                else
                {
                    return asbb.Position == bsbb.Position && asbb.Size == bsbb.Size && asbb.Stream.Equals(bsbb.Stream);
                }
            }
            else if (a is CompositeByteBuffer acbb && b is CompositeByteBuffer bcbb)
            {
                return acbb.Buffers.Zip(bcbb.Buffers, ValueEquals).All(x => x);
            }
            else
            {
                return a.Equals(b);
            }
        }

        private static DicomDataset BuildAllTypesDataset_()
        {
            var privateCreator = DicomDictionary.Default.GetPrivateCreator("TEST");
            var privDict = DicomDictionary.Default[privateCreator];

            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx02"), "Private Tag 02", "PrivateTag02", DicomVM.VM_1, false, DicomVR.AE));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx03"), "Private Tag 03", "PrivateTag03", DicomVM.VM_1, false, DicomVR.AS));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx04"), "Private Tag 04", "PrivateTag04", DicomVM.VM_1, false, DicomVR.AT));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx05"), "Private Tag 05", "PrivateTag05", DicomVM.VM_1, false, DicomVR.CS));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx06"), "Private Tag 06", "PrivateTag06", DicomVM.VM_1, false, DicomVR.DA));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx07"), "Private Tag 07", "PrivateTag07", DicomVM.VM_1, false, DicomVR.DS));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx08"), "Private Tag 08", "PrivateTag08", DicomVM.VM_1, false, DicomVR.DT));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx09"), "Private Tag 09", "PrivateTag09", DicomVM.VM_1, false, DicomVR.FL));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx0a"), "Private Tag 0a", "PrivateTag0a", DicomVM.VM_1, false, DicomVR.FD));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx0b"), "Private Tag 0b", "PrivateTag0b", DicomVM.VM_1, false, DicomVR.IS));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx0c"), "Private Tag 0c", "PrivateTag0c", DicomVM.VM_1, false, DicomVR.LO));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx0d"), "Private Tag 0d", "PrivateTag0d", DicomVM.VM_1, false, DicomVR.LT));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx0e"), "Private Tag 0e", "PrivateTag0e", DicomVM.VM_1, false, DicomVR.OB));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx0f"), "Private Tag 0f", "PrivateTag0f", DicomVM.VM_1, false, DicomVR.OD));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx10"), "Private Tag 10", "PrivateTag10", DicomVM.VM_1, false, DicomVR.OF));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx11"), "Private Tag 11", "PrivateTag11", DicomVM.VM_1, false, DicomVR.OL));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx12"), "Private Tag 12", "PrivateTag12", DicomVM.VM_1, false, DicomVR.OW));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx13"), "Private Tag 13", "PrivateTag13", DicomVM.VM_1, false, DicomVR.OV));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx14"), "Private Tag 14", "PrivateTag14", DicomVM.VM_1, false, DicomVR.PN));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx15"), "Private Tag 15", "PrivateTag15", DicomVM.VM_1, false, DicomVR.SH));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx16"), "Private Tag 16", "PrivateTag16", DicomVM.VM_1, false, DicomVR.SL));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx17"), "Private Tag 17", "PrivateTag17", DicomVM.VM_1, false, DicomVR.SQ));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx18"), "Private Tag 18", "PrivateTag18", DicomVM.VM_1, false, DicomVR.ST));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx19"), "Private Tag 19", "PrivateTag19", DicomVM.VM_1, false, DicomVR.SS));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx1a"), "Private Tag 1a", "PrivateTag1a", DicomVM.VM_1, false, DicomVR.ST));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx1b"), "Private Tag 1b", "PrivateTag1b", DicomVM.VM_1, false, DicomVR.SV));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx1c"), "Private Tag 1c", "PrivateTag1c", DicomVM.VM_1, false, DicomVR.TM));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx1d"), "Private Tag 1d", "PrivateTag1d", DicomVM.VM_1, false, DicomVR.UC));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx1e"), "Private Tag 1e", "PrivateTag1e", DicomVM.VM_1, false, DicomVR.UI));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx1f"), "Private Tag 1f", "PrivateTag1f", DicomVM.VM_1, false, DicomVR.UL));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx20"), "Private Tag 20", "PrivateTag20", DicomVM.VM_1, false, DicomVR.UN));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx21"), "Private Tag 21", "PrivateTag21", DicomVM.VM_1, false, DicomVR.UR));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx22"), "Private Tag 22", "PrivateTag22", DicomVM.VM_1, false, DicomVR.US));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx23"), "Private Tag 23", "PrivateTag23", DicomVM.VM_1, false, DicomVR.UT));
            privDict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("0003", "xx24"), "Private Tag 24", "PrivateTag24", DicomVM.VM_1, false, DicomVR.UV));

            var ds = new DicomDataset();

            ds.Add(new DicomApplicationEntity(ds.GetPrivateTag(new DicomTag(3, 0x0002, privateCreator)), "AETITLE"));
            ds.Add(new DicomAgeString(ds.GetPrivateTag(new DicomTag(3, 0x0003, privateCreator)), "034Y"));
            ds.Add(new DicomAttributeTag(ds.GetPrivateTag(new DicomTag(3, 0x0004, privateCreator)), new[] { DicomTag.SOPInstanceUID }));
            ds.Add(new DicomCodeString(ds.GetPrivateTag(new DicomTag(3, 0x0005, privateCreator)), "FOOBAR"));
            ds.Add(new DicomDate(ds.GetPrivateTag(new DicomTag(3, 0x0006, privateCreator)), "20000229"));
            ds.Add(new DicomDecimalString(ds.GetPrivateTag(new DicomTag(3, 0x0007, privateCreator)), new[] { "9876543210123457" }));
            ds.Add(new DicomDateTime(ds.GetPrivateTag(new DicomTag(3, 0x0008, privateCreator)), "20141231194212"));
            ds.Add(new DicomFloatingPointSingle(ds.GetPrivateTag(new DicomTag(3, 0x0009, privateCreator)), new[] { 0.25f }));
            ds.Add(new DicomFloatingPointDouble(ds.GetPrivateTag(new DicomTag(3, 0x000a, privateCreator)), new[] { Math.PI }));
            ds.Add(new DicomIntegerString(ds.GetPrivateTag(new DicomTag(3, 0x000b, privateCreator)), 2147483647));
            ds.Add(new DicomLongString(ds.GetPrivateTag(new DicomTag(3, 0x000c, privateCreator)), "(╯°□°）╯︵ ┻━┻"));
            ds.Add(new DicomLongText(ds.GetPrivateTag(new DicomTag(3, 0x000d, privateCreator)), "┬──┬ ノ( ゜-゜ノ)"));
            ds.Add(new DicomOtherByte(ds.GetPrivateTag(new DicomTag(3, 0x000e, privateCreator)), new byte[] { 1, 2, 3, 0, 255 }));
            ds.Add(new DicomOtherDouble(ds.GetPrivateTag(new DicomTag(3, 0x000f, privateCreator)), new double[] { 1.0, 2.5 }));
            ds.Add(new DicomOtherFloat(ds.GetPrivateTag(new DicomTag(3, 0x0010, privateCreator)), new float[] { 1.0f, 2.9f }));
            ds.Add(new DicomOtherLong(ds.GetPrivateTag(new DicomTag(3, 0x0011, privateCreator)), new uint[] { 0xffffffff, 0x00000000, 0x12345678 }));
            ds.Add(new DicomOtherWord(ds.GetPrivateTag(new DicomTag(3, 0x0012, privateCreator)), new ushort[] { 0xffff, 0x0000, 0x1234 }));
            ds.Add(new DicomOtherVeryLong(ds.GetPrivateTag(new DicomTag(3, 0x0013, privateCreator)), new ulong[] { ulong.MaxValue, ulong.MinValue, 0x1234 }));
            ds.Add(new DicomPersonName(ds.GetPrivateTag(new DicomTag(3, 0x0014, privateCreator)), "Morrison-Jones^Susan^^^Ph.D."));
            ds.Add(new DicomShortString(ds.GetPrivateTag(new DicomTag(3, 0x0015, privateCreator)), "顔文字"));
            ds.Add(new DicomSignedLong(ds.GetPrivateTag(new DicomTag(3, 0x0016, privateCreator)), -65538));
            ds.Add(new DicomSequence(ds.GetPrivateTag(new DicomTag(3, 0x0017, privateCreator)), new[] { new DicomDataset { new DicomShortText(new DicomTag(3, 0x0018, privateCreator), "ಠ_ಠ") } }));
            ds.Add(new DicomSignedShort(ds.GetPrivateTag(new DicomTag(3, 0x0019, privateCreator)), -32768));
            ds.Add(new DicomShortText(ds.GetPrivateTag(new DicomTag(3, 0x001a, privateCreator)), "ಠ_ಠ"));
            ds.Add(new DicomSignedVeryLong(ds.GetPrivateTag(new DicomTag(3, 0x001b, privateCreator)), -12345678));
            ds.Add(new DicomTime(ds.GetPrivateTag(new DicomTag(3, 0x001c, privateCreator)), "123456"));
            ds.Add(new DicomUnlimitedCharacters(ds.GetPrivateTag(new DicomTag(3, 0x001d, privateCreator)), "Hmph."));
            ds.Add(new DicomUniqueIdentifier(ds.GetPrivateTag(new DicomTag(3, 0x001e, privateCreator)), DicomUID.CTImageStorage));
            ds.Add(new DicomUnsignedLong(ds.GetPrivateTag(new DicomTag(3, 0x001f, privateCreator)), 0xffffffff));
            ds.Add(new DicomUnknown(ds.GetPrivateTag(new DicomTag(3, 0x0020, privateCreator)), new byte[] { 1, 2, 3, 0, 255 }));
            ds.Add(new DicomUniversalResource(ds.GetPrivateTag(new DicomTag(3, 0x0021, privateCreator)), "http://example.com?q=1"));
            ds.Add(new DicomUnsignedShort(ds.GetPrivateTag(new DicomTag(3, 0x0022, privateCreator)), 0xffff));
            ds.Add(new DicomUnlimitedText(ds.GetPrivateTag(new DicomTag(3, 0x0023, privateCreator)), "unlimited!"));
            ds.Add(new DicomUnsignedVeryLong(ds.GetPrivateTag(new DicomTag(3, 0x0024, privateCreator)), 0xffffffffffffffff));

            return ds;
        }

        private static DicomDataset BuildAllTypesNullDataset_()
        {
            var privateCreator = DicomDictionary.Default.GetPrivateCreator("TEST");
            return new DicomDataset {
                           new DicomApplicationEntity(new DicomTag(3, 0x1002, privateCreator)),
                           new DicomAgeString(new DicomTag(3, 0x1003, privateCreator)),
                           new DicomAttributeTag(new DicomTag(3, 0x1004, privateCreator)),
                           new DicomCodeString(new DicomTag(3, 0x1005, privateCreator)),
                           new DicomDate(new DicomTag(3, 0x1006, privateCreator), Array.Empty<string>()),
                           new DicomDecimalString(new DicomTag(3, 0x1007, privateCreator), Array.Empty<string>()),
                           new DicomDateTime(new DicomTag(3, 0x1008, privateCreator), Array.Empty<string>()),
                           new DicomFloatingPointSingle(new DicomTag(3, 0x1009, privateCreator)),
                           new DicomFloatingPointDouble(new DicomTag(3, 0x100a, privateCreator)),
                           new DicomIntegerString(new DicomTag(3, 0x100b, privateCreator), Array.Empty<string>()),
                           new DicomLongString(new DicomTag(3, 0x100c, privateCreator)),
                           new DicomLongText(new DicomTag(3, 0x100d, privateCreator), null),
                           new DicomOtherByte(new DicomTag(3, 0x100e, privateCreator), Array.Empty<byte>()),
                           new DicomOtherDouble(new DicomTag(3, 0x100f, privateCreator), Array.Empty<double>()),
                           new DicomOtherFloat(new DicomTag(3, 0x1010, privateCreator), Array.Empty<float>()),
                           new DicomOtherLong(new DicomTag(3, 0x1014, privateCreator), Array.Empty<uint>()),
                           new DicomOtherWord(new DicomTag(3, 0x1011, privateCreator), Array.Empty<ushort>()),
                           new DicomPersonName(new DicomTag(3, 0x1012, privateCreator)),
                           new DicomShortString(new DicomTag(3, 0x1013, privateCreator)),
                           new DicomSignedLong(new DicomTag(3, 0x1001, privateCreator)),
                           new DicomSequence(new DicomTag(3, 0x1015, privateCreator)),
                           new DicomSignedShort(new DicomTag(3, 0x1017, privateCreator)),
                           new DicomShortText(new DicomTag(3, 0x1018, privateCreator), null),
                           new DicomTime(new DicomTag(3, 0x1019, privateCreator), Array.Empty<string>()),
                           new DicomUnlimitedCharacters(new DicomTag(3, 0x101a, privateCreator), (string)null),
                           new DicomUniqueIdentifier(new DicomTag(3, 0x101b, privateCreator), Array.Empty<string>()),
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
                               new DicomPersonName(DicomTag.PatientName, new[] { "Doe^John" }),
                               new DicomPersonName(DicomTag.OtherPatientNames, new[] { "Anna^Pelle", null, "Olle^Jöns^Pyjamas" }),
                             { DicomTag.SOPClassUID, DicomUID.RTPlanStorage },
                             { DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateDerivedFromUUID() },
                             { DicomTag.SeriesInstanceUID, Array.Empty<DicomUID>() },
                             { DicomTag.DoseType, new[] { "HEJ" } },
                             { DicomTag.ControlPointSequence, (DicomSequence[])null }
                           };

            var beams = new[] { 1, 2, 3 }.Select(beamNumber =>
            {
                var beam = new DicomDataset
                {
                    { DicomTag.BeamNumber, beamNumber },
                    { DicomTag.BeamName, $"Beam #{beamNumber}" }
                };
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

            Assert.True(ValueEquals(originalDataset, reconstituatedDataset));
            Assert.Equal(json, json2);
        }

        [Fact]
        public static void TestKeywordSerialization()
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
            Assert.Equal("{\"00030010\":{\"vr\":\"LO\",\"Value\":[\"TEST\"]},\"00031002\":{\"vr\":\"AE\",\"Value\":[\"AETITLE\"]},\"00031003\":{\"vr\":\"UN\",\"InlineBinary\":\"V0hBVElTVEhJUw==\"},\"00031010\":{\"vr\":\"LO\",\"Value\":[\"TEST\"]},\"GantryAngle\":{\"vr\":\"DS\",\"Value\":[36]}}",
                json);
        }

        [Fact]
        public static void TripleTripNulls()
        {
            var x = BuildAllTypesNullDataset_();
            VerifyJsonTripleTrip(x);
        }


        [Fact]
        public static void TestPrivateTagsDeserialization()
        {
            var privateCreator = DicomDictionary.Default.GetPrivateCreator("Testing");
            var privTag1 = new DicomTag(4013, 0x008, privateCreator);
            var privTag2 = new DicomTag(4013, 0x009, privateCreator);

            var ds = new DicomDataset
            {
                { DicomTag.Modality, "CT" },
                new DicomCodeString(privTag1, "TESTA"),
                { DicomVR.LO, privTag2, "TESTB" },
            };

            var json = JsonConvert.SerializeObject(ds, new JsonDicomConverter());
            var ds2 = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());

            Assert.Equal(ds.GetString(privTag1), ds2.GetString(privTag1));
            Assert.Equal(ds.GetString(privTag2), ds2.GetString(privTag2));
        }

        [Fact]
        public static void GivenDicomDatasetWithInvalidPaddedCharacterForDecimalStringVRType_WhenSerialized_IsDeserializedCorrectly()
        {
            string invalidAccerationValue = "0\0";

            //Disabling the validation to add the invalid VR datatype to a dicom dataset.
            var dicomDataset = new DicomDataset().NotValidated();
            dicomDataset.Add(DicomTag.Acceleration, invalidAccerationValue);

            var json = JsonConvert.SerializeObject(dicomDataset, new JsonDicomConverter());
            JObject.Parse(json);
            DicomDataset deserializedDataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var recoveredString = deserializedDataset.GetValue<string>(DicomTag.Acceleration, 0);
            Assert.Equal("0", recoveredString);
        }

        [Fact]
        public static void GivenDicomDatasetWithValidDecimalStringVRType_WhenSerialized_IsDeserializedCorrectly()
        {
            string validAccelarationValue = "97";

            var dicomDataset = new DicomDataset
            {
                { DicomTag.Acceleration, validAccelarationValue },
            };

            var json = JsonConvert.SerializeObject(dicomDataset, new JsonDicomConverter());
            JObject.Parse(json);
            DicomDataset deserializedDataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var recoveredString = deserializedDataset.GetValue<string>(DicomTag.Acceleration, 0);
            Assert.Equal(validAccelarationValue, recoveredString);
        }


        [Fact]
        public static void GivenJsonIsInvalid_WhenDeserialization_ThenThrowsDicomValidationException()
        {
            string invalidDatasetJson = @"
{
    ""00101010"": {
        ""vr"": ""AS"",
        ""Value"": [
            ""34""
        ]
    }
}";
            Assert.Throws<DicomValidationException>(() => JsonConvert.DeserializeObject<DicomDataset>(invalidDatasetJson, new JsonDicomConverter()));
        }


        [Fact]
        public static void GivenJsonIsInvalid_WhenDeserializationWithAutoValidationIsFalse_ThenShouldSucceed()
        {
            string invalidDatasetJson = @"
{
    ""00101010"": {
        ""vr"": ""AS"",
        ""Value"": [
            ""34""
        ]
    }
}";
            var ds = JsonConvert.DeserializeObject<DicomDataset>(invalidDatasetJson, new JsonDicomConverter(autoValidate: false));
            Assert.NotNull(ds);
            Assert.True(ds.Contains(DicomTag.PatientAge));
        }

        [Fact]
        public static void GivenInvalidValue_WhenNumberSerializationModeAsString_ThenDeserializationShouldSucceed()
        {
            var dataset = new DicomDataset().NotValidated();
            const string invalidDS = "InvalidDS";
            const string invalidIS = "InvalidIS";
            dataset.Add(new DicomDecimalString(DicomTag.PatientSize, new MemoryByteBuffer(Encoding.ASCII.GetBytes(invalidDS))));
            dataset.Add(new DicomIntegerString(DicomTag.ReferencedFrameNumber, new MemoryByteBuffer(Encoding.ASCII.GetBytes(invalidIS))));
            var json = JsonConvert.SerializeObject(dataset, new JsonDicomConverter(numberSerializationMode: NumberSerializationMode.AsString));
            Assert.Equal("{\"00081160\":{\"vr\":\"IS\",\"Value\":[\"InvalidIS\"]},\"00101020\":{\"vr\":\"DS\",\"Value\":[\"InvalidDS\"]}}", json);
        }

        [Fact]
        public static void GivenInvalidValue_WhenNumberSerializationModePreferablyAsNumber_ThenDeserializationShouldSucceed()
        {
            var dataset = new DicomDataset().NotValidated();
            const string invalidDS = "InvalidDS";
            const string invalidIS = "InvalidIS";
            const string invalidISThatThrowsOverflowException = "73.8";
            dataset.Add(new DicomDecimalString(DicomTag.PatientSize, new MemoryByteBuffer(Encoding.ASCII.GetBytes(invalidDS))));
            dataset.Add(new DicomIntegerString(DicomTag.ReferencedFrameNumber, new MemoryByteBuffer(Encoding.ASCII.GetBytes(invalidIS))));
            dataset.Add(new DicomIntegerString(DicomTag.Exposure, new MemoryByteBuffer(Encoding.ASCII.GetBytes(invalidISThatThrowsOverflowException))));
            var json = JsonConvert.SerializeObject(dataset, new JsonDicomConverter(numberSerializationMode: NumberSerializationMode.PreferablyAsNumber));
            Assert.Equal("{\"00081160\":{\"vr\":\"IS\",\"Value\":[\"InvalidIS\"]},\"00101020\":{\"vr\":\"DS\",\"Value\":[\"InvalidDS\"]},\"00181152\":{\"vr\":\"IS\",\"Value\":[\"73.8\"]}}", json);
        }

        [Fact]
        public static void GivenInvalidValueForDS_WhenNumberSerializationModeAsNumber_ThenDeserializationShouldThrowFormatException()
        {
            var dataset = new DicomDataset().NotValidated();
            const string invalidNumber = "InvalidNumber";
            dataset.Add(new DicomDecimalString(DicomTag.PatientSize, new MemoryByteBuffer(Encoding.ASCII.GetBytes(invalidNumber))));
            Assert.Throws<FormatException>(() => JsonConvert.SerializeObject(dataset, new JsonDicomConverter(numberSerializationMode: NumberSerializationMode.AsNumber)));
        }

        [Fact]
        public static void GivenInvalidValueForIS_WhenNumberSerializationModeAsNumber_ThenDeserializationShouldThrowFormatException()
        {
            var dataset = new DicomDataset().NotValidated();
            const string invalidNumber = "InvalidNumber";
            dataset.Add(new DicomIntegerString(DicomTag.ReferencedFrameNumber, new MemoryByteBuffer(Encoding.ASCII.GetBytes(invalidNumber))));
            Assert.Throws<FormatException>(() => JsonConvert.SerializeObject(dataset, new JsonDicomConverter(numberSerializationMode: NumberSerializationMode.AsNumber)));
        }

        [Fact]
        public static void GivenInvalidValueForIS_WhenNumberSerializationModeAsNumber_ThenDeserializationShouldThrowOverflowException()
        {
            var dataset = new DicomDataset().NotValidated();
            const string invalidNumber = "2147483647500";
            dataset.Add(new DicomIntegerString(DicomTag.Exposure, new MemoryByteBuffer(Encoding.ASCII.GetBytes(invalidNumber))));
            Assert.Throws<OverflowException>(() => JsonConvert.SerializeObject(dataset, new JsonDicomConverter(numberSerializationMode: NumberSerializationMode.AsNumber)));
        }

        [Fact]
        public static void GivenValidValue_WhenNumberSerializationModeAsNumber_ThenDeserializationShouldSucceed()
        {
            var dataset = new DicomDataset();
            const decimal validDS = 3.1415926535m;
            const int validIS = 299792458;
            const long validSV = 9223372036854775800;
            const ulong validUV = 18446744073709551600;
            dataset.Add(new DicomDecimalString(DicomTag.PatientSize, validDS));
            dataset.Add(new DicomIntegerString(DicomTag.ReferencedFrameNumber, validIS));
            dataset.Add(new DicomSignedVeryLong(DicomTag.SelectorSVValue, validSV));
            dataset.Add(new DicomUnsignedVeryLong(DicomTag.SelectorUVValue, validUV));
            var json = JsonConvert.SerializeObject(dataset, new JsonDicomConverter(numberSerializationMode: NumberSerializationMode.AsNumber));
            Assert.Equal("{\"00081160\":{\"vr\":\"IS\",\"Value\":[299792458]},\"00101020\":{\"vr\":\"DS\",\"Value\":[3.1415926535]},\"00720082\":{\"vr\":\"SV\",\"Value\":[9223372036854775800]},\"00720083\":{\"vr\":\"UV\",\"Value\":[18446744073709551600]}}", json);
        }

        [Fact]
        public static void GivenValidValue_WhenNumberSerializationModePreferablyAsNumber_ThenDeserializationShouldSucceed()
        {
            var dataset = new DicomDataset();
            const decimal validDS = 3.1415926535m;
            const int validIS = 299792458;
            const long validSV = 9223372036854775800;
            const ulong validUV = 18446744073709551600;
            dataset.Add(new DicomDecimalString(DicomTag.PatientSize, validDS));
            dataset.Add(new DicomIntegerString(DicomTag.ReferencedFrameNumber, validIS));
            dataset.Add(new DicomSignedVeryLong(DicomTag.SelectorSVValue, validSV));
            dataset.Add(new DicomUnsignedVeryLong(DicomTag.SelectorUVValue, validUV));
            var json = JsonConvert.SerializeObject(dataset, new JsonDicomConverter(numberSerializationMode: NumberSerializationMode.PreferablyAsNumber));
            Assert.Equal("{\"00081160\":{\"vr\":\"IS\",\"Value\":[299792458]},\"00101020\":{\"vr\":\"DS\",\"Value\":[3.1415926535]},\"00720082\":{\"vr\":\"SV\",\"Value\":[9223372036854775800]},\"00720083\":{\"vr\":\"UV\",\"Value\":[18446744073709551600]}}", json);
        }

        [Fact]
        public static void GivenValidValue_WhenNumberSerializationModeAsString_ThenDeserializationShouldSucceed()
        {
            var dataset = new DicomDataset();
            const decimal validDS = 3.1415926535m;
            const int validIS = 299792458;
            const long validSV = 9223372036854775800;
            const ulong validUV = 18446744073709551600;
            dataset.Add(new DicomDecimalString(DicomTag.PatientSize, validDS));
            dataset.Add(new DicomIntegerString(DicomTag.ReferencedFrameNumber, validIS));
            dataset.Add(new DicomSignedVeryLong(DicomTag.SelectorSVValue, validSV));
            dataset.Add(new DicomUnsignedVeryLong(DicomTag.SelectorUVValue, validUV));
            var json = JsonConvert.SerializeObject(dataset, new JsonDicomConverter(numberSerializationMode: NumberSerializationMode.AsString));
            Assert.Equal("{\"00081160\":{\"vr\":\"IS\",\"Value\":[\"299792458\"]},\"00101020\":{\"vr\":\"DS\",\"Value\":[\"3.1415926535\"]},\"00720082\":{\"vr\":\"SV\",\"Value\":[\"9223372036854775800\"]},\"00720083\":{\"vr\":\"UV\",\"Value\":[\"18446744073709551600\"]}}", json);
        }

        [Fact]
        public static void GivenArrayWithValidAndInvalidValuesForIS_WhenNumberSerializationModeAsNumber_ThenDeserializationShouldThrowError()
        {
            var dataset = new DicomDataset().NotValidated();
            const string validNumber = "299792458";
            const string invalidNumber = "InvalidNumber";
            dataset.Add(new DicomIntegerString(DicomTag.ReferencedFrameNumber, validNumber, invalidNumber));
            Assert.Throws<FormatException>(() => JsonConvert.SerializeObject(dataset, new JsonDicomConverter(numberSerializationMode: NumberSerializationMode.AsNumber)));
        }

        [Fact]
        public static void GivenArrayWithValidAndInvalidValuesForIS_WhenNumberSerializationModePreferablyAsNumber_ThenDeserializationShouldSucceedAndAllValuesShouldBeStrings()
        {
            var dataset = new DicomDataset().NotValidated();
            const string validNumber = "299792458";
            const string invalidNumber = "InvalidNumber";
            dataset.Add(new DicomIntegerString(DicomTag.ReferencedFrameNumber, validNumber, invalidNumber));
            var json = JsonConvert.SerializeObject(dataset, new JsonDicomConverter(numberSerializationMode: NumberSerializationMode.PreferablyAsNumber));
            Assert.Equal("{\"00081160\":{\"vr\":\"IS\",\"Value\":[\"299792458\",\"InvalidNumber\"]}}", json);
        }

        [Fact]
        public static void GivenDicomJsonDatasetWithInvalidPrivateCreatorDataElement_WhenDeserialized_IsSuccessful()
        {
            // allowing deserializer to handle bad data private creator data more gracefully
            const string json = @"
            {
                ""00090010"": {
                    ""vr"": ""US"",
                     ""Value"": [
                        1234,
                        3333
                    ]
                 },
                ""00091001"": {
                    ""vr"": ""CS"",
                    ""Value"": [
                        ""00""
                    ]
                }
            } ";

            // make sure below serialization does not throw
            var ds = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter(autoValidate: false));
            Assert.NotNull(ds);
        }

        #region Sample Data

        private string _jsonExampleFromDicomNemaOrg = @"
// The following example is a QIDO-RS SearchForStudies response consisting
// of two matching studies, corresponding to the example QIDO-RS request:
// GET http://qido.nema.org/studies?PatientID=12345&includefield=all&limit=2
[
    {   // Result 1
        ""00080005"": {
            ""vr"": ""CS"",
            ""Value"": [ ""ISO_IR 192"" ]
    },
        ""00080020"": {
            ""vr"": ""DT"",
            ""Value"": [ ""20130409"" ]
},
        ""00080030"": {
            ""vr"": ""TM"",
            ""Value"": [ ""131600.0000"" ]
        },
        ""00080050"": {
            ""vr"": ""SH"",
            ""Value"": [ ""11235813"" ]
        },
        ""00080056"": {
            ""vr"": ""CS"",
            ""Value"": [ ""ONLINE"" ]
        },
        ""00080061"": {
            ""vr"": ""CS"",
            ""Value"": [
                ""CT"",
                ""PET""
            ]
        },
        ""00080090"": {
            ""vr"": ""PN"",
            ""Value"": [
              {
                ""Alphabetic"": ""^Bob^^Dr.""
              }
            ]
        },
        ""00081190"": {
            ""vr"": ""UR"",
            ""Value"": [ ""http://wado.nema.org/studies/1.2.392.200036.9116.2.2.2.1762893313.1029997326.945873"" ]
        },
        ""00090010"": {
            ""vr"": ""LO"",
            ""Value"": [ ""Vendor A"" ]
        },
        ""00091002"": {
            ""vr"": ""UN"",
            ""InlineBinary"": [ ""z0x9c8v7"" ]
        },
        ""00100010"": {
            ""vr"": ""PN"",
            ""Value"": [
              {
                ""Alphabetic"": ""Wang^XiaoDong"",
                ""Ideographic"": ""王^小東""
              }
            ]
        },
        ""00100020"": {
            ""vr"": ""LO"",
            ""Value"": [ ""12345"" ]
        },
        ""00100021"": {
            ""vr"": ""LO"",
            ""Value"": [ ""Hospital A"" ]
        },
        ""00100030"": {
            ""vr"": ""DT"",
            ""Value"": [ ""19670701"" ]
        },
        ""00100040"": {
            ""vr"": ""CS"",
            ""Value"": [ ""M"" ]
        },
        ""00101002"": {
            ""vr"": ""SQ"",
            ""Value"": [
                {
                    ""00100020"": {
                        ""vr"": ""LO"",
                        ""Value"": [ ""54321"" ]
                    },
                    ""00100021"": {
                        ""vr"": ""LO"",
                        ""Value"": [ ""Hospital B"" ]
                    }
                },
                {
                    ""00100020"": {
                        ""vr"": ""LO"",
                        ""Value"": [ ""24680"" ]
                    },
                    ""00100021"": {
                        ""vr"": ""LO"",
                        ""Value"": [ ""Hospital C"" ]
                    }
                }
            ]
        },
        ""0020000D"": {
            ""vr"": ""UI"",
            ""Value"": [ ""1.2.392.200036.9116.2.2.2.1762893313.1029997326.945873"" ]
        },
        ""00200010"": {
            ""vr"": ""SH"",
            ""Value"": [ ""11235813"" ]
        },
        ""00201206"": {
            ""vr"": ""IS"",
            ""Value"": [ 4 ]
        },
        ""00201208"": {
            ""vr"": ""IS"",
            ""Value"": [ 942 ]
        }
    },
    {   // Result 2
        ""00080005"": {
            ""vr"": ""CS"",
            ""Value"": [ ""ISO_IR 192"" ]
        },
        ""00080020"": {
            ""vr"": ""DT"",
            ""Value"": [ ""20130309"" ]
        },
        ""00080030"": {
            ""vr"": ""TM"",
            ""Value"": [ ""111900.0000"" ]
        },
        ""00080050"": {
            ""vr"": ""SH"",
            ""Value"": [ ""11235821"" ]
        },
        ""00080056"": {
            ""vr"": ""CS"",
            ""Value"": [ ""ONLINE"" ]
        },
        ""00080061"": {
            ""vr"": ""CS"",
            ""Value"": [
                ""CT"",
                ""PET""
            ]
        },
        ""00080090"": {
            ""vr"": ""PN"",
            ""Value"": [
              {
                ""Alphabetic"": ""^Bob^^Dr.""
              }
            ]
        },
        ""00081190"": {
            ""vr"": ""UR"",
            ""Value"": [ ""http://wado.nema.org/studies/1.2.392.200036.9116.2.2.2.2162893313.1029997326.945876"" ]
        },
        ""00090010"": {
            ""vr"": ""LO"",
            ""Value"": [ ""Vendor A"" ]
        },
        ""00091002"": {
            ""vr"": ""UN"",
            ""InlineBinary"": [ ""z0x9c8v7"" ]
        },
        ""00100010"": {
            ""vr"": ""PN"",
            ""Value"": [
              {
                ""Alphabetic"": ""Wang^XiaoDong"",
                ""Ideographic"": ""王^小東""
              }
            ]
        },
        ""00100020"": {
            ""vr"": ""LO"",
            ""Value"": [ ""12345"" ]
        },
        ""00100021"": {
            ""vr"": ""LO"",
            ""Value"": [ ""Hospital A"" ]
        },
        ""00100030"": {
            ""vr"": ""DT"",
            ""Value"": [ ""19670701"" ]
        },
        ""00100040"": {
            ""vr"": ""CS"",
            ""Value"": [ ""M"" ]
        },
        ""00101002"": {
            ""vr"": ""SQ"",
            ""Value"": [
                {
                    ""00100020"": {
                        ""vr"": ""LO"",
                        ""Value"": [ ""54321"" ]
                    },
                    ""00100021"": {
                        ""vr"": ""LO"",
                        ""Value"": [ ""Hospital B"" ]
                    }
                },
                {
                    ""00100020"": {
                        ""vr"": ""LO"",
                        ""Value"": [ ""24680"" ]
                    },
                    ""00100021"": {
                        ""vr"": ""LO"",
                        ""Value"": [ ""Hospital C"" ]
                    }
                }
            ]
        },
        ""0020000D"": {
            ""vr"": ""UI"",
            ""Value"": [ ""1.2.392.200036.9116.2.2.2.2162893313.1029997326.945876"" ]
        },
        ""00200010"": {
            ""vr"": ""SH"",
            ""Value"": [ ""11235821"" ]
        },
        ""00201206"": {
            ""vr"": ""IS"",
            ""Value"": [ 5 ]
        },
        ""00201208"": {
            ""vr"": ""IS"",
            ""Value"": [ 1123 ]
        }
    }
]";

        #endregion
    }
}
