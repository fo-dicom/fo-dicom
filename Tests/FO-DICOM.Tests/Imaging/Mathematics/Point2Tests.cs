// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;
using FellowOakDicom.Tests.Helpers;
using System.Runtime.Serialization;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Mathematics
{
    [Collection(TestCollections.General)]
    public class Point2Tests
    {
        [Fact]
        public void Serialization_RegularPoint_CanBeDeserialized()
        {
            var expected = new Point2(3, -5);
            var actual = expected.GetDataContractSerializerDeserializedObject();
            Assert.Equal(expected, actual);
        }
    }
}
