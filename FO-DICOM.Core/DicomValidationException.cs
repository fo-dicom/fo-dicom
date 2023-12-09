// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom
{

    /// <summary>
    /// This exception is thrown by a validator. The validation is mostly triggert when adding string values to a DicomDataset that have to be parsed.
    /// </summary>
    public class DicomValidationException : DicomDataException
    {

        /// <summary>The string-content that does not validate.</summary>
        public string Content { get; private set; }

        /// <summary>The value representation that validates.</summary>
        public DicomVR VR { get; private set; }


        public DicomValidationException(string content, DicomVR vr, string message)
           : base(message)
        {
            Content = content;
            VR = vr;
        }


        public override string Message => $"Content \"{Content}\" does not validate VR {VR.Code}: {base.Message}";

    }
}
