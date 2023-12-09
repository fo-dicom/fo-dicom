// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom
{

    public class DicomExceptionEventArgs : EventArgs
    {
        public readonly DicomException Exception;

        public DicomExceptionEventArgs(DicomException ex)
        {
            Exception = ex;
        }
    }

    /// <summary>Base type for all DICOM library exceptions.</summary>
    public abstract class DicomException : Exception
    {
        protected DicomException(string message)
            : base(message)
        {
            DicomExceptionConstructed(this);
        }

        protected DicomException(string message, Exception innerException)
            : base(message, innerException)
        {
            DicomExceptionConstructed(this);
        }

        internal static void DicomExceptionConstructed(DicomException ex)
        {
            if (OnException != null)
            {
                try
                {
                    OnException(ex, new DicomExceptionEventArgs(ex));
                }
                catch
                {
                }
            }
        }

        public static EventHandler<DicomExceptionEventArgs> OnException;
    }
}
