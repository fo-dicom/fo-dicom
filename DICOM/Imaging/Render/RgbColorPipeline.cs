using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render {
	/// <summary>
	/// RGB color pipeline implementation of <seealso cref="IPipeline"/> interface
	/// </summary>
	public class RgbColorPipeline : IPipeline {
		public ILUT LUT {
			get { return null; }
		}
	}
}
