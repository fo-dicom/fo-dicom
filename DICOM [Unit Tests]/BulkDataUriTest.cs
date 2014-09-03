using System;
using Dicom;
using Xunit;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Bson;
using System.Linq;
using System.Collections.Generic;
using Dicom.IO.Buffer;
using Dicom.IO;
using System.Text;

namespace DICOM__Unit_Tests_
{
    public class BulkDataUriTest
    {
        [Fact]
        public void TestReadBulkData()
        {
            var path = System.IO.Path.GetFullPath("test.txt");
            var bulkData = new BulkUriByteBuffer("file:" + path);

            byte[] expected = File.ReadAllBytes("test.txt");

            Assert.True(bulkData.Data.SequenceEqual(expected));
            Assert.Equal(bulkData.Size, (uint)expected.Length);
        }
    }
}