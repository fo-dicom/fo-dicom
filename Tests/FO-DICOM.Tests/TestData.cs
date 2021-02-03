// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;

namespace FellowOakDicom.Tests
{
    internal static class TestData
    {
        private static DirectoryInfo TestDataDirectory => new DirectoryInfo(Path.Combine(".", "Test Data"));

        public static string Resolve(string path) => Path.Combine(TestDataDirectory.FullName, path);
    }
}
