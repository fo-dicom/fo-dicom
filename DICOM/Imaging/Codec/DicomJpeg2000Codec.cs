using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging.Codec {
	public class DicomJpeg2000Params : DicomCodecParams {
		private bool _irreversible;
		private int _rate;
		private int[] _rates;
		private bool _isVerbose;
		private bool _enableMct;
		private bool _updatePmi;
		private bool _signedAsUnsigned;

		public DicomJpeg2000Params() {
			_irreversible = true;
			_rate = 20;
			_isVerbose = false;
			_enableMct = true;
			_updatePmi = true;
			_signedAsUnsigned = false;

			_rates = new int[9];
			_rates[0] = 1280;
			_rates[1] = 640;
			_rates[2] = 320;
			_rates[3] = 160;
			_rates[4] = 80;
			_rates[5] = 40;
			_rates[6] = 20;
			_rates[7] = 10;
			_rates[8] = 5;
		}

		public bool Irreversible {
			get { return _irreversible; }
			set { _irreversible = value; }
		}

		public int Rate {
			get { return _rate; }
			set { _rate = value; }
		}

		public int[] RateLevels {
			get { return _rates; }
			set { _rates = value; }
		}

		public bool IsVerbose {
			get { return _isVerbose; }
			set { _isVerbose = value; }
		}

		public bool AllowMCT {
			get { return _enableMct; }
			set { _enableMct = value; }
		}

		public bool UpdatePhotometricInterpretation {
			get { return _updatePmi; }
			set { _updatePmi = value; }
		}

		public bool EncodeSignedPixelValuesAsUnsigned {
			get { return _signedAsUnsigned; }
			set { _signedAsUnsigned = value; }
		}
	}

	public abstract class DicomJpeg2000Codec : IDicomCodec {
		public string Name {
			get { return TransferSyntax.UID.Name; }
		}

		public abstract DicomTransferSyntax TransferSyntax {
			get;
		}

		public DicomCodecParams GetDefaultParameters() {
			return new DicomJpeg2000Params();
		}

		public abstract void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters);
		public abstract void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters);
	}
}
