// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;

namespace Dicom.Helpers
{
    internal static class ApplicationContent
    {
        internal static async Task<DicomFile> OpenDicomFileAsync(string relativePath)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///{relativePath}"));
            using (var stream = await file.OpenReadAsync())
            using (var classicStream = stream.AsStreamForRead())
            {
                return await DicomFile.OpenAsync(classicStream);
            }
        }
    }
}
