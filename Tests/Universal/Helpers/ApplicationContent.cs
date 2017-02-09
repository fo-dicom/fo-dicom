// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;

namespace Dicom.Helpers
{
    internal static class ApplicationContent
    {
        internal static async Task<Stream> GetStreamAsync(string relativePath)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///{relativePath}"));
            return (await file.OpenReadAsync()).AsStreamForRead();
        }
    }
}
