using System;
using Dicom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Bson;
using System.Linq;
using System.Collections.Generic;
using Dicom.IO.Buffer;
using Dicom.IO;

namespace DICOM__Unit_Tests_
{
    [TestClass]
    public class JsonDicomConverterTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void SerializeAndDeserializeZoo()
        {
            var target = BuildZooDataset();
            VerifyJsonTripleTrip(target);
        }

        /// <summary>
        ///     A test for ToString
        /// </summary>
        [TestMethod]
        public void TestKeywordDeserialization()
        {
            string json = "{\"PatientName\": { \"vr\": \"PN\", \"Value\": [{ \"Alphabetic\": \"Kalle\" }] } }";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            Assert.AreEqual("Kalle", reconstituated.Get<string>(DicomTag.PatientName));
        }

        [TestMethod]
        public void TestBulkDataUri()
        {
            string json = @"
{
  ""7FE00010"": {
    ""vr"": ""OW"",
    ""BulkDataURI"": ""http://www.example.com/testdicom.dcm""
  }
}
";
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            var buffer = reconstituated.Get<IByteBuffer>(DicomTag.PixelData);
            Assert.IsInstanceOfType(buffer, typeof(BulkUriByteBuffer));
            Assert.AreEqual("http://www.example.com/testdicom.dcm", ((BulkUriByteBuffer)buffer).BulkDataUri);
        }

        [TestMethod]
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
            Assert.AreEqual("1.2.392.200036.9116.2.2.2.1762893313.1029997326.945873", reconstituated[0].Get<DicomUID>(0x0020000d).UID);
            Assert.AreEqual("1.2.392.200036.9116.2.2.2.2162893313.1029997326.945876", reconstituated[1].Get<DicomUID>(0x0020000d).UID);
        }

        [TestMethod]
        public void BulkDataRoundTrip()
        {
            var target = new DicomDataset();
            target.Add(new DicomOtherWord(DicomTag.PixelData, new BulkUriByteBuffer("http://www.example.com/bulk.dcm")));
            VerifyJsonTripleTrip(target);
        }

        [TestMethod]
        public void BulkDataRead()
        {
            var path = System.IO.Path.GetFullPath("test.txt");
            var bulkData = new BulkUriByteBuffer("file:" + path);

            var target = new DicomDataset();
            target.Add(new DicomOtherWord(DicomTag.PixelData, bulkData));
            var json = JsonConvert.SerializeObject(target, Formatting.Indented, new JsonDicomConverter());
            var reconstituated = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            Console.WriteLine(json);
            var json2 = JsonConvert.SerializeObject(reconstituated, Formatting.Indented, new JsonDicomConverter());
            Assert.AreEqual(json, json2);
            Assert.AreEqual(target, reconstituated);

            byte[] expectedPixelData = File.ReadAllBytes("test.txt");

            Assert.AreEqual(target.Get<IByteBuffer>(DicomTag.PixelData).Size, (uint)expectedPixelData.Length);
            Assert.IsTrue(target.Get<byte[]>(DicomTag.PixelData).SequenceEqual(expectedPixelData));
            Assert.AreEqual(reconstituated.Get<IByteBuffer>(DicomTag.PixelData).Size, (uint)expectedPixelData.Length);
            Assert.IsTrue(reconstituated.Get<byte[]>(DicomTag.PixelData).SequenceEqual(expectedPixelData));
        }

        private static DicomDataset BuildZooDataset()
        {
            var target = new DicomDataset();
            target.Add(new DicomPersonName(DicomTag.PatientName, new string[] { "Anna^Pelle", null, "Olle^Jöns^Pyjamas" }));
            target.Add(DicomTag.SOPClassUID, DicomUID.RTPlanStorage);
            target.Add(DicomTag.SOPInstanceUID, new DicomUIDGenerator().Generate());
            target.Add(DicomTag.SeriesInstanceUID, new DicomUID[] { });
            target.Add(DicomTag.DoseType, new string[] { "HEJ", null, "BLA" });

            target.Add<DicomSequence>(DicomTag.ControlPointSequence, null);
            var beams = new[] { 1, 2, 3 }.Select(beamNumber =>
            {
                var beam = new DicomDataset();
                beam.Add(DicomTag.BeamNumber, beamNumber);
                beam.Add(DicomTag.BeamName, String.Format("Beam #{0}", beamNumber));
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
            Assert.AreEqual(json, json2);
            Assert.AreEqual(originalDataset, reconstituatedDataset);
        }
    }
}