// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public static class DicomCodecExtensions
    {
        public static DicomFile ChangeTransferSyntax(
            this DicomFile file,
            DicomTransferSyntax syntax,
            DicomCodecParams parameters = null)
        {
            DicomTranscoder transcoder = new DicomTranscoder(file.FileMetaInfo.TransferSyntax, syntax);
            transcoder.InputCodecParams = parameters;
            transcoder.OutputCodecParams = parameters;
            return transcoder.Transcode(file);
        }

        public static DicomDataset ChangeTransferSyntax(
            this DicomDataset dataset,
            DicomTransferSyntax syntax,
            DicomCodecParams parameters = null)
        {
            DicomTranscoder transcoder = new DicomTranscoder(dataset.InternalTransferSyntax, syntax);
            transcoder.InputCodecParams = parameters;
            transcoder.OutputCodecParams = parameters;
            return transcoder.Transcode(dataset);
        }
    }
}
