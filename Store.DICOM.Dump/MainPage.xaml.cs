using System;
using System.IO;
using Dicom;
using Dicom.Imaging;
using Dicom.Network;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
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
        #region FIELDS

        public static readonly DependencyProperty DatasetProperty =
            DependencyProperty.Register("Dataset", typeof(DicomDataset), typeof(MainPage), new PropertyMetadata(null));

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(MainPage), new PropertyMetadata(null));
        
        #endregion

        public MainPage()
        {
            InitializeComponent();
        }

        #region PROPERTIES

        public DicomDataset Dataset
        {
            get { return (DicomDataset)GetValue(DatasetProperty); }
            set { SetValue(DatasetProperty, value); }
        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
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

        private async void OpenFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker { SuggestedStartLocation = PickerLocationId.DocumentsLibrary };
            filePicker.FileTypeFilter.Add(".dcm");
            filePicker.FileTypeFilter.Add(".dic");

            var file = await filePicker.PickSingleFileAsync();
            if (file == null) return;

            var storeStream = await file.OpenAsync(FileAccessMode.Read);
            var dicomFile = DicomFile.Open(storeStream.AsStream());
            Dataset = dicomFile.Dataset;

            if (!Dataset.Contains(DicomTag.PixelData)) return;
            Image = new DicomImage(Dataset).RenderImageSource();
        }

	    private void EchoButton_OnClick(object sender, RoutedEventArgs e)
	    {
		    var client = new DicomClient();
			client.NegotiateAsyncOps();
		    var request = new DicomCEchoRequest();
		    request.OnResponseReceived = (echoRequest, response) => EchoStatus.Text = response.Status.Description;
			client.AddRequest(request);
			client.Send("localhost", 104, false, "ANY-SCU", "STORESCP");
	    }
    }
}
