// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Interface for a DICOM C-STORE Service Class Provider.
    /// </summary>
    public interface IDicomCStoreProvider
    {
        /// <summary>
        /// Callback for each Sop Instance received.  The default implementation of
        /// DicomService is to write a temporary file that is automatically deleted
        /// for each SopInstance received.  See the documentation for DicomService.CreateCStoreReceiveStream()
        /// and DicomService.GetCStoreDicomFile() for information about changing this 
        /// behavior (e.g. writing to your own custom stream and avoiding the temporary file)
        /// </summary>
        /// <param name="request">C-STORE request.</param>
        /// <returns>C-STORE response.</returns>
        Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request);

        /// <summary>
        /// Callback for exceptions raised during the parsing of the received SopInstance.  Note that
        /// it is possible to avoid parsing the file by overriding DicomService.GetCStoreDicomFile() if
        /// desired.
        /// </summary>
        /// <param name="tempFileName">Name of the temporary file, may be null.</param>
        /// <param name="e">Thrown exception.</param>
        Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e);
    }
}
