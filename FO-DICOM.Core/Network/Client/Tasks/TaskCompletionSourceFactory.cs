// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client
{

    // Remove this class when Fellow Oak DICOM is completely upgraded to .NET Standard 2.0 or higher
    internal static class TaskCompletionSourceFactory
    {
        public static TaskCompletionSource<T> Create<T>()
        {
            return new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
        }
    }
}
