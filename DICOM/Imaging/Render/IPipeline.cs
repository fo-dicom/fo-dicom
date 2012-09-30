using Dicom;
using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render {
	/// <summary>
	/// Pipeline interface
	/// </summary>
	public interface IPipeline {
		/// <summary>
		/// Get the LUT of the pipeline 
		/// </summary>
		ILUT LUT {
			get;
		}
	}
}
