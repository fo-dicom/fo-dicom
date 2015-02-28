using System;
using System.Collections.ObjectModel;
using System.IO;
using Dicom;
using Dicom.IO.Buffer;
using Dicom.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.DICOM.Dump
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ViewDicomFileImagePage
	{
		#region FIELDS

		private readonly ObservableCollection<DicomTextItem> _textItems = new ObservableCollection<DicomTextItem>();
		private readonly ObservableCollection<ImageSource> _imageFrames = new ObservableCollection<ImageSource>();

		public static readonly DependencyProperty TransferSyntaxesProperty =
			DependencyProperty.Register("TransferSyntaxes", typeof(ReadOnlyObservableCollection<DicomTransferSyntax>),
										typeof(ViewDicomFileImagePage),
										new PropertyMetadata(new[]
			                                                     {
																	 DicomTransferSyntax.ExplicitVRLittleEndian,
																	 DicomTransferSyntax.ExplicitVRBigEndian,
																	 DicomTransferSyntax.ImplicitVRLittleEndian,
																	 DicomTransferSyntax.JPEGProcess1,
																	 DicomTransferSyntax.JPEGProcess2_4,
																	 DicomTransferSyntax.JPEGProcess14,
																	 DicomTransferSyntax.JPEGProcess14SV1,
				                                                     DicomTransferSyntax.JPEG2000Lossless,
				                                                     DicomTransferSyntax.JPEG2000Lossy,
				                                                     DicomTransferSyntax.JPEGLSLossless,
				                                                     DicomTransferSyntax.JPEGLSNearLossless,
				                                                     DicomTransferSyntax.RLELossless
			                                                     }));

		#endregion

		#region CONSTRUCTORS

		public ViewDicomFileImagePage()
		{
			InitializeComponent();
		}
		
		#endregion

		#region PROPERTIES

		public DicomFileImage FileImage { get; private set; }

		public ObservableCollection<DicomTextItem> TextItems { get { return _textItems; } }

		public ObservableCollection<ImageSource> ImageFrames { get { return _imageFrames; } }

		public ReadOnlyObservableCollection<DicomTransferSyntax> TransferSyntaxes
		{
			get { return (ReadOnlyObservableCollection<DicomTransferSyntax>)GetValue(TransferSyntaxesProperty); }
			set { SetValue(TransferSyntaxesProperty, value); }
		}

		#endregion

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.  The Parameter
		/// property is typically used to configure the page.</param>
		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			FileImage = e.Parameter as DicomFileImage;
			if (FileImage == null || FileImage.DicomFile == null)
			{
				Frame.GoBack();
				return;
			}

			try
			{
				_textItems.Clear();
				new DicomDatasetWalker(FileImage.DicomFile.FileMetaInfo).Walk(new DicomDumpWalker(_textItems));
				new DicomDatasetWalker(FileImage.DicomFile.Dataset).Walk(new DicomDumpWalker(_textItems));

				ImageFrames.Clear();
				if (FileImage.FirstFrameImage == null) return;

				await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ImageFrames.Add(FileImage.FirstFrameImage));
				for (var i = 1; i < FileImage.NumberOfFrames; ++i)
				{
					var frame = i;
					await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
					                          () => ImageFrames.Add(new DicomImage(FileImage.DicomFile.Dataset).RenderImageSource(frame)));
				}
			}
			catch
			{
				Frame.GoBack();
			}
		}

		private void OnBackButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.GoBack();
		}

		private async void SaveFileButtonOnTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
		{
			var savePicker = new FileSavePicker { SuggestedSaveFile = FileImage.StorageFile };
			savePicker.FileTypeChoices.Add("DICOM files", new[] { ".dcm", ".dic" });

			var file = await savePicker.PickSaveFileAsync();
			using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
			{
				FileImage.DicomFile.Save(stream.AsStreamForWrite());
			}
		}

		private void TransferSyntaxButtonOnTapped(object sender, TappedRoutedEventArgs e)
		{
			TransferSyntaxPopup.IsOpen = true;
		}

		private void TransferSyntaxesViewOnItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				FileImage.ChangeTransferSyntax(e.ClickedItem as DicomTransferSyntax);
			}
			catch
			{
			}
			TransferSyntaxPopup.IsOpen = false;
			BottomAppBar.IsOpen = false;
		}
	}
}
