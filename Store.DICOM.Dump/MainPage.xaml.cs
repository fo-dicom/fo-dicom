using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Dicom;
using Dicom.Imaging;
using Dicom.Network;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.DICOM.Dump
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
	{
		#region INNER CLASSES

		public class DicomFileImage
		{
			#region CONSTRUCTORS

			public DicomFileImage(string displayName, DicomFile file)
			{
				DisplayName = displayName;
				File = file;

				var dataset = file.Dataset;
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
			public DicomFile File { get; private set; }
			public ImageSource FirstFrameImage { get; private set; }
			public string Modality { get; private set; }
			public int NumberOfFrames { get; private set; }

			#endregion
		}

		public class ModalityGroup
		{
			public string Modality { get; set; }
			public ICollection<DicomFileImage> FileImages { get; set; }
		}

		#endregion

		#region FIELDS

	    private readonly ObservableCollection<ModalityGroup> _modalityGroups =
		    new ObservableCollection<ModalityGroup>();
 
		private DicomServer<CEchoScp> _server;

	    public static readonly DependencyProperty TransferSyntaxesProperty =
		    DependencyProperty.Register("TransferSyntaxes", typeof(ReadOnlyObservableCollection<DicomTransferSyntax>),
		                                typeof(MainPage),
		                                new PropertyMetadata(new[]
			                                                     {
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

		public MainPage()
		{
			InitializeComponent();
		}
		
		#endregion

        #region PROPERTIES

	    public ObservableCollection<ModalityGroup> ModalityGroups
	    {
			get { return _modalityGroups; }
	    }

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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void OpenFileButtonOnTapped(object sender, TappedRoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker { SuggestedStartLocation = PickerLocationId.DocumentsLibrary };
            filePicker.FileTypeFilter.Add("*");

	        var files = await filePicker.PickMultipleFilesAsync();
			if (files == null || files.Count == 0) return;

			_modalityGroups.Clear();
			foreach (var file in files)
	        {
		        try
		        {
					var storeStream = await file.OpenAsync(FileAccessMode.Read);
					var dicomFile = DicomFile.Open(storeStream.AsStream());
			        var fileImage = new DicomFileImage(Path.GetFileNameWithoutExtension(file.Path), dicomFile);

			        var modality = fileImage.Modality;
			        var modalityGroup = _modalityGroups.FirstOrDefault(m => m.Modality.Equals(modality));
					if (modalityGroup == null)
					{
						_modalityGroups.Add(new ModalityGroup { Modality = modality, FileImages = new ObservableCollection<DicomFileImage> { fileImage }});
					}
					else
					{
						modalityGroup.FileImages.Add(fileImage);
					}
		        }
		        catch
		        {
		        }
	        }

		}

		private void DicomEchoButtonOnTapped(object sender, TappedRoutedEventArgs e)
	    {
		    var client = new DicomClient();
			var request = new DicomCEchoRequest
				              {
					              OnResponseReceived =
						              async (req, res) =>
						              await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
						                                        () => EchoStatus.Text = res.Status.Description)
				              };
		    client.AddRequest(request);
			client.Send("server", 104, false, "cureos", "cureos");
	    }

	    private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
	    {
			_server = new DicomServer<CEchoScp>(104);
	    }
/*
	    private void TransferSyntaxBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
			if (Image == null) return;
			try
			{
				Dataset = Dataset.ChangeTransferSyntax(TransferSyntaxBox.SelectedItem as DicomTransferSyntax);
				Image = new DicomImage(Dataset).RenderImageSource();
				ErrorMessage.Text = String.Empty;
			}
			catch (Exception exception)
			{
				ErrorMessage.Text = exception.Message + Environment.NewLine + exception.StackTrace;
			} 
		}
 */

	    private void ViewDicomFileButtonOnTapped(object sender, TappedRoutedEventArgs e)
	    {
			if (DicomFilesView.SelectedItem == null) return;
		    Frame.Navigate(typeof(ViewDicomFileImagePage), DicomFilesView.SelectedItem);
	    }
	}
}
