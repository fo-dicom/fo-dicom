using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public interface IDicomCStoreProvider {
        /// <summary>
        /// Callback for each Sop Instance received.  The default implementation of
        /// DicomService is to write a temporary file that is automatically deleted
        /// for each SopInstance received.  See the documentation for DicomService.CreateCStoreReceiveStream()
        /// and DicomService.GetCStoreDicomFile() for information about changing this 
        /// behavior (e.g. writing to your own custom stream and avoiding the temporary file)
        /// <returns></returns>
		DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request);
		
        /// <summary>
		/// Callback for exceptions raised during the parsing of the received SopInstance.  Note that
        /// it is possible to avoid parsing the file by overriding DicomService.GetCStoreDicomFile() if
        /// desired.
		/// </summary>
		/// <param name="tempFileName"></param>
		/// <param name="e"></param>
        void OnCStoreRequestException(string tempFileName, Exception e);
	}
}
