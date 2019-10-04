// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using FellowOakDicom.Network;
using System.IO;
using Xunit;

namespace FellowOakDicom.Tests.Log
{

    public class TextWriterLoggerTest
    {
        [Fact]
        public void Log_ToTextWriterLogger_SufficientlyLogged()
        {
            var writer = new StringWriter();
            LogManager.SetImplementation(new TextWriterLogManager(writer));

            const string expected = "[INFO] A: 1 B: Permanent";

            LogManager.GetLogger(null).Info("A: {0} B: {err}", 1, DicomRejectResult.Permanent);
            var actual = writer.ToString().Replace("\r", "").Replace("\n", "");

            LogManager.SetImplementation(null);

            Assert.Equal(expected, actual);
        }
    }
}

