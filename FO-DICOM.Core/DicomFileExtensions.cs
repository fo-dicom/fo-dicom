// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom
{

    /// <summary>
    /// Extension class providing legacy method support from <see cref="DicomFile"/> class.
    /// </summary>
    public static partial class DicomFileExtensions
    {
        /// <summary>
        /// Deep-copy clone <see cref="DicomFile"/> object.
        /// </summary>
        /// <param name="original"><see cref="DicomFile"/> source.</param>
        /// <returns>Deep-copy clone of <paramref name="original"/>.</returns>
        public static DicomFile Clone(this DicomFile original)
        {
            var df = new DicomFile();
            df.FileMetaInfo.Add(original.FileMetaInfo);
            df.Dataset.ValidateItems = false;
            df.Dataset.Add(original.Dataset);
            df.Dataset.ValidateItems = original.Dataset.ValidateItems;
            df.Dataset.InternalTransferSyntax = original.Dataset.InternalTransferSyntax;
            return df;
        }
    }
}
