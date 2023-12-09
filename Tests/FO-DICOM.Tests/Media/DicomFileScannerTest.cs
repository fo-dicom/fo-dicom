// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Media;
using System.Threading;
using Xunit;

namespace FellowOakDicom.Tests.Media
{

    [Collection(TestCollections.General)]
    public class DicomFileScannerTest
    {
        [Fact]
        public void Start_ScanTestData_DicomDirFileFound()
        {
            var dicomdirFileFound = false;
            var complete = false;

            var scanner = new DicomFileScanner();
            scanner.FileFound += (fileScanner, file, name) =>
                {
                    if (name.EndsWith("DICOMDIR"))
                    {
                        dicomdirFileFound = true;
                        complete = true;
                    }
                };
            scanner.Complete += fileScanner => complete = true;

            scanner.Start(@".");
            while (!complete) Thread.Sleep(100);
            scanner.Stop();

            Assert.True(dicomdirFileFound);
        }
    }
}
