// Copyright (c) 2012-2022 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH1308
    {
        [Fact]
        public void AnonymizeNullValues()
        {
            // This dataset contains some empty values
            var dataset = DicomFile.Open(TestData.Resolve("GH1308.dcm")).Dataset;
            var securityProfile = DicomAnonymizer.SecurityProfile.LoadProfile(null, (DicomAnonymizer.SecurityProfileOptions)15);
            var _anonymizer = new DicomAnonymizer(securityProfile);
            var ex = Record.Exception(() => _anonymizer.AnonymizeInPlace(dataset));
            Assert.Null(ex);
        }
    }
}
