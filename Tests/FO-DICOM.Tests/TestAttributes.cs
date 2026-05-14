// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Runtime.InteropServices;
using Xunit;

namespace FellowOakDicom.Tests
{

    public class FactWithCodecAttribute : FactAttribute
    {

        public FactWithCodecAttribute()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // TODO: all codec-related tests are failing on macos because the runner cannot load the native dlls.
                // but this is not related to fo-dicom, but to codec package.
                // So until this is fixed, these tests are exclued.
                Skip = "Do not run on MacOS";
            }

        }

    }
}