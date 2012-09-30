using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO;
using Dicom.IO.Buffer;

namespace Dicom.Imaging {
	/// <summary>
	/// Convert pixels from presentation from interleaved to planar and from planar to interleaved
	/// </summary>
	public static class PixelDataConverter {
		/// <summary>
		/// Convert 24 bits pixels from interleaved (RGB) to planar (RRR...GGG...BBB...)
		/// </summary>
		/// <param name="data">Pixels data in interleaved format (RGB)</param>
		/// <returns>Pixels data in planar format (RRR...GGG...BBB...)</returns>
		public static IByteBuffer InterleavedToPlanar24(IByteBuffer data) {
			byte[] oldPixels = data.Data;
			byte[] newPixels = new byte[oldPixels.Length];
			int pixelCount = newPixels.Length / 3;

			unchecked {
				for (int n = 0; n < pixelCount; n++) {
					newPixels[n + (pixelCount * 0)] = oldPixels[(n * 3) + 0];
					newPixels[n + (pixelCount * 1)] = oldPixels[(n * 3) + 1];
					newPixels[n + (pixelCount * 2)] = oldPixels[(n * 3) + 2];
				}
			}

			return new MemoryByteBuffer(newPixels);
		}

		/// <summary>
		/// Convert 24 bits pixels from planar (RRR...GGG...BBB...) to interleaved (RGB)
		/// </summary>
		/// <param name="data">Pixels data in planar format (RRR...GGG...BBB...)</param>
		/// <returns>Pixels data in interleaved format (RGB)</returns>
		public static IByteBuffer PlanarToInterleaved24(IByteBuffer data) {
			byte[] oldPixels = data.Data;
			byte[] newPixels = new byte[oldPixels.Length];
			int pixelCount = newPixels.Length / 3;

			unchecked {
				for (int n = 0; n < pixelCount; n++) {
					newPixels[(n * 3) + 0] = oldPixels[n + (pixelCount * 0)];
					newPixels[(n * 3) + 1] = oldPixels[n + (pixelCount * 1)];
					newPixels[(n * 3) + 2] = oldPixels[n + (pixelCount * 2)];
				}
			}
			
			return new MemoryByteBuffer(newPixels);
		}
	}
}
