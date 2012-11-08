// Copyright (c) 2012 Anders Gustafsson, Cureos AB.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Helpers;
using NUnit.Framework;

namespace Dicom
{
    [TestFixture]
    public class DicomTagTests
    {
        [Test]
        public void Serialization_RegularTag_CanBeDeserialized()
        {
            var expected = DicomTag.BeamDose;
            var actual = expected.GetDataContractSerializerDeserializedObject();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Serialization_PrivateCreatorTag_CanBeDeserialized()
        {
            var expected = new DicomTag(0x3005, 0x1013, "CUREOS");
            var actual = expected.GetDataContractSerializerDeserializedObject();
            Assert.AreEqual(expected, actual);
        }
    }
}
