﻿// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Reflection;

namespace FellowOakDicom
{

    public static class DicomImplementation
    {
        public static DicomUID ClassUID { get; } = new DicomUID(
            "1.3.6.1.4.1.30071.8",
            "Implementation Class UID",
            DicomUidType.Unknown);

        public static string Version { get; } = GetImplementationVersion();

        private static string GetImplementationVersion()
        {
            var version = typeof(DicomImplementation).GetTypeInfo().Assembly.GetName().Version;
            return $"fo-dicom {version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
