// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Tests
{
    public sealed class InitializationFixture : IDisposable
    {
        public InitializationFixture()
        {
            new DicomSetupBuilder()
                .RegisterServices(services =>
                    services.AddWinFormsImaging()
                )
                .Build();
        }

        public void Dispose()
        { }
    }


}
