// Copyright (c) 2012 Anders Gustafsson, Cureos AB.
// Licensed under the Microsoft Public License (MS-PL).

using System.Runtime.Serialization;

using Xunit;

using Dicom.Helpers;

namespace Dicom.Imaging.Mathematics
{
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
	        Assert.Throws<SerializationException>(
		        () =>
			        {
				        var expected = new Point2(-2, 12);
				        var actual = expected.GetBinaryFormatterDeserializedObject();
				        Assert.Equal(expected, actual);
			        });
        }
    }
}
