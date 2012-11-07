// Copyright (c) 2012 Anders Gustafsson, Cureos AB.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Helpers;
using NUnit.Framework;

namespace Dicom.Imaging.Mathematics
{
    [TestFixture]
    public class Point2Tests
    {
        [Test]
        public void Serialization_RegularPoint_CanBeDeserialized()
        {
            var expected = new Point2(3, -5);
            var actual = expected.GetSerializedDeserializedObject();
            Assert.AreEqual(expected, actual);
        }
    }
}