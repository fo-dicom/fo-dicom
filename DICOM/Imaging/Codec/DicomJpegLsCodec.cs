using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging.Codec {
	public enum DicomJpegLsInterleaveMode {
		None = 0,
		Line = 1,
		Sample = 2
	};

	public enum DicomJpegLsColorTransform {
		None = 0,
		HP1 = 1,
		HP2 = 2,
		HP3 = 3
	};

	public class DicomJpegLsParams : DicomCodecParams {
		private int _allowedError;
		private DicomJpegLsInterleaveMode _ilMode;
		private DicomJpegLsColorTransform _colorTransform;

		public DicomJpegLsParams() {
			_allowedError = 3;
			_ilMode = DicomJpegLsInterleaveMode.Line;
			_colorTransform = DicomJpegLsColorTransform.HP1;
		}

		public int AllowedError {
			get { return _allowedError; }
			set { _allowedError = value; }
		}

		public DicomJpegLsInterleaveMode InterleaveMode {
			get { return _ilMode; }
			set { _ilMode = value; }
		}

		public DicomJpegLsColorTransform ColorTransform {
			get { return _colorTransform; }
			set { _colorTransform = value; }
		}
	};

	public abstract class DicomJpegLsCodec : IDicomCodec {
		public string Name {
			get { return TransferSyntax.UID.Name; }
		}

		public abstract DicomTransferSyntax TransferSyntax {
			get;
		}

		public DicomCodecParams GetDefaultParameters() {
			return new DicomJpegLsParams();
		}

		public abstract void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters);
		public abstract void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters);
	};
}
