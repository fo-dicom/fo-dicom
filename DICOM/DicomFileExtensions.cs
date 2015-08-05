// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    public static class DicomFileExtensions
    {
        public static DicomFile Clone(this DicomFile original)
        {
            var df = new DicomFile();
            df.FileMetaInfo.Add(original.FileMetaInfo);
            df.Dataset.Add(original.Dataset);
            df.Dataset.InternalTransferSyntax = original.Dataset.InternalTransferSyntax;
            return df;
        }
    }
}
