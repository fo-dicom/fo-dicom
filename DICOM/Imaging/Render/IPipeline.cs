using Dicom;
using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render {
	public interface IPipeline {
		ILUT LUT {
			get;
		}
	}
}
