// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.AspNetCore.Builder;

namespace FellowOakDicom.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseFellowOakDicom(this IApplicationBuilder app)
        {
            DicomSetupBuilder.UseServiceProvider(app.ApplicationServices);
            return app;
        }

    }
}
