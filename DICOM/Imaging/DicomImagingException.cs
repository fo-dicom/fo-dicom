using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging {
	/// <summary>
	/// <seealso cref="DicomImage"/> operations related exceptions
	/// </summary>
	public class DicomImagingException : DicomException {
		/// <summary>
		/// Initialize new instance of <seealso cref="DicomImagingException"/> class
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public DicomImagingException(string message) : base(message) {
		}

		/// <summary>
		/// Initialize new instance of <seealso cref="DicomImagingException"/> class
		/// </summary>
		/// <param name="format">The format string the describes the error</param>
		/// <param name="args">The format string parameters</param>
		public DicomImagingException(string format, params object[] args) : base(format, args) {
		}

		/// <summary>
		/// Initialize new instance of <seealso cref="DicomImagingException"/> class
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		/// <param name="innerException">The exception that is the cause of the current exception</param>
		public DicomImagingException(string message, Exception innerException) : base(message, innerException) {
		}
	}
}
