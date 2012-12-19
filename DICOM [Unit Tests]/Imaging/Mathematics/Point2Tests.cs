// Copyright (c) 2012 Anders Gustafsson, Cureos AB.
// Licensed under the Microsoft Public License (MS-PL).

using System.Runtime.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dicom.Helpers;

namespace Dicom.Imaging.Mathematics
{
    [TestClass]
    public class Point2Tests
    {
		[TestMethod]
        public void Serialization_RegularPoint_CanBeDeserialized()
        {
            var expected = new Point2(3, -5);
            var actual = expected.GetDataContractSerializerDeserializedObject();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod, ExpectedException(typeof(SerializationException))]
        public void Serialization_BinaryFormatter_Throws()
        {
            var expected = new Point2(-2, 12);
            var actual = expected.GetBinaryFormatterDeserializedObject();
            Assert.AreEqual(expected, actual);
        }
    }
}