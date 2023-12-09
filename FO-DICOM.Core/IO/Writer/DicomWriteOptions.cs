// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.IO.Writer
{

    public class DicomWriteOptions
    {

        public DicomWriteOptions()
        {
            ExplicitLengthSequences = false;
            ExplicitLengthSequenceItems = false;
            KeepGroupLengths = false;
            LargeObjectSize = 1024 * 1024;
        }

        public DicomWriteOptions(DicomWriteOptions options)
        {
            ExplicitLengthSequences = options.ExplicitLengthSequences;
            ExplicitLengthSequenceItems = options.ExplicitLengthSequenceItems;
            KeepGroupLengths = options.KeepGroupLengths;
            LargeObjectSize = options.LargeObjectSize;
        }

        private static DicomWriteOptions _default;

        public static DicomWriteOptions Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new DicomWriteOptions();
                }
                return _default;
            }
        }

        public bool ExplicitLengthSequences { get; set; }

        public bool ExplicitLengthSequenceItems { get; set; }

        public bool KeepGroupLengths { get; set; }

        public uint LargeObjectSize { get; set; }
    }
}
