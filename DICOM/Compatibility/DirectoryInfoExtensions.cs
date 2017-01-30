// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace System.IO
{
    using System.Collections.Generic;

    /// <summary>
    /// .NET 3.5 compatibility extension methods for <see cref="DirectoryInfo"/>
    /// </summary>
    internal static class DirectoryInfoExtensions
    {
        internal static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo directoryInfo)
        {
            return directoryInfo.GetDirectories();
        } 
    }
}