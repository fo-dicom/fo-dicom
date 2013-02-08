using System;
using Dicom;
using Dicom.Imaging;
using Dicom.Imaging.Codec;
using Windows.Storage;
using Windows.UI.Xaml.Media;

namespace Store.DICOM.Dump
{
	public class DicomFileImage
	{
		#region CONSTRUCTORS

		public DicomFileImage(string displayName, StorageFile storageFile, DicomFile dicomFile)
		{
			DisplayName = displayName;
			StorageFile = storageFile;
			DicomFile = dicomFile;

			var dataset = dicomFile.Dataset;
			if (dataset != null)
			{
				FirstFrameImage = dataset.Contains(DicomTag.PixelData)
					                  ? new DicomImage(dataset).RenderImageSource()
					                  : null;
				Modality = dataset.Get(DicomTag.Modality, String.Empty);
				NumberOfFrames = FirstFrameImage != null ? dataset.Get(DicomTag.NumberOfFrames, 1) : 0;
			}
			else
			{
				FirstFrameImage = null;
				Modality = String.Empty;
				NumberOfFrames = 0;
			}
		}

		#endregion

		#region PROPERTIES

		public string DisplayName { get; private set; }
		public StorageFile StorageFile { get; private set; }
		public DicomFile DicomFile { get; private set; }
		public ImageSource FirstFrameImage { get; private set; }
		public string Modality { get; private set; }
		public int NumberOfFrames { get; private set; }

		#endregion

		public bool ChangeTransferSyntax(DicomTransferSyntax newTransferSyntax)
		{
			try
			{
				DicomFile = DicomFile.ChangeTransferSyntax(newTransferSyntax);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}