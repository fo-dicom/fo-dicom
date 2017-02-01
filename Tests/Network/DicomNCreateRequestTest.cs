﻿// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom.Network
{
    public class DicomNCreateRequestTest
    {
        [Fact]
        public void SOPInstanceUIDGetter_SOPInstanceUIDNotDefinedInConstruction_IsDefinedAndConstant()
        {
            var command = new DicomDataset();
            command.Add(DicomTag.CommandField, (ushort)DicomCommandField.NCreateRequest);
            command.Add(DicomTag.MessageID, (ushort)1);
            command.Add(DicomTag.RequestedSOPClassUID, "1.2.3");

            var request = new DicomNCreateRequest(command);
            var expected = request.SOPInstanceUID;
            Assert.NotNull(expected);

            var actual = request.SOPInstanceUID;
            Assert.Equal(expected, actual);
        }
    }
}
