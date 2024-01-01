// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.Codec
{
    public enum ProgressionOrder
    {
        LRCP,
        RLCP, 
        RPCL, 
        PCRL, 
        CPRL
    }

    public class DicomHtJpeg2000Params : DicomCodecParams
    {
        public DicomHtJpeg2000Params()
        {
            Irreversible = true;
            NumberOfDecompositions = 5;
            EmployColorTransform = true;
            ProgressionOrder = ProgressionOrder.RPCL;
            InsertTlmMarkers = false;
        }

        /// <summary>
        /// Perform lossy compression using the 9/7 wavelet transform or
        /// perform lossless compression using the 5/3 wavelet transform.
        /// </summary>
        public bool Irreversible { get; set; }

        /// <summary>
        /// Number of decompositions.
        /// </summary>
        public int NumberOfDecompositions { get; set; }

        /// <summary>
        /// Employs a color transform,
        /// to transform RGB color images into the YUV domain.
        /// </summary>
        public bool EmployColorTransform { get; set; }

        /// <summary>
        /// Progression order.
        /// </summary>
        public ProgressionOrder ProgressionOrder { get; set; }

        /// <summary>
        /// Insert TLM markers.
        /// </summary>
        public bool InsertTlmMarkers { get; set; }
    }

    public abstract class DicomHtJpeg2000Codec : IDicomCodec
    {
        public string Name => TransferSyntax.UID.Name;

        public abstract DicomTransferSyntax TransferSyntax { get; }

        public DicomCodecParams GetDefaultParameters() => new DicomHtJpeg2000Params();

        public abstract void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters);

        public abstract void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters);
    }
}
