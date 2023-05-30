﻿// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH223
    {
        [Fact]
        public async Task DicomFile_OpenAsync_FirstPrivateSeqItemEmtpySecondContainsData()
        {
            var file = await DicomFile.OpenAsync(TestData.Resolve("GH223.dcm")).ConfigureAwait(false);

            var seq = file.Dataset.GetSequence(new DicomTag(0x01f3, 0x1011, "ELSCINT1"));

            Assert.Equal(2, seq.Items.Count);
            Assert.Empty(seq.Items[0]);
            Assert.True(seq.Items[1].Contains(new DicomTag(0x01f3, 0x1024, "ELSCINT1")));
        }
    }
}
