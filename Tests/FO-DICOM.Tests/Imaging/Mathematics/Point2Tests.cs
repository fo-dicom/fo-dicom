// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.Mathematics;
using FellowOakDicom.Tests.Helpers;
using System.Runtime.Serialization;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Mathematics
{

    [Collection("General")]
    public class Point2Tests
    {
        [Fact]
        public void Serialization_RegularPoint_CanBeDeserialized()
        {
            var expected = new Point2(3, -5);
            var actual = expected.GetDataContractSerializerDeserializedObject();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Serialization_BinaryFormatter_Throws()
        {
            var point = new Point2(-2, 12);
            Assert.Throws<SerializationException>(() => point.GetBinaryFormatterDeserializedObject());
        }
    }
}
