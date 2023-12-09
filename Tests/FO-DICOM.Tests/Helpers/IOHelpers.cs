// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;

namespace FellowOakDicom.Tests.Helpers
{
    public static class IOHelper
    {

        public static void DeleteIfExists(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

    }
}
