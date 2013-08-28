using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Dicom;
using Dicom.Imaging;
using Microsoft.Phone.Shell;
using Phone.DICOM.Viewer.Resources;
using Store.DICOM.Dump;
using Windows.Storage;

namespace Phone.DICOM.Viewer
{
	public partial class MainPage
	{
		private readonly ObservableCollection<DicomTextItem> _textItems = new ObservableCollection<DicomTextItem>();

		// Using a DependencyProperty as the backing store for DicomImageSource.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DicomImageSourceProperty =
			DependencyProperty.Register("DicomImageSource", typeof(ImageSource), typeof(MainPage), new PropertyMetadata(null));
	
		// Constructor
		public MainPage()
		{
			InitializeComponent();
		}

		public ImageSource DicomImageSource
		{
			get { return (ImageSource)GetValue(DicomImageSourceProperty); }
			set { SetValue(DicomImageSourceProperty, value); }
		}

		public ObservableCollection<DicomTextItem> TextItems { get { return _textItems; } }

		private void DownloadButtonOnClick(object sender, EventArgs e)
		{
		}

		private void SettingsButtonOnClick(object sender, EventArgs eventArgs)
		{
		}

		private async void MainPageOnLoaded(object sender, RoutedEventArgs e)
		{
			var downloadButton = ApplicationBar.Buttons.OfType<ApplicationBarIconButton>().SingleOrDefault(btn => btn.Text.Equals("DownloadButton"));
			if (downloadButton != null) downloadButton.Text = AppResources.AppBarDownloadButtonText;

			var settingsButton = ApplicationBar.Buttons.OfType<ApplicationBarIconButton>().SingleOrDefault(btn => btn.Text.Equals("SettingsButton"));
			if (settingsButton != null) settingsButton.Text = AppResources.AppBarSettingsButtonText;

			var uri = new Uri("ms-appx:///Images/CT-MONO2-16-ankle.dcm");
			var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
			var stream = (await file.OpenAsync(FileAccessMode.Read)).AsStream();

			var dicomObj = DicomFile.Open(stream);

			_textItems.Clear();
			new DicomDatasetWalker(dicomObj.FileMetaInfo).Walk(new DicomDumpWalker(_textItems));
			new DicomDatasetWalker(dicomObj.Dataset).Walk(new DicomDumpWalker(_textItems));

			var dicomImage = new DicomImage(dicomObj.Dataset);
			DicomImageSource = dicomImage.RenderImageSource();
		}
	}
}