// Copyright (c) 2012 Anders Gustafsson, Cureos AB.
// Licensed under the Microsoft Public License (MS-PL).

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dicom.Helpers;

namespace Dicom
{
    [TestClass]
    public class DicomTagTests
    {
        [TestMethod]
        public void Serialization_RegularTag_CanBeDeserialized()
        {
            var expected = DicomTag.BeamDose;
            var actual = expected.GetDataContractSerializerDeserializedObject();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialization_PrivateCreatorTag_CanBeDeserialized()
        {
            var expected = new DicomTag(0x3005, 0x1013, "CUREOS");
            var actual = expected.GetDataContractSerializerDeserializedObject();
            Assert.AreEqual(expected, actual);
        }
    }
}
