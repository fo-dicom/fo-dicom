// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace FellowOakDicom.Tests
{
    public class TheoryForNetCoreAttribute : TheoryAttribute
    {

        public TheoryForNetCoreAttribute()
        {
#if NET462
            Skip = "Do not run in net462";
#endif
        }

    }


    public class FactForNetCoreAttribute : FactAttribute
    {

        public FactForNetCoreAttribute()
        {
#if NET462
            Skip = "Do not run in net462";
#endif
        }

    }
}
