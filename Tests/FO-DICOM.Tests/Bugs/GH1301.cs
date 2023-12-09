// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Log;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH1301
    {
        [Fact]
        public void BadValueInCodeStringIsNotChangedAfterWriteToString()
        {
            // dataset has GB18030 encoding, PatientSex has content "男" in that encoding
            var dataset = DicomFile.Open(TestData.Resolve("GH1301.dcm")).Dataset;
            var buffer = dataset.GetValues<byte>(DicomTag.PatientSex);
            Assert.Equal(196, buffer[0]);
            Assert.Equal(208, buffer[1]);
            dataset.WriteToString();
            buffer = dataset.GetValues<byte>(DicomTag.PatientSex);
            Assert.Equal(196, buffer[0]);
            Assert.Equal(208, buffer[1]);
        }
    }
}
