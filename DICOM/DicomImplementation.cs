// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System.Reflection;

    public static class DicomImplementation
    {
        public static DicomUID ClassUID = new DicomUID(
            "1.3.6.1.4.1.30071.8",
            "Implementation Class UID",
            DicomUidType.Unknown);

        public static string Version = GetImplementationVersion();

        private static string GetImplementationVersion()
        {
            var version = typeof(DicomImplementation).GetTypeInfo().Assembly.GetName().Version;
            return string.Format("fo-dicom {0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }
    }
}
