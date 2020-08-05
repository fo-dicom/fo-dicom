// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace Dicom.Network.Client.Tasks
{
    // Remove this class when Fellow Oak DICOM is completely upgraded to .NET Standard 2.0 or higher
    internal static class CompletedTaskProvider
    {
#if NETSTANDARD
        public static readonly Task CompletedTask = Task.CompletedTask;
#else
        public static readonly Task CompletedTask = Task.FromResult(false);
#endif
    }
}
